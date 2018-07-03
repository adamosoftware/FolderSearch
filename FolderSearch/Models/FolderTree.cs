using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FolderSearch.Models
{
	public class FolderTree
	{
		public string Name { get; set; }
		public FolderTree[] Subfolders { get; set; }

		public async static Task<FolderTree> FromPathAsync(string path)
		{
			FolderTree root = new FolderTree() { Name = path };

			await Task.Run(() =>
			{
				root.Subfolders = DrilldownSubfolders(root, path);
			});

			return root;
		}

		private static FolderTree[] DrilldownSubfolders(FolderTree parent, string path)
		{
			string[] folders = TryGetDirectories(path);

			List<FolderTree> subfolders = new List<FolderTree>();
			foreach (string folder in folders)
			{
				FolderTree child = new FolderTree() { Name = folder.Split('\\').Last() };
				child.Subfolders = DrilldownSubfolders(child, folder);
				subfolders.Add(child);
			}

			return subfolders.ToArray();
		}

		private static string[] TryGetDirectories(string path)
		{
			try
			{
				return Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
			}
			catch (Exception)
			{
				return Enumerable.Empty<string>().ToArray();
			}
		}

		public async Task<FolderTree> SearchAsync(string query)
		{
			string[] terms = query.Split(' ').Select(s => s.Trim()).ToArray();

			throw new NotImplementedException();
		}
	}
}