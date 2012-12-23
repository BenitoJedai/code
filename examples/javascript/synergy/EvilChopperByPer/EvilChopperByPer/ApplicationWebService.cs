using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace EvilChopperByPer
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
            //Console.WriteLine(
            //    h.Context.Request.Path
            //);

            //  The following information can be helpful to determine why the assembly 'ScriptCoreLib.Ultra, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null' could not be loaded.
            var ref0 = typeof(ScriptCoreLib.Shared.RemotingToken);


            if (h.Context.Request.Path == "/levels/level1.json")
            {
                //var src = XElement.Parse(
                //    //new EvilChopperByPer.HTML.Pages.Levels.XMLSourceSource().Text

                //).Elements("script").ElementAt(0).Attribute("src").Value;

                //h.Context.Response.Redirect("/" + src);
                h.Context.Response.ContentType = "application/json";
                //h.Context.Response.WriteFile("/" + src);
                h.Context.Response.WriteFile("/assets/EvilChopperByPer/" + h.Context.Request.Path.SkipUntilOrEmpty("/levels/").TakeUntilIfAny(".") + ".js");
                h.CompleteRequest();
                return;
            }

            if (h.Context.Request.Path == "/levels/level2.json")
            {
                //var src = XElement.Parse(
                //    new EvilChopperByPer.HTML.Pages.Levels.XMLSourceSource().Text
                //).Elements("script").ElementAt(1).Attribute("src").Value;

                ////h.Context.Response.Redirect("/" + src);
                h.Context.Response.ContentType = "application/json";
                //h.Context.Response.WriteFile("/" + src);
                h.Context.Response.WriteFile("/assets/EvilChopperByPer/" + h.Context.Request.Path.SkipUntilOrEmpty("/levels/").TakeUntilIfAny(".") + ".js");
                h.CompleteRequest();
                return;
            }

            if (h.Context.Request.Path == "/levels/level3.json")
            {
                //var src = XElement.Parse(
                //    new EvilChopperByPer.HTML.Pages.Levels.XMLSourceSource().Text
                //).Elements("script").ElementAt(2).Attribute("src").Value;

                //h.Context.Response.Redirect("/" + src);

                h.Context.Response.ContentType = "application/json";
                h.Context.Response.WriteFile("/assets/EvilChopperByPer/" + h.Context.Request.Path.SkipUntilOrEmpty("/levels/").TakeUntilIfAny(".") + ".js");
                h.CompleteRequest();
                return;
            }

            if (h.Context.Request.Path == "/swf/soundmanager2_flash9_debug.swf")
            {
                h.Context.Response.ContentType = "application/swf";

                var src = "assets/EvilChopperByPer.Application/EvilChopperByPer.Library.SoundManager2.swf";

                h.Context.Response.WriteFile("/" + src);


                h.CompleteRequest();
            }

            if (h.Context.Request.Path.StartsWith("/sounds/"))
            {
               // var src = XElement.Parse(
               //    new EvilChopperByPer.HTML.Pages.SoundsSounds.XMLSourceSource().Text
               //).Elements("audio").FirstOrDefault(
               //     k => k.Attribute("src").Value.SkipUntilLastIfAny("/") == h.Context.Request.Path.SkipUntilOrEmpty("/sounds/")
               //);

                // jsc is not correctly updating the path of assets?
                h.Context.Response.WriteFile("/assets/EvilChopperByPer/" + h.Context.Request.Path.SkipUntilOrEmpty("/sounds/"));
                h.CompleteRequest();
                return;
            }
        }
    }
}
