using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestByRefCall
{
    public struct TaskAwaiter
    {
        public int state;

        public bool IsCompleted { get; set; }


    }

    public class foo
    {
        public void xMoveNext(ref TaskAwaiter e)
        {
            var state = e.state;

            state++;

            e.state = state;

            var x = e.IsCompleted;
        }
    }
}
