using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Controls.NatureBoy;

namespace NatureBoyTestPad.js
{
    [Script]
    static class MyFrames
    {
        
        public static DudeAnimationInfo NPC3
        {
            get
            {
                var a = new[]
                    {
                        Assets.Path + "/npc3_rt2.gif",
                        Assets.Path + "/npc3_fr2.gif",
                        Assets.Path + "/npc3_lf2.gif",
                        Assets.Path + "/npc3_bk2.gif"
                    };

                var b = new[]
                    {
                        Assets.Path + "/npc3_rt1.gif",
                        Assets.Path + "/npc3_fr1.gif",
                        Assets.Path + "/npc3_lf1.gif",
                        Assets.Path + "/npc3_bk1.gif"
                    };

                var ax = a.Select(Source => new FrameInfo { Source = Source, Weight = 1 / a.Length }).ToArray();
                var bx = b.Select(Source => new FrameInfo { Source = Source, Weight = 1 / b.Length }).ToArray();

                return new DudeAnimationInfo
                {
                    Frames_Stand = ax,
                    Frames_Walk = new [] { bx, ax }
                };
            }
        }
    }
}
