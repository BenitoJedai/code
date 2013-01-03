using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace TestNamedParameters.Schema
{
    public class TheGridTable : TheGridTableQueries
    {
        // Data Source cannot be empty.  Use :memory: to open an in-memory database

        public SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder
        {
            DataSource = "TestNamedParameters1",
            Version = 3
        };

        public SQLiteConnectionStringBuilder csb_admin = new SQLiteConnectionStringBuilder
        {
            DataSource = "TestNamedParameters1",
            Version = 3
        };

        public SQLiteConnectionStringBuilder csb_write = new SQLiteConnectionStringBuilder
        {
            DataSource = "TestNamedParameters1",
            Version = 3
        };

        public readonly Action<Action<SQLiteConnection>> WithConnection;
        public readonly Action<Action<SQLiteConnection>> WithAdminConnection;
        public readonly Action<Action<SQLiteConnection>> WithWriteConnection;

        public TheGridTable()
        {
            this.WithConnection = csb.AsWithConnection();
            this.WithAdminConnection = csb_admin.AsWithConnection();
            this.WithWriteConnection = csb_write.AsWithConnection();


        }

        public void Create()
        {
            Console.WriteLine("Create enter");
            WithAdminConnection(
                c =>
                {
                    Console.WriteLine("Create before ExecuteNonQuery");
                    new Create { }.ExecuteNonQuery(c);
                    Console.WriteLine("Create after ExecuteNonQuery");
                }
            );
            Console.WriteLine("Create exit");
        }

        #region queries


        public void Insert(Insert value, Action<long> yield)
        {
            Console.WriteLine("Insert enter");
            WithWriteConnection(
                c =>
                {
                    Console.WriteLine("Insert before ExecuteNonQuery");
                    value.ExecuteNonQuery(c);

                    yield(c.LastInsertRowId);
                    Console.WriteLine("Insert after ExecuteNonQuery");
                }
             );
            Console.WriteLine("Insert exit");
        }

        public void SelectContent(SelectContentByParent value, Action<dynamic> yield)
        {
            Console.WriteLine("SelectContent enter");

            WithConnection(
                c =>
                {
                    Console.WriteLine("SelectContent before ExecuteReader");
                    if (value.ParentContentKey == null)
                    {
                        new SelectContent().ExecuteReader(c).WithEach(yield);

                    }
                    else
                    {
                        value.ExecuteReader(c).WithEach(yield);
                    }
                    Console.WriteLine("SelectContent after ExecuteReader");
                }
             );

            Console.WriteLine("SelectContent exit");
        }


        #endregion



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





        public static Action<Action<SQLiteConnection>> AsWithConnection(this SQLiteConnectionStringBuilder csb)
        {
            //Console.WriteLine("AsWithConnection...");

            return y =>
            {
                //Console.WriteLine("AsWithConnection... invoke");

                using (var c = new SQLiteConnection(csb.ConnectionString))
                {
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
