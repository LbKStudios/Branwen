using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using Yogesh.ExcelXml;
using Yogesh.Extensions;

namespace Disc_Inventory_Program
{
    public partial class GUI : Form
    {
		DirectoryInfo onDeck;
		DirectoryInfo [] directories;
		FileInfo [] files;
		Stream writer;
		List<FileInfo> test;
		ExcelXmlWorkbook workBook;
		Worksheet sheet;
		int count = 0;
        int sheetCount = 0;
		BackgroundWorker worker;
		
        public GUI()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

		#region buttons

		private void buttonSelectFileDirectory_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog folderDialog = new FolderBrowserDialog();
				folderDialog.Description = "Select Destination Folder for Inventory file";
				folderDialog.ShowDialog();
				workBook = new ExcelXmlWorkbook();
				workBook.Properties.Author = Environment.UserName;
				writer = File.OpenWrite(folderDialog.SelectedPath + "\\" + textBoxFileName.Text + ".xml");
				sheetCount = 0;

				//make this thing and all associated stuff go away
				buttonSelectFileDirectory.Enabled = false;
				buttonSelectFileDirectory.Visible = false;
				textBoxFileName.Enabled = false;
				textBoxFileName.Visible = false;

				//next phase appear
				buttonSelectInventoryDirectory.Enabled = true;
				buttonSelectInventoryDirectory.Visible = true;
				
			}
			catch(ArgumentNullException ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.ParamName);
			}
			catch(IOException ex)
			{
				System.Windows.Forms.MessageBox.Show("Could not make file-" + ex.Message);
			}
			catch(Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("Asking for directory failed" + ex.Message);
			}
		}

        private void buttonSelectInventoryDirectory_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog folderDialog = new FolderBrowserDialog();
				folderDialog.Description = "Set Folder to Inventory";
				folderDialog.ShowDialog();
				onDeck = new DirectoryInfo(folderDialog.SelectedPath);

				//make this thing and all associated stuff go away
				buttonSelectInventoryDirectory.Enabled = false;
				buttonSelectInventoryDirectory.Visible = false;

				//next phase appear
				textBoxWorkSheetName.Enabled = true;
				textBoxWorkSheetName.Visible = true;
				buttonNameWorksheet.Enabled = true;
				buttonNameWorksheet.Visible = true;
			}
			catch(Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("Asking for directory failed" + ex.Message);
			}
        }

        private void buttonNameWorksheet_Click(object sender, EventArgs e)
        {
            sheet = workBook[sheetCount];
            sheet.Name = textBoxWorkSheetName.Text;
			sheet.Font.Name = "Calibri";
			sheet.Font.Size = 11;
			
			//make this thing and all associated stuff go away
			textBoxWorkSheetName.Enabled = false;
			textBoxWorkSheetName.Visible = false;
			buttonNameWorksheet.Enabled = false;
			buttonNameWorksheet.Visible = false;

			//next phase appear
            buttonStartInventory.Enabled = true;
            buttonStartInventory.Visible = true;
        }

        private void buttonStartInventory_Click(object sender, EventArgs e)
        {
			try
			{
				//make this thing and all associated stuff go away
				buttonStartInventory.Enabled = false;
				buttonStartInventory.Visible = false;

				//next phase appear
                buttonRestart.Visible = true;
                buttonNewWorksheet.Visible = true;
				buttonExit.Visible = true;
				textboxOutput.Clear();
				if(onDeck == null || writer == null)
				{
					throw new ArgumentNullException("Please select a Directory to Inventory or Destination Folder for Inventory File first");
				}
				worker = new BackgroundWorker();
				worker.DoWork += new DoWorkEventHandler(worker_DoWork);
				worker.WorkerReportsProgress = true;
				worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_Done);
				worker.ProgressChanged += new ProgressChangedEventHandler(worker_Update);
				worker.RunWorkerAsync();
			}
			catch(ArgumentNullException ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.ParamName);
			}
			catch(Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("Failed to Inventory-" + ex.Message);
			}
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
			workBook.Export(writer);
            writer.Close();

			//make this thing and all associated stuff go away
			buttonRestart.Enabled = false;
			buttonRestart.Visible = false;
			buttonNewWorksheet.Enabled = false;
			buttonNewWorksheet.Visible = false;
			buttonExit.Enabled = false;
			buttonExit.Visible = false;

			//next phase appear
            buttonSelectFileDirectory.Enabled = true;
            buttonSelectFileDirectory.Visible = true;
			textBoxFileName.Enabled = true;
			textBoxFileName.Visible = true;
        }

        private void buttonNewWorksheet_Click(object sender, EventArgs e)
        {
			sheetCount++;
			//make this thing and all associated stuff go away
            buttonRestart.Enabled = false;
            buttonRestart.Visible = false;
            buttonNewWorksheet.Enabled = false;
            buttonNewWorksheet.Visible = false;
			buttonExit.Enabled = false;
			buttonExit.Visible = false;

			//next phase appear
			buttonSelectInventoryDirectory.Enabled = true;
			buttonSelectInventoryDirectory.Visible = true;
        }

		private void buttonExit_Click(object sender, EventArgs e)
		{
			workBook.Export(writer);
			Environment.Exit(0);
		}

		#endregion

		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			count = 0;
			test = Inventory(onDeck);
			for(int i = 0; i < test.Count; i++)
			{
				writeFile(test[i]);
			}
			worker.ReportProgress(100, "Done. ");
		}

		private void worker_Update(object sender, ProgressChangedEventArgs e)
		{
			this.textboxOutput.AppendText((string)e.UserState);
		}

		private void worker_Done(object sender, RunWorkerCompletedEventArgs e)
		{
			this.textboxOutput.AppendText(count + " Items Processed");
			buttonRestart.Enabled = true;
			buttonNewWorksheet.Enabled = true;
			buttonExit.Enabled = true;
		}

		private List<FileInfo> Inventory(DirectoryInfo parent)
        {
			directories = parent.GetDirectories();
			files = parent.GetFiles();
			List<FileInfo> toReturn = new List<FileInfo>();
			List<FileInfo> filesInDirectory = new List<FileInfo>();
			for(int i = 0; i < files.Length; i++)
			{
				filesInDirectory.Add(files[i]);
			}
			for(int i = 0; i < directories.Length; i++)
			{
				directories = parent.GetDirectories();
				toReturn.AddRange(Inventory(directories[i]));
				directories = parent.GetDirectories();
			}
			toReturn.AddRange(filesInDirectory);
			return toReturn;
        }

		private void writeFile(FileInfo file)
		{
			string [] path = file.DirectoryName.Split('\\');
			sheet[0, count].Value = Path.GetFileNameWithoutExtension(file.Name);
			sheet[1, count].Value = Path.GetExtension(file.Name);
			for(int i = 0; i < path.Length; i++)
			{
				sheet[i + 2, count].Value = path[i] + "\\";
			}
			worker.ReportProgress(50, file.FullName + "\n");
			count++;
		}
    }
}
