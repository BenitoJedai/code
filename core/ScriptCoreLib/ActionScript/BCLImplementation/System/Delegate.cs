using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.Serialization;
using System.Reflection;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Delegate))]
    internal class __Delegate : __ICloneable, __ISerializable
    {
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Target)]
        public object _Target;

        public object Target { get { return this._Target; } }

        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Method)]
        public global::System.IntPtr _Method;

        public MethodInfo Method
        {
            get
            {
                return new __MethodInfo { 
                    //_Target = this._Target, 
                    _Method = this._Method 
                };
            }
        }


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





        public static __Delegate Combine(__Delegate a, __Delegate b)
        {
            if (a == null)
            {
                return b;
            }
            if (b == null)
            {
                return a;
            }

            return a.CombineImpl(b);
        }

        protected virtual __Delegate CombineImpl(__Delegate d)
        {
            throw new global::System.Exception("use MulticastDelegate instead");
        }

        public static __Delegate Remove(__Delegate source, __Delegate value)
        {
            if (source == null)
            {
                return null;
            }
            if (value == null)
            {
                return source;
            }
            return source.RemoveImpl(value);
        }

        protected virtual __Delegate RemoveImpl(__Delegate d)
        {
            throw new global::System.Exception("use MulticastDelegate instead");
        }
    }
}
