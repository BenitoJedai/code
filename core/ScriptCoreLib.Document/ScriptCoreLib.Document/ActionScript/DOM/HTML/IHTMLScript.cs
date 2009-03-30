using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.DOM.HTML
{
	[Script]
	public partial class IHTMLScript : IHTMLElement
	{
		// http://www.w3schools.com/HTMLDOM/prop_style_cursor.asp


		// use code generator...
		// open Class View, select CodeGenerator, Invoke Static Method
		// "type, src"

		public IHTMLScript()
		{
			this.tag = "script";

			InitializeProperties();

		}

		partial void InitializeProperties();

		#region Properties
		partial void InitializeProperties()
		{
			this.__type = new ExternalContext.Token.Property(this.Token, "type");
			this.__src = new ExternalContext.Token.Property(this.Token, "src");
		}


		public string type
		{
			set
			{
				this.__type.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __type;


		public string src
		{
			set
			{
				this.__src.PropertyValue = value;
			}
		}
		internal ExternalContext.Token.Property __src;

		#endregion

	}
}
