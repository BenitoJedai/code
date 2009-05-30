using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibAppJet.JavaScript.BCLImplementation.System
{
    // System.Environment
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
