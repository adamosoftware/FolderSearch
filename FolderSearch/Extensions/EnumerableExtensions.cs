using FolderSearch.Interfaces;
using FolderSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FolderSearch.Extensions
{
	public static class EnumerableExtensions
	{
		public static H ToHierarchy<H>(this IEnumerable<string> items, char separator) where H : IHierarchy, new()
		{
			H result = new H();
			result.Children = GetChildren<IHierarchy>(items, separator);
			return result;
		}

		private static IEnumerable<H> GetChildren<H>(IEnumerable<string> items, char separator) where H : IHierarchy, new()
		{
			var folders = GetFolders(items, separator);

			return folders
				.Select(dir => new H()
				{
					Name = dir.Key,
					Children = GetChildren<H>(dir, separator)
				});
		}

		public static ILookup<string, string> GetFolders(IEnumerable<string> items, char separator) 
		{
			return items
				.Select(item => item.Split(new char[] { separator }, 2))
				.ToLookup(parts => parts[0], parts => parts[1]);
		}
	}
}