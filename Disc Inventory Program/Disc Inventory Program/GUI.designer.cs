namespace Disc_Inventory_Program
{
    partial class GUI
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
            this.buttonSelectSaveDirectory = new System.Windows.Forms.Button();
            this.buttonSelectInventoryDirectory = new System.Windows.Forms.Button();
            this.buttonStartInventory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonSelectSaveDirectory
            // 
            this.buttonSelectSaveDirectory.Location = new System.Drawing.Point(12, 12);
            this.buttonSelectSaveDirectory.Name = "buttonSelectSaveDirectory";
            this.buttonSelectSaveDirectory.Size = new System.Drawing.Size(320, 23);
            this.buttonSelectSaveDirectory.TabIndex = 0;
            this.buttonSelectSaveDirectory.Text = "Step 1: Select Save Directory";
            this.buttonSelectSaveDirectory.UseVisualStyleBackColor = true;
            this.buttonSelectSaveDirectory.Click += new System.EventHandler(this.buttonSelectSaveDirectory_Click);
            // 
            // buttonSelectInventoryDirectory
            // 
            this.buttonSelectInventoryDirectory.Enabled = false;
            this.buttonSelectInventoryDirectory.Location = new System.Drawing.Point(12, 41);
            this.buttonSelectInventoryDirectory.Name = "buttonSelectInventoryDirectory";
            this.buttonSelectInventoryDirectory.Size = new System.Drawing.Size(320, 23);
            this.buttonSelectInventoryDirectory.TabIndex = 1;
            this.buttonSelectInventoryDirectory.Text = "Step 2: Select Inventory Directory";
            this.buttonSelectInventoryDirectory.UseVisualStyleBackColor = true;
            this.buttonSelectInventoryDirectory.Click += new System.EventHandler(this.buttonSelectInventoryDirectory_Click);
            // 
            // buttonStartInventory
            // 
            this.buttonStartInventory.Enabled = false;
            this.buttonStartInventory.Location = new System.Drawing.Point(12, 70);
            this.buttonStartInventory.Name = "buttonStartInventory";
            this.buttonStartInventory.Size = new System.Drawing.Size(320, 23);
            this.buttonStartInventory.TabIndex = 2;
            this.buttonStartInventory.Text = "Step 3: Start Inventory";
            this.buttonStartInventory.UseVisualStyleBackColor = true;
            this.buttonStartInventory.Click += new System.EventHandler(this.buttonStartInventory_Click);
            // 
            // GUI
            // 
            this.ClientSize = new System.Drawing.Size(351, 108);
            this.Controls.Add(this.buttonStartInventory);
            this.Controls.Add(this.buttonSelectInventoryDirectory);
            this.Controls.Add(this.buttonSelectSaveDirectory);
            this.Name = "GUI";
            this.Text = "Inventory Master";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button buttonSelectSaveDirectory;
        private System.Windows.Forms.Button buttonSelectInventoryDirectory;
        private System.Windows.Forms.Button buttonStartInventory;
    }
}

