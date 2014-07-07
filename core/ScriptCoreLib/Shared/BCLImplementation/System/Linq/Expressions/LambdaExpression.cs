
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{

    // Src/Compilers/CSharp/Source/Lowering/LambdaRewriter/ExpressionLambdaRewriter.cs

    // http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Ast/LambdaExpression.cs
    [Script(Implements = typeof(global::System.Linq.Expressions.LambdaExpression))]
    internal abstract class __LambdaExpression : __Expression
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs

        public ReadOnlyCollection<ParameterExpression> Parameters { get; set; }

        public Expression Body { get; set; }


        public static implicit operator LambdaExpression(__LambdaExpression x)
        {
            return (LambdaExpression)(object)x;
        }
    }

}
