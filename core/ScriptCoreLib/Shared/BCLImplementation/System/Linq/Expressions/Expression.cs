using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq.Expressions
{
    [Script(Implements = typeof(global::System.Linq.Expressions.Expression))]
    internal abstract partial class __Expression
    {
        public virtual Type Type { get; set; }

        public virtual ExpressionType NodeType { get; set; }

        // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\ApplicationWebService.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121101/20121127
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140111-iquery

        //V:\web\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Expressions\__Expression.as(76): col: 66 Error: Type was not found or was not a compile-time constant: ConstructorInfo.

        //public static function New_4ebbe596_0600169f(constructor:ConstructorInfo, ___arguments:__IEnumerable_1, members:Array):__NewExpression
        //                                                         ^



        public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments, params MemberInfo[] members)
        {

            return
                (NewExpression)(object)
                new __NewExpression
                {
                    NodeType = ExpressionType.New,

                    Constructor = constructor,
                    Arguments = new global::System.Collections.ObjectModel.ReadOnlyCollection<Expression>(arguments.ToList()),
                    Members = new global::System.Collections.ObjectModel.ReadOnlyCollection<MemberInfo>(members.ToList()),

                };
        }




        //method: System.Linq.Expressions.UnaryExpression Convert(System.Linq.Expressions.Expression, System.Type)
        //public static UnaryExpression Convert(Expression expression, Type type, MethodInfo method);
        public static UnaryExpression Convert(Expression expression, Type type)
        {
            return

                (UnaryExpression)(object)
                new __UnaryExpression
            {
                Type = type,
                Operand = expression
            };
        }

        //script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Expressions.Expression.Constant(System.Object, System.Type)]
        // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Expressions.Expression.Constant(System.Object)]

        //method: System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression, System.Reflection.MethodInfo, System.Linq.Expressions.Expression[])
        public static MethodCallExpression Call(Expression instance, MethodInfo method, params Expression[] arguments)
        {
            //Console.WriteLine("Call " + new { instance, method, arguments });

            return
                (MethodCallExpression)(object)
                new __MethodCallExpression
                {
                    NodeType = ExpressionType.Call,

                    Object = instance,
                    Method = method,
                    Arguments = new global::System.Collections.ObjectModel.ReadOnlyCollection<Expression>(arguments.ToList())
                };
        }

        #region Constant
        public static ConstantExpression Constant(object value)
        {
            //Console.WriteLine("Constant " + new { value });

            return
                (ConstantExpression)(object)
                new __ConstantExpression
                {
                    NodeType = ExpressionType.Constant,

                    Value = value,
                };
        }

        public static ConstantExpression Constant(object value, Type type)
        {
            //Console.WriteLine("Constant " + new { value, type });

            return
                (ConstantExpression)(object)
                new __ConstantExpression
                {
                    NodeType = ExpressionType.Constant,

                    Value = value,
                    type = type,
                };
        }
        #endregion












        public static BinaryExpression GreaterThan(Expression left, Expression right)
        {
            // X:\jsc.svn\examples\javascript\appengine\AppEngineWhereOperator\AppEngineWhereOperator\ApplicationWebService.cs
            return
                 (BinaryExpression)(object)
                 new __BinaryExpression
                 {
                     NodeType = ExpressionType.GreaterThan,

                     Left = left,
                     Right = right,
                 };
        }



        public static BinaryExpression LessThan(Expression left, Expression right)
        {
            // X:\jsc.svn\examples\javascript\appengine\AppEngineWhereOperator\AppEngineWhereOperator\ApplicationWebService.cs
            return
                 (BinaryExpression)(object)
                 new __BinaryExpression
                 {
                     NodeType = ExpressionType.LessThan,

                     Left = left,
                     Right = right,
                 };
        }


        //method: System.Linq.Expressions.BinaryExpression Equal(System.Linq.Expressions.Expression, System.Linq.Expressions.Expression)
        public static BinaryExpression Equal(Expression left, Expression right)
        {
            return
                 (BinaryExpression)(object)
                 new __BinaryExpression
                 {
                     // when is this used?
                     NodeType = ExpressionType.Equal,

                     Left = left,
                     Right = right,
                     //liftToNull = liftToNull,
                     //Method = method
                 };
        }

        public static BinaryExpression Equal(Expression left, Expression right, bool liftToNull, MethodInfo method)
        {

            //Console.WriteLine("Equal " + new { left, right, liftToNull, method });

            return
                (BinaryExpression)(object)
                new __BinaryExpression
                {
                    // when is this used?
                    NodeType = ExpressionType.Equal,

                    Left = left,
                    Right = right,
                    liftToNull = liftToNull,
                    Method = method
                };
        }

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

        public static ParameterExpression Parameter(Type type, string name)
        {
            //Console.WriteLine("Parameter " + new { type, name });

            return
                (ParameterExpression)(object)
                new __ParameterExpression
                {
                    NodeType = ExpressionType.Parameter,

                    type = type,
                    name = name
                };
        }


        public static Expression<TDelegate> Lambda<TDelegate>(Expression body, params ParameterExpression[] parameters)
        {
            // MemberExpression


            //Console.WriteLine("Lambda " + new { body, parameters });


            return
                 (Expression<TDelegate>)(object)
                 new __Expression<TDelegate>
                 {
                     NodeType = ExpressionType.Lambda,

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
