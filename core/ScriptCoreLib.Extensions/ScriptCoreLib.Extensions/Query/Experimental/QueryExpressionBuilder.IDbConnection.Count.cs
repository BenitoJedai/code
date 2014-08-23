using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Data;
using System.Threading.Tasks;
using System.Data.Common;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Insert.cs
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Count.cs

        public static class xReferencesOfLong
        {
            // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRGenericField\TestJVMCLRGenericField\Program.cs
            // java will not like static generic fields.
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140810

            public static readonly Func<IQueryStrategy<object>, long> CountReference = Count;
            public static readonly Func<IQueryStrategy<object>, Expression<Func<object, bool>>, IQueryStrategy<object>> WhereReference = Where;
            public static readonly Func<IQueryStrategy<object>, Expression<Func<object, object>>, IQueryStrategy<object>> SelectReference = Select;


            public static readonly Func<IQueryStrategy<object>, Expression<Func<object, object>>, IQueryStrategy<object>> ThenByReference = ThenBy;
            public static readonly Func<IQueryStrategy<object>, Expression<Func<object, object>>, IQueryStrategy<object>> ThenByDescendingReference = ThenByDescending;
            public static readonly Func<IQueryStrategy<object>, Expression<Func<object, object>>, IQueryStrategy<object>> OrderByReference = OrderBy;
            public static readonly Func<IQueryStrategy<object>, Expression<Func<object, object>>, IQueryStrategy<object>> OrderByDescendingReference = OrderByDescending;

            public static readonly Func<IQueryStrategy<object>, Expression<Func<object, object>>, IQueryStrategy<IQueryStrategyGrouping<object, object>>> GroupByReference = GroupBy;

            public static readonly Func<IQueryStrategy<object>, object> FirstOrDefaultReference = FirstOrDefault;
            public static readonly Func<IQueryStrategyGrouping<long, object>, object> LastReference = Last;

            public static readonly Func<
                IQueryStrategy<object>,
                IQueryStrategy<object>,
                Expression<Func<object, object>>,
                Expression<Func<object, object>>,
                Expression<Func<object, object, object>>,
                IQueryStrategy<object>
            > JoinReference = Join;


            public static readonly Func<IQueryStrategy<long>, long> SumOfLongReference = Sum;
            public static readonly Func<IQueryStrategy<long>, long> MinOfLongReference = Min;
            public static readonly Func<IQueryStrategy<long>, long> MaxOfLongReference = Max;

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectAverage\Program.cs
            public static readonly Func<IQueryStrategy<long>, double> AverageOfLongReference = Average;

            // X:\jsc.svn\examples\javascript\Test\TestPropertyGetMethodExpression\TestPropertyGetMethodExpression\Application.cs




            // how do we get the MethodRef from the expression above?

            [Obsolete("Expression<> is to large to be set as fieldinit..")]
            public static MemberInfo KeyReference
            {
                get
                {
                    // x:\jsc.svn\examples\javascript\linq\test\auto\testselect\syntaxorderbythengroupby\program.cs

                    Expression<
                        Func<IQueryStrategyGrouping<long, object>, long>
                        > KeyExpression =
                            x => x.Key;

                    return ((MemberExpression)KeyExpression.Body).Member;
                }
            }

            //public final static __MemberInfo KeyReference = ((__MemberExpression)QueryExpressionBuilder_xReferencesOfLong.KeyExpression.get_Body()).get_Member();
            //public final static __Expression_1<__Func_2<QueryExpressionBuilder_IQueryStrategyGrouping_2<Long, Object>, Long>> KeyExpression = __Expression.<__Func_2<QueryExpressionBuilder_IQueryStrategyGrouping_2<Long, Object>, Long>>Lambda_060005b5(expression1, expressionArray2);

            //static
            //{
            //    __ParameterExpression expression0;
            //    __MemberExpression expression1;
            //    __ParameterExpression[] expressionArray2;

            //    expression0 = __Expression.Parameter(__Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(QueryExpressionBuilder_IQueryStrategyGrouping_2.class)), "x");
            //    expression1 = __Expression.Property(expression0, cctor__0000011f.FromHandle());
            //    expressionArray2 = new __ParameterExpression[] {
            //        expression0
            //    };
            //}
        }



        class xScalar : IQueryStrategy
        {

            public IQueryStrategy source;



            public MethodInfo Operand;

            public override string ToString()
            {
                return "scalar(x)";
            }
        }

        class xScalar<TElement> : xScalar, IQueryStrategy<TElement>
        {

        }

        public static IDbCommand GetScalarCommand<TElement>(
            this IQueryStrategy<TElement> source,
            IDbConnection cc,

            MethodInfo Operand
            )
        {
            Console.WriteLine("enter GetScalarCommand " + new { cc });

            var nsource = new xScalar { source = source, Operand = Operand };
            var Command = default(DbCommand);


            if (cc != null)
            {
                Command = (DbCommand)cc.CreateCommand();
            }
            else
            {
                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestAndroidInsert\ApplicationWebService.cs

                Console.WriteLine("enter GetScalarCommand cc null ?");
            }

            var w = new SQLWriter<TElement>(nsource, new IQueryStrategy[] { nsource }, Command: Command);

            return Command;
        }











    }

}
