using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    [Script(Implements = typeof(global::System.Linq.Expressions.NewExpression))]
    internal class __NewExpression : __Expression
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression

        public ReadOnlyCollection<Expression> Arguments { get; set; }
        public ConstructorInfo Constructor { get; set; }
        public ReadOnlyCollection<MemberInfo> Members { get; set; }


        public override string ToString()
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
            //Console.WriteLine("Expression.New " + new { constructor, constructor.DeclaringType });

            return "NewExpression " + new { Constructor, Type }.ToString();
        }

    }

}
