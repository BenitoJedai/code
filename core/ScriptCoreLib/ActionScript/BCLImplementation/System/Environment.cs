﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Environment))]
    internal static class __Environment
    {
        public static string NewLine
        {
            get
            {
                return "\r\n";
            }
        }
    }
}
