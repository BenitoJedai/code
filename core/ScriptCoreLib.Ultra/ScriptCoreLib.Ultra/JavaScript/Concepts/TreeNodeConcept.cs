﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.Concepts
{
	public interface ITreeNodeConcept : IUltraComponent
	{
		IHTMLDiv NodeContainer { get; set; }

		IHTMLDiv NodePrototype { get; set; }

		IHTMLAnchor ButtonArea { get; set; }

		IHTMLImage ButtonOpenActive { get; set; }
		IHTMLImage ButtonClosedActive { get; set; }
		IHTMLImage ButtonOpen { get; set; }
		IHTMLImage ButtonClosed { get; set; }
		IHTMLSpan ButtonChildless { get; set; }

		IHTMLAnchor SelectionArea { get; set; }

		IHTMLSpan IconArea { get; set; }

		IHTMLImage OpenImage { get; set; }
		IHTMLImage ClosedImage { get; set; }

		IHTMLSpan TextArea { get; set; }

		IHTMLDiv ChildArea { get; set; }
		IHTMLDiv ChildContainer { get; set; }
	}

	public class TreeNode
	{
		public ITreeNodeConcept Element { get; private set; }
		public Func<ITreeNodeConcept> Constructor { get; private set; }

		public TreeNode(Func<ITreeNodeConcept> Constructor)
		{
			this.Constructor = Constructor;

			this.Element = Constructor();

			this.Element.NodePrototype.style.border = "";
			this.Element.ChildContainer.style.border = "";

			this.Element.ButtonClosed.Hide();
			this.Element.ButtonOpenActive.Hide();
			this.Element.ButtonClosedActive.Hide();

			this.Element.ClosedImage.Hide();

			this.Element.SelectionArea.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Tahoma;
			this.Element.SelectionArea.style.color = JSColor.Black;
			this.Element.SelectionArea.style.textDecoration = "none";

			var SelectionArea = this.Element.SelectionArea;

			SelectionArea.ondblclick +=
				e =>
				{
					e.PreventDefault();

					IsExpanded = !IsExpanded;
				};

			SelectionArea.onfocus +=
				e =>
				{
					this.Element.TextArea.style.backgroundColor = JSColor.System.Highlight;
					this.Element.TextArea.style.color = JSColor.System.HighlightText;
				};


			SelectionArea.onblur +=
				e =>
				{
					this.Element.TextArea.style.backgroundColor = JSColor.None;
					this.Element.TextArea.style.color = JSColor.System.WindowText;
				};

			SelectionArea.onmouseout +=
				e =>
				{
					this.Element.TextArea.style.textDecoration = "none";

				};

			SelectionArea.onmouseover +=
				e =>
				{
					this.Element.TextArea.style.textDecoration = "underline";

				};
			SelectionArea.onclick +=
				e =>
				{
					SelectionArea.focus();

					e.PreventDefault();

					if (this.Click != null)
						this.Click();

					if (!this.IsExpanded)
						this.IsExpanded = true;
				};

			var ButtonArea = this.Element.ButtonArea;

			ButtonArea.onclick +=
				e =>
				{
					e.PreventDefault();

					IsExpanded = !IsExpanded;
				};

			ButtonArea.onmouseover +=
				e =>
				{


					IsActive = true;

					InternalUpdate();
				};

			ButtonArea.onmouseout +=
				e =>
				{
					IsActive = false;

					InternalUpdate();


				};

			InternalUpdate();
		}

		public bool IsActive { get; private set; }

		public string Text
		{
			get
			{
				return this.Element.TextArea.innerText;
			}
			set
			{
				this.Element.TextArea.innerText = value;
			}
		}

		public TreeNode Add(string Text)
		{
			var n = Add();

			n.Text = Text;

			return n;
		}

		public TreeNode Add(string Text, IHTMLImage Image)
		{
			var n = Add(Text);

			n.Element.OpenImage = Image;
			n.Element.OpenImage.border = 0;
			n.Element.ClosedImage = (IHTMLImage)Image.cloneNode(true);
			n.Element.ClosedImage.border = 0;
			n.InternalUpdate();

			return n;
		}

		readonly List<TreeNode> InternalChildren = new List<TreeNode>();

		public TreeNode Add()
		{
			var c = new TreeNode(this.Constructor);

			c.Element.NodeContainer.AttachTo(this.Element.ChildContainer);

			InternalChildren.Add(c);

			InternalUpdate();

			return c;
		}

		public IHTMLDiv Container
		{
			get
			{
				return this.Element.NodeContainer;
			}
		}

		// http://msdn.microsoft.com/en-us/library/system.windows.controls.treeviewitem.isexpanded.aspx

		public event Action Click;
		public event Action Collapsed;
		public event Action Expanded;

		bool InternalIsExpanded;
		public bool IsExpanded
		{
			get
			{
				return InternalIsExpanded;
			}
			set
			{
				if (InternalIsExpanded == value)
					return;

				InternalIsExpanded = value;

				InternalUpdate();

				if (InternalIsExpanded)
				{

					if (this.Expanded != null)
						this.Expanded();

				}
				else
				{

					if (this.Collapsed != null)
						this.Collapsed();
				}
			}
		}

		internal void InternalUpdate()
		{
			if (InternalIsExpanded)
			{
				InternalExpanded();


			}
			else
			{
				InternalCollapsed();

			}
		}

		private void InternalExpanded()
		{
			this.Element.ClosedImage.Hide();
			this.Element.OpenImage.Show();
			this.Element.ChildArea.Show();

			if (IsActive)
			{
				InternalPlaceholderOrButton(this.Element.ButtonOpenActive);
				this.Element.ButtonOpen.Hide();
			}
			else
			{
				this.Element.ButtonOpenActive.Hide();
				InternalPlaceholderOrButton(this.Element.ButtonOpen);
			}

			this.Element.ButtonClosedActive.Hide();
			this.Element.ButtonClosed.Hide();
		}

		private void InternalCollapsed()
		{
			this.Element.ClosedImage.Show();
			this.Element.OpenImage.Hide();
			this.Element.ChildArea.Hide();

			if (IsActive)
			{
				InternalPlaceholderOrButton(this.Element.ButtonClosedActive);
				this.Element.ButtonClosed.Hide();
			}
			else
			{
				this.Element.ButtonClosedActive.Hide();
				InternalPlaceholderOrButton(this.Element.ButtonClosed);
			}

			this.Element.ButtonOpenActive.Hide();
			this.Element.ButtonOpen.Hide();
		}

		private void InternalPlaceholderOrButton(IHTMLImage x)
		{
			if (this.InternalChildren.Any())
			{
				x.Show();
				this.Element.ButtonChildless.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.none;
			}
			else
			{

				x.Hide();
				this.Element.ButtonChildless.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.inline_block;
			}
		}

		public void WithIcon(Func<IHTMLImage> img)
		{
			this.Element.OpenImage = img();
			this.Element.ClosedImage = img();

			InternalUpdate();
		}
	}


	public static class TreeNodeExtensionsForXElement
	{
		public static TreeNode Visualize(this TreeNode t, XElement doc)
		{
			ApplyLocalName(t, doc);

			ApplyChildren(doc, t);

			return t;
		}

		private static XElement ApplyChildren(XElement doc, TreeNode t)
		{
			var q = Enumerable.ToArray(
				from n in doc.Elements()
				let nt = ApplyLocalName(t.Add(), n)
				let cc = ApplyChildren(n, nt)
				select nt
			);

			return doc;
		}

		private static TreeNode ApplyLocalName(TreeNode t, XElement cc)
		{
			t.IsExpanded = true;
			t.Element.TextArea.Clear();
			var c = new IHTMLCode();
			t.Element.TextArea.Add(c);

			t.Element.ButtonArea.Hide();
			t.Element.IconArea.Hide();

			Action<string, JSColor> Write =
				(Text, Color) =>
				{
					var cs = new IHTMLSpan { innerText = Text };

					cs.style.color = Color;

					cs.AttachTo(c);
				};

			Write("<", JSColor.Blue);
			Write(cc.Name.LocalName, JSColor.FromRGB(0xa0, 0, 0));

			foreach (var item in cc.Attributes().ToArray())
			{
				Write(" ", JSColor.None);

				Write("foo", JSColor.Red);
				Write("='", JSColor.Blue);
				Write(item.Value, JSColor.Blue);
				Write("'", JSColor.Blue);
			}

			if (!cc.Elements().Any())
			{
				Write(">", JSColor.Blue);

				Write(cc.Value, JSColor.None);

				Write("</", JSColor.Blue);
				Write(cc.Name.LocalName, JSColor.FromRGB(0xa0, 0, 0));
				Write(">", JSColor.Blue);

			}
			else
			{
				Write("/>", JSColor.Blue);
			}

			return t;
		}

	}
}
