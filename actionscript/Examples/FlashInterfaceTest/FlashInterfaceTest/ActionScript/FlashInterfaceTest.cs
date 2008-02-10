using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System;


namespace FlashInterfaceTest.ActionScript
{
    [Script]
    interface IMyDisposableHandler
    {
        Action Handler { get; set; }
    }

    [Script]
    interface IMyDisposable : IDisposable, IMyDisposableHandler
    {
    }

    [Script]
    class MyDisposable : IMyDisposable
    {

        #region IDisposable Members

        public void Dispose()
        {
            if (Handler != null)
                Handler();
        }

        #endregion

        #region IProp1 Members

        public Action Handler { get; set; }

        #endregion
    }

    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashInterfaceTest : Sprite
    {
        public FlashInterfaceTest()
        {
            var t = new TextField
                    {
                        text = "powered by jsc",
                        x = 20,
                        y = 40,
                        selectable = false,
                        sharpness = -400,
                        textColor = 0xffffff
                    }.AttachTo(this);


            using (
                var x =
                    new MyDisposable
                    {
                        Handler =
                            delegate
                            {
                                t.text = "done!";
                            }
                    }
            )
            {
                t.textColor = 0x00ff00;
            }



        }
    }

}
