using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace SQLiteWithDataGridView.Schema
{
    public class TheGridTable : TheGridTableQueries
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;
        //public readonly Action<Action<SQLiteConnection>> WithReadOnlyConnection;

        public TheGridTable(string DataSource = "SQLiteWithDataGridView5")
        {
            this.WithConnection = DataSource.AsWithConnection();
            //this.WithReadOnlyConnection = DataSource.AsWithConnection();

            WithConnection(
               c =>
               {
                   new Create { }.ExecuteNonQuery(c);
                   new CreateLog { }.ExecuteNonQuery(c);
               }
            );
        }

        public void SelectTransactionKey(Action<long> yield)
        {
            WithConnection(
                c =>
                {
                    new SelectTransaction { }.ExecuteReader(c).WithEach(
                        reader =>
                        {
                            long ContentKey = reader.ContentKey;

                            yield(ContentKey);
                        }
                    );
                }
             );
        }

        public void InsertLog(InsertLog value)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);
                }
             );
        }

        public void Update(Update value)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);
                }
             );
        }

        public void Insert(Insert value, Action<long> yield)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);

                    yield(c.LastInsertRowId);
                }
             );
        }

        public void SelectContent(SelectContent value, Action<dynamic> yield)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteReader(c).WithEach(yield);
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

                        Console.WriteLine("AsWithConnection... error: " + message);

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
