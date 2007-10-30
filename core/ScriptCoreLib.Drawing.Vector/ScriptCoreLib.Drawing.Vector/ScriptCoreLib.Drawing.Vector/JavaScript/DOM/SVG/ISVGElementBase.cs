using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
    [Script(InternalConstructor = true)]
    public class ISVGElementBase : IHTMLElement
    {
        [Script]
        public class Settings
        {
            public static readonly string MimeType = "image/svg+xml";

            static bool _IsSupported;

            public static bool IsSupported
            {
                get
                {
                    var p = Native.Window.navigator.mimeTypes;

                    for (int i = 0; i < p.length; i++)
                    {
                        if (p[i].type == MimeType)
                        {
                            _IsSupported = true;

                            break;
                        }

                        //Console.WriteLine("mime: " + p[i].type + " : " + p[i].description);
                    }

                    return _IsSupported;
                }
            }
        }

        public static readonly string NS = "http://www.w3.org/2000/svg";

        public string type;
        public ISVGElementBase ownerSVGElement;
        public ISVGElementBase viewportSVGElement;

        protected ISVGElementBase()
        {
        }

        protected static ISVGElementBase InternalConstructor()
        {
            throw new Exception();
        }

        public ISVGElementBase(string tag)
        {
        }

        internal static ISVGElementBase InternalConstructor(string tag)
        {

            return (ISVGElementBase)(object) //null;
                Native.Document.createElementNS(NS, tag);




        }

    }

}
