using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLibJava.BCLImplementation.System.Configuration
{
	[Script(Implements = typeof(global::System.Configuration.ApplicationSettingsBase))]
	internal class __ApplicationSettingsBase : __SettingsBase, __INotifyPropertyChanged
	{
	}
}
