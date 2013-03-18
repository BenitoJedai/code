using HerokuFacebookLogin.ActionScript.Design;
using HerokuFacebookLogin.ActionScript.HTML.Pages;
using HerokuFacebookLoginApp;
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

namespace HerokuFacebookLogin.ActionScript
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        //public readonly ApplicationWebService service = new ApplicationWebService();


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {



            //sprite.set_Login(HerokuFacebookLoginAppLoginExperience.Login);

            Action<string, string[]> embed_CallFunction = delegate { };


            #region __Login
            Action __Login = delegate
            {
                HerokuFacebookLoginAppLoginExperience.Login(
                    (string id, string name, string third_party_id) =>
                    {
                        embed_CallFunction("__Login_yield", new[] { id, name, third_party_id });
                    }
                );
            };

            IFunction.OfDelegate(__Login).Export("__Login");
            #endregion

            if (page.__marker_HerokuFacebookLoginActionScript == null)
            {
                // find me

                return;
            }

            ApplicationSprite sprite = new ApplicationSprite();

            sprite.AutoSizeSpriteTo(page.ContentSize);
            sprite.AttachSpriteTo(page.Content);

            embed_CallFunction =
                (method, args) =>
                {
                    var embed = (IHTMLEmbedFlash)sprite.ToHTMLElement();

                    embed.CallFunction(method, args);
                };

            //@"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
