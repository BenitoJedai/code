using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Dynamic
{
    [Script(Implements = typeof(global::System.Dynamic.InvokeMemberBinder))]
    internal class __InvokeMemberBinder : __DynamicMetaObjectBinder
    {
        public Type ReturnType { get; set; }

        public CSharpBinderFlags flags;


        public string Name { get; set; }

        public IEnumerable<Type> typeArguments;
        public Type context;
        public IEnumerable<CSharpArgumentInfo> argumentInfo;
    }
}
