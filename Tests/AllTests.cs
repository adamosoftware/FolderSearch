using System;
using FolderSearch.Extensions;
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

			var result = EnumerableExtensions.GetFolders(fileNames, '\\');
		}
	}
}
