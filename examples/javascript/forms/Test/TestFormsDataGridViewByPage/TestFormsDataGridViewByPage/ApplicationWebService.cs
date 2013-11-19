using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;
using System.Diagnostics;

namespace TestFormsDataGridViewByPage
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        public Task<int> GetAllQueryCount()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand command = new SqlCommand("Select * From Logs", conn);
                var dataAdapter = new SqlDataAdapter(command);
                var table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                var ret = table.Rows.Count / 100 + 1;
                var x = new TaskCompletionSource<int>();
                x.SetResult(ret);
                return x.Task;
            }
            catch
            {
                var x = new TaskCompletionSource<int>();
                x.SetResult(0);
                return x.Task;
            }
            
        }

        public Task<int> GetAllItemsCount()
        {
            var connString= @"Server=Buckler-PC\SQLEXPRESS;User Id=test;Database=QueryModule;Password=test;Connection Timeout=5;";
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("Select * From Logs", conn);
            var dataAdapter = new SqlDataAdapter(command);
            var table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            var ret = table.Rows.Count/100+1;
            var x = new TaskCompletionSource<int>();
            x.SetResult(ret);
            return x.Task;
        }
        public Task<DataTable> GetAllItemsFromDB(int page)
        {
            var connString= @"Server=Buckler-PC\SQLEXPRESS;User Id=test;Database=QueryModule;Password=test;Connection Timeout=5;";
            SqlConnection conn = new SqlConnection(connString);
            var pageNumber = (page-1)*100;
            SqlCommand command = new SqlCommand("Select * From Logs", conn);
            conn.Open();
            var reader = command.ExecuteReader();
            var table = new DataTable();
            var count = 0;
            var s = new Stopwatch();
            s.Start();
            while (reader.Read())
            {
                if (count == 0) 
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        table.Columns.Add(reader.GetName(i));
                    }
                }
                if (pageNumber < count && count < pageNumber + 100)
                {
                    var row = table.NewRow();
                    for (var m = 0; m < table.Columns.Count; m++)
                    {
                        row[m] = reader[m].ToString();
                    }
                    table.Rows.Add(row);
                }
                count++;
            }
            conn.Close();
            s.Stop();
            Console.WriteLine(s.ElapsedMilliseconds);
            var x = new TaskCompletionSource<DataTable>();
            x.SetResult(table);
            return x.Task;
        }
    }
}
