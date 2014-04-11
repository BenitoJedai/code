using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;
using System;

namespace FormsAutoSumGridSelection
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {


            //var x =
            //    global::FormsAutoSumGridSelection.Data.ZooBookSheet1BindingSource.CreateDataSource();


#if DEBUG
            DesktopFormsExtensions.Launch(
                () => new ApplicationControl()
            );
#else
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

    }
}
