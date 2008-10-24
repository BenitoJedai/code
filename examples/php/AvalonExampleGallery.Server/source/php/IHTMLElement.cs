using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptApplication.source.php
{

	[Script]
	public class IStyle
	{
		public string color;
		public string backgroundColor;
		public string position;
		public string left;
		public string top;
		public string width;
		public string height;
		public string textAlign;
		public string borderWidth;
		
		public double opacity = 1;

		public override string ToString()
		{
			var w = new StringBuilder();

			Action<string, string> Append =
				(key, value) =>
				{
					if (value == null)
						return;

					w.Append(key).Append(": ").Append(value).Append("; ");
				};

			Append("color", color);
			Append("left", left);
			Append("top", top);
			Append("width", width);
			Append("height", height);
			Append("background-color", backgroundColor);
			Append("position", position);
			Append("text-align", textAlign);
			Append("border-width", borderWidth);

			if (opacity >= 0)
				if (opacity < 1)
				{
					Append("opacity", "" + opacity);
					Append("filter", "Alpha(Opacity=" + (opacity * 100) + ")");
				}



			return w.ToString();
		}

	}

	[Script]
	public class IHTMLInput : IHTMLElement
	{
		public IHTMLInput()
		{
			this.Name = "input";
		}

		public string Type;
		public string Value;
		public bool IsReadOnly;

		public override string ToString()
		{
			var style = "";

			if (this.Style != null)
				style = this.Style.ToString();

			var _readonly = "";

			if (this.IsReadOnly)
				_readonly = "readonly='readonly'";

			return "<" + Name + " " + _readonly + " value='" + Value + "' type='" + Type + "' style='" + style + "' />";
		}
	}

	[Script]
	public class IHTMLImage : IHTMLElement
	{
		public IHTMLImage()
		{
			this.Name = "img";
		}

		public string Source;

		public override string ToString()
		{
			var style = "";

			if (this.Style != null)
				style = this.Style.ToString();

			return "<" + Name + " src='" + Source + "' style='" + style + "' />";
		}
	}

	[Script]
	public class IHTMLElement
	{
		public string Name = "div";

		public IStyle Style;

		public string Content;
		public string Class;



		public override string ToString()
		{
			var _class = "";

			if (this.Class != null)
				_class = "class='" + this.Class + "'";

			var style = "";

			if (this.Style != null)
				style = "style='" + this.Style.ToString() + "'";

			return "<" + Name + " " + _class + " " + style + ">" + Content + "</" + Name + ">";
		}

		public static implicit operator string(IHTMLElement e)
		{
			return e.ToString();
		}
	}

}
