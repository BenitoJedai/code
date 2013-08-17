using ScriptCoreLib;
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
using SpaceInvadersTemplate.Design;
using SpaceInvadersTemplate.HTML.Pages;

namespace SpaceInvadersTemplate
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // port from: Z:\jsc.svn\javascript\Games\SpaceInvaders\source\js\Controls\SpaceInvaders.cs

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault  page)
        {
            new SpaceInvadersTemplate.Library.Controls.SpaceInvaders();

            //style.Content.AttachToHead();
            @"Space Invaders".ToDocumentTitle();
     
        }

    }
}
