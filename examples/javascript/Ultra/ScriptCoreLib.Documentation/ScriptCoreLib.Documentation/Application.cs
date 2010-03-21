using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using ScriptCoreLib.Documentation.Documentation;

namespace ScriptCoreLib.Documentation
{

	[Description("ScriptCoreLib.Documentation. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{

		public Application(IHTMLElement e)
		{
			Native.Document.title = "ScriptCoreLib.Documentation";
			Native.Document.body.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.auto;

			var c = new Compilation();

			RenderArchives(c);

		}

		private static void RenderArchives(Compilation c)
		{
			foreach (var item in c.GetArchives())
			{
				var div = new IHTMLDiv().AttachToDocument();

				new ScriptCoreLib.Documentation.HTML.Images.FromAssets.Archive().AttachTo(div).style.SetSize(24, 24);

				new IHTMLCode { innerText = item.Name }.AttachTo(div);


				RenderAssemblies(item);
			}
		}

		private static void RenderAssemblies(CompilationArchiveBase item)
		{
			foreach (var item2 in item.GetAssemblies())
			{
				var div = new IHTMLDiv().AttachToDocument();

				div.style.paddingLeft = "2em";

				new ScriptCoreLib.Documentation.HTML.Images.FromAssets.DLL().AttachTo(div).style.SetSize(24, 24);

				new IHTMLCode { innerText = item2.Name }.AttachTo(div);

			}
		}
	}

}
