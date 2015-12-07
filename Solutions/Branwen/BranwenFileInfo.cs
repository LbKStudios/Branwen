using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branwen
{
	public class BranwenFileInfo
	{
		public string Name
		{
			get;
			set;
		}

		public string Extension
		{
			get;
			set;
		}

		public long FileSize
		{
			get;
			set;
		}

		public List<string> Path
		{
			get;
			set;
		}
	}
}
