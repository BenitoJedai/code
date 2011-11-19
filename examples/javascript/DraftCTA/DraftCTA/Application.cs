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
using DraftCTA.HTML.Pages;

namespace DraftCTA
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            Action UpdateHistory =
                delegate
                {
                    page.History.value = "";

                    page.History.value = "loading...";

                    // Send data from JavaScript to the server tier
                    service.WebMethod2(
                        @"A string from JavaScript.",
                        value => page.History.value = value + "\n---\n" + page.History.value
                    );

                    page.History.value = ":)" + "\n---\n" + page.History.value;
                };

            #region CTA to landingpage form
            page.CTA1.onclick +=
                delegate
                {
                    page.Page1Container.Hide();

                    page.CTA1Content.disabled = false;
                    page.CTA1Submit.disabled = false;

                    page.Page2Container.Show();
                };

            page.CTA2.onclick +=
                delegate
                {
                    page.Page1Container.Hide();
                    page.Page3Container.Show();
                };
            #endregion

            #region landingpage submit
            page.CTA1Submit.onclick +=
                delegate
                {
                    page.CTA1Content.disabled = true;
                    page.CTA1Submit.disabled = true;
                    service.SendCTAContent(page.CTA1Content.value,
                        delegate
                        {
                            page.Page1Container.Show();
                            page.Page2Container.Hide();

                            UpdateHistory();
                        }
                    );
                };


            #endregion

            @"Hello world".ToDocumentTitle();

            UpdateHistory();
        }

    }
}
