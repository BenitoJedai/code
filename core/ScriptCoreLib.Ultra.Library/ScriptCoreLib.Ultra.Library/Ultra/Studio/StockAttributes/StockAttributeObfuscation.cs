using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.StockAttributes
{
	public class StockAttributeObfuscation : SolutionProjectLanguageAttribute
	{
		public StockAttributeObfuscation()
		{
			Type = new SolutionProjectLanguageType
			{
				Namespace = "System",
				Name = "Obfuscation"
			};

			var set_Feature = new SolutionProjectLanguageMethod
			{
				IsProperty = true,
				Name = "set_Feature",
				DeclaringType = Type
			};
			var merge = new PseudoStringConstantExpression
			{
				Value = "merge"
			};

			var p = new Dictionary<SolutionProjectLanguageMethod, object>
			{
				{ set_Feature, merge}
			};

			this.Properties = p;
		}
	}
}
