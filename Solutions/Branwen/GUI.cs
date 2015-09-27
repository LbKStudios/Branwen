using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Branwen
{
    public partial class GUI : Form
    {
		private DirectoryInfo [] directories;
		private FileInfo [] files;
        private int fileCount = 0;
		
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
                buttonSelectAndRunInventory.Enabled = true;
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                folderDialog.Description = "Set Folder to Inventory";
                if (folderDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                DirectoryInfo[] topLevelDirectories = new DirectoryInfo(folderDialog.SelectedPath).GetDirectories();
                string outputFile = Path.Combine(folderDialog.SelectedPath, "MediadriveInventory.xlsx");
                if (topLevelDirectories == null || outputFile == null)
                {
                    MessageBox.Show("This Directory is Invalid. Please try again");
                    return;
                }

                //make this thing and all associated stuff go away
                buttonSelectAndRunInventory.Enabled = false;
                buttonSelectAndRunInventory.Text = "Working";

                if(File.Exists(outputFile))
                {
                    File.Delete(outputFile);
                }

                //Create worksheet
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(outputFile, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

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
                MessageBox.Show("DONE! Files Inventoried:  " + fileCount);
                buttonSelectAndRunInventory.Enabled = true;
                buttonSelectAndRunInventory.Text = "Select Inventory Directory";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Inventory:" + Environment.NewLine + ex.Message);
                buttonSelectAndRunInventory.Enabled = true;
                buttonSelectAndRunInventory.Text = "Select Inventory Directory";
            }
        }

        /// <summary>
        /// Recursively finds all Files in the parent directory
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private List<FileInfo> RunInventory(DirectoryInfo parent)
        {
            directories = parent.GetDirectories();
            files = parent.GetFiles();
            List<FileInfo> toReturn = new List<FileInfo>();
            List<FileInfo> filesInDirectory = new List<FileInfo>();
            for (int i = 0; i < files.Length; i++)
            {
                filesInDirectory.Add(files[i]);
            }
            for (int i = 0; i < directories.Length; i++)
            {
                //TODO:DEBUG WITH THIS LINE GONE:
                //directories = parent.GetDirectories();
                toReturn.AddRange(RunInventory(directories[i]));
                //TODO:DEBUG WITH THIS LINE GONE:
                directories = parent.GetDirectories();
            }
            toReturn.AddRange(filesInDirectory);
            return toReturn;
        }

        /// <summary>
        /// Writes all files in the Directory to the Workbook
        /// </summary>
        /// <param name="files"></param>
        /// <param name="writer"></param>
        private void WriteDirectoryToWorksheet(List<FileInfo> files, SheetData sheetData)
        {
            //Write Worksheet Header
            WriteHeader(sheetData);

            for (int i = 0; i < files.Count; i++)
            {
                Row row = new Row();
                Cell cell = new Cell();
                
                //File Name
                cell.CellValue = new CellValue(Path.GetFileNameWithoutExtension(files[i].Name));
                cell.DataType = CellValues.String;
                row.Append(cell);

                //File Extension
                cell = new Cell();
                cell.CellValue = new CellValue(Path.GetExtension(files[i].Name));
                cell.DataType = CellValues.String;
                row.Append(cell);

                //Filesize
                cell = new Cell();
                cell.CellValue = new CellValue((files[i].Length / 1048576).ToString());
                cell.DataType = CellValues.Number;
                row.Append(cell);

                //write all the pathNames to the spreadsheet
                List<string> path = files[i].DirectoryName.Split('\\').ToList(); ;
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
        /// <param name="worksheet"></param>
        private void WriteHeader(SheetData sheetData)
        {
            Row row = new Row();
            List<string> headers = new List<string>() { "File Name", "File-Type", "Size(In MB)", "Folder1", "Folder2", "Folder3", "Folder4", "Folder5", "Folder6", "Folder7" };
            foreach(string header in headers)
            {
                Cell someCell = new Cell();
                someCell.CellValue = new CellValue(header);
                someCell.DataType = CellValues.String;
                row.Append(someCell);
            }
            sheetData.Append(row);
        }
    }
}
