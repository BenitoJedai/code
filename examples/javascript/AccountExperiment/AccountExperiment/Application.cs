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
using AccountExperiment.MyDevicesComponent.Library;
using AccountExperiment.MyDevicesComponent;

namespace AccountExperiment
{
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public Application(IApp page)
        {
            service.account_SelectCount(
                count =>
                {
                    page.accounts.innerText = count;
                }
            );
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
                #region message
                new Cookie("message").With(
                      message =>
                      {
                          if (string.IsNullOrEmpty(message.Value))
                              return;

                          Native.window.alert(message.Value);
                          message.Delete();


                          //Native.Document.location.reload();
                      }
                 );
                #endregion

                #region gravatar_Gravatar
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
                #endregion

                #region OK
                page.OK.disabled = false;
                page.OK.onclick +=
                    delegate
                    {
                        page.OK.disabled = true;


                        // oldschool, we want bowser to remember our password!
                        page.form.submit();
                    };
                #endregion
            }


            public static string Tracker()
            {
                return "xx login";
            }

        }

        public sealed class Registerr
        {
            public readonly ApplicationWebService service = new ApplicationWebService();

            public Registerr(global::Abstractatech.Design.Blue.HTML.Pages.IApp page)
            {

            }
        }

        public sealed class Register
        {
            public readonly ApplicationWebService service = new ApplicationWebService();

            public Register(IAppRegister page)
            {
                #region gravatar_Gravatar
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
                #endregion



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

                page.TellServerToDropMySession.onclick +=
                    delegate
                    {
                        service.page_TellServerToDropMySession_onclick(session.Value,
                            delegate
                            {
                                Native.Document.location.reload();
                            }
                        );
                    };

                con.Show();

                Console.WriteLine(session.Value);

                service.account_SelectByCookie(
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
                            x => Native.window.alert(x)
                        );
                    };


                page.ManageMyDevices.onclick +=
                    delegate
                    {
                        new MyDevicesForm
                        {
                            // at this point the client does not actually need to know the account id
                            // account will be taken from session token
                            __account = 42,

                            service = service.IMyDevicesComponent_MyDevices()
                        }.Show();
                    };
            }


            public static string Tracker()
            {
                return "xx dashboard";
            }

        }

    }


    public static class ExtensionsForApplication
    {
        class __IMyDevicesComponent_MyDevices : IMyDevicesComponent_MyDevices
        {
            public ApplicationWebService service;

            public readonly Cookie session = new Cookie("session");

            public void MyDevices_Insert(string account, string name, string value, Action<string> yield)
            {
                service.MyDevices_Insert(session.Value, name, value, yield);
            }

            public void MyDevices_SelectByAccount(string account, Action<string, string, string> yield, Action done)
            {
                service.MyDevices_SelectByAccount(session.Value, yield, done);
            }

            public void MyDevices_Update(string account, string id, string name, string value, Action done)
            {
                service.MyDevices_Update(session.Value, id, name, value, done);
            }
        }

        public static IMyDevicesComponent_MyDevices IMyDevicesComponent_MyDevices(this ApplicationWebService service)
        {
            return new __IMyDevicesComponent_MyDevices { service = service };
        }
    }
}
