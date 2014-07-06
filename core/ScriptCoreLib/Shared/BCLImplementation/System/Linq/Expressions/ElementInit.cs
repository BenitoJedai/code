using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    // http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Ast/ElementInit.cs
    [Script(Implements = typeof(global::System.Linq.Expressions.ElementInit))]
    internal class __ElementInit
    {
        public MethodInfo AddMethod { get; set; }
        public ReadOnlyCollection<Expression> Arguments { get; set; }
    }

}
