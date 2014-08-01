using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Dynamic
{
    // http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Actions/InvokeMemberBinder.cs

    [Script(Implements = typeof(global::System.Dynamic.InvokeMemberBinder))]
    public class __InvokeMemberBinder : __DynamicMetaObjectBinder
    {
        public Type ReturnType { get; set; }

        public CSharpBinderFlags flags;


        public string Name { get; set; }

        public IEnumerable<Type> typeArguments;
        public Type context;
        public IEnumerable<CSharpArgumentInfo> argumentInfo;



        public override string ToString()
        {
            return "InvokeMemberBinder";
        }
    }
}
