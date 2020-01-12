using System.Collections.Generic;

namespace Branwen.Models
{
	public class SingleInventory
	{
		public Dictionary<string, List<BranwenFileInfo>> TopLevelDirectoriesAndFiles { get; set; }
	}
}
