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
using ScriptCoreLib.Shared.Drawing;
using System.Collections.Generic;

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

			var Sections = new List<SectionConcept<Section>>();

			Action<string, IHTMLDiv> AddSection =
				(Header, Content) =>
				{
					var s = new Section().ToSectionConcept();

					Sections.Add(s);

					if (Sections.Count % 2 == 0)
						s.Content.style.border = "1px solid gray";

					s.Header = Header;

					s.Content = Content;

					s.IsExpanded = false;
					s.Target.Container.AttachTo(p.Content);
				};

			AddSection5(AddSection);
			AddSection6(AddSection);
			AddSection7(AddSection);
			AddSection8(AddSection);

		}

		private static void AddSection8(Action<string, IHTMLDiv> AddSection)
		{

			var Content = new IHTMLDiv().With(
				k =>
				{
					k.style.border = "1px solid gray";
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
					k.style.width = "100%";
					k.style.height = "25em";
				}
			);

			var vsv = new VisualStudioView();

			vsv.Container.AttachTo(Content);

			AddSection(
				"TwentyTen Design",
				Content
			);
		}

		private static void AddSection7(Action<string, IHTMLDiv> AddSection)
		{

			var Content = new IHTMLDiv().With(
				k =>
				{
					k.style.border = "1px solid gray";
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
					k.style.width = "100%";
					k.style.height = "15em";
				}
			);



			var ToolboxSplit = new HorizontalSplit
			{
				Minimum = 0.05,
				Maximum = 0.5,
				Value = 0.15,
			};

			var ToolboxContainer = ToolboxSplit.LeftContainer;

			Action<string, string> AddGroup =
				(Header, Title) =>
				{
					#region group
					{
						var s = new Section().ToSectionConcept();

						s.Target.Header.style.marginLeft = "1em";
						s.Header = Header;
						s.Header.title = Title;

						s.Content.Clear();

						VisualStudioView.CreateToolboxTo(s.Content);

						s.IsExpanded = false;
						s.Target.Container.AttachTo(ToolboxContainer);
					}
					#endregion
				};

			AddGroup("HTML Components", "For example a section or a split view");
			AddGroup("Flash Components", "For example an empty sprite or a visualization");
			AddGroup("Java Applets", "For example a calculator form or a visualization");

			var EditorTreeSplit = new HorizontalSplit
			{
				Minimum = 0.5,
				Maximum = 0.95,
				Value = 0.6,
			};

			EditorTreeSplit.Split.LeftScrollable = VisualStudioView.CreateEditor().WithinContainer();
			EditorTreeSplit.Split.RightContainer = VisualStudioView.DemoTree().Container;

			ToolboxSplit.Split.RightScrollable = EditorTreeSplit.Container;

			ToolboxSplit.Container.AttachTo(Content);

			AddSection(
				"Horizontal split with tree and sub split reversed",
				Content
			);
		}

	
		private static void AddSection6(Action<string, IHTMLDiv> AddSection)
		{

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
				RightContainer = VisualStudioView.DemoTree().Container,
			};

			var hh2 = new HorizontalSplit
			{
				Minimum = 0.05,
				Maximum = 0.5,
				Value = 0.7,
			};

			hh.Split.LeftScrollable = hh2.Container;

			hh.Container.AttachTo(Content);

			AddSection(
				"Horizontal split with tree and sub split",
				Content
			);
		}


		private static void AddSection5(Action<string, IHTMLDiv> AddSection)
		{

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
				RightContainer = VisualStudioView.DemoTree().Container,
			};

			hh.Split.LeftScrollable = new IHTMLDiv();

			hh.Split.LeftScrollable.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			hh.Split.LeftScrollable.style.width = "100%";
			hh.Split.LeftScrollable.style.height = "100%";


			var edit = VisualStudioView.CreateEditor();

			edit.AttachTo(hh.Split.LeftScrollable);

			hh.Container.AttachTo(Content);

			AddSection(
				"Horizontal split with tree",
				Content
			);
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

			s.Content = VisualStudioView.DemoTree().Container;
			s.Content.style.border = "1px solid gray";


			s.IsExpanded = false;
			s.Target.Container.style.backgroundColor = "#efefef";
			s.Target.Container.AttachTo(p.Content);
		}



	}


}
