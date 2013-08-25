using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.external;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.IO;
using System.Text;

namespace Abstractatech.ActionScript.ConsoleFormPackage
{
    public static class ConsoleFormPackageExperience
    {
        public static Action<string> Diagnostics = delegate { };

        public static void Initialize()
        {
            #region ConsoleFormPackageExperience
            #region AtInitializeConsoleFormWriter

            var w = new __OutWriter();
            var o = Console.Out;
            var __reentry = false;

            var __buffer = new StringBuilder();

            w.AtWrite =
                x =>
                {
                    __buffer.Append(x);
                };

            w.AtWriteLine =
                x =>
                {
                    __buffer.AppendLine(x);
                };

            Console.SetOut(w);

            AtInitializeConsoleFormWriter = (
                Action<string> Console_Write,
                Action<string> Console_WriteLine
            ) =>
            {

                try
                {


                    w.AtWrite =
                        x =>
                        {
                            o.Write(x);

                            if (!__reentry)
                            {
                                __reentry = true;
                                Console_Write(x);
                                __reentry = false;
                            }
                        };

                    w.AtWriteLine =
                        x =>
                        {
                            o.WriteLine(x);

                            if (!__reentry)
                            {
                                __reentry = true;
                                Console_WriteLine(x);
                                __reentry = false;
                            }
                        };

                    Console.WriteLine("flash Console.WriteLine should now appear in JavaScript form!");
                    Console.WriteLine(__buffer.ToString());
                }
                catch
                {

                }
            };
            #endregion




            ExternalInterface.call("setTimeout", "window.__check__SystemConsoleWrite = function (e) { return '__SystemConsoleWrite' in window; };", 0);

            #region function2
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
            #endregion


            #region yield_to_sprite
            Action<string> yield_to_sprite = delegate
            {
                // check again!

                try
                {
                    var value = (bool)ExternalInterface.call("__check__SystemConsoleWrite");

                    Diagnostics("yield_to_sprite: " + new { value }.ToString());

                    if (value)
                    {
                        InitializeConsoleFormWriter();
                    }
                    else
                    {
                        // reload javascript
                        // await when ready

                        //reload();
                    }
                }
                catch (Exception ex)
                {
                    Diagnostics("error? " + new { ex.Message });
                }
            };
            #endregion

         

            ExternalExtensions.TryAddCallback("WhenReady", yield_to_sprite.ToFunction());

            100.AtDelay(
             delegate
             {

                 #region reload
                 Action reload = delegate
                 {
                     var source = KnownEmbeddedResources.Default["assets/Abstractatech.ActionScript.ConsoleFormPackage/view-source"].ToStringAsset();
                     var q = source; //.Substring(x, 16);
                     q = q.Replace("\\", "\\\\");
                     try
                     {
                         ExternalInterface.call("function2", q);
                     }
                     catch (Exception ex)
                     {
                         //content.t.Text = "error? " + new { ex.Message, ex.StackTrace };
                     }



                 };
                 #endregion





                 try
                 {
                     var value = (bool)ExternalInterface.call("__check__SystemConsoleWrite");

                     Diagnostics("InvokeWhenStageIsReady: " + new { value }.ToString());

                     if (value)
                     {
                         InitializeConsoleFormWriter();
                     }
                     else
                     {
                         // reload javascript
                         // await when ready

                         reload();
                     }
                 }
                 catch (Exception ex)
                 {
                     Diagnostics("error? " + new { ex.Message });
                 }
            #endregion
             }
         );


        }



        #region InitializeConsoleFormWriter
        static Action<Action<string>, Action<string>> AtInitializeConsoleFormWriter;

        class __OutWriter : TextWriter
        {
            public Action<string> AtWrite;
            public Action<string> AtWriteLine;

            public override void Write(string value)
            {
                AtWrite(value);
            }

            public override void WriteLine(string value)
            {
                AtWriteLine(value);
            }

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        static public void InitializeConsoleFormWriter()
        {
            Action<string> __SystemConsoleWrite =
                x => ExternalInterface.call("__SystemConsoleWrite", x);



            AtInitializeConsoleFormWriter(
                __SystemConsoleWrite,
                x => __SystemConsoleWrite(x + Environment.NewLine)
            );
        }
        #endregion


    }
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            //this.loaderInfo.uncaughtErrorEvents.uncaughtError +=
            //    e =>
            //    {
            //        Console.WriteLine("error: " + new { e.errorID, e.error, e } + "\n run in flash debugger for more details!");

            //    };

            this.InvokeWhenStageIsReady(
                   () =>
                   {
                       content.AttachToContainer(this);
                       content.AutoSizeTo(this.stage);

                       content.MouseLeftButtonUp +=
                           (sender, e) =>
                           {
                               var p = e.GetPosition(content);

                               Console.WriteLine(new { p.X, p.Y });
                           };

                       ConsoleFormPackageExperience.Diagnostics =
                           x => content.t.Text = x;

                       ConsoleFormPackageExperience.Initialize();
                   }
             );

        }









    }
}
