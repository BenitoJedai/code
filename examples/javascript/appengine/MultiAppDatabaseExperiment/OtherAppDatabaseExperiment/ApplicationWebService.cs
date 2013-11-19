using MultiAppDatabase.Schema.Clients;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OtherAppDatabaseExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        static Type t = typeof(MultiAppDatabase.Schema.ClientsQueries.SelectAll);
        static Type t2 = typeof(SQLiteConnection);

        public void _ClientsTable_Insert(ClientsTable.Insert value)
        {
            new ClientsTable().Insert(value);
        }

        public Task<DataTable> _ClientsTable_SelectAll()
        {
            return  new ClientsTable().Select().ToTaskResult();
        }
        
    }
}
