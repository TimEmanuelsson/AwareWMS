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
    public partial class MainView : MenuView
    {
        public MainView()
        {
            InitializeComponent();
        }
        private AsynchronousClient async = new AsynchronousClient();
        private List<Product> products;
        private void ShowProducts_Click()
        {
            
           // ShowProductsButton.Enabled = false;
           // TableView.ColumnHeaderMouseClick
            TableViewPanel.Visible = true;
            ShowProductsMainContainer.Visible = true;
            async.StartClient("GET/products");
            string response = async.Response;
            TableView.Visible = true;
            products = JsonConvert.DeserializeObject<List<Product>>(response);

            TableView.AutoSize = true;
            //TableView.ColumnHeadersVisible = false;
            foreach (Product item in products)
            {
                var bindinglist = new BindingList<Product>(products);
                var source = new BindingSource(bindinglist, null);
                TableView.DataSource = source;
            }

            //TableView.Columns[0].Name = "column0";
            //TableView.Columns[1].Name = "column1";
            //TableView.Columns[2].Name = "column2";
            //TableView.Columns[3].Name = "column3";
            //TableView.Columns[4].Name = "column4";
            //TableView.Columns[5].Name = "column5";
            //TableView.Columns[6].Name = "column6";
            // row1 = new string[] { "row1", "row2", "row3", "row4", "row5", "row6", "row7" };
            //TableView.Rows.Add(row1);
            //TableView.Visible = true;
        }
        private string[] row1;
        private string g;
        

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
           
        private void TableView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var cellRowIndex = TableView.SelectedCells[0].RowIndex;
            int cellColumnIndex = TableView.ColumnCount -1;
           // var cellcollection = TableView.Rows[cellRowIndex].Cells[0];  
            
            NameLabel.Text = TableView.Rows[cellRowIndex].Cells[0].Value.ToString();
            SKULabel.Text = TableView.Rows[cellRowIndex].Cells[1].Value.ToString();
            QuantityLabel.Text = TableView.Rows[cellRowIndex].Cells[2].Value.ToString();
            WeightLabel.Text = TableView.Rows[cellRowIndex].Cells[3].Value.ToString();
            StorageSpaceLabel.Text = TableView.Rows[cellRowIndex].Cells[4].Value.ToString();
            BarcodeNumberLabel.Text = TableView.Rows[cellRowIndex].Cells[5].Value.ToString();
            ImageLocationLabel.Text = TableView.Rows[cellRowIndex].Cells[6].Value.ToString();
        }
        //TODO:: All Edit Stora Bokstäver
        private void SearchField_TextChanged(object sender, EventArgs e)
        {
            //Looks using the Product object class, if it contains any character withing the searchfield, filter.
            BindingList<Product> filtered = new BindingList<Product>(products.Where(p => p.Name.Contains(SearchField.Text)).ToList());
            TableView.DataSource = filtered;
        }
        protected override void ShowProductsButton_Click(object sender, EventArgs e)
        {
            ShowProducts_Click();
        }
        protected override void ViewOrders_Click(object sender, EventArgs e)
        {
            //OrderView s = new OrderView();

            ShowProductsMainContainer.Visible = false;
            //ShowProductsButton.Enabled = true;
        }

        protected override void Stocktaking_Click(object sender, EventArgs e)
        {
           // ShowProductsButton.Enabled = true;
        }

        

    }
}
