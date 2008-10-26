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
		public string paddingTop;
		public string display;

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

			Append("display", display);
			Append("color", color);
			Append("left", left);
			Append("top", top);
			Append("width", width);
			Append("height", height);
			Append("background-color", backgroundColor);
			Append("position", position);
			Append("text-align", textAlign);
			Append("border-width", borderWidth);
			Append("padding-top", paddingTop);

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
	public class IHTMLMeta : IHTMLElement
	{
		// http://www.w3schools.com/tags/tag_meta.asp


		[Script]
		public class Generator : IHTMLMeta
		{
			public Generator()
			{
				this.MetaName = "fenerator";
			}

			public static implicit operator Generator(string e)
			{
				return new Generator { MetaContent = e };
			}
		}

		[Script]
		public class Description : IHTMLMeta
		{
			public Description()
			{
				this.MetaName = "description";
			}

			public static implicit operator Description(string e)
			{
				return new Description { MetaContent = e };
			}
		}

		[Script]
		public class Keywords : IHTMLMeta
		{
			public Keywords()
			{
				this.MetaName = "keywords";
			}

			public static implicit operator Keywords(string e)
			{
				return new Keywords { MetaContent = e };
			}
		}

		public IHTMLMeta()
		{
			this.Name = "meta";
		}


		public string MetaName { get; set; }
		public string MetaContent { get; set; }


		public override string ToString()
		{
			var name = "";
			if (this.MetaName != null)
				name = "name='" + this.MetaName + "'";

			var content = "";
			if (this.MetaContent != null)
				content = "content='" + this.MetaContent + "'";






			return "<" + Name + " " + name + " " + content + " />";
		}
	}

	[Script]
	public class IHTMLTitle : IHTMLElement
	{
		public IHTMLTitle()
		{
			this.Name = "title";
		}

		public static implicit operator IHTMLTitle(string e)
		{
			return new IHTMLTitle { Content = e };
		}
	}

	[Script]
	public class IHTMLStyle : IHTMLElement
	{
		public IHTMLStyle()
		{
			this.Name = "style";
		}

		public static implicit operator IHTMLStyle(string e)
		{
			return new IHTMLStyle { Content = e };
		}
	}

	[Script]
	public class IHTMLHead : IHTMLElement
	{
		public IHTMLHead()
		{
			this.Name = "head";
		}
	}

	[Script]
	public class IHTMLBreak : IHTMLElement
	{



		public IHTMLBreak()
		{
			this.Name = "br";
		}

		
		public override string ToString()
		{
			
			return "<" + Name + "/>";
		}
	}

	[Script]
	public class IHTMLAnchor : IHTMLElement
	{



		public IHTMLAnchor()
		{
			this.Name = "a";
		}

		/// <summary>
		/// Sets or retrieves the destination URL or anchor point. 
		/// </summary>
		public string URL { get; set; }

		public override string ToString()
		{
			var href = "";
			if (this.URL != null)
				href = "href='" + this.URL + "'";



			return "<" + Name + " " + href + ">" + this.Content + "</" + Name + ">";
		}
	}


	[Script]
	public class IHTMLLink : IHTMLElement
	{
		[Script]
		public class RSS : IHTMLLink
		{
			public RSS()
			{
				this.Relationship = "alternate";
				this.Type = "application/rss+xml";
				this.Title = "RSS 2.0";
			}

			public static implicit operator RSS(string e)
			{
				return new RSS { URL = e };
			}
		}

		// http://msdn.microsoft.com/en-us/library/ms535848(VS.85).aspx#

		public IHTMLLink()
		{
			this.Name = "link";
		}

		/// <summary>
		/// Sets or retrieves the relationship between the object and the destination of the link.
		/// </summary>
		public string Relationship { get; set; }


		/// <summary>
		/// Sets or retrieves the MIME type of the object. 
		/// </summary>
		public string Type { get; set; }


		public string Title { get; set; }

		/// <summary>
		/// Sets or retrieves the destination URL or anchor point. 
		/// </summary>
		public string URL { get; set; }

		public override string ToString()
		{
			var rel = "";
			if (this.Relationship != null)
				rel = "rel='" + this.Relationship + "'";

			var type = "";
			if (this.Type != null)
				type = "type='" + this.Type + "'";




			var title = "";
			if (this.Title != null)
				title = "title='" + this.Title + "'";

			var href = "";
			if (this.URL != null)
				href = "href='" + this.URL + "'";



			return "<" + Name + " " + rel + " " + type + " " + title + " " + href + " />";
		}
	}

	[Script]
	public class IHTMLScript : IHTMLElement
	{
		// http://msdn.microsoft.com/en-us/library/ms535848(VS.85).aspx#

		public IHTMLScript()
		{
			this.Name = "script";
			this.Type = "text/javascript";
		}


		/// <summary>
		/// Sets or retrieves the MIME type of the object. 
		/// </summary>
		public string Type { get; set; }


		/// <summary>
		/// Sets or retrieves the destination URL or anchor point. 
		/// </summary>
		public string URL { get; set; }

		public override string ToString()
		{

			var type = "";
			if (this.Type != null)
				type = "type='" + this.Type + "'";



			var href = "";
			if (this.URL != null)
				href = "src='" + this.URL + "'";



			return "<" + Name + " " + type + " " + href + " >" + Content + "</" + Name + ">";
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
		[Script]
		public class Hidden : IHTMLElement
		{
			public Hidden()
			{
				this.Style = new IStyle
				{
					display = "none"
				};
			}
		}

		public string Name = "div";

		public IStyle Style;

		public string Content;
		public string Class;



		public override string ToString()
		{
			var _class = "";

			if (this.Class != null)
				_class = " class='" + this.Class + "'";

			var style = "";

			if (this.Style != null)
				style = " style='" + this.Style.ToString() + "'";

			return "<" + Name + _class + style + ">" + Content + "</" + Name + ">";
		}

		public static implicit operator string(IHTMLElement e)
		{
			return e.ToString();
		}
	}

}
