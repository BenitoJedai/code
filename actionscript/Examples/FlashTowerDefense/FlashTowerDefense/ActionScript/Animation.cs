using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace FlashTowerDefense.ActionScript
{
    [Script]
    class Animation : Sprite
    {
        readonly BitmapAsset StillFrame;
        readonly BitmapAsset[] AnimatedFrames;

        Timer _Timer;

        public int FrameRate = (1000 / 15);

        public bool AnimationEnabled
        {
            get
            {
                return _Timer != null;
            }

            set
            {
                if (_Timer != null)
                {
                    _Timer.stop();
                    _Timer = null;
                }

                Clear();

                if (value)
                {
                    _Timer = FrameRate.AtInterval(
                        delegate
                        {
                            Clear();

                            ShowCurrentFrame();
                        }
                    );

                }

                ShowCurrentFrame();

            }
        }

        private void ShowCurrentFrame()
        {
            if (AnimationEnabled)
                AnimatedFrames[_Timer.currentCount % AnimatedFrames.Length].AttachTo(this).MoveToCenter();
            else
                if (this.StillFrame != null)
                    this.StillFrame.AttachTo(this).MoveToCenter();
        }

        void Clear()
        {
            if (this.StillFrame != null)
                this.StillFrame.Orphanize();

            foreach (var v in AnimatedFrames)
            {
                v.Orphanize();
            }
        }


        public Animation(Class StillFrame, params Class[] AnimatedFrames)
        {
            this.StillFrame = StillFrame;

            this.AnimatedFrames = AnimatedFrames.Select(i => (BitmapAsset)i).ToArray();

            ShowCurrentFrame();
        }
    }

}
