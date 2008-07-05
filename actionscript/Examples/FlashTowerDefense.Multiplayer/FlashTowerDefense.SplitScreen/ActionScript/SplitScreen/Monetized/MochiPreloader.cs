using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

using FlashTowerDefense.ActionScript;

namespace FlashTowerDefense.ActionScript.SplitScreen.Monetized
{
    [Script, ScriptApplicationEntryPoint(Width = FlashTowerDefenseSplitScreen.DefaultWidth, Height = FlashTowerDefenseSplitScreen.DefaultHeight)]
    [SWF(width = FlashTowerDefenseSplitScreen.DefaultWidth, height = FlashTowerDefenseSplitScreen.DefaultHeight, backgroundColor = FlashTowerDefense.ColorBlack)]
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

        public string _mochiads_game_id = "ba46d03ebd4540fe";
           
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
                res = FlashTowerDefenseSplitScreen.DefaultWidth + "x" + FlashTowerDefenseSplitScreen.DefaultHeight,
                ad_finished =
                    delegate
                    {
                        var c = new FlashTowerDefenseSplitScreen();

                        stage.addChild(
                            c
                            );
                    }

            }.showPreGameAd();
        }
    }
}
