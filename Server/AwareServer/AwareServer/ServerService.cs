using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AwareServer
{
    public partial class ServerService : ServiceBase
    {
        public ServerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Do the stuff in the Program.cs Main().
        }

        protected override void OnStop()
        {
        }
    }
}
