﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input
{
    //[Script(ImplementsViaAssemblyQualifiedName = "System.Windows.Input.TouchDevice, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
    [Script(Implements = typeof(global::System.Windows.Input.TouchDevice))]
    public class __TouchDevice : __InputDevice, __IManipulator
    {
        public int Id { get; set; }

        //public abstract TouchPointCollection GetIntermediateTouchPoints(IInputElement relativeTo);
    }
}
