using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Dynamic
{
    [Script(Implements = typeof(global::System.Dynamic.IDynamicMetaObjectProvider))]
    internal interface __IDynamicMetaObjectProvider
    {
        DynamicMetaObject GetMetaObject(Expression parameter);
    }
}
