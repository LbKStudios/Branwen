using System.Collections.Generic;

namespace Branwen.Models
{
	public class SingleInventory
	{
		public Dictionary<string, IEnumerable<BranwenFileInfo>> TopLevelDirectoriesAndFiles { get; set; }
	}
}
