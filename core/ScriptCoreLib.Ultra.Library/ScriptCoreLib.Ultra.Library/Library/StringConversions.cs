using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace ScriptCoreLib.Library
{
    public static class StringConversions
    {
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
    }
}
