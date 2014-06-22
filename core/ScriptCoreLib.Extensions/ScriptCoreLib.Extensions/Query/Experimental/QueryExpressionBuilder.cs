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

            public SQLWriter(IQueryStrategy source, IEnumerable<IQueryStrategy> upper, SQLWriterContext context = null)
            {
                //Console.Clear();

                // selector = {<>h__TransparentIdentifier6 => <>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.z}

                // convert to SQL!

                #region WriteLine
                if (context == null)
                    context = new SQLWriterContext();

                #region WithoutLinefeeds
                var WithoutLinefeedsCounter = 0;
                var WithoutLinefeedsDirty = false;
                Func<IDisposable> WithoutLinefeeds =
                    delegate
                    {
                        WithoutLinefeedsCounter++;
                        WithoutLinefeedsDirty = false;

                        return new SQLWriterWithoutLinefeeds
                        {
                            yield = delegate
                            {
                                WithoutLinefeedsCounter--;
                                Console.WriteLine();
                            }
                        };
                    };
                #endregion


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

                        #region asBinaryExpression
                        var asBinaryExpression = asExpression as BinaryExpression;
                        if (asBinaryExpression != null)
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
                            return;
                        }
                        #endregion

                        #region WriteExpression::UnaryExpression
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

                        #region zMemberExpression
                        var zMemberExpression = asExpression as MemberExpression;
                        if (zMemberExpression != null)
                        {
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

                        #region xConstantExpression
                        var xConstantExpression = asExpression as ConstantExpression;
                        if (xConstantExpression != null)
                        {
                            //Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + ("? as " + item.m.Name + "+*"));
                            WriteLine(1, "@constant " + new { xConstantExpression.Value });
                            return;
                        }
                        #endregion

                        #region xMethodCallExpression
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



                        #region yaa
                        Action<Expression> yaa =
                            aa =>
                            {
                                // ?
                                WriteLineWithColor(1, ("let " + GetTargetName()) + " <- (", ConsoleColor.White);

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


                        var zSelect = zsource as xSelect;

                        walker(zSelect.source as xSelect, __source.Expression);
                    };
                #endregion



                #region WriteProjection
                Action<IQueryStrategy, Expression, Tuple<MemberInfo, int>[]> WriteProjection = null;

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

                          #region xxMethodCallExpression
                          var xxMethodCallExpression = zExpression as MethodCallExpression;
                          if (xxMethodCallExpression != null)
                          {
                              // whatif its a nested query?

                              // !!!

                              if (xxMethodCallExpression.Method.DeclaringType == typeof(QueryExpressionBuilder))
                              {
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

                              #region string
                              if (xxMethodCallExpression.Method.DeclaringType == typeof(string))
                              {
                                  if (xxMethodCallExpression.Method.Name == "ToUpper")
                                  {
                                      using (WithoutLinefeeds())
                                      {
                                          WriteLineWithColor(1, ("let " + GetTargetName()) + " <- upper(", ConsoleColor.White);
                                          WriteScalarExpression(xxMethodCallExpression.Object);
                                          WriteLineWithColor(1, ")", ConsoleColor.White);
                                      }
                                      return;
                                  }

                                  if (xxMethodCallExpression.Method.Name == "ToLower")
                                  {
                                      using (WithoutLinefeeds())
                                      {
                                          WriteLineWithColor(1, ("let " + GetTargetName()) + " <- lower(", ConsoleColor.White);
                                          WriteScalarExpression(xxMethodCallExpression.Object);
                                          WriteLineWithColor(1, ")", ConsoleColor.White);
                                      }
                                      return;
                                  }
                              }
                              #endregion


                              WriteLineWithColor(1, ("let " + GetTargetName()) + " <- " + xxMethodCallExpression.Method.Name + "(?)", ConsoleColor.White);

                              return;
                          }
                          #endregion



                          #region zMemberExpression
                          var zMemberExpression = zExpression as MemberExpression;
                          if (zMemberExpression != null)
                          {

                              if (zMemberExpression.Member.DeclaringType == typeof(DateTime))
                              {
                                  WriteLineWithColor(1, ("let " + GetTargetName()) + " <- date(?)", ConsoleColor.White);
                                  return;
                              }

                              #region zMMemberExpression
                              var zMMemberExpression = zMemberExpression.Expression as MemberExpression;
                              if (zMMemberExpression != null)
                              {
                                  // walk the source
                                  // upper to deeper

                                  #region diagnostics
                                  //var member1 = (Expression)zMMemberExpression;
                                  //while (member1 is MemberExpression)
                                  //{
                                  //    Console.WriteLine(new { member1 });

                                  //    member1 = ((MemberExpression)member1).Expression;
                                  //}

                                  //var member1p = (ParameterExpression)member1;

                                  //// we have rewinded to the parameter
                                  //Console.WriteLine(new { member1p });
                                  //Console.WriteLine();

                                  //// now replay to get the value?
                                  ////var source1 = source;
                                  //var source1 = zsource;

                                  //if (member1p.Name != source1.ToString())
                                  //{
                                  //    Debugger.Break();

                                  //}

                                  //while (source1 != null)
                                  //{
                                  //    Console.WriteLine(new { source1 });

                                  //    if (source1 is xSelect)
                                  //        source1 = (source1 as xSelect).source;
                                  //    else if (source1 is xOrderBy)
                                  //        source1 = (source1 as xOrderBy).source;
                                  //    else if (source1 is xWhere)
                                  //        source1 = (source1 as xWhere).source;
                                  //    else Debugger.Break();
                                  //}
                                  #endregion

                                  //var p1 = xSelect.source;
                                  //p1 = (zsource as xSelect).source;
                                  // whatif we shall not look at our zsource?
                                  var p1 = (zsource as xSelect).source;

                                  var depth = 0;
                                  while (zMMemberExpression.Expression is MemberExpression)
                                  {
                                      //if (zMemberExpression.Member.Name == "field1")
                                      //WriteLineWithColor(4, ">" + new { depth, p1, zMemberExpression.Member, zMM = zMMemberExpression.Member.Name, zMMemberExpression }, ConsoleColor.Cyan);


                                      depth++;

                                      zMMemberExpression = zMMemberExpression.Expression as MemberExpression;


                                      if (p1 is xWhere)
                                          p1 = (p1 as xWhere).source;

                                      if (p1 is xOrderBy)
                                          p1 = (p1 as xOrderBy).source;

                                      if (p1 is xWhere)
                                          p1 = (p1 as xWhere).source;

                                      if (p1 is xSelect)
                                          p1 = (p1 as xSelect).source;

                                      else Debugger.Break();
                                  }

                                  var pp0 = zMMemberExpression.Expression as ParameterExpression;
                                  var pp1 = default(IQueryStrategy);


                                  if (p1 is xWhere)
                                      p1 = (p1 as xWhere).source;

                                  if (p1 is xOrderBy)
                                      p1 = (p1 as xOrderBy).source;

                                  if (p1 is xWhere)
                                      p1 = (p1 as xWhere).source;

                                  if (p1 is xSelect)
                                      pp1 = (p1 as xSelect).source;
                                  //else if (p1 is xOrderBy)
                                  //    pp1 = ((p1 as xOrderBy).source as xSelect).source;
                                  //else if (p1 is xWhere)
                                  //    pp1 = ((p1 as xWhere).source as xSelect).source;
                                  else
                                      Debugger.Break();

                                  if (pp1 is xWhere)
                                      pp1 = (pp1 as xWhere).source;

                                  if (pp1 is xOrderBy)
                                      pp1 = (pp1 as xOrderBy).source;

                                  if (pp1 is xWhere)
                                      pp1 = (pp1 as xWhere).source;


                                  //if (zMemberExpression.Member.Name == "field1")
                                  //    WriteLineWithColor(4, ">" + new { depth, pp1 }, ConsoleColor.Cyan);


                                  var aa = default(Expression);

                                  if (!(pp1 is xSelect))
                                  {
                                      WriteLine(1, ("let " + GetTargetName()) + " <- ??? nested not select");

                                      return;
                                  }



                                  var xxMemberInitExpression = (pp1 as xSelect).selector.Body as MemberInitExpression;
                                  if (xxMemberInitExpression != null)
                                  {
                                      // 		xxMemberInitExpression.Bindings[4].Member == zMemberExpression.Member	true	bool

                                      var ii = xxMemberInitExpression.Bindings.Select(xx => xx.Member).ToList().IndexOf(zMemberExpression.Member);
                                      if (ii < 0)
                                      {
                                          WriteLine(1, ("let " + GetTargetName()) + " <- ??? wrong level ???");
                                          return;
                                      }

                                      aa = (xxMemberInitExpression.Bindings[ii] as MemberAssignment).Expression;

                                      //aa = xxMemberInitExpression.new[ii].;
                                  }
                                  else
                                  {
                                      var xxNewExpression = (pp1 as xSelect).selector.Body as NewExpression;

                                      var ii = xxNewExpression.Members.IndexOf(zMemberExpression.Member);
                                      if (ii < 0)
                                      {
                                          WriteLine(1, ("let " + GetTargetName()) + " <- ??? wrong level ???");

                                          return;
                                      }

                                      aa = xxNewExpression.Arguments[ii];
                                  }

                                  // is it a complex object?
                                  // aa = {new [] {(<>h__TransparentIdentifier7.scalar1count - 1), <>h__TransparentIdentifier7.scalar1count, (<>h__TransparentIdentifier7.scalar1count + 1)}}



                                  WriteProjection(
                                      pp1,
                                      //zsource,
                                      aa,
                                      Target

                                  );
                                  return;
                              }
                              #endregion


                              // X:\jsc.svn\examples\javascript\LINQ\test\TestLINQ\UnitTestProject1\ApplicationWebService\ApplicationWebService select x.cs
                              using (WithoutLinefeeds())
                              {

                                  WriteLine(1, "let ");
                                  WriteLineWithColor(0, GetTargetName(), ConsoleColor.Cyan);
                                  WriteLine(1, " <- ");
                                  WriteScalarExpression(zExpression);
                              }
                              return;
                          }
                          #endregion

                          #region zUnaryExpression
                          var zUnaryExpression = zExpression as UnaryExpression;
                          if (zUnaryExpression != null)
                          {
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
                                  WriteLineWithColor(0, GetTargetName(), ConsoleColor.Cyan);
                                  WriteLine(1, " <- ");
                                  WriteScalarExpression(xConstantExpression);
                              }
                              return;
                          }
                          #endregion

                          #region xBinaryExpression
                          var xxBinaryExpression = zExpression as BinaryExpression;
                          if (xxBinaryExpression != null)
                          {
                              using (WithoutLinefeeds())
                              {
                                  WriteLine(1, ("let " + GetTargetName()) + " <- ");
                                  WriteScalarExpression(xxBinaryExpression);
                              }
                              return;
                          }
                          #endregion

                          #region xxNewArrayExpression
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

                          #region xxNewExpression
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

                          #region WriteProjection:zParameterExpression
                          var zParameterExpression = zExpression as ParameterExpression;
                          if (zParameterExpression != null)
                          {

                              using (WithoutLinefeeds())
                              {

                                  WriteLine(1, "proxy ");
                                  WriteLineWithColor(0, GetTargetName(), ConsoleColor.Magenta);
                                  WriteLine(1, " {...}");
                              }

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
                    WriteLine(0, "select ?");

                    WriteLine(0, "from (");
                    var sql0 = new SQLWriter<TElement>(xGroupBy.source, upper.Concat(new[] { source }), context);
                    WriteLine(0, ") as ?");

                    //using (WithoutLinefeeds())
                    {
                        WriteLine(0, "group by ");
                        WriteProjection(source, xGroupBy.keySelector.Body, new Tuple<MemberInfo, int>[0]);
                        //WriteScalarExpression(xGroupBy.keySelector.Body);
                    }
                    return;
                }
                #endregion


                #region xJoin
                var xJoin = source as xJoin;
                if (xJoin != null)
                {
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
                    else Debugger.Break();

                    // resultSelector = {(xouter, xinner) => new <>f__AnonymousType13`2(xouter = xouter, xinner = xinner)}

                    WriteLine(0, "from (");
                    var sql0 = new SQLWriter<TElement>(xJoin.outer, upper.Concat(new[] { source }), context);
                    WriteLine(0, ") as outer");

                    WriteLine(0, "inner join (");

                    var sql1 = new SQLWriter<TElement>(xJoin.inner, upper.Concat(new[] { source }), context);

                    WriteLine(0, ") as inner");

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

                var xSelect = source as xSelect;




                var xSelect_source = xSelect.source;


                // what are looking at? select selector will know the name of the source
                // (source as xSelect).selector.Body = {<>h__TransparentIdentifier6 . <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.z}
                var x = xSelect.selector.Body;

                WriteLine(0, "select");

                var sParameterExpression = (source as xSelect).selector.Body as ParameterExpression;
                if (sParameterExpression != null)
                {
                    // that lambda shall be run during readout, not within db
                    //WriteLine(1, "f(?)");

                    WriteProjection(source, sParameterExpression, new Tuple<MemberInfo, int>[0]);
                }
                else
                {

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
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectKey\Program.cs
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMemberExpression\Program.cs

                            using (WithoutLinefeeds())
                                WriteScalarExpression(sMemberExpression);
                        }
                        else
                        {
                            // look we can select array from single row, in db this would be a union. for reading the data out, we just need to prefix with index.

                            var xBinaryExpression = (source as xSelect).selector.Body as BinaryExpression;
                            if (xBinaryExpression != null)
                            {
                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectBinaryExpression\Program.cs

                                // scalar select
                                // this will look like roslyn dictionary indexer initializer
                                using (WithoutLinefeeds())
                                    WriteScalarExpression(xBinaryExpression);
                            }
                            else
                            {

                                var xNewArrayExpression = xSelect.selector.Body as NewArrayExpression;
                                if (xNewArrayExpression != null)
                                {
                                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectArray\Program.cs
                                    // no longer merging.
                                    WriteProjection(source, xNewArrayExpression, new Tuple<MemberInfo, int>[0]);
                                }
                                else
                                {
                                    var xNewExpression = xSelect.selector.Body as NewExpression;
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
                                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMemberInitExpression\Program.cs
                                        var xMemberInitExpression = xSelect.selector.Body as MemberInitExpression;
                                        if (xMemberInitExpression != null)
                                        {
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

                                        }
                                        else Debugger.Break();

                                    }
                                }
                            }
                        }
                    }
                }

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
                        context
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
