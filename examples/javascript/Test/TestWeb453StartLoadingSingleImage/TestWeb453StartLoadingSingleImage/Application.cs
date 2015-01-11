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
using TestWeb453StartLoadingSingleImage;
using TestWeb453StartLoadingSingleImage.Design;
using TestWeb453StartLoadingSingleImage.HTML.Pages;

namespace TestWeb453StartLoadingSingleImage
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
            var ImagesThatAreCurrentlyLoading = new List<IHTMLImage>();
            var ImagesThatAreCurrentlyLoadingCounter = 0;



            //          // Test453StartLoadingSingleImage.Tycoon4+<>c__DisplayClass0.<.ctor>b__2
            //          type$a5BarLoBUjK8R1JTOyKDbw.BAAABroBUjK8R1JTOyKDbw = function(b)
            //{
            //              var a = [this], c;

            //              c = a[0].ImagesThatAreCurrentlyLoadingCounter;
            //              a[0].ImagesThatAreCurrentlyLoadingCounter = (((c + 1)));
            //          };

            #region StartLoadingSingleImage
            // X:\jsc.svn\examples\javascript\Test\TestWeb453StartLoadingSingleImage\TestWeb453StartLoadingSingleImage\Application.cs
            // X:\jsc.svn\examples\javascript\IsometricTycoonViewWithToolbar\IsometricTycoonViewWithToolbar\Library\Tycoon4.cs
            // X:\jsc.svn\examples\javascript\Test\Test453StartLoadingSingleImage\Test453StartLoadingSingleImage\Class1.cs
            Action<IHTMLImage> StartLoadingSingleImage = Image =>
            {
                ImagesThatAreCurrentlyLoading.Add(Image);

                Image.InvokeOnComplete(img => { ImagesThatAreCurrentlyLoadingCounter++; }, 30);
                //LoadingSingleImageDone(Image);
            };
            #endregion
        }

    }
}
