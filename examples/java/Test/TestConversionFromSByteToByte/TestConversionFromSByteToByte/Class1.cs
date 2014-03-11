using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestConversionFromSByteToByte
{
    public class Class1
    {
        public Class1(object x)
        {
            //byte0 = (byte)((Short)x).shortValue();
            //byte1 = ((byte)(byte0));

            //var z = (byte)x;
            //var y = (sbyte)z;

            var z = (sbyte)x;
            var y = (byte)z;
        }
    }
}
