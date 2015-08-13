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
using System.Xml;
using OfficeOpenXml;

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

                File.Delete(outputFile);
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(outputFile)))
                {
                    excelPackage.Workbook.Properties.Author = "Zeeshan Umar";
                    excelPackage.Workbook.Properties.Title = "EPPlus Sample";
                    foreach (DirectoryInfo topLevelDirectory in topLevelDirectories)
                    {
                        ExcelWorksheet worksheet = CreateWorkSheet(excelPackage, topLevelDirectory.Name);
                        WriteDirectoryToWorksheet(topLevelDirectory, RunInventory(topLevelDirectory), worksheet);
                    }
                    excelPackage.Save();
                }

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

        private ExcelWorksheet CreateWorkSheet(ExcelPackage excelPackage, string worksheetName)
        {
            excelPackage.Workbook.Worksheets.Add(worksheetName);
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[excelPackage.Workbook.Worksheets.Count];
            worksheet.Name = worksheetName;
            return worksheet;
        }

        /// <summary>
        /// Writes all files in the Directory to the Workbook
        /// </summary>
        /// <param name="files"></param>
        /// <param name="writer"></param>
        private void WriteDirectoryToWorksheet(DirectoryInfo parent, List<FileInfo> files, ExcelWorksheet worksheet)
        {
            //Write Worksheet Header
            WriteHeader(worksheet);

            for (int i = 0; i < files.Count; i++)
            {
                //using i+2 because header is at 1 and it's easier to do count in index values rather than starting i at 2 and then doing i-2 everywhere
                worksheet.Cell(i + 2, 1).Value = Path.GetFileNameWithoutExtension(files[i].Name);
                worksheet.Cell(i + 2, 2).Value = Path.GetExtension(files[i].Name);
                double merp = (files[i].Length / 1024);
                worksheet.Cell(i + 2, 3).Value = (files[i].Length / 1024).ToString();  //.Length comes back in Bytes, want it in MegaBytes so divide by 1024

                //write all the paths to the spreadsheet
                string[] path = files[i].DirectoryName.Split('\\');
                for (int k = 0; k < path.Length; k++)   //using 'k' because 'j' is too easily confused with 'i'
                {
                    worksheet.Cell(i + 2, k + 4).Value = path[k] + @"\";
                }
                fileCount++;
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
        /// Write the standard header to the Worksheet
        /// </summary>
        /// <param name="worksheet"></param>
        private void WriteHeader(ExcelWorksheet worksheet)
        {
            List<string> headers = new List<string>() { "File Name", "File-Type", "Size(In MB)", "Folder1", "Folder2", "Folder3", "Folder4", "Folder5", "Folder6", "Folder7" };
            for(int i = 0; i < headers.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
            }
        }
    }
}
