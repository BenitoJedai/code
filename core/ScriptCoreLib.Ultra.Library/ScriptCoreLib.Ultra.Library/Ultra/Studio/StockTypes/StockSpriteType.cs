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

			this.BaseType = new SolutionProjectLanguageType
			{
				Namespace = "ScriptCoreLib.ActionScript.flash.display",
				Name = "Sprite"
			};

			this.IsSealed = true;

			var ctor = new SolutionProjectLanguageMethod
			{
				DeclaringType = this,
				Name = SolutionProjectLanguageMethod.ConstructorName,
				Code = new SolutionProjectLanguageCode
				{
				}
			};

			this.Methods.Add(ctor);
		}

	
	}
}
