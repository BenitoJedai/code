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
    // can we have this as a component?
    public class Table1Component : Component, ITable1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        public Table1Component()
        {
            this.InitializeComponent();
        }

        public ITable1 Proxy { get; set; }

        public void Add(Tables.Table1.AddQuery value)
        {
#if DEBUG
            Proxy.With(p => p.Add(value));
#endif
        }

        public void Enumerate(Action<dynamic> yield)
        {
#if DEBUG
            Proxy.With(p => p.Enumerate(yield));
#endif
        }
    }

    public interface ITable1
    {
#if DEBUG

        void Add(Tables.Table1.AddQuery value);
        void Enumerate(Action<dynamic> yield);
#endif
    }

    // generated for namespace Tables
    class Table1 : ITable1
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
                                      yield(r.ContentKey);
                                  }
                              );
                          }
                      }
                  }
             );
        }
    }


}
