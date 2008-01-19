using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.String),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.String)
        )]
    internal class __String
    {
        #region Concat
        [Script(OptimizedCode = "return a+b;")]
        public static string Concat(object a, object b)
        {
            return default(string);
        }


        [Script(OptimizedCode = "return a+b;")]
        public static string Concat(string a, string b)
        {
            return default(string);
        }
        #endregion

        #region Length
        public int Length
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return InternalLength(this);
            }
        }

        [Script(OptimizedCode = "return e.length;")]
        static internal int InternalLength(__String e)
        {
            return default(int);
        }
        #endregion

    }
}
