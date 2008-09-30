using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.ISite))]
	internal interface __ISite
	{
		IComponent Component { get; }
		IContainer Container { get; }

		bool DesignMode { get; }

		string Name { get; set; }
	}
}
