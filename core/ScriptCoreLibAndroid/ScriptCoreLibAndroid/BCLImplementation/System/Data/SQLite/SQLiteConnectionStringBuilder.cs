using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnectionStringBuilder))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteConnectionStringBuilder")]
    internal class __SQLiteConnectionStringBuilder : __DbConnectionStringBuilder
    {
        public string DataSource { get; set; }
        public int Version { get; set; }
        public bool ReadOnly { get; set; }

        // protected internal virtual string InternalGetConnectionString();
        // { Message = Derived method 'InternalGetConnectionString' in type 'ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite.__SQLiteConnectionStringBuilder' from assembly 'ScriptCoreLibAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' cannot reduce access. }
        public override string InternalGetConnectionString()
        {
            // public  String InternalGetConnectionString()
            //            [javac] T:\src\ScriptCoreLib\Android\BCLImplementation\System\Data\SQLite\__SQLiteConnectionStringBuilder.java:51: error: InternalGetConnectionString() in __SQLiteConnectionStringBuilder cannot override InternalGetConnectionString() in __DbConnectionStringBuilder
            //[javac]     protected  String InternalGetConnectionString()
            //[javac]                       ^

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
