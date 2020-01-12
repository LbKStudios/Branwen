using Branwen.FileOutputHandlers;
using Branwen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Branwen
{
	public partial class BuildCompleteInventoryGUI : Form
	{
		public BuildCompleteInventoryGUI()
		{
			InitializeComponent();
		}

		private void SelectedSourceLabel_Click(object sender, System.EventArgs e)
		{
			SelectedSourceLabel.Enabled = false;
			FolderBrowserDialog folderDialog = new FolderBrowserDialog();
			folderDialog.Description = "Set Folder containing inventory file(s)";
			if (folderDialog.ShowDialog() != DialogResult.OK)
			{
				folderDialog.Description = "Select Source Directory";
				SelectedSourceLabel.Enabled = true;
			}
			else
			{
				SelectedSourceLabel.Text = folderDialog.SelectedPath;
			}
			SelectedOutputLabel.Enabled = true;
		}

		private void SelectedOutputLabel_Click(object sender, System.EventArgs e)
		{
			SelectedOutputLabel.Enabled = false;
			FolderBrowserDialog folderDialog = new FolderBrowserDialog();
			folderDialog.Description = "Set Folder to Output Inventory File To";
			if (folderDialog.ShowDialog() != DialogResult.OK)
			{
				folderDialog.Description = "Select Output Directory";
				SelectedOutputLabel.Enabled = true;
			}
			else
			{
				SelectedOutputLabel.Text = folderDialog.SelectedPath;
			}
			GoButton.Enabled = true;
		}

		private void GoButton_Click(object sender, System.EventArgs e)
		{
			GoButton.Enabled = false;
			GoButton.Text = "Working";
			List<SingleInventory> inventories = JsonHandler.ImportAllLocalInventoriesFromFiles(SelectedSourceLabel.Text);
			SingleInventory allFiles = convertMultipleSingleInventoryObjectsToOne(inventories);
			GoButton.Text = $"Imported all individual inventories. Writing complete inventory to Output";
			string outputFile = Path.Combine(SelectedOutputLabel.Text, "CompleteInventory.xlsx");
			try
			{
				int fileCount = SpreadsheetHandler.WriteInventoryToSpreadsheet(outputFile, allFiles);
				MessageBox.Show($"Spreadsheet Complete. Output file is at {outputFile}. Filecount: {fileCount}");
				Application.Exit();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to Inventory:" + Environment.NewLine + ex.Message);
				Application.Exit();
			}
		}

		#region Helper Methods

		private static SingleInventory convertMultipleSingleInventoryObjectsToOne(List<SingleInventory> inventories)
		{
			SingleInventory allFiles = new SingleInventory();
			foreach (SingleInventory inventory in inventories)
			{
				foreach (var item in inventory.TopLevelDirectoriesAndFiles)
				{
					if (allFiles.TopLevelDirectoriesAndFiles.ContainsKey(item.Key))
					{
						allFiles.TopLevelDirectoriesAndFiles[item.Key].AddRange(item.Value);
					}
					else
					{
						inventory.TopLevelDirectoriesAndFiles.Add(item.Key, item.Value);
					}
				}
			}
			foreach (var items in allFiles.TopLevelDirectoriesAndFiles)
			{
				//MAY NEED A FOLDER-LEVEL SORT OF SOME FORM, THIS MAY FAIL BADLY
				items.Value.Sort((a, b) => a.Name.CompareTo(b.Name));
			}
			return allFiles;
		}

		#endregion
	}
}
