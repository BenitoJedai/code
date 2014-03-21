using FormsPictureboxFlipTransition;
using FormsPictureboxFlipTransition.Design;
using FormsPictureboxFlipTransition.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FormsPictureboxFlipTransition
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new IStyle(Native.css[typeof(ApplicationControl)])
            {
                perspective = "2000"
            };

            new IStyle(Native.css[typeof(Panel)])
            {
                transform = "rotateY(180deg)",
                backfaceVisibility = "hidden"
            };
            new IStyle(Native.css[typeof(PictureBox)])
            {
                backfaceVisibility = "hidden"
            };

            new IStyle(Native.css[typeof(GroupBox)].hover)
            {
                transform = "rotateY(180deg)"
            };

            new IStyle(Native.css[typeof(GroupBox)])
            {
                transformStyle = "preserve-3d",
                transition = "0.6s",
            };

            //new IStyle(Native.css[typeof(PictureBox)])
            //{
            //    perspective = "1000",
            //    transformStyle = "preserve-3d",
            //    transition = "0.6s",
            //    backfaceVisibility = "hidden"
            //};

            




            

            content.AttachControlToDocument();

        }

    }
}
