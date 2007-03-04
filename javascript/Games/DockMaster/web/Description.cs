using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

[assembly:
ScriptResources("fx")
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
                    "fx/k6is.gif",
                    "fx/kala.gif",
                    "fx/kalar.gif",
                    "fx/kast.gif",
                    "fx/kraanakabiin.gif",
                    "fx/kraanax.gif",
                    "fx/kraanay.gif",
                    "fx/laev.gif",
                    "fx/magnet.gif",
                    "fx/meri.gif",
                    "fx/sadam.gif",
                    "fx/taust.jpg"
                };
            }
        }

        public void WaitUntilLoaded(Action e)
        {
            Timer.While(
                delegate
                {
                    bool r = false;

                    foreach (IHTMLImage v in Images)
                    {
                        if (!v.complete)
                        {
                            Console.WriteLine("not loaded: "  + v.src);

                            r = true;
                            break;
                        }
                    }

                    return r;
                }, e, 300);
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