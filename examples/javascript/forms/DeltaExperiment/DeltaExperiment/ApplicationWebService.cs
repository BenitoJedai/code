using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace DeltaExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        // http://www.isbe.state.il.us/ILDS/pdf/SQL_server_standards.pdf

        public readonly DeltaTable delta = new DeltaTable();

        // write only
        public void __button2_Click(string x)
        {
            var now = DateTime.Now;

            delta.Add(
                ticks: now.Ticks,
                x: int.Parse(x)
            );
        }

    }
}
