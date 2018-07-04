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
