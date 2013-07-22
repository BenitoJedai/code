using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnectionStringBuilder))]
    internal class __SQLiteConnectionStringBuilder : __DbConnectionStringBuilder
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121014-gae-data

        public string DataSource { get; set; }
        public int Version { get; set; }
        public bool ReadOnly { get; set; }
        public string Uri { get; set; }
        public string Password { get; set; }

        public string InternalUser { get; set; }
        public string InternalHost { get; set; }
        public string InternalInstanceName { get; set; }


        public __SQLiteConnectionStringBuilder()
        {
            this.InternalUser = "root";
            this.InternalHost = "localhost";
            this.InternalInstanceName = "instance_name";
            this.Password = "";
        }

        public override void InternalAdd(string keyword, object value)
        {
            if (keyword == "InternalUser")
                this.InternalUser = (string)value;
            else if (keyword == "InternalHost")
                this.InternalHost = (string)value;
            else if (keyword == "InternalInstanceName")
                this.InternalInstanceName = (string)value;
        }

        public static __SQLiteConnectionStringBuilder InternalConnectionString;

        public override string InternalGetConnectionString()
        {
            var r = "";

            // we should serialize to string here
            // this will break once multiple connections are needed!
            InternalConnectionString = this;

            return r;
        }
    }
}
