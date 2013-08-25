using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.external;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace FlashBootstrapsSecondaryApplication
{
    partial class Application
    {
        [Description("JavaScript world, remember to manually update view-source asset")]
        public static void Initialize(Action<string> yield_to_sprite)
        {

            var f = new Form();

            f.Show();

            yield_to_sprite("fooo1");
        }
    }

    #region Application for Flash world
    class FakeApplication
    {
        [Description("Flash world, initialize JavaScript world if missing")]
        public static void Initialize(Action<string> yield_to_sprite)
        {
            #region export callback

            ExternalExtensions.TryAddCallback("WhenReady", yield_to_sprite.ToFunction());

            #endregion

            #region Initialize JavaScript
            //TypeError: Cannot call method 'apply' of undefined


            //ExternalInterface.call("setTimeout", "window.function2 = function (e) { document.title = 'length: ' + e.length; document.getElementById('foo').value = e; return 0; };", 0);
            ExternalInterface.call("setTimeout",

@"window.function2 = function (e) 
{  
    console.log('length: '  + e.length); 
// debugger;

var f = null;
//try
//{
    f = eval(e);

    console.log('done!'); 

//}
//catch (err)
//{
//    console.log('error! ' + err); 
//    console.log('typeof f: ' + typeof f); 
//    console.log('f: ' + f); 
//}
    return 0; 

};", 0);
            //ExternalInterface.call("setTimeout", "window.function1 = function (e) { alert(e.length); };", 0);
            //ExternalInterface.call("setTimeout", "document.title = 'flashed';", 0);


            var source = KnownEmbeddedResources.Default["assets/FlashBootstrapsSecondaryApplication/view-source"].ToStringAsset();

            //content.t.Text = new { source.Length }.ToString();

            //ExternalInterface.call("setTimeout", "document.title = 'source';", 0);


            //ExternalInterface.call("function2", "hi").ToString();
            //ExternalInterface.call("function2", "<\\&>\n\r\t<A//>").ToString();

            1.AtDelay(
                delegate
                {
                    // Uncaught SyntaxError: Unexpected token ILLEGAL 

                    //var x = 1024 * 31 + 1;
                    //var x = 1024 * 30 + 1 + 256;
                    //var x = 1024 * 30 + 1 + 256 + 64 + 48 + 5;

                    //for (d = 0; (d < AQQABtNdQz66ZYUODttTfw(c)); d++)
                    //{
                    //  e = EwQABtNdQz66ZYUODttTfw(c, d);
                    //  f = CQQABtNdQz66ZYUODttTfw(c, d);
                    //  i = !(EAQABtNdQz66ZYUODttTfw('\"\'\u005c\u0008\u000c\u000a\u000d\u0009', e) > -1);

                    // YUODttTfw('\""\ }
                    // tTfw('\""\u005c }
                    var q = source; //.Substring(x, 16);

                    //q = q.Replace("\\'", "X");
                    //q = q.Replace("\\\"", "\\x22");


                    q = q.Replace("\\", "\\\\");
                    //q = q.Replace("'", "X");


                    // { x = 31041, Length = 1118850, q = 66ZYUODttTfw(c, d);
                    //i = !(EAQABtNdQz66Z }
                    // { x = 31089, Length = 1118850, q = YUODttTfw('\"\'\ }

                    //content.t.Text = new { x, source.Length, q }.ToString();


                    try
                    {
                        ExternalInterface.call("function2", q);
                    }
                    catch (Exception ex)
                    {
                        //content.t.Text = "error? " + new { ex.Message, ex.StackTrace };
                    }
                }
            );



            //ExternalInterface.call("function1",
            //    // too much
            //    //"hello world".PadLeft(1024 * 1024)


            //    // too much
            //    //"hello world".PadLeft(1024 * 512)

            //    // 8K OK
            //    //"hello world".PadLeft(1024 * 8)

            //    // 131072 OK
            //    //"hello world".PadLeft(1024 * 8 * 16)

            //    // something wrong with pad?
            //    //524288
            //    // 1048576
            //    // 4194304
            //       1118850
            //    //new string('x', 1024 * 1024 * 4)
            //    source

            //    ).ToString();
            #endregion

        }
    }
    #endregion


    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        // different compilations
        // use different identifiers?

        //public void WhenReady(Action yield)
        //{
        //    yield();
        //}

        public ApplicationSprite()
        {



            // this.loaderInfo.uncaughtErrorEvents.uncaughtError +=
            //e =>
            //{
            //    content.t.Text = ("error: " + new { e.errorID, e.error, e } + "\n run in flash debugger for more details!");

            //};

            content.t.Text = "before InvokeWhenStageIsReady";


            this.InvokeWhenStageIsReady(
                () =>
                {
                    content.t.Text = "at InvokeWhenStageIsReady";

                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);


                    // this wont work yet!
                    //Application.Initialize(
                    FakeApplication.Initialize(
                        args =>
                        {
                            content.t.Text = "WhenReady: " + new { args };
                        }
                    );



                }
            );
        }

    }
}
