using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.DOM;

	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/MulticastDelegate.cs
	// http://referencesource.microsoft.com/#mscorlib/system/multicastdelegate.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System/MulticastDelegate.cs
	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\MulticastDelegate.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\MulticastDelegate.cs
	// X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\MulticastDelegate.cs
	// X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\MulticastDelegate.cs

	// X:\opensource\github\WootzJs\WootzJs.Runtime\MulticastDelegate.cs

	[Script(Implements = typeof(global::System.MulticastDelegate))]
    internal class __MulticastDelegate : __Delegate
    {
        // X:\jsc.svn\examples\javascript\test\TestIDLDelegateToFunction\TestIDLDelegateToFunction\Class1.cs
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.List)]
        IArray<__Delegate> list = new IArray<__Delegate>();

        //public __MulticastDelegate()
        //{
        //    list.push(this);
        //}

        public __MulticastDelegate(object e, global::System.IntPtr p)
            :
            base(e, p)
        {
            list.push(this);
        }

        protected override __Delegate CombineImpl(__Delegate d)
        {
            list.push(d);

            return this;
        }

        protected override __Delegate RemoveImpl(__Delegate d)
        {
            var j = -1;

            for (int i = 0; i < list.length; i++)
            {
                if (list[i] == d)
                {
                    j = i;
                    break;
                }
            }

            if (j > -1)
                list.splice(j, 1);

            if (list.length == 0)
                return null;

            return this;
        }


        public static implicit operator MulticastDelegate(__MulticastDelegate e)
        {
            return (MulticastDelegate)(object)e;
        }

    }

}
