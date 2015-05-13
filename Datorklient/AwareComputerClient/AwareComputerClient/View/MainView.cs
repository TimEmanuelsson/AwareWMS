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
using System.Diagnostics;
using System.Globalization;
using System.Drawing.Printing;

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
        private BindingList<Product> bindinglist;
        private Timer checkForChanged;
        HandleInput handleinput = new HandleInput();
        private string savedJsonString = null;
        private string response = null;
        private void ShowProducts_Click()
        {

            
            // ShowProductsButton.Enabled = false;
            // TableView.ColumnHeaderMouseClick
            TableViewPanel.Visible = true;
            ShowProductsMainContainer.Visible = true;
            TableView.Visible = true;
            TableView.AutoSize = true;
            TableView.ColumnHeadersVisible = false;
            checkForChanged = new Timer();
            checkForChanged.Interval = 1000;
            checkForChanged.Tick += new EventHandler(updateList);
            checkForChanged.Start();

            if (savedJsonString == null)
            {
                async.StartClient("GET/products");
                response = async.Response;
                savedJsonString = response;
                products = JsonConvert.DeserializeObject<List<Product>>(response);

                bindinglist = new BindingList<Product>(products);
                var source = new BindingSource(bindinglist, null);

                TableView.DataSource = source;
                refreshDataSource();

            }

        }

        private void updateList(object sender, EventArgs e)
        {
            async.StartClient("GET/products");
            response = async.Response;

            if (savedJsonString != null)
            {
                if (!response.Equals(savedJsonString))
                {
                    savedJsonString = response;
                    products = JsonConvert.DeserializeObject<List<Product>>(response);
                    refreshDataSource();
                }
            }
        }
        private void refreshDataSource()
        {
            TableView.Refresh();
            TableView.ClearSelection();
            TableView.Rows[cellRowIndex].Selected = true;
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
        private int cellRowIndex;
        private void TableView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

            //Gets our current selected rowcell
            cellRowIndex = TableView.SelectedCells[0].RowIndex;
            TableView.Rows[cellRowIndex].Selected = true;
           // int cellColumnIndex = TableView.ColumnCount - 1;
            
            // ProductIdLabel.Text = TableView.Rows[cellRowIndex].Cells[0].Value.ToString();
            NameLabel.Text = TableView.Rows[cellRowIndex].Cells[0].Value.ToString();
            SKULabel.Text = TableView.Rows[cellRowIndex].Cells[1].Value.ToString();
            QuantityLabel.Text = TableView.Rows[cellRowIndex].Cells[2].Value.ToString();
            WeightLabel.Text = TableView.Rows[cellRowIndex].Cells[3].Value.ToString();
            StorageSpaceLabel.Text = TableView.Rows[cellRowIndex].Cells[4].Value.ToString();
            // BarcodeNumberLabel.Text = TableView.Rows[cellRowIndex].Cells[5].Value.ToString();
            // ImageLocationLabel.Text = TableView.Rows[cellRowIndex].Cells[6].Value.ToString();
            //LastInventoryLabel.Text = TableView.Rows[cellRowIndex].Cells[8].Value.ToString();
        }
        //TODO:: All Edit Stora Bokstäver
        private void SearchField_TextChanged(object sender, EventArgs e)
        {
            //Capitalizes All Words In A Sentence
            string capitalizedSearchBoxText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(SearchField.Text);

            //Looks using the Product object class, if it contains any character within the searchfield filter.
            BindingList<Product> filtered = new BindingList<Product>(products.Where(p => p.Name.Contains(capitalizedSearchBoxText)).ToList());


            TableView.DataSource = filtered;
        }

        protected override void ShowProductsButton_Click(object sender, EventArgs e)
        {
            ShowProducts_Click();
        }

        protected override void ViewOrders_Click(object sender, EventArgs e)
        {
            ShowProductsMainContainer.Visible = false;
            TableViewPanel.Visible = false;
        }

        protected override void Stocktaking_Click(object sender, EventArgs e)
        {
            // ShowProductsButton.Enabled = true;
        }
        
        private void SaveEditButton_Click(object sender, EventArgs e)
        {
           //Uppercase edited textboxes
            NameLabel.Text = handleinput.uppercaseFirst(NameLabel.Text);
            SKULabel.Text = handleinput.uppercaseFirst(SKULabel.Text);
            StorageSpaceLabel.Text = handleinput.uppercaseFirst(StorageSpaceLabel.Text);

            
            Product currentobject = (Product)TableView.CurrentRow.DataBoundItem;
            if (!handleinput.stringValidation(NameLabel.Text)
                || !handleinput.stringValidation(SKULabel.Text)
                || !handleinput.stringValidation(QuantityLabel.Text)
                || !handleinput.stringValidation(WeightLabel.Text)
                || !handleinput.stringValidation(StorageSpaceLabel.Text))
            {
                MessageBox.Show("Input fields cannot be empty");
            }
            else
            {
                currentobject.Name = NameLabel.Text;
                currentobject.SKU = SKULabel.Text;
                currentobject.Quantity = Convert.ToInt32(QuantityLabel.Text);
                currentobject.Weight = Convert.ToDouble(WeightLabel.Text);
                currentobject.StorageSpace = StorageSpaceLabel.Text;
                refreshDataSource();
                string serializedObject = JsonConvert.SerializeObject(currentobject);
                async.StartClient("PUT/products/json=" + serializedObject);
            }
        }
        private PrintDialog printDialog = new PrintDialog();
        private void print_Click(object sender, EventArgs e)
        {
            
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.TableView.Width, this.TableView.Height);
            TableView.DrawToBitmap(bm, new Rectangle(0, 0, this.TableView.Width, this.TableView.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }
    }
}



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
//private string[] row1;

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