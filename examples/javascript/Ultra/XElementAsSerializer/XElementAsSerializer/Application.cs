using System;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using XElementAsSerializer.HTML.Audio.FromAssets;
using System.ComponentModel;
using XElementAsSerializer.HTML.Pages;
using System.Linq;
using ScriptCoreLib.Shared.Lambda;
using XElementAsSerializer.Data;
using System.Xml.Linq;

namespace XElementAsSerializer
{

	[Description("XElementAsSerializer. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IAbout a)
		{
			var v = new Class1 { Foo = "Hello", Bar = "World" };

			a.Content.Add(
				new IHTMLTextArea { innerText = ((XElement)v).ToString() }
			);

			new UltraWebService().Swap(v,
				v_ =>
				{
					v_.Bar += ".";
					v_.Foo += "!";

					a.Content.Add(
						new IHTMLTextArea { innerText = ((XElement)v_).ToString() }
					);
				}
			);
		}
	}


}
