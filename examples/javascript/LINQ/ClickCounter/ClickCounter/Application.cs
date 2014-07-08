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
using ClickCounter;
using ClickCounter.Design;
using ClickCounter.HTML.Pages;
using ScriptCoreLib.Query.Experimental;
using System.Linq.Expressions;
using System.Data.SQLite;
using System.Threading;
using ClickCounter.Data;

namespace ClickCounter
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\examples\javascript\LINQ\LINQWebCamAvatars\LINQWebCamAvatars\Application.cs

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

        public Application(IApp page)
        {
            // can we do this?

            //new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox }.AttachToDocument()

            //    // hide the next pre
            //    .css

            //    .@unchecked

            //    .adjacentSibling[IHTMLElement.HTMLElementEnum.pre].style.display = IStyle.DisplayEnum.none;

            // make sure it exists?
            new xxAvatar().Create();


            // how much load will we cause?
            new IHTMLPre { "count: ", () => new xxAvatar().CountAsync() }.AttachToDocument();
            //new IHTMLPre { "descending: ", () => new xAvatar().CountAsync() }.AttachToDocument();
            new IHTMLPre { "descending: ",
                () => (
                    from x in new xxAvatar()
                    orderby x.Key descending
                    select new xxAvatarRow
                    {
                        // message: "type$zrk5B64_bQTe7tpWTj7myyA is not defined"
                        // we cannot select Key as we are missing the enum type referenced by FromHandle?
                        //x.Key,

                        Tag = x.Tag
                    }
                ).FirstOrDefaultAsync()
            }.AttachToDocument();

            //new IHTMLButton { "FirstOrDefaultAsync" }.AttachToDocument().onclick +=
            //async delegate
            //{
            //    //0:3830ms {
            //    //    CommandText = /* 0000:0001 */  select
            //    ///* 0000:0002 */   /* let */ `x`.`Tag` as `bwEABr9BfTWaWvALBzsJOQ`

            //    var q = from x in new xAvatar()

            //                // can we do that?
            //            orderby x.Key descending

            //            // anonyous member name gets messed up?
            //            select new xAvatarRow
            //            {
            //                // message: "type$zrk5B64_bQTe7tpWTj7myyA is not defined"
            //                // we cannot select Key as we are missing the enum type referenced by FromHandle?
            //                //x.Key,

            //                Tag = x.Tag
            //            };

            //    var f = await q.FirstOrDefaultAsync();

            //    new IHTMLPre { new { f } }.AttachToDocument();
            //};

            //new IHTMLPre { "ascending: ", () =>
            //    (
            //    from x in new xAvatar()
            //        //orderby x.Key ascending
            //    select new { x.Key, x.Tag }
            //    ).FirstOrDefaultAsync(cc)
            //}.AttachToDocument();

            //new IHTMLPre { "descending: ", () =>
            //    (
            //    from x in new xAvatar()
            //    orderby x.Key descending
            //    select x.Key
            //    ).FirstOrDefaultAsync(cc)
            //}.AttachToDocument();


            //new IHTMLPre().AttachToDocument().Add(
            //     () => new xAvatar().Create(cc).CountAsync(cc)
            //);

            new IHTMLButton { "InsertAsync" }.AttachToDocument().onclick +=
                 async e =>
            {
                e.Element.disabled = true;
                await new xxAvatar().InsertAsync(
                    new xxAvatarRow
                {
                    Tag = "hi! " + new
                    {

                        Thread.CurrentThread.ManagedThreadId,

                        Count = await new xxAvatar().CountAsync()
                    }
                }
               );

                new IHTMLPre { "reload to count !" }.AttachToDocument();
                e.Element.disabled = false;
            };

            new IHTMLButton { "InsertAsync worker" }.AttachToDocument().onclick +=
                 async e =>
            {
                e.Element.disabled = true;

                await Task.Run(
                    async delegate
                {
                    // lets have our background thread add to db!


                    //var cc2 = new SQLiteConnection();
                    //cc2.Open();


                    // time for our DataBound DataRepeater class?
                    await new xxAvatar().InsertAsync(
                        new xxAvatarRow
                    {
                        Tag = "hi! " + new
                        {
                            Thread.CurrentThread.ManagedThreadId,

                            Count = await new xxAvatar().CountAsync()
                        }
                    });


                    // wait some while still inside worker
                    //await Task.Delay(500);
                }
                );

                e.Element.disabled = false;
            };
        }

    }


    #region example generated data layer
    //public class xAvatar : QueryExpressionBuilder.xSelect<xAvatarRow>
    //{
    //    public xAvatar()
    //    {
    //        // xlsx needs to generate this?
    //        Expression<Func<xAvatarRow, xAvatarRow>> selector =
    //            (xAvatar) => new xAvatarRow
    //        {
    //            Key = xAvatar.Key,

    //            Avatar96gif = xAvatar.Avatar96gif,

    //            Tag = xAvatar.Tag,
    //            Timestamp = xAvatar.Timestamp,

    //        };

    //        this.selector = selector;
    //    }
    //}


    //public enum xAvatarKey : long { }

    //public class xAvatarRow
    //{
    //    public xAvatarKey Key;

    //    public string Avatar96gif;

    //    public string Tag;
    //    public DateTime Timestamp;


    //    public override string ToString()
    //    {
    //        return new { Tag }.ToString();
    //    }

    //}
    #endregion

}
