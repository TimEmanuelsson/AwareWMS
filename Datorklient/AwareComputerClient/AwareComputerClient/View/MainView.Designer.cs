namespace AwareComputerClient.View
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.TableView = new System.Windows.Forms.DataGridView();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.FileMenuContainer = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.NameLabel2 = new System.Windows.Forms.Label();
            this.SKULabel2 = new System.Windows.Forms.Label();
            this.QuantityLabel2 = new System.Windows.Forms.Label();
            this.WeightLabel2 = new System.Windows.Forms.Label();
            this.StorageSpaceLabel2 = new System.Windows.Forms.Label();
            this.BarcodeNumberLabel2 = new System.Windows.Forms.Label();
            this.ImageLocationLabel2 = new System.Windows.Forms.Label();
            this.SearchField = new System.Windows.Forms.TextBox();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.ShowProductsPanel = new System.Windows.Forms.Panel();
            this.TableViewPanel = new System.Windows.Forms.TableLayoutPanel();
            this.WeightLabel = new System.Windows.Forms.TextBox();
            this.StorageSpaceText = new System.Windows.Forms.Label();
            this.QuantityLabel = new System.Windows.Forms.TextBox();
            this.NameText = new System.Windows.Forms.Label();
            this.WeightText = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.TextBox();
            this.ImageLocationText = new System.Windows.Forms.Label();
            this.SKUText = new System.Windows.Forms.Label();
            this.SKULabel = new System.Windows.Forms.TextBox();
            this.ImageLocationLabel = new System.Windows.Forms.TextBox();
            this.QuantityText = new System.Windows.Forms.Label();
            this.BarcodeNumberText = new System.Windows.Forms.Label();
            this.BarcodeNumberLabel = new System.Windows.Forms.TextBox();
            this.StorageSpaceLabel = new System.Windows.Forms.TextBox();
            this.SaveEditButton = new System.Windows.Forms.Button();
            this.SearchBarPanel = new System.Windows.Forms.Panel();
            this.ShowProductsMainContainer = new System.Windows.Forms.Panel();
            this.ColumnHeaderImageLocation = new System.Windows.Forms.Label();
            this.ColumnHeaderStorageSpace = new System.Windows.Forms.Label();
            this.ColumnHeaderBarcodeNumber = new System.Windows.Forms.Label();
            this.ColumnHeaderWeight = new System.Windows.Forms.Label();
            this.ColumnHeaderName = new System.Windows.Forms.Label();
            this.ColumnHeaderSKU = new System.Windows.Forms.Label();
            this.ColumnHeaderQuantity = new System.Windows.Forms.Label();
            this.LastInventoryText = new System.Windows.Forms.Label();
            this.LastInventoryLabel = new System.Windows.Forms.TextBox();
            this.ProductId = new System.Windows.Forms.Label();
            this.ProductIdLabel = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).BeginInit();
            this.MenuBar.SuspendLayout();
            this.ShowProductsPanel.SuspendLayout();
            this.TableViewPanel.SuspendLayout();
            this.SearchBarPanel.SuspendLayout();
            this.ShowProductsMainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableView
            // 
            this.TableView.AllowUserToAddRows = false;
            this.TableView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.TableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableView.Location = new System.Drawing.Point(3, 0);
            this.TableView.Name = "TableView";
            this.TableView.Size = new System.Drawing.Size(744, 318);
            this.TableView.TabIndex = 1;
            this.TableView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TableView_CellContentClick);
            // 
            // MenuBar
            // 
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuContainer});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(1264, 24);
            this.MenuBar.TabIndex = 5;
            this.MenuBar.Text = "MenuBar";
            this.MenuBar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // FileMenuContainer
            // 
            this.FileMenuContainer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuExit});
            this.FileMenuContainer.Name = "FileMenuContainer";
            this.FileMenuContainer.Size = new System.Drawing.Size(37, 20);
            this.FileMenuContainer.Text = "File";
            this.FileMenuContainer.Click += new System.EventHandler(this.FileMenuContainer_Click);
            // 
            // FileMenuExit
            // 
            this.FileMenuExit.Name = "FileMenuExit";
            this.FileMenuExit.Size = new System.Drawing.Size(92, 22);
            this.FileMenuExit.Text = "Exit";
            this.FileMenuExit.Click += new System.EventHandler(this.FileMenuExit_Click);
            // 
            // NameLabel2
            // 
            this.NameLabel2.AutoSize = true;
            this.NameLabel2.Location = new System.Drawing.Point(693, 704);
            this.NameLabel2.Name = "NameLabel2";
            this.NameLabel2.Size = new System.Drawing.Size(33, 13);
            this.NameLabel2.TabIndex = 7;
            this.NameLabel2.Text = "name";
            // 
            // SKULabel2
            // 
            this.SKULabel2.AutoSize = true;
            this.SKULabel2.Location = new System.Drawing.Point(746, 704);
            this.SKULabel2.Name = "SKULabel2";
            this.SKULabel2.Size = new System.Drawing.Size(24, 13);
            this.SKULabel2.TabIndex = 9;
            this.SKULabel2.Text = "sku";
            // 
            // QuantityLabel2
            // 
            this.QuantityLabel2.AutoSize = true;
            this.QuantityLabel2.Location = new System.Drawing.Point(813, 704);
            this.QuantityLabel2.Name = "QuantityLabel2";
            this.QuantityLabel2.Size = new System.Drawing.Size(44, 13);
            this.QuantityLabel2.TabIndex = 11;
            this.QuantityLabel2.Text = "quantity";
            // 
            // WeightLabel2
            // 
            this.WeightLabel2.AutoSize = true;
            this.WeightLabel2.Location = new System.Drawing.Point(899, 704);
            this.WeightLabel2.Name = "WeightLabel2";
            this.WeightLabel2.Size = new System.Drawing.Size(38, 13);
            this.WeightLabel2.TabIndex = 13;
            this.WeightLabel2.Text = "weight";
            // 
            // StorageSpaceLabel2
            // 
            this.StorageSpaceLabel2.AutoSize = true;
            this.StorageSpaceLabel2.Location = new System.Drawing.Point(973, 704);
            this.StorageSpaceLabel2.Name = "StorageSpaceLabel2";
            this.StorageSpaceLabel2.Size = new System.Drawing.Size(71, 13);
            this.StorageSpaceLabel2.TabIndex = 15;
            this.StorageSpaceLabel2.Text = "storagespace";
            // 
            // BarcodeNumberLabel2
            // 
            this.BarcodeNumberLabel2.AutoSize = true;
            this.BarcodeNumberLabel2.Location = new System.Drawing.Point(1170, 704);
            this.BarcodeNumberLabel2.Name = "BarcodeNumberLabel2";
            this.BarcodeNumberLabel2.Size = new System.Drawing.Size(81, 13);
            this.BarcodeNumberLabel2.TabIndex = 17;
            this.BarcodeNumberLabel2.Text = "barcodenumber";
            // 
            // ImageLocationLabel2
            // 
            this.ImageLocationLabel2.AutoSize = true;
            this.ImageLocationLabel2.Location = new System.Drawing.Point(1073, 704);
            this.ImageLocationLabel2.Name = "ImageLocationLabel2";
            this.ImageLocationLabel2.Size = new System.Drawing.Size(72, 13);
            this.ImageLocationLabel2.TabIndex = 19;
            this.ImageLocationLabel2.Text = "imagelocation";
            // 
            // SearchField
            // 
            this.SearchField.Location = new System.Drawing.Point(74, 15);
            this.SearchField.Name = "SearchField";
            this.SearchField.Size = new System.Drawing.Size(193, 20);
            this.SearchField.TabIndex = 26;
            this.SearchField.TextChanged += new System.EventHandler(this.SearchField_TextChanged);
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(27, 18);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(41, 13);
            this.SearchLabel.TabIndex = 27;
            this.SearchLabel.Text = "Search";
            // 
            // ShowProductsPanel
            // 
            this.ShowProductsPanel.AutoScroll = true;
            this.ShowProductsPanel.Controls.Add(this.TableView);
            this.ShowProductsPanel.Location = new System.Drawing.Point(18, 75);
            this.ShowProductsPanel.Name = "ShowProductsPanel";
            this.ShowProductsPanel.Size = new System.Drawing.Size(748, 510);
            this.ShowProductsPanel.TabIndex = 28;
            // 
            // TableViewPanel
            // 
            this.TableViewPanel.ColumnCount = 4;
            this.TableViewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableViewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableViewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableViewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableViewPanel.Controls.Add(this.WeightLabel, 3, 1);
            this.TableViewPanel.Controls.Add(this.StorageSpaceText, 0, 2);
            this.TableViewPanel.Controls.Add(this.QuantityLabel, 1, 1);
            this.TableViewPanel.Controls.Add(this.NameText, 0, 0);
            this.TableViewPanel.Controls.Add(this.WeightText, 2, 1);
            this.TableViewPanel.Controls.Add(this.NameLabel, 1, 0);
            this.TableViewPanel.Controls.Add(this.ImageLocationText, 0, 3);
            this.TableViewPanel.Controls.Add(this.SKUText, 2, 0);
            this.TableViewPanel.Controls.Add(this.SKULabel, 3, 0);
            this.TableViewPanel.Controls.Add(this.ImageLocationLabel, 1, 3);
            this.TableViewPanel.Controls.Add(this.QuantityText, 0, 1);
            this.TableViewPanel.Controls.Add(this.BarcodeNumberText, 2, 2);
            this.TableViewPanel.Controls.Add(this.BarcodeNumberLabel, 3, 2);
            this.TableViewPanel.Controls.Add(this.StorageSpaceLabel, 1, 2);
            this.TableViewPanel.Controls.Add(this.SaveEditButton, 3, 3);
            this.TableViewPanel.Location = new System.Drawing.Point(12, 400);
            this.TableViewPanel.Name = "TableViewPanel";
            this.TableViewPanel.RowCount = 4;
            this.TableViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.85356F));
            this.TableViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.60669F));
            this.TableViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.11297F));
            this.TableViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.42678F));
            this.TableViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableViewPanel.Size = new System.Drawing.Size(406, 144);
            this.TableViewPanel.TabIndex = 25;
            this.TableViewPanel.Visible = false;
            // 
            // WeightLabel
            // 
            this.WeightLabel.Location = new System.Drawing.Point(312, 38);
            this.WeightLabel.Name = "WeightLabel";
            this.WeightLabel.Size = new System.Drawing.Size(61, 20);
            this.WeightLabel.TabIndex = 29;
            // 
            // StorageSpaceText
            // 
            this.StorageSpaceText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.StorageSpaceText.AutoSize = true;
            this.StorageSpaceText.Location = new System.Drawing.Point(22, 84);
            this.StorageSpaceText.Name = "StorageSpaceText";
            this.StorageSpaceText.Size = new System.Drawing.Size(78, 13);
            this.StorageSpaceText.TabIndex = 14;
            this.StorageSpaceText.Text = "StorageSpace:";
            // 
            // QuantityLabel
            // 
            this.QuantityLabel.Location = new System.Drawing.Point(106, 38);
            this.QuantityLabel.Name = "QuantityLabel";
            this.QuantityLabel.Size = new System.Drawing.Size(61, 20);
            this.QuantityLabel.TabIndex = 28;
            // 
            // NameText
            // 
            this.NameText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.NameText.AutoSize = true;
            this.NameText.Location = new System.Drawing.Point(62, 11);
            this.NameText.Name = "NameText";
            this.NameText.Size = new System.Drawing.Size(38, 13);
            this.NameText.TabIndex = 6;
            this.NameText.Text = "Name:";
            // 
            // WeightText
            // 
            this.WeightText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.WeightText.AutoSize = true;
            this.WeightText.Location = new System.Drawing.Point(262, 46);
            this.WeightText.Name = "WeightText";
            this.WeightText.Size = new System.Drawing.Size(44, 13);
            this.WeightText.TabIndex = 12;
            this.WeightText.Text = "Weight:";
            // 
            // NameLabel
            // 
            this.NameLabel.Location = new System.Drawing.Point(106, 3);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(61, 20);
            this.NameLabel.TabIndex = 26;
            // 
            // ImageLocationText
            // 
            this.ImageLocationText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ImageLocationText.AutoSize = true;
            this.ImageLocationText.Location = new System.Drawing.Point(20, 120);
            this.ImageLocationText.Name = "ImageLocationText";
            this.ImageLocationText.Size = new System.Drawing.Size(80, 13);
            this.ImageLocationText.TabIndex = 18;
            this.ImageLocationText.Text = "ImageLocation:";
            // 
            // SKUText
            // 
            this.SKUText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SKUText.AutoSize = true;
            this.SKUText.Location = new System.Drawing.Point(274, 11);
            this.SKUText.Name = "SKUText";
            this.SKUText.Size = new System.Drawing.Size(32, 13);
            this.SKUText.TabIndex = 8;
            this.SKUText.Text = "SKU:";
            // 
            // SKULabel
            // 
            this.SKULabel.Location = new System.Drawing.Point(312, 3);
            this.SKULabel.Name = "SKULabel";
            this.SKULabel.Size = new System.Drawing.Size(61, 20);
            this.SKULabel.TabIndex = 27;
            // 
            // ImageLocationLabel
            // 
            this.ImageLocationLabel.Location = new System.Drawing.Point(106, 113);
            this.ImageLocationLabel.Name = "ImageLocationLabel";
            this.ImageLocationLabel.Size = new System.Drawing.Size(61, 20);
            this.ImageLocationLabel.TabIndex = 31;
            // 
            // QuantityText
            // 
            this.QuantityText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.QuantityText.AutoSize = true;
            this.QuantityText.Location = new System.Drawing.Point(51, 46);
            this.QuantityText.Name = "QuantityText";
            this.QuantityText.Size = new System.Drawing.Size(49, 13);
            this.QuantityText.TabIndex = 10;
            this.QuantityText.Text = "Quantity:";
            // 
            // BarcodeNumberText
            // 
            this.BarcodeNumberText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BarcodeNumberText.AutoSize = true;
            this.BarcodeNumberText.Location = new System.Drawing.Point(219, 84);
            this.BarcodeNumberText.Name = "BarcodeNumberText";
            this.BarcodeNumberText.Size = new System.Drawing.Size(87, 13);
            this.BarcodeNumberText.TabIndex = 16;
            this.BarcodeNumberText.Text = "BarcodeNumber:";
            // 
            // BarcodeNumberLabel
            // 
            this.BarcodeNumberLabel.Location = new System.Drawing.Point(312, 74);
            this.BarcodeNumberLabel.Name = "BarcodeNumberLabel";
            this.BarcodeNumberLabel.Size = new System.Drawing.Size(61, 20);
            this.BarcodeNumberLabel.TabIndex = 32;
            // 
            // StorageSpaceLabel
            // 
            this.StorageSpaceLabel.Location = new System.Drawing.Point(106, 74);
            this.StorageSpaceLabel.Name = "StorageSpaceLabel";
            this.StorageSpaceLabel.Size = new System.Drawing.Size(61, 20);
            this.StorageSpaceLabel.TabIndex = 30;
            // 
            // SaveEditButton
            // 
            this.SaveEditButton.Location = new System.Drawing.Point(312, 113);
            this.SaveEditButton.Name = "SaveEditButton";
            this.SaveEditButton.Size = new System.Drawing.Size(91, 28);
            this.SaveEditButton.TabIndex = 33;
            this.SaveEditButton.Text = "Save";
            this.SaveEditButton.UseVisualStyleBackColor = true;
            this.SaveEditButton.Click += new System.EventHandler(this.SaveEditButton_Click);
            // 
            // SearchBarPanel
            // 
            this.SearchBarPanel.Controls.Add(this.SearchField);
            this.SearchBarPanel.Controls.Add(this.SearchLabel);
            this.SearchBarPanel.Location = new System.Drawing.Point(98, 12);
            this.SearchBarPanel.Name = "SearchBarPanel";
            this.SearchBarPanel.Size = new System.Drawing.Size(326, 40);
            this.SearchBarPanel.TabIndex = 29;
            // 
            // ShowProductsMainContainer
            // 
            this.ShowProductsMainContainer.Controls.Add(this.ColumnHeaderImageLocation);
            this.ShowProductsMainContainer.Controls.Add(this.ColumnHeaderStorageSpace);
            this.ShowProductsMainContainer.Controls.Add(this.ColumnHeaderBarcodeNumber);
            this.ShowProductsMainContainer.Controls.Add(this.SearchBarPanel);
            this.ShowProductsMainContainer.Controls.Add(this.ColumnHeaderWeight);
            this.ShowProductsMainContainer.Controls.Add(this.ShowProductsPanel);
            this.ShowProductsMainContainer.Controls.Add(this.ColumnHeaderName);
            this.ShowProductsMainContainer.Controls.Add(this.ColumnHeaderSKU);
            this.ShowProductsMainContainer.Controls.Add(this.ColumnHeaderQuantity);
            this.ShowProductsMainContainer.Location = new System.Drawing.Point(424, 30);
            this.ShowProductsMainContainer.Name = "ShowProductsMainContainer";
            this.ShowProductsMainContainer.Size = new System.Drawing.Size(840, 585);
            this.ShowProductsMainContainer.TabIndex = 30;
            this.ShowProductsMainContainer.Visible = false;
            // 
            // ColumnHeaderImageLocation
            // 
            this.ColumnHeaderImageLocation.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ColumnHeaderImageLocation.AutoSize = true;
            this.ColumnHeaderImageLocation.Location = new System.Drawing.Point(672, 59);
            this.ColumnHeaderImageLocation.Name = "ColumnHeaderImageLocation";
            this.ColumnHeaderImageLocation.Size = new System.Drawing.Size(77, 13);
            this.ColumnHeaderImageLocation.TabIndex = 37;
            this.ColumnHeaderImageLocation.Text = "ImageLocation";
            // 
            // ColumnHeaderStorageSpace
            // 
            this.ColumnHeaderStorageSpace.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ColumnHeaderStorageSpace.AutoSize = true;
            this.ColumnHeaderStorageSpace.Location = new System.Drawing.Point(465, 59);
            this.ColumnHeaderStorageSpace.Name = "ColumnHeaderStorageSpace";
            this.ColumnHeaderStorageSpace.Size = new System.Drawing.Size(75, 13);
            this.ColumnHeaderStorageSpace.TabIndex = 35;
            this.ColumnHeaderStorageSpace.Text = "StorageSpace";
            // 
            // ColumnHeaderBarcodeNumber
            // 
            this.ColumnHeaderBarcodeNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ColumnHeaderBarcodeNumber.AutoSize = true;
            this.ColumnHeaderBarcodeNumber.Location = new System.Drawing.Point(560, 59);
            this.ColumnHeaderBarcodeNumber.Name = "ColumnHeaderBarcodeNumber";
            this.ColumnHeaderBarcodeNumber.Size = new System.Drawing.Size(84, 13);
            this.ColumnHeaderBarcodeNumber.TabIndex = 36;
            this.ColumnHeaderBarcodeNumber.Text = "BarcodeNumber";
            // 
            // ColumnHeaderWeight
            // 
            this.ColumnHeaderWeight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ColumnHeaderWeight.AutoSize = true;
            this.ColumnHeaderWeight.Location = new System.Drawing.Point(369, 59);
            this.ColumnHeaderWeight.Name = "ColumnHeaderWeight";
            this.ColumnHeaderWeight.Size = new System.Drawing.Size(41, 13);
            this.ColumnHeaderWeight.TabIndex = 34;
            this.ColumnHeaderWeight.Text = "Weight";
            // 
            // ColumnHeaderName
            // 
            this.ColumnHeaderName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ColumnHeaderName.AutoSize = true;
            this.ColumnHeaderName.Location = new System.Drawing.Point(68, 59);
            this.ColumnHeaderName.Name = "ColumnHeaderName";
            this.ColumnHeaderName.Size = new System.Drawing.Size(35, 13);
            this.ColumnHeaderName.TabIndex = 31;
            this.ColumnHeaderName.Text = "Name";
            // 
            // ColumnHeaderSKU
            // 
            this.ColumnHeaderSKU.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ColumnHeaderSKU.AutoSize = true;
            this.ColumnHeaderSKU.Location = new System.Drawing.Point(270, 59);
            this.ColumnHeaderSKU.Name = "ColumnHeaderSKU";
            this.ColumnHeaderSKU.Size = new System.Drawing.Size(29, 13);
            this.ColumnHeaderSKU.TabIndex = 32;
            this.ColumnHeaderSKU.Text = "SKU";
            // 
            // ColumnHeaderQuantity
            // 
            this.ColumnHeaderQuantity.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ColumnHeaderQuantity.AutoSize = true;
            this.ColumnHeaderQuantity.Location = new System.Drawing.Point(169, 59);
            this.ColumnHeaderQuantity.Name = "ColumnHeaderQuantity";
            this.ColumnHeaderQuantity.Size = new System.Drawing.Size(46, 13);
            this.ColumnHeaderQuantity.TabIndex = 33;
            this.ColumnHeaderQuantity.Text = "Quantity";
            // 
            // LastInventoryText
            // 
            this.LastInventoryText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LastInventoryText.AutoSize = true;
            this.LastInventoryText.Location = new System.Drawing.Point(34, 560);
            this.LastInventoryText.Name = "LastInventoryText";
            this.LastInventoryText.Size = new System.Drawing.Size(74, 13);
            this.LastInventoryText.TabIndex = 34;
            this.LastInventoryText.Text = "LastInventory:";
            // 
            // LastInventoryLabel
            // 
            this.LastInventoryLabel.Location = new System.Drawing.Point(138, 557);
            this.LastInventoryLabel.Name = "LastInventoryLabel";
            this.LastInventoryLabel.Size = new System.Drawing.Size(61, 20);
            this.LastInventoryLabel.TabIndex = 34;
            // 
            // ProductId
            // 
            this.ProductId.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ProductId.AutoSize = true;
            this.ProductId.Location = new System.Drawing.Point(50, 371);
            this.ProductId.Name = "ProductId";
            this.ProductId.Size = new System.Drawing.Size(58, 13);
            this.ProductId.TabIndex = 35;
            this.ProductId.Text = "ProductID:";
            // 
            // ProductIdLabel
            // 
            this.ProductIdLabel.Location = new System.Drawing.Point(119, 371);
            this.ProductIdLabel.Name = "ProductIdLabel";
            this.ProductIdLabel.Size = new System.Drawing.Size(61, 20);
            this.ProductIdLabel.TabIndex = 36;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 782);
            this.Controls.Add(this.ProductId);
            this.Controls.Add(this.ProductIdLabel);
            this.Controls.Add(this.LastInventoryLabel);
            this.Controls.Add(this.LastInventoryText);
            this.Controls.Add(this.ShowProductsMainContainer);
            this.Controls.Add(this.NameLabel2);
            this.Controls.Add(this.WeightLabel2);
            this.Controls.Add(this.TableViewPanel);
            this.Controls.Add(this.MenuBar);
            this.Controls.Add(this.ImageLocationLabel2);
            this.Controls.Add(this.SKULabel2);
            this.Controls.Add(this.BarcodeNumberLabel2);
            this.Controls.Add(this.QuantityLabel2);
            this.Controls.Add(this.StorageSpaceLabel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.Text = "Aware";
            this.Controls.SetChildIndex(this.StorageSpaceLabel2, 0);
            this.Controls.SetChildIndex(this.QuantityLabel2, 0);
            this.Controls.SetChildIndex(this.BarcodeNumberLabel2, 0);
            this.Controls.SetChildIndex(this.SKULabel2, 0);
            this.Controls.SetChildIndex(this.ImageLocationLabel2, 0);
            this.Controls.SetChildIndex(this.MenuBar, 0);
            this.Controls.SetChildIndex(this.TableViewPanel, 0);
            this.Controls.SetChildIndex(this.WeightLabel2, 0);
            this.Controls.SetChildIndex(this.NameLabel2, 0);
            this.Controls.SetChildIndex(this.ShowProductsMainContainer, 0);
            this.Controls.SetChildIndex(this.LastInventoryText, 0);
            this.Controls.SetChildIndex(this.LastInventoryLabel, 0);
            this.Controls.SetChildIndex(this.ProductIdLabel, 0);
            this.Controls.SetChildIndex(this.ProductId, 0);
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).EndInit();
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ShowProductsPanel.ResumeLayout(false);
            this.TableViewPanel.ResumeLayout(false);
            this.TableViewPanel.PerformLayout();
            this.SearchBarPanel.ResumeLayout(false);
            this.SearchBarPanel.PerformLayout();
            this.ShowProductsMainContainer.ResumeLayout(false);
            this.ShowProductsMainContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView TableView;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem FileMenuContainer;
        private System.Windows.Forms.ToolStripMenuItem FileMenuExit;
        private System.Windows.Forms.Label NameLabel2;
        private System.Windows.Forms.Label SKULabel2;
        private System.Windows.Forms.Label QuantityLabel2;
        private System.Windows.Forms.Label WeightLabel2;
        private System.Windows.Forms.Label StorageSpaceLabel2;
        private System.Windows.Forms.Label BarcodeNumberLabel2;
        private System.Windows.Forms.Label ImageLocationLabel2;
        private System.Windows.Forms.TextBox SearchField;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.Panel ShowProductsPanel;
        private System.Windows.Forms.TableLayoutPanel TableViewPanel;
        private System.Windows.Forms.TextBox WeightLabel;
        private System.Windows.Forms.Label StorageSpaceText;
        private System.Windows.Forms.TextBox QuantityLabel;
        private System.Windows.Forms.Label NameText;
        private System.Windows.Forms.Label WeightText;
        private System.Windows.Forms.TextBox NameLabel;
        private System.Windows.Forms.Label ImageLocationText;
        private System.Windows.Forms.Label SKUText;
        private System.Windows.Forms.TextBox SKULabel;
        private System.Windows.Forms.TextBox ImageLocationLabel;
        private System.Windows.Forms.Label QuantityText;
        private System.Windows.Forms.Label BarcodeNumberText;
        private System.Windows.Forms.TextBox BarcodeNumberLabel;
        private System.Windows.Forms.TextBox StorageSpaceLabel;
        private System.Windows.Forms.Panel SearchBarPanel;
        private System.Windows.Forms.Panel ShowProductsMainContainer;
        private System.Windows.Forms.Label ColumnHeaderStorageSpace;
        private System.Windows.Forms.Label ColumnHeaderName;
        private System.Windows.Forms.Label ColumnHeaderWeight;
        private System.Windows.Forms.Label ColumnHeaderImageLocation;
        private System.Windows.Forms.Label ColumnHeaderSKU;
        private System.Windows.Forms.Label ColumnHeaderQuantity;
        private System.Windows.Forms.Label ColumnHeaderBarcodeNumber;
        private System.Windows.Forms.Button SaveEditButton;
        private System.Windows.Forms.Label LastInventoryText;
        private System.Windows.Forms.TextBox LastInventoryLabel;
        private System.Windows.Forms.Label ProductId;
        private System.Windows.Forms.TextBox ProductIdLabel;
    }
}