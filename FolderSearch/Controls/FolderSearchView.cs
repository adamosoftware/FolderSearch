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
		private List<FolderNode> _folderTrees = new List<FolderNode>();

		public FolderSearchView()
		{
			InitializeComponent();
		}

		public async Task LoadAsync(string path)
		{
			FolderNode tree = await FolderNode.FromPath(path);
			_folderTrees.Add(tree);
			FillTreeView(tree);
		}

		private void FillTreeView(FolderNode tree)
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

		private void FillTreeViewR(TreeNode parent, FolderNode tree)
		{
			foreach (FolderNode subfolder in tree.Subfolders)
			{
				TreeNode child = parent.Nodes.Add(subfolder.Name);
				FillTreeViewR(child, subfolder);
			}
		}
	}
}