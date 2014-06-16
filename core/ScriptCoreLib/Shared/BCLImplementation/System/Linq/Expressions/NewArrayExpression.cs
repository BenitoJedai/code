using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    [Script(Implements = typeof(global::System.Linq.Expressions.NewArrayExpression))]
    internal class __NewArrayExpression : __Expression
    {
        public ReadOnlyCollection<Expression> Expressions { get; set; }


        public override string ToString()
        {
            return "NewArrayExpression " + new { Expressions }.ToString();
        }

    }

}
