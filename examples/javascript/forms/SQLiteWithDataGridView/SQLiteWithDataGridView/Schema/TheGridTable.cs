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
                    new Create { }.ExecuteNonQuery(c);
                    new CreateLog { }.ExecuteNonQuery(c);
                }
            );
        }

        #region queries
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
            WithWriteConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);
                }
             );
        }

        public void Update(Update value)
        {
            WithWriteConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);
                }
             );
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

        public void SelectContent(SelectContentByParent value, Action<dynamic> yield)
        {
            if (value.ParentContentKey == null)
            {
                WithConnection(
                    c =>
                    {
                        
                        new SelectContent().ExecuteReader(c).WithEach(yield);
                    }
                 );
            }

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
        #endregion



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
