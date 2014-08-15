using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/convert.cs
    [Script(Implements = typeof(global::System.Convert))]
    public static partial class __Convert
    {
        //        Implementation not found for type import :
        //type: System.Convert
        //method: Int64 ToInt64(Boolean)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!

        public static long ToInt64(bool value)
        {
            if (value)
                return 1;


            return 0;
        }


        public static long ToInt64(double value)
        {
            return (long)global::System.Math.Floor(value);
        }




        public static string ToString(char value)
        {
            return new string(value, 1);
        }


        public static string ToString(bool value)
        {
            // CLR is using True and False?
            if (value)
                return "true";

            return "false";
        }
        public static bool ToBoolean(string value)
        {
            // X:\jsc.svn\examples\javascript\appengine\StopwatchTimetravelExperiment\StopwatchTimetravelExperiment\Application.cs

            if (value != null)
                if ("true" == value.ToLower())
                    return true;

            return false;
        }

        public static string ToString(int value)
        {

            return "" + value;
        }

        public static string ToString(short value)
        {
            // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\ILStringConversions.cs
            return "" + value;
        }

        public static string ToString(long value)
        {
            return "" + value;
        }

        //public static string ToString(short value)
        //{
        //    return "" + value;
        //}

        public static string ToString(double value)
        {
            return "" + value;
        }

        public static string ToString(float value)
        {
            return "" + value;
        }

        public static string ToString(byte value)
        {
            return "" + value;
        }






        public static short ToInt16(string e)
        {
            return short.Parse(e);
        }

        public static byte ToByte(string e)
        {
            return byte.Parse(e);
        }

        //        Implementation not found for type import :
        //type: System.Convert
        //method: Int64 ToInt64(System.Object)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!
        public static long ToInt64(object e)
        {
            // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs
            // X:\jsc.svn\examples\java\Test\JVMCLRTypeOfInt32\JVMCLRTypeOfInt32\Program.cs

            var i32 = e is int;
            if (i32)
                return (int)e;


            // I/System.Console( 4611): getType is unavailable at API 8
            // either this is ok, or xlsx assets library has to use 
            // a wiser method to cope with android api8 limits
            var s = e as string;
            if (s != null)
            {
                // tested by
                // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs

                var x = default(long);

                try
                {
                    x = long.Parse(s);
                }
                catch
                {
                    // ???
                }

                return x;
            }

            //Caused by: java.lang.ClassCastException: java.lang.String cannot be cast to java.lang.Long
            //       at ScriptCoreLib.Shared.BCLImplementation.System.__Convert.ToInt64(__Convert.java:105)
            //       at AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1B_Sheet2__SelectAllAsEnumerable_closure.yield(Book1B_Sheet2__SelectAllAsEnumerable_closure.java:27)
            //       ... 34 more

            return (long)e;
        }

        public static long ToInt64(string e)
        {
            // "" Input string was not in a correct format.

            // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\ApplicationWebService.cs

            if (string.IsNullOrEmpty(e))
                return 0;

            return long.Parse(e);
        }


        // what about this : public static int ToInt32(int e)

        public static int ToInt32(uint e)
        {
            int x = (int)e;

            return x;
        }

        public static int ToInt32(double e)
        {
            return (int)Math.Floor((double)e);
        }



        // conflict in java with uint 
        //public static int ToInt32(int value)
        //{
        //    return (int)global::System.Math.Floor((double)value);
        //}


        public static int ToInt32(float value)
        {
            return (int)global::System.Math.Floor(value);
        }

        public static byte ToByte(int value)
        {
            return (byte)(value & 0xff);

        }

        public static byte ToByte(double value)
        {
            return (byte)(((int)global::System.Math.Floor(value)) & 0xff);
        }


        

        public static string ToString(object value)
        {
            // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\String.cs

            if (value == null)
                return null;

            var s = value as string;
            if (s != null)
                return s;

            // ?
            var i8 = value is long;
            if (i8)
                return "" + ((long)value);

            return value.ToString();
        }





        public static float ToSingle(string value)
        {
            return float.Parse(value);

        }

        public static bool ToBoolean(int value)
        {
            return value != 0;
        }

        public static bool ToBoolean(long value)
        {
            return value != 0;
        }

        public static int ToInt32(bool value)
        {
            if (value)
                return 1;

            return 0;
        }

        // ...



        public static uint ToUInt32(int value)
        {
            return ((uint)value);

        }

        public static uint ToUInt32(long value)
        {
            return ((uint)value & 0xffffffff);

        }


    }

}
