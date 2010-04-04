using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;

namespace ScriptCoreLib.Ultra.Components
{

	[Description("ScriptCoreLib.Ultra.Components. Write javascript, flash and java applets within a C# project.")]
	internal sealed partial class Application
	{
		public Application(IApplicationLoader p)
		{
			p.LoadingAnimation.FadeOut();

			p.Content.Add("hello world");

			Native.Document.title = "Hi!";

			p.Content.style.margin = "2em";

			{
				var s = new Section().ToSectionConcept();

				s.Header = "Syntax1";

				s.Content = DemoTree().Container;
				s.Content.style.border = "1px solid gray";


				s.Target.Container.AttachTo(p.Content);
			}

			{
				var s = new Section().ToSectionConcept();

				s.Header = "Countdown";

				s.Content = new CountDownGadgetConcept(CountDownGadget.Create)
				{
					Event = new DateTime(2010, 5, 24, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
					AutoUpdate = true
				};

				//s.Content = x.Container;

				s.Target.Container.AttachTo(p.Content);
			}

			{
				var s = new Section().ToSectionConcept();

				s.Header = "Countdown Days";

				s.Content = new CountDownGadgetConcept(CountDownGadget.Create)
				{
					ShowOnlyDays = true,
					Event = new DateTime(2010, 5, 24),
					
				};

				//s.Content = x.Container;

				s.Target.Container.AttachTo(p.Content);
			}
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
