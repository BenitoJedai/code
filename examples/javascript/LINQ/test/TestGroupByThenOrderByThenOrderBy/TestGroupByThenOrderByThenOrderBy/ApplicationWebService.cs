using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;

namespace TestGroupByThenOrderByThenOrderBy
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService :
        // lets pretend we are a row in a database
        // if we are how can we be preselected?
        // and how can we autolink to the UI?
        Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow,

        // lets look like we can be inserted into
        IAsyncQueryStrategyInsert<Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow, Data.PerformanceResourceTimingData2ApplicationResourcePerformanceKey>
    {
        public string XTag;

        public ApplicationWebService()
        {
            // base fields are not selected to be serialized?
            base.Tag = "i am just a row in a database. the roslyn base type PrimaryConstructor can be used to set fields?";

            this.XTag = "i am just a row in a database. the roslyn base type PrimaryConstructor can be used to set fields?";

            // will the tag be shown?
            // can we autoshow our fields?
            // how?

        }



        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public async Task<Data.PerformanceResourceTimingData2ApplicationResourcePerformanceKey> Insert(Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow value = null)
        {
            // !! could jsc allow access to XML docs and bake that xml in the method?
            // either we would need roslyn to help us or. the pre build event could benefit from xml doc?
            // but it wont be available for pre build event will it?
            // !! what about optional async returns?

            var x = new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance();

            //var x = new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance<Goo>();

            // += ?

            // should the tag be a property bag? a string dictionary instead of just a string?
            // or a dynamic even?
            // generic data tables?

            var keyForClient = x.Insert(value);

            var keys = x.Insert(
                new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 45, path = " /zfoo/BAR/B " },
                new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 44, path = " /zfoo/BAR/A " },
                new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 40, path = " /zfoo/BAR/A " }
            );


            // getting this to work was the first step into enabling reflection baking
            // we will be figting jvm type erasure, enum erasure and js type erasure,


            //GroupBy { keySelector = k => k.duration }
            //MutableOrderBy { ColumnName = Key }
            //Select { selector = gg => new <> f__AnonymousType0`2(duration = gg.Key, count = gg.Count()) }



            var g = from k in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                    group k by k.duration into gg


                    // when can we start using aliases? :P
                    //let duration = gg.Key

                    // curently a group only allows to order by key
                    orderby gg.Key descending
                    select new
                    {
                        duration = gg.Key,
                        count = gg.Count(),


                        // can we do toLower here?
                        //path = gg.Last().path.ToLower()
                        path = gg.Last().path
                    } into ggg

                    orderby ggg.path descending, ggg.count descending

                    where ggg.duration < 46

                    select ggg
                    ;

            var dt = g.AsDataTable();

            var ff = g.FirstOrDefault();

            Debugger.Break();

            // datagridview string formatter.
            // how can we show webbrowser inside datagridview?
            // group by order by then order by

            // the more code jsc generates the harder its to reverse engineeer. slowing the adversiaries.

            //Debugger.Break();

            return keyForClient;
        }

    }
}
