using ScriptCoreLib;

namespace java.awt.@event
{
    [Script(IsNative = true)]
    public interface KeyListener : java.util.EventListener
    {
        void keyPressed(KeyEvent e);
        void keyReleased(KeyEvent e);
        void keyTyped(KeyEvent e);
    }
}
