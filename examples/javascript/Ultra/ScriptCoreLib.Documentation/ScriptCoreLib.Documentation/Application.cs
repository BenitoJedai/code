using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using ScriptCoreLib.Documentation.Documentation;
using System.Linq;
using ScriptCoreLib.Documentation.HTML.Pages.FromAssets;

namespace ScriptCoreLib.Documentation
{

	[Description("ScriptCoreLib.Documentation. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{

		public Application(IHTMLElement e)
		{
			Native.Document.title = "ScriptCoreLib.Documentation";

			var info = new IHTMLDiv();

			info.style.width = "65%";
			info.style.height = "100%";
			info.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			info.style.left = "35%";

			info.AttachToDocument();
			info.style.backgroundColor = JSColor.System.InfoBackground;
			info.style.color = JSColor.System.InfoText;
			info.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;

			var infoscrollable = new IHTMLDiv().AttachTo(info);

			infoscrollable.style.width = "100%";
			infoscrollable.style.height = "100%";
			infoscrollable.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			infoscrollable.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.auto;

			var infodrag = new IHTMLDiv().AttachTo(info);

			infodrag.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			infodrag.style.backgroundColor = JSColor.White;
			infodrag.style.Opacity = 0.4;
			infodrag.style.borderLeft = "1px solid gray";
			infodrag.style.width = "2em";
			infodrag.style.height = "100%";
			infodrag.style.cursor = ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.move;


			var infopadding = new IHTMLDiv().AttachTo(infoscrollable);
			infopadding.style.margin = "1em";
			infopadding.style.marginLeft = "3em";

			var infocontent = new Lorem();

			infocontent.Container.AttachTo(infopadding);

			var tree = new IHTMLDiv();

			tree.style.width = "35%";
			tree.style.height = "100%";
			tree.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			tree.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.auto;


			var c = new Compilation();

			var treepadding = new IHTMLDiv().AttachTo(tree);

			treepadding.style.margin = "1em";

			RenderArchives(c, treepadding,

				n => infocontent.Location.innerText = n

			);

			tree.AttachToDocument();

			var dragarea = new IHTMLDiv();

			dragarea.style.cursor = ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.move;
			dragarea.style.width = "100%";
			dragarea.style.height = "100%";
			dragarea.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;

			var dragareaabort = new IHTMLDiv().AttachTo(dragarea);

			dragareaabort.style.width = "100%";
			dragareaabort.style.height = "100%";

			dragareaabort.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;

			dragareaabort.style.backgroundColor = JSColor.Black;
			dragareaabort.style.Opacity = 0.05;


			var dragtarget = new IHTMLDiv().AttachTo(dragarea);

			dragtarget.style.borderLeft = "1px solid gray";
			dragtarget.style.borderRight = "1px solid gray";
			dragtarget.style.width = "2em";
			dragtarget.style.height = "100%";
			dragtarget.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			dragtarget.style.left = "35%";

			var dragmode = false;

			dragtarget.onmousedown +=
				ee =>
				{
					dragtarget.style.backgroundColor = JSColor.System.Highlight;
					dragmode = true;

					ee.PreventDefault();
					dragareaabort.style.Opacity = 0.05;
				};

			dragarea.onmousemove +=
				ee =>
				{
					if (!dragmode)
						return;

					var p = System.Convert.ToInt32(ee.CursorX * 100 / dragarea.offsetWidth);

					if (p < 20)
						p = 20;
					if (p > 80)
						p = 80;

					dragtarget.style.left = p + "%";
				};

			dragarea.onmouseup +=
				ee =>
				{
					if (!dragmode)
						return;

					dragmode = false;
					var p = System.Convert.ToInt32(ee.CursorX * 100 / dragarea.offsetWidth);

					if (p < 20)
						p = 20;
					if (p > 80)
						p = 80;

					dragtarget.style.left = p + "%";
					info.style.left = p + "%";
					info.style.width = (100 - p) + "%";
					tree.style.width = p + "%";

					dragareaabort.style.Opacity = 0;
					dragtarget.style.backgroundColor = JSColor.None;

				};

			dragareaabort.onmousemove +=
				ee =>
				{
					if (dragmode)
					{
						return;
					}

					dragtarget.style.backgroundColor = JSColor.None;
					dragarea.Orphanize();
				};

			infodrag.onmouseover +=
				delegate
				{
					dragareaabort.style.Opacity = 0.05;
					dragarea.AttachToDocument();
				};
		}

		private static void RenderArchives(Compilation c, IHTMLElement parent, Action<string> UpdateLocation)
		{
			foreach (var item in c.GetArchives())
			{
				var div = new IHTMLDiv().AttachTo(parent);

				div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;


				var i = new ScriptCoreLib.Documentation.HTML.Images.FromAssets.References().AttachTo(div);

				i.style.verticalAlign = "middle";
				i.style.marginRight = "0.5em";

				new IHTMLSpan { innerText = item.Name }.AttachTo(div);

				var children = new IHTMLDiv().AttachTo(parent);

				children.style.paddingLeft = "2em";


				RenderAssemblies(item, children, UpdateLocation);
			}
		}

		private static void RenderAssemblies(CompilationArchiveBase archive, IHTMLElement parent, Action<string> UpdateLocation)
		{
			foreach (var item2 in
				from a in archive.GetAssemblies()
				where a.Name.StartsWith("ScriptCoreLib")
				orderby a.Name
				select a)
			{
				var item = item2;

				var div = new IHTMLDiv().AttachTo(parent);

				div.style.marginTop = "0.1em";
				div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
				div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


				var i = new ScriptCoreLib.Documentation.HTML.Images.FromAssets.Assembly().AttachTo(div);

				i.style.verticalAlign = "middle";
				i.style.marginRight = "0.5em";

				var s = new IHTMLAnchor { innerText = item2.Name }.AttachTo(div);


				s.href = "#";
				s.style.textDecoration = "none";
				s.style.color = JSColor.System.WindowText;

				Action onclick = delegate
				{

				};

				s.onclick +=
					e =>
					{
						e.PreventDefault();

						s.focus();

						UpdateLocation(item.Name);

						onclick();
					};

				s.onfocus +=
					delegate
					{

						s.style.backgroundColor = JSColor.System.Highlight;
						s.style.color = JSColor.System.HighlightText;
					};

				s.onblur +=
					delegate
					{

						s.style.backgroundColor = JSColor.None;
						s.style.color = JSColor.System.WindowText;
					};

				onclick =
					delegate
					{
						var children = new IHTMLDiv().AttachTo(div);

						children.style.paddingLeft = "2em";

						AddDummyType(children, UpdateLocation);

						var NextClickHide = default(Action);
						var NextClickShow = default(Action);

						NextClickHide =
							delegate
							{
								children.Hide();

								onclick = NextClickShow;
							};

						NextClickShow =
							delegate
							{
								children.Show();

								onclick = NextClickHide;
							};


						onclick = NextClickHide;
					};
			}
		}

		private static void AddDummyType(IHTMLDiv parent, Action<string> UpdateLocation)
		{
			var div = new IHTMLDiv().AttachTo(parent);

			div.style.marginTop = "0.1em";
			div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


			var i = new ScriptCoreLib.Documentation.HTML.Images.FromAssets.PublicClass().AttachTo(div);

			i.style.verticalAlign = "middle";
			i.style.marginRight = "0.5em";

			var s = new IHTMLAnchor { innerText = "Class1" }.AttachTo(div);


			s.href = "#";
			s.style.textDecoration = "none";
			s.style.color = JSColor.System.WindowText;

			Action onclick = delegate
			{

			};

			s.onclick +=
				e =>
				{
					e.PreventDefault();

					s.focus();

					UpdateLocation("Class1");

					onclick();
				};

			s.onfocus +=
				delegate
				{

					s.style.backgroundColor = JSColor.System.Highlight;
					s.style.color = JSColor.System.HighlightText;
				};

			s.onblur +=
				delegate
				{

					s.style.backgroundColor = JSColor.None;
					s.style.color = JSColor.System.WindowText;
				};
		}
	}

}
