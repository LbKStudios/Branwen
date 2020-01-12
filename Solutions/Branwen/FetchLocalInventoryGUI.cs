using Branwen.FileOutputHandlers;
using Branwen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Branwen
{
	public partial class FetchLocalInventoryGUI : Form
	{
		public static string GenericFileNameEnding = "FileInventory.json";

		public FetchLocalInventoryGUI()
		{
			InitializeComponent();
		}

		private void SelectedSourceLabel_Click(object sender, EventArgs e)
		{
			SelectedSourceLabel.Enabled = false;
			FolderBrowserDialog folderDialog = new FolderBrowserDialog();
			folderDialog.Description = "Set Folder to Inventory";
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

		private void SelectedOutputLabel_Click(object sender, EventArgs e)
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

		private void GoButton_Click(object sender, EventArgs e)
		{
			GoButton.Enabled = false;
			GoButton.Text = "Working";
			DirectoryInfo[] topLevelDirectories = new DirectoryInfo(SelectedSourceLabel.Text).GetDirectories();
			SingleInventory inventory = new SingleInventory();
			int fileCount = 0;
			//Loop through all directories and build local inventory
			for (int i = 0; i < topLevelDirectories.Length; i++)
			{
				IEnumerable<BranwenFileInfo> files = RunInventory(topLevelDirectories[i], DriveNameTextBox.Text);
				fileCount += files.Count();
				inventory.TopLevelDirectoriesAndFiles.Add(topLevelDirectories[i].Name, files);
			}
			GoButton.Text = $"Found {fileCount} files. Writing to Output";
			string outputFile = Path.Combine(SelectedOutputLabel.Text, $"{DriveNameTextBox.Text}_{GenericFileNameEnding}");
			try
			{
				JsonHandler.WriteLocalInventoryToFile(inventory, outputFile);
				MessageBox.Show($"Inventory Complete. Output file is at {outputFile}. Filecount: {fileCount}");
				Application.Exit();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to Inventory:" + Environment.NewLine + ex.Message);
				Application.Exit();
			}
		}

		#region Helper Methods

		/// <summary>
		/// Recursively finds all Files in the parent directory
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		private static IEnumerable<BranwenFileInfo> RunInventory(DirectoryInfo parent, string driveName)
		{
			List<BranwenFileInfo> files = new List<BranwenFileInfo>();
			foreach (DirectoryInfo directory in parent.GetDirectories())
			{
				files.AddRange(RunInventory(directory, driveName));
			}
			foreach (FileInfo file in parent.GetFiles())
			{
				BranwenFileInfo newFile = new BranwenFileInfo();
				newFile.Name = Path.GetFileNameWithoutExtension(file.Name);
				newFile.FileType = Path.GetExtension(file.Name);
				newFile.Size = file.Length;
				newFile.DriveName = driveName;
				newFile.Path = file.DirectoryName.Split('\\').ToList();
				newFile.Path.RemoveAt(0);
				files.Add(newFile);
			}
			return files;
		}

		#endregion
	}
}
