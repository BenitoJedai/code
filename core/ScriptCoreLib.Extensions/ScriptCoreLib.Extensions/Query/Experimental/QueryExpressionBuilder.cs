using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Data;
using System.Data.Common;

namespace ScriptCoreLib.Query.Experimental
{
    #region example generated data layer
    [Obsolete]
    public class xTable : QueryExpressionBuilder.xSelect<xRow>
    {
        public xTable()
        {

            Expression<Func<xRow, xRow>> selector = (xTableDefaultSelector) => new xRow
            {
                Key = xTableDefaultSelector.Key,
                field1 = xTableDefaultSelector.field1,
                field2 = xTableDefaultSelector.field2,
                field3 = xTableDefaultSelector.field3,
                Timestamp = xTableDefaultSelector.Timestamp,
                Tag = xTableDefaultSelector.Tag

                // orderby ?
            };

            this.selector = selector;
        }
    }


    [Obsolete]
    public enum xKey : long { }

    [Obsolete]
    public class xRow
    {
        public xKey Key;

        public int field1;
        public int field2;

        public long field3;

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

        public class SQLWriterContext
        {
            public int LineNumber = 0;
            public IDbCommand Command;



            public int CommandParametersCount;
            public void CommandAddParameter(string ParameterName, object value)
            {
                CommandParametersCount++;

                if (Command != null)
                    Command.AddParameter(ParameterName, value);
            }
        }

        class SQLWriterWithoutLinefeeds : IDisposable
        {
            public Action yield;
            public void Dispose()
            {
                yield();
            }

        }


        delegate void WriteScalarExpressionAction(bool DiscardAlias, Expression asExpression);


        public partial class SQLWriter<TElement>
        {
            //0200004f ScriptCoreLib.Query.Experimental.QueryExpressionBuilder+SQLWriter`1+<>c__DisplayClass64
            //script: error JSC1000: unsupported flow detected, try to simplify.
            // Assembly V:\TestSQLiteConnection.Application.exe
            // DeclaringType ScriptCoreLib.Query.Experimental.QueryExpressionBuilder+SQLWriter`1+<>c__DisplayClass64, TestSQLiteConnection.Application, Version= 1.0.0.0, Culture= neutral, PublicKeyToken= null
            // OwnerMethod<.ctor> b__3e
            // Offset 037a
            // .Try ommiting the return, break or continue instruction.



            public SQLWriter(
                IQueryStrategy source,
                IEnumerable<IQueryStrategy> upper,
                SQLWriterContext context = null,
                ParameterExpression upperParameter = null,
                 IDbCommand Command = null)
            {

                Action<string> Write =
                    text =>
                    {
                        if (Command != null)
                            Command.CommandText += text;

                        Console.Write(text);
                    };




                // selector = {<>h__TransparentIdentifier6 => <>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.z}

                // convert to SQL!

                var xSelect = source as xSelect;


                if (context == null)
                    context = new SQLWriterContext { Command = Command };

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
                                    Write("\r\n");
                            }
                        };
                    };
                #endregion

                // X:\jsc.svn\examples\javascript\LINQ\GGearAlpha\GGearAlpha\Application.cs
                #region DoWithoutLinefeeds
                Action<Action> DoWithoutLinefeeds =
                y =>
                {
                    if (WithoutLinefeedsCounter == 0)
                        WithoutLinefeedsDirty = false;

                    WithoutLinefeedsCounter++;

                    y();

                    WithoutLinefeedsCounter--;

                    if (WithoutLinefeedsCounter == 0)
                        Write("\r\n");
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
                          Write(trace + "".PadLeft(upper.Count() + padding, ' '));

                      }


                      Console.ForegroundColor = c;

                      Write("/* " + text + " */");

                      Console.ForegroundColor = old.ForegroundColor;

                      if (WithoutLinefeedsCounter == 0)
                      {
                          Write("\r\n");
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
                            Write(trace + "".PadLeft(upper.Count() + padding, ' '));

                        }


                        Console.ForegroundColor = c;
                        Write(text);
                        Console.ForegroundColor = old.ForegroundColor;

                        if (WithoutLinefeedsCounter == 0)
                        {
                            Write("\r\n");
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

                            Write(trace + "".PadLeft(upper.Count() + padding, ' '));


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


                        Write(text);
                        Console.ForegroundColor = old.ForegroundColor;

                        if (WithoutLinefeedsCounter == 0)
                        {
                            Write("\r\n");
                        }


                    };


                #endregion

                // called by?
                #region OBSOLETE WriteScalarExpression
                var WriteScalarExpression = default(WriteScalarExpressionAction);

                WriteScalarExpression =
                    (DiscardAlias, asExpression) =>
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
                                WriteScalarExpression(true, asBinaryExpression.Left);


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

                                WriteScalarExpression(true, asBinaryExpression.Right);
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

                                WriteScalarExpression(DiscardAlias, asUnaryExpression.Operand);
                                return;
                            }
                            else if (asUnaryExpression.NodeType == ExpressionType.Not)
                            {

                                WriteLineWithColor(1, "not(", ConsoleColor.White);

                                // ?
                                WriteScalarExpression(true, asUnaryExpression.Operand);

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
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWhere\Program.cs
                            // are we looking at a constant?

                            var GetValue = default(Func<object>);

                            #region xConstantExpression
                            Action<MemberExpression, Action<object>> yy = null;

                            yy = (xx, yield) =>
                            {
                                #region atyield
                                Action<object> atyield =
                                    __value =>
                                    {
                                        var xPropertyInfo = xx.Member as PropertyInfo;
                                        if (xPropertyInfo != null)
                                        {
                                            yield(
                                                xPropertyInfo.GetValue(__value, null)
                                            );
                                            return;
                                        }


                                        var xFieldInfo = xx.Member as FieldInfo;
                                        if (xFieldInfo != null)
                                        {
                                            yield(
                                                xFieldInfo.GetValue(__value)
                                            );
                                            // __value2 = { u = { n = C } }
                                            return;
                                        }
                                    };
                                #endregion


                                var xConstantExpression = xx.Expression as ConstantExpression;
                                if (xConstantExpression != null)
                                {
                                    // z = { u = { n = C } }
                                    atyield(xConstantExpression.Value);
                                }


                                if (xx.Expression is MemberExpression)
                                    yy(xx.Expression as MemberExpression, atyield);
                            };
                            #endregion

                            yy(zMemberExpression,
                                 __value =>
                                 {
                                     GetValue = () => __value;
                                 }
                            );

                            if (GetValue != null)
                            {
                                var __value = GetValue();


                                var ParameterName = "@WriteScalarMemberExpressionArgument" + context.CommandParametersCount;
                                context.CommandAddParameter(ParameterName, __value);


                                //WriteLineWithColor(1, "@arg(" + xConstantExpression.Value + ")", ConsoleColor.Red);
                                WriteLineWithColor(1, ParameterName, ConsoleColor.Red);

                                //WriteLineWithColor(1, "@arg(" + __value + ")", ConsoleColor.Red);
                                return;
                            }


                            using (WithoutLinefeeds())
                            {
                                var xxDelete = source as xDelete;
                                if (xxDelete == null)
                                {
                                    // if we are inside a delete, we can not do any aliasing can we?
                                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestDeleteWhere\Program.cs

                                    var zMMemberExpression = zMemberExpression.Expression as MemberExpression;
                                    if (zMMemberExpression != null)
                                    {
                                        WriteLine(1, "`");
                                        WriteLineWithColor(2, zMMemberExpression.Member.Name, ConsoleColor.Magenta);
                                        WriteLine(1, "`");
                                        WriteLineWithColor(2, ".", ConsoleColor.Magenta);
                                    }


                                    var zMParameterExpression = zMemberExpression.Expression as ParameterExpression;
                                    if (zMParameterExpression != null)
                                    {
                                        WriteLine(1, "`");
                                        WriteLineWithColor(2, zMParameterExpression.Name, ConsoleColor.Magenta);
                                        WriteLine(1, "`");
                                        WriteLineWithColor(2, ".", ConsoleColor.Magenta);
                                    }

                                }

                                // xMySQL likes its Keys quoted
                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
                                WriteLine(1, "`");
                                WriteLineWithColor(2, zMemberExpression.Member.Name, ConsoleColor.Cyan);
                                WriteLine(1, "`");


                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectScalarXElementField\Program.cs


                                if (DiscardAlias)
                                {
                                    // are we in order by?
                                }
                                else
                                {
                                    WriteLine(1, " as ");

                                    WriteLine(1, "`");
                                    WriteLineWithColor(2, zMemberExpression.Member.Name, ConsoleColor.Cyan);
                                    WriteLine(1, "`");
                                }
                            }

                            return;
                        }
                        #endregion

                        #region WriteScalarExpression:xConstantExpression
                        {
                            var xConstantExpression = asExpression as ConstantExpression;
                            if (xConstantExpression != null)
                            {
                                //Console.WriteLine("".PadLeft(upper.Count() + 1, ' ') + ("? as " + item.m.Name + "+*"));
                                //WriteLine(1, "@constant " + new { xConstantExpression.Value });

                                // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.IDbConnection.Insert.cs



                                var ParameterName = "@WriteScalarConstantExpressionArgument" + context.CommandParametersCount;
                                context.CommandAddParameter(ParameterName, xConstantExpression.Value);

                                //WriteLineWithColor(1, "@arg(" + xConstantExpression.Value + ")", ConsoleColor.Red);
                                WriteLineWithColor(1, ParameterName, ConsoleColor.Red);
                                return;
                            }
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



                #region xDelete
                var xDelete = source as xDelete;
                if (xDelete != null)
                {
                    // whats 
                    // http://codesynthesis.com/~boris/blog/2013/02/13/odb-2-2-0-released/

                    // SQL delete? sub queries?
                    // http://www.w3schools.com/sql/sql_delete.asp
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs

                    //delete from XApplicationPerformance 
                    //where Key in (select Key from XApplicationPerformance where Key = 2)


                    // to allow subqueries we need to rsort to using .Key in ? 
                    // what about join?
                    // http://stackoverflow.com/questions/10854520/how-do-i-create-a-sql-delete-statement-to-delete-rows-in-a-3rd-table

                    //delete from XApplicationPerformance 
                    //where Key in (select x.Key from XApplicationPerformance as x inner join XApplicationPerformance as y on x.Key = y.Key  where x.Key = 2)

                    // um then we would need to delete from both join sources?
                    // lets support simple delete and delete all for now instead.



                    var xxSelect = xDelete.source as xSelect;
                    if (xxSelect != null)
                    {
                        // whats the name of the damn table?

                        if (xxSelect.source == null)
                        {
                            // there needs to be an external source. a table in db?

                            // why not drop the table while you ar at it?

                            using (WithoutLinefeeds())
                            {
                                WriteLine(0, "delete from ");
                                WriteLineWithColor(0, "" + xxSelect.selector.Parameters[0].Name, ConsoleColor.Magenta);
                            }
                            return;
                        }
                    }


                    var xxWhere = xDelete.source as xWhere;
                    if (xxWhere != null)
                    {
                        // ok. we are about to support where clauses.
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestDeleteWhere\Program.cs


                        var xxxSelect = xxWhere.source as xSelect;
                        if (xxxSelect != null)
                        {
                            // whats the name of the damn table?

                            if (xxxSelect.source == null)
                            {
                                // there needs to be an external source. a table in db?

                                // why not drop the table while you ar at it?

                                using (WithoutLinefeeds())
                                {
                                    WriteLine(0, "delete from ");
                                    WriteLineWithColor(0, "" + xxxSelect.selector.Parameters[0].Name, ConsoleColor.Magenta);


                                }

                                xxWhere.filter.WithEachIndex(
                                    (wExpression, wExpressionIndex) =>
                                    {
                                        using (WithoutLinefeeds())
                                        {
                                            if (wExpressionIndex == 0)
                                                WriteLine(0, "where ");
                                            else
                                                WriteLine(1, "and ");

                                            WriteScalarExpression(true, wExpression.Body);
                                        }
                                    }
                                );

                                return;
                            }
                        }
                    }

                    // not yet supported!
                    Debugger.Break();
                }
                #endregion


                #region xTake
                var xTake = source as xTake;
                if (xTake != null)
                {
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestOrderByDescending\Program.cs
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestOrderBy\Program.cs
                    var sql = new SQLWriter<TElement>(
                        xTake.source,
                        upper.Concat(new[] { source }),
                        context,
                        upperParameter: upperParameter,
                        Command: Command);

                    using (WithoutLinefeeds())
                    {
                        WriteLine(0, "limit ");
                        WriteLineWithColor(0, "" + xTake.count, ConsoleColor.Yellow);
                    }

                    return;
                }
                #endregion




                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectWhereOrderBy\Program.cs
                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectWhereOrderBy\Program.cs
                // X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs

                // we are basically doing a proxy for one field arent we...

                #region xWhere
                var xWhere = source as xWhere;
                if (xWhere != null)
                {
                    // see also xDelete

                    var sql = new SQLWriter<TElement>(
                        xWhere.source,
                        upper.Concat(new[] { source }),
                        context, upperParameter:
                        xWhere.filter.First().Parameters[0],
                        Command: Command
                        );

                    xWhere.filter.WithEachIndex(
                        (wExpression, wExpressionIndex) =>
                        {
                            using (WithoutLinefeeds())
                            {
                                if (wExpressionIndex == 0)
                                    WriteLine(0, "where ");
                                else
                                    WriteLine(1, "and ");

                                WriteScalarExpression(true, wExpression.Body);
                            }
                        }
                    );


                    return;
                }
                #endregion





                Action<IQueryStrategy, Expression, Tuple<MemberInfo, int>[], Tuple<string, MemberInfo, int>[]> WriteProjectionProxy = null;
                Action<IQueryStrategy, Expression, Tuple<MemberInfo, int>[]> WriteProjection = null;

                // called by? WriteProjection:
                #region ? WriteScalarOperand

                Action<IQueryStrategy, MethodCallExpression, Func<string>, MethodInfo, Tuple<MemberInfo, int>[]> WriteScalarOperand =
                    (zsource, xxMethodCallExpression, GetTargetName, Operand, Target) =>
                    {
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarCount\Program.cs

                        // [0] = {new xTable().Select(y => y.Tag)}



                        // x:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByScalarFirstOrDefault\Program.cs

                        var zSelect = zsource as xSelect;
                        if (zSelect == null)
                        {
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByThenSelectKeyCount\Program.cs

                            WriteLine(1, "?");
                            return;
                        }

                        if (zSelect.source is xGroupBy)
                        {

                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByThenSelectKeyCount\Program.cs
                            // called by WriteProjection:xxMethodCallExpression
                            // Count
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxGroupByConstantSelectCount\Program.cs

                            using (WithoutLinefeeds())
                            {
                                WriteCommentLine(1, "proxy");

                                //WriteProjectionProxy(
                                //    zsource, xxMethodCallExpression, Target, null);

                                WriteLine(1, " ");

                                WriteLine(1, zSelect.selector.Parameters[0].Name);
                                WriteLine(1, ".");

                                WriteLine(1, GetTargetName());

                                WriteLineWithColor(1, " as  ", ConsoleColor.Magenta);

                                WriteLine(1, "`");
                                WriteLineWithColor(1, GetTargetName(), ConsoleColor.Cyan);
                                WriteLine(1, "`");
                            }
                            return;
                        }

                        var __source = xxMethodCallExpression.Arguments[0] as MemberExpression;

                        if (__source == null)
                        {
                            Debugger.Break();

                        }


                        #region yaa
                        Action<Expression> yaa =
                            aa =>
                            {
                                // X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs
                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectScalarFirstOrDefault\Program.cs

                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxScalarUpper\Program.cs
                                if (Target.Length > 0)
                                {
                                    using (WithoutLinefeeds())
                                    {
                                        WriteCommentLine(1, "let");

                                        if (Target.Last().Item2 > 0)
                                            WriteLine(1, ",");

                                        //var uSelect = upper.LastOrDefault() as xSelect;
                                        //if (uSelect != null)
                                        //{
                                        //    WriteLineWithColor(0, uSelect.selector.Parameters[0].Name, ConsoleColor.DarkCyan);
                                        //    WriteLine(1, " ");
                                        //}

                                        //WriteLineWithColor(1, GetTargetName(), ConsoleColor.Cyan);
                                        // ?
                                        //WriteLineWithColor(1, " <- (", ConsoleColor.White);
                                        WriteLineWithColor(1, " (", ConsoleColor.White);
                                    }
                                }

                                // whats aa? the where clause?
                                // aa = {new xTable().Where(zz => (Convert(zz.Key) == 77))}



                                #region  yyaa
                                Action<MethodCallExpression, Action<IQueryStrategy>> yyaa = null;


                                yyaa =
                                    (aa_MethodCallExpression, yield) =>
                                    {
                                        var IsOrderBy = aa_MethodCallExpression.Method.Name == xReferencesOfLong.OrderByReference.Method.Name;
                                        var IsThenBy = aa_MethodCallExpression.Method.Name == xReferencesOfLong.ThenByReference.Method.Name;
                                        var IsOrderByDescending = aa_MethodCallExpression.Method.Name == xReferencesOfLong.OrderByDescendingReference.Method.Name;
                                        var IsThenByDescending = aa_MethodCallExpression.Method.Name == xReferencesOfLong.ThenByDescendingReference.Method.Name;


                                        // desc ?
                                        #region scalar:OrderBy
                                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxScalarWhereOrderByDescending\Program.cs
                                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxScalarOrderByThenBy\Program.cs
                                        if (IsOrderBy || IsOrderByDescending || IsThenBy || IsThenByDescending)
                                        {
                                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarOrderByFirstOrDefault\Program.cs

                                            var aa_filterQuote = aa_MethodCallExpression.Arguments[1] as UnaryExpression;

                                            #region aa_source_NewExpression
                                            var aa_source_NewExpression = aa_MethodCallExpression.Arguments[0] as NewExpression;
                                            if (aa_source_NewExpression != null)
                                            {
                                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarWhereFirstOrDefault\Program.cs
                                                var aa_sourcei = aa_source_NewExpression.Constructor.Invoke(new object[0]);

                                                var newsource2 = (IQueryStrategy)aa_MethodCallExpression.Method.Invoke(null,
                                                    new object[] {
                                                        aa_sourcei,
                                                        aa_filterQuote.Operand
                                                    }
                                                );

                                                yield(
                                                    newsource2
                                                );
                                                return;

                                            }
                                            #endregion

                                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarWhereWhereFirstOrDefault\Program.cs
                                            var aa_source_MethodCallExpression = aa_MethodCallExpression.Arguments[0] as MethodCallExpression;

                                            yyaa(aa_source_MethodCallExpression,
                                                aa_sourcei =>
                                                {
                                                    var newsource2 = (IQueryStrategy)aa_MethodCallExpression.Method.Invoke(null,
                                                            new object[] {
                                                                    aa_sourcei,
                                                                    aa_filterQuote.Operand
                                                                }
                                                    );

                                                    yield(
                                                        newsource2
                                                    );
                                                }
                                            );

                                            return;
                                        }
                                        #endregion

                                        #region scalar:Where
                                        if (aa_MethodCallExpression.Method.Name == xReferencesOfLong.WhereReference.Method.Name)
                                        {
                                            var aa_filterQuote = aa_MethodCallExpression.Arguments[1] as UnaryExpression;

                                            #region aa_source_NewExpression
                                            var aa_source_NewExpression = aa_MethodCallExpression.Arguments[0] as NewExpression;
                                            if (aa_source_NewExpression != null)
                                            {
                                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarWhereFirstOrDefault\Program.cs
                                                var aa_sourcei = aa_source_NewExpression.Constructor.Invoke(new object[0]);
                                                var newsource2 = (IQueryStrategy)aa_MethodCallExpression.Method.Invoke(null,
                                                    new object[] {
                                                        aa_sourcei,
                                                        aa_filterQuote.Operand
                                                    }
                                                );

                                                yield(
                                                    newsource2
                                                );
                                                return;

                                            }
                                            #endregion

                                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarWhereWhereFirstOrDefault\Program.cs
                                            var aa_source_MethodCallExpression = aa_MethodCallExpression.Arguments[0] as MethodCallExpression;

                                            yyaa(aa_source_MethodCallExpression,
                                                aa_sourcei =>
                                                {
                                                    var newsource2 = (IQueryStrategy)aa_MethodCallExpression.Method.Invoke(null,
                                                            new object[] {
                                                                    aa_sourcei,
                                                                    aa_filterQuote.Operand
                                                                }
                                                    );

                                                    yield(
                                                        newsource2
                                                    );
                                                }
                                            );

                                            return;
                                        }
                                        #endregion


                                        #region scalar:Select
                                        if (aa_MethodCallExpression.Method.Name == xReferencesOfLong.SelectReference.Method.Name)
                                        {
                                            var aa_selectorQuote = aa_MethodCallExpression.Arguments[1] as UnaryExpression;

                                            #region aa_source_NewExpression
                                            var aa_source_NewExpression = aa_MethodCallExpression.Arguments[0] as NewExpression;
                                            if (aa_source_NewExpression != null)
                                            {
                                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarFirstOrDefault\Program.cs
                                                var aaaa_sourcei = (IQueryStrategy)aa_source_NewExpression.Constructor.Invoke(new object[0]);


                                                // let!

                                                var newsource1 = (IQueryStrategy)aa_MethodCallExpression.Method.Invoke(null,
                                                        new object[] {
                                                                            aaaa_sourcei,
                                                                            aa_selectorQuote.Operand
                                                                        }
                                                );

                                                yield(
                                                    newsource1
                                                );
                                                return;
                                            }
                                            #endregion



                                            var aa_source_MethodCallExpression = aa_MethodCallExpression.Arguments[0] as MethodCallExpression;

                                            yyaa(aa_source_MethodCallExpression,
                                              aa_sourcei =>
                                              {
                                                  var newsource2 = (IQueryStrategy)aa_MethodCallExpression.Method.Invoke(null,
                                                          new object[] {
                                                                    aa_sourcei,
                                                                    aa_selectorQuote.Operand
                                                                }
                                                  );

                                                  yield(
                                                      newsource2
                                                  );
                                              }
                                          );

                                            //var aa_source_MethodCallExpression = aa_MethodCallExpression.Arguments[0] as MethodCallExpression;
                                            //if (aa_source_MethodCallExpression != null)
                                            //{
                                            //    var aaa_sQuote = aa_source_MethodCallExpression.Arguments[1] as UnaryExpression;
                                            //    // the let key word!
                                            //    // [0x00000001] = {<>h__TransparentIdentifier2 => (<>h__TransparentIdentifier2.xx.field1 == 44)}
                                            //    var aaa_source_NewExpression = aa_source_MethodCallExpression.Arguments[0] as NewExpression;
                                            //    if (aaa_source_NewExpression != null)
                                            //    {
                                            //        var aaa_sourcei = aaa_source_NewExpression.Constructor.Invoke(new object[0]);

                                            //        var newsource2 = (IQueryStrategy)aa_source_MethodCallExpression.Method.Invoke(null,
                                            //              new object[] {
                                            //            aaa_sourcei,
                                            //            aaa_sQuote.Operand
                                            //        }
                                            //        );

                                            //        var newsource3 = (IQueryStrategy)aa_MethodCallExpression.Method.Invoke(null,
                                            //                  new object[] {
                                            //                        newsource2,
                                            //                        aa_selectorQuote.Operand
                                            //                    }
                                            //            );

                                            //        var sqalarsql = new SQLWriter<TElement>(
                                            //             newsource2,
                                            //             upper.Concat(new[] { source }).ToArray(),
                                            //             context,
                                            //             Command: Command
                                            //         );
                                            //    }
                                            //    else
                                            //    {

                                            //        #region aaa2_source_MethodCallExpression
                                            //        var aaa2_source_MethodCallExpression = aa_source_MethodCallExpression.Arguments[0] as MethodCallExpression;
                                            //        // select

                                            //        var aaaa_sQuote = aaa2_source_MethodCallExpression.Arguments[1] as UnaryExpression;
                                            //        var aaaa_source_NewExpression = aaa2_source_MethodCallExpression.Arguments[0] as NewExpression;
                                            //        var aaaa_sourcei = aaaa_source_NewExpression.Constructor.Invoke(new object[0]);


                                            //        var newsource3 = (IQueryStrategy)aaa2_source_MethodCallExpression.Method.Invoke(null,
                                            //             new object[] {
                                            //            aaaa_sourcei,
                                            //            aaaa_sQuote.Operand
                                            //        }
                                            //       );

                                            //        var newsource2 = (IQueryStrategy)aa_source_MethodCallExpression.Method.Invoke(null,
                                            //             new object[] {
                                            //            newsource3,
                                            //            aaa_sQuote.Operand
                                            //        }
                                            //       );

                                            //        var sqalarsql = new SQLWriter<TElement>(
                                            //             newsource2,
                                            //             upper.Concat(new[] { source }).ToArray(),
                                            //             context,
                                            //             Command: Command
                                            //         );
                                            //        #endregion



                                            //    }

                                            //    return;
                                            //}


                                            //{
                                            //    // ?

                                            //}


                                            return;
                                        }
                                        #endregion


                                        #region scalar:GroupBy
                                        if (aa_MethodCallExpression.Method.Name == xReferencesOfLong.GroupByReference.Method.Name)
                                        {
                                            var aa_kQuote = aa_MethodCallExpression.Arguments[1] as UnaryExpression;
                                            var aa_source = aa_MethodCallExpression.Arguments[0] as NewExpression;
                                            var aa_sourcei = aa_source.Constructor.Invoke(new object[0]);

                                            var newsource2 = (IQueryStrategy)aa_MethodCallExpression.Method.Invoke(null,
                                                    new object[] {
                                                              aa_sourcei,
                                                              aa_kQuote.Operand
                                                          }
                                                );

                                            var sqalarsql = new SQLWriter<TElement>(
                                                newsource2,
                                                upper.Concat(new[] { source }).ToArray(),
                                                context,
                                                Command: Command
                                            );

                                            return;
                                        }
                                        #endregion

                                        #region scalar:Join
                                        if (aa_MethodCallExpression.Method.Name == xReferencesOfLong.JoinReference.Method.Name)
                                        {
                                            // can we ve fast templates of the quoted params??

                                            var aa_outer = aa_MethodCallExpression.Arguments[0] as NewExpression;
                                            var aa_outeri = aa_outer.Constructor.Invoke(new object[0]);

                                            var aa_inner = aa_MethodCallExpression.Arguments[1];
                                            var aa_inneri = aa_outer.Constructor.Invoke(new object[0]);

                                            var aa_of = aa_MethodCallExpression.Arguments[2] as UnaryExpression;
                                            var aa_if = aa_MethodCallExpression.Arguments[3] as UnaryExpression;
                                            var aa_rs = aa_MethodCallExpression.Arguments[4] as UnaryExpression;

                                            var newsource2 = (IQueryStrategy)aa_MethodCallExpression.Method.Invoke(null,
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


                                                // x:\jsc.svn\examples\java\hybrid\test\testjvmclrarrayinitasenumerable\testjvmclrarrayinitasenumerable\program.cs
                                                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140811
                                                //  new IQueryStrategy[1][0] = this.CS___8__locals38.CS___8__locals19.CS___8__locals18.source;
                                                // ?

                                                upper.Concat(new[] { source }).ToArray(),

                                                context,
                                                Command: Command
                                            );
                                            return;
                                        }
                                        #endregion




                                        WriteLineWithColor(1, "?", ConsoleColor.White);
                                        //Debugger.Break();
                                    };
                                #endregion



                                yyaa(aa as MethodCallExpression,
                                    newsource2 =>
                                    {
                                        var xxScalar = new xScalar { source = newsource2, Operand = Operand };

                                        var sqalarsql = new SQLWriter<TElement>(
                                            xxScalar,
                                            upper.Concat(new[] { source }).ToArray(),
                                            context,
                                            Command: Command
                                        );
                                    }
                                );

                                if (Target.Length > 0)
                                {
                                    using (WithoutLinefeeds())
                                    {
                                        WriteLineWithColor(1, ")", ConsoleColor.White);

                                        WriteLine(1, " as ");
                                        if (upperParameter != null)
                                        {
                                            WriteCommentLine(0, upperParameter.Name);
                                            WriteLine(1, " ");
                                        }
                                        WriteLine(1, "`");
                                        WriteLineWithColor(0, GetTargetName(), ConsoleColor.Magenta);
                                        WriteLine(1, "`");
                                    }
                                }
                            };
                        #endregion



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


                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByThenSelectKeyCount\Program.cs
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectNewExpressionScalarFirstOrDefault\Program.cs
                        walker(zSelect.source as xSelect, __source.Expression);
                    };
                #endregion


                #region WriteProjectionProxy
                // how does a proxy differ from projection?
                // called by?
                WriteProjectionProxy =
                    (zsource, zExpression, Target, Source) =>
                    {
                        //WriteCommentLine(1, "WriteProjectionProxy");


                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestJoinOnNewExpression\Program.cs



                        #region WriteProjectionProxy:GetSourceName
                        Func<string> GetSourceName =
                            delegate
                            {
                                var w = "";

                                if (Target != null)
                                    foreach (var item in Source)
                                    {
                                        if (item.Item1 != null)
                                        {
                                            if (!string.IsNullOrEmpty(w))
                                                w += ".";

                                            w += item.Item1;
                                        }
                                        else if (item.Item2 == null)
                                            w += "[" + item.Item3 + "]";
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(w))
                                                w += ".";

                                            w += item.Item2.Name + "";
                                        }

                                    }

                                return w;
                            };
                        #endregion

                        #region WriteProjectionProxy:GetSourceNameWithQuotes
                        Func<string> GetSourceNameWithQuotes =
                            delegate
                            {
                                var w = "";
                                var i = 0;
                                w += "`";
                                var needToClose = false;

                                if (Target != null)
                                    foreach (var item in Source)
                                    {
                                        if (item.Item1 != null)
                                        {
                                            if (i > 0)
                                            {
                                                if (i == 1)
                                                {
                                                    w += ".`";
                                                    needToClose = true;
                                                }
                                                else
                                                    w += ".";
                                            }


                                            w += item.Item1;
                                        }
                                        else if (item.Item2 == null)
                                            w += "[" + item.Item3 + "]";
                                        else
                                        {
                                            if (i > 0)
                                            {
                                                if (i == 1)
                                                {
                                                    w += ".`";
                                                    needToClose = true;
                                                }
                                                else
                                                    w += ".";
                                            }


                                            w += item.Item2.Name + "";
                                        }





                                        if (i == 0)
                                            w += "`";

                                        i++;
                                    }

                                if (needToClose)
                                    w += "`";

                                return w;
                            };
                        #endregion

                        #region WriteProjectionProxy:GetTargetNameWithQuotes
                        Func<string> GetTargetNameWithQuotes =
                            delegate
                            {
                                var w = "";
                                var i = 0;
                                w += "`";
                                var needToClose = false;

                                if (Target != null)
                                    foreach (var item in Target)
                                    {
                                        if (item.Item1 == null)
                                            w += "[" + item.Item2 + "]";
                                        else
                                        {
                                            if (i > 0)
                                            {
                                                if (i == 1)
                                                {
                                                    w += ".`";
                                                    needToClose = true;
                                                }
                                                else
                                                    w += ".";
                                            }

                                            w += item.Item1.Name + "";
                                        }

                                        if (i == 0)
                                            w += "`";

                                        i++;
                                    }

                                if (needToClose)
                                    w += "`";

                                return w;
                            };
                        #endregion



                        #region WriteProjectionProxy:GetTargetName
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


                                            // 		LastReference.Method.Name == item.Item1.Name	true	bool

                                            if (xReferencesOfLong.LastReference.Method.Name == item.Item1.Name)
                                            {
                                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWebOrderByThenGroupBy\Application.cs
                                                // keep Last method name from being obfuscated for js..
                                                w += "Last";

                                            }
                                            else
                                            {

                                                w += item.Item1.Name + "";
                                            }
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
                                    var zzOrderBy = zzsource as xOrderBy;
                                    if (zzOrderBy != null)
                                    {
                                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWhere\Program.cs
                                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestOrderByDescending\Program.cs

                                        y(zzOrderBy.source);
                                        return;
                                    }

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
                                            Target,
                                            Source
                                        );

                                        return;
                                    }

                                    var zzSelect = zzsource as xSelect;
                                    if (zzSelect != null)
                                    {
                                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxLetGroupBy\Program.cs

                                        WriteProjectionProxy(zzSelect, zzSelect.selector.Body, Target,


                                            Source
                                        );


                                        return;
                                    }



                                    var zzJoin = zzsource as xJoin;
                                    if (zzJoin != null)
                                    {
                                        WriteProjectionProxy(zzJoin, zzJoin.resultSelector.Body, Target, Source);
                                        return;
                                    }
                                };
                            #endregion

                            #region WriteProjectionProxy:zParameterExpression:zGroupBy
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


                        #region WriteProjectionProxy:MemberInitExpression
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMemberInitExpression\Program.cs
                        var zMemberInitExpression = zExpression as MemberInitExpression;
                        if (zMemberInitExpression != null)
                        {
                            WriteCommentLine(1, "WriteProjectionProxy:MemberInitExpression");

                            WriteProjectionProxy(zsource, zMemberInitExpression.NewExpression, Target, Source);

                            // what about XElement?

                            zMemberInitExpression.Bindings.WithEachIndex(
                                (SourceBinding_1585, SourceBindingIndex) =>
                                {
                                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140816/jvm

                                    var item = new
                                    {
                                        a = (SourceBinding_1585 as MemberAssignment).Expression,
                                        m = SourceBinding_1585.Member
                                    };

                                    var Target_Concat = Target.Concat(new[] { Tuple.Create(item.m, SourceBindingIndex) });

                                    // will this cause a type mismatch?
                                    var Target_Array = Target_Concat.ToArray();

                                    WriteProjectionProxy(zsource, item.a,
                                        Target_Array,
                                        Source.Concat(new[] { Tuple.Create(default(string), item.m, SourceBindingIndex) }).ToArray()


                                            );
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

                                              Target.Concat(new[] { Tuple.Create(default(MemberInfo), SourceArgumentIndex) }).ToArray(),
                                              Source.Concat(new[] { Tuple.Create(default(string), default(MemberInfo), SourceArgumentIndex) }).ToArray()
                                          )
                                  );
                                return;
                            }

                            zNewExpression.Arguments.WithEachIndex(
                                (SourceArgument, SourceArgumentIndex) =>
                                    WriteProjectionProxy(
                                    zsource,
                                        SourceArgument,
                                        Target.Concat(new[] { Tuple.Create(zNewExpression.Members[SourceArgumentIndex], SourceArgumentIndex) }).ToArray(),
                                        Source.Concat(new[] { Tuple.Create(default(string), zNewExpression.Members[SourceArgumentIndex], SourceArgumentIndex) }).ToArray()
                                    )
                            );
                            return;
                        }
                        #endregion

                        #region WriteProjectionProxy:zMemberExpression
                        var zMemberExpression = zExpression as MemberExpression;
                        if (zMemberExpression != null)
                        {
                            WriteCommentLine(1, "WriteProjectionProxy:MemberExpression");


                            // js still dont like using/return
                            //DoWithoutLinefeeds(
                            //delegate

                            using (WithoutLinefeeds())
                            {
                                // we have to unpack everything?


                                WriteCommentLine(1, "proxy");

                                if (Target.Last().Item2 > 0)
                                    WriteLine(1, ",");




                                WriteLine(1, " ");

                                // this can not be correct
                                // GetSourceNameWithQuotes
                                //WriteLine(1, "" + GetTargetNameWithQuotes());

                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByScalar\Program.cs
                                //WriteLine(1, "" + GetSourceName());
                                WriteLine(1, "" + GetSourceNameWithQuotes());

                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxOrderByThenGroupBy\Program.cs
                                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140726


                                //WriteLine(1, "x.");
                                //WriteLineWithColor(0, zMemberExpression.Member.Name, ConsoleColor.Magenta);


                                WriteLine(1, " as ");
                                if (upperParameter != null)
                                {
                                    WriteCommentLine(0, upperParameter.Name);
                                    WriteLine(1, " ");
                                }
                                WriteLine(1, "`");
                                WriteLineWithColor(0, GetTargetName(), ConsoleColor.Cyan);
                                WriteLine(1, "`");
                            }
                            //);

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

                                WriteCommentLine(1, "proxy");

                                if (Target.Last().Item2 > 0)
                                    WriteLine(1, ", ");



                                WriteLine(1, "" + GetTargetNameWithQuotes());
                                //WriteLineWithColor(0, zParameterExpression.Name, ConsoleColor.Magenta);

                                WriteLine(1, " as ");

                                if (upperParameter != null)
                                {
                                    WriteCommentLine(0, upperParameter.Name);
                                    WriteLine(1, " ");
                                }


                                WriteLine(1, "`");
                                WriteLineWithColor(0, GetTargetName(), ConsoleColor.Magenta);
                                WriteLine(1, "`");
                            }
                            return;
                        }
                        #endregion

                        #region WriteProjectionProxy:zBinaryExpression
                        var zBinaryExpression = zExpression as BinaryExpression;
                        if (zBinaryExpression != null)
                        {
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestOrderByDescending\Program.cs
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs
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
                            WriteCommentLine(1, "WriteProjectionProxy:MethodCallExpression");

                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarMin\Program.cs
                            // we should not proxy partial queries, like select

                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectScalarCount\Program.cs
                            using (WithoutLinefeeds())
                            {
                                // we have to unpack everything?

                                if (zMethodCallExpression.Method.DeclaringType == typeof(QueryExpressionBuilder))
                                {
                                    // its one of our own?

                                    WriteCommentLine(1, "proxy " + GetTargetName() + " <- " + zMethodCallExpression.Method.Name);
                                }
                                else
                                {

                                    // when is this happening?
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

                            }




                            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140809
                            // jsc cant cope with it?
                            return;
                        }
                        #endregion

                        #region WriteProjectionProxy:zUnaryExpression
                        var zUnaryExpression = zExpression as UnaryExpression;
                        if (zUnaryExpression != null)
                        {
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectXElement\Program.cs
                            // Method = {System.Xml.Linq.XName op_Implicit(System.String)}

                            // descending xml: { IsCompleted = false, Result =  }
                            if (zUnaryExpression.NodeType == ExpressionType.Convert)
                            {
                                // could we just discard the type?
                                WriteProjectionProxy(zsource, zUnaryExpression.Operand, Target, Source);
                                // tested by
                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
                                return;
                            }


                            // when does this happen?
                            WriteLine(1, ("let " + GetTargetName()) + " <- unary");
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
                          //WriteCommentLine(1, "WriteProjection");

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
                              // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140726
                              // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxLetGroupBy\Program.cs

                              WriteProjectionProxy(zsource, zParameterExpression, Target,


                                  new[] { new Tuple<string, MemberInfo, int>(zParameterExpression.Name, null, 0) }

                              );

                              return;
                          }
                          #endregion


                          #region WriteProjection:xxMethodCallExpression
                          var xxMethodCallExpression = zExpression as MethodCallExpression;
                          if (xxMethodCallExpression != null)
                          {
                              // whatif its a nested query?

                              // !!!

                              #region QueryExpressionBuilder::
                              if (xxMethodCallExpression.Method.DeclaringType == typeof(QueryExpressionBuilder))
                              {
                                  #region WriteProjection:Last
                                  if (xxMethodCallExpression.Method.Name == xReferencesOfLong.LastReference.Method.Name)
                                  {
                                      // x:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByScalarFirstOrDefault\Program.cs
                                      // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByScalar\Program.cs

                                      // new[] { new Tuple<string, MemberInfo, int>(zParameterExpression.Name, null, 0) }

                                      //WriteProjection(zsource, xxMethodCallExpression.Arguments[0], Target);

                                      var arg0 = xxMethodCallExpression.Arguments[0] as ParameterExpression;

                                      WriteProjectionProxy(zsource,
                                                                xxMethodCallExpression.Arguments[0],
                                                                Target,
                                                                new[] {
                                                                    new Tuple<string, MemberInfo, int>(arg0.Name, null, 0),
                                                                    new Tuple<string, MemberInfo, int>("Last", null, 0),

                                                                }
                                                            );

                                      return;
                                  }
                                  #endregion

                                  #region WriteProjection:FirstOrDefault
                                  if (xxMethodCallExpression.Method.Name == xReferencesOfLong.FirstOrDefaultReference.Method.Name)
                                  {
                                      // what about inline testing?
                                      WriteScalarOperand(zsource, xxMethodCallExpression, GetTargetName, xReferencesOfLong.FirstOrDefaultReference.Method, Target);
                                      return;
                                  }
                                  #endregion


                                  #region WriteProjection:Count
                                  if (xxMethodCallExpression.Method.Name == xReferencesOfLong.CountReference.Method.Name)
                                  {
                                      // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByThenSelectKeyCount\Program.cs
                                      // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarCount\Program.cs
                                      WriteScalarOperand(zsource, xxMethodCallExpression, GetTargetName, xReferencesOfLong.CountReference.Method, Target);
                                      return;
                                  }
                                  #endregion

                                  #region WriteProjection:Sum
                                  if (xxMethodCallExpression.Method.Name == xReferencesOfLong.SumOfLongReference.Method.Name)
                                  {
                                      // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarSum\Program.cs
                                      WriteScalarOperand(zsource, xxMethodCallExpression, GetTargetName, xReferencesOfLong.SumOfLongReference.Method, Target);
                                      return;
                                  }
                                  #endregion

                                  #region WriteProjection:Average
                                  if (xxMethodCallExpression.Method.Name == xReferencesOfLong.AverageOfLongReference.Method.Name)
                                  {
                                      // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarAverage\Program.cs
                                      WriteScalarOperand(zsource, xxMethodCallExpression, GetTargetName, xReferencesOfLong.AverageOfLongReference.Method, Target);
                                      return;
                                  }
                                  #endregion

                                  #region WriteProjection:Max
                                  if (xxMethodCallExpression.Method.Name == xReferencesOfLong.MaxOfLongReference.Method.Name)
                                  {
                                      // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarMax\Program.cs
                                      WriteScalarOperand(zsource, xxMethodCallExpression, GetTargetName, xReferencesOfLong.MaxOfLongReference.Method, Target);
                                      return;
                                  }
                                  #endregion

                                  #region WriteProjection:Min
                                  if (xxMethodCallExpression.Method.Name == xReferencesOfLong.MinOfLongReference.Method.Name)
                                  {
                                      // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarMin\Program.cs
                                      WriteScalarOperand(zsource, xxMethodCallExpression, GetTargetName, xReferencesOfLong.MinOfLongReference.Method, Target);
                                      return;
                                  }
                                  #endregion


                                  var IsSelect = xxMethodCallExpression.Method.Name == xReferencesOfLong.SelectReference.Method.Name;
                                  var IsOrderBy = xxMethodCallExpression.Method.Name == xReferencesOfLong.OrderByReference.Method.Name;
                                  var IsThenBy = xxMethodCallExpression.Method.Name == xReferencesOfLong.ThenByReference.Method.Name;
                                  var IsOrderByDescending = xxMethodCallExpression.Method.Name == xReferencesOfLong.OrderByDescendingReference.Method.Name;
                                  var IsThenByDescending = xxMethodCallExpression.Method.Name == xReferencesOfLong.ThenByDescendingReference.Method.Name;

                                  // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxScalarOrderByThenBy\Program.cs
                                  // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxScalarWhereOrderByDescending\Program.cs
                                  #region Let
                                  if (IsSelect || IsOrderBy || IsOrderByDescending || IsThenBy || IsThenByDescending)
                                  {
                                      // its one of our own?

                                      WriteCommentLine(1, "let " + GetTargetName() + " <- " + xxMethodCallExpression.Method.Name);
                                      //WriteScalarOperand(zsource, xxMethodCallExpression, GetTargetName, SQLWriter<TElement>.SelectReference.Method);
                                      return;
                                  }
                                  #endregion
                              }
                              #endregion

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
                                      // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxScalarUpper\Program.cs
                                      // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxScalarLetUpper\Program.cs

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

                                          WriteProjection(zsource, xxMethodCallExpression.Object, new Tuple<MemberInfo, int>[] {
                                              // Tuple.Create(item.m, index)
                                          });

                                          //WriteScalarExpression(true, xxMethodCallExpression.Object);


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

                                          WriteProjection(zsource, xxMethodCallExpression.Object, new Tuple<MemberInfo, int>[] {
                                              // Tuple.Create(item.m, index)
                                          });


                                          //WriteScalarExpression(true, xxMethodCallExpression.Object);
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
                              // x:\jsc.svn\examples\javascript\linq\test\auto\testselect\syntaxletgroupby\program.cs
                              // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxLet2GroupBy\Program.cs
                              // x:\jsc.svn\examples\javascript\linq\test\auto\testselect\syntaxletgroupby\program.cs

                              var zGroupBy = source as xGroupBy;
                              if (zGroupBy != null)
                              {
                                  ////var zGroupBy_Select = zGroupBy.source as xSelect;

                                  ////if (zGroupBy_Select.selector.Parameters[0].Name == zMemberExpression.Member.Name)
                                  ////{
                                  ////    // i think we need to do a proxy?
                                  ////    //WriteProjectionProxy(zsource, zExpression, Target);
                                  ////    //WriteProjectionProxy(zsource, zGroupBy_Select.selector, Target);
                                  ////    WriteProjection(zsource, zGroupBy_Select.selector.Body, Target);


                                  ////    return;
                                  ////}
                              }


                              var isQuoteMode = false;

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

                                          // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLROrOperator\TestJVMCLROrOperator\Program.cs

                                          //Y:\staging\web\java\JVMCLRSyntaxOrderByThenGroupBy__i__d\Internal\Query\Experimental\QueryExpressionBuilder_SQLWriter_1___c__DisplayClass14.java:54: error: bad operand types for binary operator '||'
                                          //            if (!(!(this.CS___8__locals64.zSelect != null) || ((this.CS___8__locals64.zSelect.source) || ((this.CS___8__locals64.CS___8__locals19.CS___8__locals18.upperParameter == null)))))
                                          //                                                            ^

                                          var flag1 = false;

                                          if (zSelect != null)
                                              if (zSelect.source == null)
                                                  if (upperParameter != null)
                                                      flag1 = true;


                                          if (flag1)
                                          {
                                              // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelect\Program.cs

                                              WriteLine(1, "`");
                                              WriteLineWithColor(0, "" + upperParameter.Name, ConsoleColor.Magenta);
                                              WriteLine(1, "`");
                                          }
                                          else
                                          {

                                              WriteLine(1, "`");
                                              WriteLineWithColor(0, mp.Name, ConsoleColor.DarkCyan);
                                              WriteLine(1, "`");
                                          }

                                      }

                                      // m.Expression = {<>h__TransparentIdentifier0.g.Last()}
                                      // tested by?
                                      var mMethodCallExpression = m.Expression as MethodCallExpression;
                                      if (mMethodCallExpression != null)
                                      {
                                          // for group by we shall support Last


                                          // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByConstant\Program.cs

                                          var a0p = mMethodCallExpression.Arguments[0] as ParameterExpression;
                                          if (a0p != null)
                                          {
                                              WriteLine(1, "`");
                                              WriteLineWithColor(0, a0p.Name, ConsoleColor.DarkCyan);
                                              WriteLine(1, "`");
                                          }


                                          var a0m = mMethodCallExpression.Arguments[0] as MemberExpression;

                                          if (a0m != null)
                                          {
                                              y(a0m);
                                          }

                                          WriteLine(1, ".");

                                          if (!isQuoteMode)
                                          {
                                              // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByConstant\Program.cs

                                              WriteLine(1, "`");
                                              isQuoteMode = true;
                                          }

                                          WriteLineWithColor(1, "Last", ConsoleColor.White);
                                      }

                                      WriteLine(1, ".");
                                      //if (zMemberExpression == m)
                                      //{

                                      if (!isQuoteMode)
                                      {
                                          WriteLine(1, "`");
                                          isQuoteMode = true;
                                      }

                                      WriteLineWithColor(1, m.Member.Name, ConsoleColor.Cyan);
                                      //}
                                      //else

                                      //    WriteLineWithColor(1, m.Member.Name, ConsoleColor.DarkCyan);
                                  };
                              #endregion


                              // X:\jsc.svn\examples\javascript\LINQ\GGearAlpha\GGearAlpha\Application.cs
                              // X:\jsc.svn\examples\javascript\LINQ\test\TestLINQ\UnitTestProject1\ApplicationWebService\ApplicationWebService select x.cs
                              //using (WithoutLinefeeds())

                              // jsc when can we start returning from within using blocks?
                              DoWithoutLinefeeds(delegate
                              {
                                  // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\JVMCLRSyntaxOrderByThenGroupBy\Program.cs

                                  // xScalar wont send the target
                                  if (Target.Length > 0)
                                  {
                                      WriteCommentLine(1, "let");

                                      // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRTupleArrayLast\TestJVMCLRTupleArrayLast\Program.cs
                                      // why is this causing trouble?

                                      var TargetLast = Target.Last();

                                      // wtf? TargetLast :2296 { TargetLast = (System.String Tag, 0) }
                                      //Console.WriteLine("TargetLast :2296 " + new { TargetLast });

                                      //    if ((__Enumerable.<__Tuple_2<__MemberInfo, Integer>>Last(__SZArrayEnumerator_1.<__Tuple_2<__MemberInfo, Integer>>Of(this.CS___8__locals64.Target)).get_Item2() > 0))
                                      if (TargetLast.Item2 > 0)
                                          WriteLine(1, ", ");
                                      else
                                          WriteLine(1, " ");
                                  }



                                  // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWebOrderByThenGroupBy\Application.cs
                                  // tested by?
                                  #region DateTime::
                                  if (zMemberExpression.Member.DeclaringType == typeof(DateTime))
                                  {
                                      WriteLineWithColor(1, "date(", ConsoleColor.White);

                                      // date of what?
                                      y(zMemberExpression.Expression as MemberExpression);

                                      WriteLineWithColor(1, ")", ConsoleColor.White);
                                      return;
                                  }
                                  #endregion


                                  y(zMemberExpression);

                                  // true always?
                                  if (isQuoteMode)
                                  {
                                      WriteLine(1, "`");
                                      isQuoteMode = false;
                                  }


                                  // xScalar wont send the target
                                  if (Target.Length > 0)
                                  {
                                      WriteLine(1, " as ");

                                      if (upperParameter != null)
                                      {
                                          WriteCommentLine(0, upperParameter.Name);
                                          WriteLine(1, " ");
                                      }




                                      // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
                                      WriteLine(1, "`");
                                      WriteLineWithColor(0, GetTargetName(), ConsoleColor.Cyan);
                                      WriteLine(1, "`");
                                  }
                              });
                              return;
                          }
                          #endregion

                          #region WriteProjection:zUnaryExpression
                          var zUnaryExpression = zExpression as UnaryExpression;
                          if (zUnaryExpression != null)
                          {
                              // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectXElement\Program.cs
                              // Method = {System.Xml.Linq.XName op_Implicit(System.String)}

                              // descending xml: { IsCompleted = false, Result =  }
                              if (zUnaryExpression.NodeType == ExpressionType.Convert)
                              {
                                  // could we just discard the type?
                                  WriteProjection(zsource, zUnaryExpression.Operand, Target);
                                  // tested by
                                  // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
                                  return;
                              }


                              // when does this happen?
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
                                  WriteCommentLine(1, "let");

                                  if (Target.Last().Item2 > 0)
                                      WriteLine(1, ", ");


                                  WriteLine(1, " ");

                                  WriteScalarExpression(true, xConstantExpression);
                                  WriteLine(1, " as ");

                                  if (upperParameter != null)
                                  {
                                      WriteCommentLine(0, upperParameter.Name);
                                      WriteLine(1, " ");
                                  }

                                  WriteLineWithColor(0, GetTargetName(), ConsoleColor.Cyan);
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
                                  WriteScalarExpression(true, xxBinaryExpression);
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
                              WriteCommentLine(1, "WriteProjection:MemberInitExpression");

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

                // used by?
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

                          #region WriteScalarExpressionArray:MemberExpression
                          var zMemberExpression = zExpression as MemberExpression;
                          if (zMemberExpression != null)
                          {
                              // ?
                              WriteScalarExpression(true, zExpression);
                              return;
                          }
                          #endregion

                          #region WriteScalarExpressionArray:ConstantExpression
                          var zConstantExpression = zExpression as ConstantExpression;
                          if (zConstantExpression != null)
                          {
                              WriteScalarExpression(true, zConstantExpression);
                              return;
                          }
                          #endregion


                          #region WriteScalarExpressionArray:NewArrayExpression
                          var zNewArrayExpression = zExpression as NewArrayExpression;
                          if (zNewArrayExpression != null)
                          {
                              // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByArray\Program.cs

                              zNewArrayExpression.Expressions.WithEachIndex(
                                   (SourceArgument, SourceArgumentIndex) =>
                                       WriteScalarExpression(
                                       //zsource,

                                        true,
                                           SourceArgument
                                       //,
                                       //Target.Concat(new[] { Tuple.Create(default(MemberInfo), SourceArgumentIndex) }).ToArray()
                                       )
                             );
                              return;
                          }
                          #endregion

                          #region WriteScalarExpressionArray:xxNewExpression
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

                                                true,
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
                                      true,
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


                #region xScalar
                var xScalar = source as xScalar;
                if (xScalar != null)
                {
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectAverage\Program.cs

                    // http://www.w3schools.com/sql/sql_func_count.asp
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestDeleteAll\Program.cs

                    //WriteLine(0, "select count(*) from (");


                    #region  FirstOrDefaultReference
                    if (xScalar.Operand == xReferencesOfLong.FirstOrDefaultReference.Method)
                    {
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectScalarFirstOrDefault\Program.cs

                        var xsource = new SQLWriter<TElement>(
                             xScalar.source,
                              upper.Concat(new[] { source }),
                              context,
                            //upperParameter: (source as xSelect).selector.Parameters[0],
                            upperParameter: null,
                              Command: Command
                          );

                        return;
                    }
                    #endregion


                    // count wont work?
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestJoinOnNewExpression\Program.cs

                    var xxSelect = xScalar.source as xSelect;

                    #region xxMemberExpression
                    var xxMemberExpression = xxSelect.selector.Body as MemberExpression;
                    if (xxMemberExpression != null)
                    {





                        using (WithoutLinefeeds())
                        {
                            WriteLine(0, "select ");
                            //WriteCommentLine(1, xCount.Operand.Name);

                            if (xScalar.Operand == xReferencesOfLong.SumOfLongReference.Method)
                                WriteLine(0, "sum");
                            else if (xScalar.Operand == xReferencesOfLong.MinOfLongReference.Method)
                                WriteLine(0, "min");
                            else if (xScalar.Operand == xReferencesOfLong.MaxOfLongReference.Method)
                                WriteLine(0, "max");
                            else if (xScalar.Operand == xReferencesOfLong.AverageOfLongReference.Method)
                                WriteLine(0, "avg");
                            else if (xScalar.Operand == xReferencesOfLong.CountReference.Method)
                                WriteLine(0, "count"); // or select the row instead of field...
                            else
                                // what else is there?
                                WriteLine(0, "?");

                            WriteLine(0, "(");

                            // x:\jsc.svn\examples\javascript\linq\test\auto\testselect\syntaxselectlet3min\program.cs
                            // this does not seem to be correct.


                            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140720/linq


                            WriteProjection(xxSelect, xxMemberExpression, new Tuple<MemberInfo, int>[] {
                                // Tuple.Create(item.m, index)
                            });


                            //WriteScalarExpression(true, xxMemberExpression);
                            WriteLine(0, ") from (");
                        }

                        var xsource = new SQLWriter<TElement>(
                           xxSelect.source,
                            //xxSelect,
                            upper.Concat(new[] { source }),
                            context,
                            //upperParameter: (source as xSelect).selector.Parameters[0],
                            upperParameter: null,
                            Command: Command
                        );


                        using (WithoutLinefeeds())
                        {
                            using (WithoutLinefeeds())
                            {
                                WriteLine(0, ") as `");
                                WriteLineWithColor(0, "" + xxSelect.selector.Parameters[0].Name, ConsoleColor.Magenta);
                                WriteLine(0, "`");
                            }
                        }

                        return;
                    }
                    #endregion


                    {
                        WriteLine(0, "select count(*) from (");

                        var xsource = new SQLWriter<TElement>(
                           xScalar.source,
                            upper.Concat(new[] { source }),
                            context,
                            //upperParameter: (source as xSelect).selector.Parameters[0],
                            upperParameter: null,
                            Command: Command
                        );

                        // render the source and with parent
                        using (WithoutLinefeeds())
                        {
                            WriteLine(0, ") as `");
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
                            // Additional information: Every derived table must have its own alias
                            WriteLineWithColor(0, "TCountable", ConsoleColor.Magenta);
                            WriteLine(0, "`");
                        }
                    }

                    return;
                }
                #endregion



                #region xOrderBy
                var xOrderBy = source as xOrderBy;
                if (xOrderBy != null)
                {
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestOrderByDescending\Program.cs
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestOrderBy\Program.cs
                    var sql = new SQLWriter<TElement>(
                        xOrderBy.source,
                        upper.Concat(new[] { source }),
                        context,
                        upperParameter: xOrderBy.keySelector.First().keySelector.Parameters[0],
                        Command: Command);

                    using (WithoutLinefeeds())
                    {
                        xOrderBy.keySelector.WithEachIndex(
                            (oExpression, oExpressionIndex) =>
                            {

                                if (oExpressionIndex == 0)
                                    WriteLine(0, "order by ");
                                else
                                    WriteLine(0, ", ");



                                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140720/linq
                                WriteProjection(xOrderBy.source, oExpression.keySelector.Body, new Tuple<MemberInfo, int>[] {
                                    // Tuple.Create(item.m, index)
                                });

                                //WriteScalarExpression(DiscardAlias: true, asExpression: oExpression.keySelector.Body);

                                //WriteOrderByKeySelector(
                                //    xOrderBy.source,
                                //    oExpression.keySelector.Body,
                                //    null,
                                //    new Tuple<MemberInfo, int>[0]
                                //);

                                if (!oExpression.ascending)
                                    WriteLine(0, " desc");
                            }
                        );
                    }

                    return;
                }
                #endregion


                #region xGroupBy
                var xGroupBy = source as xGroupBy;
                if (xGroupBy != null)
                {

                    // proxy x?
                    // elementSelector = {<>h__TransparentIdentifier3 => <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.x}
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupBy\Program.cs

                    WriteCommentLine(0, "xGroupBy");
                    WriteLine(0, "select");

                    {
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByConstant\Program.cs
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupBySelectKeyAndLast\Program.cs
                        //WriteLine(1, "proxy { Key, Last() ... }");


                        // IQueryStrategyGrouping



                        // xGroupBy.elementSelector.Body = {e}
                        //  WriteProjectionProxy(zsource, zParameterExpression, Target);


                        // xGroupBy.elementSelector.Body = {<>h__TransparentIdentifier0.x}
                        // where should we unpack it?

                        // WriteProjectionProxy(zsource, zParameterExpression, Target);

                        // xGroupBy.elementSelector.Body = {e}


                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxGroupByConstantSelectCount\Program.cs
                        var xParameterExpression = xGroupBy.elementSelector.Body as ParameterExpression;
                        if (xParameterExpression != null)
                        {
                            // x:\jsc.svn\examples\javascript\linq\test\auto\testselect\testgroupbyconstant\program.cs
                            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140723


                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxOrderByThenGroupBy\Program.cs

                            var xxOrderBy = xGroupBy.source as xOrderBy;
                            if (xxOrderBy != null)
                            {
                                var xxSelect = xxOrderBy.source as xSelect;


                                var xxMemberInitExpression = xxSelect.selector.Body as MemberInitExpression;
                                if (xxMemberInitExpression != null)
                                {
                                    WriteProjectionProxy(
                                        xGroupBy.source,
                                        xxMemberInitExpression,
                                         new[] { new Tuple<MemberInfo, int>(xReferencesOfLong.LastReference.Method, 1) },
                                         new[] { new Tuple<string, MemberInfo, int>(xGroupBy.keySelector.Parameters[0].Name, null, 1) }
                                        );
                                }
                                else Debugger.Break();
                            }
                            else
                            {

                                var xxSelect = xGroupBy.source as xSelect;

                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByConstant\Program.cs
                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectWhereOrderBy\Program.cs

                                var xxMemberInitExpression = xxSelect.selector.Body as MemberInitExpression;
                                if (xxMemberInitExpression != null)
                                {
                                    WriteProjectionProxy(
                                        xGroupBy.source,
                                        xxMemberInitExpression,
                                         new[] { new Tuple<MemberInfo, int>(xReferencesOfLong.LastReference.Method, 1) },
                                         new[] { new Tuple<string, MemberInfo, int>(xGroupBy.keySelector.Parameters[0].Name, null, 1) }
                                        );
                                }
                                else Debugger.Break();
                            }
                        }
                        else
                        {
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxLetGroupBy\Program.cs
                            // xGroupBy.elementSelector = {<>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.x}
                            var xMemberExpression = xGroupBy.elementSelector.Body as MemberExpression;
                            if (xMemberExpression != null)
                            {
                                // xMemberExpression = {<>h__TransparentIdentifier0.x}

                                var xxSelect = xGroupBy.source as xSelect;

                                var xxNewExpression = xxSelect.selector.Body as NewExpression;

                                // this wont work for js?
                                var ii = xxNewExpression.Members.Select(xx => xx.Name).ToList().IndexOf(xMemberExpression.Member.Name);
                                var aa = xxNewExpression.Arguments[ii];

                                WriteProjectionProxy(
                                   xGroupBy.source,
                                   aa,
                                    new[] { new Tuple<MemberInfo, int>(xReferencesOfLong.LastReference.Method, 1) },
                                    new[] {

                                        new Tuple<string, MemberInfo, int>(xGroupBy.keySelector.Parameters[0].Name, null, 1),
                                        new Tuple<string, MemberInfo, int>(null, xMemberExpression.Member, 1)

                                    }
                                   );
                            }
                            else
                            {
                                // xGroupBy.elementSelector.Body = {new <>f__AnonymousType27`2(c = <>h__TransparentIdentifier18f.c, cData = <>h__TransparentIdentifier18f.cData)}
                                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141208
                                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxJoinThenGroupBy\Program.cs
                                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141209

                                //Debugger.Break();

                                var xxNewExpression = xGroupBy.elementSelector.Body as NewExpression;
                                // xGroupBy.elementSelector.Body = {new <>f__AnonymousType0`2(x = <>h__TransparentIdentifier0.x, y = <>h__TransparentIdentifier0.y)}
                                // um, we need to write both of them.
                                // how can we do that?

                                //var ii = xxNewExpression.Members.Select(xx => xx.Name).ToList().IndexOf(xMemberExpression.Member.Name);
                                //var aa = xxNewExpression.Arguments[ii];

                                WriteProjectionProxy(
                                   xGroupBy.source,
                                    //aa,
                                   xxNewExpression,
                                    new[] { new Tuple<MemberInfo, int>(xReferencesOfLong.LastReference.Method, 1) },
                                    new[] {

                                        new Tuple<string, MemberInfo, int>(xGroupBy.keySelector.Parameters[0].Name, null, 1)
                                        //,
                                        //new Tuple<string, MemberInfo, int>(null, xMemberExpression.Member, 1)

                                    }
                                   );

                                //WriteLine(1, "proxy { ? }");
                            }
                        }


                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupByScalar\Program.cs
                        // sending over the key?
                        WriteProjection(
                            source,
                            xGroupBy.keySelector.Body,
                               new[] { new Tuple<MemberInfo, int>(xReferencesOfLong.KeyReference, 1) }
                        );

                        // do we need to do counting in the group?
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxGroupByConstantSelectCount\Program.cs
                        var uSelect = upper.Last() as xSelect;
                        if (uSelect != null)
                        {
                            // selector = {gg => new <>f__AnonymousType0`2(count1 = gg.Count(), Key = gg.Key)}

                            // how will we know if upper wants to do count?

                            var uNewExpression = uSelect.selector.Body as NewExpression;
                            if (uNewExpression != null)
                            {
                                uNewExpression.Arguments.WithEachIndex(
                                    (uArgument, uIndex) =>
                                    {
                                        // uArgument = {gg.Count()}

                                        // perhaps we should always do a count to make sure the Last is not optimized to be First ?

                                        var uMethodCallExpression = uArgument as MethodCallExpression;
                                        if (uMethodCallExpression != null)
                                        {
                                            // sync with WriteScalarOperand

                                            if (uMethodCallExpression.Method.DeclaringType == typeof(QueryExpressionBuilder))
                                            {
                                                // should be one of our methods.
                                                // which?
                                                if (uMethodCallExpression.Method.Name == xReferencesOfLong.CountReference.Method.Name)
                                                {
                                                    using (WithoutLinefeeds())
                                                    {
                                                        WriteCommentLine(1, "group");
                                                        WriteLine(2, ", count(*) as " + uNewExpression.Members[uIndex].Name);
                                                    }

                                                    return;
                                                }

                                                if (uMethodCallExpression.Method.Name == xReferencesOfLong.AverageOfLongReference.Method.Name)
                                                {
                                                    using (WithoutLinefeeds())
                                                    {
                                                        WriteCommentLine(1, "group");

                                                        // Unhandled Exception: System.Data.SQLite.SQLiteSyntaxException: wrong number of arguments to function avg()

                                                        //var uSelector = uMethodCallExpression.Arguments[1] as LambdaExpression;
                                                        var uUnaryExpression = uMethodCallExpression.Arguments[1] as UnaryExpression;
                                                        var uLambdaExpression = uUnaryExpression.Operand as LambdaExpression;

                                                        WriteLine(2, ", avg(");

                                                        WriteProjection(uSelect, uLambdaExpression.Body, new Tuple<MemberInfo, int>[] {
                                                            // Tuple.Create(item.m, index)
                                                        });

                                                        WriteLine(2, ") as " + uNewExpression.Members[uIndex].Name);
                                                    }

                                                    return;
                                                }

                                                Debugger.Break();
                                            }
                                        }
                                    }
                                );

                            }
                        }
                    }

                    // elementSelector = {<>h__TransparentIdentifier3 => new <>f__AnonymousType4`3(x = <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.x, xFoo = <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.xFoo, xKey = <>h__TransparentIdentifier3.xKey)}

                    // proxy the key
                    // proxy the outer

                    WriteLine(0, "from (");
                    var sql0 = new SQLWriter<TElement>(
                        xGroupBy.source,
                        upper.Concat(new[] { source }),
                        context,
                        upperParameter: xGroupBy.keySelector.Parameters[0],
                        Command: Command);

                    // keySelector = {<>h__TransparentIdentifier3 => new <>f__AnonymousType3`3(xKey = <>h__TransparentIdentifier3.xKey, xFoo = <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.xFoo, g3 = (<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.x.field2 + 2))}

                    using (WithoutLinefeeds())
                    {
                        WriteLine(0, ") as ");
                        WriteLine(0, "`");
                        WriteLineWithColor(0, xGroupBy.keySelector.Parameters[0].Name, ConsoleColor.Magenta);
                        WriteLine(0, "`");
                    }

                    //var xConstantExpression = xGroupBy.keySelector.Body as ConstantExpression;
                    //if (xConstantExpression != null)
                    //{
                    //    // does it even matter whats the value of it?
                    //    using (WithoutLinefeeds())
                    //    {
                    //        WriteLine(0, "group by ");
                    //        WriteLineWithColor(1, "1", ConsoleColor.Yellow);
                    //    }

                    //    return;
                    //}


                    //using (WithoutLinefeeds())
                    {
                        WriteLine(0, "group by ");

                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupBy\Program.cs

                        // we need it unpacked
                        //WriteProjection(source, xGroupBy.keySelector.Body, new Tuple<MemberInfo, int>[0]);

                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxLetGroupBy\Program.cs



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

                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxJoin\Program.cs
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestJoin\Program.cs

                        xArguments.WithEachIndex(
                            (item, index) =>
                            {
                                WriteProjection(
                                    item.source,
                                    item.a,
                                    new[] { Tuple.Create(item.m, index) }
                                );

                            }
                        );
                    }
                    else
                    {
                        Debugger.Break();
                    }

                    // resultSelector = {(xouter, xinner) => new <>f__AnonymousType13`2(xouter = xouter, xinner = xinner)}

                    WriteLine(0, "from (");
                    var sql0 = new SQLWriter<TElement>(
                        xJoin.outer,
                        upper.Concat(new[] { source }),
                        context,
                        upperParameter: xJoin.outerKeySelector.Parameters[0],
                        Command: Command);
                    using (WithoutLinefeeds())
                    {
                        WriteLine(0, ") as ");
                        WriteLine(0, "`");
                        WriteLineWithColor(0, xJoin.outerKeySelector.Parameters[0].Name, ConsoleColor.Magenta);
                        WriteLine(0, "`");
                    }

                    WriteLine(0, "inner join (");

                    var sql1 = new SQLWriter<TElement>(
                        xJoin.inner,
                        upper.Concat(new[] { source }),
                        context,
                        upperParameter: xJoin.innerKeySelector.Parameters[0],
                        Command: Command);

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


                                WriteScalarExpression(true, item.a);
                                WriteLine(0, " = ");
                                WriteScalarExpression(true, item.m);
                            }
                        }

                        return;
                    }

                    using (WithoutLinefeeds())
                    {
                        WriteLine(0, "on ");
                        WriteScalarExpression(true, xJoin.outerKeySelector.Body);
                        WriteLine(0, " = ");
                        WriteScalarExpression(true, xJoin.innerKeySelector.Body);
                    }

                    return;
                }
                #endregion





                var xSelect_source = xSelect.source;


                // what are looking at? select selector will know the name of the source
                // (source as xSelect).selector.Body = {<>h__TransparentIdentifier6 . <>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.z}
                var x = xSelect.selector.Body;

                WriteCommentLine(0, "xSelect");
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
                            WriteCommentLine(1, "WriteSelectProjection:MemberInitExpression");

                            WriteProjection(source, xMemberInitExpression.NewExpression, new Tuple<MemberInfo, int>[0]);

                            // what about XElement?

                            xMemberInitExpression.Bindings.WithEachIndex(
                                (SourceBinding_3266, SourceBindingIndex) =>
                                {
                                    var item = new
                                    {
                                        a = (SourceBinding_3266 as MemberAssignment).Expression,
                                        m = SourceBinding_3266.Member
                                    };

                                    WriteProjection(source, item.a, new[] { Tuple.Create(item.m, SourceBindingIndex) });
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


                            // tested by?

                            // we are selecting a group for upper select arent we.
                            var xArguments = xNewExpression.Arguments.Zip(xNewExpression.Members, (a, m) => new { a, m, source }).ToList();
                            xArguments.WithEachIndex(
                                     (item, index) =>
                                     {
                                         //Console.WriteLine(":3312 " + new { item.m, index });
                                         WriteProjection(item.source, item.a, new[] { Tuple.Create(item.m, index) });
                                     }
                            );

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
                        var sMemberExpression = xSelect.selector.Body as MemberExpression;
                        if (sMemberExpression != null)
                        {
                            WriteCommentLine(1, "MemberExpression");

                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectScalarXElementField\Program.cs
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectKey\Program.cs
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMemberExpression\Program.cs
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxLetOrderBy\Program.cs

                            using (WithoutLinefeeds())
                            {

                                //WriteScalarExpression(false, sMemberExpression);

                                WriteProjection(xSelect, sMemberExpression, new Tuple<MemberInfo, int>[] {
                                // Tuple.Create(item.m, index)
                            });
                            }

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
                                WriteScalarExpression(false, xBinaryExpression);

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
                        WriteLine(0, "from ");
                        WriteLineWithColor(0, "" + xSelect.selector.Parameters[0].Name, ConsoleColor.Magenta);

                        // Additional information: Every derived table must have its own alias


                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxOrderBy\Program.cs
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxLetWhere\Program.cs
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxWhere\Program.cs
                        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectMin\Program.cs

                        // when would it be unwanted?

                        if (upperParameter != null)
                        {
                            WriteLine(0, " as `");
                            WriteLineWithColor(0, "" + upperParameter.Name, ConsoleColor.Magenta);
                            WriteLine(0, "`");
                        }

                    }
                }
                else
                {
                    WriteLine(0, "from (");

                    var xsource = new SQLWriter<TElement>(
                       xSelect_source,
                        upper.Concat(new[] { source }),
                        context,
                        upperParameter: xSelect.selector.Parameters[0],
                        Command: Command
                    );

                    // render the source and with parent



                    using (WithoutLinefeeds())
                    {
                        WriteLine(0, ") as `");
                        WriteLineWithColor(0, "" + xSelect.selector.Parameters[0].Name, ConsoleColor.Magenta);
                        WriteLine(0, "`");
                    }
                }

                //Debugger.Break();
            }
        }













    }

}
