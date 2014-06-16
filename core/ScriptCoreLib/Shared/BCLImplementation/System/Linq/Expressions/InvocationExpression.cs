using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    [Script(Implements = typeof(global::System.Linq.Expressions.InvocationExpression))]
    internal class __InvocationExpression : __Expression
    {
        // X:\jsc.svn\examples\javascript\LINQ\ComplexQueryExperiment\ComplexQueryExperiment\Application.cs
        public ReadOnlyCollection<Expression> Arguments { get; set;  }


        public override string ToString()
        {
            return "InvocationExpression " + new { Arguments }.ToString();
        }

    }

}
