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
using System.Windows.Forms;

namespace AwareServer
{
    
     class Program
    {
        /// <summary>
        /// Launching graphical elements of the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());

        }

        static Service service = new Service();
        private  Thread eCommerceConnectionThread;
        public void StartServer()
        {
            try
            {
                // Fetching data from Magento
                DataFetch dataFetch = new DataFetch();
                eCommerceConnectionThread = new Thread(new ThreadStart(dataFetch.Initialize));
                eCommerceConnectionThread.Name = "MagentoThread";
                eCommerceConnectionThread.Start();

                // Starts listening for connections
                AsynchronousSocketListener.Start();
            }
            catch (Exception e)
            {
                ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                service.InsertException(log);
            }

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
        //Not in use
        public void StopServer()
        {
            AsynchronousSocketListener.Stop();
            eCommerceConnectionThread.Abort();
        }
        
    }
}
