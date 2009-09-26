using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.TextBox))]
	internal class __TextBox : __TextBoxBase
	{
		// should we listen for enter key?
		public bool AcceptsReturn { get; set; }

		private HorizontalAlignment _TextAlign;

		public HorizontalAlignment TextAlign
		{
			get { return _TextAlign; }
			set { _TextAlign = value; }
		}

		ScrollBars InternalScrollBars;

		public ScrollBars ScrollBars
		{
			get
			{
				return InternalScrollBars;
			}
			set
			{
				InternalScrollBars = value;

				if (value != ScrollBars.None)
					this.HTMLTarget.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.auto;
				else
					this.HTMLTarget.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;
			}
		}
	}
}
