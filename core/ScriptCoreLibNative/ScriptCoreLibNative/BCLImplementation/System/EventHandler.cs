using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.BCLImplementation.System
{
	[Script(IsNative = true, Implements = typeof(global::System.EventHandler))]
	internal delegate void __EventHandler(object sender, EventArgs e);
}
