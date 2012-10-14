using com.google.appengine.api.rdbms;
using java.sql;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Data.SQLite;
using System.Linq;
using System.Xml.Linq;

namespace GAEMeetsNuGetSQLite
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
        public void AddItem(string Key, string Content, Action<string> y)
        {
#if !DEBUG
            // should jsc do this implictly?
            try
            {
                //DriverManager.registerDriver(new AppEngineDriver());
            }
            catch
            {
                throw;
            }
#endif

            using (var c = new SQLiteConnection(

                  new SQLiteConnectionStringBuilder
                  {
                      DataSource = "MY_DATABASE.sqlite",
                      Version = 3
                  }.ConnectionString

            ))
            {
                c.Open();

                using (var cmd = new SQLiteCommand("create table if not exists MY_TABLEXX (XKey text not null, Content text not null)", c))
                {
                    cmd.ExecuteNonQuery();
                }

                new SQLiteCommand("insert into MY_TABLEXX (XKey, Content) values ('" + Key.Replace("'", "\\'") + "', '" + Content.Replace("'", "\\'") + "')", c).ExecuteNonQuery();

                using (var reader = new SQLiteCommand("select count(*) from MY_TABLEXX", c).ExecuteReader())
                {

                    if (reader.Read())
                    {
                        var count = (int)reader.GetInt32(0);

                        y("" + count);

                    }
                }


                c.Close();
            }

        }

    }
}
