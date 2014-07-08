#define FCHROME


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
using GGearAlpha;
using GGearAlpha.Design;
using GGearAlpha.HTML.Pages;
using ScriptCoreLib.Query.Experimental;
using System.Linq.Expressions;
using System.Data.SQLite;

namespace GGearAlpha
{
    #region example generated data layer
    //public class xPostcard : QueryExpressionBuilder.xSelect<xPostcardRow>
    //{
    //    public xPostcard()
    //    {
    //        Expression<Func<xPostcardRow, xPostcardRow>> selector =
    //            (xPostcard) => new xPostcardRow
    //        {
    //            Key = xPostcard.Key,

    //            Id = xPostcard.Id,

    //            Text = xPostcard.Text,
    //            X = xPostcard.X,
    //            Y = xPostcard.Y,
    //            Zoom100 = xPostcard.Zoom100,

    //            Tag = xPostcard.Tag,
    //            Timestamp = xPostcard.Timestamp,

    //        };

    //        this.selector = selector;
    //    }
    //}


    //public enum xPostcardKey : long { }

    //public class xPostcardRow
    //{
    //    public xPostcardKey Key;

    //    public string Id;
    //    public string Text;

    //    // double instead?
    //    public long X;
    //    public long Y;
    //    public long Zoom100;

    //    public string Tag;
    //    public DateTime Timestamp;

    //}
    #endregion



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

        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\Test\TestNativeStaticDelegateCall\TestNativeStaticDelegateCall\Application.cs




#if FCHROME
            #region AtFormCreated
            FormStyler.AtFormCreated =
                 s =>
                 {
                     s.Context.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                     // this is working?
                     var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDrag().AttachTo(s.Context.GetHTMLTarget());
                 };
            #endregion



            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultTitle = "GGearAlpha";
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview128().src;

                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text,
                    AtFormCreated: FormStyler.AtFormCreated
                );

                return;
            }
            #endregion


#endif

            // 7 years later,  28.06.2007 to    20140629
            // google gears ws discontinued
            // yet today jsc allows to use websql in a worker thread in a browser.
            // can we just do a svn move from7 years ago 
            // and then just call
            // "X:\jsc.svn\javascript\Examples\GoogleGears\GGearAlpha\bin\Debug\web\GoogleGearsAdvanced.htm"

            // we do have to update the way we refernce images.in 2014 we haev AssetsLibrary. :) what about 2021? timetravel?
            // http://gearsblog.blogspot.com/2011/03/stopping-gears.html
            // once images are restored, lets switch to SQLite api.

            new js.GoogleGearsAdvanced();
        }

    }
}
