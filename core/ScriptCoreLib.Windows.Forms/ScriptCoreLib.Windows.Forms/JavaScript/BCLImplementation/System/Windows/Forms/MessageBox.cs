using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
	// http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/MessageBox.cs,e426fc24b95c791e
	// X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\MessageBox.cs
	// X:\jsc.svn\core\ScriptCoreLibAndroid.Windows.Forms\ScriptCoreLibAndroid.Windows.Forms\Android\BCLImplementation\System\Windows\Forms\MessageBox.cs

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
			// or should worker threads be allowed to do fire and forget?
			// as sync await wont work?
			// or, would we be able to resume a worker thread, after we interrupt it? terminate it?


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
