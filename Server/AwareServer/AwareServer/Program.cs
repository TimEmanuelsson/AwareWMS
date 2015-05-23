using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Web.Script.Serialization;
using System.Threading;
using AwareClassLibrary;
using Repository.Model;

namespace AwareServer
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static Service service = new Service();

        public static int Main(String[] args)
        {
            try
            {
                DataFetch dataFetch = new DataFetch();
                Thread eCommerceConnectionThread = new Thread(new ThreadStart(dataFetch.Initialize));
                eCommerceConnectionThread.Name = "MagentoThread";
                eCommerceConnectionThread.Start();

                AsynchronousSocketListener.Start();
            }
            catch (Exception e)
            {
                ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                service.InsertException(log);
            }
            return 0;

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
