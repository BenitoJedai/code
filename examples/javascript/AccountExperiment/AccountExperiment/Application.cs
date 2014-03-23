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
using System.Threading.Tasks;

namespace AccountExperiment
{
    public sealed class Application : ApplicationWebService
    {
        public Application(IApp page)
        {
            this.account_SelectCount(
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

        public sealed class Login : ApplicationWebService
        {

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
                        this.gravatar_Gravatar(
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

        public sealed class Registerr : ApplicationWebService
        {
            public Registerr(global::Abstractatech.Design.Blue.HTML.Pages.IApp page)
            {

            }
        }

        public sealed class Register : ApplicationWebService
        {

            public Register(IAppRegister page)
            {
                #region gravatar_Gravatar
                Action changed =
                    delegate
                    {
                        this.gravatar_Gravatar(
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
                    async delegate
                    {
                        page.CreateMyNewAccount.disabled = true;

                        var session = await this.CreateAccount(
                            page.name,
                            page.web,
                            page.email,
                            page.password,
                            page.skype
                        );

                        new Cookie("session").Value = session;

                        Native.Document.location.replace("/");
                    };
            }
        }


        public sealed class Dashboard : ApplicationWebService
        {

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
                        this.page_TellServerToDropMySession_onclick(session.Value,
                            delegate
                            {
                                Native.Document.location.reload();
                            }
                        );
                    };

                con.Show();

                Console.WriteLine(session.Value);

                this.account_SelectByCookie(
                    // wow. webmethods are too isolated, cant see cookies:)
                    session.Value,
                    email =>
                    {
                        page.email.innerText = email;
                    }
                );



                page.SinceIAmNowLggedInTellMeHowManyActiveSessionsAreThere.onclick +=
                    async delegate
                    {
                        var x = await this.SinceIAmNowLggedInTellMeHowManyActiveSessionsAreThere(session.Value);

                        Native.window.alert(x);
                    };


                page.ManageMyDevices.onclick +=
                    delegate
                    {
                        new MyDevicesForm
                        {
                            // at this point the client does not actually need to know the account id
                            // account will be taken from session token

                            // we cannot set the account like this anymore?
                            // ??
                            //__account = 42,

                            service = this.IMyDevicesComponent_MyDevices()
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

            // how does this help us?
            public readonly Cookie session = new Cookie("session");

            //Task<long> MyDevices_Insert(string name, string value);
            //Task MyDevices_SelectByAccount(Action<long, string, string> yield);
            //Task MyDevices_Update(long id, string name, string value);

            public Task<long> MyDevices_Insert(string name, string value)
            {
                return service.MyDevices_Insert(session.Value, name, value);
            }

            public Task MyDevices_SelectByAccount(Action<long, string, string> yield)
            {
                return service.MyDevices_SelectByAccount(session.Value, yield);
            }

            public Task MyDevices_Update(long id, string name, string value)
            {
                return service.MyDevices_Update(session.Value, id, name, value);
            }
        }

        public static IMyDevicesComponent_MyDevices IMyDevicesComponent_MyDevices(this ApplicationWebService service)
        {
            return new __IMyDevicesComponent_MyDevices { service = service };
        }
    }
}
