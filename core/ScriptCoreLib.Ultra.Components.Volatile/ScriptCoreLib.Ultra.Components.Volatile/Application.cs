using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using ScriptCoreLib.Ultra.Components.Volatile.HTML.Pages;
using System.Linq;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Components.Volatile
{

	[Description("ScriptCoreLib.Ultra.Components.Volatile. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IAbout a)
		{
			Native.document.title = "ScriptCoreLib.Ultra.Components.Volatile";



			
		}

	}


}
