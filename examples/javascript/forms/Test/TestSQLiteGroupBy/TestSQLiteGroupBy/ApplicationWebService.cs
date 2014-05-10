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
        //public async Task<IEnumerable<IGrouping<GooStateEnum, Book1MiddleRow>>> WebMethod2()
        public async Task<IEnumerable<XGrouping>> WebMethod2()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/201405

            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLiteEnumWhere\TestSQLiteEnumWhere\ApplicationWebService.cs
            // can we do sql group by or even send it over to client if we do it in memory?

            // how can this type define it depends on another assembly so 
            // security can detect it fast?
            // like throws for java.
            // http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.dependencyattribute.aspx
            // we will need to reference a specific type on that third party.
            // we can initally do it only for assetslibrary genererated types?
            // http://stackoverflow.com/questions/473575/unity-framework-dependencyattribute-only-works-for-public-properties


            //[FileNotFoundException]: Could not load file or assembly &#39;System.Data.XSQLite, Version=3.7.7.1, Culture=neutral, PublicKeyToken=null&#39; or one of its dependencies. The system cannot find the file specified.
            //   at ScriptCoreLib.Shared.Data.Diagnostics.InternalWithConnectionLambda.WithConnection(String DataSource)
            //   at ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambda.WithConnection(String DataSource)
            //   at TestSQLiteGroupBy.Data.Book1.Middle..ctor(String DataSource)

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



            //MutableWhere { Method = , NodeType = Equal, ColumnName = FooStateEnum, Right = 0 }
            //AsDataTable
            //MutableWhere { n = @arg0, r = 0 }
            //select `Key`, `Title`, `Ratio`, `FooStateEnum`, `GooStateEnum`, `Tag`, `Timestamp`
            //from `Book1.Middle`
            // where `FooStateEnum` = @arg0


            var zz = from z in x
                     where z.FooStateEnum == FooStateEnum.Foo0
                     where z.Ratio > 0.1
                     where z.Ratio < 0.9
                     //where z.GooStateEnum == GooStateEnum.Goo2
                     select z;

            // http://www.viblend.com/Questions/WinForms/HowToGroupWinFormsDataGridViewByColumn.aspx
            // tooltips?

            var zzz = zz.AsEnumerable();

            var g = from z in zzz
                    group z by z.GooStateEnum;

            // 0380:01:01 RewriteToAssembly error: System.NotImplementedException: { SourceType = System.Linq.IGrouping`2[TestSQLiteGroupBy.Data.GooStateEnum,TestSQLiteGroupBy.Data.Book1MiddleRow] }

            return g.Select(gg => new XGrouping { Key = gg.Key, Items = gg.AsEnumerable() });
        }

    }

    public class XGrouping
    {
        public GooStateEnum Key;
        public IEnumerable<Book1MiddleRow> Items;
    }
}
