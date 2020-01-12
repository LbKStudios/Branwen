using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Branwen
{
	public partial class MainGUI : Form
	{
		public MainGUI()
		{
			InitializeComponent();
		}

		private void FetchLocalInventoryButton_Click(object sender, EventArgs e)
		{
			Hide();
			FetchLocalInventoryGUI fetchLocalInventoryGUI = new FetchLocalInventoryGUI();
			fetchLocalInventoryGUI.ShowDialog();
		}

		private void BuildCompleteInventoryButton_Click(object sender, EventArgs e)
		{
			Hide();
			BuildCompleteInventoryGUI buildCompleteInventoryGUI = new BuildCompleteInventoryGUI();
			buildCompleteInventoryGUI.ShowDialog();
		}

		private void HelpButton_Click(object sender, EventArgs e)
		{
			ProcessStartInfo sInfo = new ProcessStartInfo("https://github.com/LbKStudios/Branwen/wiki/How-To-Use/");
			Process.Start(sInfo);
		}
	}
}
