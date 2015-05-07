namespace AwareComputerClient.View
{
    partial class StocktakingView
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
            this.TableView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShowProductsButton = new System.Windows.Forms.Button();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.FileMenuContainer = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.NameText = new System.Windows.Forms.Label();
            this.NameLabel2 = new System.Windows.Forms.Label();
            this.SKUText = new System.Windows.Forms.Label();
            this.SKULabel2 = new System.Windows.Forms.Label();
            this.QuantityLabel2 = new System.Windows.Forms.Label();
            this.QuantityText = new System.Windows.Forms.Label();
            this.WeightLabel2 = new System.Windows.Forms.Label();
            this.WeightText = new System.Windows.Forms.Label();
            this.StorageSpaceLabel2 = new System.Windows.Forms.Label();
            this.StorageSpaceText = new System.Windows.Forms.Label();
            this.BarcodeNumberLabel2 = new System.Windows.Forms.Label();
            this.BarcodeNumberText = new System.Windows.Forms.Label();
            this.ImageLocationLabel2 = new System.Windows.Forms.Label();
            this.ImageLocationText = new System.Windows.Forms.Label();
            this.ViewOrders = new System.Windows.Forms.Button();
            this.Returns = new System.Windows.Forms.Button();
            this.Stocktaking = new System.Windows.Forms.Button();
            this.GoodsIn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TableViewPanel = new System.Windows.Forms.TableLayoutPanel();
            this.WeightLabel = new System.Windows.Forms.TextBox();
            this.QuantityLabel = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.TextBox();
            this.SKULabel = new System.Windows.Forms.TextBox();
            this.ImageLocationLabel = new System.Windows.Forms.TextBox();
            this.BarcodeNumberLabel = new System.Windows.Forms.TextBox();
            this.StorageSpaceLabel = new System.Windows.Forms.TextBox();
            this.SearchField = new System.Windows.Forms.TextBox();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.ShowProductsPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).BeginInit();
            this.MenuBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.TableViewPanel.SuspendLayout();
            this.ShowProductsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableView
            // 
            this.TableView.AllowUserToAddRows = false;
            this.TableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.TableView.Location = new System.Drawing.Point(18, 36);
            this.TableView.Name = "TableView";
            this.TableView.Size = new System.Drawing.Size(744, 318);
            this.TableView.TabIndex = 1;
            this.TableView.Visible = false;
            this.TableView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TableView_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            // 
            // ShowProductsButton
            // 
            this.ShowProductsButton.Location = new System.Drawing.Point(34, 18);
            this.ShowProductsButton.Name = "ShowProductsButton";
            this.ShowProductsButton.Size = new System.Drawing.Size(87, 23);
            this.ShowProductsButton.TabIndex = 4;
            this.ShowProductsButton.Text = "Show Products";
            this.ShowProductsButton.UseVisualStyleBackColor = true;
            this.ShowProductsButton.Click += new System.EventHandler(this.ShowProducts_Click);
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
            // NameText
            // 
            this.NameText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.NameText.AutoSize = true;
            this.NameText.Location = new System.Drawing.Point(60, 10);
            this.NameText.Name = "NameText";
            this.NameText.Size = new System.Drawing.Size(38, 13);
            this.NameText.TabIndex = 6;
            this.NameText.Text = "Name:";
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
            // SKUText
            // 
            this.SKUText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SKUText.AutoSize = true;
            this.SKUText.Location = new System.Drawing.Point(268, 10);
            this.SKUText.Name = "SKUText";
            this.SKUText.Size = new System.Drawing.Size(32, 13);
            this.SKUText.TabIndex = 8;
            this.SKUText.Text = "SKU:";
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
            // QuantityText
            // 
            this.QuantityText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.QuantityText.AutoSize = true;
            this.QuantityText.Location = new System.Drawing.Point(49, 43);
            this.QuantityText.Name = "QuantityText";
            this.QuantityText.Size = new System.Drawing.Size(49, 13);
            this.QuantityText.TabIndex = 10;
            this.QuantityText.Text = "Quantity:";
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
            // WeightText
            // 
            this.WeightText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.WeightText.AutoSize = true;
            this.WeightText.Location = new System.Drawing.Point(256, 43);
            this.WeightText.Name = "WeightText";
            this.WeightText.Size = new System.Drawing.Size(44, 13);
            this.WeightText.TabIndex = 12;
            this.WeightText.Text = "Weight:";
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
            // StorageSpaceText
            // 
            this.StorageSpaceText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.StorageSpaceText.AutoSize = true;
            this.StorageSpaceText.Location = new System.Drawing.Point(20, 78);
            this.StorageSpaceText.Name = "StorageSpaceText";
            this.StorageSpaceText.Size = new System.Drawing.Size(78, 13);
            this.StorageSpaceText.TabIndex = 14;
            this.StorageSpaceText.Text = "StorageSpace:";
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
            // BarcodeNumberText
            // 
            this.BarcodeNumberText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BarcodeNumberText.AutoSize = true;
            this.BarcodeNumberText.Location = new System.Drawing.Point(213, 78);
            this.BarcodeNumberText.Name = "BarcodeNumberText";
            this.BarcodeNumberText.Size = new System.Drawing.Size(87, 13);
            this.BarcodeNumberText.TabIndex = 16;
            this.BarcodeNumberText.Text = "BarcodeNumber:";
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
            // ImageLocationText
            // 
            this.ImageLocationText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ImageLocationText.AutoSize = true;
            this.ImageLocationText.Location = new System.Drawing.Point(18, 112);
            this.ImageLocationText.Name = "ImageLocationText";
            this.ImageLocationText.Size = new System.Drawing.Size(80, 13);
            this.ImageLocationText.TabIndex = 18;
            this.ImageLocationText.Text = "ImageLocation:";
            // 
            // ViewOrders
            // 
            this.ViewOrders.Location = new System.Drawing.Point(34, 47);
            this.ViewOrders.Name = "ViewOrders";
            this.ViewOrders.Size = new System.Drawing.Size(87, 23);
            this.ViewOrders.TabIndex = 20;
            this.ViewOrders.Text = "View Orders";
            this.ViewOrders.UseVisualStyleBackColor = true;
            this.ViewOrders.Click += new System.EventHandler(this.ViewOrders_Click);
            // 
            // Returns
            // 
            this.Returns.Location = new System.Drawing.Point(34, 105);
            this.Returns.Name = "Returns";
            this.Returns.Size = new System.Drawing.Size(87, 23);
            this.Returns.TabIndex = 21;
            this.Returns.Text = "Returns";
            this.Returns.UseVisualStyleBackColor = true;
            // 
            // Stocktaking
            // 
            this.Stocktaking.Location = new System.Drawing.Point(34, 76);
            this.Stocktaking.Name = "Stocktaking";
            this.Stocktaking.Size = new System.Drawing.Size(87, 23);
            this.Stocktaking.TabIndex = 22;
            this.Stocktaking.Text = "Stocktaking";
            this.Stocktaking.UseVisualStyleBackColor = true;
            this.Stocktaking.Click += new System.EventHandler(this.Stocktaking_Click);
            // 
            // GoodsIn
            // 
            this.GoodsIn.Location = new System.Drawing.Point(34, 134);
            this.GoodsIn.Name = "GoodsIn";
            this.GoodsIn.Size = new System.Drawing.Size(87, 23);
            this.GoodsIn.TabIndex = 23;
            this.GoodsIn.Text = "Goods In";
            this.GoodsIn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ShowProductsButton);
            this.panel1.Controls.Add(this.GoodsIn);
            this.panel1.Controls.Add(this.ViewOrders);
            this.panel1.Controls.Add(this.Stocktaking);
            this.panel1.Controls.Add(this.Returns);
            this.panel1.Location = new System.Drawing.Point(7, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 184);
            this.panel1.TabIndex = 24;
            // 
            // TableViewPanel
            // 
            this.TableViewPanel.ColumnCount = 4;
            this.TableViewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableViewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableViewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableViewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
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
            this.TableViewPanel.Location = new System.Drawing.Point(355, 414);
            this.TableViewPanel.Name = "TableViewPanel";
            this.TableViewPanel.RowCount = 4;
            this.TableViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.85356F));
            this.TableViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.60669F));
            this.TableViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.11297F));
            this.TableViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.42678F));
            this.TableViewPanel.Size = new System.Drawing.Size(406, 134);
            this.TableViewPanel.TabIndex = 25;
            // 
            // WeightLabel
            // 
            this.WeightLabel.Location = new System.Drawing.Point(306, 36);
            this.WeightLabel.Name = "WeightLabel";
            this.WeightLabel.Size = new System.Drawing.Size(61, 20);
            this.WeightLabel.TabIndex = 29;
            // 
            // QuantityLabel
            // 
            this.QuantityLabel.Location = new System.Drawing.Point(104, 36);
            this.QuantityLabel.Name = "QuantityLabel";
            this.QuantityLabel.Size = new System.Drawing.Size(61, 20);
            this.QuantityLabel.TabIndex = 28;
            // 
            // NameLabel
            // 
            this.NameLabel.Location = new System.Drawing.Point(104, 3);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(61, 20);
            this.NameLabel.TabIndex = 26;
            // 
            // SKULabel
            // 
            this.SKULabel.Location = new System.Drawing.Point(306, 3);
            this.SKULabel.Name = "SKULabel";
            this.SKULabel.Size = new System.Drawing.Size(61, 20);
            this.SKULabel.TabIndex = 27;
            // 
            // ImageLocationLabel
            // 
            this.ImageLocationLabel.Location = new System.Drawing.Point(104, 106);
            this.ImageLocationLabel.Name = "ImageLocationLabel";
            this.ImageLocationLabel.Size = new System.Drawing.Size(61, 20);
            this.ImageLocationLabel.TabIndex = 31;
            // 
            // BarcodeNumberLabel
            // 
            this.BarcodeNumberLabel.Location = new System.Drawing.Point(306, 70);
            this.BarcodeNumberLabel.Name = "BarcodeNumberLabel";
            this.BarcodeNumberLabel.Size = new System.Drawing.Size(61, 20);
            this.BarcodeNumberLabel.TabIndex = 32;
            // 
            // StorageSpaceLabel
            // 
            this.StorageSpaceLabel.Location = new System.Drawing.Point(104, 70);
            this.StorageSpaceLabel.Name = "StorageSpaceLabel";
            this.StorageSpaceLabel.Size = new System.Drawing.Size(61, 20);
            this.StorageSpaceLabel.TabIndex = 30;
            // 
            // SearchField
            // 
            this.SearchField.Location = new System.Drawing.Point(88, 10);
            this.SearchField.Name = "SearchField";
            this.SearchField.Size = new System.Drawing.Size(193, 20);
            this.SearchField.TabIndex = 26;
            this.SearchField.TextChanged += new System.EventHandler(this.SearchField_TextChanged);
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(41, 13);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(41, 13);
            this.SearchLabel.TabIndex = 27;
            this.SearchLabel.Text = "Search";
            // 
            // ShowProductsPanel
            // 
            this.ShowProductsPanel.Controls.Add(this.SearchLabel);
            this.ShowProductsPanel.Controls.Add(this.SearchField);
            this.ShowProductsPanel.Controls.Add(this.TableView);
            this.ShowProductsPanel.Enabled = false;
            this.ShowProductsPanel.Location = new System.Drawing.Point(337, 27);
            this.ShowProductsPanel.Name = "ShowProductsPanel";
            this.ShowProductsPanel.Size = new System.Drawing.Size(764, 358);
            this.ShowProductsPanel.TabIndex = 28;
            this.ShowProductsPanel.Visible = false;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 782);
            this.Controls.Add(this.ShowProductsPanel);
            this.Controls.Add(this.NameLabel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.WeightLabel2);
            this.Controls.Add(this.MenuBar);
            this.Controls.Add(this.TableViewPanel);
            this.Controls.Add(this.ImageLocationLabel2);
            this.Controls.Add(this.SKULabel2);
            this.Controls.Add(this.BarcodeNumberLabel2);
            this.Controls.Add(this.QuantityLabel2);
            this.Controls.Add(this.StorageSpaceLabel2);
            this.Name = "MainView";
            this.Text = "MainView";
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).EndInit();
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.TableViewPanel.ResumeLayout(false);
            this.TableViewPanel.PerformLayout();
            this.ShowProductsPanel.ResumeLayout(false);
            this.ShowProductsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView TableView;
        private System.Windows.Forms.Button ShowProductsButton;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem FileMenuContainer;
        private System.Windows.Forms.ToolStripMenuItem FileMenuExit;
        private System.Windows.Forms.Label NameText;
        private System.Windows.Forms.Label NameLabel2;
        private System.Windows.Forms.Label SKUText;
        private System.Windows.Forms.Label SKULabel2;
        private System.Windows.Forms.Label QuantityLabel2;
        private System.Windows.Forms.Label QuantityText;
        private System.Windows.Forms.Label WeightLabel2;
        private System.Windows.Forms.Label WeightText;
        private System.Windows.Forms.Label StorageSpaceLabel2;
        private System.Windows.Forms.Label StorageSpaceText;
        private System.Windows.Forms.Label BarcodeNumberLabel2;
        private System.Windows.Forms.Label BarcodeNumberText;
        private System.Windows.Forms.Label ImageLocationLabel2;
        private System.Windows.Forms.Label ImageLocationText;
        private System.Windows.Forms.Button ViewOrders;
        private System.Windows.Forms.Button Returns;
        private System.Windows.Forms.Button Stocktaking;
        private System.Windows.Forms.Button GoodsIn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel TableViewPanel;
        private System.Windows.Forms.TextBox NameLabel;
        private System.Windows.Forms.TextBox BarcodeNumberLabel;
        private System.Windows.Forms.TextBox ImageLocationLabel;
        private System.Windows.Forms.TextBox StorageSpaceLabel;
        private System.Windows.Forms.TextBox WeightLabel;
        private System.Windows.Forms.TextBox QuantityLabel;
        private System.Windows.Forms.TextBox SKULabel;
        private System.Windows.Forms.TextBox SearchField;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Panel ShowProductsPanel;
    }
}