using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation
{
    using ScriptCoreLib.JavaScript.DOM;

    [Script(Implements = typeof(global::System.DateTime))]
    internal class __DateTime
    {
        public IDate Value;


        public static __DateTime Now
        {
            get
            {
                return new __DateTime { Value = new IDate() };
            }
        }

        public long Ticks
        {
            get
            {
                // conversion needed

                return this.Value.getTime();
            }
        }
    }
}
