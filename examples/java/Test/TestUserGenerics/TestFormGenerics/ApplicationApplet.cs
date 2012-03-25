using java.applet;
using ScriptCoreLib.Java.Extensions;
using System;

namespace TestFormGenerics
{
    internal sealed class ApplicationApplet : Applet
    {
        public readonly ApplicationControl content = new ApplicationControl();

        public event Action<string> SendStringViaGeneric;

        public ApplicationApplet()
        {
            content.SendStringViaGeneric +=
                e =>
                {
                    if (SendStringViaGeneric != null)
                        SendStringViaGeneric(e);
                };
        }

        public override void init()
        {
            content.AttachTo(this);
            content.AutoSizeTo(this);
            this.EnableVisualStyles();
        }

    }
}
