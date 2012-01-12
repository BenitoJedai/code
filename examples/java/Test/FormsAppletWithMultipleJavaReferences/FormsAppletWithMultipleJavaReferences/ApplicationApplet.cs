using java.applet;
using ScriptCoreLib.Java.Extensions;
using System;
using ScriptCoreLib.Delegates;

namespace FormsAppletWithMultipleJavaReferences
{
    internal sealed class ApplicationApplet : Applet
    {
        public readonly ApplicationControl content = new ApplicationControl();

        public event StringAction AtClick;

        public override void init()
        {
            content.AttachTo(this);
            content.AutoSizeTo(this);
            this.EnableVisualStyles();

            content.AtClick +=
                delegate
                {
                    global::TestJavaNativesWithReferences.Class1 s = new global::TestJavaNativesWithReferences.Class2();


                    if (AtClick != null)
                        AtClick(s.GetDescription());
                };
        }

    }
}
