using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Extensions;
using System.Diagnostics;

namespace ScriptCoreLib.Ultra.WebService
{
    public class InternalWebMethodInfo
    {
        // whats up with const support jsc.meta?

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140110-xml
        public static string QueryKey = "WebMethod";

        public string MethodName;

        // DeclaringType
        public string TypeFullName;

        public string MetadataToken;


        public InternalWebMethodParameterInfo[] Parameters;

        public ArrayList InternalParameters;

        public Dictionary<string, string> InternalFields;


        // to be called for field init a /view-source
        public bool IsConstructor;


        public Action<long> AtElapsedMilliseconds;

        // called by?
        public static void AddField(InternalWebMethodInfo that, string FieldName, string FieldValue)
        {
            if (that.InternalFields == null)
                that.InternalFields = new Dictionary<string, string>();

            //> 000a 0x01d5 bytes
            //time to serialize fields into cookie { Length = 1 }
            //AddField { FieldName = field_Foo, FieldValue = 7 }

            // tested by
            // x:\jsc.svn\examples\javascript\test\testienumerableforservice\testienumerableforservice\applicationwebservice.cs
            // X:\jsc.svn\examples\javascript\Test\TestWebServiceTaskFields\TestWebServiceTaskFields\ApplicationWebService.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201310/20131011-fields

            //Console.WriteLine("InternalWebMethodInfo.AddField " + new { FieldName, FieldValue });

            that.InternalFields[FieldName] = FieldValue;
        }

        #region TaskComplete
        public bool TaskComplete;
        public string TaskResult;

        // methods called
        public InternalWebMethodInfo[] Results;


        public static void SetResult(InternalWebMethodInfo that)
        {
            that.TaskComplete = true;
        }

        public static void SetResult(string TaskResult, InternalWebMethodInfo that)
        {
            // called by
            // x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WebService.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130924-async-web-service

            that.TaskResult = TaskResult;

            SetResult(that);
        }
        #endregion


        public static void AddParameter(InternalWebMethodInfo that, string Name, string Value)
        {
            //Console.WriteLine("InternalWebMethodInfo.AddParameter " + new { Name, Value });

            if (that.InternalParameters == null)
                that.InternalParameters = new ArrayList();

            var n = new InternalWebMethodParameterInfo
            {
                Name = Name,
                Value = Value
            };

            that.InternalParameters.Add(n);

            that.Parameters = (InternalWebMethodParameterInfo[])that.InternalParameters.ToArray(
                typeof(InternalWebMethodParameterInfo)
            );
        }

        public override string ToString()
        {
            return new
            {

                IsConstructor,
                MetadataToken,
                Name = MethodName,
                TypeFullName,

                Parameters = this.Parameters.Length

            }.ToString();
        }

        [Obsolete]
        public string ToQueryString()
        {
            return "?" + QueryKey + "=" + MetadataToken;
        }

        public static InternalWebMethodInfo First(InternalWebMethodInfo[] e, string MetadataToken)
        {
            var k = default(InternalWebMethodInfo);

            if (!string.IsNullOrEmpty(MetadataToken))
                foreach (var item in e)
                {
                    if (item.MetadataToken == MetadataToken)
                    {
                        k = item;
                        break;
                    }
                }

            return k;
        }

        public static string GetParameterValue(InternalWebMethodInfo that, string name)
        {
            var r = default(string);

            Console.WriteLine("GetParameterValue: " + new { name, that.Parameters.Length });


            // do we support null parameters?
            var value = default(string);

            //Console.WriteLine("LoadParameters: name: " + Parameter.Name);

            var key = "_" + that.MetadataToken + "_" + name;

            Console.WriteLine("GetParameterValue: " + new { key });

            var value_Form = that.InternalContext.Request.Form[key];

            if (null != value_Form)
            {
                value = value_Form;
            }

            Console.WriteLine("GetParameterValue: " + new { value });

            //Console.WriteLine("LoadParameters: value: " + value);

            //Parameter.Value = value.FromXMLString();
            r = value.FromXMLString();

            Console.WriteLine("GetParameterValue: " + new { r });

            return r;
        }





        public HttpContext InternalContext;
        [Obsolete("is that it, just store the context?")]
        public void LoadParameters(HttpContext c)
        {
            this.InternalContext = c;
        }







        //I/System.Console(13599): exit InternalURLDecode { ElapsedMilliseconds = 47, Value = /xml/WebMethod2 }
        //D/dalvikvm(13599): GC_CONCURRENT freed 394K, 6% free 8155K/8672K, paused 9ms+3ms, total 37ms



        static class __InternalURLDecode_lookup
        {
            // Caused by: java.lang.ArrayIndexOutOfBoundsException: length=255; index=255

            public static string[] xToUpper = new string[0xff + 1];
            public static string[] xToLower = new string[0xff + 1];
            public static string[] xi = new string[0xff + 1];

            static __InternalURLDecode_lookup()
            {

                for (int i = 0; i <= 255; i++)
                {
                    // X:\jsc.svn\examples\javascript\android\Test\TestAndroidCryptoKeyGenerate\TestAndroidCryptoKeyGenerate\ApplicationWebService.cs
                    // on android we shall try both?

                    xToUpper[i] = "%" + i.ToString("x2").ToUpper();
                    xToLower[i] = "%" + i.ToString("x2").ToLower();
                    xi[i] = new string((char)i, 1);

                }
            }
        }


        // called by?
        public static string InternalURLDecode(string Value)
        {
            // X:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalAndroidWebServiceActivity.cs

            // https://docs.oracle.com/javase/7/docs/api/java/net/URLDecoder.html

            // is this a slow method?

            // X:\jsc.svn\examples\javascript\android\forms\AndroidHopToActivityThread\AndroidHopToActivityThread\ApplicationWebService.cs

            // http://stackoverflow.com/questions/6753901/httputility-urldecode-turns-2b-into-space-but-i-need-it-to-be

            var sw = Stopwatch.StartNew();

            Console.WriteLine("enter InternalURLDecode " + new { Value });

            // http://www.w3schools.com/tags/ref_urlencode.asp
            // http://www.albionresearch.com/misc/urlencode.php

            for (int i = 0; i <= 255; i++)
            {
                // X:\jsc.svn\examples\javascript\android\Test\TestAndroidCryptoKeyGenerate\TestAndroidCryptoKeyGenerate\ApplicationWebService.cs
                // on android we shall try both?

                //var xToUpper = "%" + i.ToString("x2").ToUpper();
                //var xToLower = "%" + i.ToString("x2").ToLower();

                Value = Value.Replace(__InternalURLDecode_lookup.xToUpper[i], __InternalURLDecode_lookup.xi[i]);
                Value = Value.Replace(__InternalURLDecode_lookup.xToLower[i], __InternalURLDecode_lookup.xi[i]);
            }

            Console.WriteLine("exit InternalURLDecode " + new { sw.ElapsedMilliseconds, Value });
            return Value;
        }

        private static void WriteFormKeysToConsole(HttpContext c)
        {
            foreach (var item in c.Request.Form.AllKeys)
            {
                Console.WriteLine("WriteFormKeysToConsole: existing key: " + item);
            }
        }
    }

}
