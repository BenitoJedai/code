using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel
{

    [Script(Implements = typeof(global::System.ComponentModel.ListChangedEventHandler))]
    public delegate void __ListChangedEventHandler(object sender, ListChangedEventArgs e);


}
