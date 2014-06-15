using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using ScriptCoreLib.Extensions;

namespace ComplexQueryExperiment
{
    public partial class ApplicationWebService
    {
        public void WebMethod2()
        {
            // running out of brain power are we?
            // we need a high level overview.
            // all examples at the same time.
            // all tests shall work for sqlite and mysql

            Func<int, int> scalarLambda = xx => xx + 55;


            // Error	1	Could not find an implementation of the query pattern for source type 'ComplexQueryExperiment.xTable'.  'Select' not found.	X:\jsc.svn\examples\javascript\LINQ\ComplexQueryExperiment\ComplexQueryExperiment\ApplicationWebService.WebMethod2.cs	13	31	ComplexQueryExperiment
            var x = from z in new xTable()
                    let field3 = z.field1
                    let field4 = z.field1 + z.field2
                    let field5 = z.field1 + 33

                    where field5 > 44
                    where new { field3 }.field3 > field5

                    let field6 = field3 + field4 + field5
                    let field7 = new { z.field1, field6 }
                    let field8 = "???".ToLower()

                    orderby field8, field7, field6, field5 > 33

                    let scalar1 =
                        from zz in new xTable()
                        select zz

                    let scalar1count = scalar1.Count()

                    let field9 = new { field3, field7, x = new { field4 }, y = new[] { field5, field6 } }


                    //select z.field1;
                    select new { f11 = z.field1, f12 = z.field1, f13 = z.field1 + 3, z };

            //select scalarLambda(field5);

            //select field5 > 44;


            //select new[] {
            //    new { x = field3, z},
            //    new { x= field5, z},
            //};

            //select new
            //{
            //    fieldConstant = "????",
            //    fieldArray = new[] { 11, 22 },
            //    fieldObject = new { field3, field7 },
            //    field9final = field9,
            //    field9x = field9.field3,
            //    z
            //};



            var f = x.FirstOrDefault();
            // order by
            // typed ctor

            Debugger.Break();
        }

        // we need to extract alll above into a hige frikking expression tree
        // we need to be able to encrypt the private state of the query
        // so we could send it to the client for additions?

    }

    static class FrikkingExpressionBuilder
    {
        class SQLWriterContext
        {
            public int LineNumber = 0;
        }

        class SQLWriter
        {

            public SQLWriter(IQueryStrategy source, IEnumerable<IQueryStrategy> upper, SQLWriterContext context = null)
            {
                // selector = {<>h__TransparentIdentifier6 => <>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.z}

                // convert to SQL!

                #region WriteLine
                if (context == null)
                    context = new SQLWriterContext();

                Action<int, string, ConsoleColor> WriteLineWithColor  =
                    (padding, text, c) =>
                    {
                        context.LineNumber++;

                        // what would happen id we did this elsewhere?
                        var f = new StackTrace(fNeedFileInfo: true).GetFrame(1);

                        // http://dev.mysql.com/doc/refman/5.0/en/comments.html
                        var trace = "/* " + f.GetFileLineNumber().ToString().PadLeft(4, '0') + ":" + context.LineNumber.ToString().PadLeft(4, '0') + " */ ";

                        var old = new { Console.ForegroundColor };
                        Console.ForegroundColor = ConsoleColor.Gray;

                        Console.Write(trace + "".PadLeft(upper.Count() + padding, ' '));

                        if (text.StartsWith("let"))
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        else
                            Console.ForegroundColor = old.ForegroundColor;

                        Console.WriteLine(text);
                        Console.ForegroundColor = old.ForegroundColor;

                    };
                #endregion

                Action<int, string> WriteLine =
                    (padding, text, c) =>
                    {

                    };

                #region xOrderBy
                var xOrderBy = source as xOrderBy;
                if (xOrderBy != null)
                {
                    var sql = new SQLWriter(xOrderBy.source, upper.Concat(new[] { source }), context);

                    foreach (var item in xOrderBy.keySelector)
                    {
                        WriteLine(0, "orderby ?");
                    }
                    return;
                }
                #endregion

                #region xWhere
                var xWhere = source as xWhere;
                if (xWhere != null)
                {
                    var sql = new SQLWriter(xWhere.source, upper.Concat(new[] { source }), context);

                    foreach (var item in xWhere.filter)
                    {
                        WriteLine(0, "where ?");
                    }
                    return;
                }
                #endregion


                // what are looking at? select selector will know the name of the source
                // (source as xSelect).selector.Body = {<>h__TransparentIdentifier6 . <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.z}
                var x = (source as xSelect).selector.Body;

                WriteLine(0, "select");

                var sInvocationExpression = (source as xSelect).selector.Body as InvocationExpression;
                if (sInvocationExpression != null)
                {
                    // that lambda shall be run during readout, not within db
                    WriteLine(1, "f(?)");
                }
                else
                {
                    var sMemberExpression = (source as xSelect).selector.Body as MemberExpression;
                    if (sMemberExpression != null)
                    {
                        // the parameter name is not entirely correct. we need a flattened projection instead.
                        WriteLine(1, (source as xSelect).selector.Parameters[0].Name + "::" + sMemberExpression.Member.Name);
                    }
                    else
                    {
                        // look we can select array from single row, in db this would be a union. for reading the data out, we just need to prefix with index.

                        var xBinaryExpression = (source as xSelect).selector.Body as BinaryExpression;
                        if (xBinaryExpression != null)
                        {
                            // scalar select
                            // this will look like roslyn dictionary indexer initializer
                            WriteLine(1, ("? > ?"));
                        }
                        else
                        {

                            var xNewArrayExpression = (source as xSelect).selector.Body as NewArrayExpression;
                            if (xNewArrayExpression != null)
                            {
                                // this will look like roslyn dictionary indexer initializer
                                Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + ("new [] "));
                                Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + (" [0] <- ?"));
                                Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + (" [1] <- ?"));
                            }
                            else
                            {
                                var xNewExpression = (source as xSelect).selector.Body as NewExpression;
                                if (xNewExpression != null)
                                {
                                    // we are selecting a group for upper select arent we.


                                    foreach (var item in xNewExpression.Arguments.Zip(xNewExpression.Members, (a, m) => new { a, m }))
                                    {
                                        #region xxMethodCallExpression
                                        var xxMethodCallExpression = item.a as MethodCallExpression;
                                        if (xxMethodCallExpression != null)
                                        {
                                            // whatif its a nested query?



                                            WriteLine(1, ("let " + item.m.Name) + " <- " + xxMethodCallExpression.Method.Name + "(?)");
                                            continue;
                                        }
                                        #endregion


                                        #region xBinaryExpression
                                        var xxBinaryExpression = item.a as BinaryExpression;
                                        if (xxBinaryExpression != null)
                                        {
                                            WriteLine(1, ("let " + item.m.Name) + " <- ? + ?");
                                            continue;
                                        }
                                        #endregion

                                        #region xConstantExpression
                                        var xConstantExpression = item.a as ConstantExpression;
                                        if (xConstantExpression != null)
                                        {
                                            //Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + ("? as " + item.m.Name + "+*"));
                                            WriteLine(1, ("let " + item.m.Name) + " <- constant");
                                            continue;
                                        }
                                        #endregion


                                        #region xParameterExpression
                                        var xParameterExpression = item.a as ParameterExpression;
                                        if (xParameterExpression != null)
                                        {
                                            // + (source as xSelect).selector.Parameters[0].Name);
                                            // whats available for proxy? whats used from upper?

                                            var upper_xSelect = (upper.Last() as xSelect);

                                            // as above so below
                                            // skip orderby and where?
                                            if (upper_xSelect != null)
                                                WriteLine(1, "let " + (upper.Last() as xSelect).selector.Parameters[0].Name + "." + item.m.Name + " <- " + xParameterExpression.Name);
                                            else
                                                WriteLine(1, "let ?." + item.m.Name + " <- " + xParameterExpression.Name);
                                            //WriteLine(1, (xParameterExpression.Name + "+* as " + item.m.Name + "+*"));
                                            continue;
                                        }
                                        #endregion

                                        #region xxNewArrayExpression
                                        var xxNewArrayExpression = item.a as NewArrayExpression;
                                        if (xxNewArrayExpression != null)
                                        {
                                            WriteLine(1, ("let " + item.m.Name) + " <- new[]{?}");
                                            continue;
                                        }
                                        #endregion


                                        #region xxNewExpression
                                        var xxNewExpression = item.a as NewExpression;
                                        if (xxNewExpression != null)
                                        {
                                            // let

                                            WriteLine(1, ("let " + item.m.Name) + " <- ?");
                                            //Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + ("?:* as " + item.m.Name));


                                            //Debugger.Break();
                                            continue;
                                        }
                                        #endregion

                                        #region xMemberExpression
                                        var xMemberExpression = item.a as MemberExpression;
                                        if (xMemberExpression != null)
                                        {
                                            // we are selecting a field. was it flattened for us?
                                            // xMemberExpression = {<>h__TransparentIdentifier6.field9}

                                            var xMMemberExpression = xMemberExpression.Expression as MemberExpression;
                                            if (xMMemberExpression != null)
                                            {
                                                // we are selecting only one field of a datagroup
                                                var xMParameterExpression = xMMemberExpression.Expression as ParameterExpression;

                                                // assume non scalar, members and array elements by default

                                                if (xMParameterExpression == null)
                                                {
                                                    WriteLine(1, ("let " + item.m.Name) + " <- ?");
                                                    //Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + "? as " + item.m.Name + "+*");
                                                }
                                                else
                                                {
                                                    WriteLine(1, xMParameterExpression.Name + ":" + xMMemberExpression.Member.Name + "." + (xMemberExpression.Member.Name + "+* as " + item.m.Name + "+*"));
                                                }
                                            }
                                            else
                                            {

                                                var xMParameterExpression = xMemberExpression.Expression as ParameterExpression;

                                                // assume non scalar, members and array elements by default
                                                WriteLine(1, xMParameterExpression.Name + ":" + (xMemberExpression.Member.Name + "+* as " + item.m.Name + "+*"));
                                            }

                                            continue;
                                        }
                                        #endregion


                                        Debugger.Break();
                                    }




                                }
                                else Debugger.Break();
                            }
                        }
                    }
                }

                if ((source as xSelect).source == null)
                {
                    // there needs to be an external source. a table in db?

                    WriteLine(0, "from xTable as " + (source as xSelect).selector.Parameters[0].Name);
                }
                else
                {
                    WriteLine(0, "from (");

                    var xsource = new SQLWriter(
                        (source as xSelect).source,
                        upper.Concat(new[] { source }),
                        context
                    );

                    // render the source and with parent


                    WriteLine(0, ") as " + (source as xSelect).selector.Parameters[0].Name);
                }

                //Debugger.Break();
            }
        }

        public static TElement FirstOrDefault<TElement>(this IQueryStrategy<TElement> source)
        {
            // cache it?
            var sql = new SQLWriter(source, new IQueryStrategy[0].AsEnumerable());


            return default(TElement);
        }





        class xWhere
        {
            public IQueryStrategy source;
            public IEnumerable<Expression> filter;

        }

        class xWhere<TElement> : xWhere, IQueryStrategy<TElement>
        {

        }

        // called by LINQ
        public static IQueryStrategy<TElement> Where<TElement>(this IQueryStrategy<TElement> source, Expression<Func<TElement, bool>> filter)
        {
            var xWhere = source as xWhere;
            if (xWhere != null)
            {
                // flatten where
                return new xWhere<TElement>
                {
                    source = xWhere.source,
                    filter = xWhere.filter.Concat(new[] { filter })
                };
            }

            return new xWhere<TElement>
            {
                source = source,
                filter = new[] { filter }
            };
        }

        // allow xTable to predefine a select
        public class xSelect
        {
            public IQueryStrategy source;
            public LambdaExpression selector;
        }

        class xSelect<TResult> : xSelect, IQueryStrategy<TResult>
        {

        }

        // called by LINQ
        public static IQueryStrategy<TResult> Select<TSource, TResult>(this IQueryStrategy<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            return new xSelect<TResult>
            {
                source = source,
                selector = selector
            };
        }





        public class xOrderBy
        {
            public IQueryStrategy source;


            public IEnumerable<Expression> keySelector;
        }

        public class xOrderBy<TElement> : xOrderBy, IQueryStrategy<TElement>
        {
        }

        public static IQueryStrategy<TElement> ThenBy<TElement, TKey>(this IQueryStrategy<TElement> source, Expression<Func<TElement, TKey>> keySelector)
        {
            var xOrderBy = source as xOrderBy;
            if (xOrderBy != null)
            {
                // flatten orderbys
                return new xOrderBy<TElement>
                {
                    source = xOrderBy.source,
                    keySelector = xOrderBy.keySelector.Concat(new[] { keySelector })
                };
            }

            return new xOrderBy<TElement>
            {
                source = source,
                keySelector = new[] { keySelector }
            };
        }

        //[Obsolete("mutable")]
        public static IQueryStrategy<TElement> OrderBy<TElement, TKey>(this IQueryStrategy<TElement> source, Expression<Func<TElement, TKey>> keySelector)
        {
            return new xOrderBy<TElement>
            {
                source = source,
                keySelector = new[] { keySelector }
            };
        }

        // first lets allow scalar subqueries
        public static long Count(this IQueryStrategy Strategy)
        {
            return 0;
        }
    }
}
