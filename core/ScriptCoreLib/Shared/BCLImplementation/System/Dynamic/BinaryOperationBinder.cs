using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Dynamic
{
    // http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Actions/BinaryOperationBinder.cs
    // https://github.com/mono/mono/blob/master/mcs/class/dlr/Runtime/Microsoft.Scripting.Core/Actions/BinaryOperationBinder.cs

    [Script(Implements = typeof(global::System.Dynamic.BinaryOperationBinder))]
    public class __BinaryOperationBinder : __DynamicMetaObjectBinder
    {
        // X:\jsc.svn\examples\javascript\Test\TestUnaryOperation\TestUnaryOperation\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs

        public CSharpBinderFlags flags;
        public ExpressionType operation;
        public Type context;
        public IEnumerable<CSharpArgumentInfo> argumentInfo;

        public override string ToString()
        {
            return "BinaryOperationBinder";
        }
    }
}
