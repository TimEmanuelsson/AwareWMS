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
    public partial class StocktakingView : Form
    {
        public StocktakingView()
        {
            InitializeComponent();
        }
        private AsynchronousClient async = new AsynchronousClient();
        private void ShowProducts_Click(object sender, EventArgs e)
        {
            ShowProductsButton.Enabled = false;
            ShowProductsPanel.Visible = true;
            ShowProductsPanel.Enabled = true;
            //async.StartClient("GET/products");
            //string response = async.Response;
            //TableView.Visible = true;
            //products = JsonConvert.DeserializeObject<List<Product>>(response);

            //TableView.AutoSize = true;
            //TableView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            //foreach (Product item in products)
            //{
            //    var bindinglist = new BindingList<Product>(products);
            //    var source = new BindingSource(bindinglist, null);
            //    TableView.DataSource = source;
            //}
            TableView.Columns[0].Name = "column0";
            TableView.Columns[1].Name = "column1";
            TableView.Columns[2].Name = "column2";
            TableView.Columns[3].Name = "column3";
            TableView.Columns[4].Name = "column4";
            TableView.Columns[5].Name = "column5";
            TableView.Columns[6].Name = "column6";
             row1 = new string[] { "row1", "row2", "row3", "row4", "row5", "row6", "row7" };
            TableView.Rows.Add(row1);
            TableView.Visible = true;
        }
        private string[] row1;

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

        private void SearchField_TextChanged(object sender, EventArgs e)
        {
            //BindingSource bs = new BindingSource();
            //bs.DataSource = TableView.DataSource;
            //bs.Filter = TableView.Columns[6].HeaderText.ToString() + " LIKE '%" + SearchField.Text + "%'";
            //TableView.DataSource = bs;
          
        }

        private void ViewOrders_Click(object sender, EventArgs e)
        {
            ShowProductsPanel.Visible = false;
            ShowProductsPanel.Enabled = false;
            ShowProductsButton.Enabled = true;
        }

        private void Stocktaking_Click(object sender, EventArgs e)
        {
            ShowProductsPanel.Visible = false;
            ShowProductsPanel.Enabled = false;
            ShowProductsButton.Enabled = true;
        }

    }
}
