using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.GLSL;
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

        public void __button3_Click(Action3<string> yield)
        {
            delta.Enumerate(
                reader =>
                {
                    long id = reader.id;
                    long ticks = reader.ticks;

                    ivec3 xyz = reader.xyz;

                    yield(
                        "" + xyz.x,
                        "" + xyz.y,
                        "" + xyz.z
                    );
                }
            );
        }

        public void __button4_Click(Action<string> yield)
        {
            delta.Last(
                ticks =>
                {
                    yield("" + ticks);
                }
            );
        }

        public void __button5_Click(string ticks, Action3<string> yield)
        {
            delta.Sum(
                new Schema.DeltaTable.SumQuery { ticks = long.Parse(ticks) },
                reader =>
                {
                    ivec3 xyz = reader.xyz;

                    yield(
                        "" + xyz.x,
                        "" + xyz.y,
                        "" + xyz.z
                    );
                }
            );
        }
    }
}
