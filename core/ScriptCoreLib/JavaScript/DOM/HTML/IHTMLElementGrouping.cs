using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ScriptCoreLib.JavaScript.DOM.SVG;


namespace ScriptCoreLib.JavaScript.DOM.HTML
{


    [Script]
    [Obsolete("experimental")]
    public sealed class IHTMLElementGrouping
    {
        // ?
        public IHTMLElementGrouping()
        {

        }

        public IHTMLElement contextElement;
        public IHTMLElement.HTMLElementEnum selectorByNodeName;

        // X:\jsc.svn\examples\javascript\VirtualElementEvents\VirtualElementEvents\Application.cs
        // like .async we want to expose the grouping also.

        public CSSStyleRuleMonkier css
        {
            get
            {
                // haha. recursion error:
                //return contextElement[selectorByNodeName].css;

                return contextElement.css[selectorByNodeName];
            }
        }

        // what about .async for grouping ?

        public event Action<IEvent> onclick
        {
            add
            {
                var selectorByNodeNameString = "" + selectorByNodeName;

                this.contextElement.onclick +=
                    e =>
                    {
                        var lhs = e.Element.nodeName.ToLower();
                        var rhs = selectorByNodeNameString.ToLower();

                        //Console.WriteLine("onclick " + new { lhs, rhs });

                        if (lhs != rhs)
                            return;

                        value(e);
                    };
            }
            remove { }
        }


        public event Action<IEvent> oncontextmenu
        {
            add
            {
                var selectorByNodeNameString = "" + selectorByNodeName;

                this.contextElement.oncontextmenu +=
                    e =>
                    {
                        var lhs = e.Element.nodeName.ToLower();
                        var rhs = selectorByNodeNameString.ToLower();

                        //Console.WriteLine("onclick " + new { lhs, rhs });

                        if (lhs != rhs)
                            return;

                        value(e);
                    };
            }
            remove { }
        }


        // X:\jsc.svn\examples\javascript\WebGL\HeatZeekerRTS\HeatZeekerRTS\Application.cs

        public event Action<IEvent> onmouseover
        {
            add
            {
                var selectorByNodeNameString = "" + selectorByNodeName;

                this.contextElement.onmouseover +=
                    e =>
                    {
                        var lhs = e.Element.nodeName.ToLower();
                        var rhs = selectorByNodeNameString.ToLower();

                        //Console.WriteLine("onclick " + new { lhs, rhs });

                        if (lhs != rhs)
                            return;

                        value(e);
                    };
            }
            remove { }
        }



        public event Action<IEvent> onmouseout
        {
            add
            {
                var selectorByNodeNameString = "" + selectorByNodeName;

                this.contextElement.onmouseout +=
                    e =>
                    {
                        var lhs = e.Element.nodeName.ToLower();
                        var rhs = selectorByNodeNameString.ToLower();

                        //Console.WriteLine("onclick " + new { lhs, rhs });

                        if (lhs != rhs)
                            return;

                        value(e);
                    };
            }
            remove { }
        }

    }
}
