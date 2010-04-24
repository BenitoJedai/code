using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using jsc.meta.Library;
using System.Reflection.Emit;
using System.Reflection;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	partial class ReferenceUltraSource
	{
		protected bool DirectoryNeedsConversion(string Directory)
		{
			if (this.SelectAll)
				return true;

			Directory = Directory.Replace("\\", "/");

			if (Directory.Contains("." + UltraSource + "/"))
				return true;

			if (Directory == WebSource_HTML || Directory.EndsWith("." + WebSource_HTML) || Directory.EndsWith("/" + WebSource_HTML))
				return true;

			if (Directory == UltraSource || Directory.EndsWith("." + UltraSource) || Directory.EndsWith("/" + UltraSource))
				return true;

			return false;
		}



	}
}
