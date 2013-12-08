using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    [Script(Implements = typeof(global::System.Linq.Expressions.Expression))]
    internal abstract class __Expression
    {
        // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\ApplicationWebService.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121101/20121127
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression

        //script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Expressions.Expression.Constant(System.Object, System.Type)]
        // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Expressions.Expression.Constant(System.Object)]

        public static ConstantExpression Constant(object value)
        {
            Console.WriteLine("Constant " + new { value });

            return
                (ConstantExpression)(object)
                new __ConstantExpression
                {
                    Value = value,
                };
        }

        public static ConstantExpression Constant(object value, Type type)
        {
            Console.WriteLine("Constant " + new { value, type });

            return
                (ConstantExpression)(object)
                new __ConstantExpression
                {
                    Value = value,
                    type = type,
                };
        }

        public static BinaryExpression Equal(Expression left, Expression right, bool liftToNull, MethodInfo method)
        {

            Console.WriteLine("Equal " + new { left, right, liftToNull, method });

            return
                (BinaryExpression)(object)
                new __BinaryExpression
                {
                    Left = left,
                    Right = right,
                    liftToNull = liftToNull,
                    Method = method
                };
        }

        public static MemberExpression Field(Expression expression, FieldInfo field)
        {
            // ParameterExpression


            Console.WriteLine("Field " + new { expression, field });

            return
                (MemberExpression)(object)
                new __MemberExpression
                {
                    Expression = expression,
                    Member = field
                };

        }

        public static ParameterExpression Parameter(Type type, string name)
        {
            Console.WriteLine("Parameter " + new { type, name });

            return
                (ParameterExpression)(object)
                new __ParameterExpression
                {
                    type = type,
                    name = name
                };
        }


        public static Expression<TDelegate> Lambda<TDelegate>(Expression body, params ParameterExpression[] parameters)
        {
            // MemberExpression


            Console.WriteLine("Lambda " + new { body, parameters });


            return
                 (Expression<TDelegate>)(object)
                 new __Expression<TDelegate>
                 {
                     Body = body,
                     parameters = parameters
                 };
        }
    }

    [Script(Implements = typeof(global::System.Linq.Expressions.Expression<>))]
    internal sealed class __Expression<TDelegate> : __LambdaExpression
    {
        //Error	71	The type 'System.Linq.Expressions.LambdaExpression' has no constructors defined	X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Expressions\Expression.cs	43	27	ScriptCoreLib

        public ParameterExpression[] parameters;

        public override string ToString()
        {
            return new { Body, parameters }.ToString();
        }
    }
}
