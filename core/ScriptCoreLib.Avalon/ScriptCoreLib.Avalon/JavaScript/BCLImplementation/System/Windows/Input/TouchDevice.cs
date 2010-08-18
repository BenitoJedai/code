﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Windows.Input.TouchDevice, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
    internal class __TouchDevice : __InputDevice, __IManipulator
    {
        public int Id { get; set; }

        //public abstract TouchPointCollection GetIntermediateTouchPoints(IInputElement relativeTo);
    }
}
