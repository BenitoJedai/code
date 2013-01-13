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
                page.OK.disabled = false;
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

        public sealed class Register
        {
            public readonly ApplicationWebService service = new ApplicationWebService();

            public Register(IAppRegister page)
            {
                Action changed =
                    delegate
                    {
                        service.gravatar_Gravatar(
                            page.email.value,
                            avatar: value => page.avatar.src = value,
                            profile: value => page.profile.href = value
                        );

                    };

                page.email.onchange +=
                   delegate
                   {

                       changed();

                   };

                page.email.onkeyup +=
                    delegate
                    {

                        changed();

                    };

                changed();


                page.CreateMyNewAccount.onclick +=
                    delegate
                    {
                        page.CreateMyNewAccount.disabled = true;

                        service.CreateAccount(
                            page.name.value,
                            page.web.value,
                            page.email.value,
                            page.password.value,
                            page.skype.value,
                            session =>
                            {
                                new Cookie("session").Value = session;

                                Native.Document.location.replace("/");
                            }
                        );
                    };
            }
        }
    }
}
