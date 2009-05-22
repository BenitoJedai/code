using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using Microsoft.CSharp.RuntimeBinder;

namespace ScriptCoreLibAppJet.JavaScript.BCLImplementation.Microsoft.CSharp.RuntimeBinder
{
    [Script(Implements = typeof(global::Microsoft.CSharp.RuntimeBinder.CSharpInvokeMemberBinder))]
    internal class __CSharpInvokeMemberBinder
    {
        public __CSharpInvokeMemberBinder(CSharpCallFlags flags, string name, Type callingContext, IEnumerable<Type> typeArguments, IEnumerable<CSharpArgumentInfo> argumentInfo)
        //: base(name, false, BinderHelper.CreateCallInfo(argumentInfo, 1))
        {





        }
    }
}
