namespace Branwen
{
	partial class BuildCompleteInventoryGUI
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
			this.GoButton = new System.Windows.Forms.Button();
			this.SourceLabel = new System.Windows.Forms.Label();
			this.OutputLabel = new System.Windows.Forms.Label();
			this.SelectedSourceLabel = new System.Windows.Forms.Label();
			this.SelectedOutputLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// GoButton
			// 
			this.GoButton.Enabled = false;
			this.GoButton.Location = new System.Drawing.Point(85, 73);
			this.GoButton.Name = "GoButton";
			this.GoButton.Size = new System.Drawing.Size(75, 23);
			this.GoButton.TabIndex = 0;
			this.GoButton.Text = "Go";
			this.GoButton.UseVisualStyleBackColor = true;
			this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
			// 
			// SourceLabel
			// 
			this.SourceLabel.AutoSize = true;
			this.SourceLabel.Location = new System.Drawing.Point(12, 9);
			this.SourceLabel.Name = "SourceLabel";
			this.SourceLabel.Size = new System.Drawing.Size(89, 13);
			this.SourceLabel.TabIndex = 1;
			this.SourceLabel.Text = "Source Directory:";
			// 
			// OutputLabel
			// 
			this.OutputLabel.AutoSize = true;
			this.OutputLabel.Location = new System.Drawing.Point(12, 36);
			this.OutputLabel.Name = "OutputLabel";
			this.OutputLabel.Size = new System.Drawing.Size(87, 13);
			this.OutputLabel.TabIndex = 2;
			this.OutputLabel.Text = "Output Directory:";
			// 
			// SelectedSourceLabel
			// 
			this.SelectedSourceLabel.AutoSize = true;
			this.SelectedSourceLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.SelectedSourceLabel.Location = new System.Drawing.Point(107, 9);
			this.SelectedSourceLabel.Name = "SelectedSourceLabel";
			this.SelectedSourceLabel.Size = new System.Drawing.Size(119, 13);
			this.SelectedSourceLabel.TabIndex = 3;
			this.SelectedSourceLabel.Text = "Select Source Directory";
			this.SelectedSourceLabel.Click += new System.EventHandler(this.SelectedSourceLabel_Click);
			// 
			// SelectedOutputLabel
			// 
			this.SelectedOutputLabel.AutoSize = true;
			this.SelectedOutputLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.SelectedOutputLabel.Enabled = false;
			this.SelectedOutputLabel.Location = new System.Drawing.Point(107, 36);
			this.SelectedOutputLabel.Name = "SelectedOutputLabel";
			this.SelectedOutputLabel.Size = new System.Drawing.Size(117, 13);
			this.SelectedOutputLabel.TabIndex = 4;
			this.SelectedOutputLabel.Text = "Select Output Directory";
			this.SelectedOutputLabel.Click += new System.EventHandler(this.SelectedOutputLabel_Click);
			// 
			// BuildCompleteInventoryGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(248, 108);
			this.Controls.Add(this.SelectedOutputLabel);
			this.Controls.Add(this.SelectedSourceLabel);
			this.Controls.Add(this.OutputLabel);
			this.Controls.Add(this.SourceLabel);
			this.Controls.Add(this.GoButton);
			this.Name = "BuildCompleteInventoryGUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BuildInventoryGUI";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button GoButton;
		private System.Windows.Forms.Label SourceLabel;
		private System.Windows.Forms.Label OutputLabel;
		private System.Windows.Forms.Label SelectedSourceLabel;
		private System.Windows.Forms.Label SelectedOutputLabel;
	}
}