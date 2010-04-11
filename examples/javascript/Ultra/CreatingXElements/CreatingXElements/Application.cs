using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using CreatingXElements.HTML.Pages;
using System.Linq;
using ScriptCoreLib.Shared.Lambda;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using System.Collections.Generic;

namespace CreatingXElements
{

	[Description("CreatingXElements. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IAboutJSC a)
		{
			Native.Document.title = "CreatingXElements";

			var doc = DocumentBuilder.Create();

			a.XMLSource.value = doc.ToString();


			var t = new TreeNode(VistaTreeNodePage.Create).Visualize(doc);

			t.Container.AttachTo(a.XMLVisualizer);

			


		}


	}


}
