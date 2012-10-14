using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnectionStringBuilder))]
    internal class __SQLiteConnectionStringBuilder : __DbConnectionStringBuilder
    {
        public string DataSource { get; set; }
        public int Version { get; set; }
        public bool ReadOnly { get; set; }

        protected override string InternalGetConnectionString()
        {

            var r = "";

            r += "Data Source=" + this.DataSource + ";";
            r += "Version=" + ((object)this.Version).ToString() + ";";

            if (this.ReadOnly)
            {
                r += "Read Only=True;";
            }


            return r;
        }
    }

}
