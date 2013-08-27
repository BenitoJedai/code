using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class RandomExtensions
    {
        public static float NextFloat(this Random r)
        {
            return (float)r.NextDouble();
        }

        public static byte NextByte(this Random r)
        {
            return (byte)r.Next(255);
        }
    }
}
