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
using Abstractatech.ConsoleFormPackage.Library;

namespace AccountExperiment
{
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public Application(IApp page)
        {

        }

        public static string Tracker()
        {
            return "xx";
        }

        public sealed class Login
        {
            public readonly ApplicationWebService service = new ApplicationWebService();

            public Login(IAppLogin page)
            {
                new Cookie("message").With(
                      message =>
                      {
                          if (string.IsNullOrEmpty(message.Value))
                              return;

                          Native.Window.alert(message.Value);
                          message.Delete();


                          //Native.Document.location.reload();
                      }
                 );

                page.OK.disabled = false;
                page.OK.onclick +=
                    delegate
                    {
                        page.OK.disabled = true;


                        // oldschool, we want bowser to remember our password!
                        page.form.submit();
                    };
            }


            public static string Tracker()
            {
                return "xx login";
            }

        }

        public sealed class Dashboard
        {
            public readonly ApplicationWebService service = new ApplicationWebService();

            public readonly ConsoleForm con = new ConsoleForm().InitializeConsoleFormWriter();

            public readonly Cookie session = new Cookie("session");

            public Dashboard(IAppDashboard page)
            {
                page.LogOut.onclick +=
                    delegate
                    {
                        session.Delete();

                        Native.Document.location.reload();
                    };

                con.Show();

                Console.WriteLine(session.Value);
                Console.WriteLine(session.ValueBase64);

                service.WhatsMyEmail(
                    // wow. webmethods are too isolated, cant see cookies:)
                    session.Value,
                    email =>
                    {
                        page.email.innerText = email;
                    }
                );



                page.SinceIAmNowLggedInTellMeHowManyActiveSessionsAreThere.onclick +=
                    delegate
                    {
                        service.SinceIAmNowLggedInTellMeHowManyActiveSessionsAreThere(session.Value,
                            x => Native.Window.alert(x)
                        );
                    };
            }


            public static string Tracker()
            {
                return "xx dashboard";
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
