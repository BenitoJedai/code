using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MultipleXLSXAssets
{
    using MultipleXLSXAssets.Data;
    using ScriptCoreLib.Shared.Data.Diagnostics;
    using System.Diagnostics;


    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2()
        {
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyOfTRowExtensions.Join.cs

            // X:\jsc.svn\examples\javascript\forms\ThreeWay\ThreeWay\ApplicationControl.cs
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs



            // ? new SchemaViewsMiddleViewContext()
            var j = from im in new Schema.MiddleSheet()
                    join il in new Schema.LeftSheet() on im.Key equals il.MiddleSheet
                    join ir in new Schema.RightSheet() on im.Key equals ir.MiddleSheet


                    select new SchemaViewsMiddleViewRow
                    {
                        //Key = im.Key,


                        Content = im.Content,
                        LatestLeftContent = il.LeftContent,
                        LatestRightContent = ir.RightContent,
                    };

            var a = j.AsDataTable();

            Debugger.Break();
        }

    }
}
