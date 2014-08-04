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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestShadowFlipForForm;
using TestShadowFlipForForm.Design;
using TestShadowFlipForForm.HTML.Pages;
using System.Windows.Forms;

namespace TestShadowFlipForForm
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
            // X:\jsc.svn\examples\javascript\DynamicStylePerspective\DynamicStylePerspective\Application.cs
            // X:\jsc.svn\examples\javascript\css\Test\TestPerspectiveInShadow\TestPerspectiveInShadow\Application.cs
            // x:\jsc.svn\examples\javascript\css\test\testperspectiveinshadow\testperspectiveinshadow\design\shadowlayout.htm


            // forms has some effects defined.
            // for special css3d effects we want shadow dom
            // but lets not used shadowdom from forms just yet as it removes other browsers
            var f = new Form();

            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeNexus7\ChromeNexus7\Application.cs

            // this seems to cause <webview> to render only white. cannot used together for now?
            f.GetHTMLTarget().shadow.With(
                async shadow =>
                {
                    var s = new ShadowLayoutManual().AttachTo(shadow);

                    s.content.setAttribute("state", "animateout");

                    //var s = new ShadowLayout().AttachTo(shadow);

                    // shadow content needs to be boxed to the same size the element thinks
                    // it has!
                    s.content.style.SetSize(f.Width, f.Height);

                    f.SizeChanged +=
                        delegate
                    {
                        s.content.style.SetSize(f.Width, f.Height);
                    };

                    await Native.window.async.onframe;

                    s.content.setAttribute("state", "animatein");
                }
            );

            f.Show();



        }

    }
}
