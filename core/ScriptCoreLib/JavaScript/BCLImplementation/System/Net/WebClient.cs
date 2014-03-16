﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ScriptCoreLib.Shared.BCLImplementation.System.Net;
using ScriptCoreLib.JavaScript.DOM;
using System.Threading.Tasks;
using System.Collections.Specialized;
using ScriptCoreLib.JavaScript.WebGL;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Net
{
    [Script(Implements = typeof(global::System.Net.WebClient))]
    public class __WebClient
    {
        public WebHeaderCollection ResponseHeaders { get; set; }

        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\JavaScript\InternalWebMethodRequest.cs
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\WebClient.cs
        // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Net\WebClient.cs

        public event UploadValuesCompletedEventHandler UploadValuesCompleted;

        public void UploadValuesAsync(Uri address, NameValueCollection data)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140119

            var x = new IXMLHttpRequest();

            x.open(Shared.HTTPMethodEnum.POST, address.ToString(), async: true);
            x.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");


            var xx = ToFormDataString(data);

            //Uncaught InvalidStateError: Failed to execute 'send' on 'XMLHttpRequest': the object's state must be OPENED.
            // X:\jsc.svn\examples\javascript\Test\TestUploadValuesAsync\TestUploadValuesAsync\Application.cs

            // UploadValuesAsync { responseType = , response = <document><TaskComplete><TaskResult>13</TaskResult></TaskComplete></document> }

            x.InvokeOnComplete(
                delegate
                {
                    var response = new byte[0];

                    // UploadValuesAsync { status = 204, responseType = arraybuffer, response = [object Uint8ClampedArray] }

                    //if (x.status == 204)
                    // 304?
                    //if (x.status == IXMLHttpRequest.HTTPStatusCodes.NoContent)
                    //{
                    //    // android webview  wants us to do this
                    //    response = new byte[0];
                    //}

                    //Uncaught InvalidStateError: Failed to read the 'responseText' property from 'XMLHttpRequest': 
                    // The value is only accessible if the object's 'responseType' is '' or 'text' (was 'arraybuffer').

                    // X:\jsc.svn\examples\javascript\android\com.abstractatech.battery\com.abstractatech.battery\ApplicationWebService.cs
                    Console.WriteLine("UploadValuesAsync " + new { x.status, x.responseType });

                    //I/chromium(10616): [INFO:CONSOLE(36216)] "%c0:576ms UploadValuesAsync { status = 204, responseType = arraybuffer }", source: http://192.168.1.103:10129/view-source (36216)
                    //I/chromium(10616): [INFO:CONSOLE(49940)] "Uncaught InvalidStateError: An attempt was made to use an object that is not, or is no longer, usable.", source: http://192.168.1.103:10129/view-source (49940)

                    // what about android webview?
                    if (x.response == null)
                    {
                        //Console.WriteLine("UploadValuesAsync " + new { x.status, x.responseType, x.response, x.responseText });

                        //I/Web Console( 5012): %c0:198484ms UploadValuesAsync { status = 200, responseType = arraybuffer } at http://192.168.43.1:9417/view-source:37081
                        //I/Web Console( 5012): %c0:198500ms UploadValuesAsync { status = 200, responseType = arraybuffer, response = , responseText = <document><avatar><obj>aHR0cDovL3d3dy5ncmF2YXRhci5jb20vYXZhdGFyLzhlNmQzZGUw
                        //I/Web Console( 5012): %c0:198524ms InternalWebMethodRequest.Complete { Name = Gravatar, Length = 0 } at http://192.168.43.1:9417/view-source:37081

                        // did we not fix it already?
                        // android 2.3 only seems to have responseText

                        response = Encoding.UTF8.GetBytes(x.responseText);
                    }
                    else
                    {
                        // http://stackoverflow.com/questions/8022425/getting-blob-data-from-xhr-request

                        var a = (ArrayBuffer)x.response;

                        //Console.WriteLine("UploadValuesAsync " + new { x.status, x.responseType, a.byteLength });

                        // IE?
                        //var u8 = new Uint8Array(array: a);

                        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\DOM\FileEntryAsyncExtensions.cs
                        var u8c = new Uint8ClampedArray(array: a);

                        response = u8c;
                    }

                    //Console.WriteLine("UploadValuesAsync " + new { x.status, x.responseType, response });

                    var e = new __UploadValuesCompletedEventArgs { Result = response };

                    #region ResponseHeaders
                    this.ResponseHeaders = new WebHeaderCollection();
                    //this.ResponseHeaders.Clear();

                    var ResponseHeaders = x.getAllResponseHeaders();
                    //0:8209ms { ResponseHeaders = Date: Sat, 15 Mar 2014 12:25:45 GMT
                    //Server: ASP.NET Development Server/11.0.0.0
                    //X-AspNet-Version: 4.0.30319
                    //ETag: BqShORsRkdny750pWBdVyQ==
                    //X-ElapsedMilliseconds: 10
                    //Content-Type: text/xml; charset=utf-8
                    //Access-Control-Allow-Origin: *
                    //Cache-Control: private
                    //Connection: Close
                    //Content-Length: 239

                    foreach (var item in ResponseHeaders.Split('\n'))
                    {
                        var u = item.IndexOf(":");

                        var ukey = item.Substring(0, u);
                        var uvalue = item.Substring(u + 1).Trim();

                        this.ResponseHeaders[ukey] = uvalue;
                    }
                    #endregion

                    //Console.WriteLine(new { ResponseHeaders });


                    if (UploadValuesCompleted != null)
                        UploadValuesCompleted(null, (UploadValuesCompletedEventArgs)(object)e);

                }
               );

            x.responseType = "arraybuffer";
            x.send(xx);



        }

        public static string ToFormDataString(NameValueCollection data)
        {
            #region AllKeys
            var xx = "";

            foreach (var item in data.AllKeys)
            {
                if (xx != "")
                {
                    xx += "&";
                }

                var evalue = Native.window.escape(data[item]).Replace("+", "%" + ((byte)'+').ToString("x2"));
                xx += item + "=" + evalue;
            }


            #endregion
            return xx;
        }

        #region DownloadString
        public event DownloadStringCompletedEventHandler DownloadStringCompleted;

        [Obsolete("this will not work if the baseURI has changed and worker is using blob: workaround")]
        public string DownloadString(string address)
        {

            // X:\jsc.svn\examples\javascript\Test\TestDownloadStringAsync\TestDownloadStringAsync\Application.cs

            var x = new IXMLHttpRequest();

            x.open(Shared.HTTPMethodEnum.GET, address.ToString(), async: false);

            //Console.WriteLine("DownloadStringAsync");
            x.send();

            return x.responseText;
        }

        public void DownloadStringAsync(Uri address)
        {
            var x = new IXMLHttpRequest();

            x.open(Shared.HTTPMethodEnum.GET, address.ToString());

            x.InvokeOnComplete(
                r =>
                {
                    //var e = new __DownloadStringCompletedEventArgs { Error = new Exception("Not implemented. (__WebClient.DownloadStringAsync)") };
                    var e = new __DownloadStringCompletedEventArgs { Result = r.responseText };

                    if (DownloadStringCompleted != null)
                        DownloadStringCompleted(null, (DownloadStringCompletedEventArgs)(object)e);
                }
            );

            //Console.WriteLine("DownloadStringAsync");
            x.send();

        }

        public Task<string> DownloadStringTaskAsync(string address)
        {
            var z = new TaskCompletionSource<string>();

            var x = new IXMLHttpRequest();


            x.open(Shared.HTTPMethodEnum.GET, address.ToString());

            x.InvokeOnComplete(
                r =>
                {
                    z.SetResult(r.responseText);
                }
            );

            //Console.WriteLine("DownloadStringAsync");
            x.send();

            return z.Task;
        }
        #endregion

    }
}
