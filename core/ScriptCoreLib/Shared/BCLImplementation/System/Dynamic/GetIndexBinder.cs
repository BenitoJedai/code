using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Dynamic
{
    [Script(Implements = typeof(global::System.Dynamic.GetIndexBinder))]
    public class __GetIndexBinder : __DynamicMetaObjectBinder
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs

        public CSharpBinderFlags flags;
        public Type context;
        public IEnumerable<CSharpArgumentInfo> argumentInfo;

        // X:\jsc.svn\examples\javascript\test\TestDynamicGetIndex\TestDynamicGetIndex\Application.cs
        // CSharpBinderFlags flags, Type context, IEnumerable<CSharpArgumentInfo> argumentInfo


        //public Type ReturnType { get; set; }

        public override string ToString()
        {
            return "GetIndexBinder";
        }
    }
}
