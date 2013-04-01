using ScriptCoreLib.Shared.Data;
using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using JellyworldExperiment.Library;
using JellyworldExperiment.Schema;

//namespace com.abstractatech.gamification.jellyworldexperiment
namespace com.abstractatech.gamification.jwe
{
    public partial class ApplicationWebService
    {
        public void AtFrame(
          string id,

          // do we care about the frame?
          string frame,

          string dx = "0",
          string dy = "0",

          string goleft = "0",
          string goup = "0",
          string goright = "0",
          string godown = "0",

          Action<string> yield = null
          )
        {
            Console.WriteLine(
                new { dx, dy, goleft, goup, goright, godown }
            );

            var now = DateTime.Now;
            var ms = now.Ticks / 1000;

            new SensorySync().InsertVector(
                new SensorySyncQueries.InsertVector
                {
                    ms = ms,

                    x = int.Parse(dx),
                    y = int.Parse(dy),

                    goleft = int.Parse(goleft),
                    goup = int.Parse(goup),
                    goright = int.Parse(goright),
                    godown = int.Parse(godown),
                }
            );

            yield("");

            Console.WriteLine(
           new { dx, dy, goleft, goup, goright, godown } + " done"
       );
        }



        public /* will not be part of web service itself */ void SensorySync_Handler(WebServiceHandler h)
        {
            // X:\jsc.svn\examples\javascript\ServerSideEventExperiment\ServerSideEventExperiment\ApplicationWebService.cs


            var Accepts = h.Context.Request.Headers["Accept"];

            if (Accepts != null)
                if (Accepts.Contains(EventSourceGenerator.ContentType))
                {
                    var s = new EventSourceGenerator(h);

                    var status = s["status"];

                    // lets start talking to db
                    new SensorySync().With(
                        SensorySync =>
                        {

                            if (s.id == 0)
                            {
                                // ah. first time.
                                // lets ask the database then.

                                var last_ms = SensorySync.Last();
                                if (last_ms > 0)
                                {
                                    s.id = last_ms;
                                    // well the database gave as the last id.
                                    // for the client
                                    // ther will be no data until new events tho 

                                }
                                else
                                {
                                    // well the client doesnt know
                                    // and the database is empty.

                                    // tell the client to come back at a later time
                                    s.retry = 250;
                                    status("android device is quiet...");
                                    h.CompleteRequest();
                                    return;
                                }
                            }


                            s.retry = 1000 / 60;

                            var DoNextFrame = true;
                            var RetryCount = 60;

                            Action AtFrame = delegate
                            {
                                // wait for next frame
                                DoNextFrame = false;
                                Thread.Sleep(s.retry);

                                SensorySync.Sum(s.id,
                                    reader =>
                                    {
                                        // can we use dynamic xelement here instead?

                                        long
                                            last_ms = reader.ms,
                                            x = reader.x,
                                            y = reader.y,
                                            goleft = reader.goleft,
                                            goup = reader.goup,
                                            goright = reader.goright,
                                            godown = reader.godown;

                                        if (last_ms > 0)
                                        {
                                            status("Yea!");

                                            var data = new XElement("yield",
                                                // can xattribute support long?
                                                new XAttribute("last_ms", "" + last_ms),
                                                new XAttribute("x", "" + x),
                                                new XAttribute("y", "" + y),
                                                new XAttribute("goleft", "" + goleft),
                                                new XAttribute("goup", "" + goup),
                                                new XAttribute("goright", "" + goright),
                                                new XAttribute("godown", "" + godown)
                                            );

                                            DoNextFrame = true;

                                            s.id = last_ms;
                                            s.postMessage(data);

                                        }
                                        else
                                        {
                                            //Thread.Sleep(300);

                                            if (RetryCount > 0)
                                            {
                                                RetryCount--;
                                                DoNextFrame = true;
                                            }
                                            // try again at the next frame?
                                        }

                                    }
                                );

                            };

                            while (DoNextFrame)
                            {
                                AtFrame();
                            }



                            //Thread.Sleep(1000);
                            status("android device is quiet...");

                            h.CompleteRequest();

                        }
                    );
                    return;
                }
        }
    }
}

namespace JellyworldExperiment
{
    namespace Schema
    {
        public class SensorySync : SensorySyncQueries
        {
            public readonly Action<Action<SQLiteConnection>> WithConnection;
            public readonly Action<Action<SQLiteConnection>> WithReadOnlyConnection;


            public SensorySync(string DataSource = "JellyworldExperiment.sqlite")
            {
                this.WithReadOnlyConnection = DataSource.AsWithConnection();
                this.WithConnection = DataSource.AsWithConnection(ReadOnly: false);

                WithConnection(
                   c =>
                   {
                       new Create { }.ExecuteNonQuery(c);
                   }
                );
            }
            public void InsertVector(SensorySyncQueries.InsertVector insertVector)
            {
                WithConnection(
                   c =>
                   {
                       insertVector.ExecuteNonQuery(c);
                   }
                );
            }

            public long Last()
            {
                long value = 0;

                WithReadOnlyConnection(
                    c =>
                    {
                        using (var reader = new SelectLast().ExecuteReader(c))
                        {
                            if (reader.Read())
                            {
                                dynamic r = new DynamicDataReader(reader);

                                long ms = r.ms;

                                value = ms;

                            }
                        }
                    }
                );

                return value;
            }


            public void Sum(SelectSum e, Action<dynamic> yield)
            {
                WithReadOnlyConnection(
                    c =>
                    {
                        e.ExecuteReader(c).WithEach(yield);
                    }
                );
            }
        }



        public static partial class XX
        {
            public static void WithEach(this SQLiteDataReader reader, Action<dynamic> y)
            {
                using (reader)
                {
                    while (reader.Read())
                    {
                        y(new DynamicDataReader(reader));
                    }
                }
            }





            public static Action<Action<SQLiteConnection>> AsWithConnection(this string DataSource, int Version = 3, bool ReadOnly = true)
            {
                //Console.WriteLine("AsWithConnection...");

                return (y) =>
                {
                    //Console.WriteLine("AsWithConnection... invoke");

                    using (var c = DataSource.ToConnection(Version, ReadOnly))
                    {
                        c.Open();

                        try
                        {
                            y(c);
                        }
                        catch (Exception ex)
                        {
                            var message = new { ex.Message, ex.StackTrace }.ToString();

                            Console.WriteLine("AsWithConnection... error: " + message.SkipUntilLastIfAny("Caused by:"));

                            // The database file is locked
                            throw new InvalidOperationException(message);
                        }
                    }
                };
            }

            public static SQLiteConnection ToConnection(this string DataSource, int Version = 3, bool ReadOnly = true)
            {
                var csb = new SQLiteConnectionStringBuilder
                {
                    DataSource = DataSource,
                    Version = Version,

                    // prevent locking?
                    ReadOnly = ReadOnly
                };

                var c = new SQLiteConnection(csb.ConnectionString);

                return c;
            }
        }
    }
}
