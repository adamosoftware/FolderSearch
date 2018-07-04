using FolderSearch.Interfaces;
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
			var children = GetImmediateChildren(items, separator)
				.Select(dir =>
				{
					T node = new T()
					{
						Parent = parent,
						Name = dir.Key
					};
						
					if (IsFolder(dir))
					{
						node.Children = GetChildren<T>(node, dir, separator);
					}					

					return node;
				}).ToArray();

			return children as IEnumerable<INode>;
		}

		private static bool IsFolder(IGrouping<string, string> dir)
		{
			return (!(dir.Key.Equals(dir.First()) && dir.Count() == 1));
		}

		private static ILookup<string, string> GetImmediateChildren(IEnumerable<string> items, char separator)
		{
			return items
				.Select(item => item.Split(new char[] { separator }, 2))
				.ToLookup(
					parts => parts[0], // next level down directory name (or file name if we're at the end of path)
					parts => (parts.Length > 1) ? parts[1] : parts[0]); // anything after the directory name
		}
	}
}