using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace EventMethodAttributes
{
    public class Class1
    {
        [method: Script(NotImplementedHere = true)]
        public event Action<object> detectionComplete;
    }


    [Script(Implements = typeof(Class1))]
    internal static class __Class1
    {
        public static void add_clear(Class1 that, Action<object> value)
        {
        }

        public static void remove_clear(Class1 that, Action<object> value)
        {
        }
    }
}
