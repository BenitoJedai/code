using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class TypeExtensions
    {
        public static bool TypeEqualsOrElementTypeEquals(this Type e, Type x)
        {
            // Type.Equals on arrays seems to return false even if element types are the same...

            if (e.HasElementType)
            {
                if (x.HasElementType)
                    return x.GetElementType().TypeEqualsOrElementTypeEquals(e.GetElementType());

                return false;
            }

            return e == x;
        }
    }
}
