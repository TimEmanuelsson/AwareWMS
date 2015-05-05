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
        
        public static int Main(String[] args)
        {
            AsynchronousSocketListener.Start();
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
