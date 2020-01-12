namespace Branwen
{
	partial class FetchLocalInventoryGUI
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
			this.SourceLabel = new System.Windows.Forms.Label();
			this.OutputLabel = new System.Windows.Forms.Label();
			this.GoButton = new System.Windows.Forms.Button();
			this.SelectedSourceLabel = new System.Windows.Forms.Label();
			this.SelectedOutputLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.DriveNameTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// SourceLabel
			// 
			this.SourceLabel.AutoSize = true;
			this.SourceLabel.Location = new System.Drawing.Point(12, 9);
			this.SourceLabel.Name = "SourceLabel";
			this.SourceLabel.Size = new System.Drawing.Size(89, 13);
			this.SourceLabel.TabIndex = 0;
			this.SourceLabel.Text = "Source Directory:";
			// 
			// OutputLabel
			// 
			this.OutputLabel.AutoSize = true;
			this.OutputLabel.Location = new System.Drawing.Point(12, 31);
			this.OutputLabel.Name = "OutputLabel";
			this.OutputLabel.Size = new System.Drawing.Size(87, 13);
			this.OutputLabel.TabIndex = 1;
			this.OutputLabel.Text = "Output Directory:";
			// 
			// GoButton
			// 
			this.GoButton.Enabled = false;
			this.GoButton.Location = new System.Drawing.Point(62, 77);
			this.GoButton.Name = "GoButton";
			this.GoButton.Size = new System.Drawing.Size(75, 23);
			this.GoButton.TabIndex = 2;
			this.GoButton.Text = "Go";
			this.GoButton.UseVisualStyleBackColor = true;
			this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
			// 
			// SelectedSourceLabel
			// 
			this.SelectedSourceLabel.AutoSize = true;
			this.SelectedSourceLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.SelectedSourceLabel.Location = new System.Drawing.Point(107, 9);
			this.SelectedSourceLabel.Name = "SelectedSourceLabel";
			this.SelectedSourceLabel.Size = new System.Drawing.Size(119, 13);
			this.SelectedSourceLabel.TabIndex = 5;
			this.SelectedSourceLabel.Text = "Select Source Directory";
			this.SelectedSourceLabel.Click += new System.EventHandler(this.SelectedSourceLabel_Click);
			// 
			// SelectedOutputLabel
			// 
			this.SelectedOutputLabel.AutoSize = true;
			this.SelectedOutputLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.SelectedOutputLabel.Enabled = false;
			this.SelectedOutputLabel.Location = new System.Drawing.Point(107, 31);
			this.SelectedOutputLabel.Name = "SelectedOutputLabel";
			this.SelectedOutputLabel.Size = new System.Drawing.Size(117, 13);
			this.SelectedOutputLabel.TabIndex = 6;
			this.SelectedOutputLabel.Text = "Select Output Directory";
			this.SelectedOutputLabel.Click += new System.EventHandler(this.SelectedOutputLabel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 54);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Drive Name:";
			// 
			// DriveNameTextBox
			// 
			this.DriveNameTextBox.Location = new System.Drawing.Point(110, 51);
			this.DriveNameTextBox.Name = "DriveNameTextBox";
			this.DriveNameTextBox.Size = new System.Drawing.Size(100, 20);
			this.DriveNameTextBox.TabIndex = 8;
			this.DriveNameTextBox.Text = "Drive Name";
			// 
			// FetchLocalInventoryGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(477, 112);
			this.Controls.Add(this.DriveNameTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.SelectedOutputLabel);
			this.Controls.Add(this.SelectedSourceLabel);
			this.Controls.Add(this.GoButton);
			this.Controls.Add(this.OutputLabel);
			this.Controls.Add(this.SourceLabel);
			this.Name = "FetchLocalInventoryGUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FetchLocalInventoryGUI";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label SourceLabel;
		private System.Windows.Forms.Label OutputLabel;
		private System.Windows.Forms.Button GoButton;
		private System.Windows.Forms.Label SelectedSourceLabel;
		private System.Windows.Forms.Label SelectedOutputLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox DriveNameTextBox;
	}
}