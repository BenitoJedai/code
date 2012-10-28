using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Controls.NatureBoy;
using NatureBoyTestPadExperiment.HTML.Images.FromAssets;

namespace NatureBoyTestPad.js
{
    static class MyFrames
    {
        
        public static DudeAnimationInfo NPC3
        {
            get
            {
                var a = new []
                    {
                        new npc3_rt2().src,
                        new npc3_fr2().src,
                        new npc3_lf2().src,
                        new npc3_bk2().src
                    };

                var b = new[]
                    {
                        new npc3_rt1().src,
                        new npc3_fr1().src,
                        new npc3_lf1().src,
                        new npc3_bk1().src
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

        public static DudeAnimationInfo ManWithHorns
        {
            get
            {
                var a = new NatureBoyTestPadExperiment.HTML.Pages.ManWithHornsImages().Images;
         

        
                return new DudeAnimationInfo
                {
                    
                    Frames_Stand = new [] {
                        new FrameInfo
                        {
                            OffsetY = -16,
                            Source = a[0].src
                        }
                    },
                    Frames_Walk = 

                      a.Select(
                        k=>
                            new [] {
                                new FrameInfo
                                {
                                    OffsetY = -16,
                                    Source = k.src
                                }
                            }
                    
                        ).ToArray()
                };
            }
        }

        public static DudeAnimationInfo TheSheep
        {
            get
            {
                var a = new NatureBoyTestPadExperiment.HTML.Pages.TheSheepImages().Images;



                return new DudeAnimationInfo
                {

                    Frames_Stand = new[] {
                        new FrameInfo
                        {
                            OffsetY = -16,
                            Source = a[0].src
                        }
                    },
                    Frames_Walk =

                      a.Select(
                        k =>
                            new[] {
                                new FrameInfo
                                {
                                    OffsetY = -16,
                                    Source = k.src
                                }
                            }

                        ).ToArray()
                };
            }
        }

        public static DudeAnimationInfo ThePig
        {
            get
            {
                var a = new NatureBoyTestPadExperiment.HTML.Pages.ThePigImages().Images;



                return new DudeAnimationInfo
                {

                    Frames_Stand = new[] {
                        new FrameInfo
                        {
                            OffsetY = -16,
                            Source = a[0].src
                        }
                    },
                    Frames_Walk =

                      a.Select(
                        k =>
                            new[] {
                                new FrameInfo
                                {
                                    OffsetY = -16,
                                    Source = k.src
                                }
                            }

                        ).ToArray()
                };
            }
        }

        public static DudeAnimationInfo TheCactus
        {
            get
            {
                var a = new NatureBoyTestPadExperiment.HTML.Pages.TheCactusImages().Images;



                return new DudeAnimationInfo
                {

                    Frames_Stand = new[] {
                        new FrameInfo
                        {
                            OffsetY = -16,
                            Source = a[0].src
                        }
                    },
                    Frames_Walk =

                      a.Select(
                        k =>
                            new[] {
                                new FrameInfo
                                {
                                    OffsetY = -16,
                                    Source = k.src
                                }
                            }

                        ).ToArray()
                };
            }
        }
    }
}
