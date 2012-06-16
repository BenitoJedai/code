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
using TestHistoryAPI.Design;
using TestHistoryAPI.HTML.Pages;
using ScriptCoreLib.JavaScript.HistoryAPI;

namespace TestHistoryAPI
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
            // https://github.com/balupton/history.js/wiki/The-State-of-the-HTML5-History-API
            page.GODeeper.onclick +=
                delegate
                {
                    var xdata = new XElement("state1", "content1").ToString();

                    window.history.pushState(
                        data: xdata,
                        title: "New State 1",
                        url: "/dynamic/state1"
                    );

                    page.GODeeper.disabled = true;

                    window.alert("history length = " + window.history.length + "\n\n" + xdata);

                    window.onpopstate +=
                      e =>
                      {
                          if (e.state == null)
                          {
                              return;
                          }

                          var data = XElement.Parse((string)e.state);
                          if (data.Name.LocalName != "state1")
                              return;

                          var u = data.ToString();

                          window.alert("history length = " + window.history.length + "\n\n" + u);
                      };
                };

            window.onpopstate +=
               e =>
               {
                   if (e.state == null)
                   {
                       if (window.history.length == 1)
                       {
                           // clean browser start
                           return;
                       }


                   }
               };

            page.GOWider.onclick +=
                delegate
                {
                    var xdata = new XElement("state2", "content2").ToString();

                    window.history.pushState(
                        data: xdata,
                        title: "New State 2",
                        url: "/dynamic/state2"
                    );

                    page.GOWider.disabled = true;

                    window.alert("history length = " + window.history.length + "\n\n" + xdata);

                    window.onpopstate +=
                    e =>
                    {
                        if (e.state == null)
                        {

                            return;
                        }

                        var data = XElement.Parse((string)e.state);
                        if (data.Name.LocalName != "state2")
                            return;

                        var u = data.ToString();

                        window.alert("history length = " + window.history.length + "\n\n" + u );
                    };
                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        [Script(ExternalTarget = "window")]
        static XWindow window;
    }

    public static class X
    {
        public static T XCast<T>(this T template, object data)
        {
            return (T)data;
        }
    }

    [Script(HasNoPrototype = true)]
    class XWindow : IWindow
    {
        public History history;




        #region event
        public event System.Action<PopStateEvent> onpopstate
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "popstate");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "popstate");
            }
        }
        #endregion
    }
}
