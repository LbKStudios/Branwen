namespace Branwen
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
            this.buttonSelectAndRunInventory = new System.Windows.Forms.Button();
            this.UseDBCheckBox = new System.Windows.Forms.CheckBox();
            this.buttonWipeDb = new System.Windows.Forms.Button();
            this.textBoxMediaDriveNumber = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonSelectAndRunInventory
            // 
            this.buttonSelectAndRunInventory.Location = new System.Drawing.Point(68, 37);
            this.buttonSelectAndRunInventory.Name = "buttonSelectAndRunInventory";
            this.buttonSelectAndRunInventory.Size = new System.Drawing.Size(146, 23);
            this.buttonSelectAndRunInventory.TabIndex = 1;
            this.buttonSelectAndRunInventory.Text = "Select Inventory Directory";
            this.buttonSelectAndRunInventory.UseVisualStyleBackColor = true;
            this.buttonSelectAndRunInventory.Click += new System.EventHandler(this.buttonSelectAndRunInventory_Click);
            // 
            // UseDBCheckBox
            // 
            this.UseDBCheckBox.AutoSize = true;
            this.UseDBCheckBox.Location = new System.Drawing.Point(12, 12);
            this.UseDBCheckBox.Name = "UseDBCheckBox";
            this.UseDBCheckBox.Size = new System.Drawing.Size(60, 17);
            this.UseDBCheckBox.TabIndex = 2;
            this.UseDBCheckBox.Text = "UseDB";
            this.UseDBCheckBox.UseVisualStyleBackColor = true;
            // 
            // buttonWipeDb
            // 
            this.buttonWipeDb.Location = new System.Drawing.Point(224, 6);
            this.buttonWipeDb.Name = "buttonWipeDb";
            this.buttonWipeDb.Size = new System.Drawing.Size(68, 23);
            this.buttonWipeDb.TabIndex = 3;
            this.buttonWipeDb.Text = "Wipe DB";
            this.buttonWipeDb.UseVisualStyleBackColor = true;
            this.buttonWipeDb.Click += new System.EventHandler(this.buttonWipeDb_Click);
            // 
            // textBoxMediaDriveNumber
            // 
            this.textBoxMediaDriveNumber.Location = new System.Drawing.Point(76, 8);
            this.textBoxMediaDriveNumber.Name = "textBoxMediaDriveNumber";
            this.textBoxMediaDriveNumber.Size = new System.Drawing.Size(142, 20);
            this.textBoxMediaDriveNumber.TabIndex = 4;
            this.textBoxMediaDriveNumber.Text = "MediaDrive Number";
            // 
            // GUI
            // 
            this.ClientSize = new System.Drawing.Size(304, 67);
            this.Controls.Add(this.textBoxMediaDriveNumber);
            this.Controls.Add(this.buttonWipeDb);
            this.Controls.Add(this.UseDBCheckBox);
            this.Controls.Add(this.buttonSelectAndRunInventory);
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Branwen Automatic Inventory";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSelectAndRunInventory;
        private System.Windows.Forms.CheckBox UseDBCheckBox;
        private System.Windows.Forms.Button buttonWipeDb;
        private System.Windows.Forms.TextBox textBoxMediaDriveNumber;
    }
}

