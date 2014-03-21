using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSqlTimeDifference.Design;

namespace TestSqlTimeDifference
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        public void fake()
        {
            Type sqlLitec = typeof(SQLiteConnection);
            Type ext = typeof(System.Data.SQLite.SQLiteConnectionStringBuilderExtensions);
        }

        public async Task<TestTestRow> getLastEntry()
        {
            return (from c in new Test.Test()
                    orderby c.Key descending
                    select c).FirstOrDefault();

        }
        public async Task InsertNewRow(string s)
        {
            var row = new TestTestRow {col = s};

            var i = new Test.Test();
            i.Insert(row);
            return;
        }
    }
}
