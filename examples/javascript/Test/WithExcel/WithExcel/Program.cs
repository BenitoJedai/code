using Excel;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;
using System;
using System.IO;

namespace WithExcel
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var x = ExcelReaderFactory.CreateOpenXmlReader(
                File.OpenRead("Book1.xlsx")
            );

            // http://stackoverflow.com/questions/3365641/the-value-of-the-local-or-argument-x-is-unobtainable-at-this-time
            // 		ds	The value of the local or argument 'ds' is unobtainable at this time.	System.Data.DataSet
            var ds = x.AsDataSet();

            foreach (System.Data.DataTable z in ds.Tables)
            {
                Console.WriteLine(
                    ScriptCoreLib.Library.StringConversionsForDataTable.ConvertToString(
                        z
                    )
                );
            }

            // -		(new System.Linq.SystemCore_EnumerableDebugView(x.AsDataSet().Tables)).Items[0]	{Sheet1}	object {System.Data.DataTable}



//#if DEBUG
//            DesktopFormsExtensions.Launch(
//                () => new ApplicationControl()
//            );
//#else
//            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
//#endif
        }

    }
}
