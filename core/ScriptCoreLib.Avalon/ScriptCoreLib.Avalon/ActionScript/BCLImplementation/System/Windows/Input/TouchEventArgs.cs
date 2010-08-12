using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input
{

    [Script(ImplementsViaAssemblyQualifiedName = "System.Windows.Input.TouchEventArgs, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
    internal class __TouchEventArgs : __InputEventArgs
    {
        // While targeting .NET 3.5 framework we still need to let jsc know 
        // that if it is running under .NET 4.0 framework we have some interests.

        public __TouchDevice TouchDevice { get; set; }
    }
}
