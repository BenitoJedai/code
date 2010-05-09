using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Ultra.Studio;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.JavaScript.Components
{
	public class SolutionFileView
	{
		public readonly Dictionary<SolutionFileTextFragment, Color> Colors;

		public IHTMLDiv Container { get; private set; }

		public readonly IHTMLPre View;

		public SolutionFileView()
		{
			this.Colors = new Dictionary<SolutionFileTextFragment, Color>
			{
				{ SolutionFileTextFragment.Comment, Color.FromRGB(0, 0x80, 0) },
				{ SolutionFileTextFragment.Keyword, Color.Blue },

				{ SolutionFileTextFragment.None, Color.None},

				{ SolutionFileTextFragment.String, Color.FromRGB(0x80, 0, 0) },
				{ SolutionFileTextFragment.Type, Color.FromRGB(43, 145, 175) },

				{ SolutionFileTextFragment.XMLKeyword, Color.FromRGB(0, 0, 0xff) },
				{ SolutionFileTextFragment.XMLElement, Color.FromRGB(0x80, 0, 0) },
				{ SolutionFileTextFragment.XMLAttributeName, Color.FromRGB(0xff, 0, 0) },
				{ SolutionFileTextFragment.XMLAttributeValue, Color.FromRGB(0, 0, 0xff) },
				{ SolutionFileTextFragment.XMLComment, Color.FromRGB(0, 0x80, 0) },
				{ SolutionFileTextFragment.XMLText, Color.None},

			};

			this.Container = new IHTMLDiv();

			this.View = new IHTMLPre().AttachTo(this.Container);
			this.View.style.margin = "0";

			this.Container.style.overflow = IStyle.OverflowEnum.auto;
		}

		SolutionFile InternalFile;
		public SolutionFile File
		{
			set
			{
				InternalFile = value;
				this.View.Clear();
				RenderWriteHistory(this.Colors, value, this.View);
			}
			get
			{
				return InternalFile;
			}
		}

		public event Action<Uri> LinkCommentClick;

		private void RenderWriteHistory(Dictionary<SolutionFileTextFragment, Color> Lookup, SolutionFile f, IHTMLElement Container)
		{
			Func<SolutionFileTextFragment, Color> LookupOrDefault =
				ff =>
				{
					if (this.Colors.ContainsKey(ff))
						return this.Colors[ff];

					return this.Colors[SolutionFileTextFragment.None];
				};

			var Content = new IHTMLDiv().AttachTo(Container);


			Content.style.position = IStyle.PositionEnum.relative;

			var ViewBackground = new IHTMLDiv().AttachTo(Content);

			ViewBackground.style.position = IStyle.PositionEnum.absolute;
			ViewBackground.style.left = "0px";
			ViewBackground.style.top = "0px";
			ViewBackground.style.width = "4em";
			ViewBackground.style.borderRight = "1px dotted gray";
			ViewBackground.style.paddingRight = "0.5em";

			var ViewTrap = new IHTMLDiv().AttachTo(Content);

			ViewTrap.style.position = IStyle.PositionEnum.absolute;
			ViewTrap.style.left = "0px";
			ViewTrap.style.top = "0px";
			ViewTrap.style.right = "0px";
			ViewTrap.style.bottom = "0px";
			//ViewTrap.style.backgroundColor = Color.White;

			var ViewTrapContainer = new IHTMLDiv().AttachTo(ViewTrap);

			ViewTrapContainer.style.cursor = IStyle.CursorEnum.text;
			ViewTrapContainer.style.position = IStyle.PositionEnum.relative;
			ViewTrapContainer.style.paddingLeft = "5em";

			var View = new IHTMLDiv().AttachTo(ViewTrapContainer);



			var ContentHeightDummy = new IHTMLDiv().AttachTo(Content);

			var RegionStack = new Stack<List<Action<bool>>>();
			var RegionGlobal = new List<Action<bool>>();
			RegionStack.Push(RegionGlobal);

			var Lines = new List<IHTMLDiv>();

			var CurrentLineDirty = false;
			var CurrentLine = default(IHTMLDiv);
			var CurrentLineContent = default(IHTMLDiv);

			Action NextLine = delegate
			{
				CurrentLineDirty = false;

				var c = new IHTMLDiv();
				var cc = new IHTMLDiv();
				var cb = new IHTMLDiv();

				CurrentLine = c.AttachTo(View);
				CurrentLineContent = cc.AttachTo(c);

				var CurrentRegion = RegionStack.Peek();

				RegionStack.WithEach(
					k =>
					{
						k.Add(
							IsActive =>
							{
								// should we react when in a global region
								if (k == RegionGlobal)
									return;

								if (IsActive)
								{
									cc.style.backgroundColor = Color.FromGray(0xfc);
									cb.style.backgroundColor = Color.FromGray(0xfc);
								}
								else
								{
									cc.style.backgroundColor = Color.None;
									cb.style.backgroundColor = Color.None;
								}
							}
						);
					}
				);

				CurrentLine.onmouseover +=
					delegate
					{
						CurrentRegion.Invoke(true);
						cc.style.backgroundColor = Color.FromGray(0xf7);
						cb.style.backgroundColor = Color.FromGray(0xf7);
					};

				CurrentLine.onmouseout +=
					delegate
					{
						CurrentRegion.Invoke(false);
						cc.style.backgroundColor = Color.None;
						cb.style.backgroundColor = Color.None;
					};


				Lines.Add(CurrentLine);
				//CurrentLineContent.style.marginLeft = "5em";

				var BCurrentLine = cb.AttachTo(ViewBackground);

				BCurrentLine.style.textAlign = IStyle.TextAlignEnum.right;

				var span = new IHTMLCode { innerText = "" + Lines.Count };
				span.style.color = Lookup[SolutionFileTextFragment.Type];
				span.AttachTo(BCurrentLine);

				//Content.style.height = Lines.Count + "em";

				new IHTMLDiv { new IHTMLCode { innerText = Environment.NewLine } }.AttachTo(ContentHeightDummy);
			};



			foreach (var item in f.WriteHistory.ToArray())
			{
				if (item is SolutionFileWriteArguments.BeginRegion)
				{
					RegionStack.Push(new List<Action<bool>>());
				}

				if (item is SolutionFileWriteArguments.EndRegion)
				{
					RegionStack.Pop();
				}

				if (CurrentLine == null)
					NextLine();



				var innerText = item.Text;
				innerText = innerText.TakeUntilLastIfAny(Environment.NewLine);

				if (!string.IsNullOrEmpty(innerText))
				{
					var span = new IHTMLCode { innerText = innerText };

					if (innerText == "\t")
					{
						span.innerText = " ";
						span.style.width = "2em";
						span.style.display = IStyle.DisplayEnum.inline_block;
					}


					span.style.color = LookupOrDefault(item.Fragment);

					CurrentLineDirty = true;
					span.AttachTo(CurrentLineContent);

					if (item.Tag != null)
					{
						span.style.cursor = ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.pointer;
						span.onmouseover +=
							delegate
							{
								span.style.textDecoration = "underline";
							};

						span.onmouseout +=
							delegate
							{
								span.style.textDecoration = "";
							};



						var Type = item.Tag as SolutionProjectLanguageType;
						if (Type != null)
						{
							span.title = Type.FullName;
						}

						var Method = item.Tag as SolutionProjectLanguageMethod;
						if (Method != null)
						{
							span.title = Method.Name;
						}


						var Uri = item.Tag as Uri;
						if (Uri != null)
						{
							var a = new IHTMLAnchor();
							a.style.color = LookupOrDefault(item.Fragment);

							a.href = Uri.ToString();
							a.target = "_blank";
							a.Add(span);
							a.AttachTo(CurrentLineContent);

							a.onclick +=
								e =>
								{
									e.PreventDefault();

									if (LinkCommentClick != null)
										LinkCommentClick(Uri);
								};
						}
					}
				}

				if (item.Text.EndsWith(Environment.NewLine))
				{
					if (!CurrentLineDirty)
					{
						var span = new IHTMLCode { innerText = " " };
						span.AttachTo(CurrentLineContent);
					}

					CurrentLine = null;
				}
			}
		}

	}
}
