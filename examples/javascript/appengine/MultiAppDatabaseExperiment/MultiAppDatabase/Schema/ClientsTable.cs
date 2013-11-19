using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MultiAppDatabase.Schema.Clients
{
   public class ClientsTable : ClientsQueries
    {
        public readonly Func<Insert, long> Insert;
        public readonly Func<DataTable> Select;

#if DEBUG
       
#endif
        public ClientsTable(string DataSource = @"G:\Clients.sqlite")
        {
            #region WithConnection
            Action<Action<SQLiteConnection>> WithConnection = y =>
            {
                var csb = new SQLiteConnectionStringBuilder
                {
                    DataSource = DataSource,

                    // is there any other version?
                    Version = 3
                };


                using (var c = new SQLiteConnection(csb.ConnectionString))
                {
                    c.Open();

                    try
                    {
                        y(c);
                    }
                    catch (Exception ex)
                    {
                        // ex.Message = "SQL logic error or missing database\r\nno such function: concat"
                        // ex = {"Could not load file or assembly 'System.Data.SQLite, Version=1.0.86.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT:...
                        Console.WriteLine(new { ex.Message, ex.StackTrace });
                        Debugger.Break();
                    }
                }
            };
            #endregion

            WithConnection(
                c =>
                {
                    new Create { }.ExecuteNonQuery(c);
                }
            );

            #region Insert
            this.Insert = value =>
            {
                var x = default(long);

                WithConnection(
                    c =>
                    {
                        value.ExecuteNonQuery(c);
                        x = c.LastInsertRowId;
                    }
                );

                return x;
            };
            #endregion

              #region Select
            this.Select = delegate
            {
                var t = default(DataTable);

                WithConnection(
                  c =>
                  {
                      t = new SelectAll { }.GetDataTable(c);
                  }
                );

                return t;
            };
            #endregion
        }
    }
}
