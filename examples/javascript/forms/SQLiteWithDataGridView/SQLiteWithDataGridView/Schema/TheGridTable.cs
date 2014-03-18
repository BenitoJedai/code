using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace SQLiteWithDataGridView.Schema
{
    public class TheGridTable : TheGridTableQueries
    {
        public SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder
        {
            Version = 3
        };

        public SQLiteConnectionStringBuilder csb_admin = new SQLiteConnectionStringBuilder
        {
            Version = 3
        };

        public SQLiteConnectionStringBuilder csb_write = new SQLiteConnectionStringBuilder
        {
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
            WithAdminConnection(
                c =>
                {
                    // can our generated extensions
                    // use interface methods instead?
                    new Create { }.ExecuteNonQuery(c);

                    new CreateLog { }.ExecuteNonQuery(c);
                }
            );
        }

        #region queries

        public void InsertLog(InsertLog value)
        {
            Console.WriteLine("enter InsertLog");
            WithWriteConnection(
                c =>
                {
                    Console.WriteLine("before InsertLog ExecuteNonQuery ");
                    value.ExecuteNonQuery(c);
                    Console.WriteLine("after InsertLog ExecuteNonQuery ");
                }
             );
            Console.WriteLine("exit InsertLog");
        }

        public void Update(Update value)
        {
            Console.WriteLine("enter Update");

            WithWriteConnection(
                c =>
                {
                    Console.WriteLine("before Update ExecuteNonQuery ");
                    value.ExecuteNonQuery(c);
                    Console.WriteLine("after Update ExecuteNonQuery ");
                }
             );
            Console.WriteLine("exit Update");
        }

        public void Insert(Insert value, Action<long> yield)
        {
            //{ Message = "Attempt to write a read-only database\r\nattempt to write a readonly database", StackTrace = "   at System.Data.SQLite.SQLite3.Reset(SQLiteStatement stmt)\r\n   at System.Data.SQLite.SQLite3.Step(SQLiteStatement stmt)\r\n   at System.Data.SQLite.SQLiteDataReader.NextResult()\r\n   at System.Data.SQLite.SQLiteDataReader..ctor(SQLiteCommand cmd, CommandBehavior behave)\r\n   at System.Data.SQLite.SQLiteCommand.ExecuteReader(CommandBehavior behavior)\r\n   at System.Data.SQLite.SQLiteCommand.ExecuteNonQuery()\r\n   at SQLiteWithDataGridView.Schema.TheGridTableExtensions.ExecuteNonQuery(Insert , SQLiteConnection )\r\n   at SQLiteWithDataGridView.Schema.TheGridTable.<>c__DisplayClass13.<Insert>b__12(SQLiteConnection c) in x:\\jsc.svn\\examples\\javascript\\forms\\SQLiteWithDataGridView\\SQLiteWithDataGridView\\Schema\\TheGridTable.cs:line 95\r\n   at SQLiteWithDataGridView.Schema.XX.<>c__DisplayClass1.<AsWithConnection>b__0(Action`1 y) in x:\\jsc.svn\\examples\\javascript\\forms\\SQLiteWithDataGridView\\SQLiteWithDataGridView\\Schema\\TheGridTable.cs:line 161" }


            //WithConnection(
            WithWriteConnection(
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

        public void SelectTransactionKey(Action<long> yield)
        {
            Console.WriteLine("enter SelectTransactionKey");
            WithConnection(
                c =>
                {
                    Console.WriteLine("before SelectTransactionKey ExecuteReader");
                    new SelectTransaction { }.ExecuteReader(c).WithEach(
                        reader =>
                        {
                            long ContentKey = reader.ContentKey;

                            yield(ContentKey);
                        }
                    );
                    Console.WriteLine("after SelectTransactionKey ExecuteReader");
                }
             );
            Console.WriteLine("exit SelectTransactionKey");
        }

        #endregion



    }




    public static partial class XX
    {
        //            new Create { }.ExecuteNonQuery(c);

        public static int ExecuteNonQuery(this SQLiteWithDataGridView.Schema.TheGridTableQueries.Create data, IDbConnection c)
        {
            var x = c.CreateCommand();
            x.CommandText = SQLiteWithDataGridView.Schema.TheGridTableQueries.Create.CommandText;
            return x.ExecuteNonQuery();
        }

        public static int ExecuteNonQuery(this SQLiteWithDataGridView.Schema.TheGridTableQueries.CreateLog data, IDbConnection c)
        {
            var x = c.CreateCommand();
            x.CommandText = SQLiteWithDataGridView.Schema.TheGridTableQueries.CreateLog.CommandText;
            return x.ExecuteNonQuery();
        }

        public static int ExecuteNonQuery(this SQLiteWithDataGridView.Schema.TheGridTableQueries.InsertLog data, IDbConnection c)
        {
            var x = c.CreateCommand();
            x.CommandText = SQLiteWithDataGridView.Schema.TheGridTableQueries.InsertLog.CommandText;

            return x
                .AddParameter("@ContentKey", data.ContentKey)
                .AddParameter("@ContentComment", data.ContentComment)
                .ExecuteNonQuery();
        }

        public static int ExecuteNonQuery(this SQLiteWithDataGridView.Schema.TheGridTableQueries.Update data, IDbConnection c)
        {
            var x = c.CreateCommand();
            x.CommandText = SQLiteWithDataGridView.Schema.TheGridTableQueries.Update.CommandText;

            return x
                .AddParameter("@ContentComment", data.ContentComment)
                .AddParameter("@ContentValue", data.ContentValue)
                .AddParameter("@ContentKey", data.ContentKey)
                .ExecuteNonQuery();
        }


        public static int ExecuteNonQuery(this SQLiteWithDataGridView.Schema.TheGridTableQueries.Insert data, IDbConnection c)
        {
            var x = c.CreateCommand();
            x.CommandText = SQLiteWithDataGridView.Schema.TheGridTableQueries.Insert.CommandText;

            return x
                .AddParameter("@ContentValue", data.ContentValue)
                .AddParameter("@ContentComment", data.ContentComment)
                .AddParameter("@ParentContentKey", data.ParentContentKey)
                .ExecuteNonQuery();
        }

        public static IDbCommand AddParameter(this IDbCommand x, string ParameterName, object Value)
        {
            var p = x.CreateParameter(); p.ParameterName = ParameterName; p.Value = Value; x.Parameters.Add(p);
            return x;
        }

        // 
        public static IDataReader ExecuteReader(this SQLiteWithDataGridView.Schema.TheGridTableQueries.SelectContent data, IDbConnection c)
        {
            var x = c.CreateCommand();
            x.CommandText = SQLiteWithDataGridView.Schema.TheGridTableQueries.SelectContent.CommandText;

            // http://stackoverflow.com/questions/8091214/how-to-make-c-sharp-code-using-ado-net-idbconnection-and-idbcommand-less-verbos
            return x
                .AddParameter("@ParentContentKey1", data.ParentContentKey1)
                .AddParameter("@ParentContentKey2", data.ParentContentKey2)
                .AddParameter("@ParentContentKey3", data.ParentContentKey3)
                .ExecuteReader();
        }

        public static IDataReader ExecuteReader(this SQLiteWithDataGridView.Schema.TheGridTableQueries.SelectContentUpdates data, IDbConnection c)
        {
            var x = c.CreateCommand();
            x.CommandText = SQLiteWithDataGridView.Schema.TheGridTableQueries.SelectContentUpdates.CommandText;

            return x
               .AddParameter("@ToTransaction", data.ToTransaction)
               .AddParameter("@FromTransaction", data.FromTransaction)
               .AddParameter("@ParentContentKey1", data.ParentContentKey1)
               .AddParameter("@ParentContentKey2", data.ParentContentKey2)
               .AddParameter("@ParentContentKey3", data.ParentContentKey3)
               .ExecuteReader();
        }

        public static IDataReader ExecuteReader(this SQLiteWithDataGridView.Schema.TheGridTableQueries.SelectTransaction data, IDbConnection c)
        {
            var x = c.CreateCommand();
            x.CommandText = SQLiteWithDataGridView.Schema.TheGridTableQueries.SelectTransaction.CommandText;

            return x.ExecuteReader();
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

                //using (var c = new SQLiteConnection(csb.ConnectionString))
                //var c = new SQLiteConnection("Data Source=/StressData.s3db");

                // X:\jsc.svn\examples\rewrite\Test\TestSQLiteMSIL\TestSQLiteMSIL\Class1.cs
                using (var c = new SQLiteConnection("Data Source=/StressData.s3db"))
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
