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

        public NavigationOrdersNavigateKey IncrementalSyncSkip = default(NavigationOrdersNavigateKey);
        public NavigationOrdersNavigateRow[] IncrementalSyncTake = new NavigationOrdersNavigateRow[0];



        //at ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda.WithConnection(String DataSource)
        //at SharedBrowserSessionExperiment.DataLayer.Data.NavigationOrders.Navigate.Queries..ctor(String DataSource)
        //at SharedBrowserSessionExperiment.DataLayer.Data.NavigationOrders.Navigate..ctor(String DataSource)
        //at SharedBrowserSessionExperiment.ApplicationWebService..ctor() in x:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\ApplicationWebService.cs:line 38 

        // a component? async datasource?
        [Obsolete(" ah we cannot use this more than once, since we are still using mutable builder", true)]
        NavigationOrders.Navigate n = new NavigationOrders.Navigate();

        public async Task BindingSourceSynchonization()
        {
            // since Synchronization context is not yet fully understood then
            // we cannot yet do async delay on server.


            this.RowsWithoutKeys.WithEach(
                r =>
                {
                    // either select or insert.


                    var s = new NavigationOrders.Navigate().Where(x => x.urlString == r.urlString).FirstOrDefault();
                    if (s != null)
                    {
                        r.Key = s.Key;
                        return;
                    }

                    r.Key = new NavigationOrders.Navigate().Insert(r);
                    // see. either way we are done! :)
                }
            );

            {


                //IncrementalSyncTake = n.Where(x => x.Key > this.IncrementalSyncSkip).ToArray();
                // jsc can you generate ToArray also? thanks
                IncrementalSyncTake = new NavigationOrders.Navigate().Where(x => x.Key > this.IncrementalSyncSkip).AsEnumerable().ToArray();



                Console.WriteLine(new { Retry, RowsWithoutKeys.Length });
            }
        }


        public async Task<NavigationOrdersPositionsKey> InsertPosition(NavigationOrdersPositionsRow r)
        {
            //return new NavigationOrders.Navigate() += r;
            return new NavigationOrders.Positions().Insert(r);
        }


        public void Dispose()
        {
        }
    }
}
