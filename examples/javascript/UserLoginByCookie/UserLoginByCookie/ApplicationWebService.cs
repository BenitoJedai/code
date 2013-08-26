using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace UserLoginByCookie
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
            // http://samy.pl/evercookie/
            // http://yro.slashdot.org/story/13/08/25/1521233/cookieless-web-tracking-using-https-etag

            //Hey, a new visitor to the website? I wonder who he is?
            //Well, I'll just drop a cookie on there to keep track of him... and, hmm, it seems he's blocking cookies.
            //Oh well, let me just insert this bit of Javascript; that'll work just as well.
            //Dear oh dear, it seems Javascript isn't working.
            //No worries, I'll just insert a little 0-byte web-bug graphic and... wait? That's prevented as well?
            //Damn it, Flash-cookie! That'll get him! WHAT?!?!? Disabled as well?
            //E-Tag! That has to work, right?
            //ARGH!!!!!

            //Gee... I wonder if he's trying to tell me something like, oh I don't know, "I don't like being tracked".

            //Nah, who doesn't like being pushed, filed, stamped, indexed, briefed, debriefed, or numbered? I wonder if there's some other way I can use...




            if (h.Context.Request.Path == "/Other")
            {
                var Other = h.Applications[1];

                h.Context.Response.ContentType = "text/html";

                var xml = XElement.Parse(Other.PageSource);


                //xml.Element("h3").Value = h.Context.Request.UserAgent;

                var PasswordOK = false;


                h.Context.Request.Cookies["Password"].With(
                    Password => PasswordOK = Password.Value == "mypassword"
                );

                if (PasswordOK)
                    xml.Element("body").Element("h3").ReplaceAll(
                        new XElement("code",
                            new XAttribute("style", "color:green;"),
                            "Hello! You are now logged in!"
                        )
                    );
                else
                    xml.Element("body").Element("h3").ReplaceAll(
                        new XElement("code",
                            new XAttribute("style", "color:red;"),
                            "Not logged in. Please try again!"
                        )
                    );

                //h.Context.Response.Write(
                //          "<script type='text/xml' class='" + Other.TypeName + "'></script>"
                //      );


                foreach (var r in Other.References)
                {
                    xml.Add(
                        new XElement("script",
                            new XAttribute("src", r.AssemblyFile + ".js"),
                            ""
                        )
                    );

                    //h.Context.Response.Write(
                    //    "<script src='" + r.AssemblyFile + ".js'></script>"
                    //);
                }

                h.Context.Response.Write(xml.ToString());

                h.CompleteRequest();


            }
        }
    }
}
