extern alias globalscl;
using globalscl::ScriptCoreLib.JavaScript;
using globalscl::ScriptCoreLib.JavaScript.DOM;
using globalscl::ScriptCoreLib.Shared;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Ultra.Library;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.Remoting
{
    [Obsolete("this shall also work for AIR? what about web workers?")]
    public abstract class InternalWebMethodRequest
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102
        // used by the compiler!


        // tested by
        // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\Application.cs
        [Obsolete("MethodName ?")]
        public string Name;

        public string MetadataToken;

        //public string Data;

        public InternalWebMethodRequest()
        {
            // look, a ctor!

            //Console.WriteLine("new InternalWebMethodRequest");
        }

        NameValueCollection InternalUploadValues = new NameValueCollection();

        public static void AddParameter(InternalWebMethodRequest that, string name, string value)
        {
            if (null == value)
                return;

            var key = "_" + that.MetadataToken + "_" + name;

            //Console.WriteLine("AddParameter " + new { key });

            that.InternalUploadValues[key] = value.ToXMLString();

            //if (string.IsNullOrEmpty(that.Data))
            //{
            //    that.Data = "";
            //}
            //else
            //{
            //    that.Data += "&";
            //}

            //// http://stackoverflow.com/questions/81991/a-potentially-dangerous-request-form-value-was-detected-from-the-client
            //var __value = value.ToXMLString();

            //// http://www.w3schools.com/jsref/jsref_encodeuricomponent.asp
            //var evalue = Native.window.escape(__value).Replace("+", "%" + ((byte)'+').ToString("x2"));

            ////Console.WriteLine(
            ////    new { name, __value, evalue }
            ////);


            //that.Data += "_" + that.MetadataToken + "_" + name + "=" + evalue;
        }

        public static void Invoke(InternalWebMethodRequest that)
        {
            var w = new System.Net.WebClient();

            w.UploadValuesCompleted +=
                (sender, args) =>
                {
                    //if (args.Error != null)
                    //{
                    //    //t.text = "UploadValuesCompleted error " + new { args.Error }.ToString();

                    //    return;
                    //}

                    // DownloadStringAsync { Length = 2822 }

                    //var data = Encoding.UTF8.GetString(args.Result);

                    // does this work in android webview?
                    that.Complete(args.Result, w);
                };

            //var x = new IXMLHttpRequest();

            // we are using
            // "xml" as path
            // "WebMethod" as method selector
            // we could hide those details into post.
            // what about generic methods
            // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\Application.cs

            // http://xhr.spec.whatwg.org/
            // X:\jsc.svn\examples\javascript\Test\TestWebMethodIPAddress\TestWebMethodIPAddress\ApplicationWebService.cs

            // will this help us?
            // IE will thow InvalidStateError
            //x.withCredentials = true;

            ////x.open(HTTPMethodEnum.POST, Target);
            ////// http://stackoverflow.com/questions/12072315/uncaught-error-invalid-state-err-dom-exception-11

            ////x.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            ////x.send(
            ////    globalscl::ScriptCoreLib.JavaScript.BCLImplementation.System.Net.__WebClient.ToFormDataString(
            ////        that.InternalUploadValues
            ////    )
            ////);

            ////x.InvokeOnComplete(that.Complete, 50);

            that.InternalUploadValues["WebMethodMetadataToken"] = that.MetadataToken;
            that.InternalUploadValues["WebMethodMetadataName"] = that.Name;

            // do we have a previous etag available?
            if (MetadataTokenToETagLookup.ContainsKey(that.MetadataToken))
            {
                // send in the etag to get 304.
                that.InternalUploadValues["ETag"] = Convert.ToBase64String(MetadataTokenToETagLookup[that.MetadataToken]);
            }

            //var Target = "/xml?WebMethod=" + that.MetadataToken + "&n=" + that.Name;

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140110-xml
            // description?
            var Target = "/xml/" + that.Name;

            w.UploadValuesAsync(
                new Uri(Target, UriKind.Relative),
                that.InternalUploadValues
            );


        }

        public NameValueCollection InternalFields;

        [Description("note that jsc is implicitly using base64 and xml")]
        public static string GetInternalFieldValue(InternalWebMethodRequest that, string FieldName)
        {
            //Console.WriteLine("GetInternalFieldValue " + new { FieldName });

            var FieldValue = default(string);

            if (that == null)
            {
                FieldValue = InternalFieldsFromTypeInitializer[FieldName];
            }
            else
            {
                FieldValue = that.InternalFields[FieldName];
            }

            //Console.WriteLine("GetInternalFieldValue " + new { FieldName, FieldValue });
            return FieldValue;
        }

        [Obsolete("do we need to use sessionStorage instead?")]
        public static bool __window_localStorage_available
        {
            get
            {
                //Uncaught window.localStorage is not available in packaged apps. 
                // Use chrome.storage.local instead. 
                var value = false;

                try
                {

                    if (Native.window != null)
                    {
                        // tested by
                        // X:\jsc.svn\examples\javascript\async\AsyncButtonExperiment\AsyncButtonExperiment\Application.cs


                        if (Native.window.localStorage != null)
                        {
                            // android webview does not have localStorage?
                            // what happens after reload?
                            // will AppCache remain yet cookies be lost?
                            // http://stackoverflow.com/questions/5899087/android-webview-localstorage
                            value = true;
                        }
                    }
                }
                catch
                {

                }

                return value;
            }
        }

        public static NameValueCollection GetInternalFields(object r = null, WebClient c = null)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.InternalApplication_BeginRequest.cs

            //.field field_elapsed:<Stopwatch ElapsedMilliseconds="1204" IsRunning="True" />

            if (c != null)
            {
                var value = new NameValueCollection();

                c.ResponseHeaders.AllKeys.WithEach(
                    k =>
                    {
                        var FieldName = k.SkipUntilOrEmpty(".field ");

                        if (string.IsNullOrEmpty(FieldName))
                            return;

                        var FieldValue = c.ResponseHeaders[k];

                        //Console.WriteLine("GetInternalFields " + new { FieldName, FieldValue });


                        value[FieldName] = FieldValue;
                    }
                );

                return value;
            }



            // web worker may not have window cookies

            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.cs
            var InternalFieldsCookie = new globalscl::ScriptCoreLib.JavaScript.Runtime.Cookie("InternalFields");
            var InternalFieldsCookieValue = InternalFieldsCookie.Value;
            var InternalFields = InternalFieldsCookie.Values;

            // tested by
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServer\ChromeTCPServer\Application.cs
            if (!__window_localStorage_available)
                return InternalFields;


            InternalFieldsCookie.Delete();

            // when is r null?
            if (r == null)
            {
                if (string.IsNullOrEmpty(InternalFieldsCookieValue))
                {
                    Console.WriteLine("GetInternalFields load fromlocalstorage! ");

                    InternalFieldsCookieValue = Native.window.localStorage["InternalFields"];
                    InternalFields = globalscl::ScriptCoreLib.JavaScript.Runtime.Cookie.GetValues(InternalFieldsCookieValue);
                }
                else
                {
                    Console.WriteLine("GetInternalFields save to localstorage! " + new { InternalFieldsCookieValue });


                    Native.window.localStorage["InternalFields"] = InternalFieldsCookieValue;
                }
            }

            return InternalFields;
        }

        #region InternalFieldsFromTypeInitializer
        public static NameValueCollection InternalFieldsFromTypeInitializer;

        static InternalWebMethodRequest()
        {
            Console.WriteLine("InternalFieldsFromTypeInitializer");
            InternalFieldsFromTypeInitializer = GetInternalFields();
        }
        #endregion


        // local storage dictionary?
        public static Dictionary<string, byte[]> MetadataTokenToETagLookup = new Dictionary<string, byte[]>();
        public static Dictionary<string, byte[]> MetadataTokenToContentLookup = new Dictionary<string, byte[]>();


        // called by Invoke
        public void Complete(byte[] r, WebClient c)
        {
            // X:\jsc.svn\examples\rewrite\Test\Test453If\Test453If\Program.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102
            // fails for roslyn? or is the rewriter doing something it should not?

            if (r == null)
            {
                Console.WriteLine("InternalWebMethodRequest.Complete r is null. why?");
            }



//#if FPOPERROR
            // http://stackoverflow.com/questions/3574659/how-to-get-status-code-from-webclient

            // were we getting 204 or 304?
            // http://www.blogosfera.co.uk/2013/04/why-do-i-get-an-empty-304-not-modified-response-from-webclient-with-default-cache-policy/


            #region ETag
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140323
            //Console.WriteLine("InternalWebMethodRequest.Complete " + new { this.Name, r.Length });

            var hasETag = c.ResponseHeaders.AllKeys.Contains("ETag");
            if (hasETag)
            {
                var serverETag64 = c.ResponseHeaders["ETag"];
                var serverETagbytes = Convert.FromBase64String(serverETag64);
                var serverETag = serverETagbytes.ToHexString();

                //Console.WriteLine(new { serverETag, r.Length });

                if (r.Length == 0)
                {
                    // do we have the bytes in cache?

                    if (MetadataTokenToContentLookup.ContainsKey(this.MetadataToken))
                    {
                        // yes we have the bytes
                        // do the etags match?

                        //Console.WriteLine(
                        //    "server only sent us the ETag?"
                        //    );

                        // what would this mean for interfaces? and yields and events?
                        r = MetadataTokenToContentLookup[this.MetadataToken];
                    }
                }

                // !!!
            }
            //0:32881ms new InternalWebMethodRequest view-source:37032
            //0:32942ms InternalWebMethodRequest.Complete { Name = Gravatar, Length = 239 } view-source:37032
            //0:32947ms { Name = Gravatar, MetadataToken = 06000001, ETag = 5a62b5d768653ae632ba4ff7e0b7a03c, ElapsedMilliseconds = 5 } view-source:36991
            //0:71528ms new InternalWebMethodRequest view-source:37032
            //0:71581ms InternalWebMethodRequest.Complete { Name = Gravatar, Length = 0 } view-source:37032
            //0:71582ms { Name = Gravatar, MetadataToken = 06000001, ETag = d41d8cd98f00b204e9800998ecf8427e, ElapsedMilliseconds = 1 } 


            //c.stat
            var s = Stopwatch.StartNew();

            // 0:10184ms InternalWebMethodRequest.Complete { hash_md5 = [object Uint8ClampedArray], ElapsedMilliseconds = 4 } 

            // we need to check this against the etag server sent!
            var ETag = r.ToMD5Bytes();

            // http://en.wikipedia.org/wiki/HTTP_ETag
            // http://msdn.microsoft.com/en-us/library/cc219284.aspx

            MetadataTokenToETagLookup[this.MetadataToken] = ETag;
            // local replay attack?
            MetadataTokenToContentLookup[this.MetadataToken] = r;

            //Console.WriteLine(
            //    //"InternalWebMethodRequest.Complete " + 
            //    new { this.Name, this.MetadataToken, ETag = ETag.ToHexString(), s.ElapsedMilliseconds }
            //    );

            #endregion




            this.InternalFields = GetInternalFields(r, c);

            //if (r.status == IXMLHttpRequest.HTTPStatusCodes.NoContent)
            if (r.Length == 0)
            {
                // we should be told we either expect 204 or a value?
                InvokeCallback("TaskComplete",
                    // we have to send somethng, otheriwse
                    // ApplicationWebService+<>0600001c.InvokeCallback will blow up by asking for TaskResult
                    lookup: x => null
                );

                return;
            }

            //what about service worker?
            var xml = XElement.Parse(
                Encoding.UTF8.GetString(r)
            );

            // Status Code:204 No Content
            //var xml = r.responseXML;

            //if (xml == null)
            //    throw new Exception("responseXML was null: " + new { r.responseXML, r.responseText });


            //foreach (var item in xml.documentElement.childNodes)
            foreach (var item in xml.Elements())
            {
                //Debugger.Break();

                //Native.Window.alert("callback: " + item.nodeName);

                InvokeCallback(item.Name.LocalName,
                    x =>
                    {
                        //Native.Window.alert("parameter: " + x);

                        var u = default(string);

                        foreach (var p in item.Elements())
                        {
                            if (p.Name.LocalName == x)
                            {
                                u = p.Value;
                                break;
                            }
                        }

                        return u;
                    }
                );


                //new IHTMLDiv { innerText = "callback: " + item.nodeName }.AttachToDocument();

                //foreach (var p in item.childNodes)
                //{
                //    new IHTMLDiv { innerText = "parameter: " + p.nodeName + " = " + p.text }.AttachToDocument();

                //}
            }
//#endif
        }


        public delegate string ParameterLookup(string parameter);

        public virtual void InvokeCallback(string name, ParameterLookup lookup)
        {
            throw new Exception("InvokeCallback");
        }
    }

}
