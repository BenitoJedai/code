﻿using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using ScriptCoreLib.Shared.Data.Diagnostics;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Ultra.Library;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace System.Data
{
    // move to a nuget?
    // shall reimplement IQueriable for jsc data layer gen
    //[Obsolete("the first generic extension method for all jsc data layer rows")]
    public static partial class QueryStrategyOfTRowExtensions
    {
        //[Obsolete("AsEnumerable. jsc has to bake in all metadata for callsite, like dynamic and linq expressions.")]
        [Obsolete("experimental")]
        public static IEnumerable<TSource> AsGenericEnumerable<TSource>(this IQueryStrategy<TSource> source)
        {


            // X:\jsc.svn\examples\javascript\LINQ\test\vb\TestXMLSelect\TestXMLSelect\ApplicationWebService.vb
            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectMember\TestSelectMember\ApplicationWebService.cs


            // +		(new System.Linq.Expressions.Expression.LambdaExpressionProxy(c as System.Linq.Expressions.Expression<System.Func<MinMaxAverageExperiment.Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow,<>f__AnonymousType0<MinMaxAverageExperiment.Data.PerformanceResourceTimingData2ApplicationResourcePerformanceKey,string,string,string,string,string,string,int>>>)).Body	{new <>f__AnonymousType0`8(Key = k.Key, path = k.path, Trim = k.path.Trim(), TrimStart = k.path.TrimStart(new [] {}), TrimEnd = k.path.TrimEnd(new [] {}), ToLower = k.path.ToLower(), ToUpper = k.path.ToUpper(), Length = k.path.Length)}	System.Linq.Expressions.Expression {System.Linq.Expressions.NewExpression}


            // if its called without LINQ statements,
            // we wont have any expressions available to look at
            // so IQueryStrategy needs t probide us with meta data instead!




            //Activator.CreateInstance(
            // how do we do this on JVM, JS, AS?
            //var x = asNewExpression.Constructor.Invoke(
            //    parameters:
            //        new object[]
            //        {
            //            //    k.Key,
            //            0,
            //            //    k.path,
            //            "",
            //            //    Trim = k.path.Trim(),
            //            "",
            //            //    TrimStart = k.path.TrimStart(),
            //            "",
            //            //    TrimEnd = k.path.TrimEnd(),
            //            "",
            //            //    ToLower = k.path.ToLower(),
            //            "",
            //            //    ToUpper = k.path.ToUpper(),
            //            "",
            //            //    // www.w3schools.com/sql/sql_func_len.asp
            //            //    k.path.Length
            //            0,
            //        }
            //);


            var asDataTable = source.AsDataTable();

            var z = asDataTable.Rows.AsEnumerable().Select(
                SourceRow =>
                {
                    //Additional information: Object of type 'System.String' cannot be converted to type 'MinMaxAverageExperiment.Data.PerformanceResourceTimingData2ApplicationResourcePerformanceKey'.
                    //Additional information: Object of type 'System.Int64' cannot be converted to type 'System.Int32'.


                    // -		asLambdaExpression.Body	{z}	System.Linq.Expressions.Expression {System.Linq.Expressions.TypedParameterExpression}

                    // for xRow types jsc did prepeare us implicit operator
                    // xRow <- DataRow 
                    // yet even if we asked the type for that operator
                    // that would not help us for anonymous types.
                    // so we need to be able to dynamically build such operators at run time.

                    #region CreateSourceElementType
                    Func<Type, TSource> CreateFromSourceElementType =
                        (SourceElementType) =>
                        {
                            var x = Activator.CreateInstance(SourceElementType);

                            SourceElementType.GetFields().WithEach(
                                (FieldInfo asFieldInfo) =>
                            {
                                var asString = SourceRow[asFieldInfo.Name];


                                if (asFieldInfo.FieldType == typeof(long) || asFieldInfo.FieldType.IsEnum)
                                {
                                    asFieldInfo.SetValue(x, Convert.ToInt64(asString));
                                    return;
                                }

                                if (asFieldInfo.FieldType == typeof(int))
                                {
                                    asFieldInfo.SetValue(x, Convert.ToInt32(asString));
                                    return;
                                }

                                // ref ScriptCoreLib.Ultra
                                if (asFieldInfo.FieldType == typeof(DateTime))
                                {
                                    asFieldInfo.SetValue(x, global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(asString));
                                    return;
                                }

                                if (asFieldInfo.FieldType == typeof(XElement))
                                {
                                    //e.il.Emit(OpCodes.Call, e.context.MethodCache[new Func<string, string>(x => StringConversions.UTF8FromBase64StringOrDefault(x)).ToReferencedMethod()]);
                                    //e.il.Emit(OpCodes.Call, e.context.MethodCache[new Func<string, XElement>(x => StringConversions.ConvertStringToXElement(x)).ToReferencedMethod()]);

                                    // X:\jsc.svn\examples\javascript\LINQ\test\vb\TestXMLSelect\TestXMLSelect\ApplicationWebService.vb
                                    asFieldInfo.SetValue(x, global::ScriptCoreLib.Library.StringConversions.ConvertStringToXElement(
                                        global::ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault((string)asString)
                                        ));
                                    return;
                                }

                                // +		asString	{}	object {System.DBNull}
                                if (asString == DBNull.Value)
                                    asFieldInfo.SetValue(x, default(string));
                                else
                                    asFieldInfo.SetValue(x, (asString));
                            }
                            );


                            return (TSource)x;
                        };
                    #endregion

                    var asISelectQueryStrategy = source as ISelectQueryStrategy;
                    if (asISelectQueryStrategy != null)
                    {
                        var asLambdaExpression = asISelectQueryStrategy.selectorExpression as LambdaExpression;


                        // X:\jsc.svn\examples\javascript\linq\test\TestSelectToUpper\TestSelectToUpper\ApplicationWebService.cs




                        #region yieldMemberExpression
                        Func<MemberExpression, TSource> yieldMemberExpression =
                            asMemberExpression =>
                            {
                                // asMemberExpression = {k.path}
                                // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectMember\TestSelectMember\ApplicationWebService.cs

                                var asFieldInfo = asMemberExpression.Member as FieldInfo;
                                var asString = SourceRow[asMemberExpression.Member.Name];


                                if (asFieldInfo.FieldType == typeof(long) || asFieldInfo.FieldType.IsEnum)
                                    return (TSource)(object)Convert.ToInt64(asString);

                                if (asFieldInfo.FieldType == typeof(int))
                                    return (TSource)(object)Convert.ToInt32(asString);

                                // ref ScriptCoreLib.Ultra
                                if (asFieldInfo.FieldType == typeof(DateTime))
                                    return (TSource)(object)global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(asString);

                                if (asFieldInfo.FieldType == typeof(XElement))
                                {
                                    //e.il.Emit(OpCodes.Call, e.context.MethodCache[new Func<string, string>(x => StringConversions.UTF8FromBase64StringOrDefault(x)).ToReferencedMethod()]);
                                    //e.il.Emit(OpCodes.Call, e.context.MethodCache[new Func<string, XElement>(x => StringConversions.ConvertStringToXElement(x)).ToReferencedMethod()]);

                                    // X:\jsc.svn\examples\javascript\LINQ\test\vb\TestXMLSelect\TestXMLSelect\ApplicationWebService.vb
                                    return (TSource)(object)global::ScriptCoreLib.Library.StringConversions.ConvertStringToXElement(
                                        global::ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault((string)asString)
                                        );
                                }

                                return (TSource)(object)asString;
                            };
                        #endregion


                        #region MethodCallExpression
                        var asMethodCallExpression = asLambdaExpression.Body as MethodCallExpression;
                        if (asMethodCallExpression != null)
                        {
                            if (asMethodCallExpression.Method.Name == "ToUpper")
                            {
                                // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.Select.cs
                                // X:\jsc.svn\examples\javascript\linq\test\TestSelectToUpper\TestSelectToUpper\ApplicationWebService.cs

                                var asMMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                return yieldMemberExpression(asMMemberExpression);
                            }
                        }
                        #endregion

                        #region  asParameterExpression
                        var asParameterExpression = asLambdaExpression.Body as ParameterExpression;
                        if (asParameterExpression != null)
                        {
                            var SourceElementType = asParameterExpression.Type;

                            return CreateFromSourceElementType(SourceElementType);
                        }
                        #endregion


                        #region asMemberExpression
                        {
                            var asMemberExpression = asLambdaExpression.Body as MemberExpression;
                            if (asMemberExpression != null)
                            {
                                return yieldMemberExpression(asMemberExpression);
                            }
                        }
                        #endregion

                        #region asNewExpression

                        Func<NewExpression, Tuple<int, MemberInfo>[], object> yieldNewExpression = null;

                        yieldNewExpression = (asNewExpression, prefixes) =>
                        {



                            var parameters = asNewExpression.Arguments.Select(
                                (SourceArgument, index) =>
                                 {



                                     var SourceType = SourceArgument.Type;
                                     var SourceMember = default(MemberInfo);

                                     if (asNewExpression.Members != null)
                                     {
                                         SourceMember = asNewExpression.Members[index];
                                         SourceType = (SourceMember as PropertyInfo).PropertyType;
                                     }
                                     else
                                     {
                                         //asNewExpression.
                                     }

                                     #region GetPrefixedTargetName
                                     Func<string> GetPrefixedTargetName = delegate
                                     {
                                         var w = "";


                                         foreach (var item in prefixes)
                                         {
                                             if (item.Item2 == null)
                                                 w += item.Item1 + ".";
                                             else
                                                 w += item.Item2.Name + ".";
                                         }
                                         if (SourceMember == null)
                                             w += index;
                                         else
                                             w += SourceMember.Name;

                                         return w;
                                     };
                                     #endregion



                                     #region xasInvocationExpression
                                     var xasInvocationExpression = SourceArgument as InvocationExpression;
                                     if (xasInvocationExpression != null)
                                     {
                                         // x:\jsc.svn\examples\javascript\linq\test\selecttoupperintonewexpression\selecttoupperintonewexpression\applicationwebservice.cs

                                         // Member = {System.Func`2[System.String,System.String] Special}
                                         var xasIMemberExpression = xasInvocationExpression.Expression as MemberExpression;
                                         if (xasIMemberExpression != null)
                                         {
                                             // Value = {SelectToUpperIntoNewExpression.ApplicationWebService.}

                                             // xasIMemberExpression.Expression = {value(SelectToUpperIntoNewExpression.ApplicationWebService+<>c__DisplayClass0).loc1}

                                             var xDelegate = default(Delegate);
                                             var xDelegateObject = default(object);

                                             #region xasIMConstantExpression
                                             var xasIMConstantExpression = xasIMemberExpression.Expression as ConstantExpression;
                                             if (xasIMConstantExpression != null)
                                             {
                                                 // Value = {SelectToUpperIntoNewExpression.ApplicationWebService.}

                                                 var xFieldInfo = xasIMemberExpression.Member as FieldInfo;

                                                 //  Additional information: Unable to cast object of type 'System.Func`2[System.String,System.String]' to type 'System.Reflection.MethodInfo'.

                                                 xDelegate = (Delegate)xFieldInfo.GetValue(
                                                 xasIMConstantExpression.Value
                                                );
                                                 xDelegateObject = xasIMConstantExpression.Value;
                                             }
                                             #endregion

                                             #region xasIMMemberExpression
                                             var xasIMMemberExpression = xasIMemberExpression.Expression as MemberExpression;
                                             if (xasIMMemberExpression != null)
                                             {
                                                 var xasIMMConstantExpression = xasIMMemberExpression.Expression as ConstantExpression;
                                                 var xFieldInfo = xasIMMemberExpression.Member as FieldInfo;


                                                 var loc1 = xFieldInfo.GetValue(
                                                  xasIMMConstantExpression.Value
                                                 );

                                                 var xPropertyInfo = xasIMemberExpression.Member as PropertyInfo;

                                                 // Additional information: Object does not match target type.

                                                 //xasIMConstantExpression = xasIMMConstantExpression;
                                                 xDelegate = (Delegate)xPropertyInfo.GetValue(
                                                     loc1, null
                                                 );
                                                 xDelegateObject = xasIMMConstantExpression.Value;
                                             }
                                             #endregion

                                             #region xasIMConstantExpression
                                             if (xDelegate != null)
                                             {
                                                 // Value = {SelectToUpperIntoNewExpression.ApplicationWebService.}



                                                 #region xparameters
                                                 var xparameters = xasInvocationExpression.Arguments.Select(
                                                        (xSourceArgument, i) =>
                                                        {
                                                            var xElementType = xSourceArgument.Type;

                                                            var xasString = SourceRow[GetPrefixedTargetName() + "." + i];

                                                            if (xElementType == typeof(long) || xElementType.IsEnum)
                                                                return Convert.ToInt64(xasString);

                                                            if (xElementType == typeof(int))
                                                                return Convert.ToInt32(xasString);

                                                            // ref ScriptCoreLib.Ultra
                                                            if (xElementType == typeof(DateTime))
                                                                return global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(xasString);

                                                            if (xElementType == typeof(XElement))
                                                                return (TSource)(object)global::ScriptCoreLib.Library.StringConversions.ConvertStringToXElement(
                                                                     global::ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault((string)xasString)
                                                                   );

                                                            //Additional information: Object of type 'System.String' cannot be converted to type 'System.Xml.Linq.XName'.
                                                            if (xElementType == typeof(XName))
                                                            {
                                                                return XName.Get((string)xasString);
                                                            }

                                                            return xasString;
                                                        }
                                                     ).ToArray();
                                                 #endregion
                                                 //  Additional information: Non -static method requires a target.

                                                 if (!xDelegate.Method.IsStatic)
                                                 {
                                                     var xxx = xDelegate.Method.Invoke(
                                                         xDelegateObject,

                                                         xparameters
                                                     );

                                                     return xxx;
                                                 }

                                                 var xx = xDelegate.Method.Invoke(
                                                     null,

                                                     xparameters
                                                 );

                                                 return xx;
                                             }
                                             #endregion


                                         }


                                         Debugger.Break();
                                     }
                                     #endregion

                                     #region xasNewArrayExpression
                                     var xasNewArrayExpression = SourceArgument as NewArrayExpression;
                                     if (xasNewArrayExpression != null)
                                     {
                                         // Type = {Name = "Int64[]" FullName = "System.Int64[]"}

                                         // how do we build a new array?

                                         // will this work for JVM?

                                         var xElementType = xasNewArrayExpression.Type.GetElementType();
                                         var xx = Array.CreateInstance(
                                             elementType: xElementType,
                                             length: xasNewArrayExpression.Expressions.Count
                                         );

                                         for (int i = 0; i < xx.Length; i++)
                                         {
                                             #region GetValue
                                             Func<object> GetValue = delegate
                                             {
                                                 var xasString = SourceRow[GetPrefixedTargetName() + "." + i];

                                                 if (xElementType == typeof(long) || xElementType.IsEnum)
                                                     return Convert.ToInt64(xasString);

                                                 if (xElementType == typeof(int))
                                                     return Convert.ToInt32(xasString);

                                                 // ref ScriptCoreLib.Ultra
                                                 if (xElementType == typeof(DateTime))
                                                     return global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(xasString);

                                                 if (xElementType == typeof(XElement))
                                                     return (TSource)(object)global::ScriptCoreLib.Library.StringConversions.ConvertStringToXElement(
                                                          global::ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault((string)xasString)
                                                        );

                                                 //Additional information: Object of type 'System.String' cannot be converted to type 'System.Xml.Linq.XName'.
                                                 if (xElementType == typeof(XName))
                                                 {
                                                     return XName.Get((string)xasString);
                                                 }

                                                 return xasString;
                                             };
                                             #endregion

                                             var xxx = GetValue();

                                             // http://stackoverflow.com/questions/9022059/dynamically-create-an-array-and-set-the-elements
                                             xx.SetValue(
                                                 xxx,
                                                 i
                                             );

                                             //Array
                                             //xx[]
                                         }

                                         return xx;
                                     }
                                     #endregion

                                     #region xasNewExpression
                                     var xasNewExpression = SourceArgument as NewExpression;
                                     if (xasNewExpression != null)
                                     {
                                         //return CreateFromSourceElementType(asPropertyInfo.PropertyType);
                                         var xx = yieldNewExpression(xasNewExpression, prefixes.Concat(new[] { Tuple.Create(index, SourceMember) }).ToArray());

                                         return xx;
                                     }
                                     #endregion







                                     // Additional information: Column 'datagroup1' does not belong to table .
                                     // X:\jsc.svn\examples\javascript\LINQ\test\SelectToUpperIntoNewExpression\SelectToUpperIntoNewExpression\ApplicationWebService.cs


                                     var asString = SourceRow[GetPrefixedTargetName()];

                                     if (SourceType == typeof(long) || SourceType.IsEnum)
                                         return Convert.ToInt64(asString);

                                     if (SourceType == typeof(int))
                                         return Convert.ToInt32(asString);

                                     // ref ScriptCoreLib.Ultra
                                     if (SourceType == typeof(DateTime))
                                         return global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(asString);

                                     if (SourceType == typeof(XElement))
                                         return (TSource)(object)global::ScriptCoreLib.Library.StringConversions.ConvertStringToXElement(
                                              global::ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault((string)asString)
                                            );

                                     //Additional information: Object of type 'System.String' cannot be converted to type 'System.Xml.Linq.XName'.
                                     if (SourceType == typeof(XName))
                                     {
                                         return XName.Get((string)asString);
                                     }

                                     return asString;
                                 }
                           ).ToArray();

                            var x = asNewExpression.Constructor.Invoke(
                                parameters
                            );

                            return x;
                        };


                        {
                            var asNewExpression = asLambdaExpression.Body as NewExpression;
                            if (asNewExpression != null)
                            {

                                var x = yieldNewExpression(asNewExpression, new Tuple<int, MemberInfo>[0]);

                                return (TSource)x;
                            }
                        }
                        #endregion

                    }
                    else
                    {
                        // we are not in a LINQ expression. csc did not bake us the type cookies we need.
                        // did jsc provide us with defaults?
                        // for all runtimes?

                        // if not LINQ expression data available the table can atleast tell us the defaults
                        // tested by?
                        // when will jsc engage generic type erasure and type argument baking?
                        var SourceElementType = source.GetElementType();
                        if (SourceElementType != null)
                        {
                            return CreateFromSourceElementType(SourceElementType);
                        }
                    }

                    Debugger.Break();
                    return default(TSource);
                }
            ).ToArray();

            return z.AsEnumerable();
        }
    }
}

