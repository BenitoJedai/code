using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace AccountExperiment.Schema
{
    class MyAccount : MyAccountQueries
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;

        public SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder
        {
            Version = 3,
            DataSource = "AccountExperiment.sqlite"
        };

        public MyAccount()
        {
            this.WithConnection = csb.AsWithConnection();

            WithConnection(
               c =>
               {
                   new Create { }.ExecuteNonQuery(c);
               }
           );
        }


        public long Insert(Insert value)
        {
            var id = -1L;

            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);

                    id = c.LastInsertRowId;
                }
            );

            return id;
        }

        public void SelectByPassword(SelectByPassword value, Action<long> yield)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteReader(c).WithEach(
                        r =>
                        {
                            long id = r.id;

                            yield(id);
                        }
                    );
                }
            );
        }

        public void SelectByCookie(SelectByCookie value, Action<dynamic> yield)
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
