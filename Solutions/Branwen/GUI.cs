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
        private void buttonSelectAndRunInventory_Click(object sender, EventArgs e)
        {
            try
            {
                //make this thing and all associated stuff go away
                buttonSelectAndRunInventory.Enabled = false;
                buttonSelectAndRunInventory.Text = "Working";
                buttonWipeDb.Enabled = false;
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                folderDialog.Description = "Set Folder to Inventory";
                if (folderDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                DirectoryInfo[] topLevelDirectories = new DirectoryInfo(folderDialog.SelectedPath).GetDirectories();
                if (topLevelDirectories == null)
                {
                    MessageBox.Show("This Directory is Invalid. Please try again");
                    return;
                }


                if (UseDBCheckBox.Checked == true)
                {
                    MySqlConnection mySqlConnection;
                    try
                    {
                        mySqlConnection = new MySqlConnection("server=box654.bluehost.com;user=lbkstud1_smedia;password=#sucK_my_d1ck;database=lbkstud1_SaurutobiMedia;");
                        mySqlConnection.Open();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        return;
                    }


                    for (int i = 0; i < topLevelDirectories.Length; i++)
                    {
                        WriteDirectoryToDatabase(RunInventory(topLevelDirectories[i]), mySqlConnection);
                    }
                }
                else
                {
                    string outputFile = Path.Combine(folderDialog.SelectedPath, "MediadriveInventory.xlsx");
                    if (outputFile == null)
                    {
                        MessageBox.Show("This Directory is Invalid. Please try again");
                        return;
                    }
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
                }

                //Cleanup
                MessageBox.Show("DONE! Files Inventoried:  " + fileCount);
                buttonSelectAndRunInventory.Enabled = true;
                buttonSelectAndRunInventory.Text = "Select Inventory Directory";
                buttonWipeDb.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Inventory:" + Environment.NewLine + ex.Message);
                buttonSelectAndRunInventory.Enabled = true;
                buttonSelectAndRunInventory.Text = "Select Inventory Directory";
                buttonWipeDb.Enabled = true;
            }
        }

        /// <summary>
        /// Recursively finds all Files in the parent directory
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static IEnumerable<FileInfo> RunInventory(DirectoryInfo parent)
        {
            List<FileInfo> toReturn = new List<FileInfo>();
            foreach (DirectoryInfo directory in parent.GetDirectories())
            {
                toReturn.AddRange(RunInventory(directory));
            }
            toReturn.AddRange(parent.GetFiles());
            return toReturn;
        }

        #region Database Methods

        /// <summary>
        /// Writes all files in the Directory to the Databbase
        /// </summary>
        /// <param name="files"></param>
        private void WriteDirectoryToDatabase(IEnumerable<FileInfo> files, MySqlConnection mySqlConnection)
        {
            //delete all with same mediadrive name
            //giant insert statement
            //
            string insertStatement = "INSERT INTO SaurutobiMedia (FileName, Extension, Size, MediaDrive, Type, Folder1, Folder2, Folder3, Folder4, Folder5, Folder6) VALUES";

            bool firstVal = true;
            foreach (FileInfo file in files)
            {
                if(!firstVal)
                {
                    insertStatement += ", ";
                }
                else
                {
                    firstVal = false;
                }
                
                //File Name
                insertStatement += "('" + Path.GetFileNameWithoutExtension(file.Name) + "', ";

                //File Extension
                insertStatement += "'" + Path.GetExtension(file.Name) + "', ";

                //File Size
                insertStatement += "" + (file.Length / 1048576) + ", ";

                List<string> path = file.DirectoryName.Split('\\').ToList();




                //Inventory out of range bullshit



                //MediaDrive
                insertStatement += "'" + path[0] ?? "" + "', ";

                //Type
                insertStatement += "'" + path[1] ?? "" + "', ";

                //Folder1
                insertStatement += "'" + path[2] ?? "" + "', ";

                //Folder2
                insertStatement += "'" + path[3] ?? "" + "', ";

                //Folder3
                insertStatement += "'" + path[4] ?? "" + "', ";

                //Folder4
                insertStatement += "'" + path[5] ?? "" + "', ";

                //Folder5
                insertStatement += "'" + path[6] ?? "" + "', ";

                //Folder6
                insertStatement += "'" + path[7] ?? "" + "', ";

                insertStatement += ")";

                fileCount++;
            }
            insertStatement += ";";
        }

        #endregion

        #region Worksheet Methods

        /// <summary>
        /// Writes all files in the Directory to the Workbook
        /// </summary>
        /// <param name="files"></param>
        /// <param name="sheetData"></param>
        private void WriteDirectoryToWorksheet(IEnumerable<FileInfo> files, SheetData sheetData)
        {
            //Write Worksheet Header
            WriteWorksheetHeader(sheetData);

            foreach(FileInfo file in files)
            {
                Row row = new Row();
                Cell cell = new Cell();
                
                //File Name
                cell.CellValue = new CellValue(Path.GetFileNameWithoutExtension(file.Name));
                cell.DataType = CellValues.String;
                row.Append(cell);

                //File Extension
                cell = new Cell();
                cell.CellValue = new CellValue(Path.GetExtension(file.Name));
                cell.DataType = CellValues.String;
                row.Append(cell);

                //File Size
                cell = new Cell();
                cell.CellValue = new CellValue((file.Length / 1048576).ToString());
                cell.DataType = CellValues.Number;
                row.Append(cell);

                //write all the pathNames to the spreadsheet
                List<string> path = file.DirectoryName.Split('\\').ToList();
                foreach (string pathName in path)
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
        /// Truncates the Database to clear old/renamed items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWipeDb_Click(object sender, EventArgs e)
        {
            buttonWipeDb.Enabled = false;
            buttonSelectAndRunInventory.Enabled = false;
            try
            {
                //open db connection
                //submit nonquery
                //TRUNCATE TABLE bladibla
                buttonWipeDb.Enabled = true;
                buttonSelectAndRunInventory.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Wipe:" + Environment.NewLine + ex.Message);
                buttonWipeDb.Enabled = true;
                buttonSelectAndRunInventory.Enabled = true;
            }
        }
    }
}
