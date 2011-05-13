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
        #region string[]
        public static string ConvertStringArrayToString(string[] e)
        {
            if (e == null)
                return null;

            var xml = new XElement("string");

            var Length = e.Length;

            // ScriptCoreLibJava.XLinq does not yet support reading all elements :) thus we have to name then ahead of time..
            xml.Add(new XAttribute("c", "" + Length));

            for (int i = 0; i < Length; i++)
            {
                xml.Add(new XElement("i" + i, e[i]));
            }

            return ConvertXElementToString(xml);
        }

        public static string[] ConvertStringToStringArray(string e)
        {
            if (string.IsNullOrEmpty(e))
                return null;

            var xml = ConvertStringToXElement(e);

            var Length = int.Parse(xml.Attribute("c").Value);

            var y = new string[Length];

            for (int i = 0; i < Length; i++)
            {
                y[i] = xml.Element("i" + i).Value;
            }

            return y;
        }
        #endregion

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
