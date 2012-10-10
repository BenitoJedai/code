using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.JavaScript.Controls.ImageReflection
{
    [Script]
    static class Extensions
    {
        public static int ToInt32(this double e)
        {
            return System.Convert.ToInt32(e);
        }


    }
}
