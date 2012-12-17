using DeltaExperiment.Schema;
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

        public readonly Delta delta = new Delta();

        // write only
        public void __button2_Click(string x)
        {
            var now = DateTime.Now;

            //634902317876105299 
            //634902317876105300
            // javascript side will turnacate last digits!

            var ms = now.Ticks / 1000;


            delta.Add(
                new DeltaQueries.InsertVector
                {
                    ticks = ms,
                    x = int.Parse(x),
                    y = 10,
                    //z 
                }

            );
        }

        public void __button3_Click(Action<string, string, string, string> yield)
        {
            delta.Enumerate(
                reader =>
                {
                    long ticks = reader.ticks;

                    //ivec3 xyz = reader.xyz;
                    long x = reader.x;
                    long y = reader.y;
                    long z = reader.z;

                    yield(
                        "" + ticks,

                        "" + x,
                        "" + y,
                        "" + z
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

        public void __button5_Click(string ticks, Action<string, string, string, string> yield)
        {
            var in_ticks = long.Parse(ticks);

            //Console.WriteLine(new { in_ticks });

            delta.Sum(
                in_ticks,
                reader =>
                {
                    long out_ticks = reader.ticks;
                    //Console.WriteLine(new { out_ticks });

                    //ivec3 xyz = reader.xyz;
                    //Console.WriteLine("Sum .x");
                    long x = reader.x;
                    //Console.WriteLine("Sum .y");
                    long y = reader.y;
                    //Console.WriteLine("Sum .z");
                    long z = reader.z;

                    yield(
                        "" + out_ticks,
                        "" + x,
                        "" + y,
                        "" + z
                    );
                }
            );
        }
    }
}
