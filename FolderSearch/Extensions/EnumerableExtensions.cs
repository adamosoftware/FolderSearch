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

			//throw new NotImplementedException();

			return result;
		}

		private static IEnumerable<INode> GetChildren<T>(INode parent, IEnumerable<string> items, char separator) where T : INode, new()
		{
			Console.WriteLine("I'm not called...");
			throw new NotImplementedException();

			var parsed = GetFolders(items, separator);

			var folders = parsed
				.Select(dir =>
				{
					T node = new T()
					{
						Parent = parent,
						Name = dir.Key
					};
					node.Children = GetChildren<T>(node, dir, separator);
					return node;
				}).ToArray();

			foreach (INode item in folders) yield return item;
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