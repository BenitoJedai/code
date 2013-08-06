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
        #region code template
        public class __ElementType
        {
            public static __ElementType FromString(string e)
            {
                throw new NotSupportedException();
            }

            public static string ToString(__ElementType e)
            {
                throw new NotSupportedException();
            }

            public static string ConvertElementTypeArrayToString(__ElementType[] e)
            {
                if (e == null)
                    return null;

                var xml = new XElement("array");

                var Length = e.Length;

                xml.Add(new XAttribute("c", "" + Length));

                for (int i = 0; i < Length; i++)
                {
                    xml.Add(new XElement("i" + i, ToString(e[i])));
                }

                return ConvertXElementToString(xml);
            }

            public static __ElementType[] ConvertStringToElementTypeArray(string e)
            {
                if (string.IsNullOrEmpty(e))
                    return null;

                var xml = ConvertStringToXElement(e);

                var Length = int.Parse(xml.Attribute("c").Value);

                var y = new __ElementType[Length];

                for (int i = 0; i < Length; i++)
                {
                    y[i] = FromString(xml.Element("i" + i).Value);
                }

                return y;
            }
        }
        #endregion

        #region string[]
        [Obsolete]
        public static string ConvertStringArrayToString(string[] e)
        {
            if (e == null)
                return null;

            var xml = new XElement("array");

            var Length = e.Length;

            // what about null elements?
            // what about now?
            // ScriptCoreLibJava.XLinq does not yet support reading all elements :) thus we have to name then ahead of time..
            xml.Add(new XAttribute("c", "" + Length));

            for (int i = 0; i < Length; i++)
            {
                xml.Add(new XElement("i" + i, (e[i])));
            }

            return ConvertXElementToString(xml);
        }

        [Obsolete]
        public static string[] ConvertStringToStringArray(string e)
        {
            if (string.IsNullOrEmpty(e))
                return null;

            var xml = ConvertStringToXElement(e);

            var Length = int.Parse(xml.Attribute("c").Value);

            var y = new string[Length];

            for (int i = 0; i < Length; i++)
            {
                y[i] = (xml.Element("i" + i).Value);
            }

            return y;
        }
        #endregion


    }
}
