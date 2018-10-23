using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Branwen
{
	public partial class GUI : Form
	{
		private int fileCount;
		public MySqlConnection mySqlConnection;

		public GUI()
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
			fileCount = 0;
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
					SetEnabledControls();
					return;
				}

				DirectoryInfo[] topLevelDirectories = new DirectoryInfo(folderDialog.SelectedPath).GetDirectories();
				string outputFile = Path.Combine(folderDialog.SelectedPath, "DriveInventory.xlsx");
				if (outputFile == null || topLevelDirectories == null)
				{
					MessageBox.Show("This Directory is Invalid. Please try again");
					return;
				}

				if (UseDBCheckBox.Checked)
				{
					#region DataBase

					using (mySqlConnection)
					{
						mySqlConnection.Open();
						//Do the Delete
						string deleteStatement = "DELETE FROM FilePaths WHERE Drive = " + DriveNameTextBox.Text + "; DELETE FROM Files WHERE Drive = " + DriveNameTextBox.Text + ";";
						MySqlCommand deleteCommand = new MySqlCommand(deleteStatement, mySqlConnection);
						deleteCommand.ExecuteNonQuery();

						for (int i = 0; i < topLevelDirectories.Length; i++)
						{
							WriteDirectoryToDatabase(RunInventory(topLevelDirectories[i], DriveNameTextBox.Text), mySqlConnection);
						}

						if (ExportFileCheckBox.Checked)
						{
							//Have to keep track of the fileCount before/after because the WorkSheet writing does the adding
							int oldFileCount = fileCount;
							ExportDatabaseToFile(mySqlConnection, outputFile);
							fileCount = oldFileCount;
						}
					}
					#endregion
				}
				else
				{
					#region SpreadSheet

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
						WriteDirectoryToWorksheet(RunInventory(topLevelDirectories[i], DriveNameTextBox.Text), sheetData);
					}
					spreadsheetDocument.Close();

					#endregion
				}
				MessageBox.Show("DONE! Files Inventoried:  " + fileCount);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to Inventory:" + Environment.NewLine + ex.Message);
			}
			SelectAndRunInventoryButton.Enabled = true;
			SelectAndRunInventoryButton.Text = "Select Directory to Inventory";
			UseDBCheckBox.Enabled = true;
			SetEnabledControls();
		}

		/// <summary>
		/// Switches DB-related controls Enabled property based on if it's checked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UseDBCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			SetEnabledControls();
		}

		#endregion

		#region Database Methods

		/// <summary>
		/// Writes all files in the Directory to the Databbase
		/// </summary>
		/// <param name="files"></param>
		private void WriteDirectoryToDatabase(IEnumerable<BranwenFileInfo> files, MySqlConnection mySqlConnection)
		{
			StringBuilder insertFilesStatement = new StringBuilder();
			StringBuilder insertPathsStatement = new StringBuilder();
			insertFilesStatement.Append("INSERT INTO Files (ID, FileName, Extension, Size, DriveName) VALUES ");
			insertPathsStatement.Append("INSERT INTO FilePaths (FileID, PartOfPath, Path) VALUES ");

			bool firstFileVal = true;
			foreach (BranwenFileInfo file in files)
			{
				if (!firstFileVal)
				{
					insertFilesStatement.Append(", ");
					insertPathsStatement.Append(", ");
				}
				else
				{
					firstFileVal = false;
				}

				//Start of individual value item
				insertFilesStatement.Append("('");

				//ID
				insertFilesStatement.Append($"{file.ID}, ");
				
				//File Name
				insertFilesStatement.Append($"{file.Name.Replace("'", "''")}', ");

				//File Extension
				insertFilesStatement.Append($"'{file.Extension}', ");

				//File Size
				insertFilesStatement.Append($"{file.FileSize}, ");

				//Media Type
				insertFilesStatement.Append($"'{file.MediaType.Replace("'", "''")}', ");

				//Drive Name
				insertFilesStatement.Append(file.DriveName);

				//Closing individual value item
				insertFilesStatement.Append(")");

				bool firstPathVal = true;
				for (int i = 0; i < file.Path.Count; i++)
				{
					if (!firstPathVal)
					{
						insertPathsStatement.Append(", ");
					}
					else
					{
						firstPathVal = false;
					}

					//Start of individual value item
					insertPathsStatement.Append("('");

					//FileID
					insertPathsStatement.Append($"{file.ID}', ");

					//PartOfPath
					insertPathsStatement.Append($"{i}, ");

					//Path
					insertPathsStatement.Append($"'{file.Path[i].Replace("'", "''")}', ");

					//Closing individual value item
					insertPathsStatement.Append(")");
				}

				fileCount++;
			}
			insertFilesStatement.Append(";");
			insertPathsStatement.Append(";");
			Console.WriteLine(insertFilesStatement.ToString());

			MySqlCommand insertCommand = new MySqlCommand(insertFilesStatement.ToString(), mySqlConnection);
			insertCommand.ExecuteNonQuery();
			insertCommand = new MySqlCommand(insertPathsStatement.ToString(), mySqlConnection);
			insertCommand.ExecuteNonQuery();
		}

		/// <summary>
		/// Writes an Excel document with all the contentes of the database
		/// </summary>
		/// <param name="mySqlConnection"></param>
		/// <param name="outputFile"></param>
		private void ExportDatabaseToFile(MySqlConnection mySqlConnection, string outputFile)
		{
			//Get all the MediaTypes
			List<string> mediaTypes = new List<string>();
			MySqlCommand command = new MySqlCommand("SELECT MediaType FROM Files GROUP BY MediaType;", mySqlConnection);
			using (MySqlDataReader mediaTypeReader = command.ExecuteReader())
			{
				while (mediaTypeReader.Read())
				{
					mediaTypes.Add(mediaTypeReader["MediaType"].ToString());
				}
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

			//Loop through all MediaTypes and make a new sheet and put that shit in there
			for (int i = 0; i < mediaTypes.Count; i++)
			{
				WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
				worksheetPart.Worksheet = new Worksheet(new SheetData());
				SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
				Sheet sheet = new Sheet();
				sheet.Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart);
				sheet.Name = mediaTypes[i];
				sheet.SheetId = (UInt32)(i + 1);
				sheets.Append(sheet);

				List<BranwenFileInfo> files = new List<BranwenFileInfo>();

				command = new MySqlCommand($"SELECT FileName, Extension, Size, DriveName FROM Files WHERE MediaType = '{mediaTypes[i]}';", mySqlConnection);
				using (MySqlDataReader fileReader = command.ExecuteReader())
				{
					while (fileReader.Read())
					{
						BranwenFileInfo newFile = new BranwenFileInfo();
						newFile.ID = fileReader["ID"].ToString();
						newFile.Name = fileReader["FileName"].ToString();
						newFile.Extension = fileReader["Extension"].ToString();
						long fileSize = 0;
						if(!long.TryParse(fileReader["Size"].ToString(), out fileSize))
						{
							throw new Exception("file size couldn't parse on: " + newFile.Name);
						}
						newFile.FileSize = fileSize;
						newFile.DriveName = fileReader["DriveName"].ToString();
						files.Add(newFile);
					}
				}

				//Add the Paths
				foreach (BranwenFileInfo file in files)
				{
					MySqlCommand pathCommand = new MySqlCommand($"SELECT Path FROM Paths WHERE FileID = '{file.ID}' ORDER BY PartOfPath asc;", mySqlConnection);
					using (MySqlDataReader pathReader = pathCommand.ExecuteReader())
					{
						file.Path = new List<string>();
						while (pathReader.Read())
						{
							file.Path.Add(pathReader["Path"].ToString());
						}
					}
				}
				WriteDirectoryToWorksheet(files, sheetData);
			}
			spreadsheetDocument.Close();
		}

		#endregion

		#region Worksheet Methods

		/// <summary>
		/// Writes all files in the Directory to the Workbook
		/// </summary>
		/// <param name="files"></param>
		/// <param name="sheetData"></param>
		private void WriteDirectoryToWorksheet(IEnumerable<BranwenFileInfo> files, SheetData sheetData)
		{
			//Write Worksheet Header
			WriteWorksheetHeader(sheetData);

			foreach (BranwenFileInfo file in files)
			{
				Row row = new Row();
				Cell cell = new Cell();

				//File Name
				cell.CellValue = new CellValue(file.Name);
				cell.DataType = CellValues.String;
				row.Append(cell);

				//File Extension
				cell = new Cell();
				cell.CellValue = new CellValue(file.Extension);
				cell.DataType = CellValues.String;
				row.Append(cell);

				//File Size
				cell = new Cell();
				cell.CellValue = new CellValue((file.FileSize / 1048576).ToString());	//Magic Number makes it MB
				cell.DataType = CellValues.Number;
				row.Append(cell);

				//DriveName
				cell = new Cell();
				cell.CellValue = new CellValue(file.DriveName);
				cell.DataType = CellValues.String;
				row.Append(cell);

				//write all the pathNames to the spreadsheet
				foreach (string pathName in file.Path)
				{
					cell = new Cell();
					cell.CellValue = new CellValue(pathName);
					cell.DataType = CellValues.String;
					row.Append(cell);
				}

				sheetData.Append(row);
				fileCount++;
			}
		}

		/// <summary>
		/// Write the standard header to the Worksheet
		/// </summary>
		/// <param name="sheetdata"></param>
		private static void WriteWorksheetHeader(SheetData sheetData)
		{
			Row row = new Row();
			List<string> headers = new List<string> { "File Name", "File-Type", "Size(In MB)", "DriveName", "Folder1", "Folder2", "Folder3", "Folder4", "Folder5", "Folder6", "Folder7" };
			foreach (string header in headers)
			{
				Cell someCell = new Cell();
				someCell.CellValue = new CellValue(header);
				someCell.DataType = CellValues.String;
				row.Append(someCell);
			}
			sheetData.Append(row);
		}

		#endregion

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

		/// <summary>
		/// Enables or Disables controls
		/// </summary>
		private void SetEnabledControls()
		{
			if (UseDBCheckBox.Checked)
			{
				DBConfig dBConfig = new DBConfig(this);
				dBConfig.Show();
				UseDBCheckBox.Enabled = false;
				SelectAndRunInventoryButton.Enabled = false;
			}
			else
			{
				DriveNameTextBox.Enabled = false;
				ExportFileCheckBox.Enabled = false;
			}
		}

		#endregion
	}
}
