using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;
using System.Net;
using ScriptCoreLib.Shared.BCLImplementation.System.Net;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Net
{
    [Script(Implements = typeof(global::System.Net.WebClient))]
    internal class __WebClient : __Component
    {
        public event DownloadStringCompletedEventHandler DownloadStringCompleted;

        public void DownloadStringAsync(Uri address)
        {
            // testedby
            // X:\jsc.svn\examples\actionscript\Test\TestWebClient\TestWebClient\ApplicationSprite.cs

            var request = new URLRequest(address.ToString());
            request.method = URLRequestMethod.GET;

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
    }
}
