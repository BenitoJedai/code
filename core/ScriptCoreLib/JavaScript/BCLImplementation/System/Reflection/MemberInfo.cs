using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.MemberInfo))]
    internal abstract class __MemberInfo
    {
        public virtual string Name
        {
            get
            {
                throw new Exception();
            }
        }

    }
}
