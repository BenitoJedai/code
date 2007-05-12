using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.Runtime;

    [Script(InternalConstructor = true, Implements = typeof(global::System.Exception))]
    public class __Exception 
    {

        public string Message
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return Expando<string, string>.Of(this)["message"];
            }
        }

        #region Constructor

        public __Exception(string message)  { }

        [Script(OptimizedCode = @"return new Error(e);")]
        static __Exception InternalConstructor(string e)
        {
            return default(__Exception);
        }

        #endregion

    }
}
