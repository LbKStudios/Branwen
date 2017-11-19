using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Branwen
{
	public partial class DBConfig : Form
	{
		private MySqlConnection mySqlConnection;

		private GUI gui;

		public DBConfig(GUI gui)
		{
			this.gui = gui;
			InitializeComponent();
		}

		/// <summary>
		/// Tests DB credentials for connection
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TestConnectionButton_Click(object sender, System.EventArgs e)
		{
			CloseButton.Enabled = false;
			TestConnectionButton.Enabled = false;
			ServerTextBox.Enabled = false;
			DatabaseTextBox.Enabled = false;
			UserTextBox.Enabled = false;
			PasswordTextBox.Enabled = false;
			try
			{
				mySqlConnection = new MySqlConnection($"server={ServerTextBox.Text};user={UserTextBox.Text};password={PasswordTextBox.Text};database={DatabaseTextBox.Text};");
				mySqlConnection.Open();
				mySqlConnection.Close();
				WipeDBButton.Enabled = true;
			}
			catch (Exception ex)
			{
				WipeDBButton.Enabled = false;
				MessageBox.Show("Failed to Connect to DB. Please recheck values:" + Environment.NewLine + ex.Message);
			}
			CloseButton.Enabled = true;
			TestConnectionButton.Enabled = true;
			ServerTextBox.Enabled = true;
			DatabaseTextBox.Enabled = true;
			UserTextBox.Enabled = true;
			PasswordTextBox.Enabled = true;
		}

		/// <summary>
		/// Truncates the Database to clear all items
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WipeDBButton_Click(object sender, System.EventArgs e)
		{
			TestConnectionButton.Enabled = false;
			WipeDBButton.Enabled = false;
			CloseButton.Enabled = false;
			try
			{
				mySqlConnection.Open();

				string deleteStatement = "TRUNCATE TABLE FilePaths; TRUNCATE TABLE Files;";
				MySqlCommand deleteCommand = new MySqlCommand(deleteStatement, mySqlConnection);
				deleteCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to Wipe:" + Environment.NewLine + ex.Message);
			}
			TestConnectionButton.Enabled = true;
			WipeDBButton.Enabled = true;
			CloseButton.Enabled = true;
		}

		/// <summary>
		/// Closes UI element
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			if(mySqlConnection == null)
			{
				gui.UseDBCheckBox.Checked = false;
			}
			else
			{
				gui.mySqlConnection = mySqlConnection;
				gui.ExportFileCheckBox.Enabled = true;
				gui.DriveNameTextBox.Enabled = true;
			}
			gui.UseDBCheckBox.Enabled = true;
			gui.SelectAndRunInventoryButton.Enabled = true;
			WipeDBButton.Enabled = false;
			CloseButton.Enabled = false;
			Hide();
		}
	}
}
