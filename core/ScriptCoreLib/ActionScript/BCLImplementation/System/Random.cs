using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    
    [Script(Implements = typeof(global::System.Random))]
    internal class __Random
    {
        public __Random()
        {

        }

        public virtual double NextDouble()
        {
            return Math.random();
        }

        public virtual int Next()
        {
            return Convert.ToInt32(NextDouble() * int.MaxValue);
        }

        public virtual int Next(int maxValue)
        {
            return Convert.ToInt32(NextDouble() * maxValue);
        }
    }
}
