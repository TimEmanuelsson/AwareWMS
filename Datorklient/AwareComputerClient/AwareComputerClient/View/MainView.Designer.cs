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
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.FileMenuContainer = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowProductsMainContainer = new System.Windows.Forms.Panel();
            this.ColumnHeaderStorageSpace = new System.Windows.Forms.Label();
            this.ShowProductsPanel = new System.Windows.Forms.Panel();
            this.TableView = new System.Windows.Forms.DataGridView();
            this.HeaderColumnName = new System.Windows.Forms.Label();
            this.SearchBarPanel = new System.Windows.Forms.Panel();
            this.Sort = new System.Windows.Forms.Label();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.SearchField = new System.Windows.Forms.TextBox();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.ColumnHeaderEAN = new System.Windows.Forms.Label();
            this.ColumnHeaderQuantity = new System.Windows.Forms.Label();
            this.ColumnHeaderSKU = new System.Windows.Forms.Label();
            this.ColumnHeaderWeight = new System.Windows.Forms.Label();
            this.printButton = new System.Windows.Forms.Button();
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
            this.QuantityText = new System.Windows.Forms.Label();
            this.StorageSpaceLabel = new System.Windows.Forms.TextBox();
            this.SaveEditButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.printProductsList = new System.Drawing.Printing.PrintDocument();
            this.MenuBar.SuspendLayout();
            this.ShowProductsMainContainer.SuspendLayout();
            this.ShowProductsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).BeginInit();
            this.SearchBarPanel.SuspendLayout();
            this.TableViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuBar
            // 
            this.MenuBar.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // ShowProductsMainContainer
            // 
            this.ShowProductsMainContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowProductsMainContainer.Controls.Add(this.ColumnHeaderStorageSpace);
            this.ShowProductsMainContainer.Controls.Add(this.ShowProductsPanel);
            this.ShowProductsMainContainer.Controls.Add(this.HeaderColumnName);
            this.ShowProductsMainContainer.Controls.Add(this.SearchBarPanel);
            this.ShowProductsMainContainer.Controls.Add(this.ColumnHeaderEAN);
            this.ShowProductsMainContainer.Controls.Add(this.ColumnHeaderQuantity);
            this.ShowProductsMainContainer.Controls.Add(this.ColumnHeaderSKU);
            this.ShowProductsMainContainer.Controls.Add(this.ColumnHeaderWeight);
            this.ShowProductsMainContainer.Controls.Add(this.printButton);
            this.ShowProductsMainContainer.Location = new System.Drawing.Point(3, 59);
            this.ShowProductsMainContainer.Name = "ShowProductsMainContainer";
            this.ShowProductsMainContainer.Size = new System.Drawing.Size(747, 758);
            this.ShowProductsMainContainer.TabIndex = 30;
            this.ShowProductsMainContainer.Visible = false;
            // 
            // ColumnHeaderStorageSpace
            // 
            this.ColumnHeaderStorageSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColumnHeaderStorageSpace.AutoSize = true;
            this.ColumnHeaderStorageSpace.Location = new System.Drawing.Point(478, 63);
            this.ColumnHeaderStorageSpace.Name = "ColumnHeaderStorageSpace";
            this.ColumnHeaderStorageSpace.Size = new System.Drawing.Size(75, 13);
            this.ColumnHeaderStorageSpace.TabIndex = 35;
            this.ColumnHeaderStorageSpace.Text = "StorageSpace";
            // 
            // ShowProductsPanel
            // 
            this.ShowProductsPanel.AutoScroll = true;
            this.ShowProductsPanel.Controls.Add(this.TableView);
            this.ShowProductsPanel.Location = new System.Drawing.Point(0, 80);
            this.ShowProductsPanel.Name = "ShowProductsPanel";
            this.ShowProductsPanel.Size = new System.Drawing.Size(747, 709);
            this.ShowProductsPanel.TabIndex = 28;
            // 
            // TableView
            // 
            this.TableView.AllowUserToAddRows = false;
            this.TableView.AllowUserToDeleteRows = false;
            this.TableView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TableView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.TableView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TableView.Location = new System.Drawing.Point(54, 0);
            this.TableView.MultiSelect = false;
            this.TableView.Name = "TableView";
            this.TableView.Size = new System.Drawing.Size(693, 318);
            this.TableView.TabIndex = 1;
            this.TableView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TableView_CellClick);
            // 
            // HeaderColumnName
            // 
            this.HeaderColumnName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HeaderColumnName.AutoSize = true;
            this.HeaderColumnName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.HeaderColumnName.Location = new System.Drawing.Point(76, 63);
            this.HeaderColumnName.Name = "HeaderColumnName";
            this.HeaderColumnName.Size = new System.Drawing.Size(35, 13);
            this.HeaderColumnName.TabIndex = 34;
            this.HeaderColumnName.Text = "Name";
            // 
            // SearchBarPanel
            // 
            this.SearchBarPanel.Controls.Add(this.Sort);
            this.SearchBarPanel.Controls.Add(this.comboBox);
            this.SearchBarPanel.Controls.Add(this.SearchField);
            this.SearchBarPanel.Controls.Add(this.SearchLabel);
            this.SearchBarPanel.Location = new System.Drawing.Point(54, 3);
            this.SearchBarPanel.Name = "SearchBarPanel";
            this.SearchBarPanel.Size = new System.Drawing.Size(570, 40);
            this.SearchBarPanel.TabIndex = 29;
            // 
            // Sort
            // 
            this.Sort.AutoSize = true;
            this.Sort.Location = new System.Drawing.Point(395, 18);
            this.Sort.Name = "Sort";
            this.Sort.Size = new System.Drawing.Size(26, 13);
            this.Sort.TabIndex = 38;
            this.Sort.Text = "Sort";
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "Reset",
            "Name",
            "SKU",
            "Quantity",
            "Weight",
            "Storagespace"});
            this.comboBox.Location = new System.Drawing.Point(427, 14);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(121, 21);
            this.comboBox.TabIndex = 37;
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
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
            // ColumnHeaderEAN
            // 
            this.ColumnHeaderEAN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColumnHeaderEAN.AutoSize = true;
            this.ColumnHeaderEAN.Location = new System.Drawing.Point(577, 63);
            this.ColumnHeaderEAN.Name = "ColumnHeaderEAN";
            this.ColumnHeaderEAN.Size = new System.Drawing.Size(29, 13);
            this.ColumnHeaderEAN.TabIndex = 36;
            this.ColumnHeaderEAN.Text = "EAN";
            // 
            // ColumnHeaderQuantity
            // 
            this.ColumnHeaderQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColumnHeaderQuantity.AutoSize = true;
            this.ColumnHeaderQuantity.Location = new System.Drawing.Point(279, 63);
            this.ColumnHeaderQuantity.Name = "ColumnHeaderQuantity";
            this.ColumnHeaderQuantity.Size = new System.Drawing.Size(46, 13);
            this.ColumnHeaderQuantity.TabIndex = 36;
            this.ColumnHeaderQuantity.Text = "Quantity";
            // 
            // ColumnHeaderSKU
            // 
            this.ColumnHeaderSKU.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColumnHeaderSKU.AutoSize = true;
            this.ColumnHeaderSKU.Location = new System.Drawing.Point(179, 63);
            this.ColumnHeaderSKU.Name = "ColumnHeaderSKU";
            this.ColumnHeaderSKU.Size = new System.Drawing.Size(29, 13);
            this.ColumnHeaderSKU.TabIndex = 35;
            this.ColumnHeaderSKU.Text = "SKU";
            // 
            // ColumnHeaderWeight
            // 
            this.ColumnHeaderWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColumnHeaderWeight.AutoSize = true;
            this.ColumnHeaderWeight.Location = new System.Drawing.Point(375, 63);
            this.ColumnHeaderWeight.Name = "ColumnHeaderWeight";
            this.ColumnHeaderWeight.Size = new System.Drawing.Size(41, 13);
            this.ColumnHeaderWeight.TabIndex = 34;
            this.ColumnHeaderWeight.Text = "Weight";
            // 
            // printButton
            // 
            this.printButton.Location = new System.Drawing.Point(630, 10);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(91, 28);
            this.printButton.TabIndex = 34;
            this.printButton.Text = "Print Products";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
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
            this.TableViewPanel.Controls.Add(this.QuantityText, 0, 1);
            this.TableViewPanel.Controls.Add(this.StorageSpaceLabel, 1, 2);
            this.TableViewPanel.Controls.Add(this.SaveEditButton, 3, 3);
            this.TableViewPanel.Location = new System.Drawing.Point(10, 379);
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
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.splitContainer1.Panel1.Controls.Add(this.TableViewPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ShowProductsMainContainer);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 862);
            this.splitContainer1.SplitterDistance = 421;
            this.splitContainer1.TabIndex = 31;
            // 
            // printProductsList
            // 
            this.printProductsList.DocumentName = "Products";
            this.printProductsList.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printProductsList_PrintPage);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 862);
            this.Controls.Add(this.MenuBar);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.Text = "Aware";
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.Controls.SetChildIndex(this.MenuBar, 0);
            this.Controls.SetChildIndex(this.MainMenuPanel, 0);
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ShowProductsMainContainer.ResumeLayout(false);
            this.ShowProductsMainContainer.PerformLayout();
            this.ShowProductsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).EndInit();
            this.SearchBarPanel.ResumeLayout(false);
            this.SearchBarPanel.PerformLayout();
            this.TableViewPanel.ResumeLayout(false);
            this.TableViewPanel.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem FileMenuContainer;
        private System.Windows.Forms.ToolStripMenuItem FileMenuExit;
        private System.Windows.Forms.Panel ShowProductsMainContainer;
        private System.Windows.Forms.Label ColumnHeaderStorageSpace;
        private System.Windows.Forms.Label ColumnHeaderEAN;
        private System.Windows.Forms.Panel SearchBarPanel;
        private System.Windows.Forms.TextBox SearchField;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.Label ColumnHeaderWeight;
        private System.Windows.Forms.Panel ShowProductsPanel;
        private System.Windows.Forms.DataGridView TableView;
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
        private System.Windows.Forms.Label QuantityText;
        private System.Windows.Forms.TextBox StorageSpaceLabel;
        private System.Windows.Forms.Button SaveEditButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label HeaderColumnName;
        private System.Windows.Forms.Label ColumnHeaderSKU;
        private System.Windows.Forms.Label ColumnHeaderQuantity;
        private System.Drawing.Printing.PrintDocument printProductsList;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Label Sort;
    }
}