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
using AccountExperiment.Design;
using AccountExperiment.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace AccountExperiment
{
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public Application(IApp page)
        {

        }

        public sealed class Login
        {
            public readonly ApplicationWebService service = new ApplicationWebService();

            public Login(IAppLogin page)
            {
                page.OK.onclick +=
                    delegate
                    {
                        page.OK.disabled = true;


                        // oldschool, we want bowser to remember our password!
                        page.form.submit();
                    };
            }

        }

        public sealed class Dashboard
        {
            public readonly ApplicationWebService service = new ApplicationWebService();

            public Dashboard(IAppDashboard page)
            {
                page.LogOut.onclick +=
                    delegate
                    {
                        new Cookie("session").Delete();

                        Native.Document.location.reload();
                    };
            }
        }
    }
}
