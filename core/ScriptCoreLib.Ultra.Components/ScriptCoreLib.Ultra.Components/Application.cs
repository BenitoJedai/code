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
					k.style.height = "10em";
				}
			);

			{
				var hs = new HorizontalSplit();


				hs.Container.AttachTo(Content);

				
				var hsa = new DragAreaImage();

				var hsm = new IHTMLDiv();
				hsm.AttachTo(hs.Splitter);
				hsm.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
				hsm.style.left = "1px";
				hsm.style.top = "50%";
				hsm.style.marginTop = (-DragAreaImage.ImageDefaultHeight) + "px";

				hsa.AttachTo(hsm);

				var hsArea = new HorizontalSplitArea();

				hsArea.Abort.style.Opacity = 0.05;


				var dragmode = false;

				hsArea.Target.onmousedown +=
					ee =>
					{
						hsArea.Target.style.backgroundColor = JSColor.System.Highlight;
						dragmode = true;

						ee.PreventDefault();
						hsArea.Abort.style.Opacity = 0.05;
					};

				hsArea.Container.onmousemove +=
					ee =>
					{
						var OffsetX = ee.GetOffsetX(hsArea.Container);

						s.Header = new { OffsetX }.ToString();

						if (!dragmode)
							return;

						var p = System.Convert.ToInt32(OffsetX * 100 / hsArea.Container.offsetWidth);

						if (p < 20)
							p = 20;
						if (p > 80)
							p = 80;

						hsArea.Target.style.left = p + "%";
					};

				hsArea.Container.onmouseup +=
					ee =>
					{
						if (!dragmode)
							return;

						var OffsetX = ee.GetOffsetX(hsArea.Container);

						dragmode = false;
						var p = System.Convert.ToInt32(OffsetX * 100 / hsArea.Container.offsetWidth);

						if (p < 20)
							p = 20;
						if (p > 80)
							p = 80;

						hsArea.Target.style.left = p + "%";
						hs.Right.style.left = p + "%";
						hs.Right.style.width = (100 - p) + "%";
						hs.Left.style.width = p + "%";

						hsArea.Abort.style.Opacity = 0;
						hsArea.Target.style.backgroundColor = JSColor.None;

					};

				hsArea.Abort.onmousemove +=
					ee =>
					{
						if (dragmode)
						{
							return;
						}

						hsArea.Target.style.backgroundColor = JSColor.None;
						hsArea.Container.Orphanize();
					};

				hs.Splitter.onmouseover +=
					delegate
					{
						hsArea.Abort.style.Opacity = 0.05;

						hsArea.Container.AttachTo(hs.Container);
					};
			}


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
