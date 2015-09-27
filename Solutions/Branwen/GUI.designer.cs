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
            this.SuspendLayout();
            // 
            // buttonSelectAndRunInventory
            // 
            this.buttonSelectAndRunInventory.Location = new System.Drawing.Point(12, 12);
            this.buttonSelectAndRunInventory.Name = "buttonSelectAndRunInventory";
            this.buttonSelectAndRunInventory.Size = new System.Drawing.Size(320, 23);
            this.buttonSelectAndRunInventory.TabIndex = 1;
            this.buttonSelectAndRunInventory.Text = "Select Inventory Directory";
            this.buttonSelectAndRunInventory.UseVisualStyleBackColor = true;
            this.buttonSelectAndRunInventory.Click += new System.EventHandler(this.buttonSelectAndRunInventory_Click);
            // 
            // GUI
            // 
            this.ClientSize = new System.Drawing.Size(351, 44);
            this.Controls.Add(this.buttonSelectAndRunInventory);
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Branwen Automatic Inventory";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSelectAndRunInventory;
    }
}

