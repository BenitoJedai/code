using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Forms;
using javax.swing;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.MessageBox))]
	internal class __MessageBox
	{
		public static DialogResult Show(string text)
		{
			JOptionPane.showMessageDialog(null, text);

			return DialogResult.OK;
		}
	}
}
