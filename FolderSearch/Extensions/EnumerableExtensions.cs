using FolderSearch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FolderSearch.Extensions
{
	public static class EnumerableExtensions
	{
		public static T ToHierarchy<T>(this IEnumerable<string> items, char separator, bool includeLeaves = true) where T : INode, new()
		{
			T result = new T();			
			result.Children = GetChildren<T>(result, items, separator, includeLeaves);
			return result;
		}

		private static IEnumerable<INode> GetChildren<T>(INode parent, IEnumerable<string> items, char separator, bool includeLeaves) where T : INode, new()
		{			
			var children = GetImmediateChildren(items, separator, includeLeaves)
				.Select(dir =>
				{
					T node = new T()
					{
						Parent = parent,
						Name = dir.Key
					};
						
					if (IsFolder(dir))
					{
						node.Children = GetChildren<T>(node, dir, separator, includeLeaves);
					}					

					return node;
				}).ToArray();

			return children as IEnumerable<INode>;
		}

		private static bool IsFolder(IGrouping<string, string> dir)
		{
			return (!(dir.Key.Equals(dir.First()) && dir.Count() == 1));
		}

		private static ILookup<string, string> GetImmediateChildren(IEnumerable<string> items, char separator, bool includeLeaves)
		{
			Func<string[], bool> predicate = (array) => true;

			// if excluding files (leaves), then we must filter the item array to things that have a separator (meaning they are a folder)
			if (!includeLeaves) predicate = (array) => array.Length > 1;

			return items
				.Select(item => item.Split(new char[] { separator }, 2))
				.Where(array => predicate.Invoke(array))
				.ToLookup(
					parts => parts[0], // next level down directory name (or file name if we're at the end of path)
					parts => (parts.Length > 1) ? parts[1] : parts[0]); // anything after the directory name
		}
	}
}