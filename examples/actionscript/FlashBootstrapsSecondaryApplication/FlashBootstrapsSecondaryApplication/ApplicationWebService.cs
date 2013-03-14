using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace FlashBootstrapsSecondaryApplication
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }





        public void Handler(WebServiceHandler h)
        {
            if (h.Context.Request.Path == "/Other")
            {

                h.Context.Response.ContentType = "text/html";


                h.Context.Response.Write(@"

<html><head>
<title>Application</title>
<link rel='icon' href='assets/ScriptCoreLib/jsc.ico' sizes='32x32 96x96' type='image/vnd.microsoft.icon'>
<link rel='icon' href='assets/ScriptCoreLib/jsc.png' sizes='96x96' type='image/png'>
<meta name='apple-mobile-web-app-capable' content='yes'>
<script>window['_GOOG_TRANS_EXT_VER'] = '1';</script></head>
<body style='overflow: hidden;'>
  <link rel='stylesheet' href='assets/FlashBootstrapsSecondaryApplication/App.css'>
  <div id='ContentSize' style='overflow: hidden;    position: absolute;    left: 0px;    right: 0px;    bottom: 0px;    top: 0px;'></div>
  <div id='Content' style='overflow: hidden;    position: absolute;    left: 0px;    right: 0px;    bottom: 0px;    top: 0px;'><embed type='application/x-shockwave-flash' id='__embed_355364087' name='__embed_355364087' allowfullscreeninteractive='true' allowfullscreen='true' allownetworking='all' allowscriptaccess='always' width='500' height='380' src='assets/FlashBootstrapsSecondaryApplication.Application/FlashBootstrapsSecondaryApplication.ApplicationSprite.swf' style='width: 1600px; height: 439px;'></div>
</body></html>

");

                h.CompleteRequest();


            }
        }
    }
}
