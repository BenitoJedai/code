//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

//using global::System.Collections.Generic;



namespace GameOfLife.js
{


    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";


        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {

            IHTMLDiv Control = new IHTMLDiv();


            DataElement.insertNextSibling(
                Control

            );


            var vv = new ArenaControl();





            var cx = 32;
            var cy = 16;

            var w = 24;
            var h = 24;

            vv.SetCanvasSize(new Point(cx * w, cy * h));

            vv.SetLocation(new Rectangle { Left = 32, Top = 32, Width = 400, Height = 300 });

            vv.Control.attachToDocument();

            vv.Layers.Canvas.style.backgroundColor = Color.White;


            var data = new LayeredControl.CanvasRectangle[cx][];

            var p = new WorkPool();

            vv.Layers.Canvas.Hide();

            int index = 0;

            var State = new
                        {

                            ColorDeath = Color.White,
                            ColorSurvival = Color.Gray,
                            ColorBirth = Color.Black
                        };

            Func<Color> RandomState = () => Native.Math.random() > 0.5 ? State.ColorDeath : State.ColorBirth;


            for (int __i = 0; __i < cx; __i++)
            {
                var i = __i;

                data[i] = new LayeredControl.CanvasRectangle[cy];

                p.Add(
                    () =>
                    {

                        for (int j = 0; j < cy; j++)
                        {

                            var c = new LayeredControl.CanvasRectangle
                                    {
                                        BackgroundColor = RandomState(),
                                        Location = new Rectangle { Left = i * w + 1, Top = j * h + 1, Width = w - 2, Height = h - 2 }
                                    };

                            vv.DrawRectangleToCanvas(c);

                            data[i][j] = c;

                        }

                        index++;
                        Console.Log("index: " + index);
                    });
            }

            p.Error += Console.LogError;


            p.Add(
                () =>
                {
                    Console.Log("done!");

                    vv.Layers.Canvas.Show();


                }
                );


        }




        static Class1()
        {
            //Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
