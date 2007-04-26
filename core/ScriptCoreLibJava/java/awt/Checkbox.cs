using ScriptCoreLib;

namespace java.awt
{
    [Script(IsNative = true)]
    public class Checkbox : Component
    {

        public void addItemListener(@event.ItemListener e)
        {
        }

        #region bool State
        public bool getState()
        {
            return default(bool);
        }

        public void setState(bool c)
        {

        }
        #endregion


        #region string Label
        public string getLabel()
        {
            return default(string);
        }

        public void setLabel(string c)
        {

        }
        #endregion

    }
}
