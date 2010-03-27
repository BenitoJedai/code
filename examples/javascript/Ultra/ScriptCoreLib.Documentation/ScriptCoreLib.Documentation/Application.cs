using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using System.Linq;
using ScriptCoreLib.Documentation.HTML.Pages.FromAssets;
using PromotionWebApplication.AvalonLogo;
using ScriptCoreLib.Shared.Lambda;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.Documentation
{

	[Description("ScriptCoreLib.Documentation. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{

		public Application(IHTMLElement e)
		{
			Native.Document.title = "ScriptCoreLib.Documentation";

			var hs = new HorizontalSplit();


			hs.Container.AttachToDocument();



			Action<double> AddGradient =
				gw =>
				{
					var infodraggradient0 = new IHTMLDiv().AttachTo(hs.Splitter);

					infodraggradient0.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					infodraggradient0.style.width = gw + "em";
					infodraggradient0.style.height = "100%";
					infodraggradient0.style.backgroundColor = JSColor.White;
					infodraggradient0.style.Opacity = 0.2;
				};

			for (int i = 0; i < 20; i += 2)
			{
				AddGradient(2.0 - i * 0.1);
			}


			var infocontent = new Lorem();

			infocontent.Container.AttachTo(hs.RightContainer);


			AttachLogoAnimation(infocontent);




			var c = new Compilation();

			RenderArchives(c, hs.LeftContainer,

				n => infocontent.Location.innerText = n

			);


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
					if (!dragmode)
						return;

					var p = System.Convert.ToInt32(ee.CursorX * 100 / hsArea.Container.offsetWidth);

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

					dragmode = false;
					var p = System.Convert.ToInt32(ee.CursorX * 100 / hsArea.Container.offsetWidth);

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

		private static void AttachLogoAnimation(Lorem infocontent)
		{
			#region manual precache
			new PromotionWebApplication.AvalonLogo.Avalon.Images.Apple_Safari();
			new PromotionWebApplication.AvalonLogo.Avalon.Images.Firefox_3();
			new PromotionWebApplication.AvalonLogo.Avalon.Images.Google_Chrome();
			new PromotionWebApplication.AvalonLogo.Avalon.Images.Internet_Explorer_7_Logo();
			new PromotionWebApplication.AvalonLogo.Avalon.Images.jsc();
			new PromotionWebApplication.AvalonLogo.Avalon.Images.Opera();
			#endregion

			ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
				4000,
				delegate
				{
					var logo = new AvalonLogoCanvas
					{
						CloseOnClick = false
					};


					ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(10000,
						() =>
						{
							logo.HideSattelites();
						}
					);

					infocontent.LogoContainer.removeChildren();

					var logoc = new IHTMLDiv().AttachTo(infocontent.LogoContainer);

					logoc.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					logoc.style.SetLocation((AvalonLogoCanvas.DefaultWidth - 96) / -2, (AvalonLogoCanvas.DefaultHeight - 96) / -2);

					logo.Container.AttachToContainer(logoc);
				}
				);
		}

		private void RenderArchives(Compilation c, IHTMLElement parent, Action<string> UpdateLocation)
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

				children.style.paddingLeft = "1em";


				RenderAssemblies(item, children, UpdateLocation);
			}
		}

		private void RenderAssemblies(CompilationArchiveBase archive, IHTMLElement parent, Action<string> UpdateLocation)
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

				var NamespaceLookup = new Dictionary<string, IHTMLDiv>();

				Func<IHTMLDiv, CompilationType, IHTMLDiv> GetNamespaceContainer =
					(Container, SourceType) =>
					{
						if (!NamespaceLookup.ContainsKey(SourceType.Namespace))
						{
							NamespaceLookup[SourceType.Namespace] = AddNamespace(Container, SourceType.Namespace, UpdateLocation);
						}

						return NamespaceLookup[SourceType.Namespace];
					};

				onclick =
					delegate
					{
						var children = new IHTMLDiv().AttachTo(div);

						children.style.paddingLeft = "1em";

						item.WhenReady(
							a =>
							{
								a.GetTypes().ForEach(
									(Current, Next) =>
									{
										var TypeContainer = default(IHTMLDiv);

										if (Current.DeclaringType != null)
										{
											return;
										}

										TypeContainer = GetNamespaceContainer(children, Current);

										AddType(TypeContainer, Current, UpdateLocation);

										ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
											50,
											Next
										);
									}
								);
							}
						);


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

		private static IHTMLDiv AddNamespace(IHTMLDiv parent, string Namespace, Action<string> UpdateLocation)
		{
			var div = new IHTMLDiv().AttachTo(parent);

			div.style.marginTop = "0.1em";
			div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


			var i = new ScriptCoreLib.Documentation.HTML.Images.FromAssets.Namespace().AttachTo(div);

			i.style.verticalAlign = "middle";
			i.style.marginRight = "0.5em";

			var s = new IHTMLAnchor { innerText = Namespace }.AttachTo(div);


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

					UpdateLocation(Namespace);

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

			var children = new IHTMLDiv().AttachTo(div);

			children.style.paddingLeft = "1em";
			children.Hide();


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


			onclick = NextClickShow;

			return children;
		}

		public event Action<CompilationType> TouchTypeSelected;

		private void AddType(IHTMLDiv parent, CompilationType type, Action<string> UpdateLocation)
		{
			var div = new IHTMLDiv().AttachTo(parent);

			div.style.marginTop = "0.1em";
			div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


			var i = default(IHTMLImage);

			if (type.IsInterface)
			{
				i = new ScriptCoreLib.Documentation.HTML.Images.FromAssets.PublicInterface();
			}
			else
			{
				i = new ScriptCoreLib.Documentation.HTML.Images.FromAssets.PublicClass();
			}
			
			i.AttachTo(div);

			i.style.verticalAlign = "middle";
			i.style.marginRight = "0.5em";

			var s = new IHTMLAnchor { innerText = type.Name, title = "" + type.MetadataToken }.AttachTo(div);


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

					if (TouchTypeSelected != null)
						TouchTypeSelected(type);

					UpdateLocation(type.FullName);

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

					children.style.paddingLeft = "1em";

					var _nested = new IHTMLDiv().AttachTo(children);
					var _fields = new IHTMLDiv().AttachTo(children);
					var _constructors = new IHTMLDiv().AttachTo(children);
					var _methods = new IHTMLDiv().AttachTo(children);


					type.GetNestedTypes().ForEach(
						(Current, Next) =>
						{
							AddType(_nested, Current, UpdateLocation);

							ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
								50,
								Next
							);
						}
					);

					type.GetConstructors().ForEach(
						(Current, Next) =>
						{
							AddTypeConstructor(_constructors, Current, UpdateLocation);

							ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
								50,
								Next
							);
						}
					);

					type.GetMethods().ForEach(
						(Current, Next) =>
						{
							AddTypeMethod(_methods, Current, UpdateLocation);

							ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
								50,
								Next
							);
						}
					);


					type.GetFields().ForEach(
						(Current, Next) =>
						{
							AddTypeField(_fields, Current, UpdateLocation);

							ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
								50,
								Next
							);
						}
					);

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

		private static void AddTypeField(IHTMLDiv parent, CompilationField type, Action<string> UpdateLocation)
		{
			var div = new IHTMLDiv().AttachTo(parent);

			div.style.marginTop = "0.1em";
			div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


			var i = new ScriptCoreLib.Documentation.HTML.Images.FromAssets.PublicField().AttachTo(div);

			i.style.verticalAlign = "middle";
			i.style.marginRight = "0.5em";

			var s = new IHTMLAnchor { innerText = type.Name }.AttachTo(div);


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

					UpdateLocation(type.Name);

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
					//var children = new IHTMLDiv().AttachTo(div);

					//children.style.paddingLeft = "2em";

					//a.GetTypes().ForEach(
					//    (Current, Next) =>
					//    {
					//        AddType(GetNamespaceContainer(children, Current), Current, UpdateLocation);

					//        ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
					//            50,
					//            Next
					//        );
					//    }
					//);


					var NextClickHide = default(Action);
					var NextClickShow = default(Action);

					NextClickHide =
						delegate
						{
							//children.Hide();

							onclick = NextClickShow;
						};

					NextClickShow =
						delegate
						{
							//children.Show();

							onclick = NextClickHide;
						};


					onclick = NextClickHide;
				};
		}

		private static void AddTypeConstructor(IHTMLDiv parent, CompilationConstructor type, Action<string> UpdateLocation)
		{
			var div = new IHTMLDiv().AttachTo(parent);

			div.style.marginTop = "0.1em";
			div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


			var i = new ScriptCoreLib.Documentation.HTML.Images.FromAssets.PublicMethod().AttachTo(div);

			i.style.verticalAlign = "middle";
			i.style.marginRight = "0.5em";

			var w = new StringBuilder();

			w.Append(type.DeclaringType.Name);

			w.Append("(");

			type.GetParameters().ForEach(
				(p, pi) =>
				{
					if (pi > 0)
						w.Append(", ");

					w.Append(p.Name);
				}
			);

			w.Append(")");


			var s = new IHTMLAnchor { innerText = w.ToString() }.AttachTo(div);


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

					UpdateLocation(type.DeclaringType.Name + ".ctor");

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
					//var children = new IHTMLDiv().AttachTo(div);

					//children.style.paddingLeft = "2em";

					//a.GetTypes().ForEach(
					//    (Current, Next) =>
					//    {
					//        AddType(GetNamespaceContainer(children, Current), Current, UpdateLocation);

					//        ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
					//            50,
					//            Next
					//        );
					//    }
					//);


					var NextClickHide = default(Action);
					var NextClickShow = default(Action);

					NextClickHide =
						delegate
						{
							//children.Hide();

							onclick = NextClickShow;
						};

					NextClickShow =
						delegate
						{
							//children.Show();

							onclick = NextClickHide;
						};


					onclick = NextClickHide;
				};
		}

		private static void AddTypeMethod(IHTMLDiv parent, CompilationMethod type, Action<string> UpdateLocation)
		{
			var div = new IHTMLDiv().AttachTo(parent);

			div.style.marginTop = "0.1em";
			div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


			var i = new ScriptCoreLib.Documentation.HTML.Images.FromAssets.PublicMethod().AttachTo(div);

			i.style.verticalAlign = "middle";
			i.style.marginRight = "0.5em";

			var w = new StringBuilder();

			w.Append(type.Name);

			w.Append("(");

			type.GetParameters().ForEach(
				(p, pi) =>
				{
					if (pi > 0)
						w.Append(", ");

					w.Append(p.Name);
				}
			);

			w.Append(")");


			var s = new IHTMLAnchor { innerText = w.ToString() }.AttachTo(div);


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

					UpdateLocation(type.Name);

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
					//var children = new IHTMLDiv().AttachTo(div);

					//children.style.paddingLeft = "2em";

					//a.GetTypes().ForEach(
					//    (Current, Next) =>
					//    {
					//        AddType(GetNamespaceContainer(children, Current), Current, UpdateLocation);

					//        ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
					//            50,
					//            Next
					//        );
					//    }
					//);


					var NextClickHide = default(Action);
					var NextClickShow = default(Action);

					NextClickHide =
						delegate
						{
							//children.Hide();

							onclick = NextClickShow;
						};

					NextClickShow =
						delegate
						{
							//children.Show();

							onclick = NextClickHide;
						};


					onclick = NextClickHide;
				};
		}
	}

}
