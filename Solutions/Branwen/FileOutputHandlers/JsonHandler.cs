using Branwen.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Branwen.FileOutputHandlers
{
	public class JsonHandler
	{
		public static void WriteLocalInventoryToFile(SingleInventory inventory, string outputFile)
		{
			//Make sure the outputFile doesn't exist
			if (File.Exists(outputFile))
			{
				File.Delete(outputFile);
			}
			File.WriteAllText(outputFile, JsonConvert.SerializeObject(inventory));
		}

		public static IEnumerable<SingleInventory> ImportAllLocalInventoriesFromFiles(string path)
		{
			List<SingleInventory> inventories = new List<SingleInventory>();
			foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
			{
				if(file.Name.Contains($"_{FetchLocalInventoryGUI.GenericFileNameEnding}"))
				{
					string input = File.ReadAllText(file.FullName);
					SingleInventory singleLocalDriveInventory = JsonConvert.DeserializeObject<SingleInventory>(input);
					inventories.Add(singleLocalDriveInventory);
				}
			}
			return inventories;
		}
	}
}
