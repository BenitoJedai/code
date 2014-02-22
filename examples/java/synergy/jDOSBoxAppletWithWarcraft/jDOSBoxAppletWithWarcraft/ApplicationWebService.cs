using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace jDOSBoxAppletWithWarcraft
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService
    {




        public void Handler(WebServiceHandler h)
        {
            var HostUri = new
            {
                Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                Port = h.Context.Request.Headers["Host"].SkipUntilOrEmpty(":")
            };

            if (HostUri.Port == "")
                HostUri = new { HostUri.Host, Port = "80" };

            Console.WriteLine(
                new { h.Context.Request.Path }
                );
            // http://isorecorder.alexfeinman.com/W7.htm
            // http://kbarr.net/bochs
            // http://www.imgburn.com/index.php?act=download
            if (h.Context.Request.Path == "/war1.img")
            {
                h.Context.Response.Redirect("/assets/jDOSBoxAppletWithWarcraft/war1.img");
                h.CompleteRequest();
                return;
            }

            if (h.Context.Request.Path == "/jDOSBoxAppletWithWarcraft.jnlp")
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140222

                h.Context.Response.ContentType = "application/x-java-jnlp-file";

                // X:\jsc.smokescreen.svn\core\javascript\com.abstractatech.analytics\com.abstractatech.analytics\ApplicationWebService.cs


                //#### Java Web Start Error:
                //#### Unable to load resource: file:/assets/jDOSBoxAppletWithWarcraft.Application/jDOSBoxAppletWithWarcraft.jnlp


                // http://en.wikipedia.org/wiki/Java_Web_Start
                h.Context.Response.Write(@"
<jnlp spec='1.4+' codebase='http://" + HostUri.Host + ":" + HostUri.Port + @"/' href='jDOSBoxAppletWithWarcraft.jnlp'
><information><title>jDOSBoxAppletWithWarcraft</title>
<vendor>Example vendor</vendor><description>Example long description</description>
<description kind='short'>Example short description</description></information>
<resources><j2se href='http://java.sun.com/products/autodl/j2se' version='1.4+' />
  <jar href='assets/jDOSBoxAppletWithWarcraft.Application/jDOSBoxAppletWithWarcraft.ApplicationApplet.jar'/>  

</resources>
<application-desc main-class='jDOSBoxAppletWithWarcraft.ApplicationApplet' />
<j2se version='1.4+' ava-vm-args='' /></jnlp>
");


                h.CompleteRequest();
                return;
            }


        }
    }
}
