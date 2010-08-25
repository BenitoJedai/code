using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
    [Script(Implements = typeof(global::System.Windows.SizeChangedEventHandler))]
    internal delegate void __SizeChangedEventHandler(object sender, SizeChangedEventArgs e);
}
