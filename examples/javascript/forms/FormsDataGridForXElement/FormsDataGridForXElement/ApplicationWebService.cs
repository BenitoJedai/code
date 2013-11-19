using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FormsDataGridForXElement
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public async Task<XElement> GetDataSource()
        {
            // Send it back to the caller.
            var xel = new XElement("testColumn1");
            {
                var row = new XElement("row");
            var cell = new XElement("cell1");
            row.Add(cell);
            xel.Add(row);
            }
            {
            var row = new XElement("row2");
            var cell = new XElement("cell2");
            row.Add(cell);
            xel.Add(row);
            }


            return xel;
        }

    }
}
