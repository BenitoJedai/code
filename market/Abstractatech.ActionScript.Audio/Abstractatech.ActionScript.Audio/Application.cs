using Abstractatech.ActionScript.Audio.Design;
using Abstractatech.ActionScript.Audio.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.SVG;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Abstractatech.ActionScript.Audio
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // Initialize ApplicationSprite
            sprite.AttachSpriteTo(page.Content);

            page.PlayDiesel.onclick += delegate
            {
                sprite.PlayDiesel();
            };


            page.PlayHelicopter.onclick += delegate
            {
                sprite.Playhelicopter1();
            };


            Action<byte[]> visualize =
                bytes =>
                {
                    var r = new BinaryReader(new MemoryStream(bytes));

                    var floats = new double[bytes.Length / 4];

                    Console.WriteLine("floats " + new { floats.Length });


                    for (int i = 0; i < floats.Length; i++)
                    {
                        floats[i] = r.ReadSingle();
                    }

                    //try
                    //{
                    //    Console.WriteLine("XLomontFFT");

                    //    new Lomont.XLomontFFT().FFT(floats);
                    //}
                    //catch (Exception error)
                    //{
                    //    Console.WriteLine("error " + new { error.Message, error });
                    //}


                    var w = new IWindow();

                    w.onload +=
                        delegate
                        {
                            Console.WriteLine("onload");

                            try
                            {

                                //BitConverter.ToSingle(
                                w.document.body.style.margin = "0px";

                                // verbose huh. svg::svg?
                                var svg = new ISVGSVGElement().AttachTo(w.document.body);

                                var path = new ISVGPathElement().AttachTo(svg);

                                path.setAttribute("style", "stroke: black; fill: none;");

                                var xw = new StringBuilder().Append("M0,400 ");



                                var paddingmode = true;
                                var paddingsamples = 0;

                                var min = 0.0;
                                var minset = false;

                                var max = 0.0;
                                var maxset = false;

                                // done { min = 7.847271400218976e-44, max = 2.320612754833406e-38, paddingsamples = 1337 }

                                w.document.body.style.minHeight = 800 + "px";

                                var scalex = 4.0 / 44.1;

                                w.document.body.style.minWidth = floats.Length * scalex + "px";
                                w.document.body.style.overflow = IStyle.OverflowEnum.auto;

                                // we should have 4096 stereo samples
                                var samples = floats.Length;
                                var samplesperchannel = samples / 2;

                                for (int ix = 0; ix < floats.Length; ix += 2)
                                {
                                    //                                    arg[0] is typeof System.Single
                                    //script: error JSC1000: No implementation found for this native method, please implement [static System.Console.WriteLine(System.Single)]

                                    var l0 = floats[ix];
                                    var r0 = floats[ix + 1];

                                    var iy = 400.0;

                                    if (l0 != 0)
                                    {
                                        paddingmode = false;
                                    }

                                    if (paddingmode)
                                    {
                                        paddingsamples++;
                                    }
                                    else
                                    {
                                        // 0 is -60db
                                        // max is 0db

                                        if (l0 != 0)
                                            if (minset)
                                            {
                                                min = Math.Min(min, l0);
                                            }
                                            else
                                            {
                                                min = l0;
                                                minset = true;
                                            }

                                        if (maxset)
                                        {
                                            max = Math.Max(max, l0);
                                        }
                                        else
                                        {
                                            max = l0;
                                            maxset = true;
                                        }


                                        // http://audio.tutsplus.com/articles/general/all-youll-ever-need-to-know-about-samples-and-bits/


                                        //iy = (200.0 - l0 * 1E37 * 200);
                                        iy = (400.0 - l0 * 400.0);
                                    }

                                    xw.Append(" L" + (ix * scalex) + "," + iy);


                                    //Console.WriteLine("" + ReadFloat32(i));
                                }

                                // A frame rate of 44,100 is 44,100 samples per SECOND, or 44.1 kHz.

                                var duration_seconds = samplesperchannel / 44100;

                                w.document.title = new { samplesperchannel, paddingsamples, duration_seconds }.ToString();

                                Console.WriteLine("done " + new { min, max, paddingsamples });



                                //xw.Append(" L30,210");

                                path.d = xw.ToString();

                            }
                            catch (Exception error)
                            {
                                Console.WriteLine("error " + new { error.Message, error });
                            }

                            Console.WriteLine("done");

                        };

                };

            page.PlayJeep.onclick += delegate
            {
                sprite.PlayJeep();
            };


            page.VisualizeJeep.onclick += delegate
            {
                sprite.BytesForJeep(
                    base64 =>
                    {
                        var bytes = Convert.FromBase64String(base64);

                        visualize(bytes);
                    }
                );
            };



            page.PlayTone.onclick += delegate
            {
                sprite.PlayTone();
            };


            page.VisualizeTone.onclick += delegate
            {

                sprite.BytesForTone(
                    base64 =>
                    {
                        var bytes = Convert.FromBase64String(base64);

                        visualize(bytes);
                    }
                );

            };
        }

    }
}
