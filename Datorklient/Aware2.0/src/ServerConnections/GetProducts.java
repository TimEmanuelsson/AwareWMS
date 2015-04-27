package ServerConnections;



import java.io.BufferedWriter;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;

/**
 * Created by Jocke on 2015-04-15.
 */


 public class GetProducts  {


            String response = "";
            private Socket socket = null;
            private String JSONResp;
            private String stringToGetProducts;
            
            	public String getAllProducts() {
                try {

                    OpenConnection Oc = new OpenConnection();
                    socket = Oc.establish();
                    PrintWriter out = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())), true);

                    out.println(stringToGetProducts);
                    out.flush();
                    ByteArrayOutputStream byteArrayOutputStream =
                            new ByteArrayOutputStream(10024);
                    byte[] buffer = new byte[10024];

                    int bytesRead;
                    InputStream inputStream = socket.getInputStream();


                    while ((bytesRead = inputStream.read(buffer)) != -1) {
                        byteArrayOutputStream.write(buffer, 0, bytesRead);
                        response += byteArrayOutputStream.toString("UTF-8");

                    }
                        JSONResp = new String (byteArrayOutputStream.toByteArray());

                } catch (UnknownHostException e) {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                    response = "UnknownHostException: " + e.toString();
                } catch (IOException e) {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                    response = "IOException: " + e.toString();
                } finally {
                    if (socket != null) {
                        try {
                            socket.close();
                        } catch (IOException e) {
                            // TODO Auto-generated catch block
                            e.printStackTrace();
                        }
                    }
                }
                return JSONResp;
            }
                public void storeConnectionString(String stringToGetProducts){
                    this.stringToGetProducts = stringToGetProducts;
                }
}