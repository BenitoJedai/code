// For more information visit:
// http://studio.jsc-solutions.net

// View as Visual Basic project
// http://do.jsc-solutions.net/View-as-Visual-Basic-project

// View as Visual FSharp project
// http://do.jsc-solutions.net/View-as-Visual-FSharp-project

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using TestSolutionBuilderWithDesigner.HTML.Pages;
using ScriptCoreLib.Ultra.Studio;
using ScriptCoreLib.Ultra.Studio.StockData;
using ScriptCoreLib.Ultra.Studio.StockPages;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.Ultra.Lookup;
using System.Collections.Generic;

namespace TestSolutionBuilderWithDesigner
{
	/// <summary>
	/// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
	/// </summary>
	public sealed class Application
	{
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IDefaultPage page)
		{
			new JSCSolutionsNETImage().ToBackground(page.Item1.style, false);
			new JSCSolutionsNETImage().ToBackground(page.Item2.style, false);


			// Update document title
			// http://do.jsc-solutions.net/Update-document-title

			@"Hello world".ToDocumentTitle();
			// Send xml to server
			// http://do.jsc-solutions.net/Send-xml-to-server

			var v = new SolutionFileView();

			var f = new SolutionFile();

			f.WriteHTMLElement(StockPageDefault.Element);

			v.File = f;

			var Container = new IHTMLDiv();

			Container.style.position = IStyle.PositionEnum.relative;
			Container.style.display = IStyle.DisplayEnum.inline_block;
			Container.style.width = "600px";
			Container.style.height = "400px";
			Container.style.border = "1px solid gray";

			var ToolbarHeight = "1.3em";
			var Content = new IHTMLDiv().AttachTo(Container);

			Content.style.position = IStyle.PositionEnum.absolute;
			Content.style.left = "0px";
			Content.style.top = "0px";
			Content.style.right = "0px";
			Content.style.bottom = ToolbarHeight;


			var Toolbar = new IHTMLDiv().AttachTo(Container);

			Toolbar.style.backgroundColor = Color.FromGray(0xef);
			Toolbar.style.position = IStyle.PositionEnum.absolute;
			Toolbar.style.left = "0px";
			Toolbar.style.height = ToolbarHeight;
			Toolbar.style.right = "0px";
			Toolbar.style.bottom = "0px";

			Action<IHTMLImage, string, Action> AddToolbarButton =
				(img, text, handler) =>
				{
					var span = new IHTMLSpan { innerText = text };

					span.style.paddingLeft = "1.5em";
					span.style.paddingRight = "0.3em";

					var a = new IHTMLAnchor
					{
						img, span
					};

					img.style.verticalAlign = "middle";
					img.border = 0;
					img.style.position = IStyle.PositionEnum.absolute;

					a.style.backgroundColor = Color.FromGray(0xef);
					a.style.color = Color.Black;
					a.style.textDecoration = "none";
					a.style.fontFamily = IStyle.FontFamilyEnum.Tahoma;

					a.href = "javascript: void(0);";
					a.onclick +=
						delegate
						{
							handler();
						};
					a.style.display = IStyle.DisplayEnum.inline_block;
					a.style.height = "100%";


					a.onmousemove +=
						delegate
						{
							a.style.backgroundColor = Color.FromGray(0xff);
						};

					a.onmouseout +=
						delegate
						{
							a.style.backgroundColor = Color.FromGray(0xef);
						};

					Toolbar.Add(a);
				};


			v.Container.style.height = "100%";
			v.Container.AttachTo(Content);


			Content.Add(v.Container);

			var i = CreateEditor();

			i.AttachTo(Content);



			var ii = new IHTMLPre().AttachTo(Content);

			ii.style.position = IStyle.PositionEnum.absolute;
			ii.style.left = "0px";
			ii.style.top = "0px";
			ii.style.right = "0px";
			ii.style.bottom = "0px";
			ii.style.overflow = IStyle.OverflowEnum.auto;
			ii.style.padding = "0px";
			ii.style.margin = "0px";
			ii.style.whiteSpace = IStyle.WhiteSpaceEnum.normal;

			v.Container.style.display = IStyle.DisplayEnum.none;
			i.style.display = IStyle.DisplayEnum.empty;
			ii.style.display = IStyle.DisplayEnum.none;

			AddToolbarButton(new RTA_mode_design(), "Design",
				delegate
				{
					v.Container.style.display = IStyle.DisplayEnum.none;
					ii.style.display = IStyle.DisplayEnum.none;
					i.style.display = IStyle.DisplayEnum.empty;
				}
			);

			AddToolbarButton(new RTA_mode_html(), "Source",
				delegate
				{
					v.Container.style.display = IStyle.DisplayEnum.empty;
					ii.style.display = IStyle.DisplayEnum.none;
					i.style.display = IStyle.DisplayEnum.none;

					f.Clear();

					i.WhenContentReady(
						body =>
						{
							f.WriteHTMLElement(body.AsXElement());

							// update
							v.File = f;
						}
					);


				}
			);

			AddToolbarButton(new RTA_mode_html(), "Source raw",
				delegate
				{
					v.Container.style.display = IStyle.DisplayEnum.none;
					ii.style.display = IStyle.DisplayEnum.empty;
					i.style.display = IStyle.DisplayEnum.none;



					i.WhenContentReady(
						body =>
						{
							ii.innerText = body.AsXElement().ToString();
						}
					);


				}
			);

			page.PageContainer.Add(Container);

			new ApplicationWebService().WebMethod2(
				new XElement(@"Document",
					new object[] {
						new XElement(@"Data", 
							new object[] {
								@"Hello world"
							}
						),
						new XElement(@"Client", 
							new object[] {
								@"Unchanged text"
							}
						)
					}
				),
				delegate(XElement doc)
				{
					// Show server message as document title
					// http://do.jsc-solutions.net/Show-server-message-as-document-title

					doc.Element(@"Data").Value.ToDocumentTitle();
				}
			);
		}





		internal static IHTMLIFrame CreateEditor()
		{
			var edit = new IHTMLIFrame { src = "about:blank" };

			edit.style.width = "100%";
			edit.style.height = "100%";
			edit.style.border = "0";
			edit.style.margin = "0";
			edit.style.padding = "0";
			edit.frameborder = "0";
			edit.border = "0";

			edit.WhenDocumentReady(
				document =>
				{

					document.WithContent(StockPageDefault.Element);

					document.DesignMode = true;
				}
			);
			return edit;
		}

	}
}
