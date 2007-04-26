using ScriptCoreLib;

using java.awt;
using java.net;

namespace java.applet
{
    [Script(IsNative=true)]
    public interface AppletContext
    {
        void showDocument(URL url);
    }
}
