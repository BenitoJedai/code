using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System.Data.SQLite
{
    // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\SQLiteConnectionStringBuilderExtensions.cs

    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141102
    //#if FSQLiteConnection
    public static class SQLiteConnectionStringBuilderExtensions
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

        public static long Counter = 0;


        public class StillUseableForSomeTime
        {
            public long ThreadID;
            public SQLiteConnection c;
            public readonly Stopwatch w = Stopwatch.StartNew();

            //public static object SyncLock = new object();

            public static long OpenCounter = 0L;

            public static SQLiteConnection Open(SQLiteConnectionStringBuilder csb)
            {
                return InterlockedOpenOrDispose(close: null, open: csb);
            }

            public static SQLiteConnection InternalOpen(SQLiteConnectionStringBuilder csb)
            {
                var c = default(SQLiteConnection);


                Action restore = delegate { };

                while (lookup.Count > 0)
                {
                    // look at the pool

                    var candidate = lookup.Dequeue();

                    // the pool timeout
                    if (candidate.w.ElapsedMilliseconds > 2000)
                    {
                        candidate.c.Dispose();
                    }
                    else
                    {
                        // doe the dbs match?
                        //Console.WriteLine(new { candidate.c.ConnectionString, csbconn = csb.ConnectionString });
                        var flag = candidate.c.ConnectionString == csb.ConnectionString; // && candidate.ThreadID == Thread.CurrentThread.ManagedThreadId;
                        if (flag)
                        {
                            c = candidate.c;
                            break;
                        }

                        restore += delegate
                        {
                            lookup.Enqueue(candidate);
                        };
                    }
                }

                restore();

                if (c == null)
                {
                    c = new SQLiteConnection(csb.ConnectionString);

                    // the magic number
                    c.BusyTimeout = 5000;

                    c.Open();

                    // http://devcon5.blogspot.com/2012/09/threadsafe-in-appengine-gaej.html

                    OpenCounter++;
                }

                return c;
            }

            // http://msdn.microsoft.com/en-us/library/7977ey2c(v=vs.110).aspx
            // To allow the collection to be accessed by multiple threads for reading and writing, you must implement your own synchronization.
            [Obsolete("only to be accessed by methods with [MethodImpl(MethodImplOptions.Synchronized)]")]
            public static readonly Queue<StillUseableForSomeTime> lookup = new Queue<StillUseableForSomeTime>();

            //    Y:\xmoneseservicesweb.ApplicationWebService\staging.java\web\java\System\Data\SQLite\SQLiteConnectionStringBuilderExtensions_StillUseableForSomeTime___c__DisplayClass7.java:22: error: lookup has private access in SQLiteConnectionStringBuilderExtensions_StillUseableForSomeTime
            //SQLiteConnectionStringBuilderExtensions_StillUseableForSomeTime.lookup.Enqueue(this.candidate);
            //                                                               ^

            [MethodImpl(MethodImplOptions.Synchronized)]
            static SQLiteConnection InterlockedOpenOrDispose(SQLiteConnection close, SQLiteConnectionStringBuilder open)
            {
                if (close != null)
                {
                    DisposeCounter++;

                    lookup.Enqueue(
                        new StillUseableForSomeTime { c = close, ThreadID = Thread.CurrentThread.ManagedThreadId }
                    );
                    return null;
                }

                return InternalOpen(open);
            }

            // will different appengine instance start from 0?
            public static long DisposeCounter;

            // http://stackoverflow.com/questions/6140048/difference-between-manual-locking-and-synchronized-methods
            public static void Dispose(SQLiteConnection c)
            {
                InterlockedOpenOrDispose(close: c, open: null);
            }
        }



        public static Action<Action<SQLiteConnection>> AsWithConnection(
            this SQLiteConnectionStringBuilder csb,
            //this DbConnectionStringBuilder csb,

            // used by?
            Action<SQLiteConnection> Initializer = null
            )
        {
            //Console.WriteLine("enter SQLiteConnection " + new { Thread.CurrentThread.ManagedThreadId, StillUseableForSomeTime.OpenCounter });
            // we a re missing :memory: as used in multimon svg draw experiment

            var cc = default(SQLiteConnection);
            var ccc = 0L;

            return y =>
            {
                //Console.WriteLine("at SQLiteConnection");

                if (cc != null)
                {
                    //Console.WriteLine(
                    //    "AsWithConnection reopen SQLiteConnection " + new { StillUseableForSomeTime.OpenCounter, Thread.CurrentThread.ManagedThreadId });

                    // reenty!
                    y(cc);
                    return;
                }

                //Console.WriteLine("AsWithConnection... invoke");

                // X:\jsc.svn\examples\rewrite\Test\TestInlineIncrement\TestInlineIncrement\Program.cs
                ccc = Counter++;


                var c = StillUseableForSomeTime.Open(csb);

                {
                    //Console.WriteLine(
                    //    "AsWithConnection open SQLiteConnection " + new { StillUseableForSomeTime.OpenCounter, Thread.CurrentThread.ManagedThreadId, csb.ConnectionString });

                    cc = c;

                    try
                    {
                        if (Initializer != null)
                            Initializer(c);

                        y(c);
                    }
                    //catch (Exception ex)
                    //{
                    //    var message = new { ex.Message, ex.StackTrace };

                    //    //Console.WriteLine("AsWithConnection... error: " + message);

                    //    //java
                    //    //throw new InvalidOperationException(message.ToString());

                    //    // php
                    //    throw new Exception(message.ToString());
                    //}

                    catch (Exception ex)
                    {
                        // ex.Message = "SQL logic error or missing database\r\nno such function: concat"
                        // ex.Message = "SQL logic error or missing database\r\nno such table: Sheet2"
                        // table Book1.Sheet1 has no column named Sheet2
                        //Console.WriteLine(new { ex.Message, ex.StackTrace });

                        Console.WriteLine();

                        // script: error JSC1000: No implementation found for this native method, please implement [System.Exception.get_StackTrace()]
                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201401/20140101
                        var text = "ScriptCoreLib.Extensions::ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda.WithConnection\n\n error: "
                            + new
                            {
                                ex.Message,
                                ex,
                                ex.StackTrace
                            };

                        Console.WriteLine(text);


                        if (Debugger.IsAttached)
                            Debugger.Break();

                        throw new InvalidOperationException(text);
                    }

                    cc = null;
                }

                //Console.WriteLine("AsWithConnection close SQLiteConnection or pool it for a few seconds?  " + new { StillUseableForSomeTime.OpenCounter, Thread.CurrentThread.ManagedThreadId });
                StillUseableForSomeTime.Dispose(c);


            };
        }


    }
    //#endif

}
