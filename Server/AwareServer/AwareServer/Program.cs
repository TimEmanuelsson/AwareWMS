using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.Web.Script.Serialization;

namespace AwareServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public class HttpProcessor
        {
            public TcpClient socket;
            public HttpServer srv;

            private Stream inputStream;
            public StreamWriter outputStream;

            public String http_method;
            public String http_url;
            public String http_protocol_versionstring;
            public Hashtable httpHeaders = new Hashtable();


            private static int MAX_POST_SIZE = 10 * 1024 * 1024; // 10MB

            public HttpProcessor(TcpClient s, HttpServer srv)
            {
                this.socket = s;
                this.srv = srv;
            }


            private string streamReadLine(Stream inputStream)
            {
                int next_char;
                string data = "";
                while (true)
                {
                    next_char = inputStream.ReadByte();
                    if (next_char == '\n') { break; }
                    if (next_char == '\r') { continue; }
                    if (next_char == -1) { Thread.Sleep(1); continue; };
                    data += Convert.ToChar(next_char);
                }
                return data;
            }
            public void process()
            {
                // we can't use a StreamReader for input, because it buffers up extra data on us inside it's
                // "processed" view of the world, and we want the data raw after the headers
                inputStream = new BufferedStream(socket.GetStream());

                // we probably shouldn't be using a streamwriter for all output from handlers either
                outputStream = new StreamWriter(new BufferedStream(socket.GetStream()));
                try
                {
                    parseRequest();
                    readHeaders();
                    if (http_method.Equals("GET"))
                    {
                        handleGETRequest();
                    }
                    else if (http_method.Equals("POST"))
                    {
                        handlePOSTRequest();
                    }
                }
                catch (Exception e)
                {
                    writeFailure();
                }
                try
                {
                    outputStream.Flush();
                    // bs.Flush(); // flush any remaining output
                    inputStream = null; outputStream = null; // bs = null;            
                    socket.Close();
                }
                catch (IOException e)
                {
                    //
                }
            }

            public void parseRequest()
            {
                String request = streamReadLine(inputStream);
                string[] tokens = request.Split(' ');
                if (tokens.Length != 3)
                {
                    throw new Exception("invalid http request line");
                }
                http_method = tokens[0].ToUpper();
                http_url = tokens[1];
                http_protocol_versionstring = tokens[2];
                if (http_protocol_versionstring != "HTTP/1.0")
                {
                    throw new Exception("invalid http protocol version");
                }

                Console.WriteLine("starting: " + request);
            }

            public void readHeaders()
            {
                Console.WriteLine("readHeaders()");
                String line;
                while ((line = streamReadLine(inputStream)) != null)
                {
                    if (line.Equals(""))
                    {
                        Console.WriteLine("got headers");
                        return;
                    }

                    int separator = line.IndexOf(':');
                    if (separator == -1)
                    {
                        throw new Exception("invalid http header line: " + line);
                    }
                    String name = line.Substring(0, separator);
                    int pos = separator + 1;
                    while ((pos < line.Length) && (line[pos] == ' '))
                    {
                        pos++; // strip any spaces
                    }

                    string value = line.Substring(pos, line.Length - pos);
                    Console.WriteLine("header: {0}:{1}", name, value);
                    httpHeaders[name] = value;
                }
            }

            public void handleGETRequest()
            {
                srv.handleGETRequest(this);
            }

            private const int BUF_SIZE = 4096;
            public void handlePOSTRequest()
            {
                // this post data processing just reads everything into a memory stream.
                // this is fine for smallish things, but for large stuff we should really
                // hand an input stream to the request processor. However, the input stream 
                // we hand him needs to let him see the "end of the stream" at this content 
                // length, because otherwise he won't know when he's seen it all! 

                Console.WriteLine("get post data start");
                int content_len = 0;
                MemoryStream ms = new MemoryStream();
                if (this.httpHeaders.ContainsKey("Content-Length"))
                {
                    content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);
                    if (content_len > MAX_POST_SIZE)
                    {
                        throw new Exception(
                            String.Format("POST Content-Length({0}) too big for this simple server",
                              content_len));
                    }
                    byte[] buf = new byte[BUF_SIZE];
                    int to_read = content_len;
                    while (to_read > 0)
                    {
                        Console.WriteLine("starting Read, to_read={0}", to_read);

                        int numread = this.inputStream.Read(buf, 0, Math.Min(BUF_SIZE, to_read));
                        Console.WriteLine("read finished, numread={0}", numread);
                        if (numread == 0)
                        {
                            if (to_read == 0)
                            {
                                break;
                            }
                            else
                            {
                                throw new Exception("client disconnected during post");
                            }
                        }
                        to_read -= numread;
                        ms.Write(buf, 0, numread);
                    }
                    ms.Seek(0, SeekOrigin.Begin);
                }
                Console.WriteLine("get post data end");
                srv.handlePOSTRequest(this, new StreamReader(ms));

            }

            public void writeSuccess(string content_type = "text/html")
            {
                //// this is the successful HTTP response line
                outputStream.WriteLine("HTTP/1.0 200 OK");
                //// these are the HTTP headers...          
                outputStream.WriteLine("Content-Type: " + content_type);
                outputStream.WriteLine("Current Time: " + DateTime.Now.ToString());
                outputStream.WriteLine("Request URI : {0}", http_url);
                outputStream.WriteLine("Connection: close");
                // ..add your own headers here if you like

                outputStream.WriteLine(""); // this terminates the HTTP headers.. everything after this is HTTP body..
            }

        public void writeFailure()
            {
                // this is an http 404 failure response
                outputStream.WriteLine("HTTP/1.0 404 File not found");
                // these are the HTTP headers
                outputStream.WriteLine("Connection: close");
                // ..add your own headers here

                outputStream.WriteLine(""); // this terminates the HTTP headers.
            }
        }

        public abstract class HttpServer
        {

            protected int port;
            TcpListener listener;
            bool is_active = true;

            public HttpServer(int port)
            {
                this.port = port;
            }

            public void listen()
            {
                listener = new TcpListener(IPAddress.Parse("192.168.1.3"), port);
                listener.Start();
                while (is_active)
                {
                    TcpClient s = listener.AcceptTcpClient();
                    HttpProcessor processor = new HttpProcessor(s, this);
                    Thread thread = new Thread(new ThreadStart(processor.process));
                    thread.Start();
                    Thread.Sleep(1);
                }
            }

            public abstract void handleGETRequest(HttpProcessor p);
            public abstract void handlePOSTRequest(HttpProcessor p, StreamReader inputData);
        }

        public class MyHttpServer : HttpServer
        {
            public MyHttpServer(int port)
                : base(port)
            {
            }
            public override void handleGETRequest(HttpProcessor p)
            {
                DatabaseRequest dbReq = new DatabaseRequest();
                string ret = "";

                // Exempel på att ta emot bilder
                if (p.http_url.Equals("/Test.png"))
                {
                    Stream fs = File.Open("../../Test.png", FileMode.Open);

                    p.writeSuccess("image/png");
                    fs.CopyTo(p.outputStream.BaseStream);
                    p.outputStream.BaseStream.Flush();
                }

                if (p.http_url.Equals("/orders"))
                {
                    ret = dbReq.GetOrders();

                    if (ret == null || ret == "null")
                    {
                        ret = "";
                        p.writeFailure();
                    }
                    else
                    {
                        p.writeSuccess("application/json");
                    }

                }

                if (p.http_url.Equals("/products"))
                {
                    ret = dbReq.GetProducts();

                    if (ret == null || ret == "null")
                    {
                        ret = "";
                        p.writeFailure();
                    }
                    else
                    {
                        p.writeSuccess("application/json");
                    }
                }

                if (p.http_url.Contains("/orders?id="))
                {
                    try
                    {
                        Char[] split = { '=' };
                        string[] strParts = p.http_url.Split(split);
                        int id = int.Parse(strParts[1]);

                        ret = dbReq.GetOrderById(id);
                        if (ret == null || ret == "null")
                        {
                            ret = "";
                            p.writeFailure();
                        }
                        else
                        {
                            p.writeSuccess("application/json");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR: {0}", e);
                    }
                }

                if (p.http_url.Contains("/products?id="))
                {
                    try
                    {
                        Char[] split = { '=' };
                        string[] strParts = p.http_url.Split(split);
                        int id = int.Parse(strParts[1]);

                        ret = dbReq.GetProductById(id);
                        if (ret == null || ret == "null")
                        {
                            ret = "";
                            p.writeFailure();
                        }
                        else
                        {
                            p.writeSuccess("application/json");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR: {0}", e);
                    }
                }

                if (p.http_url.Contains("/customers?id="))
                {
                    try
                    {
                        Char[] split = { '=' };
                        string[] strParts = p.http_url.Split(split);
                        int id = int.Parse(strParts[1]);

                        ret = dbReq.GetCustomerById(id);
                        if (ret == null || ret == "null")
                        {
                            ret = "";
                            p.writeFailure();
                        }
                        else
                        {
                            p.writeSuccess("application/json");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR: {0}", e);
                    }
                }

                if (p.http_url.Contains("/products?barcodenumber="))
                {
                    try
                    {
                        Char[] split = { '=' };
                        string[] strParts = p.http_url.Split(split);
                        int barcodenumber = int.Parse(strParts[1]);

                        ret = dbReq.GetProductByBarcodeNumber(barcodenumber);
                        if (ret == null || ret == "null")
                        {
                            ret = "";
                            p.writeFailure();
                        }
                        else
                        {
                            p.writeSuccess("application/json");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR: {0}", e);
                    }
                }

                Console.WriteLine("request: {0}", p.http_url);

                p.outputStream.WriteLine(ret);
                p.outputStream.WriteLine("");
                //p.outputStream.WriteLine("<form method=post action=/form>");
                //p.outputStream.WriteLine("<input type=text name=foo value=foovalue>");
                //p.outputStream.WriteLine("<input type=submit name=bar value=barvalue>");
                //p.outputStream.WriteLine("</form>");
            }

            public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
            {
                Console.WriteLine("POST request: {0}", p.http_url);
                string data = inputData.ReadToEnd();

                p.writeSuccess();
                p.outputStream.WriteLine("<html><body><h1>test server</h1>");
                p.outputStream.WriteLine("<a href=/test>return</a><p>");
                p.outputStream.WriteLine("postbody: <pre>{0}</pre>", data);


            }
        }

        public class DatabaseRequest
        {
            private Repository.Model.Service service = null;
            private JavaScriptSerializer serializer;
            private string json = "";

            public DatabaseRequest()
            {
                service = new Repository.Model.Service();
                serializer = new JavaScriptSerializer();
            }
            public string GetOrders()
            {
                IEnumerable<AwareClassLibrary.Order> orders = service.GetOrders();
                int i = 0;
                foreach (AwareClassLibrary.Order order in orders)
                {
                    if (i > 0)
                    {
                        json += "#";
                    }
                    json += serializer.Serialize(order);

                    i++;
                }
                return json;
            }

            public string GetProducts()
            {
                IEnumerable<AwareClassLibrary.Product> products = service.GetProducts();
                int i = 0;
                foreach (AwareClassLibrary.Product product in products)
                {
                    if (i > 0)
                    {
                        json += "#";
                    }
                    json += serializer.Serialize(product);

                    i++;
                }
                return json;
            }

            public string GetOrderById(int id)
            {
                AwareClassLibrary.Order order = service.GetOrderById(id);
                json = serializer.Serialize(order);
                return json;
            }

            public string GetProductById(int id)
            {
                AwareClassLibrary.Product product = service.GetProductById(id);
                json = serializer.Serialize(product);
                return json;
            }

            public string GetProductByBarcodeNumber(int barcodenumber)
            {
                AwareClassLibrary.Product product = service.GetProductByBarcodeNumber(barcodenumber);
                json = serializer.Serialize(product);
                return json;
            }

            public string GetCustomerById(int id)
            {
                AwareClassLibrary.Customer customer = service.GetCustomerById(id);
                json = serializer.Serialize(customer);
                return json;
            }
        }

        public class TestMain
        {
            public static int Main(String[] args)
            {
                HttpServer httpServer;
                if (args.GetLength(0) > 0)
                {
                    httpServer = new MyHttpServer(Convert.ToInt16(args[0]));
                }
                else
                {
                    httpServer = new MyHttpServer(11000);
                }
                Thread thread = new Thread(new ThreadStart(httpServer.listen));
                thread.Start();
                Console.WriteLine("Waiting for a connection..");
                return 0;
            }

        }

        /*
        static void Main()
        {
            HttpListener listener = new HttpListener();
            listener.Start();
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;

            //request.

            // Temporary test code:
            DataFetch dataFetch = new DataFetch();
            dataFetch.FetchAndInsert();
            // End of temporary test code.


            // Service related code below, uncomment when we can install this shit.
            /*
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ServerService() 
            };
            ServiceBase.Run(ServicesToRun);
            
        }
         */

    }
}
