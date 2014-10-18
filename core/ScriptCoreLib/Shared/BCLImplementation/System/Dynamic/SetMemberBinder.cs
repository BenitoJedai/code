using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Dynamic
{
    // http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Actions/SetMemberBinder.cs

    [Script(Implements = typeof(global::System.Dynamic.SetMemberBinder))]
    public class __SetMemberBinder : __DynamicMetaObjectBinder
    {
        public CSharpBinderFlags flags;

      
        public Type context;
        public IEnumerable<CSharpArgumentInfo> argumentInfo;

        public string Name { get; set; }


        public override string ToString()
        {
            return "SetMemberBinder";
        }
    }
}
