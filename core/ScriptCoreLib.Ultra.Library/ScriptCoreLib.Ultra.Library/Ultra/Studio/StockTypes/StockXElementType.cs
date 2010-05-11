using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
	public class StockXElementType : SolutionProjectLanguageType
	{
		public StockXElementType()
		{
			this.Name = "XElement";
			this.Namespace = "System.Xml.Linq";
		}
	}
}
