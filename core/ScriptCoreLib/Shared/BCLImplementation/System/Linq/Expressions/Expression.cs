using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    [Script(Implements = typeof(global::System.Linq.Expressions.Expression))]
    internal abstract class __Expression
    {
        // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\ApplicationWebService.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121101/20121127
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression


        public static MemberExpression Field(Expression expression, FieldInfo field)
        {
            return
                (MemberExpression)(object)
                new __MemberExpression();

        }

        public static ParameterExpression Parameter(Type type, string name)
        {
            return
                (ParameterExpression)(object)
                new __ParameterExpression();
        }


        public static Expression<TDelegate> Lambda<TDelegate>(Expression body, params ParameterExpression[] parameters)
        {
            return
                 (Expression<TDelegate>)(object)
                 new __Expression<TDelegate>();
        }
    }

    [Script(Implements = typeof(global::System.Linq.Expressions.Expression<>))]
    internal sealed class __Expression<TDelegate> : __LambdaExpression
    {
        //Error	71	The type 'System.Linq.Expressions.LambdaExpression' has no constructors defined	X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Expressions\Expression.cs	43	27	ScriptCoreLib

    }
}
