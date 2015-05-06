using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AwareComputerClient.Model;
using Newtonsoft.Json;

namespace AwareComputerClient.View
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();
        }
        private List<Product> products;
        private void button1_Click(object sender, EventArgs e)
        {
            AsynchronousClient async = new AsynchronousClient();
            string response = async.Response;
            
            products = JsonConvert.DeserializeObject<List<Product>>(response);
            
            foreach (Product item in products)
            {
                
                TableView.AutoSize = true;
                var bindinglist = new BindingList<Product>(products);
                var source = new BindingSource(bindinglist, null);
                TableView.DataSource = source;
            }
        }

    }
}
