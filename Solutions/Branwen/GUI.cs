using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Branwen
{
    public partial class GUI : Form
    {
        private int fileCount;
		
        public GUI()
        {
            InitializeComponent();
        }

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
                WipeDbButton.Enabled = false;
				MediaDriveNumberTextBox.Enabled = false;
                UseDBCheckBox.Enabled = false;
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                folderDialog.Description = "Set Folder to Inventory";
                if (folderDialog.ShowDialog() != DialogResult.OK)
                {
					SelectAndRunInventoryButton.Enabled = true;
					SelectAndRunInventoryButton.Text = "Select Inventory Directory";
                    WipeDbButton.Enabled = true;
                    MediaDriveNumberTextBox.Enabled = true;
                    UseDBCheckBox.Enabled = true;
                    return;
                }

                DirectoryInfo[] topLevelDirectories = new DirectoryInfo(folderDialog.SelectedPath).GetDirectories();
				string outputFile = Path.Combine(folderDialog.SelectedPath, "MediadriveInventory.xlsx");
                if (outputFile == null || topLevelDirectories == null)
                {
                    MessageBox.Show("This Directory is Invalid. Please try again");
                    return;
                }

                if (UseDBCheckBox.Checked == true)
                {
                    #region DB

                    MySqlConnection mySqlConnection;
                    try
                    {
                        mySqlConnection = new MySqlConnection("server=box654.bluehost.com;user=lbkstud1_smedia;password=#sucK_my_d1ck;database=lbkstud1_SaurutobiMedia;");
                        mySqlConnection.Open();
                        //Do the Delete
						string deleteStatement = "DELETE FROM SaurutobiMediaPaths WHERE MediaDrive = " + MediaDriveNumberTextBox.Text + "; DELETE FROM SaurutobiMediaFiles WHERE MediaDrive = " + MediaDriveNumberTextBox.Text + ";";
                        MySqlCommand deleteCommand = new MySqlCommand(deleteStatement, mySqlConnection);
                        deleteCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        return;
                    }
                    
                    for (int i = 0; i < topLevelDirectories.Length; i++)
                    {
						WriteDirectoryToDatabase(RunInventory(topLevelDirectories[i]), mySqlConnection, MediaDriveNumberTextBox.Text);
                    }

					if (ExportFileCheckBox.Checked)
					{
						ExportDatabaseToFile(mySqlConnection, outputFile);
					}

					mySqlConnection.Close();

                    #endregion
                }
                else
                {
                    #region SpreadSheet

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
                        sheet.SheetId = (UInt32)Convert.ToInt32(i + 1);
                        sheets.Append(sheet);
                        WriteDirectoryToWorksheet(RunInventory(topLevelDirectories[i]), sheetData);
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
			SelectAndRunInventoryButton.Text = "Select Inventory Directory";
			WipeDbButton.Enabled = true;
			MediaDriveNumberTextBox.Enabled = true;
            UseDBCheckBox.Enabled = true;
        }

        /// <summary>
        /// Recursively finds all Files in the parent directory
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
		private static IEnumerable<BranwenFileInfo> RunInventory(DirectoryInfo parent)
        {
			List<BranwenFileInfo> toReturn = new List<BranwenFileInfo>();
            foreach (DirectoryInfo directory in parent.GetDirectories())
            {
                toReturn.AddRange(RunInventory(directory));
            }
			foreach(FileInfo file in parent.GetFiles())
			{
				BranwenFileInfo newFile = new BranwenFileInfo();
				newFile.Name = Path.GetFileNameWithoutExtension(file.Name);
				newFile.Extension = Path.GetExtension(file.Name);
				newFile.FileSize = file.Length;
				newFile.Path = file.DirectoryName.Split('\\').ToList();
				toReturn.Add(newFile);
			}
			return toReturn;
        }

        #region Database Methods

        /// <summary>
        /// Writes all files in the Directory to the Databbase
        /// </summary>
        /// <param name="files"></param>
		private void WriteDirectoryToDatabase(IEnumerable<BranwenFileInfo> files, MySqlConnection mySqlConnection, string mediaDrive)
        {
            string insertFilesStatement = "INSERT INTO SaurutobiMediaFiles (FileName, Extension, Size, MediaType, MediaDrive) VALUES ";
            string insertPathsStatement = "INSERT INTO SaurutobiMediaPaths (FileName, PartOfPath, Path, MediaDrive) VALUES ";

            bool firstFileVal = true;
			foreach (BranwenFileInfo file in files)
            {
                if (!firstFileVal)
                {
                    insertFilesStatement += ", ";
                    insertPathsStatement += ", ";
                }
                else
                {
                    firstFileVal = false;
                }

                //File Name
                insertFilesStatement += "('" + file.Name.Replace("'", "''") + "', ";

                //File Extension
                insertFilesStatement += "'" + file.Name + "', ";

                //File Size
                insertFilesStatement += (file.FileSize / 1048576) + ", ";

                //Media Type
                insertFilesStatement += "'" + file.Path[2].Replace("'", "''") + "', ";

                //MediaDrive
                insertFilesStatement += mediaDrive + ")";

                bool firstPathVal = true;
				for (int i = 0; i < file.Path.Count; i++)
                {
                    if (!firstPathVal)
                    {
                        insertPathsStatement += ", ";
                    }
                    else
                    {
                        firstPathVal = false;
                    }
                    //File Name
                    insertPathsStatement += "('" + file.Name.Replace("'", "''") + "', ";

                    //PartOfPath
                    insertPathsStatement += i + ", ";

                    //Path
					insertPathsStatement += "'" + file.Path[i].Replace("'", "''") + "', ";

                    //MediaDrive
                    insertPathsStatement += mediaDrive + ")";
                }

                fileCount++;
            }
            insertFilesStatement += ";";
            insertPathsStatement += ";";

            MySqlCommand insertCommand = new MySqlCommand(insertFilesStatement, mySqlConnection);
            insertCommand.ExecuteNonQuery();
            insertCommand = new MySqlCommand(insertPathsStatement, mySqlConnection);
            insertCommand.ExecuteNonQuery();
        }

		/// <summary>
		/// Writes an Excel document with all the contentes of the database
		/// </summary>
		/// <param name="mySqlConnection"></param>
		/// <param name="outputFile"></param>
		private void ExportDatabaseToFile(MySqlConnection mySqlConnection, string outputFile)
		{
			string[] types = new string[5];
			//populate with DBTypes

			List<BranwenFileInfo> toReturn = new List<BranwenFileInfo>();
			//populate file filestuff, make sure to make some form of fileInfo

			FileInfo test = new FileInfo("somefilename");

			//grab data, assort into files list properly, then dump to WriteDirectoryToWorksheet
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
			for (int i = 0; i < types.Length; i++)
			{
				WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
				worksheetPart.Worksheet = new Worksheet(new SheetData());
				SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
				Sheet sheet = new Sheet();
				sheet.Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart);
				sheet.Name = types[i];
				sheet.SheetId = (UInt32)Convert.ToInt32(i + 1);
				sheets.Append(sheet);
				WriteDirectoryToWorksheet(toReturn, sheetData);
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
				cell.CellValue = new CellValue((file.FileSize / 1048576).ToString());
                cell.DataType = CellValues.Number;
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
            List<string> headers = new List<string> { "File Name", "File-Type", "Size(In MB)", "Folder1", "Folder2", "Folder3", "Folder4", "Folder5", "Folder6", "Folder7" };
            foreach(string header in headers)
            {
                Cell someCell = new Cell();
                someCell.CellValue = new CellValue(header);
                someCell.DataType = CellValues.String;
                row.Append(someCell);
            }
            sheetData.Append(row);
        }

        #endregion

        /// <summary>
        /// Truncates the Database to clear all items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void WipeDbButton_Click(object sender, EventArgs e)
        {
            WipeDbButton.Enabled = false;
            SelectAndRunInventoryButton.Enabled = false;
            SelectAndRunInventoryButton.Text = "Working";
            MediaDriveNumberTextBox.Enabled = false;
            UseDBCheckBox.Enabled = false;

            try
            {
                MySqlConnection mySqlConnection;
                mySqlConnection = new MySqlConnection("server=box654.bluehost.com;user=lbkstud1_smedia;password=#sucK_my_d1ck;database=lbkstud1_SaurutobiMedia;");
                mySqlConnection.Open();

                string deleteStatement = "TRUNCATE TABLE SaurutobiMediaPaths; TRUNCATE TABLE SaurutobiMediaFiles;";
                MySqlCommand deleteCommand = new MySqlCommand(deleteStatement, mySqlConnection);
                deleteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Wipe:" + Environment.NewLine + ex.Message);
            }
            WipeDbButton.Enabled = true;
            SelectAndRunInventoryButton.Enabled = true;
            SelectAndRunInventoryButton.Text = "Select Inventory Directory";
            MediaDriveNumberTextBox.Enabled = true;
            UseDBCheckBox.Enabled = true;
        }

		/// <summary>
		/// Switched DB-related controls Enabled property based on if it's checked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UseDBCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if(this.UseDBCheckBox.Checked)
			{
				WipeDbButton.Enabled = true;
				MediaDriveNumberTextBox.Enabled = true;
				ExportFileCheckBox.Enabled = true;
			}
			else
			{
				WipeDbButton.Enabled = false;
				MediaDriveNumberTextBox.Enabled = false;
				ExportFileCheckBox.Enabled = false;
			}
		}
    }
}
