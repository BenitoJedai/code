using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Security;
using System.Xml.Linq;
using TestSQLiteParameter.Tables;

namespace TestSQLiteParameter
{


    // generated for namespace Tables
    class Table1 : Table1Queries
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;

        //public Table1(Action<Action<SQLiteConnection>> WithConnection)
        public Table1(string DataSource)
        {
            this.WithConnection = DataSource.AsWithConnection();

            WithConnection(
                c =>
                {
                    Console.WriteLine("Table1 create...");

                    new Create().Command(c).ExecuteNonQuery();

                
                    Console.WriteLine("Table1 create... done");
                }
            );
        }

        public void Add(Insert value)
        {
            WithConnection(
                  c =>
                  {
                      Console.WriteLine("before Add");

                      var cmd = value.Command(c);

                      cmd.Parameters.AddWithValue(
                           value
                      );

                      cmd.ExecuteNonQuery();


                      Console.WriteLine("after Add");
                  }
             );
        }

        public void Enumerate(Action<dynamic> yield)
        {
            WithConnection(
                  c =>
                  {
                      var cmd = new SelectAll().Command(c);

                      using (var reader = cmd.ExecuteReader())
                      {
                          while (reader.Read())
                          {
                              yield(new DynamicDataReader(reader));
                          }
                      }
                  }
             );
        }

        public void Last(Action<long> yield)
        {
            WithConnection(
                  c =>
                  {
                      var cmd = new SelectLast().Command(c);

                      using (var reader = cmd.ExecuteReader())
                      {
                          while (reader.Read())
                          {
                              new DynamicDataReader(reader).With(
                                  (dynamic r) =>
                                  {
                                      Console.WriteLine("Last before ContentKey");
                                      long ContentKey = r.ContentKey;

                                      Console.WriteLine("Last before yield");
                                      yield(ContentKey);
                                  }
                              );
                          }
                      }
                  }
             );
        }
    }


}
