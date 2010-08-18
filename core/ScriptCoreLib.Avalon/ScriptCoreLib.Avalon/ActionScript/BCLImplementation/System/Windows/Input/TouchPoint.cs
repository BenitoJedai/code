using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Windows.Input.TouchPoint, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
    internal class __TouchPoint : IEquatable<__TouchPoint>
    {
        public Point Position { get; internal set;  }

        public __TouchDevice TouchDevice { get; internal set; }

        public bool Equals(__TouchPoint other)
        {
            throw new NotImplementedException();
        }
    }
}
