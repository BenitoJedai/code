using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestChainedAsync;
using TestChainedAsync.Design;
using TestChainedAsync.HTML.Pages;

namespace TestChainedAsync
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static Application that;
        public IApp page;

        public Application(IApp page)
        {
            this.page = page;
            // historic callsite scoping does not yet support instance references
            that = this;



            // X:\jsc.svn\examples\javascript\UIAutomationEvents\UIAutomationEvents\Application.cs

            page.YesIAgree.Historic(
                async scope =>
                {
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140526

                    new UIEvent(UIEventsOfInterestAndSignificance.ClickAgree, NestedEvents: that.Events.ToArray()).With(
                         e =>
                            {
                                that.Events.Clear();
                                that.Events.Add(e);
                            }
                     );

                    var message = await that.Agree(that.page.email);
                    var x = new IHTMLPre { new { message } }.AttachToDocument();
                    await scope;
                    x.Orphanize();

                }
            );
        }

    }




    public enum UIEventsOfInterestAndSignificance
    {
        ClickAgree,
        ClickAgreeComplete,
        ClickAgreeHistoryGoBack,
        DOM
    };

    public enum IHTMLEvents
    {
        none,

        // hascode based? crc?
        ondeviceorientation,

        onmouseover,
        onmouseout,

        onchange,
        onfocus,
        onblur,
        onscroll,
        onkeyup,
        onresize,
        onclick,
        ondblclick,
    };

    public sealed class UIEvent(
    public UIEventsOfInterestAndSignificance EventName,
    public string Data = "",
    public IHTMLEvents DOMEvent = default(IHTMLEvents),

        //, public XElement xml = null

        //public params IEnumerable<UIEvent> NestedEvents 
        public params UIEvent[] NestedEvents
    )
    { }
}
