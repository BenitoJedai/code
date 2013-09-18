using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ExperimentalPaletteCycler;
using ExperimentalPaletteCycler.Design;
using ExperimentalPaletteCycler.HTML.Pages;
using com.abstractatech.gamification.craft.HTML.Images.FromAssets;
using System.Diagnostics;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;
using System.Threading.Tasks;

namespace ExperimentalPaletteCycler
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new forest5().With(
                async img =>
                {

                    await img;

                    var st0 = new Stopwatch();
                    st0.Start();
                    var c0 = new CanvasRenderingContext2D(
                         img.width,
                         img.height
                     );


                    c0.drawImage(img, 0, 0, img.width, img.height);
                    var imageData = c0.getImageData(0, 0, img.width, img.height);

                    var AnimatedColors = new List<int> {
                        0x3C4C8C,
                        0x2C407C,
                        0x2C3C84,
                        0x20306C
                    };



                    #region f
                    Func<int[], Task<CanvasRenderingContext2D>> f =
                         AnimatedColorsTo =>
                         {

                             var c1 = new CanvasRenderingContext2D(
                                  img.width,
                                  img.height
                              );



                             var imageData1 = c1.getImageData(0, 0, img.width, img.height);

                             // This loop gets every pixels on the image and
                             for (var j = 0; j < imageData.width; j++)
                             {
                                 for (var ji = 0; ji < imageData.height; ji++)
                                 {
                                     var index = (uint)((ji * 4) * imageData.width + (j * 4));
                                     var red = imageData.data[index];
                                     var green = imageData.data[index + 1];
                                     var blue = imageData.data[index + 2];
                                     var alpha = imageData.data[index + 3];
                                     var average = (byte)((red + green + blue) / 3);

                                     var u8 = (red << 16) + (green << 8) + blue;
                                     var u8i = AnimatedColors.IndexOf(u8);

                                     if (u8i >= 0)
                                     {
                                         u8 = AnimatedColorsTo[u8i];

                                         red = (byte)((u8 >> 16) & 0xff);
                                         green = (byte)((u8 >> 8) & 0xff);
                                         blue = (byte)((u8 >> 0) & 0xff);
                                     }
                                     else
                                     {
                                         alpha = 0x00;
                                     }


                                     // 3C4C8C
                                     // 2C407C
                                     // 20306C

                                     imageData1.data[index] = red;
                                     imageData1.data[index + 1] = green;
                                     imageData1.data[index + 2] = blue;

                                     //imageData.data[index + 1] = average;
                                     //imageData.data[index + 2] = average;

                                     imageData1.data[index + 3] = alpha;
                                 }
                             }

                             // overwrite original image
                             c1.putImageData(imageData1, 0, 0, 0, 0, img.width, img.height);

                             var xx = new TaskCompletionSource<CanvasRenderingContext2D>();
                             xx.SetResult(c1);
                             return xx.Task;

                         };
                    #endregion

                    var st = new Stopwatch();
                    st.Start();

                    //{ ElapsedMilliseconds = 4795 } 

                    var a1 = new[]{
                            AnimatedColors[1],
                            AnimatedColors[2],
                            AnimatedColors[3],
                            AnimatedColors[0],
                        };
                    var o1 = f(a1).Result;


                    var a2 = new[]{
                                    AnimatedColors[2],
                                    AnimatedColors[3],
                                    AnimatedColors[0],
                                    AnimatedColors[1],
                                };
                    var o2 = f(a2).Result;

                    var a3 =
                       new[]{
                                            AnimatedColors[3],
                                            AnimatedColors[0],
                                            AnimatedColors[1],
                                            AnimatedColors[2],
                                        };
                    var o3 = f(a3).Result;

                    Console.WriteLine(new { st.ElapsedMilliseconds });

                    c0.canvas.AttachToDocument().style.SetLocation(0, 0);
                    o1.canvas.AttachToDocument().style.SetLocation(0, 0);
                    o2.canvas.AttachToDocument().style.SetLocation(0, 0);
                    o3.canvas.AttachToDocument().style.SetLocation(0, 0);

                    new Timer(
                        t =>
                        {
                            if (t.Counter % AnimatedColors.Count == 0)
                            {
                                o1.canvas.Hide();
                                o2.canvas.Hide();
                                o3.canvas.Hide();
                                return;
                            }

                            if (t.Counter % AnimatedColors.Count == 1)
                            {
                                o1.canvas.Show();
                                o2.canvas.Hide();
                                o3.canvas.Hide();
                                return;
                            }

                            if (t.Counter % AnimatedColors.Count == 2)
                            {
                                o1.canvas.Hide();
                                o2.canvas.Show();
                                o3.canvas.Hide();
                                return;
                            }

                            if (t.Counter % AnimatedColors.Count == 3)
                            {
                                o1.canvas.Hide();
                                o2.canvas.Hide();
                                o3.canvas.Show();
                                return;
                            }
                        }
                    ).StartInterval(1000 / 5);

                    // { ElapsedMilliseconds = 1843 } 
                    Console.WriteLine("all: " + new { st.ElapsedMilliseconds });
                }
            );
        }

    }
}
