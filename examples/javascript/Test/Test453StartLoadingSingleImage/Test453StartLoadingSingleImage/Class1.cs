using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test453StartLoadingSingleImage
{
    public class Tycoon4 : ScriptCoreLib.Shared.IAssemblyReferenceToken

    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150111/tycoon
        //static List<IHTMLImage> ImagesThatAreCurrentlyLoading = new List<IHTMLImage>();
        //static int ImagesThatAreCurrentlyLoadingCounter = 0;

        public Tycoon4()
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
