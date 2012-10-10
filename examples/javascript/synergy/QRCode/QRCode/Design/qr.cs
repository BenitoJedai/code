using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib;

namespace QRCodeTemplate.Design
{
    [Description("Future versions of JSC will enable seamless integration with JavaScript libraries")]
    public class __qr : qr
    {
        [Script(ExternalTarget = "qr")]
        static public ____qr qr;
    }


    [Script(HasNoPrototype = true, ExternalTarget = "qr")]
    public class ____qr
    {
        internal ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage image(QRCodeImageArguments qRCodeImageArguments)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class QRCodeImageArguments
    {
        public string level;
        public int size;
        public string value;
    }
}
