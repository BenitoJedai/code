using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AndroidCalendarWebActivity.Design;
using AndroidCalendarWebActivity.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace AndroidCalendarWebActivity
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            @"Hello world".ToDocumentTitle();

            Action<int, Action> GetEventText =
                (e, y) =>
                {
                    page.EventTitle.style.color = JSColor.Red;

                    var ee = System.Convert.ToString(e);
                    Console.WriteLine(new { ee, e });
                    service.GetEventText(
                        ee,

                        (Location, EventText) =>
                        {
                            page.Location.innerText = Location;
                            page.EventTitle.innerText = EventText;
                            page.EventTitle.style.color = JSColor.None;

                            if (y != null)
                                y();
                        }
                    );
                };

            var reverse_position = 0;

            GetEventText(reverse_position, null);

            page.Prev.onclick +=
                delegate
                {
                    reverse_position++;

                    page.Prev.style.color = JSColor.Red;
                    GetEventText(reverse_position,
                        () => page.Prev.style.color = JSColor.Blue);
                };

            page.Next.onclick +=
             delegate
             {
                 if (reverse_position > 0)
                     reverse_position--;

                 page.Next.style.color = JSColor.Red;
                 GetEventText(reverse_position,
                     () => page.Next.style.color = JSColor.Blue);
             };

            page.CreateEvent.onclick +=
                delegate
                {
                    page.CreateEvent.style.color = JSColor.Red;
                    service.CreateEvent(
                        page.CreateEventTitle.value,
                        page.CreateEventLocation.value,
                        page.CreateEventDescription.value,
                        (e) => page.CreateEvent.style.color = JSColor.Blue);
                };
        }



    }
}
