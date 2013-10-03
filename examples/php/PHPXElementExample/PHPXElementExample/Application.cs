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
using PHPXElementExample.Design;
using PHPXElementExample.HTML.Pages;

namespace PHPXElementExample
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
        public Application(IDefault page)
        {
            #region Key_onchange
            Action Key_onchange = delegate
            {
                service.Key_onchange(page.Key.value, page.Value.value,
                        doc =>
                        {
                            page.Result.value = doc.ToString();
                        }
                    );
            };
            page.Key.onkeyup +=
                delegate
                {
                    Key_onchange();
                };

            page.Key.onchange +=
                delegate
                {
                    Key_onchange();
                };
            #endregion

            #region Value_onchange
            Action Value_onchange = delegate
            {
                service.Value_onchange(page.Key.value, page.Value.value,
                        doc =>
                        {
                            page.Result.value = doc.ToString();
                        }
                    );
            };
            page.Value.onkeyup +=
                delegate
                {
                    Value_onchange();
                };

            page.Value.onchange +=
                delegate
                {
                    Value_onchange();
                };
            #endregion


            #region Result_onchange
            Action Result_onchange =
                delegate
                {
                    service.Result_onchange(
                        page.Result.value,
                        (Key, Value) =>
                        {
                            page.Key.value = Key;
                            page.Value.value = Value;
                        }
                    );
                };

            page.Result.onchange +=
                delegate
                {
                    Result_onchange();
                };

            page.Result.onkeyup +=
                delegate
                {
                    Result_onchange();
                };
            #endregion

            page.Key.value = "foo";
            page.Value.value = "bar";

            Key_onchange();
        }

    }
}
