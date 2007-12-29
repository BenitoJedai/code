using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System;

[assembly:
ScriptResources("assets/DockMaster")
]


namespace DockMaster.source.js.gfx
{
    [Script]
    public class ImageResources
    {

        public IHTMLImage[] Images
        {
            get
            {
                return new IHTMLImage[] {
                    "assets/DockMaster/k6is.gif",
                    "assets/DockMaster/kala.gif",
                    "assets/DockMaster/kalar.gif",
                    "assets/DockMaster/kast.gif",
                    "assets/DockMaster/kraanakabiin.gif",
                    "assets/DockMaster/kraanax.gif",
                    "assets/DockMaster/kraanay.gif",
                    "assets/DockMaster/laev.gif",
                    "assets/DockMaster/magnet.gif",
                    "assets/DockMaster/meri.gif",
                    "assets/DockMaster/sadam.gif",
                    "assets/DockMaster/taust.jpg"
                };
            }
        }

        public void WaitUntilLoaded(Action e)
        {
            new Timer(
                t =>
                {
                    bool r = false;

                    foreach (IHTMLImage v in Images)
                    {
                        if (!v.complete)
                        {
                            System.Console.WriteLine("not loaded: "  + v.src);

                            r = true;
                            break;
                        }
                    }

                    
                    if (!r)
                    {
                        t.Stop();

                    e();
                    }
                    
                }, 0, 300);
        }

        static ImageResources _Default;

        public static ImageResources Default
        {
            get
            {
                if (_Default == null)
                    _Default = new ImageResources();

                return _Default;
            }
        }
    }
}