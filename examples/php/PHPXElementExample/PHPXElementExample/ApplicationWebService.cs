using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace PHPXElementExample
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {

        public void Key_onchange(string Key, string Value, Action<XElement> y)
        {
            y(
                new XElement("KeyValuePair",
                    new XAttribute("Key", Key),
                    new XElement("Value", Value)
                )     
            );
        }


        public void Value_onchange(string Key, string Value, Action<XElement> y)
        {
            y(
                new XElement("KeyValuePair",
                    new XAttribute("Key", Key),
                    new XElement("Value", Value)
                )
            );
        }

        public void Result_onchange(string doc_xml, Action<string, string> y)
        {
            try
            {
                var doc = XElement.Parse(doc_xml);

                var Key = doc.Attribute("Key").Value;
                var Value = doc.Element("Value").Value;

                y(Key, Value);
            }
            catch
            {
            }
        }
    }
}
