using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using SVGNavigationTiming.Design;
using System;
using System.Linq;
using System.Data;






namespace SVGNavigationTiming
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //java.lang.NumberFormatException: null
            //        at java.lang.Long.parseLong(Unknown Source)
            //        at java.lang.Long.parseLong(Unknown Source)
            //        at ScriptCoreLib.Shared.BCLImplementation.System.__Convert.ToInt64(__Convert.java:144)

            //at System.Data.ExceptionBuilder.SetFailed(Object value, DataColumn column, Type type, Exception innerException)
            //at System.Data.DataColumn.set_Item(Int32 record, Object value)
            //at System.Data.DataRow.set_Item(DataColumn column, Object value)
            //at System.Data.DataRow.set_Item(String columnName, Object value)
            //at SVGNavigationTiming.Design.PerformanceResourceTimingData2Extensions.<AsDataTable>ApplicationPerformance.yield

            //AsDataTable enter
            //AsDataTable Columns set
            //AsDataTable closure_DataTable set
            //AsDataTable.closure_yield enter
            //AsDataTable.closure_yield NewRow set

            //            DateTimeConvertFromObject { e = 1388320283642 }
            //DateTimeConvertFromInt64 { Kind = Utc, value = 12/29/2013 12:31:23 PM }

            //Unhandled Exception: System.AccessViolationException: Attempted to read or write protected memory. This is often an indication that other memory is corrupt.


            // QueryStrategyOfTRowExtensions



            // https://connect.microsoft.com/VisualStudio/feedback/details/850741/roslyn-csharp-compiler-doesnt-recognize-extensionattribute


            var zz =
                //QueryStrategyOfTRowExtensions.AsDataTable(

                // public static DataTable AsDataTable(IEnumerable<PerformanceResourceTimingData2ApplicationPerformanceRow> value);
                // this is missing? roslyn not seeing it?
                //PerformanceResourceTimingData2Conversions.AsDataTable(


                    new[] {

                new PerformanceResourceTimingData2ApplicationPerformanceRow {

                    Key = (PerformanceResourceTimingData2ApplicationPerformanceKey)77
                }
            
            }.AsDataTable();


            new PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                new PerformanceResourceTimingData2ApplicationPerformanceRow { }
            );

            var x = new PerformanceResourceTimingData2.ApplicationPerformance().AsDataTable();
            var y = new PerformanceResourceTimingData2.ApplicationPerformance().AsEnumerable();
            var yy = y.ToArray();
            var z = y.AsDataTable();


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
