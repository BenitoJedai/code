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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestMemberInitExpression;
using TestMemberInitExpression.Design;
using TestMemberInitExpression.HTML.Pages;
using System.Linq.Expressions;
using System.Reflection;

namespace TestMemberInitExpression
{
    public class xSpecial
    {
    }

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public class xRow
        {
            public long connectEnd;
            public xSpecial keepTypeInfo;
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\Test\TestWebSQLDatabase\TestWebSQLDatabase\Application.cs
            // X:\jsc.svn\examples\javascript\test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
            Expression<Func<xRow, xRow>> selector =
                (x) => new xRow
            {
                connectEnd = x.connectEnd,
                keepTypeInfo = x.keepTypeInfo
            };

            var xMemberInitExpression = selector.Body as MemberInitExpression;

            //{ { Name = connectEnd } }
            //{ { FieldType = [native] String } }
            //{ { Name = keepTypeInfo } }
            //{ { FieldType = [native] String } }

            xMemberInitExpression.Bindings.WithEachIndex(
             (SourceBinding, i) =>
                {
                    new IHTMLPre { new { SourceBinding.Member.Name } }.AttachToDocument();

                    var f = SourceBinding.Member as FieldInfo;

                    new IHTMLPre { new { f.FieldType } }.AttachToDocument();


                }
            );

        }


    }
}
