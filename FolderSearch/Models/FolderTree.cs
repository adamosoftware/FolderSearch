using FolderSearch.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FolderSearch.Models
{
	public class FolderTree : IHierarchy
	{
		public string Name { get; set; }
		public FolderTree[] Subfolders { get; set; }

		public IEnumerable<IHierarchy> Children { get { return Subfolders; } set { Subfolders = value as FolderTree[]; } }

		[JsonIgnore]
		public List<Folder> Paths { get; private set; } = new List<Folder>();

		public async static Task<FolderTree> FromPath(string path)
		{
			FolderTree root = new FolderTree() { Name = path };
			root.Paths.Add(new Folder(path));

			await Task.Run(() =>
			{
				root.Subfolders = DrilldownSubfolders(root, path, root.Paths);
			});

			return root;
		}

		private static FolderTree[] DrilldownSubfolders(FolderTree parent, string path, List<Folder> paths)
		{
			string[] folders = TryGetDirectories(path);
			paths.AddRange(folders.Select(dir => new Folder(dir)));

			List<FolderTree> subfolders = new List<FolderTree>();
			foreach (string folder in folders)
			{
				FolderTree child = new FolderTree() { Name = folder.Split('\\').Last() };
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

		public FolderTree Search(string query)
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