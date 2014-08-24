using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections
{
    // http://referencesource.microsoft.com/#mscorlib/system/collections/comparer.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Collections\Comparer.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Collections\Comparer.cs

    [Script(Implements = typeof(Comparer))]
    internal class __Comparer : __IComparer
    {
        static __Comparer()
        {
            Default = new __Comparer();
        }

        public static readonly __Comparer Default;



        #region __IComparer Members

        public int Compare(object ka, object kb)
        {
            if (ka == kb)
            {
                return 0;
            }
            if (ka == null)
            {
                return -1;
            }
            if (kb == null)
            {
                return 1;
            }

            var r = -2;

            if (Expando.Of(ka).IsString)
                r = Expando.Compare(ka, kb);

            if (Expando.Of(ka).IsNumber)
                r = Expando.Compare(ka, kb);

            if (Expando.Of(ka).IsBoolean)
                r = Expando.Compare(ka, kb);


            if (r == -2)
            {
                if (ka == kb)
                    return 0;

                // how do we compare two objects?
                // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Collections\Comparer.cs
                return 1;
            }

            return r;

        }

        #endregion
    }
}
