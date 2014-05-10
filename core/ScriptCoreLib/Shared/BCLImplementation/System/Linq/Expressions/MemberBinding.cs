using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    [Script(Implements = typeof(global::System.Linq.Expressions.MemberBinding))]
    internal abstract class __MemberBinding
    {
        public MemberBindingType BindingType { get; set; }
        public MemberInfo Member { get; set; }

    }

}
