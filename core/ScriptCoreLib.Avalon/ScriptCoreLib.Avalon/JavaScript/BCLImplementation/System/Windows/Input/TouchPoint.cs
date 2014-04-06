using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input
{
    //[Script(ImplementsViaAssemblyQualifiedName = "System.Windows.Input.TouchPoint, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
    [Script(Implements = typeof(global::System.Windows.Input.TouchPoint))]
    internal class __TouchPoint : IEquatable<__TouchPoint>
    {
        // X:\jsc.svn\core\ScriptCoreLib.Avalon\ScriptCoreLib.Avalon\ActionScript\BCLImplementation\System\Windows\Input\TouchPoint.cs

        public Point Position { get; internal set; }

        public __TouchDevice TouchDevice { get; internal set; }

        public bool Equals(__TouchPoint other)
        {
            throw new NotImplementedException();
        }


        public static implicit operator TouchPoint(__TouchPoint p)
        {
            return (TouchPoint)(object)p;
        }
    }
}
