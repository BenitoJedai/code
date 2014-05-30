using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
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


                        #region  asParameterExpression
                        var asParameterExpression = asLambdaExpression.Body as ParameterExpression;
                        if (asParameterExpression != null)
                        {
                            var SourceElementType = asParameterExpression.Type;

                            return CreateFromSourceElementType(SourceElementType);
                        }
                        #endregion


                        #region asMemberExpression
                        var asMemberExpression = asLambdaExpression.Body as MemberExpression;
                        if (asMemberExpression != null)
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
                        }
                        #endregion

                        #region asNewExpression
                        var asNewExpression = asLambdaExpression.Body as NewExpression;
                        if (asNewExpression != null)
                        {
                            var parameters = asNewExpression.Members.Select(
                                 SourceMember =>
                             {
                                 var asString = SourceRow[SourceMember.Name];

                                 var asPropertyInfo = SourceMember as PropertyInfo;
                                 if (asPropertyInfo.PropertyType == typeof(long) || asPropertyInfo.PropertyType.IsEnum)
                                     return Convert.ToInt64(asString);

                                 if (asPropertyInfo.PropertyType == typeof(int))
                                     return Convert.ToInt32(asString);

                                 // ref ScriptCoreLib.Ultra
                                 if (asPropertyInfo.PropertyType == typeof(DateTime))
                                     return global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(asString);

                                 if (asPropertyInfo.PropertyType == typeof(XElement))
                                     return (TSource)(object)global::ScriptCoreLib.Library.StringConversions.ConvertStringToXElement(
                                          global::ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault((string)asString)
                                        );



                                 return asString;
                             }
                            ).ToArray();

                            var x = asNewExpression.Constructor.Invoke(
                                parameters
                            );

                            return (TSource)x;
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

