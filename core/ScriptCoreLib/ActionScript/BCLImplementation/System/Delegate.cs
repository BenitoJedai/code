using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Delegate))]
    internal class __Delegate
    {
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Target)]
        public object _Target;

        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Method)]
        public global::System.IntPtr _Method;

        public __Delegate(object e, global::System.IntPtr p)
        {
            _Target = e;
            _Method = p;
        }


        Function _FunctionPointer;

        public Function FunctionPointer
        {
            get
            {
                if (_FunctionPointer == null)
                {
                    var method = ToIntPtr(_Method);

                    if (method.FunctionToken != null)
                    {
                        _FunctionPointer = method.FunctionToken;
                    }
                    else
                    {
                        _FunctionPointer = GetFunctionPointer(_Target, method.StringToken);
                    }
                }

                return _FunctionPointer;
            }
        }
        
        [Script(OptimizedCode = "return o;")]
        private static __IntPtr ToIntPtr(global::System.IntPtr o)
        {
            return default(__IntPtr);
        }
        
        [Script(OptimizedCode = "return o[n];")]
        private static Function GetFunctionPointer(object o, string n)
        {
            return default(Function);
        }
    }
}
