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

			Action<XElement, IHTMLDiv> Visualize = null;

			var t = new TreeNode(VistaTreeNodePage.Create);

			t.Container.AttachTo(a.XMLVisualizer);

			ApplyLocalName(t, doc.Name.LocalName);

			ApplyChildren(doc, t);


		}

		private static XElement ApplyChildren(XElement doc, TreeNode t)
		{
			var q = Enumerable.ToArray(
				from n in doc.Elements()
				let nt = ApplyLocalName(t.Add(), n.Name.LocalName)
				let cc = ApplyChildren(n, nt)
				select nt
			);

			return doc;
		}

		private static TreeNode ApplyLocalName(TreeNode t, string LocalName)
		{
			t.IsExpanded = true;
			t.Element.TextArea.Clear();
			var c = new IHTMLCode();
			t.Element.TextArea.Add(c);

			t.Element.ButtonArea.Hide();
			t.Element.IconArea.Hide();

			Action<string, JSColor> Write =
				(Text, Color) =>
				{
					var cs = new IHTMLSpan { innerText = Text };

					cs.style.color = Color;

					cs.AttachTo(c);
				};

			Write("<", JSColor.Blue);
			Write(LocalName, JSColor.FromRGB(0xa0, 0, 0));
			Write("/>", JSColor.Blue);

			return t;
		}

	}


}
