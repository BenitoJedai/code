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
        // x:\jsc.svn\examples\javascript\linq\test\auto\testselect\jvmclrsyntaxorderbythengroupby\program.cs

        [Obsolete("should we construct a property from the getter?")]
        public static MemberExpression Property(Expression expression, MethodInfo member)
        {
            // Property { expression = ParameterExpression { Type = __AnonymousTypes__TestSQLJoin_ApplicationWebService.__f__AnonymousType_682_0_2, Name = <>h__TransparentIdentifier0 }, member = java.lang.Object get_dealer() }

            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteGroupBy\TestSQLiteGroupBy\ApplicationWebService.cs
            //Console.WriteLine("Property " + new { expression, member });

            return
                (MemberExpression)(object)
                new __MemberExpression
                {

                    // ??
                    NodeType = ExpressionType.MemberInit,

                    Expression = expression,
                    Member = member
                };
        }


    }
}
