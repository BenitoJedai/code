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


        public static double ToDouble(int value)
        {
            return value;
        }


        public static double ToDouble(string value)
        {
            return double.Parse(value);

        }

        public static double ToDouble(object value)
        {
            return double.Parse("" + value);

        }



    }

}
