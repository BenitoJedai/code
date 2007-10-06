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

        public Class2(IHTMLElement DataElement) : base(DataElement)
        {
            Action<int, int, double, string> Spawn =
                (x, y, z, src) =>
                {
                    var i = new IHTMLImage(src);

                    //i.style.border = "1px solid red";

                    i.InvokeOnComplete(
                        delegate
                        {
                            i.style.SetLocation(x, y, Convert.ToInt32( i.width * z), Convert.ToInt32( i.height * z));
                            i.attachToDocument();
                        }
                    );
                    
                };

            var stand = "assets/NatureBoy/dude2/stand/";

            Spawn(32, 32, 2, stand + "TILE1405.png");
            Spawn(100, 32, 2, stand + "TILE1406.png");
            Spawn(200, 32, 2, stand + "TILE1407.png");
            Spawn(300, 32, 2, stand + "TILE1408.png");
            Spawn(400, 32, 2, stand + "TILE1409.png");
            Spawn(500, 32, 2, stand + "TILE1408h.png");
            Spawn(600, 32, 2, stand + "TILE1407h.png");
            Spawn(700, 32, 2, stand + "TILE1406h.png");
        }

        static Class2()
        {
            ScriptCoreLib.JavaScript.Native.Spawn(
                Alias, e => new Class2(e)
                );
        }
    }
}
