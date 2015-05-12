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
using System.Collections.ObjectModel;

namespace AwareComputerClient.View
{
    public partial class MainView : MenuView
    {
        public MainView()
        {
            InitializeComponent();
        }
        private AsynchronousClient async;
        private List<Product> products;
        private BindingList<Product> bindinglist;
        private void ShowProducts_Click()
        {
            async = new AsynchronousClient();
           // ShowProductsButton.Enabled = false;
           // TableView.ColumnHeaderMouseClick
            TableViewPanel.Visible = true;
            ShowProductsMainContainer.Visible = true;
            async.StartClient("GET/products");
            string response = async.Response;
            TableView.Visible = true;
            products = JsonConvert.DeserializeObject<List<Product>>(response);

            TableView.AutoSize = true;

        
                bindinglist = new BindingList<Product>(products);
                var source = new BindingSource(bindinglist, null);
                TableView.DataSource = source;
            

                
            //TableView.ColumnHeadersVisible = false;

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
            //private string[] row1;
        
        

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
            int cellRowIndex = TableView.SelectedCells[0].RowIndex;
            int cellColumnIndex = TableView.ColumnCount -1;
           // var cellcollection = TableView.Rows[cellRowIndex].Cells[0];  
            ProductIdLabel.Text = TableView.Rows[cellRowIndex].Cells[0].Value.ToString();
            NameLabel.Text = TableView.Rows[cellRowIndex].Cells[1].Value.ToString();
            SKULabel.Text = TableView.Rows[cellRowIndex].Cells[2].Value.ToString();
            QuantityLabel.Text = TableView.Rows[cellRowIndex].Cells[3].Value.ToString();
            WeightLabel.Text = TableView.Rows[cellRowIndex].Cells[4].Value.ToString();
            StorageSpaceLabel.Text = TableView.Rows[cellRowIndex].Cells[5].Value.ToString();
            BarcodeNumberLabel.Text = TableView.Rows[cellRowIndex].Cells[6].Value.ToString();
            ImageLocationLabel.Text = TableView.Rows[cellRowIndex].Cells[7].Value.ToString();
            LastInventoryLabel.Text = TableView.Rows[cellRowIndex].Cells[8].Value.ToString();
            
            
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
        Product editProduct = new Product();

        
  
        private void SaveEditButton_Click(object sender, EventArgs e)
        {                       
            //editProduct.Name = NameLabel.Text;
            //editProduct.SKU = SKULabel.Text;
            //editProduct.Quantity = int.Parse(QuantityLabel.Text);
            //editProduct.Weight = double.Parse(WeightLabel.Text);
            //editProduct.StorageSpace = StorageSpaceLabel.Text;
            //editProduct.BarcodeNumber = BarcodeNumberLabel.Text;
            //int cellRowIndex = TableView.SelectedCells[0].RowIndex;
            //products.Add(editProduct);
            //TableView.Rows[cellRowIndex].Cells[0].Value = ProductIdLabel.Text;
            //TableView.Rows[cellRowIndex].Cells[1].Value = NameLabel.Text;
            //TableView.Rows[cellRowIndex].Cells[2].Value = SKULabel.Text;
            //TableView.Rows[cellRowIndex].Cells[3].Value = QuantityLabel.Text;
            //TableView.Rows[cellRowIndex].Cells[4].Value = WeightLabel.Text;
            //TableView.Rows[cellRowIndex].Cells[5].Value = StorageSpaceLabel.Text;
            //TableView.Rows[cellRowIndex].Cells[6].Value = BarcodeNumberLabel.Text;
            //TableView.Rows[cellRowIndex].Cells[7].Value = ImageLocationLabel.Text;
            //TableView.Rows[cellRowIndex].Cells[8].Value = LastInventoryLabel.Text;

            async = new AsynchronousClient();
            //MessageBox.Show(jsonString);

            var bindinglist = new BindingList<Product>(products);
            var source = new BindingSource(bindinglist, null);
            TableView.DataSource = source;

            Product currentobject = (Product)TableView.CurrentRow.DataBoundItem;
            currentobject.Quantity = Convert.ToInt32(QuantityLabel.Text);

                //bindinglist.SingleOrDefault();
                //MessageBox.Show(bindinglist.ToString());
                string serializedObject = JsonConvert.SerializeObject(currentobject);
                async.StartClient("PUT/products/json=" + serializedObject);
        }
    }
}
