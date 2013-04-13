﻿using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Abstractatech.JavaScript.FileStorage.Schema
{
    class FileStorageTable : FileStorageQueries
    {
        public SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder
        {
            DataSource = "FileStorage7.sqlite",
            Version = 3
        };

        public readonly Action<Action<SQLiteConnection>> WithConnection;


        public FileStorageTable()
        {
            this.WithConnection = csb.AsWithConnection();

            WithConnection(
                c =>
                {
                    new Create { }.ExecuteNonQuery(c);
                    //new CreateLog { }.ExecuteNonQuery(c);
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
    }


    public static partial class XX
    {
        // jsc cannot handle generic that only differ in generic arguments?
        public static void WithEachReader(this SQLiteDataReader reader, Action<IDataReader> y)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    y(new DynamicDataReader(reader));
                }
            }
        }

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
