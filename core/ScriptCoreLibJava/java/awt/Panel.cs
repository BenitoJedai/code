using ScriptCoreLib;
using javax.accessibility;

namespace java.awt
{
    [Script(IsNative = true)]
    public class Panel : Container, Accessible
    {
        public virtual void paint(Graphics g)
        {
        }
    }
}
