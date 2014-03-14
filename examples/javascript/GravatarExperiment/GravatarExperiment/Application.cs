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
using ScriptCoreLib.Ultra.Library.Extensions;

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
                    //script: error JSC1000: No implementation found for this native method, please implement [System.Security.Cryptography.HashAlgorithm.ComputeHash(System.Byte[])]
                    //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
                    //script: error JSC1000: error at ScriptCoreLib.Ultra.Library.Extensions.CryptographyExtensions.ToMD5Bytes,
                    // assembly: V:\GravatarExperiment.Application.exe

                    //var a = new MD5.MD5();

                    ////a.FingerPrint
                    //a.ValueAsByte = Encoding.UTF8.GetBytes(page.email.value.ToLower());

                    var e = page.email.value;

                    var hash = Encoding.UTF8.GetBytes(e.ToLower()).ToMD5Bytes().ToHexString();
                    //var hash = a.FingerPrint.ToLower();

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
                            new IHTMLPre { new { src } }.AttachToDocument();

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
