using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Drawing;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestNestedDelegate.Design;
using TestNestedDelegate.HTML.Pages;

namespace TestNestedDelegate
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public Application(IDefaultPage page)
        {
            Action AllImagesLoaded = 
                delegate
                {
                    var data = new[] {  -0.5  };

                    var query = from a in data
                                select   a ;

                    foreach (var v in query)
                    {
                        var __hint8 = "before iteration data0";

                        var data0 = data.Where(i => true);
                     

                        var __hint11 = "after iteration";

                    }

                };

        }

    }
}
