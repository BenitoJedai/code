using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using QRCode.Design;

namespace QRCode.Library
{
    public static class QRCodeExtensions
    {
        public static IHTMLImage ToQRCode(this string value)
        {
            return __qr.qr.image(
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
