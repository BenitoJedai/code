using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    partial class __Convert
    {


        public static int ToInt32(byte e)
        {
            return (int)e;
        }


        public static int ToInt32(long e)
        {
            return (int)e;
        }

        // script: error JSC1000: No implementation found for this native method, please implement [static System.Convert.ToInt32(System.Object)]

        public static int ToInt32(string e)
        {
            return int.Parse(e);
        }


        public static int ToInt32(object e)
        {
            return ToInt32("" + e);
        }

    }

}
