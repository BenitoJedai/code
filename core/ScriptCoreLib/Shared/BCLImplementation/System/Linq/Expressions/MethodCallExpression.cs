using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    [Script(Implements = typeof(global::System.Linq.Expressions.MethodCallExpression))]
    internal class __MethodCallExpression : __Expression
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression

        public Expression Object { get; set; }
        public MethodInfo Method { get; set; }

        public ReadOnlyCollection<Expression> Arguments { get; set; }


        //Implementation not found for type import :
        //type: System.Linq.Expressions.MethodCallExpression
        //method: System.Collections.ObjectModel.ReadOnlyCollection`1[System.Linq.Expressions.Expression] get_Arguments()
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!


        public override string ToString()
        {
            return "MethodCallExpression " + new { Object, Method }.ToString();
        }
    }

}
