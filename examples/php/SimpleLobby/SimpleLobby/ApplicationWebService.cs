using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SimpleLobby
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
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

        const string DataSource = "VersionCheckV2.sqlite";

         static string ConnectionString
        {
            get
            {
                return new SQLiteConnectionStringBuilder
                {
                    DataSource = DataSource,
                    Version = 3,
                }.ConnectionString;
            }
        }


        //static List<Tuple<string, string, string>> Items = new List<Tuple<string, string, string>>();

        public void UpdateLobby(string id, string x, string y, Action<string> yield)
        {
            //Items.RemoveAll(
            //    p => p.Item1 == id
            //);

            using (var c = new SQLiteConnection(ConnectionString))
            {
                c.Open();

                var DBItems = new InternalSQLiteKeyValueGenericTable { Connection = c, Name = "DBItems" };

                DBItems.String[id] = new InternalSQLiteKeyValueGenericTable.Point { x = x, y = y };
                //Items.Add(Tuple.Create(id, x, y));

                c.Close();

            }

            yield("hack");
        }

        public void ClearLobby(Action<string> yield)
        {
            //Items.Clear();

            yield("hack");
        }

        public void EnumerateLobby(Action<string, string, string> yield)
        {
            using (var c = new SQLiteConnection(ConnectionString))
            {
                c.Open();

                var DBItems = new InternalSQLiteKeyValueGenericTable { Connection = c, Name = "DBItems" };

                foreach (var id in DBItems.GetKeys())
                {
                    var p = DBItems.String[id];

                    yield(id, p.x, p.y);

                }

                c.Close();

            }


            //foreach (var item in Items)
            //{
            //    yield(item.Item1, item.Item2, item.Item3);
            //}

            yield("", "", "");

        }
    }
}
