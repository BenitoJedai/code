using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PHPWiki.Schema
{
    public class Pages : PagesQueries
    {

        //Caused by: java.lang.IllegalStateException: java.lang.ClassNotFoundException: com.mysql.jdbc.Driver
        //at com.google.appengine.api.rdbms.dev.LocalRdbmsServiceLocalDriver.registerDriver(LocalRdbmsServiceLocalDriver.java:95)
        // "C:\util\appengine-java-sdk-1.7.4\lib\impl\mysql-connector-java-5.1.22-bin.jar"

        public readonly Action<Action<SQLiteConnection>> WithConnection;
        //public readonly Action<Action<SQLiteConnection>> WithReadOnlyConnection;

        public Pages(string DataSource = "file:PHPWiki.sqlite")
        {
            this.WithConnection = DataSource.xAsWithConnection();
            //this.WithReadOnlyConnection = DataSource.AsWithConnection();

            WithConnection(
               c =>
               {
                   new Create { }.ExecuteNonQuery(c);
               }
            );
        }

        public void InsertContent(InsertContent value)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);
                }
             );
        }

        public void Count(Action<int> yield)
        {
            WithConnection(
                c =>
                {
                    new SelectCount { }.ExecuteReader(c).WithEach(
                        reader =>
                        {
                            int count = reader.count;

                            yield(count);
                        }
                    );
                }
             );
        }

        public void SelectByKey(SelectByKey key, Action<string> yield)
        {
            WithConnection(
                c =>
                {
                    key.ExecuteReader(c).WithEach(
                        reader =>
                        {
                            string Content = reader.Content;

                            yield(Content);
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





        public static Action<Action<SQLiteConnection>> xAsWithConnection(this string DataSource, int Version = 3)
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
