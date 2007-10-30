using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.VML
{
    [Script(InternalConstructor = true)]
    public class IVMLGroup : IVMLElementBase
    {
        public IVMLGroup()
        {
        }

        public static IVMLGroup InternalConstructor()
        {
            return (IVMLGroup) new IVMLElementBase("group");
        }
    }
}
