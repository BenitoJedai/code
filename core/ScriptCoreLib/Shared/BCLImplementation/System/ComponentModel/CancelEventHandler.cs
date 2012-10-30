using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{

    [Script(Implements = typeof(global::System.ComponentModel.CancelEventHandler))]
    internal delegate void __CancelEventHandler(object sender, ListChangedEventArgs e);


}
