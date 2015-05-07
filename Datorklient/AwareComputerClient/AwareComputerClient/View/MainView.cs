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
        private void ShowProducts_Click(object sender, EventArgs e)
        {
            AsynchronousClient async = new AsynchronousClient();
            string response = async.Response;
            TableView.Visible = true;
            products = JsonConvert.DeserializeObject<List<Product>>(response);

            foreach (Product item in products)
            {

                TableView.AutoSize = true;
                var bindinglist = new BindingList<Product>(products);
                var source = new BindingSource(bindinglist, null);
                TableView.DataSource = source;
            }
            
            
        }


        private void FileMenuContainer_Click(object sender, EventArgs e)
        {
            //När File klickas på
        }

        private void FileMenuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private string s;

        private void TableView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var cellRowIndex = TableView.SelectedCells[0].RowIndex;
            int cellColumnIndex = TableView.ColumnCount -1;
            var cellcollection = TableView.Rows[cellRowIndex].Cells[0];


            for (int i = 0; i <= cellColumnIndex; i++)
            {
                s += TableView.Rows[cellRowIndex].Cells[i].Value.ToString();

            }
            MessageBox.Show(s.ToString());
            s = "";
        }

    }
}
