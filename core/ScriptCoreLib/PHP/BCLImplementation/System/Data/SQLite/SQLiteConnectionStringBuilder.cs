using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.PHP.BCLImplementation.System.Data.Common;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.SQLite
{

    [Obsolete("This class should be refactored into __SQLiteConnectionStringBuilder")]
    [Script]
    internal static class __SQLiteConnectionHack
    {
        // public static Context Context;

        public static string MYDATABASE_NAME;
        public static bool ForceReadOnly;

        public static MySQL.LoginInfo MyDBLoginInfo = new MySQL.LoginInfo();
    }


    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnectionStringBuilder))]
    internal class __SQLiteConnectionStringBuilder : __DbConnectionStringBuilder
    {
        public string DataSource { get; set; }
        public int Version { get; set; }
        public bool ReadOnly { get; set; }
        public string Uri { get; set; }
        public string Password { get; set; }


        protected override string InternalGetConnectionString()
        {

            var r = "";

            //r += "Data Source=" + this.DataSource + ";";

            __SQLiteConnectionHack.MYDATABASE_NAME = DataSource; // "MY_DATABASE.sqlite";
            __SQLiteConnectionHack.MyDBLoginInfo.Database = DataSource;     // __SQLiteConnectionHack.MYDATABASE_NAME;
            __SQLiteConnectionHack.MyDBLoginInfo.Host = "localhost";
            __SQLiteConnectionHack.MyDBLoginInfo.User = "root";     //     //"root";
            __SQLiteConnectionHack.MyDBLoginInfo.Pass = "";

            //r += "Version=" + ((object)this.Version)+";";//.ToString() + ";";

            if (this.ReadOnly)
            {
                // r += "Read Only=True;";
                __SQLiteConnectionHack.ForceReadOnly = true;
            }


            return r;
        }
    }

}
