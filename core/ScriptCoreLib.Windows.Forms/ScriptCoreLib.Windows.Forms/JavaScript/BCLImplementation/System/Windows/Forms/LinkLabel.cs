using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.LinkLabel))]
	internal partial class __LinkLabel : __Label, __IButtonControl
	{
		public __LinkLabel()
		{
			// we might use a real A element instead
			this.HTMLTargetRef.style.color = ScriptCoreLib.JavaScript.Runtime.JSColor.Blue;

			// http://www.w3schools.com/Css/pr_text_text-decoration.asp
			this.HTMLTargetRef.style.textDecoration = "underline";
			this.HTMLTargetRef.style.cursor = ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.pointer;

			this.HTMLTargetRef.onclick +=
				delegate
				{
					// we could do a lazy bind here instead
					// but we assume a link will have a handler anyway

					if (this.LinkClicked != null)
						this.LinkClicked(this, new LinkLabelLinkClickedEventArgs(null));
				};
		}

		public event LinkLabelLinkClickedEventHandler LinkClicked;


		public LinkArea LinkArea { get; set; }

		public bool UseCompatibleTextRendering { get; set; }

	}
}
