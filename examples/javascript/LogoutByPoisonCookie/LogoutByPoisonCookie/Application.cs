using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LogoutByPoisonCookie;
using LogoutByPoisonCookie.Design;
using LogoutByPoisonCookie.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace LogoutByPoisonCookie
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly IApp page;
        public static Application that;
        public Cookie cookie = new Cookie("poison");
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            that = this;
            this.page = page;
            Native.window.localStorage["poison"] = "1";
            page.Login.Historic(
                async scope =>
                {
                    var button = new IHTMLButton { innerText = "Login" };

                    if (that.cookie.BooleanValue == false)
                    {
                        that.page.Login.style.borderLeft = "1em solid red";
                        //that.page.Login.innerText = "Home";

                        button.AttachToDocument();

                        button.onclick += delegate
                        {
                            that.page.Login.style.borderLeft = "1em solid green";
                            that.cookie.BooleanValue = true;
                            button.disabled = true;
                        };
                    }
                    else
                    {
                        that.page.Login.style.borderLeft = "1em solid green";

                    }
                    await scope;
                    button.Orphanize();
                    that.page.Login.style.borderLeft = "";
                
                }
                );
            page.View1.Historic(
             async scope =>
             {
                 if (that.cookie.BooleanValue == false)
                 {
                     that.page.View1.style.borderLeft = "1em solid red";
                 }
                 else
                 {
                     that.page.View1.style.borderLeft = "1em solid green";
                 }

                 await scope;

                 that.page.View1.style.borderLeft = "";
             }
             );
            page.View2.Historic(
             async scope =>
             {
                 if (that.cookie.BooleanValue == false)
                 {
                     that.page.View2.style.borderLeft = "1em solid red";
                 }
                 else
                 {
                     that.page.View2.style.borderLeft = "1em solid green";
                 }

                 await scope;

                 that.page.View2.style.borderLeft = "";
             }
             );
            page.Logout.Historic(
             async scope =>
             {
                 that.page.Logout.style.borderLeft = "1em solid red";
                 that.cookie.BooleanValue = false;


                 await scope;

                 that.page.Logout.style.borderLeft = "";
             }
             );
            
        }

    }
}
