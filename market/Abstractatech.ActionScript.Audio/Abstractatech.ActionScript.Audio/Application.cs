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

            page.Play.onclick += delegate
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
                    var w = new IWindow();

                    w.onload +=
                        delegate
                        {
                            //BitConverter.ToSingle(
                            w.document.body.style.margin = "0px";

                            // verbose huh. svg::svg?
                            var svg = new ISVGSVGElement().AttachTo(w.document.body);

                            var path = new ISVGPathElement().AttachTo(svg);

                            path.setAttribute("style", "stroke: black; fill: none;");

                            var xw = new StringBuilder().Append("M10,200 ");

                            //Console.WriteLine(base64);

                            //var m = new MemoryStream(bytes);

                            Console.WriteLine("we have bytes" + new { bytes.Length });

                            // http://stackoverflow.com/questions/4414077/read-write-bytes-of-float-in-js
                            //var _buffer = new ArrayBuffer(bytes.Length);
                            var _bytes = new Uint8Array(bytes);

                            Console.WriteLine("we have Uint8Array" + new { _bytes.length });

                            try
                            {
                                var _floats = new Float32Array(_bytes.buffer, 0, (uint)(_bytes.length / 4));

                                Console.WriteLine("we have Float32Array " + new { _floats.length });


                                Func<int, float> ReadFloat32 = i =>
                                {
                                    // why isnt this an indexer?
                                    // Object #<Float32Array> has no method 'get_Item',

                                    var _floats_workaround = (float[])(object)_floats;
                                    return _floats_workaround[i];
                                };

                                var paddingmode = true;
                                var paddingsamples = 0;

                                var min = 0f;
                                var minset = false;

                                var max = 0f;
                                var maxset = false;

                                // done { min = 7.847271400218976e-44, max = 2.320612754833406e-38, paddingsamples = 1337 }

                                w.document.body.style.minHeight = 400 + "px";
                                w.document.body.style.minWidth = _floats.length * 4.0 + "px";
                                w.document.body.style.overflow = IStyle.OverflowEnum.auto;

                                // we should have 4096 stereo samples
                                var samples = _floats.length;
                                var samplesperchannel = samples / 2;

                                for (int ix = 0; ix < _floats.length; ix += 2)
                                {
                                    //                                    arg[0] is typeof System.Single
                                    //script: error JSC1000: No implementation found for this native method, please implement [static System.Console.WriteLine(System.Single)]

                                    var l0 = ReadFloat32(ix);
                                    var r0 = ReadFloat32(ix + 1);

                                    var iy = 200.0;

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

                                        //Console.WriteLine("" + l0);

                                        /** inverse max short value as float **/
                                        //var MAX_VALUE = 1.0f / float.MaxValue;
                                        // 3.40282e+038f

                                        // http://audio.tutsplus.com/articles/general/all-youll-ever-need-to-know-about-samples-and-bits/

                                        var l0reverse = l0 * float.MaxValue;

                                        //iy = (200.0 - l0 * 1E37 * 200);
                                        iy = (200.0 - l0reverse * 10.0);
                                    }

                                    xw.Append(" L" + (ix * 4.0) + "," + iy);


                                    //Console.WriteLine("" + ReadFloat32(i));
                                }

                                // A frame rate of 44,100 is 44,100 samples per SECOND, or 44.1 kHz.

                                var duration_seconds = samplesperchannel / 44100;

                                w.document.title = new { samplesperchannel, duration_seconds }.ToString();

                                Console.WriteLine("done " + new { min, max, paddingsamples });
                            }
                            catch (Exception error)
                            {
                                Console.WriteLine("error " + new { error.Message, error });
                            }



                            //xw.Append(" L30,210");

                            path.d = xw.ToString();

                        };

                };

            page.PlayJeep.onclick += delegate
            {
                sprite.PlayJeep();
            };


            page.VisualizeJeep.onclick += delegate
            {
                var w = new IWindow();

                w.onload +=
                    delegate
                    {
                        sprite.BytesForJeep(
                            base64 =>
                            {
                                var bytes = Convert.FromBase64String(base64);

                                visualize(bytes);
                            }
                        );
                    };
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
