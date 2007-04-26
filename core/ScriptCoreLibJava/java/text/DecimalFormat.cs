using ScriptCoreLib;

using java.lang;
using java.util;

namespace java.text
{
    [Script(IsNative=true)]
    public class DecimalFormat : NumberFormat
    {
        public void applyPattern(string e)
        {
        }

        #region bool DecimalSeparatorAlwaysShown
        public bool getDecimalSeparatorAlwaysShown()
        {
            return default(bool);
        }

        public void setDecimalSeparatorAlwaysShown(bool c)
        {

        }
        #endregion


        #region DecimalFormatSymbols DecimalFormatSymbols
        public DecimalFormatSymbols getDecimalFormatSymbols()
        {
            return default(DecimalFormatSymbols);
        }

        public void setDecimalFormatSymbols(DecimalFormatSymbols c)
        {

        }
        #endregion

    }
}
