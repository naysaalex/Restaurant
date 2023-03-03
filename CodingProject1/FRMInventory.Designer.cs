namespace CodingProject1
{
    partial class FRMInventory
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
            this.lbxInventory = new System.Windows.Forms.ListBox();
            this.BTNClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbxInventory
            // 
            this.lbxInventory.FormattingEnabled = true;
            this.lbxInventory.ItemHeight = 20;
            this.lbxInventory.Location = new System.Drawing.Point(14, 15);
            this.lbxInventory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbxInventory.Name = "lbxInventory";
            this.lbxInventory.Size = new System.Drawing.Size(276, 444);
            this.lbxInventory.TabIndex = 0;
            // 
            // BTNClose
            // 
            this.BTNClose.Location = new System.Drawing.Point(106, 467);
            this.BTNClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(84, 29);
            this.BTNClose.TabIndex = 1;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // FRMInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 516);
            this.Controls.Add(this.BTNClose);
            this.Controls.Add(this.lbxInventory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FRMInventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory";
            this.Load += new System.EventHandler(this.FRMInventory_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxInventory;
        private System.Windows.Forms.Button BTNClose;
    }
}