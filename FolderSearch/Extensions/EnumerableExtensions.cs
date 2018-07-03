using FolderSearch.Interfaces;
using FolderSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FolderSearch.Extensions
{
	public static class EnumerableExtensions
	{
		public static H ToHierarchy<H>(this IEnumerable<string> fileNames) where H : IHierarchy, new()
		{
			IEnumerable<Folder> folders = fileNames.Select(i => new Folder(i));
			return ToHierarchy<H, Folder>(folders, '\\');
		}

		public static H ToHierarchy<H, P>(this IEnumerable<P> items, char separator) where H : IHierarchy, new() where P : IPath, new()
		{
			H result = new H();

			throw new NotImplementedException();
			//result.Children = GetChildren<H>(items, 0);			

			return result;
		}

		private static IEnumerable<IHierarchy> GetChildren<H, P>(IEnumerable<P> items, int depth, char separator) 
			where H : IHierarchy, new() 
			where P : IPath, new()
		{
			ILookup<string, string> groups = GetFolders(items, separator);

			throw new NotImplementedException();

			/*
			return pathParts
				.Where(parts => parts.Length >= depth)
				.Select(parts => parts[depth])
				.GroupBy(folder => folder)
				.Select(grp => new H() { Children = GetChildren<H>(grp.Select(path => path.Split(separator)), depth + 1) });
				*/
		}

		public static ILookup<string, string> GetFolders(IEnumerable<string> items, char separator)
		{
			var folders = items.Select(s => new Folder(s));
			return GetFolders(folders, separator);
		}

		public static ILookup<string, string> GetFolders<P>(IEnumerable<P> items, char separator) where P : IPath, new()
		{
			return items
				.Select(item => item.Path.Split(new char[] { separator }, 2))
				.ToLookup(parts => parts[0], parts => parts[1]);
		}
	}
}