using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    [Script(Implements = typeof(global::System.Linq.Expressions.MemberInitExpression))]
    internal class __MemberInitExpression : __Expression
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression

        public ReadOnlyCollection<MemberBinding> Bindings { get; set;  }
        public NewExpression NewExpression { get;set; }

        public override string ToString()
        {
            return "MemberInitExpression " + new { NewExpression  }.ToString();
        }
    }

}
