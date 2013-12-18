
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    [Script(Implements = typeof(global::System.Linq.Expressions.LambdaExpression))]
    internal abstract class __LambdaExpression : __Expression
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression


        public Expression Body { get; set; }
    }

}
