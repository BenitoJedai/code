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
using AndroidContacts.Design;
using AndroidContacts.HTML.Pages;

namespace AndroidContacts
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
            @"Hello world".ToDocumentTitle();

            y = (
                    string id,
                    string name,
                    string email,
                    string gravatar
                    ) =>
                {

                    // we are replacing elements with implicit elements
                    var n = new ContactLayout { id = id, name = name, email = email };

                    n.gravatar.src = gravatar;

                    n.Container.AttachToDocument();

                };

            page.GetContacts.WhenClicked(
                async button =>
                {
                    await GetContacts();
                }
            );

        }

    }
}
