using FolderSearch.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderSearch.Controls
{
	public partial class FolderSearchView : UserControl
	{
		private List<FolderTree> _folderTrees = new List<FolderTree>();

		public FolderSearchView()
		{
			InitializeComponent();
		}

		public async Task LoadAsync(string path)
		{
			FolderTree tree = await FolderTree.FromPathAsync(path);
			_folderTrees.Add(tree);
			FillTreeView(tree);
		}

		private void FillTreeView(FolderTree tree)
		{
			try
			{
				tvwFolders.BeginUpdate();
				TreeNode root = tvwFolders.Nodes.Add(tree.Name);
				FillTreeViewR(root, tree);
			}
			finally
			{
				tvwFolders.EndUpdate();
			}
		}

		private void FillTreeViewR(TreeNode parent, FolderTree tree)
		{
			foreach (FolderTree subfolder in tree.Subfolders)
			{
				TreeNode child = parent.Nodes.Add(subfolder.Name);
				FillTreeViewR(child, subfolder);
			}
		}
	}
}