using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.Boolean),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.Boolean))]
    internal class __Boolean
    {


        [Script(DefineAsStatic = true)]
        public int CompareTo(bool value)
        {
            var v = (bool)(object)this;
            
            if (v == value)
            {
                return 0;
            }
            if (!v)
            {
                return -1;
            }
            return 1;
        }







        [Script(DefineAsStatic = true)]
        public int CompareTo(object value)
        {
            if (value == null)
            {
                return 1;
            }
            if (!(value is bool))
            {
                throw new ArgumentException("MustBeBoolean");
            }


            return CompareTo((int)value);
        }





    }
}
