namespace Branwen
{
    partial class OldGUI
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
			this.SelectAndRunInventoryButton = new System.Windows.Forms.Button();
			this.UseDBCheckBox = new System.Windows.Forms.CheckBox();
			this.DriveNameTextBox = new System.Windows.Forms.TextBox();
			this.ExportFileCheckBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// SelectAndRunInventoryButton
			// 
			this.SelectAndRunInventoryButton.Location = new System.Drawing.Point(123, 34);
			this.SelectAndRunInventoryButton.Name = "SelectAndRunInventoryButton";
			this.SelectAndRunInventoryButton.Size = new System.Drawing.Size(169, 23);
			this.SelectAndRunInventoryButton.TabIndex = 1;
			this.SelectAndRunInventoryButton.Text = "Select Directory to Inventory";
			this.SelectAndRunInventoryButton.UseVisualStyleBackColor = true;
			this.SelectAndRunInventoryButton.Click += new System.EventHandler(this.SelectAndRunInventoryButton_Click);
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
			// DriveNameTextBox
			// 
			this.DriveNameTextBox.Enabled = false;
			this.DriveNameTextBox.Location = new System.Drawing.Point(76, 8);
			this.DriveNameTextBox.Name = "DriveNameTextBox";
			this.DriveNameTextBox.Size = new System.Drawing.Size(142, 20);
			this.DriveNameTextBox.TabIndex = 4;
			this.DriveNameTextBox.Text = "Drive Name";
			// 
			// ExportFileCheckBox
			// 
			this.ExportFileCheckBox.AutoSize = true;
			this.ExportFileCheckBox.Enabled = false;
			this.ExportFileCheckBox.Location = new System.Drawing.Point(12, 35);
			this.ExportFileCheckBox.Name = "ExportFileCheckBox";
			this.ExportFileCheckBox.Size = new System.Drawing.Size(105, 17);
			this.ExportFileCheckBox.TabIndex = 5;
			this.ExportFileCheckBox.Text = "Export DB to File";
			this.ExportFileCheckBox.UseVisualStyleBackColor = true;
			// 
			// GUI
			// 
			this.ClientSize = new System.Drawing.Size(304, 65);
			this.Controls.Add(this.ExportFileCheckBox);
			this.Controls.Add(this.DriveNameTextBox);
			this.Controls.Add(this.UseDBCheckBox);
			this.Controls.Add(this.SelectAndRunInventoryButton);
			this.Name = "GUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Branwen Automatic Inventory";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		public System.Windows.Forms.Button SelectAndRunInventoryButton;
		public System.Windows.Forms.CheckBox UseDBCheckBox;
		public System.Windows.Forms.TextBox DriveNameTextBox;
		public System.Windows.Forms.CheckBox ExportFileCheckBox;
	}
}

