using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace com.abstractatech.ee.gov.verify
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {

        //arg[0] is typeof System.Drawing.ContentAlignment
        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.Label.set_TextAlign(System.Drawing.ContentAlignment)]
        //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
        //script: error JSC1000: error at PassportVerification.ApplicationControl.InitializeComponent,
        // assembly: V:\PassportVerification.Application.exe
        // type: PassportVerification.ApplicationControl, PassportVerification.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // offset: 0x029c
        //  method:Void InitializeComponent()

        public Task<string> dokumendi_kehtivuse_kontroll(string e)
        {
            if (string.IsNullOrEmpty(e))
                return Task.FromResult("");

            var c = new WebClient();
            var v = new NameValueCollection();
            var a = "http://www.politsei.ee/et/teenused/e-paringud/dokumendi-kehtivuse-kontroll/";

            var x = c.DownloadString(a);
            //  <input type="hidden" name="csrf" value="PRVXhlghqbVzeWMqD5f3m+2iK/N6w0VilWKClE2DozCAo/KdBk0N7g==" />

            // http://www.enterpriseframework.com/post/2011/10/10/XElement-Xml-nbsp3b-parse-exception.aspx
            // Additional information: Reference to undeclared entity 'nbsp'. Line 1534, position 33.
            //var xml = XElement.Parse(x);
            //var csrf = xml.XPathEvaluate("//*[@name='csrf']");



            var csrf = x.SkipUntilOrEmpty("<input type=\"hidden\" name=\"csrf\" value=\"").TakeUntilOrEmpty("\"");

            //cmd:request
            //csrf:PRVXhlghqbVc8ZI7y9R5BnsMNh4sCwnklWKClE2DozCAo/KdBk0N7g==
            //docNumber:FOO
            //subButton:Esita päring

            v["cmd"] = "request";
            v["csrf"] = csrf;
            v["docNumber"] = e.ToUpper();
            v["subButton"] = "Esita päring";

            var r = c.UploadValues(a, v);
            var rr = Encoding.UTF8.GetString(r);
            var rx = rr
                .SkipUntilOrEmpty("<form name=\"reqForm\" method=\"post\" action=\"\">")
                .SkipUntilOrEmpty("<br/>")
                .TakeUntilOrEmpty("</div>")
                .Trim();


            // Send it back to the caller.
            return Task.FromResult(rx);
        }


    }


}
