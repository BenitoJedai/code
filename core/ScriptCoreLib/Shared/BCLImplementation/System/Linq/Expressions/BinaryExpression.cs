using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140714
    // X:\opensource\github\WootzJs\WootzJs.Runtime\Linq\Expressions\BinaryExpression.cs
    // http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Ast/BinaryExpression.cs
    // https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/dlr/Runtime/Microsoft.Scripting.Core/Ast/BinaryExpression.cs


    [Script(Implements = typeof(global::System.Linq.Expressions.BinaryExpression))]
    internal class __BinaryExpression : __Expression
    {
        // created by?

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression

        public Expression Left { get; set; }
        public Expression Right { get; set; }
        public bool liftToNull;
        public MethodInfo Method { get; set; }

        public override string ToString()
        {
            return "BinaryExpression " + new { left = Left, right = Right, liftToNull, method = Method }.ToString().Replace(",", ",\n");
        }

    }

}
