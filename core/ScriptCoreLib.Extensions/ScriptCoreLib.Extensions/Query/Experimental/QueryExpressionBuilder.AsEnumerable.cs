using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Data;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System.Data.Common;
using System.Xml.Linq;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        public static DbCommand GetSelectCommand<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectXElement\Program.cs

            var c = default(DbCommand);

            if (cc != null)
                c = (DbCommand)cc.CreateCommand();

            var w = new SQLWriter<TElement>(source, new IQueryStrategy[0].AsEnumerable(), Command: c);

            return c;
        }


        public static TElement[] ToArray<TElement>(this IQueryStrategy<TElement> source)
        {
            return source.AsEnumerable().ToArray();
        }


        public static IEnumerable<TElement> AsEnumerable<TElement>(this IQueryStrategy<TElement> source)
        {
            var value = default(List<TElement>);

            WithConnection(
                cc =>
                {
                    value = AsEnumerable(source, cc).ToList();

                }
            );

            return value;
        }

        public static IEnumerable<TElement> AsEnumerable<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            //Console.WriteLine("enter AsEnumerable");
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs

            var c = GetSelectCommand(source, cc);

            if (cc == null)
                return new List<TElement>();



            //Console.WriteLine("before ExecuteReader");
            // this wont work for chrome?
            // wtf? Additional information: near "as": syntax error

            var r = c.ExecuteReader();
            Console.WriteLine("after ExecuteReader");

            return ReadToElements(r, source);
        }

        public static IEnumerable<TElement> ReadToElements<TElement>(DbDataReader r, IQueryStrategy<TElement> source)
        {
            Console.WriteLine("enter AsEnumerable ");

            while (r.Read())
            {

                yield return ReadToElement<TElement>(r, source, new Tuple<MemberInfo, int>[0]);
            }

            Console.WriteLine("exit AsEnumerable ");
            r.Dispose();
        }

        public static TElement ReadToElement<TElement>(DbDataReader r, IQueryStrategy source, Tuple<MemberInfo, int>[] Target)
        {


            #region xTake
            var xTake = source as xTake;
            if (xTake != null)
            {
                return ReadToElement<TElement>(r, xTake.source, Target);
            }
            #endregion


            var xSelect = source as xSelect;
            if (xSelect != null)
            {
                #region xMemberInitExpression
                var xMemberInitExpression = xSelect.selector.Body as MemberInitExpression;
                if (xMemberInitExpression != null)
                {
                    var xRow = default(TElement);
                    var xRowType = xMemberInitExpression.NewExpression.Type;

                    if (xRowType == null)
                        Debugger.Break();


                    // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
                    xRow = (TElement)Activator.CreateInstance(xRowType);
                    //xRow = xMemberInitExpression.NewExpression.Constructor.Invoke(new object[0]);

                    xMemberInitExpression.Bindings.WithEachIndex(
                        (SourceBinding, i) =>
                        {
                            //var k = xSelect.selector.Parameters[0].Name + SourceBinding.Member.Name;
                            var xMemberName = "" + SourceBinding.Member.Name;

                            var __value = r[xMemberName];

                            // Additional information: Object of type 'System.Int64' cannot be converted to type 'System.DateTime'.
                            var f = SourceBinding.Member as FieldInfo;

                            var xMemberAssignment = SourceBinding as MemberAssignment;

                            // this wont work. we need to dig deeper to the original selector for definition?
                            var xUnaryExpression = xMemberAssignment.Expression as UnaryExpression;

                            // is it a long?
                            var FieldType = default(Type);

                            if (xUnaryExpression != null)
                            {
                                FieldType = xUnaryExpression.Type;
                            }
                            else
                            {
                                FieldType = f.FieldType;
                            }

                            Console.WriteLine(new { xMemberName, FieldType });

                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs
                            if (FieldType == typeof(XElement))
                                __value = ScriptCoreLib.Library.StringConversions.ConvertStringToXElement(
                                    ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault(
                                        (string)__value
                                    )
                                );

                            if (FieldType == typeof(DateTime))
                                __value = global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(__value);

                            f.SetValue(xRow, __value);
                        }
                    );

                    return xRow;
                }
                #endregion



                #region xNewExpression
                var xNewExpression = xSelect.selector.Body as NewExpression;
                if (xNewExpression != null)
                {
                    var args = xNewExpression.Arguments.Select(
                       (SourceArgument, i) =>
                       {
                           var xMemberExpression = SourceArgument as MemberExpression;

                           var k = "" + xMemberExpression.Member.Name;

                           var __value = r[k];

                           // Additional information: Object of type 'System.Int64' cannot be converted to type 'System.DateTime'.


                           var f = xMemberExpression.Member as FieldInfo;

                           if (f != null)
                           {
                               // is it xml?
                               // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs
                               if (f.FieldType == typeof(XElement))
                                   //v = global::ScriptCoreLib.Library.StringConversions.ConvertStringToXElement((string)v);
                                   __value = ScriptCoreLib.Library.StringConversions.ConvertStringToXElement(
                                     ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault(
                                         (string)__value
                                     )
                                 );

                               if (f.FieldType == typeof(DateTime))
                                   __value = global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(__value);
                           }

                           return __value;
                       }
                   ).ToArray();


                    // Constructor on type '<>f__AnonymousType0`4[[Program+xPerformanceResourceTimingData2ApplicationPerformanceKey, TestXMySQL, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null],[System.Int64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Int64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]' not found.

                    // X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs
                    var xRowType = xNewExpression.Type;
                    // jsc could give us the PrimaryConstructor?
                    //var xRow = (TElement)Activator.CreateInstance(xRowType, args);
                    var xRow = (TElement)xNewExpression.Constructor.Invoke(args);



                    return xRow;
                }
                #endregion



                #region xParameterExpression
                var xParameterExpression = xSelect.selector.Body as ParameterExpression;
                if (xParameterExpression != null)
                {
                    // proxy?    
                    return ReadToElement<TElement>(r, xSelect.source,

                        Target.Concat(new[] { Tuple.Create(default(MemberInfo), 0) }).ToArray()
                        );
                }
                #endregion


                // can we select scalar?

                #region rMemberExpression
                var rMemberExpression = xSelect.selector.Body as MemberExpression;
                if (rMemberExpression != null)
                {
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs

                    // what about getting it as an array?
                    // [0x00000000] = "x.`z`"
                    var __value = r[rMemberExpression.Member.Name];

                    // cool. do we also know how we created that value?
                    // +		((xSelect.source as xOrderBy).source as xSelect).selector	{PerformanceResourceTimingData2ApplicationPerformance => new PerformanceResourceTimingData2ApplicationPerformanceRow() {Key = PerformanceResourceTimingData2ApplicationPerformance.Key, connectStart = Convert(PerformanceResourceTimingData2ApplicationPerformance.connectStart), connectEnd = Convert(PerformanceResourceTimingData2ApplicationPerformance.connectEnd), requestStart = Convert(PerformanceResourceTimingData2ApplicationPerformance.requestStart), responseStart = Convert(PerformanceResourceTimingData2ApplicationPerformance.responseStart), responseEnd = Convert(PerformanceResourceTimingData2ApplicationPerformance.responseEnd), domLoading = Convert(PerformanceResourceTimingData2ApplicationPerformance.domLoading), domComplete = Convert(PerformanceResourceTimingData2ApplicationPerformance.domComplete), loadEventStart = Convert(PerformanceResourceTimingData2ApplicationPerformance.loadEventStart), loadEventEnd = Convert(PerformanceResourceTimingData2ApplicationPerformance.loadEventEnd), EventTime = Convert(PerformanceResourceTimingData2ApplicationPerformance.EventTime), z = Convert(PerformanceResourceTimingData2ApplicationPerformance.z), Tag = Convert(PerformanceResourceTimingData2ApplicationPerformance.Tag), Timestamp = Convert(PerformanceResourceTimingData2ApplicationPerformance.Timestamp)}}	System.Linq.Expressions.LambdaExpression {System.Linq.Expressions.Expression<System.Func<TestSelectScalarXElementField.PerformanceResourceTimingData2ApplicationPerformanceRow,TestSelectScalarXElementField.PerformanceResourceTimingData2ApplicationPerformanceRow>>}
                    // do we tolerate a complex query for that yet?


                    var templateSelector =
                        ((xSelect.source as xOrderBy).source as xSelect).selector.Body as MemberInitExpression;

                    // in that template, which is it?
                    var ii = templateSelector.Bindings.Select(x => x.Member.Name).ToList().IndexOf(rMemberExpression.Member.Name);
                    var aa = templateSelector.Bindings[ii] as MemberAssignment;

                    var tUnaryExpression = aa.Expression as UnaryExpression;

                    // it is supposed to be xElement!
                    if (tUnaryExpression.Type == typeof(XElement))
                    {
                        // in that case, we need to decode it?
                        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.IDbConnection.Insert.cs
                        var xml = ScriptCoreLib.Library.StringConversions.ConvertStringToXElement(
                            ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault(
                                (string)__value
                            )
                        );

                        return (TElement)(object)xml;
                    }

                    Debugger.Break();
                }
                #endregion



                #region rNewArrayExpression
                var rNewArrayExpression = xSelect.selector.Body as NewArrayExpression;
                if (rNewArrayExpression != null)
                {
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectScalarArrayOfXElementField\Program.cs

                    Debugger.Break();
                }
                #endregion


                Debugger.Break();
            }

            return default(TElement);
        }


    }

}
