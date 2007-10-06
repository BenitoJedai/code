using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace NatureBoy.js
{
    [Script]
    class Class2 : Class1
    {
        public const string Alias = "Class2";

        public Class2(IHTMLElement DataElement)
            : base(DataElement)
        {
            Action<int, int, double, IHTMLImage> Spawn =
                (x, y, z, i) =>
                {

                    //i.style.border = "1px solid red";

                    i.InvokeOnComplete(
                        delegate
                        {
                            i.style.SetLocation(x, y, Convert.ToInt32(i.width * z), Convert.ToInt32(i.height * z));
                            i.attachToDocument();
                        }
                    );

                };

            var stand = "assets/NatureBoy/dude2/stand/";

            var frames = new[] {
                stand + "TILE1406h.png",
                stand + "TILE1405.png",
                stand + "TILE1406.png",
                stand + "TILE1407.png",
                stand + "TILE1408.png",
                stand + "TILE1409.png",
                stand + "TILE1408h.png",
                stand + "TILE1407h.png"
            }.ToImages();

            var _x = 16;

            foreach (var v in frames)
            {
                Spawn(_x, 32, 1.5, v);

                _x += 100;
            }

            var dude1 = new Dude2
            {
                Frames = frames,
                VisualZoomFunc = y => (y + 300) / (600),
                Zoom = 2
            };



            //Spawn(32, 32, 2, stand + "TILE1405.png");
            //Spawn(100, 32, 2, stand + "TILE1406.png");
            //Spawn(200, 32, 2, stand + "TILE1407.png");
            //Spawn(300, 32, 2, stand + "TILE1408.png");
            //Spawn(400, 32, 2, stand + "TILE1409.png");
            //Spawn(500, 32, 2, stand + "TILE1408h.png");
            //Spawn(600, 32, 2, stand + "TILE1407h.png");
            //Spawn(700, 32, 2, stand + "TILE1406h.png");
        }

        static Class2()
        {
            ScriptCoreLib.JavaScript.Native.Spawn(
                Alias, e => new Class2(e)
                );
        }
    }
}
