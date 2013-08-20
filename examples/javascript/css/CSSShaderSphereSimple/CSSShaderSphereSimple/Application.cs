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
using CSSShaderSphereSimple.Shaders;
using ScriptCoreLib.GLSL;

namespace CSSShaderSphereSimple
{

    public
        // jsc bug: typeof(static class) will not work 
        //static 
        class SphereRule
    {
        public static void InitializeSphereRuleFor(string className = "CLRForm")
        {

            //var frag = "assets/CSSShaderGrayScale/grayscale.frag";

            var frag = new global::CSSShaderSphereSimple.Shaders.sphere_simpleFragmentShader().ToDataUrl();
            var vert = new global::CSSShaderSphereSimple.Shaders.sphere_simpleVertexShader().ToDataUrl();

            {
                dynamic CLRForm_style = IStyleSheet.Default["." + className + ""].style;

                //new __sphere_simpleVertexShader { amount = 1, sphereRadius = 0.35f, lightPosition = new vec3(0, 0, 1f) };

                CLRForm_style.webkitFilter = "custom(url(" + vert + ") mix(url(" + frag + ") normal source-atop), 16 32, amount 1, sphereRadius 0.35, lightPosition 0.0 0.0 1.0 )";
                CLRForm_style.webkitTransition = "-webkit-filter ease-in-out 1s";
            }

            {
                dynamic CLRForm_hover_style = IStyleSheet.Default["." + className + ":hover"].style;

                //new __sphere_simpleVertexShader { amount = 0, sphereRadius = 0.35f, lightPosition = new vec3(0, 0, 1f) };

                CLRForm_hover_style.webkitFilter = "custom(url(" + vert + ") mix(url(" + frag + ") normal source-atop), 16 32, amount 0, sphereRadius 0.35, lightPosition 0.0 0.0 1.0 )";
                //CLRForm_hover_style.webkitTransition = "-webkit-filter ease-in-out 1s";
            }

           
        }
    }

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
            //FormStyler.AtFormCreated = FormStyler.LikeWindows3;

            // http://alteredqualia.com/css-shaders/sphere_simple.html

            SphereRule.InitializeSphereRuleFor("shader");

            Func<Form> q = delegate
            {
                var f =
                    //new Form1
                    new Form { Text = "CSS filter shader" };

                f.SizeTo(512, 512);
                f.Show();

                f.GetHTMLTarget().className = "shader";

                #region WhileDragging


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
                    Native.window.requestAnimationFrame += WhileDragging;
                };
                Native.window.requestAnimationFrame += WhileDragging;
                #endregion


                //var pp = new MandelbrotFormsControl.Library.MandelbrotComponent();
                //var pp = new XPlasma();
                //Native.Window.requestAnimationFrame += delegate
                //{
                //    f.Controls.Add(pp);

                //};


                return f;
            };

            q().MoveTo(32, 32);
            FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;
            q().MoveTo(96, 96);
            FormStyler.AtFormCreated = FormStyler.LikeWindows3;

            var pf = q();


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
