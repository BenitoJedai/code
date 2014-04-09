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

namespace TestDynamicBindingSourceForDataTable
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //            51:206ms InternalSetDataSource, null? ctor optimized out? view-source:37450
            //51:207ms enter MyDataSource .ctor view-source:37450
            //51:208ms exit MyDataSource .ctor view-source:37450
            //51:208ms { newT = <Namespace>.MyDataSource } view-source:37409
            //51:209ms { MyDataSource_DataSource = [object Object] }

            // 51:382ms InternalSetDataSource, null? ctor optimized out? 
            { FormsAutoSumGridSelection.Data.MyDataSource __ForActivator; }
            { FormsAutoSumGridSelection.Data.XZooBook.Sheet1BindingSource __ForActivator; }
            { FormsAutoSumGridSelection.Data.ZooBook.Sheet1BindingSource __ForActivator; }


            content.AttachControlToDocument();


        }

    }
}
