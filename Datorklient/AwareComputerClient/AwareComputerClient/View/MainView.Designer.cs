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
            this.TableView = new System.Windows.Forms.DataGridView();
            this.ShowProducts = new System.Windows.Forms.Button();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.FileMenuContainer = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).BeginInit();
            this.MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableView
            // 
            this.TableView.AllowUserToAddRows = false;
            this.TableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableView.Location = new System.Drawing.Point(181, 53);
            this.TableView.Name = "TableView";
            this.TableView.Size = new System.Drawing.Size(240, 150);
            this.TableView.TabIndex = 1;
            this.TableView.Visible = false;
            this.TableView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TableView_CellContentClick);
            // 
            // ShowProducts
            // 
            this.ShowProducts.Location = new System.Drawing.Point(25, 53);
            this.ShowProducts.Name = "ShowProducts";
            this.ShowProducts.Size = new System.Drawing.Size(87, 23);
            this.ShowProducts.TabIndex = 4;
            this.ShowProducts.Text = "Show Products";
            this.ShowProducts.UseVisualStyleBackColor = true;
            this.ShowProducts.Click += new System.EventHandler(this.ShowProducts_Click);
            // 
            // MenuBar
            // 
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuContainer});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(992, 24);
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
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 547);
            this.Controls.Add(this.ShowProducts);
            this.Controls.Add(this.TableView);
            this.Controls.Add(this.MenuBar);
            this.Name = "MainView";
            this.Text = "MainView";
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).EndInit();
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView TableView;
        private System.Windows.Forms.Button ShowProducts;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem FileMenuContainer;
        private System.Windows.Forms.ToolStripMenuItem FileMenuExit;
    }
}