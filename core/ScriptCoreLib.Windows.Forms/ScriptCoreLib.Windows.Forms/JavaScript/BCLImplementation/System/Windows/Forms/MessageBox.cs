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

			Native.window.alert(text);

			return DialogResult.OK;
		}

        public static DialogResult Show(string text, string c, MessageBoxButtons b)
        {
            // we could emulate this via html
            // we could show new window/popup

            if (b == MessageBoxButtons.YesNo)
            {
                var x = Native.window.confirm(text);

                if (x)
                    return DialogResult.Yes;

                return DialogResult.No;
            }

            Native.window.alert(text);

            return DialogResult.OK;
        }
	}
}
