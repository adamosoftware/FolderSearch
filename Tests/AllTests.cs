using System;
using System.Diagnostics;
using FolderSearch.Extensions;
using FolderSearch.Interfaces;
using FolderSearch.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
	[TestClass]
	public class AllTests
	{
		[TestMethod]
		public void FilenamesToHierarchy()
		{
			string[] fileNames = new string[]
			{
				@"this\that\hello.txt",
				@"this\yellow\whatever.txt",
				@"this\yellow\crispin.txt",
				@"that\brown\thing.txt",
				@"that\brown\crescent.txt"
			};

			var result = EnumerableExtensions.ToHierarchy<FolderNode>(fileNames, '\\');

			PrintOutput(result, 0);
		}

		private void PrintOutput(FolderNode result, int depth)
		{
			string indent = string.Empty;
			for (int i = 0; i < depth; i++) indent += " ";
			Debug.WriteLine(indent + result.Name);
			depth++;
			foreach (FolderNode child in result.Children) PrintOutput(child, depth);
			depth--;
		}
	}
}
