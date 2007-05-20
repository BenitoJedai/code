using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Object))]
    internal class __Object
    {
        [Script(DefineAsStatic=true)]
        new public Type GetType()
        {
            return null;
        }

        public static bool Equals(object objA, object objB)
        {
            if (objA == objB)
            {
                return true;
            }
            if ((objA != null) && (objB != null))
            {
                return objA.Equals(objB);
            }
            return false;


        }






        public new virtual bool Equals(object obj)
        {
            return this == obj;
        }

        public new virtual int GetHashCode()
        {
            return default(int);
        }

        public new virtual string ToString()
        {
            return default(string);
        }
    }

}
