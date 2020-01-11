using Branwen.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;

namespace Branwen.FileOutputHandlers
{
	public class SpreadsheetHandler
	{
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
				cell.CellValue = new CellValue(file.Extension);
				cell.DataType = CellValues.String;
				row.Append(cell);

				//File Size
				cell = new Cell();
				cell.CellValue = new CellValue((file.FileSize / 1048576).ToString());   //Magic Number makes it MB
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
	}
}
