using ScriptCoreLib;
using ScriptCoreLib.PHP.IO;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

using Serializable = System.SerializableAttribute;


namespace ScriptCoreLib.PHP.Runtime
{



    [Script]
    public class MySQL
    {
        public static void DumpTable(string text, params IArray[] e)
        {
            TextWriter w = new TextWriter();

            w.WriteLine("<table border='1'>");

            int i = 0;

            foreach (IArray v in e)
            {
                if (i == 0)
                {
                    w.WriteLine("<tr>");

                    foreach (string k in v.Keys.ToArray())
                    {
                        w.WriteLine("<th><code>");
                        w.WriteLine(k);
                        w.WriteLine("</code></th>");

                    }

                    w.WriteLine("</tr>");

                }

                w.WriteLine("<tr>");

                i++;

                

                foreach (string k in  v.Keys.ToArray())
	            {
                    w.WriteLine("<td><code>");

                    string n =  Convert.ToReadableSring("" + v[k]);

                

                    w.WriteLine(n);

                    w.WriteLine("</code></td>");
	            }

                w.WriteLine("</tr>");
            }

            w.WriteLine("</table>");

            Native.Message(text, w.Text, Color.Blue, false);
        }

        [Serializable]
        [Script]
        public class LoginInfo
        {
            public string Host;
            public string User;
            public string Pass;
            public string Database;

            public bool Connect()
            {
                Native.Trace("LoginInfo.Connect");

                return MySQL.Connect(this);
            }
        }

        [Script]
        public class CommandBuilder : Command
        {
            public bool AutoEscape = false;

            public FileInfo CommandFile;

            public CommandBuilder(string file): base("")
            {
                CommandFile =FileInfo.OfPath(file);

                Text = CommandFile.Text;
            }



            public string this[string e]
            {
                set
                {
                    Text = Text.Replace(e, value);
                }
            }

            public string this[int e]
            {
                set
                {
                    if (AutoEscape)
                        value = Escape(value);

                    this["{" + e + "}"] = value;
                }
            }

            public string Escape(object p)
            {
                string n = p + "";

                n = n.Replace("%", "");

                return MySQL.API.mysql_real_escape_string(n);
            }

            public static TReturn[] ReadArray<TReturn>(string e, TReturn prototype)
                where TReturn : class
            {
                CommandBuilder c = new CommandBuilder(e);

                return c.ReadTable < TReturn>(prototype);
            }

            public void Strict()
            {
                if (!CommandFile.IsReadable)
                {
                    Native.Error("sql is missing");

                    Native.exit();
                }
            }
        }

        public static bool Connect(LoginInfo e)
        {
            return Connect(e.Host, e.User, e.Pass, e.Database);
        }

        public static bool Connect(string host, string user, string pass, string database)
        {
            Native.Trace("MySQL.API.mysql_connect");

            object p = MySQL.API.mysql_connect(host, user, pass);

            if (p == null)
            {
                Native.Trace("MySQL.API.mysql_connect, failed!");

                return false;
            }
            else
            {
                Native.Trace("MySQL.API.mysql_select_db");

                bool b = MySQL.API.mysql_select_db(database); ;

                Native.Trace("MySQL.API.mysql_select_db, done");

                return b;
            }

        }

        
        [Script]
        public class Command
        {
            public string Text;

            public object Handle;

            public Command(string e)
            {
                Text = e;

            }

            bool bExec;

            public string LastError;

            public bool Verbose = true;

            public int InsertID;

            public bool ExecuteQuery()
            {
                if (Verbose)
                {
                    FileSystemInfo.WriteFile("sql.log", Native.DateTime + "\t" + Text + "\n\r", true);
                }

                Handle = API.mysql_query(Text);

                bExec = (Handle != null);

                if (!bExec)
                    LastError = API.mysql_error();
                else
                {
                    LastError = null;

                    InsertID = API.mysql_insert_id();
                }

                return bExec;
            }

            public int ReadInt(int ordinal)
            {
                IArray r = ReadArray(API.FetchArrayResult.MYSQL_NUM);

                return int.Parse( r[ordinal] + "" );
            }

            public IArray ReadArray()
            {
                return ReadArray(API.FetchArrayResult.MYSQL_ASSOC);
            }

            public IArray ReadArray(API.FetchArrayResult e)
            {
                if (!bExec)
                {
                    if (!ExecuteQuery())
                        return null;
                }

                return API.mysql_fetch_array(Handle, e);
            }

            private TReturn Read<TReturn>(TReturn prototype)
                where TReturn :class
            {
                return Expando.Copy(prototype, API.mysql_fetch_array(Handle, API.FetchArrayResult.MYSQL_ASSOC));
            }

            public IArray[] ReadTable()
            {
                List<IArray> a = new List<IArray>();

                IArray p = ReadArray();

                while (p != null)
                {
                    a.Add(p);

                    p = ReadArray();
                }

                return a.ToArray();
            }

            /// <summary>
            /// reads a table
            /// </summary>
            /// <typeparam name="TReturn"></typeparam>
            /// <param name="prototype"></param>
            /// <returns></returns>
            public TReturn[] ReadTable<TReturn>(TReturn prototype)
                where TReturn : class
            {
                if (!bExec)
                {
                    if (!ExecuteQuery())
                        return null;
                }



                bool bOk = true;
                IArray<int, TReturn> a = new IArray<int, TReturn>();



                Expando q = Expando.Of(prototype);

                int counter = 0;

                while (bOk && counter < 16)
                {

                    TReturn p = this.Read((TReturn)q.Clone());

                    if (p != null)
                    {

                        a.Push(p);


                    }
                    else
                        break;
                }


                return a;
            }

        }

        public static class API
        {
            #region int mysql_insert_id ( [resource link_identifier] )

            /// <summary>
            /// Retrieves the ID generated for an AUTO_INCREMENT column by the previous INSERT query. 
            /// </summary>
            /// <param name="_undefined"></param>
            [Script(IsNative = true)]
            public static int mysql_insert_id() { return default(int); }

            #endregion


            #region string mysql_real_escape_string ( string unescaped_string [, resource link_identifier] )

            /// <summary>
            /// undefined
            /// </summary>
            /// <param name="_unescaped_string">string unescaped_string</param>
            [Script(IsNative = true)]
            public static string mysql_real_escape_string(string _unescaped_string) { return default(string); }

            #endregion


            #region object mysql_connect

            // TODO: handle optional parameters or paramlist of mysql_connect
            /// <summary>
            /// Opens or reuses a connection to a MySQL server.
            /// </summary>
            /// <param name="_server">string server</param>
            /// <param name="_username">string username</param>
            /// <param name="_password">string password</param>
            [Script(IsNative = true, NoExeptions=true)]
            public static object mysql_connect(string _server, string _username, string _password) { return default(object); }

            #endregion

            #region bool mysql_select_db

            /// <summary>
            /// Sets the current active database on the server that's associated with the specified link identifier. Every subsequent call to mysql_query() will be made on the active database. 
            /// </summary>
            /// <param name="_database_name">string database_name</param>
            [Script(IsNative = true)]
            public static bool mysql_select_db(string _database_name) { return default(bool); }

            #endregion

            #region object mysql_query

            /// <summary>
            /// mysql_query() sends a query (to the currently active database on the server that's associated with the specified link_identifier). 
            /// </summary>
            /// <param name="_query">string query</param>
            [Script(IsNative = true)]
            public static object mysql_query(string _query) { return default(object); }

            #endregion

            #region object mysql_fetch_array

            public enum FetchArrayResult
            {
                MYSQL_ASSOC = 1, MYSQL_NUM, MYSQL_BOTH
            }
            /// <summary>
            /// Returns an array that corresponds to the fetched row and moves the internal data pointer ahead. 
            /// </summary>
            /// <param name="_result">resource result</param>
            /// <param name="_result_type">int result_type</param>
            [Script(IsNative = true)]
            public static IArray mysql_fetch_array(object _result, FetchArrayResult _result_type) { return default(IArray); }

            #endregion


            #region string mysql_error ( [resource link_identifier] )

            /// <summary>  
            /// undefined  
            /// </summary>  
            /// <param name="_undefined"></param>  
            [Script(IsNative = true)]
            public static string mysql_error() { return default(string); }

            #endregion






        }
    }
}
