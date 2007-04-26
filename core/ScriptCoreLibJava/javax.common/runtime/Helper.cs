using ScriptCoreLib;

using Exception = global::System.Exception;

using java;
using java.lang;
using java.io;
using java.util;
using java.text;


namespace javax.common.runtime
{
    [Script]
    public static class Helper
    {
        #region BuildDate
        public static Date BuildDate
        {
            get
            {
                SimpleDateFormat sdf = new SimpleDateFormat("dd.MM.yyyy h:mm:ss z");

                Date myDate = null;

                try
                {
                    myDate = sdf.parse(BuildDateString);
                }
                catch { }


                return myDate;
            }
        }

        /// <summary>
        /// gets the hard coded build date
        /// </summary>
        public static string BuildDateString
        {
            [Script(
                UseCompilerConstants = true,
                OptimizedCode = "return \"{BuildDate} UTC\";"
                )]
            get
            {
                return default(string);
            }
        }


        /// <summary>
        /// gets the hard coded compiler build date
        /// </summary>
        public static string CompilerBuildDateString
        {
            [Script(
                UseCompilerConstants = true,
               OptimizedCode = "return \"{CompilerBuildDate} UTC\";"
                )]
            get
            {
                return default(string);
            }
        }


        #endregion

        public static bool IsVisibleChar(int p)
        {
            bool bA = p >= 'a';
            bool aZ = p <= 'z';
            bool lA = (bA && aZ);

            bool buA = p >= 'A';
            bool auZ = p <= 'Z';
            bool uA = (buA && auZ);


            bool b0 = p >= '0';
            bool a9 = p <= '9';
            bool uN = (b0 && a9);

            bool x = "\'\"=[]()<>+-;:.?@/".IndexOf((char)p) > -1;

            bool isAlpha = lA || uA || uN || x;
            return isAlpha;
        }

        public static Runtime CurrentRuntime
        {
            get
            {
                return Runtime.getRuntime();
            }
        }

        public static string UsedMemoryPercentage
        {
            get
            {
                return Convert.ToInt16(UsedMemory * 100 / CurrentRuntime.totalMemory()) + "%";
            }
        }

        public static long UsedMemory
        {
            get
            {
                return (CurrentRuntime.totalMemory() - CurrentRuntime.freeMemory());
            }
        }

        public static string TotalMemoryString
        {
            get
            {
                return Convert.BytesToHuman( CurrentRuntime.totalMemory() );
            }
        }


        public static string MemoryUsageString
        {
            get
            {
                return Helper.UsedMemoryPercentage + " of " + Helper.TotalMemoryString + " (" + CurrentRuntime.totalMemory() + " bytes)"; 
            }
        }
             
        
    }

}
