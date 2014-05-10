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
using TestSQLiteGroupBy.Data;

namespace TestSQLiteGroupBy
{
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
        public async Task<IEnumerable<IGrouping<GooStateEnum, Book1MiddleRow>>> WebMethod2()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/201405

            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteEnumWhere\TestSQLiteEnumWhere\ApplicationWebService.cs
            // can we do sql group by or even send it over to client if we do it in memory?


            var x = new Book1.Middle
            {
                // collection initializer?
                // .Add ?
            };

            x.Insert(
                new Book1MiddleRow
                {
                    // enum name clash? 
                    FooStateEnum = FooStateEnum.Foo0,
                    GooStateEnum = GooStateEnum.Goo2,
                    Ratio = 0.5,
                    Title = "h1"
                }
            );


            x.Insert(
                new Book1MiddleRow
                {
                    FooStateEnum = FooStateEnum.Foo0,
                    GooStateEnum = GooStateEnum.Goo0,
                    Ratio = 0.5,
                    Title = "h1"
                }
            );



            // x:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs





            var zz = from z in x
                     where z.FooStateEnum == FooStateEnum.Foo0
                     //where z.GooStateEnum == GooStateEnum.Goo2
                     select z;

            // http://www.viblend.com/Questions/WinForms/HowToGroupWinFormsDataGridViewByColumn.aspx
            // tooltips?

            var zzz = zz.AsEnumerable();

            var g = from z in zzz
                    group z by z.GooStateEnum;


            return g;
        }

    }
}
