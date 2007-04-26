using ScriptCoreLib;

using java.awt;

namespace java.applet
{
    [Script(IsNative=true)]
    public interface AppletStub
    {
        AppletContext getAppletContext();
    }
}
