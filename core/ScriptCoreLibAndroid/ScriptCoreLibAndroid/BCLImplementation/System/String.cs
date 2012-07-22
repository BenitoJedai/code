using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System
{
    [Script(
Implements = typeof(global::System.String),
ImplementationType = typeof(global::java.lang.String),
InternalConstructor = true

)]
    internal class __String
    {

        [Script(ExternalTarget = "equals", DefineAsInstance = true)]
        public static bool operator ==(__String a, __String b)
        {
            return default(bool);
        }


        public static bool operator !=(__String a, __String b)
        {
            return !(a == b);
        }

        public static bool IsNullOrEmpty(string value)
        {
            if (((object)value) == null)
                return true;

            if ("" == value)
                return true;

            return false;
        }

        [Script(ExternalTarget = "equals")]
        public bool Equals(string e)
        {
            return default(bool);
        }

        public int Length
        {
            [Script(ExternalTarget = "length")]
            get
            {
                return default(int);
            }
        }

        [Script(ExternalTarget = "indexOf")]
        public int IndexOf(string str, int pos)
        {
            return default(int);
        }
    }

}
