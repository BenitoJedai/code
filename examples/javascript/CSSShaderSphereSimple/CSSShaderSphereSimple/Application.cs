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
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CSSShaderSphereSimple.Design;
using CSSShaderSphereSimple.HTML.Pages;
using System.Windows.Forms;
using CSSShaderSphereSimple.Library;

namespace CSSShaderSphereSimple
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;

            // http://alteredqualia.com/css-shaders/sphere_simple.html

            var fs = "assets/CSSShaderSphereSimple/sphere_simple.frag";
            var vs = "assets/CSSShaderSphereSimple/sphere_simple.vert";
            //var fs = new Design.Shaders.sphere_simpleFragmentShader().ToString();
            //var vs = new Design.Shaders.sphere_simpleVertexShader().ToString();

            var f =
                new Form1 
                //new Form 
                { Text = "CSS filter shader" };

            f.SizeTo(512, 512);
            f.Show();

            #region WhileDragging
            f.GetHTMLTarget().className = "shader";


            Action WhileDragging = null;

            WhileDragging = delegate
            {
                if (f.Capture)
                {
                    f.GetHTMLTarget().className = "";
                    f.Text = "CSS filter shader (dragging)";
                }
                else
                {
                    f.GetHTMLTarget().className = "shader";
                    f.Text = "CSS filter shader";

                }
                Native.Window.requestAnimationFrame += WhileDragging;
            };
            Native.Window.requestAnimationFrame += WhileDragging;
            #endregion

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
