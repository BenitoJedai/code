using ChromeAppletExperiment;
using java.applet;
using ScriptCoreLib.Java.Extensions;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ChromeAppletExperiment
{
    public sealed class ApplicationApplet : Applet
    {
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WriteInitialization.cs

        public readonly ApplicationControl content = new ApplicationControl();

        public override void init()
        {
            content.AttachTo(this);
            content.AutoSizeTo(this);
            this.EnableVisualStyles();
        }

    }
}
