using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace UltraWebApplication
{
	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			new IHTMLDiv { innerHTML = "Hello world" }.AttachToDocument();
		}
	}

}