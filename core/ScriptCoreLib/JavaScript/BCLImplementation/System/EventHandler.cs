using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{

    
    [Script(Implements = typeof(global::System.EventHandler))]
    internal delegate void __EventHandler(object sender, global::System.EventArgs args);

	[Script(Implements = typeof(global::System.EventHandler<>))]
	internal delegate void __EventHandler<TEventArgs>(object sender, TEventArgs e);
    
}
