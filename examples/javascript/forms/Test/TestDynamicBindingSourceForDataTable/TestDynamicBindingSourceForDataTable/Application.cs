using TestDynamicBindingSourceForDataTable;
using TestDynamicBindingSourceForDataTable.Design;
using TestDynamicBindingSourceForDataTable.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using FormsAutoSumGridSelection.Data;
using System.Data;

namespace TestDynamicBindingSourceForDataTable
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
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

            var content = new ApplicationControl();

            content.AttachControlToDocument();


        }

    }
}
