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

namespace Disc_Inventory_Program
{
    public partial class GUI : Form
    {
        DirectoryInfo [] bigDirectories;
		DirectoryInfo [] directories;
		FileInfo [] files;
        Stream outputStream;
//ExcelXmlWorkbook workBook;
//Worksheet sheet;
        int itemCount = 0;
		
        public GUI()
        {
            InitializeComponent();
        }

		#region buttons

        private void buttonSelectSaveDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                folderDialog.Description = "Select Destination Folder for Inventory file";
                folderDialog.ShowDialog();
//workBook = new ExcelXmlWorkbook();
//workBook.Properties.Author = Environment.UserName;
                outputStream = File.OpenWrite(folderDialog.SelectedPath + "\\mediadriveInventory.xml");

                //make this thing and all associated stuff go away
                buttonSelectSaveDirectory.Enabled = false;

                //next phase appear
                buttonSelectInventoryDirectory.Enabled = true;

            }
            catch (ArgumentNullException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ParamName);
            }
            catch (IOException ex)
            {
                System.Windows.Forms.MessageBox.Show("Could not make file-" + ex.Message);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Asking for directory failed" + ex.Message);
            }
        }

        private void buttonSelectInventoryDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                folderDialog.Description = "Set Folder to Inventory";
                folderDialog.ShowDialog();
                bigDirectories = new DirectoryInfo(folderDialog.SelectedPath).GetDirectories();

                //make this thing and all associated stuff go away
                buttonSelectInventoryDirectory.Enabled = false;

                //next phase appear
                buttonStartInventory.Enabled = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Asking for directory failed" + ex.Message);
            }
        }

        private void buttonStartInventory_Click(object sender, EventArgs e)
        {
            try
            {
                //make this thing and all associated stuff go away
                buttonStartInventory.Enabled = false;
                buttonSelectSaveDirectory.Text = "Working";
                buttonSelectInventoryDirectory.Text = "Working";
                buttonStartInventory.Text = "Working";
                using(StreamWriter writer = new StreamWriter(outputStream))
                {
                    bool firstSheet = true;
                    writer.Write(writeBeginningCrap());
                    foreach (DirectoryInfo bigDir in bigDirectories)
                    {
                        String dirName= 30 >= bigDir.Name.Length ? bigDir.Name : bigDir.Name.Substring(0, 30);

                        writer.Write("\n <Worksheet ss:Name=\"" + dirName + "\">");
                        
                        writeInventory(Inventory(bigDir), writer);
                        
                        writer.Write("\n  </Table>");
                        writer.Write("\n  <WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
                        writer.Write("\n   <PageSetup>");
                        writer.Write("\n    <Header x:Margin=\"0.3\"/>");
                        writer.Write("\n    <Footer x:Margin=\"0.3\"/>");
                        writer.Write("\n    <PageMargins x:Bottom=\"0.75\" x:Left=\"0.7\" x:Right=\"0.7\" x:Top=\"0.75\"/>");
                        writer.Write("\n   </PageSetup>");
                        writer.Write("\n   <Unsynced/>");
                        writer.Write("\n   <Print>");
                        writer.Write("\n    <ValidPrinterInfo/>");
                        writer.Write("\n    <VerticalResolution>0</VerticalResolution>");
                        writer.Write("\n   </Print>");
                        if (firstSheet)
                        {
                            writer.Write("\n   <Selected/>");
                            firstSheet = false;
                        }
                        writer.Write("\n   <ProtectObjects>False</ProtectObjects>");
                        writer.Write("\n   <ProtectScenarios>False</ProtectScenarios>");
                        writer.Write("\n  </WorksheetOptions>");
                        writer.Write("\n </Worksheet>");
                    }
                    writer.Write("\n</Workbook>");
                }
//workBook.Export(writer);
                buttonSelectSaveDirectory.Text = "DONE! itemCount:  " + itemCount;
                buttonSelectInventoryDirectory.Text = "DONE! itemCount:  " + itemCount;
                buttonStartInventory.Text = "DONE! itemCount:  " + itemCount;
                //Environment.Exit(0);
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

        private void writeInventory(List<FileInfo> files, StreamWriter writer)
        {
            writer.Write("\n  <Table ss:ExpandedColumnCount=\"20\" ss:ExpandedRowCount=\"" + (files.Count + 1) + "\" x:FullColumns=\"1\"");
            writer.Write("\n   x:FullRows=\"1\" ss:DefaultRowHeight=\"15\" ss:FullColumns=\"1\" ss:FullRows=\"1\">");
            writer.Write(writeSheetHeader());
            foreach (FileInfo file in files)
            {
                writer.Write("\n   <Row ss:AutoFitHeight=\"0\">");
                string[] path = file.DirectoryName.Split('\\');

                writer.Write("\n    <Cell><Data ss:Type=\"String\">" + Path.GetFileNameWithoutExtension(file.Name) + "</Data></Cell>");
                writer.Write("\n    <Cell><Data ss:Type=\"String\">" + Path.GetExtension(file.Name) + "</Data></Cell>");
                for (int i = 0; i < path.Length; i++)
                {
                    writer.Write("\n    <Cell><Data ss:Type=\"String\">" + path[i] + "\\" + "</Data></Cell>");
                }
                itemCount++;
                writer.Write("\n   </Row>");
            }
        }

        private String writeBeginningCrap()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<?xml version=\"1.0\"?>");
            builder.Append("\n<?mso-application progid=\"Excel.Sheet\"?>");
            builder.Append("\n<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            builder.Append("\n xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            builder.Append("\n xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            builder.Append("\n xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            builder.Append("\n xmlns:html=\"http://www.w3.org/TR/REC-html40\">");
            builder.Append("\n <DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            builder.Append("\n  <Author>" + Environment.UserName + "</Author>");
            builder.Append("\n  <Version>12.00</Version>");
            builder.Append("\n </DocumentProperties>");
            builder.Append("\n <ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            builder.Append("\n  <WindowHeight>10005</WindowHeight>");
            builder.Append("\n  <WindowWidth>10005</WindowWidth>");
            builder.Append("\n  <WindowTopX>120</WindowTopX>");
            builder.Append("\n  <WindowTopY>135</WindowTopY>");
            builder.Append("\n  <ProtectStructure>False</ProtectStructure>");
            builder.Append("\n  <ProtectWindows>False</ProtectWindows>");
            builder.Append("\n </ExcelWorkbook>");
            builder.Append("\n <Styles>");
            builder.Append("\n  <Style ss:ID=\"Default\" ss:Name=\"Normal\">");
            builder.Append("\n   <Alignment ss:Vertical=\"Bottom\"/>");
            builder.Append("\n   <Borders/>");
            builder.Append("\n   <Font ss:FontName=\"Calibri\" x:Family=\"Swiss\" ss:Size=\"11\" ss:Color=\"#000000\"/>");
            builder.Append("\n   <Interior/>");
            builder.Append("\n   <NumberFormat/>");
            builder.Append("\n   <Protection/>");
            builder.Append("\n  </Style>");
            builder.Append("\n </Styles>");
            return builder.ToString();
        }

        private String writeSheetHeader()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\n   <Row ss:AutoFitHeight=\"0\">");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "File Name" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "File-Type" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Drive" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder1" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder2" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder3" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder4" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder5" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder6" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder7" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder8" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder9" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder10" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder11" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder12" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder13" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder14" + "</Data></Cell>");
            builder.Append("\n    <Cell><Data ss:Type=\"String\">" + "Folder15" + "</Data></Cell>");
            builder.Append("\n   </Row>");
            return builder.ToString();
        }
    }
}
