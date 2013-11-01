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
using IntegrationToSkyIsland.Components;
using IntegrationToSkyIsland.HTML.Pages;

namespace IntegrationToSkyIsland
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        /*
         * Steps taken while creating this internal example:
         * 01. Find an interesting SWF to link to
         * 02. Add the swf to the project.
         * 03. Make sure this project builds.
         */
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            // Initialize MySprite1
            new MySprite1().AttachSpriteToDocument();
      
        }

    }
}
