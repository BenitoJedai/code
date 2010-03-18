using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.Remoting.DOM
{
	public interface PStyle
	{
		// css code autogen please

		string color { set; }
		string border { set; }
		string width { set; }
		string height { set; }
	}

	public delegate void PStyleAction(PStyle e);

	public class PIStyle : PStyle
	{
		public IStyle InternalStyle;

		#region PStyle Members

		public string color
		{
			set { InternalStyle.color = value; }
		}

		public string border
		{
			set { InternalStyle.border = value; }
		}

		public string width
		{
			set { InternalStyle.width = value; }
		}

		public string height
		{
			set { InternalStyle.height = value; }
		}

		#endregion
	}
}
