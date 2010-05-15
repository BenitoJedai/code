using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared;


namespace ScriptCoreLib.JavaScript.DOM
{
	[Script(InternalConstructor = true)]
	public partial class IStyleSheetRule
	{
		public string selectorText;
		public IStyle style;

	}

	[Script(InternalConstructor = true)]
	public partial class IStyleSheet
	{
		static IStyleSheet _Default;

		public static IStyleSheet Default
		{
			get
			{
				if (_Default == null)
					_Default = new IStyleSheet();


				return _Default;
			}
		}

		public readonly string media;
		public bool disabled;

		internal IStyleSheetRule[] rules;
		internal IStyleSheetRule[] cssRules;

		public IStyleSheetRule[] Rules
		{
			[Script(DefineAsStatic = true)]
			get
			{


				if (Expando.InternalIsMember(this, "cssRules"))
					return this.cssRules;

				if (Expando.InternalIsMember(this, "rules"))
					return this.rules;

				throw new System.Exception("member IStyleSheet.Rules not found");
			}
		}


		#region Constructor

		public IStyleSheet()
		{
			// InternalConstructor
		}

		static IStyleSheet InternalConstructor()
		{
			HTML.IHTMLStyle s = new HTML.IHTMLStyle();

			// http://phrogz.net/JS/AddCSS_test.html

			var h = Native.Document.getElementsByTagName("head");

			if (h.Length > 0)
				h[0].appendChild(s);
			else
				s.AttachToDocument();

			return s.StyleSheet;
		}

		#endregion


		internal object addRule(string s, string d, int i)
		{
			return null;
		}

		internal object insertRule(string r, int i)
		{
			return null;
		}


		// http://www.susaaland.dk/sharedoc/kdelibs-devel-3/khtml/html/classDOM_1_1CSSStyleSheet.html#a9
		// http://www.javascriptkit.com/domref/stylesheet.shtml
		[Script(DefineAsStatic = true)]
		public IStyleSheetRule AddRule(string selector, string declaration, int index)
		{
			if (Expando.InternalIsMember(this, "insertRule"))
				this.insertRule(selector + "{" + declaration + "}", index);
			else if (Expando.InternalIsMember(this, "addRule"))
				this.addRule(selector, declaration, index);
			else
				throw new System.Exception("fault at IStyleSheetRule.AddRule");


			return this.Rules[index];
		}

		[Script(DefineAsStatic = true)]
		public IStyleSheetRule AddRule(string selector)
		{
			return AddRule(selector, "/**/", this.Rules.Length);
		}

		[Script(DefineAsStatic = true)]
		public IStyleSheetRule AddRule(global::System.Collections.Generic.KeyValuePair<string, System.Action<IStyleSheetRule>> r)
		{

			return this.AddRule(r.Key, r.Value);

		}

		[Script(DefineAsStatic = true)]
		public IStyleSheetRule AddRule(string selector, System.Action<IStyleSheetRule> r)
		{
			var x = AddRule(selector);

			r(x);

			return x;
		}



		internal DOM.HTML.IHTMLStyle owningElement;
		internal DOM.HTML.IHTMLStyle ownerNode;

		public DOM.HTML.IHTMLStyle Owner
		{
			[Script(DefineAsStatic = true)]
			get
			{
				if (Expando.InternalIsMember(this, "ownerNode"))
					return this.ownerNode;

				if (Expando.InternalIsMember(this, "owningElement"))
					return this.owningElement;

				throw new System.Exception("fault at IStyleSheet.Owner");
			}
		}
	}

	[Script(HasNoPrototype = true)]
	public partial class IStyle
	{
		// todo:
		// http://samples.msdn.microsoft.com/workshop/samples/author/dhtml/filters/matrix.htm
		// http://msdn.microsoft.com/en-us/library/ms533014%28VS.85%29.aspx


		// http://kennedia.svnrepository.com/Basecamp.wdgt/trac.cgi/browser/AppleClasses/AppleButton.js?rev=5#L133
		/// <summary>
		/// Example values: "dashboard-region(control rectangle)" "none"
		/// </summary>
		public string appleDashboardRegion;

		public string lineHeight;

		// http://www.w3schools.com/CSS/pr_pos_clip.asp
		public string clip;
		public string clear;

		#region white-space

		public WhiteSpaceEnum whiteSpace;

		[Script(IsStringEnum = true)]
		public enum WhiteSpaceEnum
		{
			/// <summary>
			/// White-space is ignored by the browser
			/// </summary>
			normal,
			/// <summary>
			/// White-space is preserved by the browser. Acts like the <pre> tag in HTML
			/// </summary>

			pre,
			/// <summary>
			/// The text will never wrap, it continues on the same line until a <br> tag is encountered
			/// </summary>
			nowrap
		}
		#endregion


		#region cursor
		// TODO support per member values in enum
		[Script(IsStringEnum = true)]
		public enum CursorEnum
		{
			@default, auto, crosshair, pointer, move, text, wait, help

		}

		/// <summary>
		/// <see>http://www.w3schools.com/css/pr_class_cursor.asp</see>
		/// </summary>
		public CursorEnum cursor;
		#endregion

		// http://www.w3schools.com/css/css_background.asp
		public string background;
		public string backgroundImage;
		public string backgroundColor;
		//public string backgroundLeft;
		//public string backgroundTop;
		public string backgroundRepeat;
		public string backgroundPosition;

		#region display

		/// <summary>
		/// http://www.w3schools.com/css/pr_class_display.asp
		/// </summary>
		[Script(IsStringEnum = true)]
		public enum DisplayEnum
		{
			none,
			block,
			inline,
			[Script(ExternalTarget = "")]
			empty,


			[Script(ExternalTarget = "inline-block")]
			inline_block

		}
		public DisplayEnum display;
		#endregion

		#region visibility
		[Script(IsStringEnum = true)]
		public enum VisibilityEnum
		{
			visible,
			hidden,
			collapse
		}
		public VisibilityEnum visibility;
		#endregion


		#region padding
		public string padding;
		public string paddingTop;
		public string paddingBottom;
		public string paddingLeft;
		public string paddingRight;
		#endregion

		#region fontFamily
		[Script(IsStringEnum = true)]
		public enum FontFamilyEnum
		{
			Times, Helvetica, /*Zapf-Chancery,*/ Western,
			[Script(ExternalTarget = "Consolas, Courier New, Courier")]
			Consolas,
			Courier, Verdana, Tahoma, Arial,
			Fixedsys
		}
		public FontFamilyEnum fontFamily;

		//[Script(ExternalTarget = "fontFamily")]
		//public string fontFamilyValue;
		#endregion


		public string fontWeight;
		public string fontSize;
		public string font;

		public string margin;
		public string marginLeft;
		public string marginTop;
		public string marginRight;
		public string marginBottom;

		#region border
		public string border;
		public string borderStyle;

		public string borderRight;
		public string borderRightColor;

		public string borderLeft;
		public string borderLeftColor;

		public string borderTop;
		public string borderTopColor;

		public string borderBottom;
		public string borderBottomColor;

		public string borderWidth;
		public string borderColor;
		#endregion

		public string color;






		// http://dolfo.org/2008/07/disable-safaris-textarea-grip/
		public string resize;
		public string right;
		public string bottom;
		public string left;
		public string top;
		public string width;
		public string height;

		// http://www.w3schools.com/css/pr_dim_min-width.asp
		public string minWidth;
		public string minHeight;

		#region position
		[Script(IsStringEnum = true)]
		public enum PositionEnum
		{
			@static,
			absolute,
			relative
		}

		public PositionEnum position;
		#endregion


		#region overflow
		// http://msdn.microsoft.com/en-us/library/ms530826(VS.85).aspx
		// http://www.brunildo.org/test/Overflowxy2.html

		[Script(IsStringEnum = true)]
		public enum OverflowEnum
		{
			visible, hidden, scroll, auto
		}

		public OverflowEnum overflow;
		public OverflowEnum overflowX;
		public OverflowEnum overflowY;

		#endregion

		public string outline;


		#region textAlgin
		[Script(IsStringEnum = true)]
		public enum TextAlignEnum { left, right, center, justify }
		public TextAlignEnum textAlign;
		#endregion

		// http://www.quirksmode.org/css/textshadow.html
		public string textShadow;
		public string textDecoration;

		[Script(IsStringEnum = true)]
		public enum TextTransformEnum
		{
			/// <summary>
			/// Default. Defines normal text, with lower case letters and capital letters
			/// </summary>
			none,
			/// <summary>
			/// Each word in a text starts with a capital letter
			/// </summary>
			capitalize,
			/// <summary>
			/// Defines only capital letters
			/// </summary>
			uppercase
				,
			/// <summary>
			/// Defines no capital letters, only lower case letters
			/// </summary>
			lowercase
		}
		public TextTransformEnum textTransform;

		public string verticalAlign;

		public int zIndex;

	}
}
