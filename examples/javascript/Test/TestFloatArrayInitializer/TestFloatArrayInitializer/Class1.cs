using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]

namespace TestFloatArrayInitializer
{
    public class Class1
    {
        public static object @int()
        {
            var fdeg_state = new int[5];
            // int array not initialized?

            fdeg_state[0] = 0;
            fdeg_state[1] = 66;
            fdeg_state[2] = 66;
            fdeg_state[3] = 0;
            fdeg_state[4] = 66;

            return fdeg_state;
        }

        public static object @float()
        {
            var fdeg_state = new float[8];
            // int array not initialized?

            fdeg_state[0] = 0;
            fdeg_state[1] = 66;
            fdeg_state[2] = 66;
            fdeg_state[3] = 0;
            fdeg_state[4] = 66;
            fdeg_state[5] = 1;
            fdeg_state[6] = 1;
            fdeg_state[7] = 1;

            return fdeg_state;
        }

        public static object @float_inline()
        {
            var fdeg_state = new float[]
            {
             0,
             66,
             66,
             0,
             66,
             1,
             1,
             1
            };


            return fdeg_state;
        }
    }
}
