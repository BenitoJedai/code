using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Xml.Linq;

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

                    where z.Key == (xKey)66
                    where field5 > 44 && field5 < 77
                    where field5 > 44
                    where new { field3 }.field3 > field5

                    let field6 = field3 + field4 + field5

                    // whats wrong here?
                    let field7 = new { ff1 = z.field1, field6 }


                    let scalar1 =
                        from zz in new xTable()
                        orderby zz.Key
                        select zz

                    let field8 = "???".ToLower()

                    orderby field8, field7, field6, field5 > 33

                    let scalar1count = scalar1.Count()

                    where scalar1count < 77

                    let scalar2 = new[] {
                        scalar1count - 1,
                        scalar1count,
                        scalar1count + 1
                    }

                    // field3 not found?
                    let field9 = new { field3, field7, x = new { field4 }, y = new[] { field5, field6 } }
                    let field10 = z.Timestamp
                    let field11 = field10.Date
                    let field12 = field11.AddDays(-1)


                    let xFirstOrDefaultQuery =
                        from zz in new xTable()
                        where zz.Key == (xKey)77
                        select zz

                    let xFirstOrDefault = xFirstOrDefaultQuery.FirstOrDefault()



                    let scalarInnerJoinQuery =
                          from xouter in new xTable()
                          join xinner in new xTable() on xouter.field1 equals xinner.field2
                          select new { xouter, xinner }

                    let scalarInnerJoinFirstOrDefault =
                        scalarInnerJoinQuery.FirstOrDefault()

                    //select z.field1;
                    select new
                    {
                        scalarInnerJoinFirstOrDefault,
                        xFirstOrDefault,

                        zExpression = z.field1 > z.field2,
                        zz = z.field1 + z.field2,
                        z.field1,
                        z,


                        z.Timestamp,

                        field3,

                        a = new
                        {
                            scalar2,


                            Tag1 = z.Tag,
                            Tag2 = z.Tag.ToLower(),

                            c2 = 2,
                            c3 = new { c4 = 4 },
                        },

                        scalar2,

                        c1 = 1,
                        f11 = z.field1,
                        f12 = z.field1,
                        f13 = z.field1 + 3,
                        xml = new XElement("xml",
                            new XAttribute("tag", z.Tag)
                            ),


                        aa = new[] {
                            z.Tag,
                            z.Tag
                            }
                    };

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

            Console.ReadKey();
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

        class SQLWriterWithoutLinefeeds : IDisposable
        {
            public Action yield;
            public void Dispose()
            {
                yield();
            }

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

                            // http://dev.mysql.com/doc/refman/5.0/en/comments.html
                            var trace = "/* " + f.GetFileLineNumber().ToString().PadLeft(4, '0') + ":" + context.LineNumber.ToString().PadLeft(4, '0') + " */ ";

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

                            // http://dev.mysql.com/doc/refman/5.0/en/comments.html
                            var trace = "/* " + f.GetFileLineNumber().ToString().PadLeft(4, '0') + ":" + context.LineNumber.ToString().PadLeft(4, '0') + " */ ";

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
                            WriteLine(2, zMemberExpression.Member.Name);
                            return;
                        }
                        #endregion

                        #region xConstantExpression
                        var xConstantExpression = asExpression as ConstantExpression;
                        if (xConstantExpression != null)
                        {
                            //Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + ("? as " + item.m.Name + "+*"));
                            WriteLine(1, "@constant");
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
                    var sql = new SQLWriter(xOrderBy.source, upper.Concat(new[] { source }), context);


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
                    var sql = new SQLWriter(xWhere.source, upper.Concat(new[] { source }), context);

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

                var xSelect = source as xSelect;



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


                          #region xxMethodCallExpression
                          var xxMethodCallExpression = zExpression as MethodCallExpression;
                          if (xxMethodCallExpression != null)
                          {
                              // whatif its a nested query?

                              // !!!


                              #region FirstOrDefault
                              if (xxMethodCallExpression.Method.Name == "FirstOrDefault")
                              {
                                  WriteLineWithColor(1, ("let " + GetTargetName()) + " <- (", ConsoleColor.White);

                                  var __source = xxMethodCallExpression.Arguments[0] as MemberExpression;
                                  // __source = {<>h__TransparentIdentifierd.xFirstOrDefaultQuery}

                                  var __source_asParameterExpression = __source.Expression as ParameterExpression;

                                  // assigned by let
                                  if (xSelect.selector.Parameters[0] == __source_asParameterExpression)
                                  {
                                      // in that case get the damn argument
                                      var xxSelect = xSelect.source as xSelect;
                                      var xxNewExpression = xxSelect.selector.Body as NewExpression;

                                      var ii = xxNewExpression.Members.IndexOf(__source.Member);
                                      var aa = xxNewExpression.Arguments[ii];

                                      // whats aa? the where clause?
                                      // aa = {new xTable().Where(zz => (Convert(zz.Key) == 77))}

                                      var aaMethodCallExpression = aa as MethodCallExpression;
                                      if (aaMethodCallExpression.Method.Name == "Where")
                                      {
                                          var aa_filterQuote = aaMethodCallExpression.Arguments[1] as UnaryExpression;
                                          var aa_source = aaMethodCallExpression.Arguments[0];

                                          var oNewExpression = aa_source as NewExpression;
                                          var newsource = oNewExpression.Constructor.Invoke(new object[0]);

                                          var newsource2 = (IQueryStrategy)aaMethodCallExpression.Method.Invoke(null,
                                                  new object[] {
                                                          newsource,
                                                          aa_filterQuote.Operand
                                                      }
                                              );

                                          var sqalarsql = new SQLWriter(
                                              newsource2,
                                              upper.Concat(new[] { source }).ToArray(),
                                              context
                                          );
                                      }
                                  }

                                  WriteLineWithColor(1, ")", ConsoleColor.White);
                                  return;
                              }
                              #endregion



                              #region Count
                              if (xxMethodCallExpression.Method.Name == "Count")
                              {
                                  // scalar sub query?
                                  WriteLineWithColor(1, ("let " + GetTargetName()) + " <- select count(*) from (", ConsoleColor.White);


                                  var count_source = xxMethodCallExpression.Arguments[0] as MemberExpression;
                                  // [0x00000000] = {<>h__TransparentIdentifier6.scalar1}
                                  // [0x00000000] = {<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.scalar1}

                                  if (count_source != null)
                                  {
                                      // looks like we saved that query somewhere via let?

                                      var m = count_source.Expression as MemberExpression;
                                      // m = {<>h__TransparentIdentifier6.<>h__TransparentIdentifier5}
                                      var mp0ParameterExpression = m.Expression as ParameterExpression;

                                      if (xSelect.selector.Parameters[0] == mp0ParameterExpression)
                                      {
                                          // found it!
                                          // we should access the missing value via outer source?

                                          //var xxSelect = xSelect.source as xSelect;
                                          var xxOrderBy = xSelect.source as xOrderBy;
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

                                                  var sqalarsql = new SQLWriter(
                                                      oOrdered,
                                                      upper.Concat(new[] { source }).ToArray(),
                                                      context
                                                  );

                                              }
                                          }
                                      }
                                  }


                                  WriteLineWithColor(1, ")", ConsoleColor.White);
                                  return;

                              }
                              #endregion

                              // what other methods have we referenced yet?
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

                              WriteLine(1, ("let " + GetTargetName()) + " <- " + zMemberExpression.Member.Name);
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

                          #region xConstantExpression
                          var xConstantExpression = zExpression as ConstantExpression;
                          if (xConstantExpression != null)
                          {
                              using (WithoutLinefeeds())
                              {
                                  WriteLine(1, ("let " + GetTargetName()) + " <- ");
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

                              zNewExpression.Arguments.WithEachIndex(
                                  (SourceArgument, SourceArgumentIndex) =>
                                      WriteProjection(
                                      zsource,
                                          SourceArgument,
                                          Target.Concat(new[] { Tuple.Create(
                                              zNewExpression.Members == null ? null : zNewExpression.Members[SourceArgumentIndex],
                                              SourceArgumentIndex
                                              )
                                          }).ToArray()
                                      )
                              );
                              return;
                          }
                          #endregion

                          #region zParameterExpression
                          var zParameterExpression = zExpression as ParameterExpression;
                          if (zParameterExpression != null)
                          {
                              WriteLine(1, ("proxy " + GetTargetName()) + "");
                              return;
                          }
                          #endregion


                          Debugger.Break();
                      };



                var xSelect_source = xSelect.source;


                // what are looking at? select selector will know the name of the source
                // (source as xSelect).selector.Body = {<>h__TransparentIdentifier6 . <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.z}
                var x = xSelect.selector.Body;

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

                            var xNewArrayExpression = xSelect.selector.Body as NewArrayExpression;
                            if (xNewArrayExpression != null)
                            {
                                // this will look like roslyn dictionary indexer initializer
                                Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + ("new [] "));
                                Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + (" [0] <- ?"));
                                Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + (" [1] <- ?"));
                            }
                            else
                            {
                                var xNewExpression = xSelect.selector.Body as NewExpression;
                                if (xNewExpression != null)
                                {
                                    // we are selecting a group for upper select arent we.

                                    #region xArguments, merge unless its a scalar subselect
                                    var xArguments = xNewExpression.Arguments.Zip(xNewExpression.Members, (a, m) => new { a, m, source }).ToList();

                                    // are we able to merge with sub selects?
                                    if (upper.Any())
                                    {
                                        var ySelect = xSelect_source as xSelect;
                                        while (ySelect != null)
                                        {
                                            var yNewExpression = ySelect.selector.Body as NewExpression;
                                            if (yNewExpression != null)
                                            {
                                                if (yNewExpression.Arguments.Count == 2)
                                                {
                                                    var yParameterExpression = yNewExpression.Arguments[0] as ParameterExpression;
                                                    if (yParameterExpression != null)
                                                    {
                                                        if (yParameterExpression.Name == ySelect.selector.Parameters[0].Name)
                                                        {
                                                            var a = yNewExpression.Arguments[1];

                                                            // unless its a scalar subselect?

                                                            var aMethodCallExpression = a as MethodCallExpression;
                                                            if (aMethodCallExpression != null)
                                                            {

                                                                if (aMethodCallExpression.Method.Name == "FirstOrDefault")
                                                                    break;

                                                                if (aMethodCallExpression.Method.Name == "Count")
                                                                //if (aMethodCallExpression.Method.DeclaringType == typeof(FrikkingExpressionBuilder))
                                                                {
                                                                    break;
                                                                }
                                                            }

                                                            var m = yNewExpression.Members[1];
                                                            WriteLineWithColor(1, "merge " + yParameterExpression.Name + "." + m.Name, ConsoleColor.Gray);

                                                            var item =
                                                                  new
                                                                  {
                                                                      a = a,
                                                                      m = m,
                                                                      source = xSelect_source
                                                                  };


                                                            // we seem to be able to merge!
                                                            xSelect_source = ySelect.source;
                                                            ySelect = xSelect_source as xSelect;




                                                            WriteProjection(item.source, item.a, new[] { Tuple.Create(item.m, -1) });

                                                            continue;
                                                        }
                                                    }
                                                }
                                            }

                                            break;
                                        }
                                    }
                                    #endregion


                                    foreach (var item in xArguments)
                                    {
                                        WriteProjection(item.source, item.a, new[] { Tuple.Create(item.m, -1) });
                                    }
                                }
                                else
                                {
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

                if (xSelect_source == null)
                {
                    // there needs to be an external source. a table in db?

                    WriteLine(0, "from xTable as " + (source as xSelect).selector.Parameters[0].Name);
                }
                else
                {
                    WriteLine(0, "from (");

                    var xsource = new SQLWriter(
                       xSelect_source,
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
            public IEnumerable<LambdaExpression> filter;

            public override string ToString()
            {
                return (source as xSelect).selector.Parameters[0].Name;
            }
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

            public override string ToString()
            {
                return selector.Parameters[0].Name;
            }
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


            public IEnumerable<LambdaExpression> keySelector;
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

        public static
            IQueryStrategy<TResult>
            Join<TOuter, TInner, TKey, TResult>(
            this IQueryStrategy<TOuter> xouter,
            IQueryStrategy<TInner> xinner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
            Expression<Func<TOuter, TInner, TResult>> resultSelector
            )
        {
            return null;
        }
    }
}
