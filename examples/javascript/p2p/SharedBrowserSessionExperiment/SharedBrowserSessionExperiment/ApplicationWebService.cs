using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Query.Experimental;
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

        //public int Retry = 3333;

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
        NavigationOrdersNavigate n = new NavigationOrdersNavigate();

        public async Task BindingSourceSynchonization()
        {
            // since Synchronization context is not yet fully understood then
            // we cannot yet do async delay on server.


            this.RowsWithoutKeys.WithEach(
                r =>
                {
                    if (string.IsNullOrEmpty(r.urlString))
                        return;

                    // either select or insert.



                    var s = new NavigationOrdersNavigate().Where(x => x.urlString == r.urlString).FirstOrDefault();
                    if (s != null)
                    {
                        r.Key = s.Key;
                        return;
                    }

                    r.Key = new NavigationOrdersNavigate().Insert(r);
                    // see. either way we are done! :)
                }
            );

            {


                //IncrementalSyncTake = n.Where(x => x.Key > this.IncrementalSyncSkip).ToArray();
                // jsc can you generate ToArray also? thanks

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140413
                var IncrementalSyncSkip = this.IncrementalSyncSkip;
                IncrementalSyncTake = new NavigationOrdersNavigate().Where(x => x.Key > IncrementalSyncSkip).AsEnumerable().ToArray();



                //Console.WriteLine(new { Retry, RowsWithoutKeys.Length });
            }
        }




        public void Dispose()
        {
        }


        public async Task InsertPosition(NavigationOrdersPositionsRow r)
        {
            //return new NavigationOrders.Navigate() += r;
            LastKnownPositionKey = new NavigationOrdersPositions().Insert(r);
        }

        [Obsolete("webview not sending this?")]
        public NavigationOrdersPositionsKey LastKnownPositionKey = default(NavigationOrdersPositionsKey);

      

        public
            async
            Task<NavigationOrdersPositionsRow> GetLastPosition()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140413
            var LastKnownPositionKey = this.LastKnownPositionKey;

            // I/System.Console(30556): getType is unavailable at API 8

            Console.WriteLine("before " + new { LastKnownPositionKey });

            var r = new NavigationOrdersPositions().Where(x => x.Key > LastKnownPositionKey).OrderByDescending(x => x.Key).FirstOrDefault();

            if (r != null)
            {
                this.LastKnownPositionKey = r;
                Console.WriteLine("after " + new { LastKnownPositionKey });

                return r;
            }

            Console.WriteLine("no update " + new { LastKnownPositionKey });

            // we cant send null can we?
            return new NavigationOrdersPositionsRow { };
        }

        //[XmlException: Root element is missing.]
        //   System.Xml.XmlTextReaderImpl.Throw(Exception e) +133
        //   System.Xml.XmlTextReaderImpl.ThrowWithoutLineInfo(String res) +82
        //   System.Xml.XmlTextReaderImpl.ParseDocumentContent() +1573
        //   System.Xml.XmlTextReaderImpl.Read() +126
        //   System.Xml.XmlReader.MoveToContent() +151
        //   System.Xml.Linq.XElement.Load(XmlReader reader, LoadOptions options) +120
        //   System.Xml.Linq.XElement.Parse(String text, LoadOptions options) +144
        //   System.Xml.Linq.XElement.Parse(String text) +38
        //   SharedBrowserSessionExperiment.DataLayer.ConvertToString$5$&lt;02000013&gt;.ConvertFromString(String ) +133
        //   SharedBrowserSessionExperiment.DataLayer.&lt;02000013Array\+ConvertFromString&gt;.ConvertFromString(String ) +351
        //   SharedBrowserSessionExperiment.Global.Invoke(InternalWebMethodInfo ) +524
        //   ScriptCoreLib.Ultra.WebService.InternalGlobalExtensions.InternalApplication_BeginRequest(InternalGlobal g) +2551
        //   SharedBrowserSessionExperiment.Global.Application_BeginRequest(Object , EventArgs ) +40
        //   System.Web.SyncEventExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute() +238
        //   System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean&amp; completedSynchronously) +114

    }
}
