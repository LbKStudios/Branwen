namespace Branwen
{
	partial class DBConfig
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
			this.WipeDBButton = new System.Windows.Forms.Button();
			this.TestConnectionButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ServerTextBox = new System.Windows.Forms.TextBox();
			this.DatabaseTextBox = new System.Windows.Forms.TextBox();
			this.UserTextBox = new System.Windows.Forms.TextBox();
			this.PasswordTextBox = new System.Windows.Forms.TextBox();
			this.CloseButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// WipeDBButton
			// 
			this.WipeDBButton.Enabled = false;
			this.WipeDBButton.Location = new System.Drawing.Point(108, 110);
			this.WipeDBButton.Name = "WipeDBButton";
			this.WipeDBButton.Size = new System.Drawing.Size(75, 23);
			this.WipeDBButton.TabIndex = 0;
			this.WipeDBButton.Text = "Wipe DB";
			this.WipeDBButton.UseVisualStyleBackColor = true;
			this.WipeDBButton.Click += new System.EventHandler(this.WipeDBButton_Click);
			// 
			// TestConnectionButton
			// 
			this.TestConnectionButton.Location = new System.Drawing.Point(7, 110);
			this.TestConnectionButton.Name = "TestConnectionButton";
			this.TestConnectionButton.Size = new System.Drawing.Size(95, 23);
			this.TestConnectionButton.TabIndex = 1;
			this.TestConnectionButton.Text = "Test Connection";
			this.TestConnectionButton.UseVisualStyleBackColor = true;
			this.TestConnectionButton.Click += new System.EventHandler(this.TestConnectionButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Server";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Database";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "User";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 87);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Password";
			// 
			// ServerTextBox
			// 
			this.ServerTextBox.Location = new System.Drawing.Point(73, 6);
			this.ServerTextBox.Name = "ServerTextBox";
			this.ServerTextBox.Size = new System.Drawing.Size(180, 20);
			this.ServerTextBox.TabIndex = 6;
			// 
			// DatabaseTextBox
			// 
			this.DatabaseTextBox.Location = new System.Drawing.Point(73, 32);
			this.DatabaseTextBox.Name = "DatabaseTextBox";
			this.DatabaseTextBox.Size = new System.Drawing.Size(180, 20);
			this.DatabaseTextBox.TabIndex = 7;
			// 
			// UserTextBox
			// 
			this.UserTextBox.Location = new System.Drawing.Point(73, 58);
			this.UserTextBox.Name = "UserTextBox";
			this.UserTextBox.Size = new System.Drawing.Size(180, 20);
			this.UserTextBox.TabIndex = 8;
			// 
			// PasswordTextBox
			// 
			this.PasswordTextBox.Location = new System.Drawing.Point(73, 84);
			this.PasswordTextBox.Name = "PasswordTextBox";
			this.PasswordTextBox.Size = new System.Drawing.Size(180, 20);
			this.PasswordTextBox.TabIndex = 9;
			// 
			// CloseButton
			// 
			this.CloseButton.Location = new System.Drawing.Point(189, 110);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(75, 23);
			this.CloseButton.TabIndex = 10;
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// DBConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(270, 139);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.PasswordTextBox);
			this.Controls.Add(this.UserTextBox);
			this.Controls.Add(this.DatabaseTextBox);
			this.Controls.Add(this.ServerTextBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TestConnectionButton);
			this.Controls.Add(this.WipeDBButton);
			this.Name = "DBConfig";
			this.Text = "Database Config & Credentials";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button WipeDBButton;
		private System.Windows.Forms.Button TestConnectionButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.TextBox UserTextBox;
		protected System.Windows.Forms.TextBox ServerTextBox;
		protected System.Windows.Forms.TextBox DatabaseTextBox;
		protected System.Windows.Forms.TextBox PasswordTextBox;
		private System.Windows.Forms.Button CloseButton;
	}
}