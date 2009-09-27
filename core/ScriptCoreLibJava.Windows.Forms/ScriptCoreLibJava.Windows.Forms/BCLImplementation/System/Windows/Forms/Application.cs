using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Forms;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
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
			Console.WriteLine("? mainForm.FormClosed");
			mainForm.FormClosed +=
				delegate
				{
					Console.WriteLine("mainForm.Dispose");
				};

			mainForm.Show();

			// in javascript we cannot block here!
			// in java we actually should to prevent early termination

		

			//mainForm.Dispose();
		}


	}
}
