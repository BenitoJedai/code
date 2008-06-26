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

namespace FlashTowerDefense.ActionScript.Client.Monetized
{
    [Script, ScriptApplicationEntryPoint(Width = FlashTowerDefenseClient.Width, Height = FlashTowerDefenseClient.Height)]
    [SWF(width = FlashTowerDefenseClient.Width, height = FlashTowerDefenseClient.Height, backgroundColor = FlashTowerDefense.ColorWhite)]
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

        public string _mochiads_game_id = "5b0c6187126f195a";
           
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
                res = FlashTowerDefenseClient.Width + "x" + FlashTowerDefenseClient.Height,
                ad_finished =
                    delegate
                    {
                        var c = new FlashTowerDefenseClient();

                        stage.addChild(
                            c
                            );
                    }

            }.showPreGameAd();
        }
    }
}
