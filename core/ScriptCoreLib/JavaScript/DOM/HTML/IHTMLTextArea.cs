using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
	[Script(InternalConstructor = true)]
	public class IHTMLTextArea : IHTMLElement
	{

		public IArray<string> Lines
		{
			[Script(DefineAsStatic = true)]
			get
			{
				return IArray<string>.SplitLines(value);
			}
		}

		public string value;
		public bool disabled;
		public bool readOnly;
		public int rows;
		public int cols;
		public string wrap;

		#region constructors
		public IHTMLTextArea() { }
		public IHTMLTextArea(string value) { }

		internal static IHTMLTextArea InternalConstructor()
		{
			return (IHTMLTextArea)((object)new IHTMLElement(HTMLElementEnum.textarea));
		}

		internal static IHTMLTextArea InternalConstructor(string value)
		{
			IHTMLTextArea n = new IHTMLTextArea();

			n.value = value;

			return n;
		}


		#endregion

		public int SelectionStart
		{
			// see:
			// http://blog.vishalon.net/index.php/javascript-getting-and-setting-caret-position-in-textarea/
			// http://demo.vishalon.net/getset.htm
			// http://stackoverflow.com/questions/263743/how-to-get-cursor-position-in-textarea
			// http://the-stickman.com/web-development/javascript/finding-selection-cursor-position-in-a-textarea-in-internet-explorer/

			[Script(DefineAsStatic = true)]
			get
			{
				var value = 0;

				if (Expando.InternalIsMember(Native.Document, "selection"))
				{
					this.focus();

					var selection = Expando.InternalGetMember(Native.Document, "selection");
					var createRange = IFunction.Of(selection, "createRange");

					var r = (ITextRange)createRange.apply(selection);

					
					var createTextRange = IFunction.Of(this, "createTextRange");
					if (createTextRange != null)
					{
						var re = (ITextRange)createTextRange.apply(this);
						var rc = re.duplicate();

						re.moveToBookmark(r.getBookmark());
						rc.setEndPoint("EndToStart", re);

						value = rc.text.Length;
					}
				}

				if (Expando.InternalIsMember(this, "selectionStart"))
				{
					value = (int)Expando.InternalGetMember(this, "selectionStart");
				}

				return value;
			}
			[Script(DefineAsStatic = true)]
			set
			{
				var setSelectionRange = IFunction.Of(this, "setSelectionRange");
				if (setSelectionRange != null)
				{
					this.focus();

					setSelectionRange.apply(this, value, value);
					return;
				}

				var createTextRange = IFunction.Of(this, "createTextRange");
				if (createTextRange != null)
				{
					var r = (ITextRange)createTextRange.apply(this);

					r.collapse(true);
					r.moveEnd("character", value);
					r.moveStart("character", value);
					r.select();
				}
			}
		}
	}

	[Script(InternalConstructor = true)]
	internal class ITextRange
	{
		// http://msdn.microsoft.com/en-us/library/ms536401(VS.85).aspx
		public void collapse(bool e)
		{
		}

		public void moveEnd(string e, int pos)
		{

		}

		public void moveStart(string e, int pos)
		{

		}

		public void select()
		{

		}

		public string text;

		public ITextRange duplicate()
		{
			return default(ITextRange);
		}

		public string getBookmark()
		{
			return default(string);
		}

		public void moveToBookmark(string p)
		{
		}

		public void setEndPoint(string p, ITextRange re)
		{
		}
	}
}
