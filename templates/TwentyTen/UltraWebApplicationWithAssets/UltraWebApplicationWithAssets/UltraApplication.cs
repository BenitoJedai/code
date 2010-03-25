using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using java.applet;
using java.awt;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Ultra.Library.Delegates;
using UltraWebApplicationWithAssets.HTML.Pages.FromAssets;

namespace UltraWebApplicationWithAssets
{
	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			var a = new HTMLPage1();


			a.Button1.innerText = "Add";

			a.Button2.disabled = true;

			a.Button1.onclick +=
				delegate
				{
					a.Content.Add(new IHTMLDiv("" + DateTime.Now));
				};

			a.Container.AttachToDocument();
		}

	}




}