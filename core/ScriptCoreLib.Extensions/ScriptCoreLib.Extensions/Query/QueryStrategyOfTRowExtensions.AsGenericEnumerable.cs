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
    //[Obsolete("?", true)]
    public static partial class QueryStrategyOfTRowExtensions
    {
        public static TSource[] ToArray<TSource>(this IQueryStrategy<TSource> source)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByMultipleFields\TestGroupByMultipleFields\ApplicationWebService.cs

            return source.AsGenericEnumerable().ToArray();
        }

        //[Obsolete("AsEnumerable. jsc has to bake in all metadata for callsite, like dynamic and linq expressions.")]
        //[Obsolete("experimental")]
        public static IEnumerable<TSource> AsGenericEnumerable<TSource>(this IQueryStrategy<TSource> source)
        {
            // same concept when building, now we are about to inspec backwards..
            var that = new { source };

            // we will replace the code gened version. will need to make sure it works for appengine and android

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


                    var asLambdaExpression = default(LambdaExpression);

                    var asISelectQueryStrategy = source as ISelectQueryStrategy;
                    if (asISelectQueryStrategy != null)
                        asLambdaExpression = asISelectQueryStrategy.selectorExpression as LambdaExpression;
                    else
                    {
                        var xISelectManyQueryStrategy = source as ISelectManyQueryStrategy;
                        if (xISelectManyQueryStrategy != null)
                            asLambdaExpression = xISelectManyQueryStrategy.resultSelector as LambdaExpression;
                        else
                        {
                            var asIJoinQueryStrategy = source as IJoinQueryStrategy;
                            if (asIJoinQueryStrategy != null)
                                asLambdaExpression = asIJoinQueryStrategy.selectorExpression as LambdaExpression;

                        }
                    }

                    if (asLambdaExpression == null)
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


                    {


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
                            if (asMethodCallExpression.Method.ReturnType == typeof(bool))
                            {
                                var __value = SourceRow[0];
                                var i4 = Convert.ToInt32(__value);

                                var b = (i4 != 0);
                                return (TSource)(object)(b);
                            }

                            if (asMethodCallExpression.Method.DeclaringType == typeof(string))
                            {
                                var refIsNullOrEmpty = new Func<string, bool>(string.IsNullOrEmpty).Method;

                                if (asMethodCallExpression.Method.Name == refIsNullOrEmpty.Name)
                                {

                                }

                                if (asMethodCallExpression.Method.Name == "ToUpper")
                                {
                                    // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.Select.cs
                                    // X:\jsc.svn\examples\javascript\linq\test\TestSelectToUpper\TestSelectToUpper\ApplicationWebService.cs

                                    var asMMemberExpression = asMethodCallExpression.Object as MemberExpression;
                                    return yieldMemberExpression(asMMemberExpression);
                                }
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


                        Func<IQueryStrategy, NewExpression, Tuple<int, MemberInfo>[], object> yieldNewExpression = null;

                        #region GetArgumentValue
                        Func<IQueryStrategy, MemberInfo, Expression, int, Tuple<int, MemberInfo>[], object> GetArgumentValue = null;

                        GetArgumentValue =
                            (IQueryStrategy that_source, MemberInfo SourceMember, Expression SourceArgument, int index, Tuple<int, MemberInfo>[] prefixes) =>
                            {
                                var asMemberExpression = SourceArgument as MemberExpression;

                                // SourceArgument = {<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.g}
                                // hey its our g.
                                // we need to find the new g { x x}
                                // x:\jsc.svn\examples\javascript\linq\test\testselectdatesthencountsimilars\testselectdatesthencountsimilars\applicationwebservice.cs

                                #region flatten to find g
                                var yy = that_source as ISelectQueryStrategy;

                                // off by 1? hack it
                                if (yy != null)
                                    yy = yy.source as ISelectQueryStrategy;

                                var yym = asMemberExpression;

                                while (yym != null)
                                {
                                    if (yym.Expression is MemberExpression)
                                    {
                                        yym = yym.Expression as MemberExpression;
                                        // go even deeper
                                        if (yy != null)
                                            yy = yy.source as ISelectQueryStrategy;
                                    }
                                    else
                                    {
                                        // we seem to be at the correct level.
                                        // the generated datasources might want to implcitly be ISelectQueryStrategy ?
                                        if (yy != null)
                                        {
                                            var yyLambdaExpression = yy.selectorExpression as LambdaExpression;

                                            // yyNewExpression = {new <>f__AnonymousType2`2(<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0, xrequestStart = <>h__TransparentIdentifier0.x.requestStart)}
                                            // yyNewExpression = {new <>f__AnonymousType0`2(x = x, g = new <>f__AnonymousType1`3(requestStart = x.requestStart, Tag = x.Tag, EventTime = x.EventTime))}

                                            var yyNewExpression = yyLambdaExpression.Body as NewExpression;
                                            var yyi = yyNewExpression.Members.IndexOf(asMemberExpression.Member);
                                            var yya = yyNewExpression.Arguments[yyi];

                                            // we have just resolved the grouping
                                            return GetArgumentValue(

                                                // ?

                                                // only allow deflattening once while we figure out how to implement more robustly
                                                null,

                                                SourceMember,
                                                yya,
                                                // keep the other args the same
                                                index,
                                                //prefixes.Concat(new[] { Tuple.Create(index, asMemberExpression.Member) }).ToArray()
                                                prefixes
                                            );

                                            //WriteExpression(
                                            //     // ?
                                            //     that_source,

                                            //    index, yya,
                                            //    asMemberAssignment_Member,
                                            //    prefixes,
                                            //    null,
                                            //    new[] { Tuple.Create(index, asMemberAssignment_Member) }
                                            //   );
                                            //return;
                                        }

                                        // ?
                                        break;
                                    }
                                }
                                #endregion


                                var SourceType = SourceArgument.Type;


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


                                #region GetValue
                                Func<object> GetValue = delegate
                                {
                                    //                               Additional information: Column 'datagroup3.1.0' does not belong to table .
                                    //                        datagroup3 = new XElement("tag", new XElement("u", ss.Tag), "text element", new XAttribute("style", "color:red;")),
                                    // PrefixedTargetName = "datagroup3.1.0"
                                    //var PrefixedTargetName1 = GetPrefixedTargetName() + "." + i;
                                    var PrefixedTargetName1 = GetPrefixedTargetName();
                                    var xasString = SourceRow[PrefixedTargetName1];

                                    if (SourceType == typeof(long) || SourceType.IsEnum)
                                    {
                                        // well what if sql returns double avg. we need int64.
                                        // we could ask sql to cast it tho..

                                        var i8 = default(long);
                                        if (!long.TryParse((string)xasString, out i8))
                                        {
                                            var __double = Convert.ToDouble(xasString);

                                            // which way are we rounding?
                                            i8 = (long)__double;
                                        }

                                        //return Convert.ToInt64(xasString);
                                        return i8;
                                    }

                                    if (SourceType == typeof(int))
                                        return Convert.ToInt32(xasString);

                                    if (SourceType == typeof(double))
                                        return Convert.ToDouble(xasString);

                                    // ref ScriptCoreLib.Ultra
                                    if (SourceType == typeof(DateTime))
                                        return global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(xasString);

                                    if (SourceType == typeof(XElement))
                                        return (TSource)(object)global::ScriptCoreLib.Library.StringConversions.ConvertStringToXElement(
                                             global::ScriptCoreLib.Library.StringConversions.UTF8FromBase64StringOrDefault((string)xasString)
                                           );

                                    //Additional information: Object of type 'System.String' cannot be converted to type 'System.Xml.Linq.XName'.
                                    if (SourceType == typeof(XName))
                                    {
                                        return XName.Get((string)xasString);
                                    }

                                    return xasString;
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

                                        #region xDelegate
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

                                #region GetArgumentValue: xasNewArrayExpression
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
                                        var SourceExpression = xasNewArrayExpression.Expressions[i];

                                        var xxx = default(object);


                                        // do we have special elements?
                                        var xxasNewExpression = SourceExpression as NewExpression;
                                        if (xxasNewExpression != null)
                                        {
                                            //  /* QueryStrategyOfTRowExtensions.Select.cs:869 */       (  (x.`EventTime` / (1000 * 60 * 60 * 24) * (1000 * 60 * 60 * 24)) + (1000 * 60 * 60 * 24) * @arg0) as `xx.0.z`,

                                            xxx = yieldNewExpression(that_source, xxasNewExpression, prefixes.Concat(new[] { Tuple.Create(index, SourceMember), Tuple.Create(i, default(MemberInfo)) }).ToArray());

                                        }
                                        else
                                        {

                                            #region GetValue
                                            Func<object> xGetValue = delegate
                                            {
                                                //                               Additional information: Column 'datagroup3.1.0' does not belong to table .
                                                //                        datagroup3 = new XElement("tag", new XElement("u", ss.Tag), "text element", new XAttribute("style", "color:red;")),
                                                // PrefixedTargetName = "datagroup3.1.0"
                                                var PrefixedTargetName1 = GetPrefixedTargetName() + "." + i;
                                                var xasString = SourceRow[PrefixedTargetName1];

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

                                            xxx = xGetValue();


                                        }

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
                                    var xx = yieldNewExpression(that_source, xasNewExpression, prefixes.Concat(new[] { Tuple.Create(index, SourceMember) }).ToArray());

                                    return xx;
                                }
                                #endregion

                                #region xasMemberInitExpression
                                var xasMemberInitExpression = SourceArgument as MemberInitExpression;
                                if (xasMemberInitExpression != null)
                                {
                                    var xMNewExpression = xasMemberInitExpression.NewExpression;
                                    var xx = yieldNewExpression(that_source, xMNewExpression, prefixes.Concat(new[] { Tuple.Create(index, SourceMember) }).ToArray());

                                    // indexer init?

                                    return xx;
                                }
                                #endregion

                                #region xasMemberExpression
                                var xasMemberExpression = SourceArgument as MemberExpression;
                                if (xasMemberExpression != null)
                                {
                                    // X:\jsc.svn\examples\javascript\LINQ\test\TestJoinGroupSelectCastLong\TestJoinGroupSelectCastLong\ApplicationWebService.cs


                                    #region xISelectManyQueryStrategy
                                    var xISelectManyQueryStrategy = source as ISelectManyQueryStrategy;
                                    if (xISelectManyQueryStrategy != null)
                                    {
                                        #region asSIGroupByQueryStrategy
                                        var asSIGroupByQueryStrategy = xISelectManyQueryStrategy.source as IGroupByQueryStrategy;
                                        if (asSIGroupByQueryStrategy != null)
                                        {
                                            if (xasMemberExpression.Member.Name == "Key")
                                            {
                                                // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByMultipleFields\TestGroupByMultipleFields\ApplicationWebService.cs
                                                // are we selecting a complex key?
                                                var asSSLambdaExpression = asSIGroupByQueryStrategy.keySelector as LambdaExpression;
                                                if (asSSLambdaExpression != null)
                                                {
                                                    var asSSNNewExpression = asSSLambdaExpression.Body as NewExpression;
                                                    if (asSSNNewExpression != null)
                                                        return yieldNewExpression(that_source, asSSNNewExpression,
                                                            prefixes.Concat(new[] { Tuple.Create(index, SourceMember) }).ToArray()
                                                             );
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion



                                    var asSelectQueryStrategy = source as ISelectQueryStrategy;
                                    if (asSelectQueryStrategy != null)
                                    {
                                        #region asSIGroupByQueryStrategy
                                        var asSIGroupByQueryStrategy = asSelectQueryStrategy.source as IGroupByQueryStrategy;
                                        if (asSIGroupByQueryStrategy != null)
                                        {
                                            if (xasMemberExpression.Member.Name == "Key")
                                            {
                                                // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByMultipleFields\TestGroupByMultipleFields\ApplicationWebService.cs
                                                // are we selecting a complex key?
                                                var asSSLambdaExpression = asSIGroupByQueryStrategy.keySelector as LambdaExpression;
                                                if (asSSLambdaExpression != null)
                                                {
                                                    var asSSNNewExpression = asSSLambdaExpression.Body as NewExpression;
                                                    if (asSSNNewExpression != null)
                                                        return yieldNewExpression(that_source, asSSNNewExpression,
                                                            prefixes.Concat(new[] { Tuple.Create(index, SourceMember) }).ToArray()
                                                             );
                                                }
                                            }
                                        }
                                        #endregion


                                        // +		selector	{<>h__TransparentIdentifier1 => new <>f__AnonymousType2`2(<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1, qq = new <>f__AnonymousType3`1(u = "!!!"))}	System.Linq.Expressions.Expression<System.Func<<>f__AnonymousType1<<>f__AnonymousType0<TestSelectAndSubSelect.Data.PerformanceResourceTimingData2ApplicationPerformanceRow,string>,string>,<>f__AnonymousType2<<>f__AnonymousType1<<>f__AnonymousType0<TestSelectAndSubSelect.Data.PerformanceResourceTimingData2ApplicationPerformanceRow,string>,string>,<>f__AnonymousType3<string>>>>
                                        var asSSelectQueryStrategy = asSelectQueryStrategy.source as ISelectQueryStrategy;
                                        if (asSSelectQueryStrategy != null)
                                        {
                                            var asSSLambdaExpression = asSSelectQueryStrategy.selectorExpression as LambdaExpression;
                                            if (asSSLambdaExpression != null)
                                            {
                                                // X:\jsc.svn\examples\javascript\linq\test\TestSelectAndSubSelect\TestSelectAndSubSelect\ApplicationWebService.cs
                                                var asSSNewExpression = asSSLambdaExpression.Body as NewExpression;
                                                if (asSSNewExpression.Members[1].Name == xasMemberExpression.Member.Name)
                                                {
                                                    #region asSSNMethodCallExpression
                                                    var asSSNMethodCallExpression = asSSNewExpression.Arguments[1] as MethodCallExpression;
                                                    if (asSSNMethodCallExpression != null)
                                                    {
                                                        // http://stackoverflow.com/questions/141203/when-would-i-need-a-securestring-in-net
                                                        // can we actually call sub queries nw?


                                                        //asSSNMethodCallExpression.Method.Invoke(
                                                        //    asSSNMethodCallExpression.Object,
                                                        //    null
                                                        //    //asSSNMethodCallExpression.Arguments
                                                        //);

                                                        // x:\jsc.svn\examples\javascript\linq\test\testselectandsubselect\testselectandsubselect\applicationwebservice.cs

                                                        if (asSSNMethodCallExpression.Method.Name == refAverage.Name)
                                                        {
                                                            // x:\jsc.svn\examples\javascript\linq\test\testselectscalaraverage\testselectscalaraverage\applicationwebservice.cs
                                                            //SourceRow

                                                            var xasString = SourceRow[(SourceArgument as MemberExpression).Member.Name];

                                                            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectAboveAverage\TestSelectAboveAverage\ApplicationWebService.cs
                                                            // whats the datatype?
                                                            return GetValue();
                                                        }


                                                        if (asSSNMethodCallExpression.Method.Name == refCount.Name)
                                                        {
                                                            // x:\jsc.svn\examples\javascript\linq\test\testselectscalaraverage\testselectscalaraverage\applicationwebservice.cs
                                                            //SourceRow

                                                            var xasString = SourceRow[(SourceArgument as MemberExpression).Member.Name];

                                                            return Convert.ToInt64(xasString);
                                                        }


                                                        #region fFirstOrDefault
                                                        if (asSSNMethodCallExpression.Method.Name == refFirstOrDefault.Name)
                                                        {
                                                            var fFirstOrDefault = asSSNMethodCallExpression.Method;
                                                            var arg0InvocationExpression = asSSNMethodCallExpression.Arguments[0] as InvocationExpression;

                                                            // [0] = {<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.x}

                                                            #region xparameters
                                                            var xparameters = arg0InvocationExpression.Arguments.Select(
                                                                   (xSourceArgument, i) =>
                                                                {
                                                                    var xElementType = xSourceArgument.Type;

                                                                    // Additional information: Column 'qq.x' does not belong to table .
                                                                    //var xasString = SourceRow[GetPrefixedTargetName() + "." + (xSourceArgument as MemberExpression).Member.Name];
                                                                    // System.Data.dll
                                                                    //Additional information: Column 'x' does not belong to table.
                                                                    // Additional information: Column 'Key' does not belong to table .

                                                                    var xasString = SourceRow[(xSourceArgument as MemberExpression).Member.Name];

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


                                                            // Expression = {value(TestSelectAndSubSelect.ApplicationWebService+<>c__DisplayClass0).child1}

                                                            var arg0IMemberExpression = arg0InvocationExpression.Expression as MemberExpression;
                                                            if (arg0IMemberExpression != null)
                                                            {
                                                                // Expression = {value(TestSelectAndSubSelect.ApplicationWebService+<>c__DisplayClass0)}

                                                                var arg0IMConstantExpression = arg0IMemberExpression.Expression as ConstantExpression;
                                                                if (arg0IMConstantExpression != null)
                                                                {
                                                                    var xFieldInfo = arg0IMemberExpression.Member as FieldInfo;

                                                                    var xDelegate = (Delegate)xFieldInfo.GetValue(arg0IMConstantExpression.Value);

                                                                    var xElements = xDelegate.Method.Invoke(
                                                                        null,
                                                                        xparameters
                                                                    );


                                                                    var xxx = fFirstOrDefault.Invoke(null, new[] { xElements });


                                                                    return xxx;
                                                                }
                                                            }

                                                            Debugger.Break();
                                                            return null;
                                                        }
                                                        #endregion


                                                        Debugger.Break();
                                                    }
                                                    #endregion



                                                    var asSSNNewExpression = asSSNewExpression.Arguments[1] as NewExpression;
                                                    if (asSSNNewExpression != null)
                                                        return yieldNewExpression(that_source, asSSNNewExpression,
                                                            prefixes.Concat(new[] { Tuple.Create(index, SourceMember) }).ToArray()
                                                             );
                                                }
                                            }

                                        }


                                    }
                                }
                                #endregion

                                #region xasConstantExpression
                                var xasConstantExpression = SourceArgument as ConstantExpression;
                                if (xasConstantExpression != null)
                                {
                                    // we sent the value to sql server, and now want it back.
                                    // the ony benefit might be to use it in a where clause?

                                    return xasConstantExpression.Value;
                                }
                                #endregion

                                // Additional information: Column 'datagroup1' does not belong to table .
                                // X:\jsc.svn\examples\javascript\LINQ\test\SelectToUpperIntoNewExpression\SelectToUpperIntoNewExpression\ApplicationWebService.cs

                                var PrefixedTargetName = GetPrefixedTargetName();

                                var asString = SourceRow[PrefixedTargetName];
                                // um. is our data adapter sending us strings?

                                if (SourceType == typeof(long) || SourceType.IsEnum)
                                    return Convert.ToInt64(asString);

                                if (SourceType == typeof(int))
                                    return Convert.ToInt32(asString);

                                if (SourceType == typeof(double))
                                    return Convert.ToDouble(asString);

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

                                // x:\jsc.svn\examples\javascript\linq\test\testselectofselect\testselectofselect\applicationwebservice.cs
                                // why DBNull exists??
                                if (asString == DBNull.Value)
                                    return null;

                                return asString;
                            };
                        #endregion


                        #region asNewExpression


                        yieldNewExpression = (that_source, asNewExpression, prefixes) =>
                        {



                            var parameters = asNewExpression.Arguments.Select(
                               (a, i) =>
                                {
                                    var SourceMember = default(MemberInfo);

                                    if (asNewExpression.Members != null)
                                    {
                                        SourceMember = asNewExpression.Members[i];
                                    }

                                    return GetArgumentValue(
                                        // ?
                                        that_source,
                                        SourceMember, a, i, prefixes);
                                }
                           ).ToArray();


                            // 
                            //Additional information: Object of type 'System.String' cannot be converted to type '<>f__AnonymousType1`1[System.String]'.

                            var x = asNewExpression.Constructor.Invoke(
                                parameters
                            );

                            return x;
                        };


                        {
                            var asNewExpression = asLambdaExpression.Body as NewExpression;
                            if (asNewExpression != null)
                            {

                                var x = yieldNewExpression(that.source, asNewExpression, new Tuple<int, MemberInfo>[0]);

                                return (TSource)x;
                            }
                        }
                        #endregion

                        //+asLambdaExpression.Body { new PerformanceResourceTimingData2ApplicationResourcePerformanceRow() { path = ggg.Last().u.path }}
                        //System.Linq.Expressions.Expression { System.Linq.Expressions.MemberInitExpression}

                        #region xasMemberInitExpression
                        {
                            var xasMemberInitExpression = asLambdaExpression.Body as MemberInitExpression;
                            if (xasMemberInitExpression != null)
                            {
                                var xMNewExpression = xasMemberInitExpression.NewExpression;
                                var xx = yieldNewExpression(that.source, xMNewExpression, new Tuple<int, MemberInfo>[0]);

                                // indexer init?

                                xasMemberInitExpression.Bindings.WithEachIndex(
                                    (SourceBinding, i) =>
                                    {
                                        // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectIntoViewRow\TestSelectIntoViewRow\ApplicationWebService.cs

                                        var mm = SourceBinding as MemberAssignment;

                                        var v = GetArgumentValue(
                                            source,

                                            SourceBinding.Member, mm.Expression, i, new Tuple<int, MemberInfo>[0]);


                                        var xFieldInfo = SourceBinding.Member as FieldInfo;
                                        if (xFieldInfo != null)
                                            xFieldInfo.SetValue(xx, v);
                                    }
                                );


                                return (TSource)xx;
                            }
                        }
                        #endregion


                        #region xUnaryExpression
                        var xUnaryExpression = asLambdaExpression.Body as UnaryExpression;
                        if (xUnaryExpression != null)
                        {
                            // X:\jsc.svn\examples\javascript\LINQ\test\TestWhereIsNullOrEmpty\TestWhereIsNullOrEmpty\ApplicationWebService.cs

                            var __value = SourceRow[0];
                            if (xUnaryExpression.Type == typeof(bool))
                            {
                                var i4 = Convert.ToInt32(__value);

                                var b = (i4 != 0);
                                return (TSource)(object)(b);
                            }
                        }
                        #endregion


                        var xNewArrayExpression = asLambdaExpression.Body as NewArrayExpression;
                        if (xNewArrayExpression != null)
                        {
                            // X:\jsc.svn\examples\javascript\linq\test\TestSelectTwoOfEach\TestSelectTwoOfEach\ApplicationWebService.cs

                            return default(TSource);
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

