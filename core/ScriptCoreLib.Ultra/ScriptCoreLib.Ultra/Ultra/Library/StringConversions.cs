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

                return y.AsEnumerable();
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
