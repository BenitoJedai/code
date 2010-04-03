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

			DemoTree(p);


			{
				var s = new Section().ToSectionConcept();

				s.Header = "Syntax1";

				s.Target.Container.AttachTo(p.Content);
			}

			{
				var s = new Section().ToSectionConcept();

				s.Header = "Methods2";

				s.Target.Container.AttachTo(p.Content);
			}

			{
				var r = new VistaTreeNodePage();

			
				var g = r.ChildContainer.cloneNode(true).AttachTo(r.ChildArea);

				r.ChildContainer.Orphanize();

				{
					var n = new VistaTreeNodePage();

					n.OpenImage = new VisualCSharpProject();
					n.ClosedImage = new VisualCSharpProject();


					n.Container.AttachTo(g);
				}

				{
					var n = new VistaTreeNodePage();


					n.OpenImage = new VisualBasicProject();
					n.ClosedImage = new VisualBasicProject();


					n.Container.AttachTo(g);
				}
				{
					var n = new VistaTreeNodePage();

					n.OpenImage = new VisualFSharpProject();
					n.ClosedImage = new VisualFSharpProject();


					n.Container.AttachTo(g);

				
				}

				r.Container.AttachTo(p.Content);


				
			}
		}

		private static void DemoTree(IApplicationLoader a)
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
					my.Add("Default.htm", new HTMLDocument());
					my.Add("jsc.png", new ImageFile());

				};

			{
				var p = sln.Add("Visual C# Project", new VisualCSharpProject());


				AddReferences(p);
				AddUltraSource(p);



				p.Add("Application.cs", new VisualCSharpCode());
				p.Add("WebService.cs", new VisualCSharpCode());
				p.Add("Program.cs", new VisualCSharpCode());
			}

			{
				var p = sln.Add("Visual Basic Project", new VisualBasicProject());

				AddReferences(p);
				AddUltraSource(p);

				p.Add("Application.vb", new VisualBasicCode());
				p.Add("WebService.vb", new VisualBasicCode());
				p.Add("Program.vb", new VisualBasicCode());
			}


			{
				var p = sln.Add("Visual F# Project", new VisualFSharpProject());

				AddReferences(p);
				AddUltraSource(p);


				p.Add("Application.fs", new VisualFSharpCode());
				p.Add("WebService.fs", new VisualFSharpCode());
				p.Add("Program.fs", new VisualFSharpCode());
			}

			sln.Container.AttachTo(a.Content);
		}


	}


}
