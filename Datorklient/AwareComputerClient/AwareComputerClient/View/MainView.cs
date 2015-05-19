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
using System.Reflection;


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
        private List<Product> unsortedList;
        private BindingList<Product> bindinglist;
        private Timer checkForChanged;
        HandleInput handleinput = new HandleInput();
        private string savedJsonString = null;
        private string response = null;
        private int cellRowIndex = 0;

        private PrintDialog printDialog = new PrintDialog();

        
        
        private void ShowProducts_Click()
        {
            
                TableViewPanel.Visible = true;
                ShowProductsMainContainer.Visible = true;
                TableView.Visible = true;
                TableView.AutoSize = true;
                SearchField.Focus();
               

                if (savedJsonString == null && response == null)
                {
                    if (checkForChanged == null) { 
                        checkForChanged = new Timer();
                        checkForChanged.Interval = 1000;
                        checkForChanged.Tick += new EventHandler(updateList);
                        checkForChanged.Start();
                    }
                        async.StartClient("GET/products");
                        response = async.Response;
                        savedJsonString = response;
                        products = JsonConvert.DeserializeObject<List<Product>>(response);
                        unsortedList = products = JsonConvert.DeserializeObject<List<Product>>(response);


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

            TableView.AutoSize = true;
        }
        private void refreshList()
        {
            bindinglist = new BindingList<Product>(products);
            var source = new BindingSource(bindinglist, null);
            TableView.DataSource = source;
        }

        #region File Menu Functionality
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
        #endregion
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
        private void SearchField_TextChanged(object sender, EventArgs e)
        {

            //Capitalizes All Words In A Sentence
            string capitalizedSearchBoxText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(SearchField.Text);

            //Looks using the Product object class, if it contains any character within the searchfield filter.
            BindingList<Product> filtered = new BindingList<Product>(products.Where(p => p.Name.Contains(capitalizedSearchBoxText)).ToList());

            TableView.DataSource = filtered;
        }
        #region Menu Button Handling
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
        #endregion
        #region Save Functionality
        private void SaveEditButton_Click(object sender, EventArgs e)
        {


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
        #endregion
        #region Print Functionality
        
        
        private void printButton_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printProductsList.Print();
            }
        }
        private void printProductsList_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Allow headers to be shown when printing
            TableView.ColumnHeadersVisible = true;
            var stocktakingCol = new DataGridViewTextBoxColumn();
            stocktakingCol.HeaderText = "Stocktaking";
            TableView.Columns.Add(stocktakingCol);
            //Gets the specific area of tableview.
            Bitmap bm = new Bitmap(this.TableView.Width, this.TableView.Height);
            TableView.DrawToBitmap(bm, new Rectangle(0, 0, this.TableView.Width, this.TableView.Height));
            e.Graphics.DrawImage(bm, 0, 0);
            //Set them back to normal after
            TableView.ColumnHeadersVisible = false;
            TableView.Columns.Remove(stocktakingCol);
        }
        #endregion

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox.SelectedIndex == 0)
            {
                products = unsortedList;
                refreshList();
            }
            if (comboBox.SelectedIndex == 1)
            {
                products = products.OrderBy(o => o.Name).ToList();
                refreshList();
            }
            if (comboBox.SelectedIndex == 2)
            {
                products = products.OrderBy(o => o.SKU).ToList();
                refreshList();
            }
            if (comboBox.SelectedIndex == 3)
            {
                products = products.OrderBy(o => o.Quantity).ToList();
                refreshList();
            }
            if (comboBox.SelectedIndex == 4)
            {
                products = products.OrderBy(o => o.Weight).ToList();
                refreshList();
            }
            if (comboBox.SelectedIndex == 5)
            {
                products = products.OrderBy(o => o.StorageSpace).ToList();
                refreshList();
            }
        }


    }
}
