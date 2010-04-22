using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Components.HTML.Images.SpriteSheet.FromAssets;
using ScriptCoreLib.JavaScript.Components;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Components
{

	[Description("ScriptCoreLib.Ultra.Components. Write javascript, flash and java applets within a C# project.")]
	internal sealed partial class Application
	{
		public Application(IApplicationLoader p)
		{
			p.LoadingAnimation.FadeOut();

			p.Content.Add("ScriptCoreLib.Ultra.Components");

			Native.Document.title = "Hi!";

			p.Content.style.margin = "2em";
			AddSection1(p);

			AddSection2(p);

			AddSection3(p);
			AddSection4(p);
			AddSection5(p);
		}

		private static void AddSection5(IApplicationLoader pp)
		{
			var s = new Section().ToSectionConcept();

			s.Header = "Horizontal split with tree";
			var Content = new IHTMLDiv().With(
				k =>
				{
					k.style.border = "1px solid gray";
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
					k.style.width = "100%";
					k.style.height = "6em";
				}
			);



			var hh = new HorizontalSplit
			{
				Minimum = 0.05,
				Maximum = 0.95,
				Value = 0.7,
				RightContainer = DemoTree().Container,
			};

			hh.Split.LeftScrollable = new IHTMLDiv();

			hh.Split.LeftScrollable.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			hh.Split.LeftScrollable.style.width = "100%";
			hh.Split.LeftScrollable.style.height = "100%";

			var edit = new IHTMLIFrame { src = "about:blank" }.AttachTo(hh.Split.LeftScrollable);

			edit.style.width = "100%";
			edit.style.height = "100%";
			edit.style.border = "0";
			edit.style.margin = "0";
			edit.style.padding = "0";
			edit.frameborder = "0";
			edit.border = "0";

			new Timer(
				t =>
				{
					if (edit.contentWindow.document == null)
						return;

					t.Stop();


					edit.contentWindow.document.WithContent(
						new XElement("style", "span { color: red; }"),
						new XElement("span", "hello world")
					);

					edit.contentWindow.document.DesignMode = true;
				}
			).StartInterval(15);


			hh.Container.AttachTo(Content);

			s.Content = Content;

			s.IsExpanded = false;
			s.Target.Container.AttachTo(pp.Content);
		}

		private static void AddSection4(IApplicationLoader pp)
		{
			var s = new Section().ToSectionConcept();

			s.Header = "Horizontal split";
			var Content = new IHTMLDiv().With(
				k =>
				{
					k.style.border = "1px solid gray";
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
					k.style.width = "100%";
					k.style.height = "4em";
				}
			);

			var hh = new HorizontalSplit
			{
				Minimum = 0.05,
				Maximum = 0.95,
				Value = 0.7
			};

			hh.Container.AttachTo(Content);

			s.Content = Content;

			s.IsExpanded = false;
			s.Target.Container.AttachTo(pp.Content);
		}

		private static void AddSection3(IApplicationLoader p)
		{
			var s = new Section().ToSectionConcept();

			s.Header = "Countdown Days";

			s.Content = new CountDownGadgetConcept(CountDownGadget.Create)
			{
				ShowOnlyDays = true,
				Event = new DateTime(2010, 5, 24),

			};

			//s.Content = x.Container;

			s.IsExpanded = false;
			s.Target.Container.style.backgroundColor = "#efefef";
			s.Target.Container.AttachTo(p.Content);
		}

		private static void AddSection2(IApplicationLoader p)
		{
			var s = new Section().ToSectionConcept();

			s.Header = "Countdown";

			var Now = DateTime.Now;
			var Event = new DateTime(2010, 5, 24, Now.Hour, Now.Minute, Now.Second);

			s.Content = new CountDownGadgetConcept(CountDownGadget.Create)
			{
				Event = Event,
				AutoUpdate = true
			};

			//s.Content = x.Container;

			s.IsExpanded = false;
			s.Target.Container.AttachTo(p.Content);
		}

		private static void AddSection1(IApplicationLoader p)
		{
			var s = new Section().ToSectionConcept();

			s.Header = "Syntax1";

			s.Content = DemoTree().Container;
			s.Content.style.border = "1px solid gray";


			s.IsExpanded = false;
			s.Target.Container.style.backgroundColor = "#efefef";
			s.Target.Container.AttachTo(p.Content);
		}

		private static TreeNode DemoTree()
		{
			var sln = new TreeNode(() => new VistaTreeNodePage());

			sln.Text = "Solution";
			sln.IsExpanded = true;

			Action<TreeNode> AddReferences =
				p =>
				{
					var r = p.Add("References", new References());

					r.Add("System", new Assembly());
					r.Add("System.Core", new Assembly());
					r.Add("ScriptCoreLib", new Assembly());
					r.Add("ScriptCoreLib.Ultra", new Assembly());
					r.Add("ScriptCoreLib.Ultra.Library", new Assembly());
					r.Add("ScriptCoreLib.Ultra.Controls", new Assembly());
					r.Add("ScriptCoreLibJava", new Assembly());
					r.Add("jsc.meta", new Assembly());
				};

			Action<TreeNode> AddUltraSource =
				p =>
				{
					var my = p.Add("My.UltraSource");
					my.Add("Control1.htm", new HTMLDocument());
					my.Add("Default.htm", new HTMLDocument());
					my.Add("jsc.png", new ImageFile());

				};

			{
				var p = sln.Add("UltraApplication1.Concepts", new VisualCSharpProject());

				AddReferences(p);

				p.Add("Control1Concept.cs", new VisualCSharpCode());
			}


			{
				var p = sln.Add("UltraApplication1", new VisualCSharpProject());


				AddReferences(p);
				AddUltraSource(p);



				p.Add("Application.cs", new VisualCSharpCode());
				p.Add("WebService.cs", new VisualCSharpCode());
				p.Add("Program.cs", new VisualCSharpCode());
			}

			{
				var p = sln.Add("UltraApplication1", new VisualBasicProject());

				AddReferences(p);
				AddUltraSource(p);

				p.Add("Application.vb", new VisualBasicCode());
				p.Add("WebService.vb", new VisualBasicCode());
				p.Add("Program.vb", new VisualBasicCode());
			}


			{
				var p = sln.Add("UltraApplication1", new VisualFSharpProject());

				AddReferences(p);
				AddUltraSource(p);


				p.Add("Application.fs", new VisualFSharpCode());
				p.Add("WebService.fs", new VisualFSharpCode());
				p.Add("Program.fs", new VisualFSharpCode());
			}

			return sln;
		}


	}


}
