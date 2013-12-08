using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    [Script(Implements = typeof(global::System.Linq.Expressions.ConstantExpression))]
    internal class __ConstantExpression : __Expression
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression

        public object Value { get; set; }
        public Type type;

        public override string ToString()
        {
            // 
            return "Constant " + new { value = Value, type }.ToString();
        }

    }

}
