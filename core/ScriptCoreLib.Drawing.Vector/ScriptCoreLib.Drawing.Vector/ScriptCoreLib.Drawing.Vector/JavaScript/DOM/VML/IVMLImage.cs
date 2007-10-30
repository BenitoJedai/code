using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.VML
{
    [Script(InternalConstructor = true)]
    public class IVMLImage : IVMLElementBase
    {
        public string src;

        public IVMLImage()
        {
        }

        public static IVMLImage InternalConstructor()
        {
            return (IVMLImage)new IVMLElementBase("image");
        }
    }
}
