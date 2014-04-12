using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;
using System;
using ScriptCoreLib.Extensions;
using System.Diagnostics;
//using FormsAutoSumGridSelection.Data;
using System.Data;


namespace TestDynamicBindingSourceForDataTable
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

            //new TestDataSourcesToolbarDrag().ShowDialog();

#if FDATA
            #region ZooBookSheet1BindingSource
            global::FormsAutoSumGridSelection.Data.ZooBookSheet1BindingSource.CreateDataSource.With(
                CreateDataSource =>
                {
                    //                    ZooBookSheet1BindingSource.CreateDataSource
                    //ApplicationControl_Load

                    global::FormsAutoSumGridSelection.Data.ZooBookSheet1BindingSource.CreateDataSource =
                        delegate
                        {
                            var x = CreateDataSource();

                            Console.WriteLine("ZooBookSheet1BindingSource.CreateDataSource");
                            //Debugger.Break();


                            var r = new ZooBookSheet1Row { FooColumn = "foo1", GooColumn = 400 };


                            (x as DataTable).Rows.Add(r.FooColumn, r.GooColumn);

                            //(x as DataTable).ImportRow(
                            //    );



                            return x;
                        };
                }
            );
            #endregion
#endif



#if DEBUG
            if (Debugger.IsAttached)
            {
                DesktopFormsExtensions.Launch(
                    () => new ApplicationControl()
                );
            }

            else
            {
                RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
            }

#else
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

    }
}
