using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib;
namespace jsc.meta.Library.Templates.JavaScript
{
	[Script(InternalConstructor = true)]
	public class NamedImage : global::ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage
	{
		internal const string IHTMLImage_src = "IHTMLImage.src";

		public NamedImage()
		{

		}

		public static NamedImage InternalConstructor()
		{
			return (NamedImage)new IHTMLImage { src = IHTMLImage_src };
		}
	}
}
