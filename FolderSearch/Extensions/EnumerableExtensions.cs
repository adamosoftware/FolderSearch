using FolderSearch.Interfaces;
using FolderSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FolderSearch.Extensions
{
	public static class EnumerableExtensions
	{
		public static T ToHierarchy<T>(this IEnumerable<string> items, char separator) where T : INode, new()
		{
			T result = new T();
			result.Children = GetChildren<T>(result, items, separator);
			return result;
		}

		private static IEnumerable<INode> GetChildren<T>(INode parent, IEnumerable<string> items, char separator) where T : INode, new()
		{
			var folders = GetFolders(items, separator);

			var result = folders
				.Select(dir => new T()
				{
					Parent = parent,
					Name = dir.Key,
					Children = GetChildren<T>(parent, dir, separator)
				}).ToArray();			

			return result as IEnumerable<INode>;
		}

		public static ILookup<string, string> GetFolders(IEnumerable<string> items, char separator) 
		{
			return items
				.Select(item => item.Split(new char[] { separator }, 2))
				.Where(parts => parts.Length >= 2)
				.ToLookup(parts => parts[0], parts => parts[1]);
		}
	}
}