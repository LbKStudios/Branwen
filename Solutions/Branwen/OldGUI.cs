using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Branwen.Models;
using Branwen.FileOutputHandlers;

namespace Branwen
{
	public partial class OldGUI : Form
	{
		public OldGUI()
		{
			InitializeComponent();
		}

		#region Button Handlers

		/// <summary>
		/// Button Handler, Selects Directory then does Inventory
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectAndRunInventoryButton_Click(object sender, EventArgs e)
		{
			int fileCount = 0;
			try
			{
				//make this thing and all associated stuff go away
				SelectAndRunInventoryButton.Enabled = false;
				SelectAndRunInventoryButton.Text = "Working";
				DriveNameTextBox.Enabled = false;
				UseDBCheckBox.Enabled = false;
				ExportFileCheckBox.Enabled = false;
				FolderBrowserDialog folderDialog = new FolderBrowserDialog();
				folderDialog.Description = "Set Folder to Inventory";
				if (folderDialog.ShowDialog() != DialogResult.OK)
				{
					SelectAndRunInventoryButton.Enabled = true;
					SelectAndRunInventoryButton.Text = "Select Directory to Inventory";
					UseDBCheckBox.Enabled = true;
					return;
				}

				DirectoryInfo[] topLevelDirectories = new DirectoryInfo(folderDialog.SelectedPath).GetDirectories();
				string outputFile = Path.Combine(folderDialog.SelectedPath, "DriveInventory.xlsx");
				if (outputFile == null || topLevelDirectories == null)
				{
					MessageBox.Show("This Directory is Invalid. Please try again");
					return;
				}
				//Make sure the outputFile doesn't exist
				if (File.Exists(outputFile))
				{
					File.Delete(outputFile);
				}

				//Create worksheet
				SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(outputFile, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook);
				WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
				workbookpart.Workbook = new Workbook();
				Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());

				//Loop through all Directories and make a new sheet and put that shit in there
				for (int i = 0; i < topLevelDirectories.Length; i++)
				{
					WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
					worksheetPart.Worksheet = new Worksheet(new SheetData());
					SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
					Sheet sheet = new Sheet();
					sheet.Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart);
					sheet.Name = topLevelDirectories[i].Name;
					sheet.SheetId = (UInt32)(i + 1);
					sheets.Append(sheet);
					//fileCount += SpreadsheetHandler.WriteDirectoryToWorksheet(RunInventory(topLevelDirectories[i], DriveNameTextBox.Text), sheetData);
				}
				spreadsheetDocument.Close();

				MessageBox.Show("DONE! Files Inventoried:  " + fileCount);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to Inventory:" + Environment.NewLine + ex.Message);
			}
			SelectAndRunInventoryButton.Enabled = true;
			SelectAndRunInventoryButton.Text = "Select Directory to Inventory";
			UseDBCheckBox.Enabled = true;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Recursively finds all Files in the parent directory
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		private static IEnumerable<OldBranwenFileInfo> RunInventory(DirectoryInfo parent, string driveName)
		{
			List<OldBranwenFileInfo> files = new List<OldBranwenFileInfo>();
			foreach (DirectoryInfo directory in parent.GetDirectories())
			{
				files.AddRange(RunInventory(directory, driveName));
			}
			foreach (FileInfo file in parent.GetFiles())
			{
				OldBranwenFileInfo newFile = new OldBranwenFileInfo();
				newFile.ID = Guid.NewGuid().ToString();
				newFile.Name = Path.GetFileNameWithoutExtension(file.Name);
				newFile.Extension = Path.GetExtension(file.Name);
				newFile.FileSize = file.Length;
				newFile.DriveName = driveName;
				newFile.Path = file.DirectoryName.Split('\\').ToList();
				newFile.MediaType = newFile.Path[1];
				newFile.Path.RemoveAt(0);
				files.Add(newFile);
			}
			return files;
		}

		#endregion
	}
}
