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
using PNGEncoderExperiment;
using PNGEncoderExperiment.Design;
using PNGEncoderExperiment.HTML.Pages;
using Plupload.PngEncoder;

namespace PNGEncoderExperiment
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
            // jsc able to take a random open source project
            // to encode a png file on the client, worker?
            // the code seems to be pre compiling.
            // how to use it?


            //Error   9   Unsafe code may only appear if compiling with /unsafe   X:\jsc.svn\examples\javascript\io\synergy\PNGEncoderExperiment\PNGEncoderExperiment\opensource\FJCore\Image.cs  147 13  PNGEncoderExperiment
            //Error   1   'FluxJpeg.Core.HuffmanTable' does not contain a definition for 'Decode' and no extension method 'Decode' accepting a first argument of type 'FluxJpeg.Core.HuffmanTable' could be found(are you missing a using directive or an assembly reference ?)	X:\jsc.svn\examples\javascript\io\synergy\PNGEncoderExperiment\PNGEncoderExperiment\opensource\FJCore\Decoder\JpegComponent.cs  468 29  PNGEncoderExperiment

            // X:\jsc.svn\examples\javascript\io\synergy\PNGEncoderExperiment\PNGEncoderExperiment\opensource\FJCore\Image.cs
            // 147

            // is jsc ready for unsafe byte opcodes yet?
            // be have byref, async, why not also support pointers then?
            // #if SILVERLIGHT


            //0200002b Plupload.PngEncoder.PngEncoder
            //script: error JSC1000: Missing Script Attribute? Native constructor was invoked, please implement[System.IO.MemoryStream..ctor(System.Int32)]


            //    0200002b Plupload.PngEncoder.PngEncoder
            //    script: error JSC1000: opcode unsupported - [0x01d7] bne.un + 0 - 2{[0x01ce]
            //rem
            //  script: error JSC1000: error at Plupload.PngEncoder.PngEncoder.WriteImageData,

            // seems to be return from within try.
            // could the rewriter fix it for us?

            // X:\jsc.svn\examples\javascript\Test\TestReturnFromWithinTry\TestReturnFromWithinTry\Application.cs
            // or we fix this source for now?

            var w = 128;
            var h = 128;


            // black pixels?
            var data = new int[w * h];

            var p = new PngEncoder(

                data,
                w,
                h,
                true,
                PngEncoder.FILTER_NONE,
                0
            );

 //           script: error JSC1000: unsupported flow detected, try to simplify.
 //Assembly U:\PNGEncoderExperiment.Application.exe
 //DeclaringType Plupload.PngEncoder.DeflaterHuffman + Tree, PNGEncoderExperiment.Application,
 //OwnerMethod BuildTree
 //            Offset 030d

            
            var bytes = p.pngEncode();

            new IHTMLPre { new { bytes } }.AttachToDocument();

        }

    }
}
