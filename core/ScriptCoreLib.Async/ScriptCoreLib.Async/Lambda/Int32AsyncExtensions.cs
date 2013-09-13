using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.Lambda
{
    // ScriptCoreLib.Query shall move here
    public static class Int32AsyncExtensions
    {
        public static TaskAwaiter GetAwaiter(this int ms)
        {
            return Task.Delay(ms).GetAwaiter();
        }




        public static async Task Delay(int ms)
        {
            await Task.Delay(ms);
        }
    }
}
