using Branwen.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;

namespace Branwen.FileOutputHandlers
{
	public class SpreadsheetHandler
	{
		public static int WriteInventoryToSpreadsheet(string outputFile, SingleInventory inventory)
		{
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
			int itemcounter = 0;
			int fileCount = 0;
			foreach (var item in inventory.TopLevelDirectoriesAndFiles)
			{
				WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
				worksheetPart.Worksheet = new Worksheet(new SheetData());
				SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
				Sheet sheet = new Sheet();
				sheet.Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart);
				sheet.Name = item.Key;
				sheet.SheetId = (UInt32)(itemcounter + 1);
				sheets.Append(sheet);
				fileCount += SpreadsheetHandler.WriteDirectoryToWorksheet(item.Value, sheetData);
				itemcounter++;
			}
			spreadsheetDocument.Close();
			return fileCount;
		}

		/// <summary>
		/// Writes all files in the Directory to the Workbook
		/// </summary>
		/// <param name="files"></param>
		/// <param name="sheetData"></param>
		public static int WriteDirectoryToWorksheet(IEnumerable<BranwenFileInfo> files, SheetData sheetData)
		{
			int fileCount = 0;
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
				cell.CellValue = new CellValue(file.FileType);
				cell.DataType = CellValues.String;
				row.Append(cell);

				//File Size
				cell = new Cell();
				cell.CellValue = new CellValue((file.Size / 1048576).ToString());   //Magic Number makes it MB
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
			return fileCount;
		}

		/// <summary>
		/// Write the standard header to the Worksheet
		/// </summary>
		/// <param name="sheetdata"></param>
		public static void WriteWorksheetHeader(SheetData sheetData)
		{
			Row row = new Row();
			List<string> headers = new List<string> { "File Name", "File Type", "Size(In MB)", "DriveName", "Folder1", "Folder2", "Folder3", "Folder4", "Folder5", "Folder6", "Folder7" };
			foreach (string header in headers)
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
