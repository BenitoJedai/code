using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
	public class StockSpriteType : SolutionProjectLanguageType
	{
		public StockSpriteType(string Namespace, string Name)
		{
			this.Namespace = Namespace;
			this.Name = Name;

			this.BaseType = new KnownStockTypes.ScriptCoreLib.ActionScript.flash.display.Sprite();

			this.IsSealed = true;
			
			this.Methods.Add(GetDefaultConstructorDefinition());
		}

	
	}
}
