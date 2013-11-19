using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace MultiAppDatabaseExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //new ApplicationWebService()._ClientsTable_Insert(new MultiAppDatabase.Schema.ClientsQueries.Insert { Username = "teset", Password="ss"});
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
