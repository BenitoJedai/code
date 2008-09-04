using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmbeddedResourcesBrowser.Shared
{
	public class Assets
	{
		public readonly static Assets Default = new Assets();
 
		public string[] FileNames
		{
			get
			{
				return new []  { "data3.txt" };
			}
		}
	}
}
