using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    // http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Ast/ParameterExpression.cs
    [Script(Implements = typeof(global::System.Linq.Expressions.ParameterExpression))]
    internal class __ParameterExpression : __Expression
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression

        public string Name { get; set; }

        public override string ToString()
        {
            return "ParameterExpression " + new { Type, Name }.ToString();
        }

    }

}
