using ScriptCoreLib;

using java.lang;
using java.util;

namespace java.text
{
    [Script(IsNative=true)]
    public class NumberFormat
    {
        public string format(double e)
        {
            return default(string);
        }

        #region int MaximumFractionDigits
        public int getMaximumFractionDigits()
        {
            return default(int);
        }

        public void setMaximumFractionDigits(int c)
        {

        }
        #endregion


        #region int MinimumFractionDigits
        public int getMinimumFractionDigits()
        {
            return default(int);
        }

        public void setMinimumFractionDigits(int c)
        {

        }
        #endregion

        public static NumberFormat getInstance()
        {
            return default(NumberFormat);
        }
    }
}
