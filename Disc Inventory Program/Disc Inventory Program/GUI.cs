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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;

namespace Disc_Inventory_Program
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

		#region buttons

        /// <summary>
        /// Select the Directory to Inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectAndRunInventory_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                folderDialog.Description = "Set Folder to Inventory";
                folderDialog.ShowDialog();
                DirectoryInfo[] topLevelDirectories = new DirectoryInfo(folderDialog.SelectedPath).GetDirectories();
                Stream outputStream = new FileStream(folderDialog.SelectedPath + "\\mediadriveInventory.xml", FileMode.OpenOrCreate);
                if (topLevelDirectories == null || outputStream == null)
                {
                    throw new ArgumentNullException("Directory Invalid");
                }

                //make this thing and all associated stuff go away
                buttonSelectAndRunInventory.Enabled = false;
                buttonSelectAndRunInventory.Text = "Working";

                using(SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Create(outputStream, SpreadsheetDocumentType.Workbook, false))
				{
                    foreach (DirectoryInfo topLevelDirectory in topLevelDirectories)
                    {
                        WriteDirectoryToNewWorksheet(topLevelDirectory, RunInventory(topLevelDirectory), spreadSheetDocument);
                    }
                }

                buttonSelectAndRunInventory.Text = "DONE! Files Inventoried:  " + fileCount;
                buttonSelectAndRunInventory.Enabled = true;
                buttonSelectAndRunInventory.Text = "Select Inventory Directory";
            }
            catch (ArgumentNullException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ParamName);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Failed to Inventory-" + ex.Message);
            }
        }
       
		#endregion

        /// <summary>
        /// Do the Inventory of a Top Level Directory
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
		private List<FileInfo> RunInventory(DirectoryInfo parent)
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
				toReturn.AddRange(RunInventory(directories[i]));
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
        private void WriteDirectoryToNewWorksheet(DirectoryInfo parent, List<FileInfo> files, SpreadsheetDocument spreadSheetDocument)
        {
            //Write Worksheet Header
            using (WorksheetWriter workSheetWriter = WorksheetWriter.Create(spreadSheetDocument, "Product Catalog Export"))
            {

            }
            //TODO: WRITE SHEET HEADER

            //Write Files to Worksheet
            foreach (FileInfo file in files)
            {
                //Columns: File Name, File-Type, Size(In MB, Folder1, Folder2, Folder3, Folder4, Folder5, Folder6, Folder7


                string[] path = file.DirectoryName.Split('\\');

                writer.Write("\n    <Cell><Data ss:Type=\"String\">" + Path.GetFileNameWithoutExtension(file.Name) + "</Data></Cell>");
                writer.Write("\n    <Cell><Data ss:Type=\"String\">" + Path.GetExtension(file.Name) + "</Data></Cell>");
                for (int i = 0; i < path.Length; i++)
                {
                    writer.Write("\n    <Cell><Data ss:Type=\"String\">" + path[i] + "\\" + "</Data></Cell>");
                }
                fileCount++;
            }
        }
    }
}
