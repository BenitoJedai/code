using ScriptCoreLib;

namespace java.awt
{
    using @event;

    // http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Button.html
    [Script(IsNative = true)]
    public class Button : Component
    {
        public string getLabel()
        {
            return default(string);
        }

        public void setLabel(string e)
        {

        }

        public void addActionListener(ActionListener l)
        {
        }


        #region bool Enabled
        public bool isEnabled()
        {
            return default(bool);
        }

        public void setEnabled(bool c)
        {

        }
        #endregion

    }
}
