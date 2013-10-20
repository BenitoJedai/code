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
using CSSShaderGrayScale.Design;
using CSSShaderGrayScale.HTML.Pages;
using System.Windows.Forms;
using CSSShaderGrayScale.Shaders;

namespace CSSShaderGrayScale
{
    public
        // jsc bug: typeof(static class) will not work 
        //static 
        class GrayScaleRule
    {
        public static void InitializeGrayScaleFor(string className = "CLRForm")
        {

            //var frag = "assets/CSSShaderGrayScale/grayscale.frag";

            var url = new global::CSSShaderGrayScale.Shaders.grayscaleFragmentShader().ToDataUrl();

            {

                var CLRForm = IStyleSheet.Default["." + className + ""];
                dynamic CLRForm_style = CLRForm.style;

                // new __grayscaleFragmentShader { amount = 1 };
                var webkitFilter = "custom(none mix(url(" + url + ") normal source-atop), amount 1)";


                CLRForm_style.webkitFilter = webkitFilter;
                CLRForm_style.webkitTransition = "-webkit-filter linear 0.2s";
            }

            {
                var CLRForm_hover = IStyleSheet.Default["." + className].hover;
                dynamic CLRForm_hover_style = CLRForm_hover.style;

                // new __grayscaleFragmentShader { amount = 0 };
                var webkitFilter = "custom(none mix(url(" + url + ") normal source-atop), amount 0)"; ;
                CLRForm_hover_style.webkitFilter = webkitFilter;
                CLRForm_hover_style.webkitTransition = "-webkit-filter linear 0.2s";
            }

            {
                var CLRForm = IStyleSheet.Default["." + className + "_nohover"];
                dynamic CLRForm_style = CLRForm.style;

                // new __grayscaleFragmentShader { amount = 0.5 };
                var webkitFilter = "custom(none mix(url(" + url + ") normal source-atop), amount 0.5)";


                CLRForm_style.webkitFilter = webkitFilter;
                CLRForm_style.webkitTransition = "-webkit-filter linear 0.2s";
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


            GrayScaleRule.InitializeGrayScaleFor("CLRForm");


            #region AddCLRForm
            Func<Form> AddCLRForm = delegate
            {
                var f = new Form();

                f.GetHTMLTarget().className = "CLRForm";

                f.Text = "CSS filter shader";

                #region WhileDragging
                Native.window.requestAnimationFrame +=
                    delegate
                    {
                        if (f.Capture)
                        {
                            f.GetHTMLTarget().className = "";
                            f.Text = "CSS filter shader (dragging)";
                        }
                        else
                        {
                            f.GetHTMLTarget().className = "CLRForm";
                            f.Text = "CSS filter shader";

                        }
                    };
                #endregion

                var i = new WebBrowser
                {
                    //Url = new Uri("/jsc"), 

                    Dock = DockStyle.Fill
                };

                i.AttachTo(f);

                f.Show();

                i.Navigate("/jsc");

                return f;
            };
            #endregion


            AddCLRForm().MoveBy(0, 0);

            FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;

            AddCLRForm().MoveBy(32, 16);

            FormStyler.AtFormCreated =
                s =>
                {

                    FormStyler.LikeVisualStudioMetro(s);

                    s.TargetOuterBorder.style.borderColor = ScriptCoreLib.JavaScript.Runtime.JSColor.Red;
                    s.Caption.style.backgroundColor = ScriptCoreLib.JavaScript.Runtime.JSColor.Red;
                    s.TargetOuterBorder.style.boxShadow = "rgba(255, 0, 0, 0.3) 0px 0px 6px 3px";
                };

            AddCLRForm().MoveBy(64, 32);

            //f.GotFocus +=
            //    delegate
            //    {
            //        f.Text = "GotFocus";
            //    };

            //f.LostFocus +=
            //  delegate
            //  {
            //      f.Text = "LostFocus";
            //  };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
