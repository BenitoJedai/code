using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;
using System.Net;
using ScriptCoreLib.Shared.BCLImplementation.System.Net;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System.Collections.Specialized;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Net
{
    [Script(Implements = typeof(global::System.Net.WebClient))]
    internal class __WebClient : __Component
    {
        #region UploadValuesAsync
        public event UploadValuesCompletedEventHandler UploadValuesCompleted;

        public void UploadValuesAsync(Uri address, NameValueCollection data)
        {
            var request = new URLRequest(address.ToString())
            {
                method = URLRequestMethod.POST
            };

            var x = new DynamicContainer { Subject = new URLVariables() };

            foreach (var item in data.AllKeys)
            {
                x[item] = data[item];
            }

            // http://stackoverflow.com/questions/12774611/urlrequest-urlloader-auto-converting-post-request-to-get
            // !!!!
            request.data = (object)x.Subject;
            //request.contentType = ""

            var loader = new URLLoader();
            // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/net/URLLoaderDataFormat.html
            //loader.dataFormat = URLLoaderDataFormat.Binary;
            loader.dataFormat = "binary";

            loader.complete +=
                args =>
                {
                    // If the dataFormat property is URLLoaderDataFormat.BINARY, the received data is a ByteArray object containing the raw binary data.
                    var bytes = (ByteArray)loader.data;
                    var e = new __UploadValuesCompletedEventArgs { Result = (byte[])bytes.ToArray() };

                    if (UploadValuesCompleted != null)
                        UploadValuesCompleted(this, (UploadValuesCompletedEventArgs)(object)e);
                };

            loader.ioError +=
                args =>
                {
                    var e = new __UploadValuesCompletedEventArgs { Error = new Exception("ioError") };
                    if (UploadValuesCompleted != null)
                        UploadValuesCompleted(this, (UploadValuesCompletedEventArgs)(object)e);
                };


            loader.securityError +=
                args =>
                {
                    var e = new __DownloadStringCompletedEventArgs
                    {
                        Error = new Exception(
                            "securityError " + new { args.errorID, args.text }
                            )
                    };

                    if (UploadValuesCompleted != null)
                        UploadValuesCompleted(this, (UploadValuesCompletedEventArgs)(object)e);
                };

            loader.load(request);
        }
        #endregion

        #region DownloadStringAsync
        public event DownloadStringCompletedEventHandler DownloadStringCompleted;

        public void DownloadStringAsync(Uri address)
        {
            // testedby
            // X:\jsc.svn\examples\actionscript\Test\TestWebClient\TestWebClient\ApplicationSprite.cs

            var request = new URLRequest(address.ToString())
            {
                method = URLRequestMethod.GET
            };

            var loader = new URLLoader();
            loader.complete +=
                args =>
                {
                    var e = new __DownloadStringCompletedEventArgs { Result = "" + loader.data };

                    DownloadStringCompleted(this, (DownloadStringCompletedEventArgs)(object)e);
                };

            loader.ioError +=
                args =>
                {
                    var e = new __DownloadStringCompletedEventArgs { Error = new Exception("ioError") };
                    DownloadStringCompleted(this, (DownloadStringCompletedEventArgs)(object)e);
                };


            loader.securityError +=
                args =>
                {
                    var e = new __DownloadStringCompletedEventArgs
                    {
                        Error = new Exception(
                            "securityError " + new { args.errorID, args.text }
                            )
                    };
                    DownloadStringCompleted(this, (DownloadStringCompletedEventArgs)(object)e);
                };

            loader.load(request);
        }
        #endregion

    }
}
