using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.Application))]
	internal class __Application
	{
		public static void EnableVisualStyles()
		{
			// i guess we should make UI pretty from now on?
		}

		public static void SetCompatibleTextRenderingDefault(bool defaultValue)
		{
			// ?
		}

		public static void Run(Form mainForm)
		{
			mainForm.Show();

			// we cannot block here tho...
		}
	}
}
