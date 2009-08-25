using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Configuration
{
	[Script(Implements = typeof(global::System.Configuration.SettingsBase))]
	internal class __SettingsBase
	{
		public static __SettingsBase Synchronized(__SettingsBase settingsBase)
		{
			// javascript is single threaded...
			return settingsBase;
		}
	}
}
