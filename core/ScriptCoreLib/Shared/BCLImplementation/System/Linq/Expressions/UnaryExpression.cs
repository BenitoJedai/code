using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    // http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Ast/UnaryExpression.cs
    [Script(Implements = typeof(global::System.Linq.Expressions.UnaryExpression))]
    internal class __UnaryExpression : __Expression
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression

        public Expression Operand { get; set; }
        public MethodInfo Method { get; set;  }

        public override string ToString()
        {
            return "UnaryExpression " + new { Operand, Type }.ToString();
        }

    }

}
