using System;
using System.Diagnostics;
using System.Text;
using FolderSearch.Extensions;
using FolderSearch.Interfaces;
using FolderSearch.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
	[TestClass]
	public class AllTests
	{
		private static string[] GetFilenames()
		{
			return new string[]
			{
				@"this\that\hello.txt",
				@"this\yellow\whatever.txt",
				@"this\yellow\crispin.txt",
				@"that\brown\thing.txt",
				@"that\brown\crescent.txt"
			};
		}

		[TestMethod]
		public void FilenamesToHierarchy()
		{
			var result = EnumerableExtensions.ToHierarchy<FolderNode>(GetFilenames(), '\\');

			StringBuilder printed = new StringBuilder();
			PrintResult(result, printed, 0);

			Assert.IsTrue(printed.ToString().Equals(
@"
  this
    that
      hello.txt
    yellow
      whatever.txt
      crispin.txt
  that
    brown
      thing.txt
      crescent.txt
"));
		}

		[TestMethod]
		public void FilenamesToFolderTree()
		{
			var result = EnumerableExtensions.ToHierarchy<FolderNode>(GetFilenames(), '\\', includeLeaves: false);

			StringBuilder printed = new StringBuilder();
			PrintResult(result, printed, 0);

			Assert.IsTrue(printed.ToString().Equals(
@"
  this
    that
    yellow
  that
    brown
"));
		}

		private void PrintResult(FolderNode result, StringBuilder printed, int depth)
		{
			string indent = string.Empty;
			for (int i = 0; i < depth; i++) indent += "  ";
			printed.AppendLine(indent + result.Name);
			depth++;
			if (result.Children != null)
			{
				foreach (FolderNode child in result.Children) PrintResult(child, printed, depth);
			}
			depth--;
		}
	}
}
