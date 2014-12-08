using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Data.SQLite;

namespace TestXLSXDouble
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {

            #region QueryExpressionBuilder.WithConnection
            ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.WithConnection =
                y =>
                {
                    // jsc should imply it?

                    var cc = new SQLiteConnection(
                        new SQLiteConnectionStringBuilder
                    {
                        DataSource = "file:Book1.xlsx.sqlite"
                    }.ToString()
                    );

                    cc.Open();
                    y(cc);
                    cc.Dispose();
                };
            #endregion

            new ApplicationWebService().WebMethod2("", delegate { });


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
