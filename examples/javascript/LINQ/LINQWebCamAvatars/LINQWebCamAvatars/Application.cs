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
using LINQWebCamAvatars;
using LINQWebCamAvatars.Design;
using LINQWebCamAvatars.HTML.Pages;
using ScriptCoreLib.Query.Experimental;
using System.Linq.Expressions;
using System.Data.SQLite;

namespace LINQWebCamAvatars
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static Application()
        {
            #region QueryExpressionBuilder.WithConnection
            QueryExpressionBuilder.WithConnection =
                y =>
                {
                    var cc = new SQLiteConnection();
                    cc.Open();
                    y(cc);
                    cc.Dispose();
                };
            #endregion
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140706/internal

            //            will skip DefineVersionInfoResource
            //1ec4: 01:01 RewriteToAssembly error: System.NotSupportedException: Type 'ScriptCoreLib.Query.Experimental.QueryExpressionBuilder+xCount`1' was not completed.

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
            // X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs


            new { }.With(
    async delegate
            {
                var Count = await new xAvatar().Create().CountAsync();
                new IHTMLPre { new { Count } }.AttachToDocument();


                foreach (var y in await new xAvatar().AsEnumerableAsync())
                {
                    new IHTMLPre { new { y.Avatar96gif.Length } }.AttachToDocument();
                    new IHTMLImage { y.Avatar96gif }.AttachToDocument();

                }
            }
);

            new IHTMLButton { "new image" }.AttachToDocument().onclick +=
                e =>
            {
                var overlay = new IHTMLDiv { }.AttachTo(Native.document.documentElement);

                new IStyle(overlay)
                {
                    position = IStyle.PositionEnum.@fixed,
                    left = "0",
                    top = "0",
                    right = "0",
                    bottom = "0",
                    backgroundColor = "black"
                };

                var div = new IHTMLDiv { }.AttachTo(overlay);

                var css = Native.document.body.css.children;

                css.style.display = IStyle.DisplayEnum.none;

                Abstractatech.JavaScript.Avatar.ApplicationImplementation.MakeCamGrabber(
                    div,
                    sizeToWindow: true,
                    yield:
                    //async y =>
                    y =>
                {
                    div.Orphanize();

                    // can we use async using yet?
                    overlay.Orphanize();
                    css.style.display = IStyle.DisplayEnum.empty;

                    //new IHTMLImage { y.Avatar640x480 }.AttachToDocument();
                    new IHTMLPre { new { y.Avatar96gif.Length } }.AttachToDocument();
                    new IHTMLImage { y.Avatar96gif }.AttachToDocument();

                    // save to chrome db?

                    // never returns??
                    // errorCallback: { code = 4, message = there was not enough remaining storage space, or the storage quota was reached and the user declined to allow more space }
                    // Clear browsing data!

                    // we need scriptcorelibasync
                    new xAvatar().InsertAsync(new xAvatarRow { Avatar96gif = y.Avatar96gif }).ContinueWith(
                        delegate
                        {

                            new xAvatar().CountAsync().ContinueWithResult(
                                Count =>
                            {
                                new IHTMLPre { new { Count } }.AttachToDocument();
                            }
                            );
                        }
                    );

                }

               );
            };


        }


        #region example generated data layer
        public class xAvatar
            //: QueryExpressionBuilder.xSelect<xAvatarRow>
            :  QueryExpressionBuilder.xSelect<xAvatarKey, xAvatarRow>
        {
            public xAvatar()
            {
                Expression<Func<xAvatarRow, xAvatarRow>> selector =
                    (xAvatar) => new xAvatarRow
                {
                    Key = xAvatar.Key,

                    Avatar96gif = xAvatar.Avatar96gif,

                    Tag = xAvatar.Tag,
                    Timestamp = xAvatar.Timestamp,

                };

                this.selector = selector;
            }
        }


        public enum xAvatarKey : long { }

        public class xAvatarRow
        {
            public xAvatarKey Key;

            public string Avatar96gif;

            public string Tag;
            public DateTime Timestamp;

        }
        #endregion



    }
}
