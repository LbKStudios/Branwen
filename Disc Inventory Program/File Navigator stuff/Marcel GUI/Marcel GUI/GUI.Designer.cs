namespace Marcel_GUI
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
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.listDirectory = new System.Windows.Forms.ListBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnSelectOriginDirectory = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.grpDisplayBox = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpMain.SuspendLayout();
            this.grpDisplayBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.grpMain.Controls.Add(this.listDirectory);
            this.grpMain.Controls.Add(this.txtInput);
            this.grpMain.Controls.Add(this.btnSelectOriginDirectory);
            this.grpMain.Controls.Add(this.btnInventory);
            this.grpMain.Location = new System.Drawing.Point(12, 12);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(327, 491);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Main Controls";
            // 
            // listDirectory
            // 
            this.listDirectory.FormattingEnabled = true;
            this.listDirectory.ItemHeight = 16;
            this.listDirectory.Location = new System.Drawing.Point(6, 35);
            this.listDirectory.Name = "listDirectory";
            this.listDirectory.Size = new System.Drawing.Size(315, 356);
            this.listDirectory.TabIndex = 4;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(6, 428);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(306, 22);
            this.txtInput.TabIndex = 3;
            // 
            // btnSelectOriginDirectory
            // 
            this.btnSelectOriginDirectory.Location = new System.Drawing.Point(6, 397);
            this.btnSelectOriginDirectory.Name = "btnSelectOriginDirectory";
            this.btnSelectOriginDirectory.Size = new System.Drawing.Size(186, 25);
            this.btnSelectOriginDirectory.TabIndex = 1;
            this.btnSelectOriginDirectory.Text = "Select Origin Directory";
            this.btnSelectOriginDirectory.UseVisualStyleBackColor = true;
            this.btnSelectOriginDirectory.Click += new System.EventHandler(this.btnSelectOriginDirectory_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.Enabled = false;
            this.btnInventory.Location = new System.Drawing.Point(6, 456);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(186, 25);
            this.btnInventory.TabIndex = 2;
            this.btnInventory.Text = "Inventory";
            this.btnInventory.UseVisualStyleBackColor = true;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // grpDisplayBox
            // 
            this.grpDisplayBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.grpDisplayBox.Controls.Add(this.textBox2);
            this.grpDisplayBox.Location = new System.Drawing.Point(345, 12);
            this.grpDisplayBox.Name = "grpDisplayBox";
            this.grpDisplayBox.Size = new System.Drawing.Size(782, 491);
            this.grpDisplayBox.TabIndex = 1;
            this.grpDisplayBox.TabStop = false;
            this.grpDisplayBox.Text = "Display Results";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 35);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(770, 446);
            this.textBox2.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 509);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 321);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1139, 829);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grpDisplayBox);
            this.Controls.Add(this.grpMain);
            this.Name = "GUI";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.grpDisplayBox.ResumeLayout(false);
            this.grpDisplayBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnSelectOriginDirectory;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.ListBox listDirectory;
        private System.Windows.Forms.GroupBox grpDisplayBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

