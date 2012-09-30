using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.LicenseManager))]
    internal class __LicenseManager
    {
        public static LicenseUsageMode UsageMode { get; set; }
    }
}
