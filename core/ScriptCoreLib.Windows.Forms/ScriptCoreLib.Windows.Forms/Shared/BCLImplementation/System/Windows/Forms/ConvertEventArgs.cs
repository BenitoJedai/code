﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(ConvertEventArgs))]
    public class __ConvertEventArgs : EventArgs
    {
        public object Value { get; set; }
    }

 

}
