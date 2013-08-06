using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace ScriptCoreLib.Library
{
    public static partial class StringConversions
    {
        // used by JVM/CLR X:\jsc.internal.svn\compiler\jsc.internal\jsc.internal\meta\Library\ILStringConversions.cs

        public static string UTF8FromBase64StringOrDefault(string e)
        {
            // allow 0 char do be sent
            if (e == null)
                return null;

            var bytes = Convert.FromBase64String(e);

            return Encoding.UTF8.GetString(bytes);
        }

        public static string UTF8ToBase64StringOrDefault(string e)
        {
            if (e == null)
                return null;

            var bytes = Encoding.UTF8.GetBytes(e);

            return Convert.ToBase64String(bytes);
        }

        public static byte[] FromBase64StringOrDefault(this string e)
        {
            if (e == null)
                return null;

            return Convert.FromBase64String(e);
        }

        public static string ToBase64StringOrDefault(this byte[] e)
        {
            if (e == null)
                return null;

            return Convert.ToBase64String(e);
        }


        #region XElement
        public static string ConvertXElementToString(XElement e)
        {
            if (e == null)
                return null;

            return e.ToString();
        }

        public static XElement ConvertStringToXElement(string e)
        {
            if (string.IsNullOrEmpty(e))
                return null;

            return XElement.Parse(e);
        }
        #endregion

        #region FileInfo
        public static string ConvertFileInfoToString(FileInfo e)
        {
            if (e == null)
                return null;

            return e.FullName;
        }

        public static FileInfo ConvertStringToFileInfo(string e)
        {
            if (string.IsNullOrEmpty(e))
                return null;

            return new FileInfo(e);
        }
        #endregion
    }
}
