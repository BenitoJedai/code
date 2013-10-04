using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestXElemToDataGridView
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        public async Task<XElement> GetQueryResultAsXElement()
        {

            var xel = new XElement("TableContainer");
            {
                var row = new XElement("Query");
                var cell = new XElement("cell1");
                row.Add(cell);
                xel.Add(row);
            }
            {
                var row = new XElement("Query");
                var cell = new XElement("cell2");
                row.Add(cell);
                xel.Add(row);
            }

            return xel;
        }

    }
}
