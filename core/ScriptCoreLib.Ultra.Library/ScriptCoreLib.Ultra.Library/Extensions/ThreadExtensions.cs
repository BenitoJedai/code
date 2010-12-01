using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScriptCoreLib.Extensions
{
    public static class ThreadExtensions
    {
        public static void WhileAlive(this Thread t, Action h)
        {
            while (t.IsAlive)
            {
                h();
            }
        }
    }
}
