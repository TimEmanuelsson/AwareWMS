namespace AwareComputerClient.View
{
    partial class MenuView
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
            this.MainMenuPanel = new System.Windows.Forms.Panel();
            this.ShowProductsButton = new System.Windows.Forms.Button();
            this.GoodsIn = new System.Windows.Forms.Button();
            this.ViewOrders = new System.Windows.Forms.Button();
            this.Stocktaking = new System.Windows.Forms.Button();
            this.Returns = new System.Windows.Forms.Button();
            this.MainMenuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuPanel
            // 
            this.MainMenuPanel.Controls.Add(this.ShowProductsButton);
            this.MainMenuPanel.Controls.Add(this.GoodsIn);
            this.MainMenuPanel.Controls.Add(this.ViewOrders);
            this.MainMenuPanel.Controls.Add(this.Stocktaking);
            this.MainMenuPanel.Controls.Add(this.Returns);
            this.MainMenuPanel.Location = new System.Drawing.Point(29, 43);
            this.MainMenuPanel.Name = "MainMenuPanel";
            this.MainMenuPanel.Size = new System.Drawing.Size(168, 184);
            this.MainMenuPanel.TabIndex = 25;
            // 
            // ShowProductsButton
            // 
            this.ShowProductsButton.Location = new System.Drawing.Point(3, 18);
            this.ShowProductsButton.Name = "ShowProductsButton";
            this.ShowProductsButton.Size = new System.Drawing.Size(162, 23);
            this.ShowProductsButton.TabIndex = 4;
            this.ShowProductsButton.Text = "Show Products";
            this.ShowProductsButton.UseVisualStyleBackColor = true;
            this.ShowProductsButton.Click += new System.EventHandler(this.ShowProductsButton_Click);
            // 
            // GoodsIn
            // 
            this.GoodsIn.Location = new System.Drawing.Point(3, 134);
            this.GoodsIn.Name = "GoodsIn";
            this.GoodsIn.Size = new System.Drawing.Size(162, 23);
            this.GoodsIn.TabIndex = 23;
            this.GoodsIn.Text = "Goods In";
            this.GoodsIn.UseVisualStyleBackColor = true;
            this.GoodsIn.Click += new System.EventHandler(this.GoodsIn_Click);
            // 
            // ViewOrders
            // 
            this.ViewOrders.Location = new System.Drawing.Point(3, 47);
            this.ViewOrders.Name = "ViewOrders";
            this.ViewOrders.Size = new System.Drawing.Size(162, 23);
            this.ViewOrders.TabIndex = 20;
            this.ViewOrders.Text = "View Orders";
            this.ViewOrders.UseVisualStyleBackColor = true;
            this.ViewOrders.Click += new System.EventHandler(this.ViewOrders_Click);
            // 
            // Stocktaking
            // 
            this.Stocktaking.Location = new System.Drawing.Point(3, 76);
            this.Stocktaking.Name = "Stocktaking";
            this.Stocktaking.Size = new System.Drawing.Size(162, 23);
            this.Stocktaking.TabIndex = 22;
            this.Stocktaking.Text = "Stocktaking";
            this.Stocktaking.UseVisualStyleBackColor = true;
            this.Stocktaking.Click += new System.EventHandler(this.Stocktaking_Click);
            // 
            // Returns
            // 
            this.Returns.Location = new System.Drawing.Point(3, 105);
            this.Returns.Name = "Returns";
            this.Returns.Size = new System.Drawing.Size(162, 23);
            this.Returns.TabIndex = 21;
            this.Returns.Text = "Returns";
            this.Returns.UseVisualStyleBackColor = true;
            this.Returns.Click += new System.EventHandler(this.Returns_Click);
            // 
            // MenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.MainMenuPanel);
            this.Name = "MenuView";
            this.Text = "MenuView";
            this.MainMenuPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel MainMenuPanel;
        private System.Windows.Forms.Button ShowProductsButton;
        private System.Windows.Forms.Button GoodsIn;
        private System.Windows.Forms.Button ViewOrders;
        private System.Windows.Forms.Button Stocktaking;
        private System.Windows.Forms.Button Returns;
    }
}