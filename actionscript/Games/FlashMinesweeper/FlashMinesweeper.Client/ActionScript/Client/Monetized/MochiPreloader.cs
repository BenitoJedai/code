using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace FlashMinesweeper.ActionScript.Client.Monetized
{
    [Script, 
    ScriptApplicationEntryPoint(
        Width = TeamPlay.DefaultControlWidth + TeamPlay.NonobaChatWidth, Height = TeamPlay.DefaultControlHeight)]
    [SWF(width = TeamPlay.DefaultControlWidth + TeamPlay.NonobaChatWidth, height = TeamPlay.DefaultControlHeight)]
    class MochiPreloader : Sprite
    {
        #region mochiad internals
        [Script]
        sealed class Options
        {
            public readonly object Data = new object();

            public Options()
            {
                ad_finished = delegate { };
                ad_started = delegate { };
            }
            public string id
            {
                set { InternalSetValue(Data, "id", value); }
            }

            public string res
            {
                set { InternalSetValue(Data, "res", value); }
            }

            public Action ad_started
            {
                set { InternalSetValue(Data, "ad_started", value.ToFunction()); }
            }

            public Action ad_finished
            {
                set { InternalSetValue(Data, "ad_finished", value.ToFunction()); }
            }

            public object clip
            {
                set { InternalSetValue(Data, "clip", value); }
            }

            [Script(OptimizedCode = "e[k] = v;")]
            public static void InternalSetValue(object e, object k, object v)
            {
            }

            public void showPreGameAd()
            {
                MochiAd.showPreGameAd(Data);
            }
        }


        public bool _mochiad_loaded;
        public object _mochiad;
        public object clip;
        public double origFrameRate;

        #endregion


        //MochiAd.showPreGameAd({id:"5a5be1df755e6cdc", res:"552x352"});

        public string _mochiads_game_id = "5a5be1df755e6cdc";
        public MochiPreloader()
        {
            loaderInfo.ioError +=
                delegate
                {
                    // Ignore event to prevent unhandled error exception
                };

            new Options
            {
                clip = this,
                id = _mochiads_game_id,
                res = (TeamPlay.DefaultControlWidth + TeamPlay.NonobaChatWidth) + "x" + TeamPlay.DefaultControlHeight,
                ad_finished =
                    delegate
                    {
                        new TeamPlay().AttachTo(stage);
                    }

            }.showPreGameAd();
        }
    }
}
