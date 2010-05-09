using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.Ultra.Components.HTML.Images.SpriteSheet.FromAssets;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Ultra.Documentation;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.JavaScript.Controls
{
	[Description("ScriptCoreLib.Documentation. Write javascript, flash and java applets within a C# project.")]
	public partial class DocumentationCompilationViewer
	{

		public DocumentationCompilationViewer()
		{
			var Split = new HorizontalSplit();

		

			Split.Container.AttachToDocument();

		




			//var infocontent = new Lorem();
			//infocontent.Container.AttachTo(hs.RightContainer);

			//{
			var Section1 = new Section
			{

			}.ToSectionConcept("Summary");

			Section1.Target.Container.AttachTo(Split.RightContainer);
			//}

			//{
			//    var Section1 = new Section
			//    {

			//    }.ToSectionConcept("Syntax");

			//    Section1.Target.Container.AttachTo(infocontent.Sections);
			//}

			//{
			//    var Section1 = new Section
			//    {

			//    }.ToSectionConcept("Remarks");

			//    Section1.Target.Container.AttachTo(infocontent.Sections);
			//}


			//AttachLogoAnimation(infocontent);




			var c = new Compilation();

			RenderArchives(c, Split.LeftContainer,

				n => Section1.Content.innerText = n

			);




		}


		private void RenderArchives(Compilation c, IHTMLElement parent, Action<string> UpdateLocation)
		{
			var AllTypes = default(IHTMLDiv);

			var AllTypesNamespaceLookup = new Dictionary<string, IHTMLDiv>();

			var GetAllTypesNamespaceContainer = default(Func<string, IHTMLDiv>);

			GetAllTypesNamespaceContainer =
				(Namespace) =>
				{
					var ParentNamespace = Namespace.TakeUntilLastIfAny(".");

					var ParentContainer = AllTypes;

					if (ParentNamespace != Namespace)
					{
						ParentContainer = GetAllTypesNamespaceContainer(ParentNamespace);
					}

					if (!AllTypesNamespaceLookup.ContainsKey(Namespace))
					{
						AllTypesNamespaceLookup[Namespace] = AddNamespace(ParentContainer, null, Namespace.SkipUntilLastIfAny("."), UpdateLocation);
					}

					return AllTypesNamespaceLookup[Namespace];
				};

			{
				var tr = new TreeNode(() => new VistaTreeNodePage());

				tr.Text = "Class Viewer";
				tr.Element.ClosedImage = new ClassViewer();
				tr.Container.AttachTo(parent);

				var div = new IHTMLDiv().AttachTo(parent);

				div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;


				var i = new References().AttachTo(div);

				i.style.verticalAlign = "middle";
				i.style.marginRight = "0.5em";

				new IHTMLSpan { innerText = "All Types" }.AttachTo(div);

				var children = new IHTMLDiv().AttachTo(parent);

				children.style.paddingLeft = "1em";

				AllTypes = children;
			}

			var LoadActionList = new List<Action<Action>>();

			foreach (var item in c.GetArchives().ToArray())
			{
				var div = new IHTMLDiv().AttachTo(parent);

				div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;


				var i = new References().AttachTo(div);

				i.style.verticalAlign = "middle";
				i.style.marginRight = "0.5em";

				new IHTMLSpan { innerText = item.Name }.AttachTo(div);

				var children = new IHTMLDiv().AttachTo(parent);

				children.style.paddingLeft = "1em";


				RenderAssemblies(item, children, GetAllTypesNamespaceContainer, UpdateLocation, LoadActionList.Add);
			}


			LoadActionList.ForEach(
				(Current, Next) =>
				{
					Current(Next);
				}
			);
		}

		private void RenderAssemblies(
			CompilationArchiveBase archive,
			IHTMLElement parent,
			Func<string, IHTMLDiv> AllTypes,
			Action<string> UpdateLocation,
			Action<Action<Action>> YieldLoadAction)
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


				var i = new Assembly().AttachTo(div);

				i.style.verticalAlign = "middle";
				i.style.marginRight = "0.5em";

				var s = new IHTMLAnchor { innerText = item2.Name }.AttachTo(div);

				//s.style.color = JSColor.Gray;

				s.href = "#";
				s.style.textDecoration = "none";
				s.style.color = JSColor.System.GrayText;

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
							NamespaceLookup[SourceType.Namespace] = null;

							var NextNamespaceOrDefault = default(IHTMLDiv);

							//var NextNamespaceOrDefault = NamespaceLookup.Keys.OrderBy(k => k).SkipWhile(k => k == SourceType.Namespace).Select(k => NamespaceLookup[k]).FirstOrDefault();

							NamespaceLookup[SourceType.Namespace] = AddNamespace(Container, NextNamespaceOrDefault, SourceType.Namespace, UpdateLocation);
						}

						return NamespaceLookup[SourceType.Namespace];
					};


				var children = new IHTMLDiv().AttachTo(div);

				children.style.paddingLeft = "1em";
				Action<Action> LoadAction =
					done =>
					{
						s.style.color = JSColor.System.Highlight;

						Action done_ = delegate
						{
							done();
						};



						item.WhenReady(
							a =>
							{
								s.style.color = JSColor.System.WindowText;


								a.GetTypes().OrderBy(k => k.Name).ForEach(
									(Current, Index, Next) =>
									{
										if (!Current.IsNested)
										{
											AddType(
												GetNamespaceContainer(children, Current),
												Current,
												UpdateLocation
											);

											AddType(
												AllTypes(Current.Namespace),
												Current,
												UpdateLocation
											);
										}


										if (Index % 8 == 0)
										{
											ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
												7,
												Next
											);
										}
										else
										{
											Next();
										}
									}
								)(done_);
							}
						);
					};

				YieldLoadAction(LoadAction);


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


				NextClickHide();
			}
		}

		private static IHTMLDiv AddNamespace(IHTMLDiv parent, IHTMLDiv NextNamespaceOrDefault, string Namespace, Action<string> UpdateLocation)
		{
			var div = new IHTMLDiv();

			if (NextNamespaceOrDefault == null)
				div.AttachTo(parent);
			else
				NextNamespaceOrDefault.insertPreviousSibling(div);

			div.style.marginTop = "0.1em";
			div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


			var i = new Namespace().AttachTo(div);

			i.style.verticalAlign = "middle";
			i.style.marginRight = "0.5em";

			if (Namespace == "")
				Namespace = "<Module>";

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
				i = new PublicInterface();
			}
			else
			{
				i = new PublicClass();
			}

			i.AttachTo(div);

			i.style.verticalAlign = "middle";
			i.style.marginRight = "0.5em";

			var s = new IHTMLAnchor { innerText = type.Name, title = "" + type.MetadataToken }.AttachTo(div);

			if (!string.IsNullOrEmpty(type.HTMLElement))
			{
				var c = new IHTMLCode();

				Action<string, JSColor> Write =
					(Text, Color) =>
					{
						var cs = new IHTMLSpan { innerText = Text };

						cs.style.color = Color;

						cs.AttachTo(c);
					};

				Write("<", JSColor.Blue);
				Write(type.HTMLElement, JSColor.FromRGB(0xa0, 0, 0));
				Write("/>", JSColor.Blue);

				//c.style.marginLeft = "1em";
				c.style.Float = ScriptCoreLib.JavaScript.DOM.IStyle.FloatEnum.right;

				c.AttachTo(s);
			}

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

					UpdateLocation(type.FullName + " - " + type.Summary + " - HTML:" + type.HTMLElement);

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

					Func<IHTMLDiv> Group = () => new IHTMLDiv().AttachTo(children);

					var Groups = new
					{
						Nested = Group(),
						Constructors = Group(),
						Methods = Group(),
						Events = Group(),
						Fields = Group(),
						Properties = Group(),
					};


					type.GetNestedTypes().ForEach(
						(Current, Next) =>
						{
							AddType(Groups.Nested, Current, UpdateLocation);

							ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
								50,
								Next
							);
						}
					);

					type.GetConstructors().ForEach(
						(Current, Next) =>
						{
							AddTypeConstructor(Groups.Constructors, Current, UpdateLocation);

							ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
								50,
								Next
							);
						}
					);

					var HiddenMethods = new List<int>();

					Action<CompilationMethod> AddIfAny =
						SourceMethod =>
						{
							if (SourceMethod == null)
								return;

							HiddenMethods.Add(SourceMethod.MetadataToken);
						};

					Action AfterEvents = delegate
					{

						type.GetMethods().ForEach(
							(Current, Next) =>
							{
								if (!HiddenMethods.Contains(Current.MetadataToken))
								{
									AddTypeMethod(Groups.Methods, Current, UpdateLocation);
								}

								ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
									50,
									Next
								);
							}
						);

					};

					Action AfterProperties = delegate
					{
						type.GetEvents().ForEach(
							(Current, Next) =>
							{
								AddIfAny(Current.GetAddMethod());
								AddIfAny(Current.GetRemoveMethod());

								AddTypeEvent(Groups.Events, Current, UpdateLocation);

								ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
									50,
									Next
								);
							}
						)(AfterEvents);
					};

					type.GetProperties().ForEach(
						(Current, Next) =>
						{
							AddIfAny(Current.GetSetMethod());
							AddIfAny(Current.GetGetMethod());

							AddTypeProperty(Groups.Properties, Current, UpdateLocation);

							ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.AtDelay(
								50,
								Next
							);
						}
					)(AfterProperties);






					type.GetFields().ForEach(
						(Current, Next) =>
						{
							AddTypeField(Groups.Fields, Current, UpdateLocation);

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

		private static void AddTypeField(
			IHTMLDiv parent,
			CompilationField type,
			Action<string> UpdateLocation
			)
		{
			var div = new IHTMLDiv().AttachTo(parent);

			div.style.marginTop = "0.1em";
			div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


			var i = new PublicField().AttachTo(div);

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

		private static void AddTypeEvent(
		IHTMLDiv parent,
		CompilationEvent type,
		Action<string> UpdateLocation
		)
		{
			var div = new IHTMLDiv().AttachTo(parent);

			div.style.marginTop = "0.1em";
			div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


			var i = new PublicEvent().AttachTo(div);

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
					var children = new IHTMLDiv().AttachTo(div);

					children.style.paddingLeft = "2em";

					AddTypeMethod(children, type.GetAddMethod(), UpdateLocation);
					AddTypeMethod(children, type.GetRemoveMethod(), UpdateLocation);



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


		private static void AddTypeProperty(
	IHTMLDiv parent,
	CompilationProperty type,
	Action<string> UpdateLocation
	)
		{
			var div = new IHTMLDiv().AttachTo(parent);

			div.style.marginTop = "0.1em";
			div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


			var i = new PublicProperty().AttachTo(div);

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
					var children = new IHTMLDiv().AttachTo(div);

					children.style.paddingLeft = "2em";

					AddTypeMethod(children, type.GetGetMethod(), UpdateLocation);
					AddTypeMethod(children, type.GetSetMethod(), UpdateLocation);


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
		private static void AddTypeConstructor(IHTMLDiv parent, CompilationConstructor type, Action<string> UpdateLocation)
		{
			var div = new IHTMLDiv().AttachTo(parent);

			div.style.marginTop = "0.1em";
			div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


			var i = new PublicConstructor().AttachTo(div);

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
			if (type == null)
				return;

			var div = new IHTMLDiv().AttachTo(parent);

			div.style.marginTop = "0.1em";
			div.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			div.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;


			var i = new PublicMethod().AttachTo(div);

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
