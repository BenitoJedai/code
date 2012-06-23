using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using TestNewGuid.HTML.Pages;
using System.Linq;
using ScriptCoreLib.Shared.Lambda;

namespace TestNewGuid
{

	[Description("TestNewGuid. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IAbout a)
		{
			AddNewGuid(a);

			//for (int i = 0; i < 8; i++)
			//{
			//    AddNewGuid(a);

			//}
		}

		private static void AddNewGuid(IAbout a)
		{
			var guid = Guid.NewGuid();
			var w = guid.ToString();

			new IHTMLPre { innerText = w }.AttachTo(a.Content);
		}

	}


}
