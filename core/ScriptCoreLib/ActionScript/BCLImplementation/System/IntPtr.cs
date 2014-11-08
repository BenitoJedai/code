using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/intptr.cs
    // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\IntPtr.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\IntPtr.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\IntPtr.cs

    [Script(Implements = typeof(global::System.IntPtr))]
    public class __IntPtr
    {
        // the fast ref
        public Function FunctionToken;

        // data for worker threads
        // to find the method yet again.
        public string FunctionToken_TypeFullName;
        public string FunctionToken_MethodName;


        public string StringToken;
        public Class ClassToken;



        public static implicit operator IntPtr(__IntPtr _ptr)
        {
            return (IntPtr)(object)_ptr;
        }
        public static implicit operator __IntPtr(IntPtr _ptr)
        {
            return (__IntPtr)(object)_ptr;
        }


        // start0 = new __ParameterizedThreadStart(null, __IntPtr.op_Explicit_4ebbe596_06001686(TheOtherClass.Invoke_6d788eff_0600000c));

        public static explicit operator __IntPtr(string _Token)
        {
            return new __IntPtr { StringToken = _Token };
        }

        public static explicit operator __IntPtr(Function _Token)
        {
            return new __IntPtr { FunctionToken = _Token };
        }

        [Obsolete("used by the compiler")]
        // OpCodes.Ldftn
        public static __IntPtr OfFunctionToken(Function FunctionToken, string FunctionToken_TypeFullName, string FunctionToken_MethodName)
        {
            return new __IntPtr
            {
                FunctionToken = FunctionToken,
                FunctionToken_TypeFullName = FunctionToken_TypeFullName,
                FunctionToken_MethodName = FunctionToken_MethodName
            };
        }

        // when is this used?
        public static explicit operator string(__IntPtr _ptr)
        {
            return _ptr.StringToken;
        }

        public static explicit operator Function(__IntPtr _ptr)
        {
            return _ptr.FunctionToken;
        }



        public override string ToString()
        {
            // X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\ApplicationSprite.cs

            return "IntPtr " + new
            {
                this.StringToken,
                this.ClassToken,

                this.FunctionToken,
                this.FunctionToken_TypeFullName,
                this.FunctionToken_MethodName,

            }.ToString();
        }



    }
}
