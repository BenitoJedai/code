using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.Extensions;
using XElementEverywhere.HTML.Pages;
using System.Xml.Linq;

namespace XElementEverywhere
{
	public sealed class XLinqSprite : Sprite
	{
		Action<XElement> InternalSetContent;

		public void SetContent(XElement e)
		{
			e.Add(
					new XElement("ActionScript", new object[] { "Zing!" })
				);


			InternalSetContent(e);
		}

		public XLinqSprite()
		{
			this.InvokeWhenStageIsReady(
				delegate
				{
					var c = new Canvas();
					var t = new TextBox().AttachTo(c);

					t.AcceptsReturn = true;

					t.Width = 400;
					t.Height = 300;

					var Text = new Data.MyDocumentSource().Text;

					t.Text = Text;
					c.AttachToContainer(this);

					var doc = XDocument.Parse(
						Text
					);

					doc.Root.Add("hello world");
					doc.Root.Add("hello world");

					doc.Root.Add(
						"Foo",
						"bar",
						new XElement("Bar", new object[] { 
							"foo",
							
							new XElement("Bar", new object[] { "foo" }),
							new XElement("Bar", new object[] { "foo" })
						
						})
					);


					InternalSetContent =
						x =>
						{
							t.Text = x.ToString();
						};

					SetContent(doc.Root);
				}
			);
		}

	}

	[Description("XElementEverywhere. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IAbout a)
		{
			Data.MyDocument.CreateAsElement(
				Document =>
				{
					var q = Enumerable.ToArray(
						from x in Document.Elements("Data")
						let Color = x.Element("Color").Value
						let Text = x.Element("Text").Value
						let div = new IHTMLDiv { innerText = Text }.Apply(k => k.style.color = Color)
						select div.AttachToDocument()
					);
				}
			);

			var s = new XLinqSprite();



			s.AttachSpriteTo(a.Foo);


			a.Bar.onclick +=
				delegate
				{
					var x = XElement.Parse(a.Bar.innerHTML);

					x.Add(
						new XElement("JavaScript", new object[] { "Yay" })
					);

					s.SetContent(
						x
					);
				};
		}

	}


}
