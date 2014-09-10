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
using UIAutomationEvents;
using UIAutomationEvents.Design;
using UIAutomationEvents.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.Native;
using System.Linq.Expressions;

namespace UIAutomationEvents
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        //public sealed class DOMEvent
        //{

        //}

        static Application that;
        public IApp page;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140517

            this.page = page;
            // historic callsite scoping does not yet support instance references
            that = this;

            page.email.ondblclick += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "email " + new { page.email.value }, IHTMLEvents.ondblclick));
            page.email.onclick += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "email " + new { page.email.value }, IHTMLEvents.onclick));
            page.email.onmouseover += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "email " + new { page.email.value }, IHTMLEvents.onmouseover));
            page.email.onmouseout += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "email " + new { page.email.value }, IHTMLEvents.onmouseout));
            page.email.onchange += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "email " + new { page.email.value }, IHTMLEvents.onchange));
            page.email.onfocus += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "email " + new { page.email.value }, IHTMLEvents.onfocus));
            page.email.onblur += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "email " + new { page.email.value }, IHTMLEvents.onblur));
            page.email.onkeyup += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "email " + new { page.email.value }, IHTMLEvents.onkeyup));

            page.EULA.onmouseover += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "EULA " + new { }, IHTMLEvents.onmouseover));
            page.EULA.onmouseout += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "EULA " + new { }, IHTMLEvents.onmouseout));
            page.EULA.onfocus += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "EULA " + new { }, IHTMLEvents.onfocus));
            page.EULA.onblur += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "EULA " + new { }, IHTMLEvents.onblur));
            page.EULA.onscroll += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "EULA " + new { }, IHTMLEvents.onscroll));

            window.onfocus += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "window " + new { }, IHTMLEvents.onfocus));
            window.onblur += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "window " + new { }, IHTMLEvents.onblur));
            window.onscroll += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "window " + new { }, IHTMLEvents.onscroll));
            //redux
            //window.ondeviceorientation += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "window " + new { }, IHTMLEvents.ondeviceorientation));
            window.onresize += e => Events.Add(new UIEvent(UIEventsOfInterestAndSignificance.DOM, "window " + new { }, IHTMLEvents.onresize));

            // ScriptCoreLib could also ask for expression which it could monitor a bit better
            // or we could use variable monitoring speed. slow down when no updates.
            // or we could ask to be notified if a field changes. jsc magic needed?
            new IHTMLPre { () => new { Events.Count, LastOrDefault = Events.LastOrDefault() } }.AttachToDocument();

            // also send in the performance data

            //:26ms enter Historic:
            //            {
            //                domain = 192.168.1.76, baseURI = http://192.168.1.76:10237/, location = http://192.168.1.76:10237/, xlocation = http://192.168.1.76:10237/, href = http://192.168.1.76:10237/agree }
            //0:26ms event: activate! { hash = , url =  }
            //0:27ms event: onclick
            //        {
            //            href = http://192.168.1.76:10237/agree, MouseButton = 1 }




            css.style.transition = "background-color 300ms linear";

            new IStyle(that.page.HideMe)
            {
                transition = "margin-top 300ms linear"
            };


            new IStyle(that.page.EULA.css)
            {
                borderLeft = "1em solid yellow",
                transition = "border-left 600ms linear",
            };

            new IStyle(that.page.EULA.css[that.page.EULA.async.onscrollToBottom])
            {
                borderLeft = "1em solid green"
            };


            //Native.window.async.onscrollToBottom
            //page.EULA.onscroll
            //page.email.async.onscr

            //page.EULA.onscroll +=
            //    delegate
            //{
            //    Console.WriteLine(new
            //    {
            //        page.EULA.scrollTop,
            //        page.EULA.scrollHeight,
            //        page.EULA.clientHeight
            //    });
            //};


            //page.EULA.async.onscrollToBottom.With(
            //    async onscrollToBottom =>
            //    {
            //        await onscrollToBottom;

            //        page.EULA.style.borderLeft = "1em solid green";
            //    }
            //);




            page.YesIAgree.Historic(
                 async (HistoryScope<object> scope) =>
            {
                // indicate we are sending data to server
                css.style.backgroundColor = "yellow";

                //new IStyle(that.page.HideMe)
                // we want it to be undone at back button!
                new IStyle(that.page.HideMe.css)
                {
                    marginTop = -that.page.HideMe.clientHeight + "px"
                };


                // X:\jsc.svn\examples\javascript\async\test\TestChainedAsync\TestChainedAsync\Application.cs


                // we are basically grouping it


                // why is it destroying rewrite?
                new UIEvent(UIEventsOfInterestAndSignificance.ClickAgree, NestedEvents: that.Events.ToArray()).With(
                    e =>
                {
                    that.Events.Clear();
                    that.Events.Add(e);
                }
                );



                var message = await that.Agree(that.page.email);
                // indicate got data
                css.style.backgroundColor = "cyan";


                var x = new IHTMLPre { new { message } }.AttachToDocument();
                that.Events.Add(
                    new UIEvent(UIEventsOfInterestAndSignificance.ClickAgreeComplete, Data: new { message }.ToString())
                );

                await scope;
                // indicate we went back in time
                // css will be undone bj scriptcorelib!

                // back button!
                x.Orphanize();
                that.Events.Add(
                    new UIEvent(UIEventsOfInterestAndSignificance.ClickAgreeHistoryGoBack)
                );


            }
            );


        }

    }


    [MonitoringDescription("tier split happens here. like pirates of carribean?")]
    public partial class ApplicationWebService : IDisposable
    {
        // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Ultra\Library\StringConversionsForStopwatch.cs
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\ILStringConversions.cs

        // what about events, properties and byref arguments?
        // what about stacktrace?
        // what about exceptions?
        // do we support expressions yet?
        // do we support list? we do support array and ienumerable?
        // basically this is our datasource

        // if it were readonly, would we still sync it?
        public List<UIEvent> Events = new List<UIEvent>();

        // can we databind to html elements?

        //      error at CopyType:
        //       * Method 'Dispose' in type 'UIAutomationEvents.Application' from assembly 'UIAutomationEvents.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.
        //       * UIAutomationEvents.Application 02000009
        //fix compiler to wait for UIAutomationEvents.Application 02000009
        //void IDisposable.Dispose()
        [Obsolete("cannot make it explicit interface yet?")]
        public void Dispose()
        {
            Console.WriteLine("enter Dispose");

        }

        //public Action<Expression<Action>> yield;


        // Error	7	Async methods cannot have ref or out parameters	X:\jsc.svn\examples\javascript\UIAutomationEvents\UIAutomationEvents\Application.cs	232	52	UIAutomationEvents

        [Obsolete("is it also called by History.GoForward?")]
        public async Task<string> Agree(string email = "?")
        {
            Console.WriteLine("enter agree " + new { email });

            // Events = Count = 0
            // 		Events	Cannot access a non-static member of outer type 'UIAutomationEvents.ApplicationWebService' via nested type 'UIAutomationEvents.ApplicationWebService.Agree'	


            var scope = new { this.Events };

            var rule =
                from e in Events.SelectMany(x => x.NestedEvents.Concat(new[] { x }))
                where e.Data.StartsWith("EULA")
                where e.DOMEvent == IHTMLEvents.onscroll
                select e;

            if (rule.Any())
                return "i think you did read our EULA. you can now close the window wait for our call.";

            // Error	6	An anonymous method expression cannot be converted to an expression tree	X:\jsc.svn\examples\javascript\UIAutomationEvents\UIAutomationEvents\Application.cs	195	17	UIAutomationEvents
            // Error	6	An expression tree may not contain an assignment operator	X:\jsc.svn\examples\javascript\UIAutomationEvents\UIAutomationEvents\Application.cs	196	23	UIAutomationEvents
            //yield(
            //    () => css.style.borderLeft = "1em solid red"
            //);


            return "did you read our eula? hit your back button";
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

    // http://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
    // http://stackoverflow.com/questions/1331739/enum-type-constraints-in-c-sharp
    // roslyn can we have it?
    //public sealed class UIEvent<T>(public T EventName, public string Data = "") where T : enum { }



    // http://msdn.microsoft.com/en-us/library/x810d419.aspx
    // [DebuggerTypeProxy(typeof(HashtableDebugView))]


    // db?
    public enum UIEventKey : long { }

    // would jsc be interested in preserving DebuggerDisplay so we could just output the type and not implementing ToString?
    [DebuggerDisplay("{EventName} {DOMEvent} {Data} {Timestamp}")]
    public sealed class UIEvent(
        public UIEventsOfInterestAndSignificance EventName,
        public string Data = "",
        public IHTMLEvents DOMEvent = default(IHTMLEvents),

        //, public XElement xml = null

        //public params IEnumerable<UIEvent> NestedEvents 
        public params UIEvent[] NestedEvents
        )
    {
        public DateTime Timestamp = DateTime.Now;

        //public UIEvent(IHTMLEvents DOMEvent) : this(DOMEvent)
        //{

        //}

        // LastOrDefault
        // {{ Count = 15, LastOrDefault = [object Object] }}

        public override string ToString()
        {
            // Error	2	Cannot implicitly convert type '<anonymous type: UIAutomationEvents.UIEventsOfInterestAndSignificance EventName, UIAutomationEvents.IHTMLEvents DOMEvent, string Data, System.DateTime Timestamp>' to 'string'	X:\jsc.svn\examples\javascript\UIAutomationEvents\UIAutomationEvents\Application.cs	147	20	UIAutomationEvents
            return new { this.EventName, this.DOMEvent, this.Data, this.Timestamp }.ToString();
        }
    }

}

