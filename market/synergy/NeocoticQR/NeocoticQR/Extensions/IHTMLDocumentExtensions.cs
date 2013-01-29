using NeocoticQR.Design;
using NeocoticQR.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    public static class IHTMLImageExtensions
    {
        // consumers of this extension method might not event know they need to load additional js
        public static IHTMLImage ToQRCode(this IHTMLDocument value)
        {
            return (value.location.href).ToQRCode();
        }

        public static IHTMLImage ToQRCode(this string value)
        {
            // what if the js file and the top level name are the same?
            // the <script> reference can be hidden once automatic loading is there.
            // the namespace currently is wrong.
            return qr.image(
                new QRCodeImageArguments
                {
                    level = "H",
                    size = 4,
                    value = value
                }
            );
        }
    }
}
