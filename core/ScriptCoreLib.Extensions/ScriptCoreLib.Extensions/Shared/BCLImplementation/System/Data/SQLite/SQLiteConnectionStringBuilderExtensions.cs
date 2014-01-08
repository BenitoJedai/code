using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace System.Data.SQLite
{
    public static class SQLiteConnectionStringBuilderExtensions
    {
        public static long Counter = 0;


        public class StillUseableForSomeTime
        {
            public SQLiteConnection c;
            public readonly Stopwatch w = Stopwatch.StartNew();

            public static object SyncLock = new object();

            public static long OpenCounter = 0L;

            public static SQLiteConnection Open(SQLiteConnectionStringBuilder csb)
            {
                var c = default(SQLiteConnection);

                lock (SyncLock)
                {
                    c = InternalOpen(csb, c);
                }

                return c;
            }

            private static SQLiteConnection InternalOpen(SQLiteConnectionStringBuilder csb, SQLiteConnection c)
            {
                Action restore = delegate { };

                while (lookup.Count > 0)
                {
                    // look at the pool

                    var candidate = lookup.Dequeue();

                    // the pool timeout
                    if (candidate.w.ElapsedMilliseconds > 5000)
                    {
                        candidate.c.Dispose();
                    }
                    else
                    {
                        // doe the dbs match?

                        if (candidate.c.ConnectionString == csb.ConnectionString)
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

                c = new SQLiteConnection(csb.ConnectionString);
                OpenCounter++;
                return c;
            }

            static readonly Queue<StillUseableForSomeTime> lookup = new Queue<StillUseableForSomeTime>();

            public static void Dispose(SQLiteConnection c)
            {
                lock (SyncLock)
                {
                    lookup.Enqueue(
                        new StillUseableForSomeTime { c = c }
                    );
                }

                //c.Dispose();
            }
        }



        public static Action<Action<SQLiteConnection>> AsWithConnection(
            this SQLiteConnectionStringBuilder csb,
            Action<SQLiteConnection> Initializer = null
            )
        {
            Console.WriteLine("enter SQLiteConnection " + new { Thread.CurrentThread.ManagedThreadId, StillUseableForSomeTime.OpenCounter });
            // we a re missing :memory: as used in multimon svg draw experiment

            var cc = default(SQLiteConnection);
            var ccc = 0L;

            return y =>
            {
                Console.WriteLine("at SQLiteConnection");

                if (cc != null)
                {
                    Console.WriteLine("reopen SQLiteConnection " + new { StillUseableForSomeTime.OpenCounter, Thread.CurrentThread.ManagedThreadId });

                    // reenty!
                    y(cc);
                    return;
                }

                //Console.WriteLine("AsWithConnection... invoke");


                ccc = Counter++;


                var c = StillUseableForSomeTime.Open(csb);

                {
                    Console.WriteLine("open SQLiteConnection " + new { StillUseableForSomeTime.OpenCounter, Thread.CurrentThread.ManagedThreadId });
                    c.Open();

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
                        // ex = {"Could not load file or assembly 'System.Data.SQLite, Version=1.0.86.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT:...
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

                        Debugger.Break();

                        throw new InvalidOperationException(text);
                    }

                    cc = null;
                }

                Console.WriteLine("close SQLiteConnection or pool it for a few seconds?  " + new { StillUseableForSomeTime.OpenCounter, Thread.CurrentThread.ManagedThreadId });
                StillUseableForSomeTime.Dispose(c);


            };
        }


    }
}
