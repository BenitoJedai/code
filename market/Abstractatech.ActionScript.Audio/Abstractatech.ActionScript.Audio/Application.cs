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
    public sealed class Application : ApplicationWebService
    {
        // script: error JSC1000: No implementation found for this native method, please implement [System.IO.BinaryReader.ReadSingle()]

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            // Initialize ApplicationSprite
            sprite.AttachSpriteToDocument();


            // how did it work before?
#if v

            #region visualize
            Action<bool, byte[], Action<double, double, Action<double>, IWindow>> visualize_and_getpadding = null;

            visualize_and_getpadding =
                (allowpadding, bytes, set_padding) =>
                {
                    var r = new BinaryReader(new MemoryStream(bytes));

                    var floats = new double[bytes.Length / 4];

                    //Console.WriteLine("floats " + new { floats.Length });


                    for (int i = 0; i < floats.Length; i++)
                    {
                        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\IO\BinaryReader.cs
                        // do we do floats?
                        floats[i] = r.ReadSingle();
                    }

                    var paddingmode_yellow = allowpadding;
                    var paddingsamples_yellow = 0;
                    var paddingmode_yellow_agg = 0.0;
                    var paddingmode_yellow_grace = 411;

                    var paddingmode_red = allowpadding;
                    var paddingsamples_red = 0;
                    var paddingmode_red_agg = 0.0;
                    var paddingmode_red_grace = 411;


            #region max
                    var min = 0.0;
                    var minset = false;

                    var max = 0.0;
                    var maxset = false;


                    for (int ix = 0; ix < floats.Length; ix += 2)
                    {
                        //                                    arg[0] is typeof System.Single
                        //script: error JSC1000: No implementation found for this native method, please implement [static System.Console.WriteLine(System.Single)]

                        var l0 = floats[ix];
                        var r0 = floats[ix + 1];

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
                    }

                    var absmax = max.Max(Math.Abs(min));

            #endregion


            #region paddingmode_yellow
                    for (int ix = 0; ix < floats.Length; ix += 2)
                    {
                        //                                    arg[0] is typeof System.Single
                        //script: error JSC1000: No implementation found for this native method, please implement [static System.Console.WriteLine(System.Single)]

                        var l0 = floats[ix];
                        var r0 = floats[ix + 1];




                        if (paddingmode_yellow)
                        {
                            // discard noise
                            if (Math.Abs(l0) > 0.08 * absmax)
                                paddingmode_yellow_agg += Math.Abs(l0);
                        }

                        if (paddingmode_yellow_agg > absmax * 2.1)
                        {
                            if (Math.Abs(l0) < 0.02 * absmax)
                            {
                                paddingmode_yellow = false;
                            }
                        }

                        if (paddingmode_yellow)
                        {
                            paddingsamples_yellow++;

                            if (paddingmode_yellow_agg > absmax * 3.2)
                            {
                                if (paddingmode_yellow_grace > 0)
                                {
                                    paddingmode_yellow_grace--;
                                }
                                else
                                {
                                    // rollback
                                    paddingsamples_yellow -= 411;
                                    paddingmode_yellow = false;
                                }
                            }
                        }

                    }
            #endregion

                    // count down while near zero, then wait for zero

            #region paddingmode_red
                    for (int ix = floats.Length - 1; ix >= 0; ix -= 2)
                    {
                        var l0 = floats[ix];
                        var r0 = floats[ix + 1];


                        if (paddingmode_red)
                        {
                            // discard noise
                            if (Math.Abs(l0) > 0.08 * absmax)
                                paddingmode_red_agg += Math.Abs(l0);
                        }

                        if (paddingmode_red_agg > absmax * 2.1)
                        {
                            if (Math.Abs(l0) < 0.02 * absmax)
                            {
                                paddingmode_red = false;
                            }
                        }

                        if (paddingmode_red)
                        {
                            paddingsamples_red++;

                            if (paddingmode_red_agg > absmax * 3.2)
                            {
                                if (paddingmode_red_grace > 0)
                                {
                                    paddingmode_red_grace--;
                                }
                                else
                                {
                                    // rollback
                                    paddingsamples_red -= 411;
                                    paddingmode_red = false;
                                }
                            }
                        }

                    }
            #endregion




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


                                var path_current = new ISVGPathElement().AttachTo(svg);
                                path_current.setAttribute("style", "stroke-width: 5; stroke: blue; fill: none;");


                                var path = new ISVGPathElement().AttachTo(svg);
                                path.setAttribute("style", "stroke: black; fill: none;");

                                var path_loop2 = new ISVGPathElement().AttachTo(svg);
                                path_loop2.setAttribute("style", "stroke: green; fill: none;");


                                var xw = new StringBuilder().Append("M0,400 ");






                                // done { min = 7.847271400218976e-44, max = 2.320612754833406e-38, paddingsamples = 1337 }

                                w.document.body.style.minHeight = 800 + "px";

                                var scalex = 4 / 44.1;

                                w.document.body.style.minWidth = floats.Length * scalex * 2 + "px";
                                w.document.body.style.overflow = IStyle.OverflowEnum.auto;

                                // we should have 4096 stereo samples
                                var samples = floats.Length;
                                var samplesperchannel = samples / 2;






            #region xw
                                for (int ix = 0; ix < floats.Length; ix += 2)
                                {
                                    //                                    arg[0] is typeof System.Single
                                    //script: error JSC1000: No implementation found for this native method, please implement [static System.Console.WriteLine(System.Single)]

                                    var l0 = floats[ix];
                                    var r0 = floats[ix + 1];








                                    // 0 is -60db
                                    // max is 0db



                                    // http://audio.tutsplus.com/articles/general/all-youll-ever-need-to-know-about-samples-and-bits/


                                    //iy = (200.0 - l0 * 1E37 * 200);
                                    var iy = (400.0 - l0 * 400.0);

                                    xw.Append(" L" + (ix * scalex) + "," + iy);


                                    //Console.WriteLine("" + ReadFloat32(i));
                                }
            #endregion

            #region xw_loop2
                                var xw_loop2 = new StringBuilder();

                                for (int ix = paddingsamples_yellow * 2; ix < floats.Length - paddingsamples_red * 2; ix += 2)
                                {
                                    //                                    arg[0] is typeof System.Single
                                    //script: error JSC1000: No implementation found for this native method, please implement [static System.Console.WriteLine(System.Single)]

                                    var l0 = floats[ix];
                                    var r0 = floats[ix + 1];

                                    var iy = (400.0 - l0 * 400.0);

                                    if (xw_loop2.ToString().Length == 0)
                                        xw_loop2.Append(" M" + ((2 * (samplesperchannel - paddingsamples_red - paddingsamples_yellow) * scalex) + ((ix + 1) * scalex)) + "," + iy);
                                    else
                                        xw_loop2.Append(" L" + ((2 * (samplesperchannel - paddingsamples_red - paddingsamples_yellow) * scalex) + ((ix + 1) * scalex)) + "," + iy);

                                }
            #endregion


                                // A frame rate of 44,100 is 44,100 samples per SECOND, or 44.1 kHz.

                                var duration_seconds = samplesperchannel / 44100;

                                w.document.title = new { samplesperchannel, paddingsamples_yellow, paddingsamples_red, duration_seconds }.ToString();

                                Console.WriteLine("done " + new { min, max, paddingsamples_yellow, paddingsamples_red });



                                //xw.Append(" L30,210");

                                path.d = xw.ToString();
                                path_loop2.d = xw_loop2.ToString();

                                var path_zero = new ISVGPathElement().AttachTo(svg);
                                path_zero.setAttribute("style", "stroke-width: 1; stroke: gray; fill: none;");
                                path_zero.d = "M0,400 L" + (2 * samplesperchannel * scalex) + ",400";



                                //var path_leftpadding = new ISVGPathElement().AttachTo(svg);
                                //path_leftpadding.setAttribute("style", "stroke-width: 5; stroke: red; fill: none;");
                                //path_leftpadding.d = "M0,400 L" + (2 * paddingsamples * scalex) + ",400";


                                var path_leftpadding_yellow = new ISVGPathElement().AttachTo(svg);
                                path_leftpadding_yellow.setAttribute("style", "stroke-width: 3; stroke: yellow; fill: none;");
                                path_leftpadding_yellow.d = "M0,400 L" + (2 * paddingsamples_yellow * scalex) + ",400";

                                var path_leftpadding_red = new ISVGPathElement().AttachTo(svg);
                                path_leftpadding_red.setAttribute("style", "stroke-width: 3; stroke: red; fill: none;");
                                path_leftpadding_red.d = "M" + (2 * (samplesperchannel - paddingsamples_red) * scalex) + ",400 L" + (2 * (samplesperchannel - 0) * scalex) + ",400";


                                Action<double> set_position =
                                     position =>
                                     {
                                         var x = (2 * position * scalex);
                                         path_current.d = "M" + x + ",50 L" + x + ",750 L" + (x + MP3PitchLoop.BLOCK_SIZE * 2 * scalex) + ",750 L" + (x + MP3PitchLoop.BLOCK_SIZE * 2 * scalex) + ",50 L" + x + ",50";

                                     };


                                set_padding(
                                    paddingsamples_yellow,
                                    paddingsamples_red,
                                    set_position,
                                    w
                                );

                                set_position(paddingsamples_yellow);

                            }
                            catch (Exception error)
                            {
                                Console.WriteLine("error " + new { error.Message, error });
                            }

                            Console.WriteLine("done");

                        };

                };
            #endregion



      

            #region f
            Func<IHTMLButton, Action<string, PlayAtAndAllowToStop>> f =
                x =>
                     (base64, playat) =>
                     {
                         var bytes = Convert.FromBase64String(base64);

                         visualize_and_getpadding(
                             true,
                             bytes,
                             (paddingleft, paddingright, set_position, w) =>
                             {
                                 var toolbar = new IHTMLDiv().AttachTo(w.document.body);

                                 toolbar.style.SetLocation(4, 4);
                                 toolbar.style.position = IStyle.PositionEnum.@fixed;

                                 new IHTMLButton { innerText = "play" }.AttachTo(toolbar).WhenClicked(
                                     delegate
                                     {
                                         playat(
                                             "" + paddingleft,
                                             "" + paddingright,
                                             yield_stop: stop =>
                                              {

                                                  var stopbtn = new IHTMLButton { innerText = "Stop" };

                                                  stopbtn.WhenClicked(
                                                      delegate
                                                      {
                                                          stop();
                                                          stopbtn.Orphanize();
                                                      }
                                                  );

                                                  stopbtn.AttachTo(toolbar);
                                              },
                                              yield_position_anddiagnostics: (position, diagnostics) =>
                                              {
                                                  set_position(Convert.ToDouble(position));

                                                  if (!string.IsNullOrEmpty(diagnostics))
                                                  {
                                                      Console.WriteLine(diagnostics);

                                                      var diagnostics_bytes = Convert.FromBase64String(diagnostics);

                                                      visualize_and_getpadding(
                                                          false,
                                                          diagnostics_bytes,
                                                          delegate
                                                          { }
                                                      );

                                                  }
                                              }


                                         );
                                     }
                                 );


                             }
                         );
                     };
            #endregion


            page.VisualizeDiesel.WhenClicked(
                delegate
                {
                    sprite.BytesForDiesel(f(page.VisualizeDiesel));
                }
            );

            page.VisualizeHelicopter.onclick += delegate
            {
                sprite.BytesForHelicopter(f(page.VisualizeHelicopter));
            };

            page.VisualizeJeep.onclick += delegate
            {
                sprite.BytesForJeep(f(page.VisualizeJeep));

            };

            page.VisualizeTone.onclick += delegate
            {

                sprite.BytesForTone(f(page.VisualizeTone));

            };

            page.VisualizeSandrun.onclick += delegate
            {

                sprite.BytesForSandrun(f(page.VisualizeSandrun));

            };
#endif

            page.PlayDiesel.onclick += delegate
            {
                sprite.PlayDiesel();
            };

            page.PlayHelicopter.onclick += delegate
            {
                sprite.Playhelicopter1();
            };



            page.PlayJeep.onclick += delegate
            {
                sprite.PlayJeep();
            };






            page.PlayTone.onclick += delegate
            {
                sprite.PlayTone();
            };




            page.PlaySandrun.onclick += delegate
            {
                sprite.PlaySandrun();
            };



        }

    }
}
