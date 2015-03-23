using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ScriptCoreLib.JavaScript.DOM.SVG;


namespace ScriptCoreLib.JavaScript.DOM.HTML
{
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLElement.idl
	// https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/Mono.WebBrowser/Mono.Mozilla/DOM/HTMLElement.cs
	// https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/Mono.WebBrowser/Mono.WebBrowser/DOM/IElement.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/Html/Microsoft/LiveLabs/Html/HtmlElement.cs

	// X:\opensource\codeplex\htmlagilitypack\HtmlAgilityPack\HtmlNode.cs

	[Script]
	// move to own file
	public static class HTMLElementEnumExtensions
	{
		// script: error JSC1000: No implementation found for this native method, please implement [static ScriptCoreLib.JavaScript.DOM.HTML.HTMLElementEnumExtensions.AsEnumerable(ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement+HTMLElementEnum)]

		//public static IHTMLElement[] querySelectorAll(this ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum selectors)
		// jsc eXperience naming
		public static IEnumerable<IHTMLElement> AsEnumerable(this ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum selectors)
		{
			var s = selectors;

			// X:\jsc.svn\examples\javascript\Test\RemoveByQuerySelectorAll\RemoveByQuerySelectorAll\Application.cs
			#region fixup
			if ((int)selectors == (int)ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum.button)
				s = ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum.button;

			if ((int)selectors == (int)ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum.p)
				s = ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum.p;
			#endregion

			var a = System.Linq.Enumerable.AsEnumerable(
				Native.document.documentElement.querySelectorAll(s)
			);

			return a;
		}

		public static IEnumerable<T> Select<T>(this ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum className, Func<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement, T> selector)
		{
			// X:\jsc.svn\examples\javascript\Test\RemoveByQuerySelectorAll\RemoveByQuerySelectorAll\Application.cs



			return System.Linq.Enumerable.Select(className.AsEnumerable(), selector);
		}

		public static IEnumerable<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement> Where(this ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum className, Func<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement, bool> f)
		{
			// Error	2	The type of the expression in the where clause is incorrect.  Type inference failed in the call to 'Where'.	X:\jsc.svn\examples\javascript\Test\RemoveByQuerySelectorAll\RemoveByQuerySelectorAll\Application.cs	51	25	RemoveByQuerySelectorAll
			// X:\jsc.svn\examples\javascript\Test\RemoveByQuerySelectorAll\RemoveByQuerySelectorAll\Application.cs



			//            ---------------------------
			//Asset Compiler
			//---------------------------
			//The Asset Compiler has found a few issues while preparing the assets! 

			//Method 'InternalAsNode' in type 'ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets.JSCSolutionsNETImageOnWhite' from assembly 'ScriptCoreLib.Ultra.Components, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.

			//Please fix the issues and try again!
			//You may need to reconnect your external drive.

			//X:\jsc.svn\examples\javascript\Test\RemoveByQuerySelectorAll\RemoveByQuerySelectorAll\RemoveByQuerySelectorAll.csproj
			//---------------------------
			//OK   
			//---------------------------



			return System.Linq.Enumerable.Where(className.AsEnumerable(), f);
		}

	}


	[Obsolete("experimental")]
	[Script(InternalConstructor = true)]
	public /* abstract */ partial class IHTMLElement<TTargetElement> : IHTMLElement where TTargetElement : IHTMLElement
	{
		// X:\jsc.svn\examples\javascript\linq\WebSQLXElement\WebSQLXElement\Application.cs

		// X:\jsc.svn\examples\javascript\Test\TestTemplateElement\TestTemplateElement\Application.cs
		//public T cloneNode(bool deep) { return default(T); }

		// how many experiments do we have already?
		[System.Obsolete("experimental like .css")]
		public ShadowRoot<TTargetElement> shadow
		{
			// https://www.youtube.com/watch?v=Is4FZxKGqqk

			[method: Script(DefineAsStatic = true)]
			get
			{
				// X:\jsc.svn\examples\javascript\Test\TestShadowRootHost\TestShadowRootHost\Application.cs

				var s = this.shadowRoot;

				if (s != null)
					return (ShadowRoot<TTargetElement>)(object)s;


				return (ShadowRoot<TTargetElement>)(object)this.createShadowRoot();
			}
		}

		#region event onclick
		public event System.Action<IEvent<TTargetElement>> onclick
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "click");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "click");
			}
		}
		#endregion

		#region event onmousedown
		public event System.Action<IMouseDownEvent<TTargetElement>> onmousedown
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "mousedown");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "mousedown");
			}
		}
		#endregion

		#region event onchange
		public event System.Action<IEvent<TTargetElement>> onchange
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "change");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "change");
			}
		}
		#endregion
	}


	// http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/html/nsIDOMHTMLElement.idl
	[Script(InternalConstructor = true)]
	public /* abstract */ partial class IHTMLElement :
		IElement,

		// Error	17	'ScriptCoreLib.JavaScript.DOM.INode' does not implement interface member 'ScriptCoreLib.JavaScript.Extensions.INodeConvertible<ScriptCoreLib.JavaScript.DOM.INode>.ToNode()'	X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\INode.cs	14	15	ScriptCoreLib
		// circular ref?
		INodeConvertible<IHTMLElement>
	{
		// X:\jsc.svn\examples\javascript\Test\TestOwnerDocumentDefaultView\TestOwnerDocumentDefaultView\Application.cs
		public readonly IHTMLDocument ownerDocument;

		// might be DocumentFragment, ShadowRoot
		//public new IHTMLElement parentNode;

		[Script(DefineAsStatic = true)]
		IHTMLElement INodeConvertible<IHTMLElement>.InternalAsNode()
		{
			// cannot call this yet via interface invoke!

			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/20130720
			return this;
		}

		[Script(DefineAsStatic = true)]
		public void appendChild<T>(INodeConvertible<T> child)
			where T : IHTMLElement
		{
			appendChild((INode)child.AsNode());
		}




		// element is like exception. its a base class. not ot be created. not to be thrown.

		public string id;


		public string name
		{
			[Script(DefineAsStatic = true)]
			get
			{
				return (string)this.getAttribute("name");
			}
			[Script(DefineAsStatic = true)]
			set
			{
				this.setAttribute("name", value);
			}
		}

		public int tabIndex;

		//public bool contentEditable;


		// rename, renamespace ?
		[Script(IsStringEnum = true)]
		public enum HTMLElementEnum
		{
			a,
			abbr,
			acronym,
			address,
			applet,
			area,
			audio,
			b,
			@base,
			basefont,
			bdo,
			big,
			blockquote,
			body,
			br,
			button,

			canvas,
			caption,
			center,
			cite,
			code,
			col,
			colgroup,
			content,

			dd,
			del,
			dfn,
			dir,
			div,
			dl,
			dt,
			em,
			embed,
			fieldset,
			font,
			form,
			frame,
			frameset,
			head,
			h1,
			h2,
			h3,
			h4,
			h5,
			h6,
			hr,
			html,
			i,
			iframe,
			img,
			input,
			ins,
			kbd,
			label,
			legend,
			li,
			link,
			map,
			marquee,
			menu,
			meta,
			noframes,
			noscript,
			@object,
			ol,
			optgroup,
			option,
			output,
			p,
			param,
			pre,
			q,
			s,

			samp,
			script,
			select,
			small,
			span,
			strike,
			strong,
			style,
			sub,
			sup,
			shadow,

			table,
			tbody,
			td,

			textarea,
			template,

			tfoot,
			th,
			thead,
			title,
			tr,
			tt,
			u,
			ul,
			@var,
			video
		}

		#region constructors
		/// <summary>
		/// creates new DIV tag
		/// </summary>
		public IHTMLElement() { }
		public IHTMLElement(string tag)
		{
			// must keep it empty?
			//throw new System.NotImplementedException();
		}
		public IHTMLElement(HTMLElementEnum tag) { }
		public IHTMLElement(HTMLElementEnum tag, IHTMLDocument doc) { }
		public IHTMLElement(HTMLElementEnum tag, string text) { }
		public IHTMLElement(params INode[] children) { }
		public IHTMLElement(HTMLElementEnum tag, params INode[] children) { }

		internal static IHTMLElement InternalConstructor()
		{
			return InternalConstructor(null, null, null);
		}

		internal static IHTMLElement InternalConstructor(string tag)
		{
			return Native.Document.createElement(tag);
		}

		internal static IHTMLElement InternalConstructor(HTMLElementEnum tag)
		{
			return InternalConstructor(tag, null, null);
		}

		internal static IHTMLElement InternalConstructor(HTMLElementEnum tag, string text)
		{
			return InternalConstructor(tag, text, null);
		}

		internal static IHTMLElement InternalConstructor(HTMLElementEnum tag, IHTMLDocument doc)
		{
			if (doc == null)
				doc = Native.Document;

			// jsc should really support enum.ToString!
			IHTMLElement c = (IHTMLElement)doc.createElement(tag);


			return c;
		}

		internal static IHTMLElement InternalConstructor(HTMLElementEnum tag, string text, IHTMLDocument doc)
		{
			IHTMLElement c = InternalConstructor(tag, doc);

			if (text != null)
				c.appendChild(new ITextNode(text));

			return c;
		}

		internal static IHTMLElement InternalConstructor(params INode[] children)
		{
			return InternalConstructor(HTMLElementEnum.div, children);
		}

		internal static IHTMLElement InternalConstructor(HTMLElementEnum tag, params INode[] children)
		{
			IHTMLElement n = InternalConstructor(tag, null, null);

			n.appendChild(children);

			return n;
		}

		#endregion



		#region innerText
		public string innerText
		{
			[Script(DefineAsStatic = true)]
			get
			{
				if (this.childNodes.Length == 1)
				{

					if (this.childNodes[0].nodeType == NodeTypeEnum.TextNode)
					{
						return ((ITextNode)this.childNodes[0]).nodeValue;
					}
				}

				return "";
			}
			[Script(DefineAsStatic = true)]
			set
			{
				// what if the caller is from Web Worker?
				// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130825

				ITextNode n = null;

				if (this.childNodes.Length == 0)
				{
					n = new ITextNode(this.ownerDocument);

					this.appendChild(n);
				}
				else if (this.childNodes.Length == 1)
				{
					if (this.childNodes[0].nodeType == NodeTypeEnum.TextNode)
					{
						n = (ITextNode)this.childNodes[0];
					}
					else
					{
						this.removeChildren();

						n = new ITextNode(this.ownerDocument);

						this.appendChild(n);
					}
				}
				else
				{
					this.removeChildren();

					n = new ITextNode(this.ownerDocument);

					this.appendChild(n);
				}



				n.nodeValue = value;
			}
		}
		#endregion


		public string innerHTML;


		public string title;



		[Obsolete("do we use it, what about .css?")]
		[Script(DefineAsStatic = true)]
		public static implicit operator IStyle(IHTMLElement e)
		{
			return e.style;
		}



		public int height;
		public int width;

		// this is special
		public string className;

		// see also
		// InternalGetExplicitRuleSelector
		// what spec defines this?
		public string[] classList;

		// what spec defines this?
		public string localName;

		public readonly int offsetLeft;
		public readonly int offsetTop;

		public readonly int offsetWidth;
		public readonly int offsetHeight;

		public readonly int clientWidth;
		public readonly int clientHeight;

		public double aspect
		{
			// used by THREE.PerspectiveCamera
			[Script(DefineAsStatic = true)]
			get
			{
				// X:\jsc.svn\examples\javascript\WebGL\WebGLOBJExperiment\WebGLOBJExperiment\Application.cs
				// X:\jsc.svn\examples\javascript\WebGL\WebGLColladaExperiment\WebGLColladaExperiment\Application.cs

				return clientWidth / (double)clientHeight;
			}
		}

		public int scrollLeft;
		public int scrollTop;

		public int scrollBottom
		{
			[Script(DefineAsStatic = true)]
			get
			{
				// X:\jsc.svn\examples\javascript\CSS\CSSConditionalScroll\CSSConditionalScroll\Application.cs

				// body seems to be special
				if (this.localName == "body")
					return this.scrollHeight - ((IHTMLElement)this.parentNode).clientHeight - this.scrollTop;

				return this.scrollHeight - this.clientHeight - this.scrollTop;
			}
		}

		public readonly int scrollWidth;
		public readonly int scrollHeight;

		public void select()
		{

		}

		public void blur()
		{

		}

		public void focus()
		{

		}

		// part of ui automation effort
		[Script(DefineAsStatic = true)]
		public void click()
		{
			// tested by
			// X:\jsc.svn\examples\javascript\DragDataTableIntoCSVFile\DragDataTableIntoCSVFile\Application.cs

			//Click to work with every HTML element
			//Tested by E:\jsc.svn\examples\javascript\Test\TestHtmlClickInBrowsers\TestHtmlClickInBrowsers

			var e = new IMouseEvent();

			// 0:17079ms onclick: { href = http://192.168.1.101:11576/#foo, MouseButton = 2, Left = 1 } 
			// https://bugzilla.mozilla.org/show_bug.cgi?id=395917
			e.initMouseEvent(
				"click",
				true,
				true,
				Native.window,
				0, 0,
				0, 0,
				0,
				false,
				false,
				false,
				false,
				buttonArg: 0,
				relatedTargetArg: this
				);


			this.dispatchEvent(e);

			//            // http://www.w3.org/TR/DOM-Level-2-Events/events.html#Events-MouseEvent
			//            new IFunction("e", @"
			//
			//                        // # First create an event
			//                        var click_ev = document.createEvent('MouseEvent');
			//
			//                        // https://developer.mozilla.org/en-US/docs/Web/API/event.initMouseEvent
			//                        // # initialize the event
			//                        click_ev.initEvent('click', true /* bubble */, true /* cancelable */);
			//                        click_ev.button = 1;
			//
			//                        // # trigger the evevnt
			//                        this.dispatchEvent(click_ev);
			//
			//                        ").apply(this);
		}



		/*
        public static void Show(bool bVisible, params IHTMLElement[] e)
        {
            foreach (IHTMLElement v in e)
            {
                Extensions.Extensions.Show(v, bVisible);
            }
        }
        */


		[Obsolete]
		[Script(DefineAsStatic = true)]
		public void SetCenteredLocation(Point p)
		{
			SetCenteredLocation(p.X, p.Y);
		}

		[Obsolete]
		[Script(DefineAsStatic = true)]
		public void SetCenteredLocation(int x, int y)
		{
			this.style.position = IStyle.PositionEnum.absolute;
			this.style.SetLocation(x - this.clientWidth / 2, y - this.clientHeight / 2);
		}

		// X:\jsc.svn\examples\javascript\css\CSSAnimationEvents\CSSAnimationEvents\Application.cs

		// tested by ?

		#region event onanimationstart
		public event System.Action<IEvent> onanimationstart
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "animationstart");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "animationstart");
			}
		}
		#endregion

		#region event onanimationend
		public event System.Action<IEvent> onanimationend
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "animationend");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "animationend");
			}
		}
		#endregion

		#region event onanimationiteration
		public event System.Action<IEvent> onanimationiteration
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "animationiteration");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "animationiteration");
			}
		}
		#endregion


		#region event onclick
		public event System.Action<IEvent> onclick
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "click");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "click");
			}
		}
		#endregion


		#region event ondblclick
		public event System.Action<IEvent> ondblclick
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "dblclick");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "dblclick");
			}
		}
		#endregion
		#region event onmouseover
		public event System.Action<IEvent> onmouseover
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "mouseover");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "mouseover");
			}
		}
		#endregion
		#region event onmouseout
		public event System.Action<IEvent> onmouseout
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "mouseout");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "mouseout");
			}
		}
		#endregion
		#region event onmousedown
		public event System.Action<IMouseDownEvent<IHTMLElement>> onmousedown
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "mousedown");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "mousedown");
			}
		}
		#endregion
		#region event onmouseup
		public event System.Action<IEvent> onmouseup
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "mouseup");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "mouseup");
			}
		}
		#endregion

		#region event onmousemove
		public event System.Action<IEvent> onmousemove
		{
			// tested by?
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "mousemove");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "mousemove");
			}
		}
		#endregion

		#region event onmousemove
		public event System.Action<IEvent> onmousewheel
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value,
					new IEventTarget.EventNames
					{
						Event = "onmousewheel",
						EventListener = "DOMMouseScroll",
						EventListenerAlt = "mousewheel"
					}
				);

			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value,
					new IEventTarget.EventNames
					{
						Event = "onmousewheel",
						EventListener = "DOMMouseScroll",
						EventListenerAlt = "mousewheel"
					}
				);
			}
		}
		#endregion

		#region event oncontextmenu
		public event System.Action<IEvent> oncontextmenu
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "contextmenu");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "contextmenu");
			}
		}
		#endregion


		#region event onselectstart
		public event System.Action<IEvent> onselectstart
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "selectstart");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "selectstart");
			}
		}
		#endregion



		// http://zya.github.io/scrollsound/
		#region event onscroll
		public event System.Action<IEvent> onscroll
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "scroll");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "scroll");
			}
		}
		#endregion


		#region event onresize
		public event System.Action<IEvent> onresize
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "resize");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "resize");
			}
		}
		#endregion

		// http://dev.w3.org/html5/spec/dnd.html#event-dragstart
		public bool draggable;

		// tested by?
		// X:\jsc.svn\examples\javascript\Test\TestDragStart\TestDragStart\Application.cs

		#region event ondragstart
		public event System.Action<DragEvent> ondragstart
		{
			[Script(DefineAsStatic = true)]
			add
			{
				this.draggable = true;
				base.InternalEvent(true, value, "dragstart");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "dragstart");
			}
		}
		#endregion

		#region event ondragover
		public event System.Action<DragEvent> ondragover
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "dragover");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "dragover");
			}
		}
		#endregion

		#region event ondragleave
		public event System.Action<DragEvent> ondragleave
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "dragleave");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "dragleave");
			}
		}
		#endregion

		#region event ondrop
		public event System.Action<DragEvent> ondrop
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "drop");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "drop");
			}
		}
		#endregion
		#region event ondragdrop
		public event System.Action<IEvent> ondragdrop
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "dragdrop");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "dragdrop");
			}
		}
		#endregion



		#region event onchange
		public event System.Action<IEvent> onchange
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "change");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "change");
			}
		}
		#endregion




		#region event onfocus
		public event System.Action<IEvent> onfocus
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "focus");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "focus");
			}
		}
		#endregion



		#region event onblur
		public event System.Action<IEvent> onblur
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "blur");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "blur");
			}
		}
		#endregion


		#region event onkeypress
		public event System.Action<IEvent> onkeypress
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "keypress");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "keypress");
			}
		}
		#endregion



		#region event onkeyup
		public event System.Action<IEvent> onkeyup
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "keyup");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "keyup");
			}
		}
		#endregion



		#region event onkeydown
		public event System.Action<IEvent> onkeydown
		{
			[Script(DefineAsStatic = true)]
			add
			{
				base.InternalEvent(true, value, "keydown");
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				base.InternalEvent(false, value, "keydown");
			}
		}
		#endregion



		// https://developer.android.com/guide/webapps/migrating.html
		// touchcancel
		// https://developer.mozilla.org/en-US/docs/Web/Events/touchcancel


		#region event ontouchstart
		public event System.Action<TouchEvent> ontouchstart
		{
			[Script(DefineAsStatic = true)]
			add
			{
				InternalEnableMultitouch();
				this.addEventListener("MozTouchDown", value, false);
				this.addEventListener("touchstart", value, false);
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				this.removeEventListener("touchstart", value, false);
			}
		}
		#endregion

		#region event ontouchmove
		public event System.Action<TouchEvent> ontouchmove
		{
			[Script(DefineAsStatic = true)]
			add
			{
				InternalEnableMultitouch();
				// http://support.mozilla.org/en-US/questions/810808
				// https://developer.mozilla.org/en-US/docs/DOM/Touch_events_(Mozilla_experimental)
				this.addEventListener("MozTouchMove", value, false);
				this.addEventListener("touchmove", value, false);

			}
			[Script(DefineAsStatic = true)]
			remove
			{
				this.removeEventListener("touchmove", value, false);
			}
		}

		private static void InternalEnableMultitouch()
		{
			Native.Document.multitouchData = true;
		}
		#endregion

		#region event ontouchend
		public event System.Action<TouchEvent> ontouchend
		{
			[Script(DefineAsStatic = true)]
			add
			{
				InternalEnableMultitouch();
				this.addEventListener("MozTouchUp", value, false);
				this.addEventListener("touchend", value, false);
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				this.removeEventListener("touchend", value, false);
			}
		}
		#endregion


		#region event onpointerlockchange
		public event System.Action<TouchEvent> onpointerlockchange
		{
			[Script(DefineAsStatic = true)]
			add
			{
				this.addEventListener("pointerlockchange", value, false);
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				this.removeEventListener("pointerlockchange", value, false);
			}
		}
		#endregion

		[Obsolete("documentation shall also mention radio button groups and .css")]
		public static int NextID = 0;

		[Script(DefineAsStatic = true)]
		public void EnsureID()
		{
			if (this.id == "")
				this.id += "$" + NextID++;
		}


		[Script(DefineAsStatic = true)]
		public void ScrollToBottom()
		{
			this.scrollTop = this.scrollHeight - this.clientHeight;

		}

		[Script(DefineAsStatic = true)]
		public void removeChildren()
		{
			while (this.firstChild != null)
				this.removeChild(this.firstChild);
		}

		[Script(DefineAsStatic = true)]
		public void FadeOut()
		{
			Fader.FadeOut(this);
		}

		[Script(DefineAsStatic = true)]
		public void replaceChildrenWith(string e)
		{
			removeChildren();
			appendChild(e);
		}

		/// <summary>
		/// user cannot select text, does not work with img in IE
		/// </summary>
		[Script(DefineAsStatic = true)]
		public void DisableSelection()
		{
			this.onmousedown += Native.DisabledEventHandler;
			this.onselectstart += Native.DisabledEventHandler;

		}

		[Script(DefineAsStatic = true)]
		public void EnableSelection()
		{
			this.onmousedown -= Native.DisabledEventHandler;
			this.onselectstart -= Native.DisabledEventHandler;
		}

		public Rectangle Bounds
		{
			[Script(DefineAsStatic = true)]
			get
			{
				Rectangle b = new Rectangle();

				b.Left = this.offsetLeft;
				b.Top = this.offsetTop;

				b.Width = this.scrollWidth;
				b.Height = this.scrollHeight;

				return b;
			}
		}

		[Script(DefineAsStatic = true)]
		public void DisableContextMenu()
		{
			oncontextmenu += Native.DisabledEventHandler;
		}





		static string[] InternalCaptureMouseEvents = new string[] {
			"click", "mousedown", "mouseup", "mousemove", "mouseover", "mouseout" };

		static System.Action InternalCaptureMouse(IHTMLElement self)
		{
			// view-source:http://help.dottoro.com/external/examples/ljrtxexf/setCapture_3.htm
			// http://www.activewidgets.com/javascript.forum.8933.28/problems-with-version-1-0.html

			if (Expando.Of(self).Contains("setCapture"))
			{
				self.setCapture();

				return
						delegate
						{
							self.releaseCapture();
						}
					;


			}

			bool flag = false;

			System.Action<IEvent> _capture =
				delegate (IEvent e)
				{
					if (flag)
						return;

					flag = true;

					e.StopPropagation();

					//IEvent _event = Native.Document.createEvent("MouseEvents");

					// https://developer.mozilla.org/en-US/docs/Web/API/MouseEvent
					var _event = new IMouseEvent();

					_event.initMouseEvent(
						e.type,
						e.bubbles, e.cancelable, e.view, e.detail,
						e.screenX, e.screenY, e.clientX, e.clientY,
						e.ctrlKey, e.altKey, e.shiftKey, e.metaKey,
						e.button, e.relatedTarget);

					self.dispatchEvent(_event);
					flag = false;
				};


			foreach (string v in InternalCaptureMouseEvents)
				Native.window.addEventListener(v, _capture, true);

			return delegate
					{
						foreach (string v in InternalCaptureMouseEvents)
							Native.window.removeEventListener(v, _capture, true);
					}
				;
		}



		// tested by
		// X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowMouseCapture\ChromeAppWindowMouseCapture\Application.cs

		[Script(DefineAsStatic = true)]
		public System.Action CaptureMouse()
		{
			// called by
			// X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IMouseDownEvent.cs

			// will it work for chrome apps?

			// who is using this?
			return InternalCaptureMouse(this);
		}

		// ff
		private void dispatchEvent(IEvent _event)
		{

		}




		[Script(OptimizedCode = @"
		if (that.requestFullscreen) {
		    that.requestFullscreen();
		}
		else if (that.mozRequestFullScreen) {
		    that.mozRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
		}
		else if (that.webkitRequestFullScreen) {
		    that.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
		}
                    
                    ")]
		static void __requestFullscreen(object that)
		{
		}


		[Script(DefineAsStatic = true)]
		public void requestFullscreen()
		{
			// https://chromium.googlesource.com/experimental/chromium/blink/+/refs/wip/bajones/webvr%5E%21/#F20

			// http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/
			// tested by X:\jsc.svn\examples\javascript\My.Solutions.Pages.Templates\My.Solutions.Pages.Templates\Application.cs
			__requestFullscreen(this);
		}


		//        +dictionary FullscreenOptions
		//        {
		//+    HMDVRDevice vrDisplay = null;
		//+    boolean vrDistortion = true;
		//+    boolean vrTimewarp = true;
		//+};

		[Script(DefineAsStatic = true)]
		public void requestFullscreen(object vrFullscreenOptions)
		{
			// 20150302 . how many days until we can test it on a HUD?

			//https://chromium.googlesource.com/experimental/chromium/blink/+/refs/wip/bajones/webvr/Source/core/dom/FullscreenOptions.idl
			// https://chromium.googlesource.com/experimental/chromium/blink/+/refs/wip/bajones/webvr/Source/core/dom/ElementFullscreen.idl
			// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150302
			// https://chromium.googlesource.com/experimental/chromium/blink/+/refs/wip/bajones/webvr%5E%21/#F20

			// http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/
			// tested by X:\jsc.svn\examples\javascript\My.Solutions.Pages.Templates\My.Solutions.Pages.Templates\Application.cs
			__requestFullscreen(this);
		}

		internal void setCapture()
		{
		}

		internal void releaseCapture()
		{
		}


		// tested by X:\jsc.svn\examples\javascript\Test\TestStaticOptimizedCode\TestStaticOptimizedCode\Class1.cs
		[Script(OptimizedCode = @"
		if (that.requestPointerLock) {
		    that.requestPointerLock();
		}
		else if (that.mozRequestPointerLock) {
		    that.mozRequestPointerLock();
		}
		else if (that.webkitRequestPointerLock) {
		    that.webkitRequestPointerLock();
		}
                    
                    ")]
		static void __requestPointerLock(object that)
		{
		}

		// http://dvcs.w3.org/hg/pointerlock/raw-file/default/index.html
		[Script(DefineAsStatic = true)]
		public void requestPointerLock()
		{
			// X:\jsc.svn\examples\javascript\WebGL\WebGLYomotsuTPS\WebGLYomotsuTPS\Application.cs
			// tested by X:\jsc.svn\examples\javascript\My.Solutions.Pages.Templates\My.Solutions.Pages.Templates\Application.cs
			__requestPointerLock(this);
		}



		public event System.Action requestAnimationFrame
		{
			[Script(DefineAsStatic = true)]
			add
			{
				// X:\jsc.svn\examples\javascript\Test\TestOwnerDocumentDefaultView\TestOwnerDocumentDefaultView\Application.cs
				var w = this.ownerDocument.defaultView;

				if (w == null)
					w = Native.window;

				w.requestAnimationFrame += value;
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				throw new System.NotSupportedException();
			}
		}




		public static implicit operator IHTMLElement(XElement x)
		{
			// tested by
			// X:\jsc.svn\examples\javascript\Test\TestXText\TestXText\Application.cs
			// X:\jsc.svn\examples\javascript\Test\TestReplaceHTMLWithXElement\TestReplaceHTMLWithXElement\Application.cs

			return x.AsHTMLElement();
		}

		public static implicit operator XElement(IHTMLElement x)
		{
			// tested by
			// X:\jsc.svn\examples\javascript\Test\TestQuerySelectorFromServer\TestQuerySelectorFromServer\Application.cs

			return x.AsXElement();
		}

		[System.Obsolete("experimental")]
		public static implicit operator IHTMLElement(Task<XElement> x)
		{
			// first step for databinding?
			// X:\jsc.svn\examples\javascript\Test\TestReplaceHTMLWithXElement\TestReplaceHTMLWithXElement\Application.cs

			var s = new IHTMLElement { };

			// to be used with first child empty rule?
			x.ContinueWith(
				task =>
				{

					s.AsXElement().ReplaceWith(task.Result);
				}
			);

			return s;
		}






		[Script(DefineAsStatic = true)]
		public IEnumerable<IHTMLElement> querySelectorAll(ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement.HTMLElementEnum selectors)
		{
			// tested by
			// X:\jsc.svn\examples\javascript\Test\RemoveByQuerySelectorAll\RemoveByQuerySelectorAll\Application.cs

			var n = (IElement)this;
			var s = (string)(object)selectors;

			// hack cast
			return (IEnumerable<IHTMLElement>)(System.Linq.Enumerable.AsEnumerable(n.querySelectorAll(s)));
		}

		[Script(DefineAsStatic = true)]
		new public IEnumerable<IHTMLElement> querySelectorAll(string selectors)
		{
			var n = (IElement)this;

			// hack cast
			return (IEnumerable<IHTMLElement>)(System.Linq.Enumerable.AsEnumerable(n.querySelectorAll(selectors)));
		}




		public IHTMLElementGrouping this[IHTMLElement.HTMLElementEnum selectorByNodeName]
		{
			[Script(DefineAsStatic = true)]
			get
			{
				// allow handlers

				return new IHTMLElementGrouping
				{
					contextElement = this,
					selectorByNodeName = selectorByNodeName
				};
			}
		}

	}



}
