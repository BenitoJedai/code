using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using SharedBrowserSessionExperiment.DataLayer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SharedBrowserSessionExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService : Component, IDisposable
    {
        // Could not load file or assembly 'ScriptCoreLib.Extensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.
        // Could not load file or assembly 'System.Data.XSQLite, Version=3.7.7.1, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.
        void References()
        {
            { var r = typeof(global::ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambdaZ); }
            { var r = typeof(global::System.Data.SQLite.SQLiteConnection); }
        }

        public int Retry = 3333;

        //public DataRow[] RowsWithoutKeys = new DataRow[0];
        public NavigationOrdersNavigateRow[] RowsWithoutKeys = new NavigationOrdersNavigateRow[0];



        // a component? async datasource?
        NavigationOrders.Navigate n = new NavigationOrders.Navigate();

        public async Task BindingSourceSynchonization()
        {
            // since Synchronization context is not yet fully understood then
            // we cannot yet do async delay on server.


            this.RowsWithoutKeys.WithEach(
                r =>
                {
                    // either select or insert.

                    var s = n.Where(x => x.urlString == r.urlString).FirstOrDefault();
                    if (s != null)
                    {
                        r.Key = s.Key;
                        return;
                    }

                    r.Key = n.Insert(r);
                    // see. either way we are done! :)
                }
            );


            Console.WriteLine(new { Retry, RowsWithoutKeys.Length });
        }




        public void Dispose()
        {
        }
    }
}
