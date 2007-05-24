using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.Windows.Forms
{

    [Script]
    public static class Extensions
    {
        static public IHTMLElement GetHTMLTarget(this Control e)
        {
            __Control x = e;

            return x.HTMLTargetRef;
        }



    }
}
