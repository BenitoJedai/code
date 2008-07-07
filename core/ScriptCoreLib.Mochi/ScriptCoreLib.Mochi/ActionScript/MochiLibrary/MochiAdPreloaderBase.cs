using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.MochiLibrary
{
    [Script]
    public abstract class MochiAdPreloaderBase : Sprite
    {

        #region mochiad internals

        // are those fields still needed?

        public bool _mochiad_loaded;
        public object _mochiad;
        public object clip;
        public double origFrameRate;

        #endregion

        public string _mochiads_game_id = "";

        public MochiAdPreloaderBase()
        {
            loaderInfo.ioError +=
                 delegate
                 {
                     // Ignore event to prevent unhandled error exception
                 };

        }

        public void showPreGameAd(Action a)
        {
            var Options = new MochiAdOptions();

            Options.clip = this;
            Options.id = _mochiads_game_id;
            Options.res = stage.width + "x" + stage.height;
            Options.ad_finished = a;
            Options.showPreGameAd();
        }

    }
}
