using ScriptCoreLib.PHP;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Delegate))]
    internal class __Delegate
    {
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Target)]
        public object Target;

        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Method)]
        public global::System.IntPtr Method;



        public __Delegate(object e, global::System.IntPtr p)
        {
            Target = e;
            Method = p;

        }


        public static __Delegate Combine([ScriptParameterByRef] __Delegate a,[ScriptParameterByRef] __Delegate b)
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

            return default(__Delegate);
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
            if (d.Equals(this))
            {
                return null;
            }

            return this;
        }

        public override bool Equals(object obj)
        {
            __Delegate v = (__Delegate)obj;


            return v.Method == this.Method && v.Target == this.Target;
        }

        public override int GetHashCode()
        {
            return default(int);
        }
    }
}