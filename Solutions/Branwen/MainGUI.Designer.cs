namespace Branwen
{
	partial class MainGUI
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
			this.FetchLocalInventoryButton = new System.Windows.Forms.Button();
			this.BuildCompleteInventoryButton = new System.Windows.Forms.Button();
			this.HelpButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// FetchLocalInventoryButton
			// 
			this.FetchLocalInventoryButton.Location = new System.Drawing.Point(12, 12);
			this.FetchLocalInventoryButton.Name = "FetchLocalInventoryButton";
			this.FetchLocalInventoryButton.Size = new System.Drawing.Size(145, 23);
			this.FetchLocalInventoryButton.TabIndex = 0;
			this.FetchLocalInventoryButton.Text = "Fetch Local Inventory";
			this.FetchLocalInventoryButton.UseVisualStyleBackColor = true;
			this.FetchLocalInventoryButton.Click += new System.EventHandler(this.FetchLocalInventoryButton_Click);
			// 
			// BuildCompleteInventoryButton
			// 
			this.BuildCompleteInventoryButton.Location = new System.Drawing.Point(174, 13);
			this.BuildCompleteInventoryButton.Name = "BuildCompleteInventoryButton";
			this.BuildCompleteInventoryButton.Size = new System.Drawing.Size(146, 23);
			this.BuildCompleteInventoryButton.TabIndex = 1;
			this.BuildCompleteInventoryButton.Text = "Build Complete Inventory";
			this.BuildCompleteInventoryButton.UseVisualStyleBackColor = true;
			this.BuildCompleteInventoryButton.Click += new System.EventHandler(this.BuildCompleteInventoryButton_Click);
			// 
			// HelpButton
			// 
			this.HelpButton.Location = new System.Drawing.Point(96, 52);
			this.HelpButton.Name = "HelpButton";
			this.HelpButton.Size = new System.Drawing.Size(140, 23);
			this.HelpButton.TabIndex = 2;
			this.HelpButton.Text = "Help (Opens Browser)";
			this.HelpButton.UseVisualStyleBackColor = true;
			this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
			// 
			// MainGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(338, 87);
			this.Controls.Add(this.HelpButton);
			this.Controls.Add(this.BuildCompleteInventoryButton);
			this.Controls.Add(this.FetchLocalInventoryButton);
			this.Name = "MainGUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GUI";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button FetchLocalInventoryButton;
		private System.Windows.Forms.Button BuildCompleteInventoryButton;
		private System.Windows.Forms.Button HelpButton;
	}
}