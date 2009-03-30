using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public partial class IHTMLStyle
	{
		// http://www.w3schools.com/HTMLDOM/prop_style_cursor.asp

		internal readonly ExternalContext.Token Token = new ExternalContext.Token();

		// use code generator...
		// open Class View, select CodeGenerator, Invoke Static Method
		// "display, color, cursor, background, backgroundColor, textAlign, position, width, height, left, top, overflow, opacity, filter, border, marginTop, marginLeft"

		public IHTMLStyle()
		{
			InitializeProperties();

		}

		partial void InitializeProperties();

		#region Properties
		partial void InitializeProperties()
		{
			this.__display = new ExternalContext.Token.Property(this.Token, "display");
			this.__color = new ExternalContext.Token.Property(this.Token, "color");
			this.__cursor = new ExternalContext.Token.Property(this.Token, "cursor");
			this.__background = new ExternalContext.Token.Property(this.Token, "background");
			this.__backgroundColor = new ExternalContext.Token.Property(this.Token, "backgroundColor");
			this.__textAlign = new ExternalContext.Token.Property(this.Token, "textAlign");
			this.__position = new ExternalContext.Token.Property(this.Token, "position");
			this.__width = new ExternalContext.Token.Property(this.Token, "width");
			this.__height = new ExternalContext.Token.Property(this.Token, "height");
			this.__left = new ExternalContext.Token.Property(this.Token, "left");
			this.__top = new ExternalContext.Token.Property(this.Token, "top");
			this.__overflow = new ExternalContext.Token.Property(this.Token, "overflow");
			this.__opacity = new ExternalContext.Token.Property(this.Token, "opacity");
			this.__filter = new ExternalContext.Token.Property(this.Token, "filter");
			this.__border = new ExternalContext.Token.Property(this.Token, "border");
			this.__marginTop = new ExternalContext.Token.Property(this.Token, "marginTop");
			this.__marginLeft = new ExternalContext.Token.Property(this.Token, "marginLeft");
		}


		public string display
		{
			set
			{
				this.__display.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __display;


		public string color
		{
			set
			{
				this.__color.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __color;


		public string cursor
		{
			set
			{
				this.__cursor.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __cursor;


		public string background
		{
			set
			{
				this.__background.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __background;


		public string backgroundColor
		{
			set
			{
				this.__backgroundColor.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __backgroundColor;


		public string textAlign
		{
			set
			{
				this.__textAlign.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __textAlign;


		public string position
		{
			set
			{
				this.__position.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __position;


		public string width
		{
			set
			{
				this.__width.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __width;


		public string height
		{
			set
			{
				this.__height.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __height;


		public string left
		{
			set
			{
				this.__left.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __left;


		public string top
		{
			set
			{
				this.__top.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __top;


		public string overflow
		{
			set
			{
				this.__overflow.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __overflow;


		public string opacity
		{
			set
			{
				this.__opacity.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __opacity;


		public string filter
		{
			set
			{
				this.__filter.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __filter;


		public string border
		{
			set
			{
				this.__border.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __border;


		public string marginTop
		{
			set
			{
				this.__marginTop.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __marginTop;


		public string marginLeft
		{
			set
			{
				this.__marginLeft.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __marginLeft;

		#endregion

	}
}
