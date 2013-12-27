using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.Runtime;

    [Script(InternalConstructor = true, Implements = typeof(global::System.Exception))]
    internal class __Exception
    {

        public string Message
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return Expando<string, string>.Of(this)["message"];
            }
        }

        public string StackTrace
        {
            [Script(DefineAsStatic = true)]
            get
            {
                // https://code.google.com/p/v8/wiki/JavaScriptStackTraceApi
                return Expando<string, string>.Of(this)["stack"];
            }
        }

        #region Constructor

        public __Exception(string message) { }

        [Script(OptimizedCode = @"return new Error(e);")]
        internal static __Exception InternalConstructor(string e)
        {
            return default(__Exception);
        }

        public __Exception() { }

        [Script(OptimizedCode = @"return new Error();")]
        static __Exception InternalConstructor()
        {
            return default(__Exception);
        }

        #endregion

    }
}
