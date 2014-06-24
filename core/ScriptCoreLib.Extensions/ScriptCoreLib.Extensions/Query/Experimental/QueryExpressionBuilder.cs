using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;

namespace ScriptCoreLib.Query.Experimental
{
    #region example generated data layer
    public class xTable : QueryExpressionBuilder.xSelect<xRow>
    {
        public xTable()
        {

            Expression<Func<xRow, xRow>> selector = (xTableDefaultSelector) => new xRow
            {
                Key = xTableDefaultSelector.Key,
                field1 = xTableDefaultSelector.field1,
                field2 = xTableDefaultSelector.field2,
                Timestamp = xTableDefaultSelector.Timestamp,
                Tag = xTableDefaultSelector.Tag
            };

            this.selector = selector;
        }
    }


    public enum xKey : long { }

    public class xRow
    {
        public xKey Key;

        public int field1;
        public int field2;

        public DateTime Timestamp;
        public string Tag;

    }
    #endregion


    [Obsolete("name clash with the old version. check namespaces!")]
    public interface IQueryStrategy
    {


    }

    [Obsolete("name clash with the old version. check namespaces!")]
    public interface IQueryStrategy<TElementType> : IQueryStrategy
    {
    }

    [Obsolete("this is the refactored version of the secured linq to sql.")]
    public static partial class QueryExpressionBuilder
    {
        // tested by
        // X:\jsc.svn\examples\javascript\LINQ\ComplexQueryExperiment\ComplexQueryExperiment\ApplicationWebService.cs

        // we need to extract alll above into a hige frikking expression tree
        // we need to be able to encrypt the private state of the query
        // so we could send it to the client for additions?

        class SQLWriterContext
        {
            public int LineNumber = 0;
        }

        class SQLWriterWithoutLinefeeds : IDisposable
        {
            public Action yield;
            public void Dispose()
            {
                yield();
            }

        }

        partial class SQLWriter<TElement>
        {

            public SQLWriter(IQueryStrategy source, IEnumerable<IQueryStrategy> upper, SQLWriterContext context = null, ParameterExpression upperParameter = null)
            {
                //Console.Clear();

                // selector = {<>h__TransparentIdentifier6 => <>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.z}

                // convert to SQL!

                var xSelect = source as xSelect;


                if (context == null)
                    context = new SQLWriterContext();

                #region WithoutLinefeeds
                var WithoutLinefeedsCounter = 0;
                var WithoutLinefeedsDirty = false;
                Func<IDisposable> WithoutLinefeeds =
                    delegate
                    {
                        if (WithoutLinefeedsCounter == 0)
                            WithoutLinefeedsDirty = false;

                        WithoutLinefeedsCounter++;

                        return new SQLWriterWithoutLinefeeds
                        {
                            yield = delegate
                            {
                                WithoutLinefeedsCounter--;

                                if (WithoutLinefeedsCounter == 0)
                                    Console.WriteLine();
                            }
                        };
                    };
                #endregion

                #region WriteCommentLine
                Action<int, string> WriteCommentLine =
                  (padding, text) =>
                  {
                      var c = ConsoleColor.Green;

                      var old = new { Console.ForegroundColor };

                      if (WithoutLinefeedsCounter == 0 || !WithoutLinefeedsDirty)
                      {
                          WithoutLinefeedsDirty = true;
                          context.LineNumber++;

                          // what would happen id we did this elsewhere?
                          var f = new StackTrace(fNeedFileInfo: true).GetFrame(1);
                          var FileLineNumber = f.GetFileLineNumber();
                          // http://dev.mysql.com/doc/refman/5.0/en/comments.html
                          var trace = "/* " + FileLineNumber.ToString().PadLeft(4, '0') + ":" + context.LineNumber.ToString().PadLeft(4, '0') + " */ ";

                          Console.ForegroundColor = ConsoleColor.Gray;
                          Console.Write(trace + "".PadLeft(upper.Count() + padding, ' '));

                      }


                      Console.ForegroundColor = c;
                      Console.Write("/* " + text + " */");
                      Console.ForegroundColor = old.ForegroundColor;

                      if (WithoutLinefeedsCounter == 0)
                      {
                          Console.WriteLine();
                      }

                  };
                #endregion

                #region WriteLineWithColor
                Action<int, string, ConsoleColor> WriteLineWithColor =
                    (padding, text, c) =>
                    {
                        var old = new { Console.ForegroundColor };

                        if (WithoutLinefeedsCounter == 0 || !WithoutLinefeedsDirty)
                        {
                            WithoutLinefeedsDirty = true;
                            context.LineNumber++;

                            // what would happen id we did this elsewhere?
                            var f = new StackTrace(fNeedFileInfo: true).GetFrame(1);
                            var FileLineNumber = f.GetFileLineNumber();
                            // http://dev.mysql.com/doc/refman/5.0/en/comments.html
                            var trace = "/* " + FileLineNumber.ToString().PadLeft(4, '0') + ":" + context.LineNumber.ToString().PadLeft(4, '0') + " */ ";

                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(trace + "".PadLeft(upper.Count() + padding, ' '));

                        }


                        Console.ForegroundColor = c;
                        Console.Write(text);
                        Console.ForegroundColor = old.ForegroundColor;

                        if (WithoutLinefeedsCounter == 0)
                        {
                            Console.WriteLine();
                        }

                    };
                #endregion

                #region WriteLine
                Action<int, string> WriteLine =
                    (padding, text) =>
                    {
                        var old = new { Console.ForegroundColor };

                        if (WithoutLinefeedsCounter == 0 || !WithoutLinefeedsDirty)
                        {
                            WithoutLinefeedsDirty = true;
                            context.LineNumber++;

                            // what would happen id we did this elsewhere?
                            var f = new StackTrace(fNeedFileInfo: true).GetFrame(1);

                            var FileLineNumber = f.GetFileLineNumber();
                            // http://dev.mysql.com/doc/refman/5.0/en/comments.html
                            var trace = "/* " + FileLineNumber.ToString().PadLeft(4, '0') + ":" + context.LineNumber.ToString().PadLeft(4, '0') + " */ ";

                            Console.ForegroundColor = ConsoleColor.Gray;

                            Console.Write(trace + "".PadLeft(upper.Count() + padding, ' '));


                            if (text.StartsWith("let"))
                            {
                                if (upper.Any())
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                else
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                            }
                            else
                                Console.ForegroundColor = old.ForegroundColor;
                        }


                        Console.Write(text);
                        Console.ForegroundColor = old.ForegroundColor;

                        if (WithoutLinefeedsCounter == 0)
                        {
                            Console.WriteLine();
                        }


                    };


                #endregion

                #region WriteScalarExpression
                Action<Expression> WriteScalarExpression = null;

                WriteScalarExpression =
                    (asExpression) =>
                    {
                        // zExpression = {zz => (Convert(zz.Key) == 77)}

                        #region WriteScalarExpression:asBinaryExpression
                        var asBinaryExpression = asExpression as BinaryExpression;
                        if (asBinaryExpression != null)
                        {
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectBinaryExpression\Program.cs

                            using (WithoutLinefeeds())
                            {

                                WriteLineWithColor(1, "(", ConsoleColor.White);
                                WriteScalarExpression(asBinaryExpression.Left);


                                #region ExpressionType
                                if (asBinaryExpression.NodeType == ExpressionType.Equal)
                                    WriteLineWithColor(1, " = ", ConsoleColor.White);
                                else if (asBinaryExpression.NodeType == ExpressionType.LessThan)
                                    WriteLineWithColor(1, " < ", ConsoleColor.White);
                                else if (asBinaryExpression.NodeType == ExpressionType.LessThanOrEqual)
                                    WriteLineWithColor(1, " <= ", ConsoleColor.White);
                                else if (asBinaryExpression.NodeType == ExpressionType.GreaterThan)
                                    WriteLineWithColor(1, " > ", ConsoleColor.White);
                                else if (asBinaryExpression.NodeType == ExpressionType.GreaterThanOrEqual)
                                    WriteLineWithColor(1, " >= ", ConsoleColor.White);
                                else if (asBinaryExpression.NodeType == ExpressionType.NotEqual)
                                    WriteLineWithColor(1, " <> ", ConsoleColor.White);
                                else if (asBinaryExpression.NodeType == ExpressionType.OrElse)
                                    WriteLineWithColor(1, " or ", ConsoleColor.White);
                                else if (asBinaryExpression.NodeType == ExpressionType.AndAlso)
                                    WriteLineWithColor(1, " and ", ConsoleColor.White);

                                // X:\jsc.svn\examples\javascript\linq\test\TestWhereMathAdd\TestWhereMathAdd\ApplicationWebService.cs
                                else if (asBinaryExpression.NodeType == ExpressionType.Add)
                                    WriteLineWithColor(1, " + ", ConsoleColor.White);
                                else if (asBinaryExpression.NodeType == ExpressionType.Subtract)
                                    WriteLineWithColor(1, " - ", ConsoleColor.White);
                                else if (asBinaryExpression.NodeType == ExpressionType.Multiply)
                                    WriteLineWithColor(1, " * ", ConsoleColor.White);
                                else if (asBinaryExpression.NodeType == ExpressionType.Divide)
                                    WriteLineWithColor(1, " / ", ConsoleColor.White);

                                else
                                    Debugger.Break();
                                #endregion

                                WriteScalarExpression(asBinaryExpression.Right);
                                WriteLineWithColor(1, ")", ConsoleColor.White);
                            }

                            return;
                        }
                        #endregion

                        #region WriteScalarExpression::UnaryExpression
                        var asUnaryExpression = asExpression as UnaryExpression;
                        if (asUnaryExpression != null)
                        {
                            if (asUnaryExpression.NodeType == ExpressionType.Convert)
                            {
                                // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOrUnaryExpression\TestSelectOrUnaryExpression\ApplicationWebService.cs
                                //state.WriteExpression(ref s, asUnaryExpression.Operand, that);

                                WriteScalarExpression(asUnaryExpression.Operand);
                                return;
                            }
                            else if (asUnaryExpression.NodeType == ExpressionType.Not)
                            {

                                WriteLineWithColor(1, "not(", ConsoleColor.White);

                                WriteScalarExpression(asUnaryExpression.Operand);

                                WriteLineWithColor(1, ")", ConsoleColor.White);
                                return;
                            }
                            else Debugger.Break();

                            return;
                        }
                        #endregion

                        #region WriteScalarExpression:zMemberExpression
                        var zMemberExpression = asExpression as MemberExpression;
                        if (zMemberExpression != null)
                        {
                            using (WithoutLinefeeds())
                            {
                                var zMMemberExpression = zMemberExpression.Expression as MemberExpression;
                                if (zMMemberExpression != null)
                                {
                                    WriteLineWithColor(2, zMMemberExpression.Member.Name + ".", ConsoleColor.Magenta);
                                }


                                var zMParameterExpression = zMemberExpression.Expression as ParameterExpression;
                                if (zMParameterExpression != null)
                                {
                                    WriteLineWithColor(2, zMParameterExpression.Name + ".", ConsoleColor.Magenta);
                                }

                                WriteLineWithColor(2, zMemberExpression.Member.Name, ConsoleColor.Cyan);
                            }

                            return;
                        }
                        #endregion

                        #region WriteScalarExpression:xConstantExpression
                        var xConstantExpression = asExpression as ConstantExpression;
                        if (xConstantExpression != null)
                        {
                            //Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + ("? as " + item.m.Name + "+*"));
                            //WriteLine(1, "@constant " + new { xConstantExpression.Value });
                            WriteLineWithColor(1, "@arg(" + xConstantExpression.Value + ")", ConsoleColor.Red);
                            return;
                        }
                        #endregion

                        #region WriteScalarExpression:xMethodCallExpression
                        var xMethodCallExpression = asExpression as MethodCallExpression;
                        if (xMethodCallExpression != null)
                        {
                            // can we do a count in where yet?
                            // xMethodCallExpression = {new xTable().Where(zz => (zz.field1 < 33)).Count()}
                            WriteLine(1, xMethodCallExpression.Method.Name + "()");
                            return;
                        }
                        #endregion


                        Debugger.Break();
                    };
                #endregion

                #region xOrderBy
                var xOrderBy = source as xOrderBy;
                if (xOrderBy != null)
                {
                    var sql = new SQLWriter<TElement>(xOrderBy.source, upper.Concat(new[] { source }), context);


                    xOrderBy.keySelector.WithEachIndex(
                        (oExpression, oExpressionIndex) =>
                        {
                            using (WithoutLinefeeds())
                            {
                                if (oExpressionIndex == 0)
                                    WriteLine(0, "orderby ");
                                else
                                    WriteLine(0, ", ");

                                WriteScalarExpression(oExpression.Body);
                            }
                        }
                    );

                    return;
                }
                #endregion

                #region xWhere
                var xWhere = source as xWhere;
                if (xWhere != null)
                {
                    var sql = new SQLWriter<TElement>(xWhere.source, upper.Concat(new[] { source }), context);

                    xWhere.filter.WithEachIndex(
                        (wExpression, wExpressionIndex) =>
                        {
                            using (WithoutLinefeeds())
                            {
                                if (wExpressionIndex == 0)
                                    WriteLine(0, "where ");
                                else
                                    WriteLine(1, "and ");

                                WriteScalarExpression(wExpression.Body);
                            }
                        }
                    );


                    return;
                }
                #endregion




                #region WriteScalarCount
                Action<IQueryStrategy, MethodCallExpression, Func<string>> WriteScalarCount =
                     (zsource, xxMethodCallExpression, GetTargetName) =>
                     {
                         // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectScalarCount\Program.cs
                         // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByThenSelectKeyCount\Program.cs


                         var aParameterExpression = xxMethodCallExpression.Arguments[0] as ParameterExpression;
                         if (aParameterExpression != null)
                         {
                             // if we are applied on a group by we need to move it down a level?
                             WriteLine(1, ("let " + GetTargetName()) + " <-  count(" + aParameterExpression.Name + ")");
                             return;
                         }

                         var zSelect = zsource as xSelect;

                         // scalar sub query?
                         WriteLineWithColor(1, ("let " + GetTargetName()) + " <- select count(*) from (", ConsoleColor.White);


                         var count_source = xxMethodCallExpression.Arguments[0] as MemberExpression;
                         // [0x00000000] = {<>h__TransparentIdentifier6.scalar1}
                         // [0x00000000] = {<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.scalar1}

                         if (count_source != null)
                         {
                             // looks like we saved that query somewhere via let?

                             var m = count_source.Expression as MemberExpression;

                             if (m == null)
                             {
                                 WriteLineWithColor(1, "?", ConsoleColor.White);
                             }
                             else
                             {

                                 // m = {<>h__TransparentIdentifier6.<>h__TransparentIdentifier5}
                                 var mp0ParameterExpression = m.Expression as ParameterExpression;

                                 if (zSelect.selector.Parameters[0] == mp0ParameterExpression)
                                 {
                                     // found it!
                                     // we should access the missing value via outer source?

                                     //var xxSelect = xSelect.source as xSelect;
                                     var xxOrderBy = zSelect.source as xOrderBy;
                                     var xxSelect = xxOrderBy.source as xSelect;

                                     var pp0 = xxSelect.selector.Parameters[0];
                                     if (pp0.Name == m.Member.Name)
                                     {
                                         // yet again?
                                         // xxMethodCallExpression = {<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.scalar1.Count()}
                                         // how is scalar1 being set?

                                         var xxxSelect = xxSelect.source as xSelect;

                                         var xxNewExpression = xxxSelect.selector.Body as NewExpression;
                                         if (xxNewExpression != null)
                                         {
                                             var ii = xxNewExpression.Members.IndexOf(count_source.Member);
                                             var aa = xxNewExpression.Arguments[ii];

                                             // this is how it is built.

                                             // aa = {new xTable().OrderBy(zz => zz.Key)}

                                             var aaMethodCallExpression = aa as MethodCallExpression;

                                             // i think we need to call that method.

                                             var aa_keySelector = aaMethodCallExpression.Arguments[1] as UnaryExpression;
                                             var aa_source = aaMethodCallExpression.Arguments[0];

                                             // OrderBy_source = {new xTable()}

                                             var oNewExpression = aa_source as NewExpression;
                                             var newsource = oNewExpression.Constructor.Invoke(new object[0]);
                                             // newsource = {ComplexQueryExperiment.xTable}

                                             // Additional information: Object of type 'System.Linq.Expressions.UnaryExpression' cannot be converted to type 
                                             // 'System.Linq.Expressions.Expression`1[System.Func`2[ComplexQueryExperiment.xRow,ComplexQueryExperiment.xKey]]'.

                                             var oOrdered = (IQueryStrategy)aaMethodCallExpression.Method.Invoke(null,
                                                 new object[] {
                                                          newsource,
                                                          aa_keySelector.Operand
                                                      }
                                             );

                                             var sqalarsql = new SQLWriter<TElement>(
                                                 oOrdered,
                                                 upper.Concat(new[] { source }).ToArray(),
                                                 context
                                             );

                                         }
                                     }
                                 }
                             }
                         }


                         WriteLineWithColor(1, ")", ConsoleColor.White);
                     };
                #endregion



                #region WriteScalarFirstOrDefault
                Action<IQueryStrategy, MethodCallExpression, Func<string>> WriteScalarFirstOrDefault =
                    (zsource, xxMethodCallExpression, GetTargetName) =>
                    {
                        // x:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByScalarFirstOrDefault\Program.cs

                        var zSelect = zsource as xSelect;
                        if (zSelect == null)
                        {
                            WriteLine(1, "?");
                            return;
                        }

                        #region yaa
                        Action<Expression> yaa =
                            aa =>
                            {
                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectScalarFirstOrDefault\Program.cs

                                using (WithoutLinefeeds())
                                {
                                    WriteLine(1, "let ");
                                    var uSelect = upper.LastOrDefault() as xSelect;
                                    if (uSelect != null)
                                    {
                                        WriteLineWithColor(0, uSelect.selector.Parameters[0].Name, ConsoleColor.DarkCyan);
                                        WriteLine(1, " ");
                                    }

                                    WriteLineWithColor(1, GetTargetName(), ConsoleColor.Cyan);
                                    // ?
                                    WriteLineWithColor(1, " <- (", ConsoleColor.White);
                                }

                                // whats aa? the where clause?
                                // aa = {new xTable().Where(zz => (Convert(zz.Key) == 77))}

                                var aaMethodCallExpression = aa as MethodCallExpression;

                                if (aaMethodCallExpression.Method.Name == SelectReference.Method.Name)
                                {
                                    #region Select
                                    var aa_selectorQuote = aaMethodCallExpression.Arguments[1] as UnaryExpression;
                                    var aa_source = aaMethodCallExpression.Arguments[0] as MethodCallExpression;

                                    var aaa_sQuote = aa_source.Arguments[1] as UnaryExpression;
                                    // the let key word!
                                    // [0x00000001] = {<>h__TransparentIdentifier2 => (<>h__TransparentIdentifier2.xx.field1 == 44)}
                                    var aaa_source = aa_source.Arguments[0] as NewExpression;

                                    if (aaa_source != null)
                                    {
                                        var aaa_sourcei = aaa_source.Constructor.Invoke(new object[0]);

                                        var newsource2 = (IQueryStrategy)aa_source.Method.Invoke(null,
                                              new object[] {
                                                    aaa_sourcei,
                                                    aaa_sQuote.Operand
                                                }
                                        );

                                        var newsource3 = (IQueryStrategy)aaMethodCallExpression.Method.Invoke(null,
                                                  new object[] {
                                                                newsource2,
                                                                aa_selectorQuote.Operand
                                                            }
                                            );

                                        var sqalarsql = new SQLWriter<TElement>(
                                             newsource2,
                                             upper.Concat(new[] { source }).ToArray(),
                                             context
                                         );
                                    }
                                    else
                                    {

                                        var aaa2_source = aa_source.Arguments[0] as MethodCallExpression;
                                        // select

                                        var aaaa_sQuote = aaa2_source.Arguments[1] as UnaryExpression;
                                        var aaaa_source = aaa2_source.Arguments[0] as NewExpression;

                                        var aaaa_sourcei = aaaa_source.Constructor.Invoke(new object[0]);


                                        var newsource3 = (IQueryStrategy)aaa2_source.Method.Invoke(null,
                                             new object[] {
                                                    aaaa_sourcei,
                                                    aaaa_sQuote.Operand
                                                }
                                       );

                                        var newsource2 = (IQueryStrategy)aa_source.Method.Invoke(null,
                                             new object[] {
                                                    newsource3,
                                                    aaa_sQuote.Operand
                                                }
                                       );

                                        var sqalarsql = new SQLWriter<TElement>(
                                             newsource2,
                                             upper.Concat(new[] { source }).ToArray(),
                                             context
                                         );

                                    }
                                    #endregion
                                }
                                else if (aaMethodCallExpression.Method.Name == GroupByReference.Method.Name)
                                {
                                    #region GroupBy
                                    var aa_kQuote = aaMethodCallExpression.Arguments[1] as UnaryExpression;
                                    var aa_source = aaMethodCallExpression.Arguments[0] as NewExpression;
                                    var aa_sourcei = aa_source.Constructor.Invoke(new object[0]);

                                    var newsource2 = (IQueryStrategy)aaMethodCallExpression.Method.Invoke(null,
                                            new object[] {
                                                          aa_sourcei,
                                                          aa_kQuote.Operand
                                                      }
                                        );

                                    var sqalarsql = new SQLWriter<TElement>(
                                        newsource2,
                                        upper.Concat(new[] { source }).ToArray(),
                                        context
                                    );
                                    #endregion
                                }
                                else if (aaMethodCallExpression.Method.Name == JoinReference.Method.Name)
                                {
                                    #region Join
                                    // can we ve fast templates of the quoted params??

                                    var aa_outer = aaMethodCallExpression.Arguments[0] as NewExpression;
                                    var aa_outeri = aa_outer.Constructor.Invoke(new object[0]);

                                    var aa_inner = aaMethodCallExpression.Arguments[1];
                                    var aa_inneri = aa_outer.Constructor.Invoke(new object[0]);

                                    var aa_of = aaMethodCallExpression.Arguments[2] as UnaryExpression;
                                    var aa_if = aaMethodCallExpression.Arguments[3] as UnaryExpression;
                                    var aa_rs = aaMethodCallExpression.Arguments[4] as UnaryExpression;

                                    var newsource2 = (IQueryStrategy)aaMethodCallExpression.Method.Invoke(null,
                                           new object[] {
                                                          aa_outeri,
                                                          aa_inneri,
                                                          aa_of.Operand,
                                                          aa_if.Operand,
                                                          aa_rs.Operand
                                                      }
                                       );

                                    var sqalarsql = new SQLWriter<TElement>(
                                        newsource2,
                                        upper.Concat(new[] { source }).ToArray(),
                                        context
                                    );
                                    #endregion
                                }
                                else if (aaMethodCallExpression.Method.Name == WhereReference.Method.Name)
                                {
                                    #region Where
                                    var aa_filterQuote = aaMethodCallExpression.Arguments[1] as UnaryExpression;
                                    var aa_source = aaMethodCallExpression.Arguments[0] as NewExpression;
                                    var aa_sourcei = aa_source.Constructor.Invoke(new object[0]);

                                    var newsource2 = (IQueryStrategy)aaMethodCallExpression.Method.Invoke(null,
                                            new object[] {
                                                          aa_sourcei,
                                                          aa_filterQuote.Operand
                                                      }
                                        );

                                    var sqalarsql = new SQLWriter<TElement>(
                                        newsource2,
                                        upper.Concat(new[] { source }).ToArray(),
                                        context
                                    );
                                    #endregion

                                }
                                else WriteLineWithColor(1, "?", ConsoleColor.White);

                                WriteLineWithColor(1, ")", ConsoleColor.White);
                            };
                        #endregion

                        var __source = xxMethodCallExpression.Arguments[0] as MemberExpression;


                        #region walker
                        Action<xSelect, Expression> walker = null;

                        walker =
                            (xinnersource, __source_Expression) =>
                            {
                                var __source_asMemberExpression = __source_Expression as MemberExpression;
                                if (__source_asMemberExpression != null)
                                {
                                    var xxinnersource = xinnersource.source as xSelect;


                                    walker(xxinnersource, __source_asMemberExpression.Expression);
                                    return;
                                }

                                var xxNewExpression = xinnersource.selector.Body as NewExpression;

                                var ii = xxNewExpression.Members.IndexOf(__source.Member);
                                var aa = xxNewExpression.Arguments[ii];

                                // now what?
                                // we have found another select. yay.
                                // would that work?
                                yaa(aa);
                            };
                        #endregion



                        walker(zSelect.source as xSelect, __source.Expression);
                    };
                #endregion

                Action<IQueryStrategy, Expression, Tuple<MemberInfo, int>[]> WriteProjectionProxy = null;
                Action<IQueryStrategy, Expression, Tuple<MemberInfo, int>[]> WriteProjection = null;


                #region WriteProjectionProxy

                WriteProjectionProxy =
                    (zsource, zExpression, Target) =>
                    {
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestJoinOnNewExpression\Program.cs


                        #region GetTargetName
                        Func<string> GetTargetName =
                            delegate
                            {
                                var w = "";

                                if (Target != null)
                                    foreach (var item in Target)
                                    {
                                        if (item.Item1 == null)
                                            w += "[" + item.Item2 + "]";
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(w))
                                                w += ".";

                                            w += item.Item1.Name + "";
                                        }

                                    }

                                return w;
                            };
                        #endregion



                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelect\Program.cs

                        #region WriteProjectionProxy:zParameterExpression
                        var zParameterExpression = zExpression as ParameterExpression;
                        if (zParameterExpression != null)
                        {
                            #region y
                            Action<IQueryStrategy> y = null;

                            y =
                                zzsource =>
                                {
                                    var zzWhere = zzsource as xWhere;
                                    if (zzWhere != null)
                                    {
                                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWhere\Program.cs

                                        y(zzWhere.source);
                                        return;
                                    }

                                    var zzGroupBy = zzsource as xGroupBy;
                                    if (zzGroupBy != null)
                                    {
                                        // x:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByScalarFirstOrDefault\Program.cs

                                        WriteProjectionProxy(
                                            zzGroupBy,
                                            zzGroupBy.elementSelector.Body,
                                            Target
                                        );

                                        return;
                                    }

                                    var zzSelect = zzsource as xSelect;
                                    if (zzSelect != null)
                                    {
                                        WriteProjectionProxy(zzSelect, zzSelect.selector.Body, Target);
                                        return;
                                    }

                                    

                                    var zzJoin = zzsource as xJoin;
                                    if (zzJoin != null)
                                    {
                                        WriteProjectionProxy(zzJoin, zzJoin.resultSelector.Body, Target);
                                        return;
                                    }
                                };
                            #endregion

                            #region zGroupBy
                            var zGroupBy = zsource as xGroupBy;
                            if (zGroupBy != null)
                            {
                                y(zGroupBy.source);
                                return;
                            }
                            #endregion

                            #region zSelect
                            var zSelect = zsource as xSelect;
                            if (zSelect != null)
                            {
                                y(zSelect.source);
                                return;
                            }
                            #endregion


                            #region zJoin
                            var zJoin = zsource as xJoin;
                            if (zJoin != null)
                            {
                                // x:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestJoinFive\Program.cs

                                if (zParameterExpression.Name == zJoin.outerKeySelector.Parameters[0].Name)
                                {
                                    y(zJoin.outer);
                                    return;
                                }

                                if (zParameterExpression.Name == zJoin.innerKeySelector.Parameters[0].Name)
                                {
                                    y(zJoin.inner);
                                    return;
                                }
                            }
                            #endregion


                        }
                        #endregion


                        #region WriteProjection:MemberInitExpression
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMemberInitExpression\Program.cs
                        var zMemberInitExpression = zExpression as MemberInitExpression;
                        if (zMemberInitExpression != null)
                        {
                            WriteCommentLine(1, "MemberInitExpression");

                            WriteProjectionProxy(zsource, zMemberInitExpression.NewExpression, Target);

                            // what about XElement?

                            zMemberInitExpression.Bindings.WithEachIndex(
                                (SourceBinding, SourceBindingIndex) =>
                                {
                                    var item = new
                                    {
                                        a = (SourceBinding as MemberAssignment).Expression,
                                        m = SourceBinding.Member
                                    };

                                    WriteProjectionProxy(zsource, item.a, Target.Concat(new[] { Tuple.Create(item.m, -1) }).ToArray());
                                }
                            );
                            return;
                        }
                        #endregion


                        #region WriteProjectionProxy:xxNewExpression
                        var zNewExpression = zExpression as NewExpression;
                        if (zNewExpression != null)
                        {
                            //WriteLine(1, "    new " + xxNewExpression.Type.Name);

                            // ternary op does not work
                            if (zNewExpression.Members == null)
                            {
                                zNewExpression.Arguments.WithEachIndex(
                                      (SourceArgument, SourceArgumentIndex) =>
                                          WriteProjectionProxy(
                                          zsource,
                                              SourceArgument,
                                              Target.Concat(new[] { Tuple.Create(
                                                                  default(MemberInfo) ,
                                                                  SourceArgumentIndex
                                                                  )
                                                              }).ToArray()
                                          )
                                  );
                                return;
                            }

                            zNewExpression.Arguments.WithEachIndex(
                                (SourceArgument, SourceArgumentIndex) =>
                                    WriteProjectionProxy(
                                    zsource,
                                        SourceArgument,
                                        Target.Concat(new[] { Tuple.Create(
                                              zNewExpression.Members[SourceArgumentIndex],
                                              SourceArgumentIndex
                                              )
                                          }).ToArray()
                                    )
                            );
                            return;
                        }
                        #endregion

                        #region WriteProjectionProxy:zMemberExpression
                        var zMemberExpression = zExpression as MemberExpression;
                        if (zMemberExpression != null)
                        {
                            using (WithoutLinefeeds())
                            {
                                // we have to unpack everything?


                                WriteLineWithColor(1, "proxy ", ConsoleColor.Magenta);

                                if (upperParameter != null)
                                {
                                    WriteLineWithColor(0, upperParameter.Name, ConsoleColor.DarkCyan);
                                    WriteLine(1, " ");
                                }

                                WriteLineWithColor(0, GetTargetName(), ConsoleColor.Magenta);
                                //WriteLineWithColor(0, zParameterExpression.Name, ConsoleColor.Magenta);



                                var zGroupBy = source as xGroupBy;
                                if (zGroupBy != null)
                                {
                                    WriteLine(1, " <- " + zGroupBy.keySelector.Parameters[0].Name + " " + zMemberExpression.Member.Name);
                                    return;
                                }

                                var zSelect = source as xSelect;
                                if (zSelect != null)
                                {
                                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestJoinOnNewExpression\Program.cs
                                    var zzJoin = zSelect.source as xJoin;
                                    if (zzJoin != null)
                                    {

                                        //WriteLine(1, " <- " + zSelect.selector.Parameters[0].Name + " ? " + zMemberExpression.Member.Name);
                                        WriteLine(1, " <- " + GetTargetName());
                                        return;
                                    }


                                    var zzGroupBy = zSelect.source as xGroupBy;
                                    if (zzGroupBy != null)
                                    {

                                        WriteLine(1, " <- " + zSelect.selector.Parameters[0].Name + " Last " + zMemberExpression.Member.Name);
                                        return;
                                    }

                                }



                                WriteLine(1, " <- " + GetTargetName());
                                //WriteLine(1, " {...}");
                            }
                            return;
                        }
                        #endregion

                        // zExpression = {1}
                        #region WriteProjectionProxy:zConstantExpression
                        var zConstantExpression = zExpression as ConstantExpression;
                        if (zConstantExpression != null)
                        {
                            using (WithoutLinefeeds())
                            {
                                // we have to unpack everything?

                                WriteLineWithColor(1, "proxy ", ConsoleColor.Magenta);

                                if (upperParameter != null)
                                {
                                    WriteLineWithColor(0, upperParameter.Name, ConsoleColor.DarkCyan);
                                    WriteLine(1, " ");
                                }

                                WriteLineWithColor(0, GetTargetName(), ConsoleColor.Magenta);
                                //WriteLineWithColor(0, zParameterExpression.Name, ConsoleColor.Magenta);
                                WriteLine(1, " <- " + GetTargetName());
                            }
                            return;
                        }
                        #endregion

                        #region WriteProjectionProxy:zMethodCallExpression
                        var zMethodCallExpression = zExpression as MethodCallExpression;
                        if (zMethodCallExpression != null)
                        {
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectScalarCount\Program.cs
                            using (WithoutLinefeeds())
                            {
                                // we have to unpack everything?

                                WriteLineWithColor(1, "proxy ", ConsoleColor.Magenta);

                                if (upperParameter != null)
                                {
                                    WriteLineWithColor(0, upperParameter.Name, ConsoleColor.DarkCyan);
                                    WriteLine(1, " ");
                                }

                                WriteLineWithColor(0, GetTargetName(), ConsoleColor.Magenta);
                                //WriteLineWithColor(0, zParameterExpression.Name, ConsoleColor.Magenta);
                                //WriteLine(1, " <- " + GetTargetName());
                                WriteLine(1, " <- ?");
                            }
                            return;
                        }
                        #endregion

                        Debugger.Break();
                    };
                #endregion


                #region WriteProjection

                WriteProjection =
                    // do we need zsource?
                      (zsource, zExpression, Target) =>
                      {
                          //Console.WriteLine(new { zsource, zExpression });

                          #region GetTargetName
                          Func<string> GetTargetName =
                              delegate
                              {
                                  var w = "";

                                  if (Target != null)
                                      foreach (var item in Target)
                                      {
                                          if (item.Item1 == null)
                                              w += "[" + item.Item2 + "]";
                                          else
                                          {
                                              if (!string.IsNullOrEmpty(w))
                                                  w += ".";

                                              w += item.Item1.Name + "";
                                          }

                                      }

                                  return w;
                              };
                          #endregion

                          var zSelect = source as xSelect;

                          #region WriteProjection:zParameterExpression
                          var zParameterExpression = zExpression as ParameterExpression;
                          if (zParameterExpression != null)
                          {
                              WriteProjectionProxy(zsource, zParameterExpression, Target);
                              return;
                          }
                          #endregion


                          #region WriteProjection:xxMethodCallExpression
                          var xxMethodCallExpression = zExpression as MethodCallExpression;
                          if (xxMethodCallExpression != null)
                          {
                              // whatif its a nested query?

                              // !!!

                              if (xxMethodCallExpression.Method.DeclaringType == typeof(QueryExpressionBuilder))
                              {
                                  #region Last
                                  if (xxMethodCallExpression.Method.Name == LastReference.Method.Name)
                                  {
                                      // x:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByScalarFirstOrDefault\Program.cs
                                      WriteProjection(zsource, xxMethodCallExpression.Arguments[0], Target);
                                      return;
                                  }
                                  #endregion

                                  #region FirstOrDefault
                                  if (xxMethodCallExpression.Method.Name == FirstOrDefaultReference.Method.Name)
                                  {
                                      // what about inline testing?
                                      WriteScalarFirstOrDefault(zsource, xxMethodCallExpression, GetTargetName);
                                      return;
                                  }
                                  #endregion



                                  #region Count
                                  if (xxMethodCallExpression.Method == CountReference.Method)
                                  {
                                      // tested by?
                                      WriteScalarCount(zsource, xxMethodCallExpression, GetTargetName);
                                      return;

                                  }
                                  #endregion
                              }

                              // what other methods have we referenced yet?

                              #region Tuple
                              if (xxMethodCallExpression.Method.DeclaringType == typeof(Tuple))
                              {
                                  // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectTuple\Program.cs

                                  xxMethodCallExpression.Arguments.WithEachIndex(
                                           (SourceArgument, SourceArgumentIndex) =>
                                               WriteProjection(
                                               zsource,
                                                   SourceArgument,
                                                   Target.Concat(new[] { Tuple.Create(
                                                                                          default(MemberInfo) ,
                                                                                          SourceArgumentIndex
                                                                                          )
                                                                                      }).ToArray()
                                               )
                                       );
                                  return;
                              }
                              #endregion


                              #region string
                              if (xxMethodCallExpression.Method.DeclaringType == typeof(string))
                              {
                                  #region Concat
                                  if (xxMethodCallExpression.Method.Name == "Concat")
                                  {

                                      xxMethodCallExpression.Arguments.WithEachIndex(
                                              (SourceArgument, SourceArgumentIndex) =>
                                                  WriteProjection(
                                                  zsource,
                                                      SourceArgument,
                                                      Target.Concat(new[] { Tuple.Create(
                                                                                                                      default(MemberInfo) ,
                                                                                                                      SourceArgumentIndex
                                                                                                                      )
                                                                                                                  }).ToArray()
                                                  )
                                          );
                                      return;
                                  }
                                  #endregion



                                  if (xxMethodCallExpression.Method.Name == "ToUpper")
                                  {
                                      using (WithoutLinefeeds())
                                      {
                                          WriteLine(1, "let ");
                                          if (upperParameter != null)
                                          {
                                              WriteLineWithColor(0, upperParameter.Name, ConsoleColor.DarkCyan);
                                              WriteLine(1, " ");
                                          }
                                          WriteLineWithColor(0, GetTargetName(), ConsoleColor.Cyan);
                                          WriteLine(1, " <- ");

                                          WriteLineWithColor(1, "upper(", ConsoleColor.White);
                                          WriteScalarExpression(xxMethodCallExpression.Object);
                                          WriteLineWithColor(1, ")", ConsoleColor.White);
                                      }
                                      return;
                                  }

                                  if (xxMethodCallExpression.Method.Name == "ToLower")
                                  {
                                      using (WithoutLinefeeds())
                                      {
                                          WriteLine(1, "let ");
                                          if (upperParameter != null)
                                          {
                                              WriteLineWithColor(0, upperParameter.Name, ConsoleColor.DarkCyan);
                                              WriteLine(1, " ");
                                          }
                                          WriteLineWithColor(0, GetTargetName(), ConsoleColor.Cyan);
                                          WriteLine(1, " <- ");

                                          WriteLineWithColor(1, "lower(", ConsoleColor.White);
                                          WriteScalarExpression(xxMethodCallExpression.Object);
                                          WriteLineWithColor(1, ")", ConsoleColor.White);
                                      }
                                      return;
                                  }
                              }
                              #endregion




                              using (WithoutLinefeeds())
                              {
                                  WriteLine(1, "let ");
                                  if (upperParameter != null)
                                  {
                                      WriteLineWithColor(0, upperParameter.Name, ConsoleColor.DarkCyan);
                                      WriteLine(1, " ");
                                  }
                                  WriteLineWithColor(0, GetTargetName(), ConsoleColor.Cyan);
                                  WriteLine(1, " <- ");

                                  WriteLineWithColor(1, xxMethodCallExpression.Method.Name + "(?)", ConsoleColor.White);
                              }

                              return;
                          }
                          #endregion



                          #region WriteProjection:zMemberExpression
                          var zMemberExpression = zExpression as MemberExpression;
                          if (zMemberExpression != null)
                          {


                              #region y
                              Action<MemberExpression> y = null;

                              y = m =>
                                  {
                                      var mm = m.Expression as MemberExpression;
                                      if (mm != null)
                                      {
                                          y(mm);
                                      }

                                      var mp = m.Expression as ParameterExpression;
                                      if (mp != null)
                                      {
                                          WriteLineWithColor(0, mp.Name, ConsoleColor.DarkCyan);
                                      }

                                      // m.Expression = {<>h__TransparentIdentifier0.g.Last()}
                                      var mMethodCallExpression = m.Expression as MethodCallExpression;
                                      if (mMethodCallExpression != null)
                                      {
                                          // for group by we shall support Last


                                          var a0m = mMethodCallExpression.Arguments[0] as MemberExpression;

                                          if (a0m != null)
                                          {
                                              y(a0m);
                                          }

                                          WriteLine(1, ".");
                                          WriteLineWithColor(1, "Last", ConsoleColor.White);
                                      }

                                      WriteLine(1, ".");
                                      //if (zMemberExpression == m)
                                      //{
                                      WriteLineWithColor(1, m.Member.Name, ConsoleColor.Cyan);
                                      //}
                                      //else

                                      //    WriteLineWithColor(1, m.Member.Name, ConsoleColor.DarkCyan);
                                  };
                              #endregion



                              // X:\jsc.svn\examples\javascript\LINQ\test\TestLINQ\UnitTestProject1\ApplicationWebService\ApplicationWebService select x.cs
                              using (WithoutLinefeeds())
                              {

                                  WriteLine(1, "let ");

                                  if (upperParameter != null)
                                  {
                                      WriteLineWithColor(0, upperParameter.Name, ConsoleColor.DarkCyan);
                                      WriteLine(1, " ");
                                  }

                                  WriteLineWithColor(0, GetTargetName(), ConsoleColor.Cyan);

                                  WriteLine(1, " <- ");

                                  // tested by?
                                  if (zMemberExpression.Member.DeclaringType == typeof(DateTime))
                                  {
                                      WriteLineWithColor(1, "date(", ConsoleColor.White);

                                      // date of what?
                                      y(zMemberExpression.Expression as MemberExpression);

                                      WriteLineWithColor(1, ")", ConsoleColor.White);
                                      return;
                                  }

                                  y(zMemberExpression);
                              }
                              return;
                          }
                          #endregion

                          #region WriteProjection:zUnaryExpression
                          var zUnaryExpression = zExpression as UnaryExpression;
                          if (zUnaryExpression != null)
                          {
                              // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectXElement\Program.cs
                              // Method = {System.Xml.Linq.XName op_Implicit(System.String)}

                              if (zUnaryExpression.NodeType == ExpressionType.Convert)
                              {
                                  using (WithoutLinefeeds())
                                  {
                                      WriteLine(1, "let ");

                                      if (zSelect != null)
                                      {
                                          WriteLineWithColor(0, zSelect.selector.Parameters[0].Name, ConsoleColor.DarkCyan);
                                          WriteLine(1, " ");
                                      }

                                      WriteLineWithColor(0, GetTargetName(), ConsoleColor.Cyan);
                                      WriteLine(1, " <- ");
                                      WriteScalarExpression(zUnaryExpression.Operand);
                                  }

                                  return;
                              }

                              WriteLine(1, ("let " + GetTargetName()) + " <- unary");
                              return;
                          }
                          #endregion

                          #region WriteProjection:xConstantExpression
                          var xConstantExpression = zExpression as ConstantExpression;
                          if (xConstantExpression != null)
                          {
                              using (WithoutLinefeeds())
                              {
                                  WriteLine(1, "let ");

                                  if (upperParameter != null)
                                  {
                                      WriteLineWithColor(0, upperParameter.Name, ConsoleColor.DarkCyan);
                                      WriteLine(1, " ");
                                  }

                                  WriteLineWithColor(0, GetTargetName(), ConsoleColor.Cyan);
                                  WriteLine(1, " <- ");
                                  WriteScalarExpression(xConstantExpression);
                              }
                              return;
                          }
                          #endregion

                          #region WriteProjection:xBinaryExpression
                          var xxBinaryExpression = zExpression as BinaryExpression;
                          if (xxBinaryExpression != null)
                          {
                              using (WithoutLinefeeds())
                              {
                                  WriteLine(1, "let ");

                                  if (upperParameter != null)
                                  {
                                      WriteLineWithColor(0, upperParameter.Name, ConsoleColor.DarkCyan);
                                      WriteLine(1, " ");
                                  }

                                  WriteLineWithColor(0, GetTargetName(), ConsoleColor.Cyan);
                                  WriteLine(1, " <- ");
                                  WriteScalarExpression(xxBinaryExpression);
                              }
                              return;
                          }
                          #endregion

                          #region WriteProjection:xxNewArrayExpression
                          var xxNewArrayExpression = zExpression as NewArrayExpression;
                          if (xxNewArrayExpression != null)
                          {
                              xxNewArrayExpression.Expressions.WithEachIndex(
                                    (SourceArgument, SourceArgumentIndex) =>
                                        WriteProjection(
                                            zsource,
                                            SourceArgument,
                                            Target.Concat(new[] { Tuple.Create(default(MemberInfo), SourceArgumentIndex) }).ToArray()
                                        )
                              );
                              return;
                          }
                          #endregion

                          #region WriteProjection:xxNewExpression
                          var zNewExpression = zExpression as NewExpression;
                          if (zNewExpression != null)
                          {
                              //WriteLine(1, "    new " + xxNewExpression.Type.Name);

                              // ternary op does not work
                              if (zNewExpression.Members == null)
                              {
                                  zNewExpression.Arguments.WithEachIndex(
                                        (SourceArgument, SourceArgumentIndex) =>
                                            WriteProjection(
                                            zsource,
                                                SourceArgument,
                                                Target.Concat(new[] { Tuple.Create(
                                                                  default(MemberInfo) ,
                                                                  SourceArgumentIndex
                                                                  )
                                                              }).ToArray()
                                            )
                                    );
                                  return;
                              }

                              zNewExpression.Arguments.WithEachIndex(
                                  (SourceArgument, SourceArgumentIndex) =>
                                      WriteProjection(
                                      zsource,
                                          SourceArgument,
                                          Target.Concat(new[] { Tuple.Create(
                                              zNewExpression.Members[SourceArgumentIndex],
                                              SourceArgumentIndex
                                              )
                                          }).ToArray()
                                      )
                              );
                              return;
                          }
                          #endregion



                          #region WriteProjection:InvocationExpression
                          var sInvocationExpression = zExpression as InvocationExpression;
                          if (sInvocationExpression != null)
                          {
                              // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectInvocationExpression\Program.cs
                              // that lambda shall be run during readout, not within db
                              //WriteLine(1, "f(?, ?)");

                              sInvocationExpression.Arguments.WithEachIndex(
                                  (SourceArgument, index) =>
                                  {
                                      WriteProjection(zsource, SourceArgument, Target.Concat(new[] { Tuple.Create(default(MemberInfo), index) }).ToArray());
                                  }
                              );

                              return;
                          }
                          #endregion

                          #region WriteProjection:MemberInitExpression
                          // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMemberInitExpression\Program.cs
                          var zMemberInitExpression = zExpression as MemberInitExpression;
                          if (zMemberInitExpression != null)
                          {
                              WriteCommentLine(1, "MemberInitExpression");

                              WriteProjection(zsource, zMemberInitExpression.NewExpression, Target);

                              // what about XElement?

                              zMemberInitExpression.Bindings.WithEachIndex(
                                  (SourceBinding, SourceBindingIndex) =>
                                  {
                                      var item = new
                                      {
                                          a = (SourceBinding as MemberAssignment).Expression,
                                          m = SourceBinding.Member
                                      };

                                      WriteProjection(zsource, item.a, Target.Concat(new[] { Tuple.Create(item.m, -1) }).ToArray());
                                  }
                              );
                              return;
                          }
                          #endregion

                          Debugger.Break();
                      };
                #endregion

                #region WriteScalarExpressionArray
                Action<IQueryStrategy, Expression, Tuple<MemberInfo, int>[]> WriteScalarExpressionArray =
                      (zsource, zExpression, Target) =>
                      {
                          // lets make the order by operator happy. need to present all fields we need to group it by

                          // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByScalarFirstOrDefault\Program.cs
                          var zMethodCallExpression = zExpression as MethodCallExpression;
                          if (zMethodCallExpression != null)
                          {
                              WriteLine(1, "?");
                              return;
                          }

                          #region MemberExpression
                          var zMemberExpression = zExpression as MemberExpression;
                          if (zMemberExpression != null)
                          {
                              WriteScalarExpression(zExpression);
                              return;
                          }
                          #endregion

                          #region ConstantExpression
                          var zConstantExpression = zExpression as ConstantExpression;
                          if (zConstantExpression != null)
                          {
                              WriteScalarExpression(zConstantExpression);
                              return;
                          }
                          #endregion


                          #region NewArrayExpression
                          var zNewArrayExpression = zExpression as NewArrayExpression;
                          if (zNewArrayExpression != null)
                          {
                              // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByArray\Program.cs

                              zNewArrayExpression.Expressions.WithEachIndex(
                                   (SourceArgument, SourceArgumentIndex) =>
                                       WriteScalarExpression(
                                       //zsource,
                                           SourceArgument
                                       //,
                                       //Target.Concat(new[] { Tuple.Create(default(MemberInfo), SourceArgumentIndex) }).ToArray()
                                       )
                             );
                              return;
                          }
                          #endregion

                          #region WriteProjection:xxNewExpression
                          var zNewExpression = zExpression as NewExpression;
                          if (zNewExpression != null)
                          {
                              //WriteLine(1, "    new " + xxNewExpression.Type.Name);

                              // ternary op does not work
                              if (zNewExpression.Members == null)
                              {
                                  zNewExpression.Arguments.WithEachIndex(
                                        (SourceArgument, SourceArgumentIndex) =>
                                            WriteScalarExpression(
                                            //zsource,
                                                SourceArgument //,
                                            //Target.Concat(new[] { Tuple.Create(
                                            //                  default(MemberInfo) ,
                                            //                  SourceArgumentIndex
                                            //                  )
                                            //              }).ToArray()
                                            )
                                    );
                                  return;
                              }

                              zNewExpression.Arguments.WithEachIndex(
                                  (SourceArgument, SourceArgumentIndex) =>
                                      WriteScalarExpression(
                                      //zsource,
                                          SourceArgument //,
                                      //Target.Concat(new[] { Tuple.Create(
                                      //    zNewExpression.Members[SourceArgumentIndex],
                                      //    SourceArgumentIndex
                                      //    )
                                      //}).ToArray()
                                      )
                              );
                              return;
                          }
                          #endregion

                          Debugger.Break();
                      };
                #endregion



                #region xGroupBy
                var xGroupBy = source as xGroupBy;
                if (xGroupBy != null)
                {
                    WriteCommentLine(0, "xGroupBy");

                    // proxy x?
                    // elementSelector = {<>h__TransparentIdentifier3 => <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.x}
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupBy\Program.cs

                    WriteLine(0, "select");

                    {
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByConstant\Program.cs
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupBySelectKeyAndLast\Program.cs
                        //WriteLine(1, "proxy { Key, Last() ... }");


                        // IQueryStrategyGrouping

                        // sending over the key?
                        WriteProjection(
                            source,
                            xGroupBy.keySelector.Body,
                               new[] { new Tuple<MemberInfo, int>(KeyReference.Method, -1) }
                        );


                        WriteProjection(source, xGroupBy.elementSelector.Body,
                             new[] { new Tuple<MemberInfo, int>(LastReference.Method, -1) }
                            );

                    }

                    // elementSelector = {<>h__TransparentIdentifier3 => new <>f__AnonymousType4`3(x = <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.x, xFoo = <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.xFoo, xKey = <>h__TransparentIdentifier3.xKey)}

                    // proxy the key
                    // proxy the outer

                    WriteLine(0, "from (");
                    var sql0 = new SQLWriter<TElement>(xGroupBy.source, upper.Concat(new[] { source }), context, upperParameter: xGroupBy.keySelector.Parameters[0]);

                    // keySelector = {<>h__TransparentIdentifier3 => new <>f__AnonymousType3`3(xKey = <>h__TransparentIdentifier3.xKey, xFoo = <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.xFoo, g3 = (<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.x.field2 + 2))}

                    using (WithoutLinefeeds())
                    {
                        WriteLine(0, ") as ");
                        WriteLineWithColor(0, xGroupBy.keySelector.Parameters[0].Name, ConsoleColor.Magenta);
                    }

                    //using (WithoutLinefeeds())
                    {
                        WriteLine(0, "group by ");

                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupBy\Program.cs

                        // we need it unpacked
                        //WriteProjection(source, xGroupBy.keySelector.Body, new Tuple<MemberInfo, int>[0]);

                        WriteScalarExpressionArray(source, xGroupBy.keySelector.Body, new Tuple<MemberInfo, int>[0]);
                    }
                    return;
                }
                #endregion


                #region xJoin
                var xJoin = source as xJoin;
                if (xJoin != null)
                {
                    WriteCommentLine(0, "xJoin");

                    WriteLine(0, "select");

                    var xNewExpression = xJoin.resultSelector.Body as NewExpression;
                    if (xNewExpression != null)
                    {
                        // we are selecting a group for upper select arent we.
                        var xArguments = xNewExpression.Arguments.Zip(xNewExpression.Members, (a, m) => new { a, m, source }).ToList();
                        foreach (var item in xArguments)
                        {
                            WriteProjection(item.source, item.a, new[] { Tuple.Create(item.m, -1) });
                        }
                    }
                    else
                    {
                        Debugger.Break();
                    }

                    // resultSelector = {(xouter, xinner) => new <>f__AnonymousType13`2(xouter = xouter, xinner = xinner)}

                    WriteLine(0, "from (");
                    var sql0 = new SQLWriter<TElement>(xJoin.outer, upper.Concat(new[] { source }), context, upperParameter: xJoin.outerKeySelector.Parameters[0]);
                    using (WithoutLinefeeds())
                    {
                        WriteLine(0, ") as ");
                        WriteLineWithColor(0, xJoin.outerKeySelector.Parameters[0].Name, ConsoleColor.Magenta);
                    }

                    WriteLine(0, "inner join (");

                    var sql1 = new SQLWriter<TElement>(xJoin.inner, upper.Concat(new[] { source }), context, upperParameter: xJoin.innerKeySelector.Parameters[0]);

                    using (WithoutLinefeeds())
                    {
                        WriteLine(0, ") as ");
                        WriteLineWithColor(0, xJoin.innerKeySelector.Parameters[0].Name, ConsoleColor.Magenta);
                    }



                    var oNewExpression = xJoin.outerKeySelector.Body as NewExpression;
                    if (oNewExpression != null)
                    {
                        var iNewExpression = xJoin.innerKeySelector.Body as NewExpression;

                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestJoinOnNewExpression\Program.cs

                        var xArguments = oNewExpression.Arguments.Zip(iNewExpression.Arguments, (a, m) => new { a, m, source }).ToList();
                        var xi = -1;
                        foreach (var item in xArguments)
                        {
                            xi++;

                            using (WithoutLinefeeds())
                            {

                                if (xi == 0)
                                    WriteLine(0, "on ");
                                else
                                    WriteLine(0, "and ");


                                WriteScalarExpression(item.a);
                                WriteLine(0, " == ");
                                WriteScalarExpression(item.m);
                            }
                        }

                        return;
                    }

                    using (WithoutLinefeeds())
                    {
                        WriteLine(0, "on ");
                        WriteScalarExpression(xJoin.outerKeySelector.Body);
                        WriteLine(0, " == ");
                        WriteScalarExpression(xJoin.innerKeySelector.Body);
                    }

                    return;
                }
                #endregion





                var xSelect_source = xSelect.source;


                // what are looking at? select selector will know the name of the source
                // (source as xSelect).selector.Body = {<>h__TransparentIdentifier6 . <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.z}
                var x = xSelect.selector.Body;

                WriteLine(0, "select");



                Action WriteSelectProjection =
                    delegate
                    {
                        // time to write our projection of selection fields
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectString\Program.cs
                        // this is a lambda to allow returns


                        #region WriteSelectProjection:ListInitExpression
                        var xListInitExpression = xSelect.selector.Body as ListInitExpression;
                        if (xListInitExpression != null)
                        {
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectStringDictionary\Program.cs
                            // [0x00000000] = {Void Add(System.String, System.String)("hello", x.Tag)}
                            WriteCommentLine(1, "ListInitExpression");
                            WriteProjection(source, xListInitExpression.NewExpression, new Tuple<MemberInfo, int>[0]);

                            xListInitExpression.Initializers.WithEachIndex(
                                (SourceInitializer, index) =>
                                {

                                    SourceInitializer.Arguments.WithEachIndex(
                                        (SourceArgument, aindex) =>
                                        {

                                            WriteProjection(source, SourceArgument, new[] { Tuple.Create(default(MemberInfo), index), Tuple.Create(default(MemberInfo), aindex) });
                                        }
                                    );

                                    //WriteProjection(source, SourceArgument, new[] { Tuple.Create(default(MemberInfo), index) });
                                }
                            );

                            return;
                        }
                        #endregion


                        #region WriteSelectProjection:MemberInitExpression
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMemberInitExpression\Program.cs
                        var xMemberInitExpression = xSelect.selector.Body as MemberInitExpression;
                        if (xMemberInitExpression != null)
                        {
                            WriteCommentLine(1, "MemberInitExpression");

                            WriteProjection(source, xMemberInitExpression.NewExpression, new Tuple<MemberInfo, int>[0]);

                            // what about XElement?

                            xMemberInitExpression.Bindings.WithEachIndex(
                                (SourceBinding, SourceBindingIndex) =>
                                {
                                    var item = new
                                    {
                                        a = (SourceBinding as MemberAssignment).Expression,
                                        m = SourceBinding.Member
                                    };

                                    WriteProjection(source, item.a, new[] { Tuple.Create(item.m, -1) });
                                }
                            );
                            return;
                        }
                        #endregion




                        #region WriteSelectProjection:NewExpression
                        var xNewExpression = xSelect.selector.Body as NewExpression;
                        if (xNewExpression != null)
                        {
                            //WriteCommentLine(1, "NewExpression");

                            if (xNewExpression.Members == null)
                            {
                                // all arguments are for ctor?
                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectNewExpressionConstructor\Program.cs
                                xNewExpression.Arguments.WithEachIndex(
                                      (SourceArgument, index) =>
                                      {

                                          WriteProjection(source, SourceArgument, new[] { Tuple.Create(default(MemberInfo), index) });
                                      }
                                  );
                                return;

                            }

                            // xNewExpression = {new XElement(Convert("hello"), x.Tag)}

                            // we are selecting a group for upper select arent we.
                            var xArguments = xNewExpression.Arguments.Zip(xNewExpression.Members, (a, m) => new { a, m, source }).ToList();
                            foreach (var item in xArguments)
                            {
                                WriteProjection(item.source, item.a, new[] { Tuple.Create(item.m, -1) });
                            }
                            return;
                        }
                        #endregion

                        #region WriteSelectProjection:MethodCallExpression
                        var xMethodCallExpression = xSelect.selector.Body as MethodCallExpression;
                        if (xMethodCallExpression != null)
                        {
                            WriteCommentLine(1, "MethodCallExpression");
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectTuple\Program.cs
                            WriteProjection(source, xMethodCallExpression, new Tuple<MemberInfo, int>[0]);
                            return;
                        }
                        #endregion


                        #region WriteSelectProjection:InvocationExpression
                        var sInvocationExpression = (source as xSelect).selector.Body as InvocationExpression;
                        if (sInvocationExpression != null)
                        {
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectInvocationExpression\Program.cs
                            // that lambda shall be run during readout, not within db
                            //WriteLine(1, "f(?, ?)");

                            sInvocationExpression.Arguments.WithEachIndex(
                                (SourceArgument, index) =>
                                {
                                    WriteProjection(source, SourceArgument, new[] { Tuple.Create(default(MemberInfo), index) });
                                }
                            );
                            return;
                        }
                        #endregion

                        #region WriteSelectProjection:ParameterExpression
                        var sParameterExpression = (source as xSelect).selector.Body as ParameterExpression;
                        if (sParameterExpression != null)
                        {
                            // that lambda shall be run during readout, not within db
                            // ? tested by?
                            WriteProjection(source, sParameterExpression, new Tuple<MemberInfo, int>[0]);
                            return;
                        }
                        #endregion

                        #region WriteSelectProjection:MemberExpression
                        var sMemberExpression = (source as xSelect).selector.Body as MemberExpression;
                        if (sMemberExpression != null)
                        {
                            WriteCommentLine(1, "MemberExpression");

                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectKey\Program.cs
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMemberExpression\Program.cs

                            using (WithoutLinefeeds())
                                WriteScalarExpression(sMemberExpression);

                            return;
                        }
                        #endregion

                        #region WriteSelectProjection:BinaryExpression
                        var xBinaryExpression = (source as xSelect).selector.Body as BinaryExpression;
                        if (xBinaryExpression != null)
                        {
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectStringConcat\Program.cs
                            WriteCommentLine(1, "BinaryExpression");

                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectBinaryExpression\Program.cs

                            // scalar select
                            // this will look like roslyn dictionary indexer initializer
                            using (WithoutLinefeeds())
                                WriteScalarExpression(xBinaryExpression);

                            return;
                        }
                        #endregion

                        #region WriteSelectProjection:NewArrayExpression
                        var xNewArrayExpression = xSelect.selector.Body as NewArrayExpression;
                        if (xNewArrayExpression != null)
                        {
                            WriteCommentLine(1, "NewArrayExpression");

                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectArray\Program.cs
                            // no longer merging.
                            WriteProjection(source, xNewArrayExpression, new Tuple<MemberInfo, int>[0]);
                            return;
                        }
                        #endregion



                        // look we can select array from single row, in db this would be a union. for reading the data out, we just need to prefix with index.


                        Debugger.Break();


                    };

                WriteSelectProjection();




                if (xSelect_source == null)
                {
                    // there needs to be an external source. a table in db?


                    using (WithoutLinefeeds())
                    {
                        WriteLine(0, "from xTable as ");
                        WriteLineWithColor(0, "" + (source as xSelect).selector.Parameters[0].Name, ConsoleColor.Magenta);
                    }
                }
                else
                {
                    WriteLine(0, "from (");

                    var xsource = new SQLWriter<TElement>(
                       xSelect_source,
                        upper.Concat(new[] { source }),
                        context,
                        upperParameter: (source as xSelect).selector.Parameters[0]
                    );

                    // render the source and with parent



                    using (WithoutLinefeeds())
                    {
                        WriteLine(0, ") as ");
                        WriteLineWithColor(0, "" + (source as xSelect).selector.Parameters[0].Name, ConsoleColor.Magenta);
                    }
                }

                //Debugger.Break();
            }
        }













    }

}
