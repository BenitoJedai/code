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
using TestSQLiteEnumWhere.Data;

namespace TestSQLiteEnumWhere
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
        public void WebMethod2()
        {
            var x = new Book1.Middle();

            x.Insert(
                new Book1MiddleRow
                {
                    FooStateEnum = FooStateEnum.Foo0,
                    GooStateEnum = GooStateEnum.Goo2,
                    Ratio = 0.5,
                    Title = "h1"
                }
            );


            // x:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyExtensions.cs
            // filter = {z => (Convert(z.FooStateEnum) == 0)}

            //select `Key`, `Title`, `Ratio`, `FooStateEnum`, `GooStateEnum`, `Tag`, `Timestamp`
            //from `Book1.Middle`
            // where `GooStateEnum` = @arg1

            //MutableWhere { n = @arg0, r = 0 }
            //MutableWhere { n = @arg1, r = 1 }
            //select `Key`, `Title`, `Ratio`, `FooStateEnum`, `GooStateEnum`, `Tag`, `Timestamp`
            //from `Book1.Middle`
            // where `FooStateEnum` = @arg0 and `GooStateEnum` = @arg1






            var zz = from z in x
                     where z.FooStateEnum == FooStateEnum.Foo0
                     where z.GooStateEnum == GooStateEnum.Goo2
                     select z;


            var zzz = zz.AsEnumerable();

            // Data.Book1MiddleKey
        }

    }
}
