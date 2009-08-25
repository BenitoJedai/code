using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.MessageBox))]
	internal class __MessageBox
	{
		public static DialogResult Show(string text)
		{
			// we could emulate this via html
			// we could show new window/popup

			Native.Window.alert(text);

			return DialogResult.OK;
		}
	}
}
