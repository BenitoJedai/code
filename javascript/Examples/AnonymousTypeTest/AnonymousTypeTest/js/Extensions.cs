using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace AnonymousTypeTest.js
{
    [Script]
    static class Extensions
    {
        public static int Random(this int i)
        {
            return new Random().Next(i);
        }
    }
}
