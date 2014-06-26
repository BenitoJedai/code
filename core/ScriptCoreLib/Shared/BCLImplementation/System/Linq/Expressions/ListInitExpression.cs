using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    [Script(Implements = typeof(global::System.Linq.Expressions.ListInitExpression))]
    internal class __ListInitExpression : __Expression
    {
        //no implementation for System.Linq.Expressions.ListInitExpression 5d5caf46-34f9-3338-97c5-3fe54aa65cd0
        //script: error JSC1000: No implementation found for this native method, please implement [System.Linq.Expressions.ListInitExpression.get_NewExpression()]


        public ReadOnlyCollection<ElementInit> Initializers { get; set; }
        public NewExpression NewExpression { get; set; }

        public override string ToString()
        {
            return "ListInitExpression " + new { NewExpression }.ToString();
        }

    }

}
