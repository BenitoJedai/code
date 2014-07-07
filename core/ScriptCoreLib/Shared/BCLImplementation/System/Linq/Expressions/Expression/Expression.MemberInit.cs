using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    internal abstract partial class __Expression
    {
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Expressions\MemberInitExpression.cs

        public static MemberInitExpression MemberInit(NewExpression NewExpression, MemberBinding[] Bindings)
        {
            //Console.WriteLine("MemberInit " + new { NewExpression });

            return
                (MemberInitExpression)(object)
                new __MemberInitExpression
                {

                    // ??
                    NodeType = ExpressionType.MemberInit,

                    NewExpression = NewExpression,
                    Bindings = new global::System.Collections.ObjectModel.ReadOnlyCollection<MemberBinding>(Bindings.ToList())
                };
        }





    }
}
