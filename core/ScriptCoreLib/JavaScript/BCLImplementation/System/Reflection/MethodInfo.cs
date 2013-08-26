using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.MethodInfo))]
    public class __MethodInfo : __MethodBase
    {
        public global::System.IntPtr InternalMethod;


        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        public override object[] GetCustomAttributes(Type x, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }
    }
}
