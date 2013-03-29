using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace com.abstractatech.notez.Schema
{
    public class XLocalStorage : XLocalStorageQueries
    {
      
        //        { Message = SQL logic error or missing database

        //near "ContentKey": syntax error, StackTrace =    at System.Data.SQLite.SQLite3.Prepare(SQLiteConnection cnn, String strSql, SQLiteStatement previous, UInt32 timeoutMS, String& strRemain)

        // Data Source cannot be empty.  Use :memory: to open an in-memory database

        //{ Message = [A]System.Data.SQLite.SQLiteConnection cannot be cast to 
        // [B]System.Data.SQLite.SQLiteConnection. 
        // Type A originates from 'System.Data.SQLite, Version=1.0.60.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' in the context 'Default' at location 'C:\Windows\assembly\GAC_32\System.Data.SQLite\1.0.60.0__db937bc2d44ff139\System.Data.SQLite.dll'. 
        // Type B originates from 'System.Data.SQLite, Version=1.0.84.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' in the context 'Default' at location 'C:\Users\Arvo\AppData\Local\Temp\Temporary ASP.NET Files\root\3ea1022a\80f01a9\assembly\dl3\c42db3ab\64c96595_812cce01\System.Data.SQLite.dll'., StackTrace =    at System.Data.SQLite.SQLiteCommand.set_DbConnection(DbConnection value)

        //   at System.Data.SQLite.SQLiteCommand..ctor(String commandText, SQLiteConnection connection, SQLiteTransaction transaction)

        //   at System.Data.SQLite.SQLiteCommand..ctor(String commandText, SQLiteConnection connection)

        //   at com.abstractatech.notez.Schema.XLocalStorageExtensions.Command(Create , SQLiteConnection )

        //   at com.abstractatech.notez.Schema.XLocalStorageExtensions.ExecuteNonQuery(Create , SQLiteConnection )

        //   at com.abstractatech.notez.Schema.XLocalStorage.<.ctor>b__1(SQLiteConnection c)

        //   at System.Data.SQLite.SQLiteConnectionStringBuilderExtensions.<>c__DisplayClass1.<AsWithConnection>b__0(Action`1 y) }

        public SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder
        {
            Version = 3,
            DataSource = "XLocalStorage.sqlite"
        };

        public readonly Action<Action<SQLiteConnection>> WithConnection;

        public XLocalStorage()
        {
            this.WithConnection = csb.AsWithConnection();

            WithConnection(
                c =>
                {
                    new Create().ExecuteNonQuery(c);

                }
            );

        }

        public void Remove(string key)
        {
            WithConnection(
                c =>
                {
                    new Delete { ContentKey = key }.ExecuteNonQuery(c);
                }
            );
        }

        public void Select(Action<dynamic> yield)
        {
            WithConnection(
                c =>
                {
                    new SelectContent().ExecuteReader(c).WithEach(yield);
                }
             );
        }

        public string this[string key]
        {
            set
            {
                WithConnection(
                    c =>
                    {
                        new Delete { ContentKey = key }.ExecuteNonQuery(c);
                        new Insert { ContentKey = key, ContentValue = value }.ExecuteNonQuery(c);
                    }
                );
            }
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

    }

}
