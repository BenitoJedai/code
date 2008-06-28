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
using ScriptCoreLib.ActionScript.flash.text;

namespace FlashTowerDefense.ActionScript
{
    [Script]
    public class Animation : Sprite
    {
        readonly BitmapAsset StillFrame;
        readonly BitmapAsset[] AnimatedFrames;

        Timer _Timer;

        public int FrameRate = (1000 / 15);

        public event Action AnimationEnabledChanged;

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

                if (AnimationEnabledChanged != null)
                    AnimationEnabledChanged();
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

            if (AnimatedFrames != null)
            {
                foreach (var v in AnimatedFrames)
                {
                    v.Orphanize();
                }
            }
        }

        //TextField _NetworkIdLabel;

        int _NetworkId;
        public int NetworkId
        {
            get
            {
                return _NetworkId;
            }
            set
            {
                _NetworkId = value;

                //if (_NetworkIdLabel == null)
                //{
                //    _NetworkIdLabel = new TextField
                //    {
                //        autoSize = TextFieldAutoSize.LEFT,
                //        mouseEnabled = false,
                //        text = "" + _NetworkId
                //    }.AttachTo(this);
                //}
                //else
                //{
                //    _NetworkIdLabel.text = "" + _NetworkId;
                //}
            }
        }

        public Animation(Class StillFrame, params Class[] AnimatedFrames)
        {
            this.StillFrame = StillFrame;

            if (AnimatedFrames.Length > 0)
                this.AnimatedFrames = AnimatedFrames.Select(i => (BitmapAsset)i).ToArray();
            else
                AnimationEnabled = false;

            ShowCurrentFrame();
        }
    }

}
