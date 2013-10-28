using com.abstractatech.notez.Schema;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

//namespace com.abstractatech.notez
namespace com.abstractatech.wiki
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService :
        // can we do explicit implementations too?
        Abstractatech.JavaScript.FileStorage.IApplicationWebServiceX
    {
        // jsc does not yet look deep enough
        Type ref0 = typeof(System.Data.SQLite.SQLiteCommand);
        Type ref1 = typeof(ScriptCoreLib.Shared.Data.DynamicDataReader);

        // { Message = Could not load file or assembly 'ScriptCoreLib.Extensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified., StackTrace =    at Abstractatech.JavaScript.FileStorage.Schema.XX.WithEach(SQLiteDataReader reader, Action`1 y)







        #region service
         Abstractatech.JavaScript.FileStorage.ApplicationWebService service = new Abstractatech.JavaScript.FileStorage.ApplicationWebService();



        public void DeleteAsync(long Key, Action done = null)
        {
            service.DeleteAsync(Key, done);

        }

        public void EnumerateFilesAsync(Abstractatech.JavaScript.FileStorage.AtFile y, Action<string> done = null)
        {
            service.EnumerateFilesAsync(y, done);
        }

        public void GetTransactionKeyAsync(Action<string> done = null)
        {
            service.GetTransactionKeyAsync(done);
        }

        public void UpdateAsync(long Key, string Value, Action done = null)
        {
            service.UpdateAsync(Key, Value, done);
        }
        #endregion


        public void InternalHandler(WebServiceHandler h)
        {
            // HTTP routing? how to do this more elegantly?
            service.InternalHandler(h);
        }
    
        SQLiteConnectionStringBuilder ref00;

        XLocalStorage data = new XLocalStorage();


        public void get_LocalStorage(Action<string, string> add_localStorage, Action done)
        {
            var count = 0;

            data.Select(
                r =>
                {
                    count++;


                    string key = r.ContentKey;
                    string value = r.ContentValue;


                    add_localStorage(key, value);
                }
            );

            if (count == 0)
            {
                #region default text
                var now = DateTime.Now;


                var yyyy = now.Year;
                var mm = now.Month;
                var dd = now.Day;


                var yyyymmdd = yyyy
                    + mm.ToString().PadLeft(2, '0')
                    + dd.ToString().PadLeft(2, '0');



                var InnerHTML = @"

<div><font face='Verdana' size='5' color='#0000fc'>" + yyyymmdd + @" Hello world</font></div><div><br /></div><blockquote style='margin: 0 0 0 40px; border: none; padding: 0px;'></blockquote><font face='Verdana'>This is your content.</font>

            ";
                #endregion

                add_localStorage(yyyymmdd + " Hello world", InnerHTML);
            }
            // Send it back to the caller.
            done();
        }


        public void remove_LocalStorage(string key, Action yield = null)
        {
            Console.WriteLine("remove_LocalStorage: " + new { key });

            data.Remove(key);

            if (yield != null)
                yield();
        }

        public void set_LocalStorage(string key, string value, Action yield = null)
        {

            Console.WriteLine("set_LocalStorage: " + new { key, value });
            data[key] = value;


            if (yield != null)
                yield();
        }


        public void WhenReady(Action yield)
        {
            yield();
        }

    }

    // jsc could auto implement recovery
    public sealed class ApplicationWebServiceWithReplay
    {
        public ApplicationWebService service = new ApplicationWebService();

        public void get_LocalStorage(Action<string, string> add_localStorage, Action done)
        {
            // pass thru
            service.get_LocalStorage(add_localStorage, done);
        }

        public Queue<Action<Action>> actions = new Queue<Action<Action>>();
        public Stopwatch CurrentPending;

        public event Action<int> AtPendingActions;
        public Stopwatch ServicePending = new Stopwatch();

        public ApplicationWebServiceWithReplay()
        {
            var sync = new Timer(
                delegate
                {
                    // in every 500ms we get to do stuff.

                    if (CurrentPending == null)
                    {
                        // there is nothing pending just yet.
                        // do we have work?

                        if (actions.Count > 0)
                        {
                            var next = actions.Peek();

                            // alright, we know what to do
                            // start our timeout

                            var timeout = new Stopwatch();

                            timeout.Start();

                            CurrentPending = timeout;

                            Console.WriteLine("start task " + new { actions.Count });


                            // .net does not need this?
                            if (!ServicePending.IsRunning)
                                ServicePending.Start();


                            if (AtPendingActions != null)
                                AtPendingActions(actions.Count);

                            next(
                                delegate
                                {
                                    if (CurrentPending == timeout)
                                    {
                                        // we have been expecting you..

                                        Console.WriteLine("task complete " + new { timeout.ElapsedMilliseconds, actions.Count });

                                        CurrentPending = null;
                                        actions.Dequeue();

                                        if (actions.Count == 0)
                                        {
                                            ServicePending = new Stopwatch();
                                            //ServicePending.Reset();
                                        }

                                        if (AtPendingActions != null)
                                            AtPendingActions(actions.Count);

                                    }
                                }
                            );
                        }

                    }
                    else
                    {
                        // there is something pending

                        // lag 4000 and we will need to retry
                        if (CurrentPending.ElapsedMilliseconds > 4000)
                        {
                            Console.WriteLine("task abort  " + new { actions.Count });
                            CurrentPending = null;



                        }

                        if (AtPendingActions != null)
                            AtPendingActions(actions.Count);
                    }


                }
            );

            sync.StartInterval(500);
        }

        public void remove_LocalStorage(string key)
        {
            actions.Enqueue(
                yield =>
                {
                    service.remove_LocalStorage(key,

                        yield: yield
                    );


                }
            );


            if (AtPendingActions != null)
                AtPendingActions(actions.Count);
        }

        public void set_LocalStorage(string key, string value)
        {
            actions.Enqueue(
                 yield =>
                 {
                     service.set_LocalStorage(key, value,

                         yield: yield
                     );


                 }
             );


            if (AtPendingActions != null)
                AtPendingActions(actions.Count);
        }
    }
}
