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
    class Table1
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;

        //public Table1(Action<Action<SQLiteConnection>> WithConnection)
        public Table1(string DataSource)
        {
            this.WithConnection = DataSource.AsWithConnection();

            WithConnection(
                c =>
                {
                    using (var reader = new SQLiteCommand(
                        Tables.Table1.CreateQuery.GetSource()
                        , c).ExecuteReader())
                    {

                    }
                }
            );
        }

        public void Add(Tables.Table1.AddQuery value)
        {
            WithConnection(
                  c =>
                  {
                      var cmd = new SQLiteCommand(
                          Tables.Table1.AddQuery.GetSource()
                      , c);

                      Tables.Table1.AddQueryExtensions.AddWithValue(
                           cmd.Parameters,
                           value
                      );

                      using (var reader = cmd.ExecuteReader())
                      {

                      }
                  }
             );
        }

        public void Enumerate(Action<dynamic> yield)
        {
            WithConnection(
                  c =>
                  {
                      var cmd = new SQLiteCommand(
                          Tables.Table1.EnumerateQuery.GetSource()
                      , c);



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
                      var cmd = new SQLiteCommand(
                          Tables.Table1.LastQuery.GetSource()
                      , c);



                      using (var reader = cmd.ExecuteReader())
                      {
                          while (reader.Read())
                          {
                              new DynamicDataReader(reader).With(
                                  (dynamic r) =>
                                  {
                                      long ContentKey = r.ContentKey;

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
