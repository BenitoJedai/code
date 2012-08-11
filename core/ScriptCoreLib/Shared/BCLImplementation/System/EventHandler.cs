using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.EventHandler))]
    internal delegate void __EventHandler(object sender, EventArgs e);


    [Script(Implements = typeof(global::System.EventHandler<>))]
    internal delegate void __EventHandler<TEventArgs>(object sender, TEventArgs e);
}
