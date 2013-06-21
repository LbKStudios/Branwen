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
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

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
		SpreadsheetDocument spreadSheet;
		WorksheetPart worksheetPart;
		Worksheet workSheet;
		SheetData sheetData;
		Sheet currentSheet;
		Row currentRow;
		Cell cellA;
		Cell cellB;
		Cell currentCell;
		string[] cells = new string[] { "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

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
				textBoxFileName.Enabled = true;
				textBoxFileName.Visible = true;
				buttonSelectFileDirectory.Enabled = true;
				buttonSelectFileDirectory.Visible = true;
				buttonSelectInventoryDirectory.Enabled = false;
				buttonSelectInventoryDirectory.Visible = false;
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
				spreadSheet = SpreadsheetDocument.Create(folderDialog.SelectedPath + "\\" + textBoxFileName.Text + ".xlsx", SpreadsheetDocumentType.Workbook);
				WorkbookPart workbookpart = spreadSheet.AddWorkbookPart();
				workbookpart.Workbook = new Workbook();
				worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
				worksheetPart.Worksheet = new Worksheet(new SheetData());
				Sheets sheets = spreadSheet.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
				currentSheet = new Sheet()
					{
						Id = spreadSheet.WorkbookPart.GetIdOfPart(worksheetPart),
						SheetId = 1,
						Name = "Your Run"
					};
				sheets.Append(currentSheet);
				workbookpart.Workbook.Save();
				workSheet = worksheetPart.Worksheet;
				sheetData = new SheetData();

				writer = new StreamWriter(folderDialog.SelectedPath + "\\" + textBoxFileName.Text + ".csv");
				buttonStartInventory.Enabled = true;
				buttonStartInventory.Visible = true;
				buttonSelectFileDirectory.Enabled = false;
				buttonSelectFileDirectory.Visible = false;
				textBoxFileName.Enabled = false;
				textBoxFileName.Visible = false;
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
				buttonSelectFileDirectory.Enabled = false;
				buttonSelectFileDirectory.Visible = false;
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
				}
				textboxOutput.AppendText("\n");
				textboxOutput.AppendText("Done. " + count + " Items Processed");
				buttonSelectInventoryDirectory.Enabled = true;
				buttonSelectInventoryDirectory.Visible = true;
				buttonStartInventory.Enabled = true;
				buttonStartInventory.Visible = false;
				writer.Flush();
				writer.Close();
				workSheet.Append(sheetData);
				workSheet.Save();
				worksheetPart.Worksheet = workSheet;
				spreadSheet.Close();
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
			string [] path = file.DirectoryName.Split('\\');
			currentRow = new Row();
			cellA = new Cell() { CellReference = "A" + count, DataType = CellValues.String, CellValue = new CellValue(Path.GetFileNameWithoutExtension(file.Name)) };
			cellB = new Cell() { CellReference = "B" + count, DataType = CellValues.String, CellValue = new CellValue(Path.GetExtension(file.Name)) };
			currentRow.Append(cellA);
			currentRow.Append(cellB);
			
			for(int i = 0; i < path.Length; i++)
			{
				currentCell = new Cell() { CellReference = cells[i] + count, DataType = CellValues.String, CellValue = new CellValue(path[i]) };
				currentRow.Append(currentCell);
				writer.Write("," + path[i] + "\\");
			}
			sheetData.Append(currentRow);
			workSheet.Save();
			writer.WriteLine("");
			textboxOutput.AppendText(file.FullName + "\n");
			writer.Flush();

			count++;
		}
    }
}
