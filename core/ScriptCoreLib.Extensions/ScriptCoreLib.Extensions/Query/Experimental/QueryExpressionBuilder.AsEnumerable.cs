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
            var c = (DbCommand)cc.CreateCommand();

            var w = new SQLWriter<TElement>(source, new IQueryStrategy[0].AsEnumerable(), Command: c);

            return c;
        }

        public static IEnumerable<TElement> AsEnumerable<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            //Console.WriteLine("enter AsEnumerable");
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs

            var c = GetSelectCommand(source, cc);



            //Console.WriteLine("before ExecuteReader");
            // this wont work for chrome?
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
            var xTake = source as xTake;
            if (xTake != null)
            {
                return ReadToElement<TElement>(r, xTake.source, Target);
            }

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

                            var v = r[xMemberName];

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
                                v = global::ScriptCoreLib.Library.StringConversions.ConvertStringToXElement((string)v);

                            if (FieldType == typeof(DateTime))
                                v = global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(v);

                            f.SetValue(xRow, v);
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

                           var v = r[k];

                           // Additional information: Object of type 'System.Int64' cannot be converted to type 'System.DateTime'.


                           var f = xMemberExpression.Member as FieldInfo;

                           if (f != null)
                           {
                               // is it xml?
                               // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs
                               if (f.FieldType == typeof(XElement))
                                   v = global::ScriptCoreLib.Library.StringConversions.ConvertStringToXElement((string)v);


                               if (f.FieldType == typeof(DateTime))
                                   v = global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(v);
                           }

                           return v;
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
            }

            return default(TElement);
        }


    }

}
