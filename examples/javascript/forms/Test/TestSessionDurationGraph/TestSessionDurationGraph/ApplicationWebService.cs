using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSessionDurationGraph.Data;

namespace TestSessionDurationGraph
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        //    <h2> <i>Could not load file or assembly 'ScriptCoreLib.Extensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.</i> </h2></span>
        // <h2> <i>Could not load file or assembly 'System.Data.XSQLite, 

        void References()
        {
            { var ref0 = typeof(ScriptCoreLib.Shared.Data.Diagnostics.QueryStrategyExtensions); }
            { var ref0 = typeof(System.Data.SQLite.SQLiteConnection); }
        }

        public Stopwatch sw = Stopwatch.StartNew();

        public void Closing()
        {
            var row = new SessionDataSessionTimeRow();
            row.ElapsedTime = sw.ElapsedMilliseconds.ToString();
            var t = new SessionData.SessionTime();
            t.Insert(row);
            sw = Stopwatch.StartNew();

        }

        public Task<DataTable> getSessionTable()
        {
            return (from s in new SessionData.SessionTime()
                    orderby s.Key ascending
                    select s);
        }


    }
}
