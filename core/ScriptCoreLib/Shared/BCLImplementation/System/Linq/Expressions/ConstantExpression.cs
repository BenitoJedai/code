using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
	// http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Ast/ConstantExpression.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Linq/Expressions/ConstantExpression.cs
	// https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Linq/Expressions/ConstantExpression.cs

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
