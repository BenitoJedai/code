using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.PropertyChangedEventHandler))]
    internal delegate void __PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

}
