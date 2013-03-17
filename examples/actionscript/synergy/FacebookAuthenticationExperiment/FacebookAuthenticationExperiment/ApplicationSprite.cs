using com.facebook.graph;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System;
using System.Windows.Media;

namespace FacebookAuthenticationExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            Abstractatech.ActionScript.ConsoleFormPackage.ConsoleFormPackageExperience.Initialize();


            this.root.loaderInfo.uncaughtErrorEvents.uncaughtError +=
               e =>
               {


                   Console.WriteLine("error: " + new { e.errorID, e.error, e } + "\n run in flash debugger for more details!");

               };


            this.InvokeWhenStageIsReady(
                () =>
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);

                    Action<object, object> callback = (result, fail) =>
                    {
                        //already logged in because of existing session
                        content.r.Fill = Brushes.Yellow;

                        Console.WriteLine("callback " + new { result });

                        content.r.MouseLeftButtonUp += delegate
                        {
                            Console.WriteLine("MouseLeftButtonUp");


                            Action<object, object> onLogin = (loginresult, loginfail) =>
                            {
                                Console.WriteLine("onLogin " + new { result });

                            };

                            Facebook.login(onLogin.ToFunction());

                        };
                    };

                    Console.WriteLine("Facebook.init");

                    // http://walrusinacanoe.com/flash-development/893

                    //Given URL is not allowed by the Application configuration.: One or more of the given URLs is not allowed by the App's settings.  It must match the Website URL or Canvas URL, or the domain must be a subdomain of one of the App's domains. 

                    Facebook.init(
                        applicationId: "223510104440829",
                        callback: callback.ToFunction()
                    );
                    Console.WriteLine("after Facebook.init");
                }
            );
        }






    }
}
