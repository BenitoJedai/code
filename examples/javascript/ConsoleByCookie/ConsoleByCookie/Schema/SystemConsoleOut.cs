using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ConsoleByCookie.Schema
{
    class SystemConsoleOut : SystemConsoleOutQueries
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;

        public SystemConsoleOut(string DataSource = "ConsoleByCookie.sqlite")
        {
            this.WithConnection = DataSource.AsWithConnection();

            WithConnection(
               c =>
               {
                   new Create { }.ExecuteNonQuery(c);
               }
            );
        }

        public void InsertContent(InsertContent value, Action<long> y)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);

                    y(c.LastInsertRowId);
                }
             );
        }

        public void SelectTransactionKey(SelectTransaction e, Action<long> yield)
        {
            WithConnection(
                c =>
                {
                    e.ExecuteReader(c).WithEach(
                        reader =>
                        {
                            long id = reader.id;

                            yield(id);
                        }
                    );
                }
             );
        }

        public void SelectContentUpdates(SelectContentUpdates value, Action<dynamic> yield)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteReader(c).WithEach(yield);
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





        public static Action<Action<SQLiteConnection>> AsWithConnection(this string DataSource, int Version = 3)
        {
            //Console.WriteLine("AsWithConnection...");

            return y =>
            {
                //Console.WriteLine("AsWithConnection... invoke");

                using (var c = DataSource.ToConnection(Version))
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

        public static SQLiteConnection ToConnection(this string DataSource, int Version = 3)
        {
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = Version
            };

            var c = new SQLiteConnection(csb.ConnectionString);

            return c;
        }
    }

}
