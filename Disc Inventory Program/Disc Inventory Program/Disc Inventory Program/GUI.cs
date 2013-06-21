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

namespace Disc_Inventory_Program
{
    public partial class GUI : Form
    {
		DirectoryInfo onDeck;
		DirectoryInfo [] directories;
		FileInfo [] files;
		StreamWriter writer;
		List<FileInfo> test;
		int count = 0;

        public GUI()
        {
            InitializeComponent();
        }

        private void buttonSelectInventoryDirectory_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog folderDialog = new FolderBrowserDialog();
				folderDialog.Description = "Set Folder to Inventory";
				folderDialog.ShowDialog();
				onDeck = new DirectoryInfo(folderDialog.SelectedPath);
			}
			catch(Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("Asking for directory failed" + ex.Message);
			}
        }

		private void buttonSelectFileDirectory_Click(object sender, EventArgs e)
		{
			try
			{
				if(onDeck == null)
				{
					throw new ArgumentNullException("Please select a Directory to Inventory first");
				}
				FolderBrowserDialog folderDialog = new FolderBrowserDialog();
				folderDialog.Description = "Select Destination Folder for Inventory file";
				folderDialog.ShowDialog();
				writer = new StreamWriter(folderDialog.SelectedPath + "\\" + textBoxFileName.Text + ".csv");
				buttonStartInventory.Enabled = true;
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

        private void buttonStartInventory_Click(object sender, EventArgs e)
        {
			try
			{
				count = 0;
				textboxOutput.Clear();
				if(onDeck == null || writer == null)
				{
					throw new ArgumentNullException("Please select a Directory to Inventory or Destination Folder for Inventory File first");
				}
				test = Inventory(onDeck);
				for(int i = 0; i < test.Count; i++)
				{
					writeFile(test[i]);
					count++;
				}
				writer.Close();
				textboxOutput.AppendText("\n");
				textboxOutput.AppendText("Done. " + count + " Items Processed");
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
			writer.Write(Path.GetFileNameWithoutExtension(file.Name) + "," + Path.GetExtension(file.Name));
			string [] path = file.FullName.Split('\\');
			for(int i = 0; i < path.Length - 1; i++)
			{
				writer.Write("," + path[i] + "\\");
			}
			writer.WriteLine("");
			textboxOutput.AppendText(file.FullName + "\n");
			writer.Flush();
		}
    }
}
