using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace FlashTowerDefense.ActionScript.Monetized
{
    [Script, ScriptApplicationEntryPoint(Width = FlashTowerDefense.Width, Height = FlashTowerDefense.Height)]
    [SWF(width = FlashTowerDefense.Width, height = FlashTowerDefense.Height, backgroundColor = FlashTowerDefense.ColorWhite)]
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
                id = "408b0484d7f64aad",
                res = "640x480",
                ad_finished =
                    delegate
                    {
                        new FlashTowerDefense().AttachTo(stage);
                    }

            }.showPreGameAd();
        }
    }
}
