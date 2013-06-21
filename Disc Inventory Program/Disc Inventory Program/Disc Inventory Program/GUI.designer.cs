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
			this.components = new System.ComponentModel.Container();
			this.grpDisplayBox = new System.Windows.Forms.GroupBox();
			this.textboxOutput = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.buttonStartInventory = new System.Windows.Forms.Button();
			this.buttonSelectInventoryDirectory = new System.Windows.Forms.Button();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.buttonSelectFileDirectory = new System.Windows.Forms.Button();
			this.textBoxFileName = new System.Windows.Forms.TextBox();
			this.grpDisplayBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			this.SuspendLayout();
			// 
			// grpDisplayBox
			// 
			this.grpDisplayBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.grpDisplayBox.Controls.Add(this.textboxOutput);
			this.grpDisplayBox.Location = new System.Drawing.Point(12, 40);
			this.grpDisplayBox.Margin = new System.Windows.Forms.Padding(2);
			this.grpDisplayBox.Name = "grpDisplayBox";
			this.grpDisplayBox.Padding = new System.Windows.Forms.Padding(2);
			this.grpDisplayBox.Size = new System.Drawing.Size(831, 433);
			this.grpDisplayBox.TabIndex = 1;
			this.grpDisplayBox.TabStop = false;
			this.grpDisplayBox.Text = "Display Results";
			// 
			// textboxOutput
			// 
			this.textboxOutput.Location = new System.Drawing.Point(4, 17);
			this.textboxOutput.Margin = new System.Windows.Forms.Padding(2);
			this.textboxOutput.Multiline = true;
			this.textboxOutput.Name = "textboxOutput";
			this.textboxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textboxOutput.Size = new System.Drawing.Size(823, 412);
			this.textboxOutput.TabIndex = 0;
			// 
			// buttonStartInventory
			// 
			this.buttonStartInventory.Location = new System.Drawing.Point(573, 12);
			this.buttonStartInventory.Name = "buttonStartInventory";
			this.buttonStartInventory.Size = new System.Drawing.Size(153, 23);
			this.buttonStartInventory.TabIndex = 3;
			this.buttonStartInventory.Text = "Start Inventory";
			this.buttonStartInventory.UseVisualStyleBackColor = true;
			this.buttonStartInventory.Click += new System.EventHandler(this.buttonStartInventory_Click);
			// 
			// buttonSelectInventoryDirectory
			// 
			this.buttonSelectInventoryDirectory.Location = new System.Drawing.Point(12, 12);
			this.buttonSelectInventoryDirectory.Name = "buttonSelectInventoryDirectory";
			this.buttonSelectInventoryDirectory.Size = new System.Drawing.Size(153, 23);
			this.buttonSelectInventoryDirectory.TabIndex = 1;
			this.buttonSelectInventoryDirectory.Text = "Select Directory to Inventory";
			this.buttonSelectInventoryDirectory.UseVisualStyleBackColor = true;
			this.buttonSelectInventoryDirectory.Click += new System.EventHandler(this.buttonSelectInventoryDirectory_Click);
			// 
			// buttonSelectFileDirectory
			// 
			this.buttonSelectFileDirectory.Location = new System.Drawing.Point(414, 12);
			this.buttonSelectFileDirectory.Name = "buttonSelectFileDirectory";
			this.buttonSelectFileDirectory.Size = new System.Drawing.Size(153, 23);
			this.buttonSelectFileDirectory.TabIndex = 2;
			this.buttonSelectFileDirectory.Text = "Select Directory to Store File";
			this.buttonSelectFileDirectory.UseVisualStyleBackColor = true;
			this.buttonSelectFileDirectory.Click += new System.EventHandler(this.buttonSelectFileDirectory_Click);
			// 
			// textBoxFileName
			// 
			this.textBoxFileName.Location = new System.Drawing.Point(171, 12);
			this.textBoxFileName.Name = "textBoxFileName";
			this.textBoxFileName.Size = new System.Drawing.Size(237, 20);
			this.textBoxFileName.TabIndex = 4;
			this.textBoxFileName.Text = "Enter File Name Here";
			// 
			// GUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(854, 484);
			this.Controls.Add(this.textBoxFileName);
			this.Controls.Add(this.buttonSelectFileDirectory);
			this.Controls.Add(this.buttonSelectInventoryDirectory);
			this.Controls.Add(this.buttonStartInventory);
			this.Controls.Add(this.grpDisplayBox);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "GUI";
			this.Text = "Main";
			this.grpDisplayBox.ResumeLayout(false);
			this.grpDisplayBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDisplayBox;
        private System.Windows.Forms.TextBox textboxOutput;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonStartInventory;
        private System.Windows.Forms.Button buttonSelectInventoryDirectory;
        private System.Windows.Forms.BindingSource bindingSource1;
		private System.Windows.Forms.Button buttonSelectFileDirectory;
		private System.Windows.Forms.TextBox textBoxFileName;
    }
}

