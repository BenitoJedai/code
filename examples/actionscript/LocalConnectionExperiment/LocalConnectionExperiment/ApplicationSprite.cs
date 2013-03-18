using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.Extensions;
using System;
using System.Windows.Media;

namespace LocalConnectionExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        LocalConnection c = new LocalConnection();
        LocalConnection rx = new LocalConnection();

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                () =>
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);


                    // http://fcontheweb.com/articles/localconnection/
                    var r = new Random();

                    var ii = r.Next(16);

                    c.allowDomain("*");
                    c.allowInsecureDomain("*");
                    c.status += delegate
                    { };

                    content.MouseLeftButtonUp +=
                        delegate
                        {
                            content.r.Fill = Brushes.Yellow;

                            for (int i = 0; i < 16; i++)
                            {
                                c.send("LocalConnectionExperiment" + i, "Invoke", "data");
                            }
                            c.send("Invoke756613399", "Invoke", "data");
                        };



                    var client = new DynamicContainer { Subject = new object() };

                    Action MouseLeftButtonUp = delegate
                    {
                        content.r.Fill = Brushes.Green;

                    };

                    // http://stackoverflow.com/questions/6834455/as3-localconnection-errors
                    client["Invoke"] = MouseLeftButtonUp.ToFunction();

                    rx.status += delegate
                    { };

                    rx.client = client.Subject;

                    // ArgumentError: Error #2082: Connect failed because the object is already connected.
                    // Error #2044: Unhandled StatusEvent:. level=error, code=
                    rx.connect("LocalConnectionExperiment" + ii);
                }
            );
        }

    }
}
