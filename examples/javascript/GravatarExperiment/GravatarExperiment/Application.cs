using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using GravatarExperiment.Design;
using GravatarExperiment.HTML.Pages;

namespace GravatarExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //arg[0] is typeof System.Char[]
            //no matching prototype
            //script: error JSC1000: error at MD5.MD5.get_Value,

            page.CheckForGravatarInBrowser.onclick +=
                delegate
                {
                    var a = new MD5.MD5();

                    //a.FingerPrint
                    a.ValueAsByte = Encoding.UTF8.GetBytes(page.email.value.ToLower());


                    //var hash = Encoding.UTF8.GetBytes(e.ToLower()).ToMD5Bytes().ToHexString();
                    var hash = a.FingerPrint.ToLower();

                    new IHTMLPre { new { hash } }.AttachToDocument();

                    page.avatar.src = ("http://www.gravatar.com/avatar/" + hash);
                    page.profile.href = ("http://www.gravatar.com/" + hash);
                };

            page.CheckForGravatar.onclick +=
                delegate
                {
                    this.Gravatar(page.email.value,
                        avatar: src =>
                        {
                            page.avatar.src = src;
                        },

                        profile: href =>
                        {
                            page.profile.href = href;
                        }
                    );
                };
        }

    }
}
