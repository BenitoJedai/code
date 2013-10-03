using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PHPXElementExample
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {

        public Task<XElement> Key_onchange(string Key, string Value)
        {
            return Task.FromResult(
                new XElement("KeyValuePair",
                    new XAttribute("Key", Key),
                    new XElement("Value", Value)
                )
            );
        }


        public Task<XElement> Value_onchange(string Key, string Value)
        {
            return Task.FromResult(
                new XElement("KeyValuePair",
                    new XAttribute("Key", Key),
                    new XElement("Value", Value)
                )
            );
        }


        public void Result_onchange(string doc_xml, Action<string, string> y)
        {
            // %26lt%3BKeyValuePair Key%3D%26quot%3Boo%26quot%3B%26gt%3B%26lt%3BValue%26gt%3Bbar%26lt%3B/Value%26gt%3B%26lt%3B/KeyValuePair%26gt%3B

            try
            {
                //Console.WriteLine(doc_xml);

                var doc = XElement.Parse(doc_xml);

                var Key = doc.Attribute("Key").Value;
                var Value = doc.Element("Value").Value;

                y(Key, Value);
            }
            catch
            {
                throw;
            }
        }
    }
}
