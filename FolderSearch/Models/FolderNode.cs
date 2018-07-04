using FolderSearch.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FolderSearch.Models
{
	public class FolderNode : INode
	{		
		public INode Parent { get; set; }
		public string Name { get; set; }
		public FolderNode[] Subfolders { get; set; }

		public IEnumerable<INode> Children { get { return Subfolders; } set { Subfolders = value as FolderNode[]; } }

		[JsonIgnore]
		public List<Folder> Paths { get; private set; } = new List<Folder>();

		public async static Task<FolderNode> FromPath(string path)
		{
			FolderNode root = new FolderNode() { Name = path };
			root.Paths.Add(new Folder(path));

			await Task.Run(() =>
			{
				root.Subfolders = DrilldownSubfolders(root, path, root.Paths);
			});

			return root;
		}

		private static FolderNode[] DrilldownSubfolders(FolderNode parent, string path, List<Folder> paths)
		{
			string[] folders = TryGetDirectories(path);
			paths.AddRange(folders.Select(dir => new Folder(dir)));

			List<FolderNode> subfolders = new List<FolderNode>();
			foreach (string folder in folders)
			{
				FolderNode child = new FolderNode() { Name = folder.Split('\\').Last() };
				child.Subfolders = DrilldownSubfolders(child, folder, paths);				
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

		public FolderNode Search(string query)
		{
			string[] terms = query.Split(' ').Select(s => s.Trim()).ToArray();

			var results = Paths.FirstOrDefault(dir => IsMatch(terms, dir.Path));

			
			

			throw new NotImplementedException();
		}

		private bool IsMatch(string[] terms, string query)
		{
			return terms.All(t => query.ToLower().Contains(t.ToLower()));
		}
	}

	public class Folder : IPath
	{
		public Folder()
		{
		}

		public Folder(string path)
		{
			Path = path;
		}

		public string Path { get; set; }
	}
}