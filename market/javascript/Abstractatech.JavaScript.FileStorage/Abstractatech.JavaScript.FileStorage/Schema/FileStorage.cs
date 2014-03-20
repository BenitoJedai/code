using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Abstractatech.JavaScript.FileStorage.Schema
{
    public class FileStorageTable : FileStorageQueries
    {
        public static SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder
        {
            // Invalid connection string: invalid URI

            DataSource = "file:FileStorage7.sqlite",
            Version = 3
        };

        public readonly Action<Action<SQLiteConnection>> WithConnection;


        public FileStorageTable()
        {
            this.WithConnection = FileStorageTable.csb.xAsWithConnection();

            WithConnection(
                c =>
                {
                    new Create { }.ExecuteNonQuery(c);
                }
            );
        }


        public void SelectAll(Action<dynamic> yield)
        {
            WithConnection(
              c =>
              {
                  new SelectAll().ExecuteReader(c).WithEach(yield);
              }
            );
        }

        public void SelectBytes(SelectBytes value, Action<IDataReader> yield)
        {
            WithConnection(
               c =>
               {
                   value.ExecuteReader(c).WithEachReader(yield);
               }
             );
        }

        public void SelectBytesRange(SelectBytesRange value, Action<IDataReader> yield)
        {
            WithConnection(
               c =>
               {
                   value.ExecuteReader(c).WithEachReader(yield);
               }
             );
        }

        public void Delete(Delete value)
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

        public void Update(Update value)
        {
            Console.WriteLine("enter Update");

            //WithWriteConnection(
            WithConnection(
                c =>
                {
                    Console.WriteLine("before Update ExecuteNonQuery ");
                    value.ExecuteNonQuery(c);
                    Console.WriteLine("after Update ExecuteNonQuery ");
                }
            );

            Console.WriteLine("exit Update");
        }
    }


    public static partial class XX
    {
        // jsc cannot handle generic that only differ in generic arguments?
        public static void WithEachReader(this IDataReader reader, Action<IDataReader> y)
        {
            //            [javac] Compiling 494 source files to O:\bin\classes
            //[javac] O:\src\Abstractatech\JavaScript\FileStorage\Schema\FileStorageTable___c__DisplayClass6.java:26: WithEachReader(ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite.__SQLiteDataReader,ScriptCoreLib.Shared.BCLImplementation.System.__Action_1<ScriptCoreLib.Shared.Data.DynamicDataReader>) in Abstractatech.JavaScript.FileStorage.Schema.XX cannot be applied to (ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite.__SQLiteDataReader,ScriptCoreLib.Shared.BCLImplementation.System.__Action_1<ScriptCoreLib.Shared.BCLImplementation.System.Data.__IDataReader>)
            //[javac]         XX.WithEachReader(FileStorageExtensions.ExecuteReader(this.value, c), this.yield);
            //[javac]           ^

            using (reader)
            {
                while (reader.Read())
                {
                    y(new DynamicDataReader(reader));
                }
            }
        }

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





        public static Action<Action<SQLiteConnection>> xAsWithConnection(this SQLiteConnectionStringBuilder csb)
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
