using FolderSearch.Extensions;
using FolderSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
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
			Console.ReadLine();
		}

		private static void PrintOutput(FolderNode result, int depth)
		{
			string indent = string.Empty;
			for (int i = 0; i < depth; i++) indent += " ";
			Console.WriteLine(indent + result.Name);
			depth++;
			if (result.Children != null)
			{
				foreach (FolderNode child in result.Children) PrintOutput(child, depth);
			}
			depth--;
		}

	}
}
