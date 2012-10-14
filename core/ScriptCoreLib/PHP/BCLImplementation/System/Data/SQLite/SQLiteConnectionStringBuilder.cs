using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.SQLite
{

    [Obsolete("This class should be refactored into __SQLiteConnectionStringBuilder")]
    [Script]
    internal static class __SQLiteConnectionHack
    {
        // public static Context Context;

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

        public string InternalUser { get; set; }
        public string InternalHost { get; set; }

        public __SQLiteConnectionStringBuilder()
        {
            this.InternalUser = "root";
            this.InternalHost = "localhost";
            this.Password = "";
        }

        public override void InternalAdd(string keyword, object value)
        {
            if (keyword == "InternalUser")
                this.InternalUser = (string)value;
            else if (keyword == "InternalHost")
                this.InternalHost = (string)value;


        }

        protected override string InternalGetConnectionString()
        {

            var r = "";

            //r += "Data Source=" + this.DataSource + ";";

            __SQLiteConnectionHack.MyDBLoginInfo.Database = DataSource;     // __SQLiteConnectionHack.MYDATABASE_NAME;
            __SQLiteConnectionHack.MyDBLoginInfo.Host = this.InternalHost;
            __SQLiteConnectionHack.MyDBLoginInfo.User = this.InternalUser;     //     //"root";
            __SQLiteConnectionHack.MyDBLoginInfo.Pass = this.Password;

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
