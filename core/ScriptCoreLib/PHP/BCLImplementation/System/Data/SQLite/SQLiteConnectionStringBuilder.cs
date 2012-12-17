using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnectionStringBuilder))]
    internal class __SQLiteConnectionStringBuilder : __DbConnectionStringBuilder
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteConnectionStringBuilder.cs

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

        public static __SQLiteConnectionStringBuilder InternalConnectionString;

        protected override string InternalGetConnectionString()
        {
            var r = "";

            // we should serialize to string here
            // this will break once multiple connections are needed!
            InternalConnectionString = this;

            return r;
        }
    }

}
