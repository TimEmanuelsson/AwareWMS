using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AwareServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            HttpListener listener = new HttpListener();
            listener.Start();
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;

            request.

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
            */
        }
    }
}
