using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    public sealed class Date
    {
        public Date(long ticks)
        {
        }

        public Date()
        {
        }
    }
}

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    //[Script(Implements = typeof(global::System.DateTime))]
    internal class __DateTime
    {
        internal Date InternalValue;


        private __DateTime()
            : this(-1, -1, -1, -1, -1, -1)
        {

        }

        public __DateTime(int year, int month, int day, int hour, int minute, int second)
        {
            this.InternalValue = new Date();

        }
    }
}
