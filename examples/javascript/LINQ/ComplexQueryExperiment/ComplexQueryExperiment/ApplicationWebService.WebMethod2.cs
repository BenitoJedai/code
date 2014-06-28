using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Xml.Linq;
using ScriptCoreLib.Query.Experimental;


namespace ComplexQueryExperiment
{
    public partial class ApplicationWebService
    {
        //public async void WebMethod2()
        public void WebMethod2()
        {
            // running out of brain power are we?
            // we need a high level overview.
            // all examples at the same time.
            // all tests shall work for sqlite and mysql

            Func<int, int> scalarLambda = xx => xx + 55;


            // Error	1	Could not find an implementation of the query pattern for source type 'ComplexQueryExperiment.xTable'.  'Select' not found.	X:\jsc.svn\examples\javascript\LINQ\ComplexQueryExperiment\ComplexQueryExperiment\ApplicationWebService.WebMethod2.cs	13	31	ComplexQueryExperiment
            var x = from z in new xTable()
                    let field3 = z.field1
                    let field4 = z.field1 + z.field2
                    let field5 = z.field1 + 33

                    where z.Key == (xKey)66
                    where field5 > 44 && field5 < 77
                    where field5 > 44
                    where new { field3 }.field3 > field5

                    let field6 = field3 + field4 + field5

                    // whats wrong here?
                    let field7 = new { ff1 = z.field1, field6 }


                    let scalar1 =
                        from zz in new xTable()
                        orderby zz.Key
                        select zz

                    let field8 = "???".ToLower()

                    //orderby field8, field7, field6, field5 > 33

                    // doesnt work anymore?
                    //let scalar1count = scalar1.Count()

                    //where scalar1count < 77

                    //let scalar2 = new[] {
                    //    scalar1count - 1,
                    //    scalar1count,
                    //    scalar1count + 1
                    //}

                    //// field3 not found?
                    //let field9 = new { field3, field7, x = new { field4 }, y = new[] { field5, field6 } }
                    let field10 = z.Timestamp
                    let field11 = field10.Date
                    let field12 = field11.AddDays(-1)

                    // can we scalar select all referenced fields?
                    // does it mean we have to ask each field separatly?
                    let xFirstOrDefaultQuery =
                        from zz in new xTable()
                        where zz.Key == (xKey)77
                        select zz

                    let xFirstOrDefault = xFirstOrDefaultQuery.FirstOrDefault()

                    let xGroupByQuery =
                        from zz in new xTable()
                        group zz by new { zz.field1 } into gg
                        // can we select the whole group as scalar?
                        //select gg
                        select gg.Key

                    let xGroupByFirstOrDefault = xGroupByQuery.FirstOrDefault()
                    where xGroupByFirstOrDefault != null


                    where (from zz in new xTable() where zz.field1 < 33 select zz).Count() < 4

                    let scalarInnerJoinQuery =
                          from xouter in new xTable()
                          join xinner in new xTable() on xouter.field1 equals xinner.field2
                          select new { xouter, xinner }

                    let scalarInnerJoinFirstOrDefault =
                        scalarInnerJoinQuery.FirstOrDefault()

                    //select z.field1;
                    select new
                    {
                        xGroupByFirstOrDefault.field1,

                        scalarInnerJoinFirstOrDefault,
                        xFirstOrDefault,

                        zExpression = z.field1 > z.field2,
                        zz = z.field1 + z.field2,
                        //z.field1,
                        z,


                        z.Timestamp,

                        field3,

                        a = new
                        {
                            //scalar2,


                            Tag1 = z.Tag,
                            Tag2 = z.Tag.ToLower(),

                            c2 = 2,
                            c3 = new { c4 = 4 },
                        },

                        //scalar2,

                        c1 = 1,
                        f11 = z.field1,
                        f12 = z.field1,
                        f13 = z.field1 + 3,
                        xml = new XElement("xml",
                            new XAttribute("tag", z.Tag)
                            ),



                            // not supported anymore?
                        //aa = new[] {
                        //    z.Tag,
                        //    z.Tag
                        //    }
                    };

            //select scalarLambda(field5);

            //select field5 > 44;


            //select new[] {
            //    new { x = field3, z},
            //    new { x= field5, z},
            //};

            //select new
            //{
            //    fieldConstant = "????",
            //    fieldArray = new[] { 11, 22 },
            //    fieldObject = new { field3, field7 },
            //    field9final = field9,
            //    field9x = field9.field3,
            //    z
            //};



            var f = x.FirstOrDefault();
            // order by
            // typed ctor

            //Console.ReadKey();
        }



    }
}
