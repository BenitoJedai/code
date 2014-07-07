using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    internal abstract partial class __Expression
    {
        public static MemberAssignment Bind(MemberInfo m, Expression e)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140707/xlsx
            // x:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs

            return

            (MemberAssignment)(object)
            new __MemberAssignment
            {
                Member = m,
                Expression = e,

                // ??
                BindingType = MemberBindingType.MemberBinding
            };
        }


    }
}
