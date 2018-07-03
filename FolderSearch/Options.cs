using Newtonsoft.Json;
using System;
using System.IO;

namespace FolderSearch
{
	public class Options
	{
		public static Options FromFile(string fileName)
		{
			if (!File.Exists(fileName)) return new Options();

			using (StreamReader reader = File.OpenText(fileName))
			{
				string json = reader.ReadToEnd();
				return JsonConvert.DeserializeObject<Options>(json);
			}
		}

		public void Save(string fileName)
		{
			using (StreamWriter writer = File.CreateText(fileName))
			{
				string json = JsonConvert.SerializeObject(this);
				writer.Write(json);
			}
		}

		public string[] RootFolders { get; set; } = new string[]
		{
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		};
	}
}