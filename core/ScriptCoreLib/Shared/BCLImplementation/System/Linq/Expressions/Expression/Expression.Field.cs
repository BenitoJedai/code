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
        public static MemberExpression Field(Expression expression, FieldInfo field)
        {
            // ParameterExpression


            //Console.WriteLine("Field " + new { expression, field });

            return
                (MemberExpression)(object)
                new __MemberExpression
                {
                    NodeType = ExpressionType.MemberAccess,


                    Expression = expression,
                    Member = field
                };

        }


    }
}
