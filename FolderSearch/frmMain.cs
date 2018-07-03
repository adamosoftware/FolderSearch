using FolderSearch.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderSearch
{
	public partial class frmMain : Form
	{
		private Options _options = null;

		public frmMain()
		{
			InitializeComponent();
		}

		private async void frmMain_Load(object sender, EventArgs e)
		{
			_options = Options.FromFile(OptionsFilename());

			foreach (string path in _options.RootFolders)
			{
				tslStatus.Text = $"Loading {path}...";
				await folderSearchView1.LoadAsync(path);				
			}
			tslStatus.Text = "Ready";
		}

		private string OptionsFilename()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FolderSearch.json");
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			_options.Save(OptionsFilename());
		}
	}
}