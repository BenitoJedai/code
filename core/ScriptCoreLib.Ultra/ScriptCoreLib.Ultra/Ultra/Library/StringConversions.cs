using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace ScriptCoreLib.Library
{
    namespace Templates
    {
        #region code template
        public class StringConversions__ElementType
        {
            public static StringConversions__ElementType FromString(string e)
            {
                throw new NotSupportedException();
            }

            public static string ToString(StringConversions__ElementType e)
            {
                throw new NotSupportedException();
            }

            public static string ConvertElementTypeEnumerableToString(IEnumerable<StringConversions__ElementType> ee)
            {
                //0:10325ms WebServiceForJavaScript.WriteMethod { Name = foo }
                //0:10325ms enter ConvertElementTypeArrayToString ee:[object Object] 

                //Console.WriteLine("enter ConvertElementTypeArrayToString " + new { ee });
                //Console.WriteLine("enter ConvertElementTypeArrayToString ee:" + ee);

                // X:\jsc.svn\examples\javascript\Test\TestIEnumerableForService\TestIEnumerableForService\ApplicationWebService.cs
                // X:\jsc.svn\examples\javascript\test\TestWebMethodTaskOfIEnumerable\TestWebMethodTaskOfIEnumerable\ApplicationWebService.cs
                //return ConvertElementTypeArrayToString(e.ToArray());


                //Unable to cast object of type 'System.Int32[]' to type 'System.Object[]'.


                #region return inline ConvertElementTypeArrayToString
                if (ee == null)
                {
                    //Console.WriteLine("enter ConvertElementTypeArrayToString ee is null");

                    //                 at System.Linq.Enumerable.ToArray[TSource](IEnumerable`1 source)
                    //at mscorlib.< 02000005IEnumerable\.ConvertToString >.ConvertToString(IEnumerable`1)
                    //at TestIEnumerableForService.Global.Invoke(InternalWebMethodInfo)
                    //at ScriptCoreLib.Ultra.WebService.InternalGlobalExtensions.<> c__DisplayClass0.< InternalApplication_BeginRequest > b__12(WebServiceScriptApplication app)
                    //at ScriptCoreLib.Ultra.WebService.InternalGlobalExtensions.InternalApplication_BeginRequest(InternalGlobal g)
                    //at TestIEnumerableForService.Global.Application_BeginRequest(Object, EventArgs)
                    //at System.Web.HttpApplication.SyncEventExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()
                    //at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean & completedSynchronously)

                    return null;
                }

                //var o = (object[])e;

                var e = ee.ToArray();

                var xml = new XElement("enumerable");

                var Length = e.Length;

                //Console.WriteLine("ConvertElementTypeArrayToString " + new { Length });
                //Console.WriteLine("ConvertElementTypeArrayToString Length: " + Length);

                xml.Add(new XAttribute("c", "" + Length));

                for (int i = 0; i < Length; i++)
                {
                    // ldelem.ref ?
                    var item = e[i];

                    //                 [MethodAccessException: Attempt by method &#39;mscorlib.&lt;020000f3Array\+ConvertToString&gt;.ConvertToString(Int32[])&#39; to access method &#39;&lt;&gt;f__AnonymousType4d`2&lt;System.Int32,System.__Canon&gt;..ctor(Int32, System.__Canon)&#39; failed.]
                    //mscorlib.&lt;020000f3Array\+ConvertToString&gt;.ConvertToString(Int32[] ) +305


                    //Console.WriteLine("i: " + i + ", item: " + item);

                    xml.Add(new XElement("i" + i, ToString((StringConversions__ElementType)item)));
                }

                var value = StringConversions.ConvertXElementToString(xml);

                //Console.WriteLine("ConvertElementTypeArrayToString " + new { value });
                //Console.WriteLine("ConvertElementTypeArrayToString value: " + value);

                return value;
                #endregion
            }


            public static string ConvertListOfElementTypeToString(List<StringConversions__ElementType> ee)
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140517
                // X:\jsc.svn\examples\javascript\test\TestWebMethodTaskOfIEnumerable\TestWebMethodTaskOfIEnumerable\ApplicationWebService.cs
                //return ConvertElementTypeArrayToString(e.ToArray());


                //Unable to cast object of type 'System.Int32[]' to type 'System.Object[]'.

                //Console.WriteLine("enter ConvertElementTypeArrayToString");

                #region return inline ConvertElementTypeArrayToString
                if (ee == null)
                    return null;

                //var o = (object[])e;

                var e = ee.ToArray();

                var xml = new XElement("enumerable");

                var Length = e.Length;

                xml.Add(new XAttribute("c", "" + Length));

                for (int i = 0; i < Length; i++)
                {
                    // ldelem.ref ?
                    var item = e[i];

                    //                 [MethodAccessException: Attempt by method &#39;mscorlib.&lt;020000f3Array\+ConvertToString&gt;.ConvertToString(Int32[])&#39; to access method &#39;&lt;&gt;f__AnonymousType4d`2&lt;System.Int32,System.__Canon&gt;..ctor(Int32, System.__Canon)&#39; failed.]
                    //mscorlib.&lt;020000f3Array\+ConvertToString&gt;.ConvertToString(Int32[] ) +305


                    //Console.WriteLine("i: " + i + ", item: " + item);

                    xml.Add(new XElement("i" + i, ToString((StringConversions__ElementType)item)));
                }

                var value = StringConversions.ConvertXElementToString(xml);

                //Console.WriteLine("value: " + value);

                return value;
                #endregion
            }

            public static string ConvertElementTypeArrayToString(StringConversions__ElementType[] e)
            {
                //Unable to cast object of type 'System.Int32[]' to type 'System.Object[]'.

                //Console.WriteLine("enter ConvertElementTypeArrayToString");

                if (e == null)
                    return null;

                //var o = (object[])e;


                var xml = new XElement("array");

                var Length = e.Length;

                xml.Add(new XAttribute("c", "" + Length));

                for (int i = 0; i < Length; i++)
                {
                    // ldelem.ref ?
                    var item = e[i];

                    //                 [MethodAccessException: Attempt by method &#39;mscorlib.&lt;020000f3Array\+ConvertToString&gt;.ConvertToString(Int32[])&#39; to access method &#39;&lt;&gt;f__AnonymousType4d`2&lt;System.Int32,System.__Canon&gt;..ctor(Int32, System.__Canon)&#39; failed.]
                    //mscorlib.&lt;020000f3Array\+ConvertToString&gt;.ConvertToString(Int32[] ) +305


                    //Console.WriteLine("i: " + i + ", item: " + item);

                    xml.Add(new XElement("i" + i, ToString((StringConversions__ElementType)item)));
                }

                var value = StringConversions.ConvertXElementToString(xml);

                //Console.WriteLine("value: " + value);

                return value;
            }

            public static IEnumerable<StringConversions__ElementType> ConvertStringToElementTypeEnumerable(string e)
            {
                //Additional information: Attempt by method 'mscorlib.<02000005IEnumerable\+ConvertToString>.ConvertToString(System.Collections.Generic.IEnumerable`1<TestIEnumerableForService.foo>)' to access method '<>f__AnonymousType6`1<System.__Canon>..ctor(System.__Canon)' failed.


                // X:\jsc.svn\examples\javascript\Test\TestIEnumerableForService\TestIEnumerableForService\ApplicationWebService.cs
                //Console.WriteLine("enter ConvertStringToElementTypeEnumerable " + new { e });
                //Console.WriteLine("enter ConvertStringToElementTypeEnumerable e:" + e);

                // X:\jsc.svn\examples\javascript\test\TestWebMethodTaskOfIEnumerable\TestWebMethodTaskOfIEnumerable\ApplicationWebService.cs
                //return ConvertStringToElementTypeArray(e).AsEnumerable();

                #region return inline ConvertStringToElementTypeArray
                var xml = StringConversions.ConvertStringToXElement(e);

                // X:\jsc.svn\examples\javascript\Test\TestIEnumerableForService\TestIEnumerableForService\ApplicationWebService.cs
                if (xml == null)
                    return null;


                //&lt;array c=&quot;2&quot;&gt;
                //  &lt;i0&gt;2&lt;/i0&gt;
                //  &lt;i1&gt;0&lt;/i1&gt;
                //&lt;/array&gt;

                //Console.WriteLine(new { xml });

                var Length = int.Parse(xml.Attribute("c").Value);

                //Console.WriteLine(new { Length });


                var y = new StringConversions__ElementType[Length];

                for (int i = 0; i < Length; i++)
                {
                    //Console.WriteLine(new { i });

                    y[i] = FromString(xml.Element("i" + i).Value);
                }

                return y.AsEnumerable();
                #endregion
            }


            public static List<StringConversions__ElementType> ConvertStringToListOfElementType(string e)
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140517
                // X:\jsc.svn\examples\javascript\UIAutomationEvents\UIAutomationEvents\Application.cs
                // X:\jsc.svn\examples\javascript\test\TestWebMethodTaskOfIEnumerable\TestWebMethodTaskOfIEnumerable\ApplicationWebService.cs
                //return ConvertStringToElementTypeArray(e).AsEnumerable();

                #region return inline ConvertStringToElementTypeArray
                var xml = StringConversions.ConvertStringToXElement(e);

                //&lt;array c=&quot;2&quot;&gt;
                //  &lt;i0&gt;2&lt;/i0&gt;
                //  &lt;i1&gt;0&lt;/i1&gt;
                //&lt;/array&gt;

                //Console.WriteLine(new { xml });

                var Length = int.Parse(xml.Attribute("c").Value);

                //Console.WriteLine(new { Length });


                var y = new StringConversions__ElementType[Length];

                for (int i = 0; i < Length; i++)
                {
                    //Console.WriteLine(new { i });

                    y[i] = FromString(xml.Element("i" + i).Value);
                }

                return y.ToList();
                #endregion
            }

            public static StringConversions__ElementType[] ConvertStringToElementTypeArray(string e)
            {
                if (string.IsNullOrEmpty(e))
                    return null;

                var xml = StringConversions.ConvertStringToXElement(e);

                //&lt;array c=&quot;2&quot;&gt;
                //  &lt;i0&gt;2&lt;/i0&gt;
                //  &lt;i1&gt;0&lt;/i1&gt;
                //&lt;/array&gt;

                //Console.WriteLine(new { xml });

                var Length = int.Parse(xml.Attribute("c").Value);

                //Console.WriteLine(new { Length });


                var y = new StringConversions__ElementType[Length];

                for (int i = 0; i < Length; i++)
                {
                    //Console.WriteLine(new { i });

                    y[i] = FromString(xml.Element("i" + i).Value);
                }

                return y;
            }
        }
        #endregion
    }

    public static partial class StringConversions
    {


        #region string[]
        [Obsolete]
        public static string ConvertStringArrayToString(string[] e)
        {
            if (e == null)
                return null;

            var xml = new XElement("array");

            var Length = e.Length;

            // what about null elements?
            // what about now?
            // ScriptCoreLibJava.XLinq does not yet support reading all elements :) thus we have to name then ahead of time..
            xml.Add(new XAttribute("c", "" + Length));

            for (int i = 0; i < Length; i++)
            {
                xml.Add(new XElement("i" + i, (e[i])));
            }

            return ConvertXElementToString(xml);
        }

        [Obsolete]
        public static string[] ConvertStringToStringArray(string e)
        {
            if (string.IsNullOrEmpty(e))
                return null;

            var xml = ConvertStringToXElement(e);

            var Length = int.Parse(xml.Attribute("c").Value);

            var y = new string[Length];

            for (int i = 0; i < Length; i++)
            {
                y[i] = (xml.Element("i" + i).Value);
            }

            return y;
        }
        #endregion


    }
}
