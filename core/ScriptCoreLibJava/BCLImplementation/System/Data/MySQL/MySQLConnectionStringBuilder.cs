using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestAppEngineOrderByThenGroupBy\ApplicationWebService.cs

    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnectionStringBuilder))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.MySQL.MySQLConnectionStringBuilder")]
    public class __MySQLConnectionStringBuilder : __DbConnectionStringBuilder
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131128-nuget
        // X:\jsc.smokescreen.svn\core\javascript\com.abstractatech.analytics\com.abstractatech.analytics\ApplicationWebService.cs

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121014-gae-data

        //Implementation not found for type import :
        //type: System.Data.SQLite.SQLiteConnectionStringBuilder
        //method: Void set_DataSource(System.String)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!


        public string DataSource { get; set; }

        public int Version { get; set; }
        public bool ReadOnly { get; set; }
        public string Uri { get; set; }
        public string Password { get; set; }

        public string UserID { get; set; }
        public string Server { get; set; }

        public string InternalUser { get; set; }
        public string InternalHost { get; set; }
        public string InternalInstanceName { get; set; }

        public static string InternalDefaultInternalInstanceName = "instance_name";

        public __MySQLConnectionStringBuilder()
        {
            this.InternalUser = "root";
            this.InternalHost = "localhost";
            this.InternalInstanceName = InternalDefaultInternalInstanceName;
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



        public override string InternalGetConnectionString()
        {
            var key = new { DataSource, ReadOnly, InternalUser, InternalHost, InternalInstanceName }.ToString();

            Console.WriteLine("InternalGetConnectionString " + new { key });

            InterlockedInternalGetConnectionString(key, this);

            return key;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static __MySQLConnectionStringBuilder InterlockedInternalGetConnectionString(
            string key,
            __MySQLConnectionStringBuilder value = null
            )
        {
            if (value != null)
            {
                lookup[key] = value;
            }

            return lookup[key];
        }

        public static __MySQLConnectionStringBuilder InternalGetConnectionString(string key)
        {
            return InterlockedInternalGetConnectionString(key);
        }

        public static Dictionary<string, __MySQLConnectionStringBuilder> lookup = new Dictionary<string, __MySQLConnectionStringBuilder>();
    }
}
