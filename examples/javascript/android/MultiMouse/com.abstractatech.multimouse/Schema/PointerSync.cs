
using ScriptCoreLib.Shared.Data;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;

namespace com.abstractatech.multimouse.Schema
{


    public class PointerSync : PointerSyncQueries
    {
        public const bool ListMode = true;


        public const bool MemoryDataSource = false;
        public const bool PersistentConnection = true;

        readonly Action<Action<SQLiteConnection>> WithConnection;
        readonly Action<Action<SQLiteConnection>> WithWriteConnection;

        //  The database file is locked



        [Obsolete("jsc does ot yet like optional parameters for web service layer")]
        public PointerSync(
            string DataSource = "file:com.abstractatech.multimouse.sqlite")
        // Data Source cannot be empty.  Use :memory: to open an in-memory database
        {


            this.WithConnection = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource
                //    , ReadOnly = true 
            }.xAsWithConnection();
            this.WithWriteConnection = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource
                //, ReadOnly = false 
            }.xAsWithConnection();

            WithWriteConnection(
              c =>
              {
                  if (ListMode)
                  {
                      return;
                  };

                  new Create { }.ExecuteNonQuery(c);
              }
           );
        }

        public static readonly List<Insert> InsertList = new List<Insert>();

        public void Insert(Insert value)
        {
#if DEBUG
            //Console.WriteLine("before Insert");
#endif


            WithWriteConnection(
                c =>
                {
                    if (ListMode)
                    {
                        InsertList.Add(value);

                        // garbage collect

                        // lesser values will cause glitches in slower clients
                        // IE for example is not streaming, this will need a longer buffer for reconnects
                        var memento = 64;
                        if (InsertList.Count > memento)
                            InsertList[InsertList.Count - memento] = null;

                        //Console.Title = ("@ " + InsertList.Count);
                        return;
                    };

                    value.ExecuteNonQuery(c);
                }
             );

#if DEBUG
            //Console.WriteLine("after Insert");
#endif
        }

        public void SelectTransaction(Action<long> yield)
        {


            WithConnection(
                c =>
                {
                    if (ListMode)
                    {
                        yield(InsertList.Count - 1);
                        return;
                    };

                    new SelectTransaction { }.ExecuteReader(c).WithEach(
                        reader =>
                        {
                            long id = reader.id;

                            yield(id);
                        }
                    );
                }
             );
        }

        public void SelectContentUpdates(SelectContentUpdates value, Action<string> yield)
        {


            WithConnection(
                c =>
                {
                    if (ListMode)
                    {
                        // Collection was modified; enumeration operation may not execute.

                        //Implementation not found for type import :
                        //type: System.Linq.Enumerable
                        //method: System.Collections.Generic.IEnumerable`1[com.abstractatech.multimouse.Schema.PointerSyncQueries+Insert] Where[Insert](System.Collections.Generic.IEnumerable`1[com.abstractatech.multimouse.Schema.PointerSyncQueries+Insert], System.Func`3[com.abstractatech.multimouse.Schema.PointerSyncQueries+Insert,System.Int32,System.Boolean])
                        //Did you forget to add the [Script] attribute?
                        //Please double check the signature!

                        //                        Implementation not found for type import :
                        //type: System.Linq.Enumerable
                        //method: System.Collections.Generic.IEnumerable`1[com.abstractatech.multimouse.Schema.PointerSyncQueries+Insert] Skip[Insert](System.Collections.Generic.IEnumerable`1[com.abstractatech.multimouse.Schema.PointerSyncQueries+Insert], Int32)
                        //Did you forget to add the [Script] attribute?
                        //Please double check the signature!

                        //InsertList.Skip(value.FromTransaction + 1).Take(value.ToTransaction - value.FromTransaction).WithEach(

                        for (int i = value.FromTransaction + 1; i <= value.ToTransaction; i++)
                        {
                            // Object reference not set to an instance of an object.

                            // memento got it!
                            if (InsertList[i] != null)
                                yield(InsertList[i].message);

                        }



                        return;
                    };


                    value.ExecuteReader(c).WithEach(
                        r =>
                        {
                            string message = r.message;

                            yield(message);
                        }
                    );
                }
             );
        }
    }

    public static partial class XX
    {
        public static void WithEach(this IDataReader reader, Action<dynamic> y)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    y(new DynamicDataReader(reader));
                }
            }
        }




        static SQLiteConnection AsWithConnection_memory;

        static object synclock = new object();


        public static Action<Action<SQLiteConnection>> xAsWithConnection(this SQLiteConnectionStringBuilder csb)
        {
            //Console.WriteLine("AsWithConnection...");

            if (PointerSync.ListMode)
            {

                return y =>
                {
                    lock (synclock)
                    {
                        y(null);
                    }
                };
            }

            #region :memory:
            if (PointerSync.MemoryDataSource || PointerSync.PersistentConnection)
            {



                return y =>
                {
                    if (AsWithConnection_memory == null)
                    {
                        AsWithConnection_memory = new SQLiteConnection(new SQLiteConnectionStringBuilder
                        {
                            DataSource = PointerSync.MemoryDataSource ?
                            // this wont work with XSQLite?
                                ":memory:"
                                : csb.DataSource
                        }.ConnectionString);
                        AsWithConnection_memory.Open();
                    }

                    y(AsWithConnection_memory);
                };
            }
            #endregion

            return y =>
            {
                //Console.WriteLine("AsWithConnection... invoke");

                using (var c = new SQLiteConnection(csb.ConnectionString))
                {
                    c.BusyTimeout = 1000;

                    c.Open();

                    try
                    {
                        y(c);
                    }
                    catch (Exception ex)
                    {
                        var message = new { ex.Message, ex.StackTrace };

                        //Console.WriteLine("AsWithConnection... error: " + message);

                        //java
                        //throw new InvalidOperationException(message.ToString());

                        // php
                        throw new Exception(message.ToString());
                    }
                }
            };
        }
    }
}
