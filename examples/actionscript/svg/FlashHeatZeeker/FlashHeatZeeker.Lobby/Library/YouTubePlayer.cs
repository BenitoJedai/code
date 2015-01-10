using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;

namespace InteractivePromotionB.Components
{
    internal static class DynamicYouTubePlayer
    {
        // we should have dynamic by now.

        // dynamic keyword please!!

        [Script(OptimizedCode = "e.seekTo(seconds, allowSeekAhead);")]
        public static void seekTo(this object e, double seconds, bool allowSeekAhead = true)
        {

        }

        [Script(OptimizedCode = "return e.data;")]
        public static YouTubePlayerState get_data_as_YouTubePlayerState(this object e)
        {
            return default(YouTubePlayerState);
        }

        [Script(OptimizedCode = "return e.getDuration();")]
        public static double getDuration(this object e)
        {
            return default(double);
        }

        [Script(OptimizedCode = "return e.getCurrentTime();")]
        public static double getCurrentTime(this object e)
        {
            return default(double);
        }

        [Script(OptimizedCode = "e.playVideo();")]
        public static void playVideo(this object e)
        {

        }

        [Script(OptimizedCode = "e.pauseVideo();")]
        public static void pauseVideo(this object e)
        {

        }

        // http://code.google.com/apis/youtube/flash_api_reference.html#setSize
        // ArgumentError: Error #1063: Argument count mismatch on com.google.youtube.application::VideoApplication/resizeApplication(). Expected 2, got 0.
        [Script(OptimizedCode = "e.setSize(width, height);")]
        public static void setSize(this object e, int width, int height)
        {

        }

        // http://code.google.com/apis/youtube/flash_api_reference.html#loadVideoById

        [Script(OptimizedCode = "e.loadVideoById(videoId, startSeconds, suggestedQuality);")]
        public static void loadVideoById(this object e, string videoId, double startSeconds = 0.0, string suggestedQuality = "hd720")
        {

        }
    }

    enum YouTubePlayerState
    {
        unknown = -127,

        unstarted = -1,
        ended = 0,
        playing = 1,
        paused = 2,
        buffering = 3,
        cued = 5
    }

    public class YouTubePlayer
    {
        public readonly Loader Loader;

        public event Action<Scene> NotPlaying;
        public event Action<Scene> Playing;
        public event Action<Scene> Paused;

        public event Action<string> StatusToClients;

        public readonly Scene DefaultScene;

        public Func<Scene, Scene> SceneTranslate = k => k;

        public Action<string> loadVideoById;

        public string CurrentVideoId;

        public Action pauseVideo;

        public YouTubePlayer(
            string TargetContent = "http://www.youtube.com/apiplayer?version=3",
            int DefaultWidth = 1280,
            int DefaultHeight = 720,
            string DefaultVideo = "Z__-3BbPq6g",

            string suggestedQuality = "hd720",


            Action yield_init = null
            )
        {
            var ldr = new Loader();
            this.Loader = ldr;
            var urlReq = new URLRequest(TargetContent);

            var ctx_app = ApplicationDomain.currentDomain;
            var ctx_sec = SecurityDomain.currentDomain;

            // http://www.youtube.com/crossdomain.xml
            ctx_app = null;
            ctx_sec = null;

            // http://www.zedia.net/2010/using-the-actionscript-3-youtube-api/

            bool once = false;

            #region onReady
            Action<Event> onReady = e =>
            {
                if (once)
                    return;

                once = true;


#if JSC_FEATURE_dynamic
                        dynamic player = ldr.content;

                        player.setSize(160, 120);
#endif
                ldr.content.setSize(DefaultWidth, DefaultHeight);

                pauseVideo = delegate
                {
                    ldr.content.pauseVideo();
                };

                loadVideoById = x =>
                {
                    CurrentVideoId = x;

                    ldr.content.loadVideoById(x, suggestedQuality: suggestedQuality);
                };

                loadVideoById(DefaultVideo);

                if (yield_init != null)
                {
                    yield_init();
                    yield_init = null;
                }


            };
            #endregion


            var PreviousCurrentState = YouTubePlayerState.unknown;
            var CurrentState = YouTubePlayerState.unknown;

            DefaultScene = this["default", 0, 1000];
            var CurrentScene = DefaultScene;
            Action CurrentSceneDone = delegate { };

            #region onStateChange
            Action<Event> onStateChange = e =>
            {
                PreviousCurrentState = CurrentState;
                CurrentState = e.get_data_as_YouTubePlayerState();

                if (PreviousCurrentState != CurrentState)
                {
                    if (CurrentState == YouTubePlayerState.playing)
                    {
                        // notify other scenes of delinking?

                        if (this.Playing != null)
                            this.Playing(SceneTranslate(CurrentScene));

                        this.ReferencedScenes.WithEach(
                            k =>
                            {
                                k.RaiseLinkDenotification();
                            }
                        );
                    }
                    else
                    {
                        if (this.NotPlaying != null)
                            this.NotPlaying(SceneTranslate(CurrentScene));
                    }

                    if (CurrentState == YouTubePlayerState.paused)
                    {
                        if (this.Paused != null)
                            this.Paused(SceneTranslate(CurrentScene));

                    }
                }
            };
            #endregion


            var TimeToPause = 0.4;

            var t = new Timer(1000 / 100);

            var PlaySceneCounter = 0;

            t.timer +=
                delegate
                {
                    if (ldr.content == null)
                        return;

                    if (CurrentState == YouTubePlayerState.playing)
                    {
                        var time = ldr.content.getCurrentTime();

                        var time_index = (int)time;

                        var duration = ldr.content.getDuration();

                        var playall = CurrentScene.end > duration;

                        // flag4 = ((double0 < (double2 - 500)) == 0);
                        // 1 second is 1.0!! :)
                        var notending = time < (duration - 0.500);
                        //var xending = time >= (duration - 500);
                        var ending = !notending;

                        // ReferenceError: Error #1069: Property getDuration not found on flash.display.Loader and there is no default value.
                        var m = new { PlaySceneCounter, time, time_index, CurrentScene.end, duration, playall, ending }.ToString();

                        if (StatusToClients != null)
                            StatusToClients(m);

                        // phone activated

                        if (playall)
                        {
                            if (ending)
                            {
                                ldr.content.pauseVideo();
                                CurrentSceneDone();
                            }
                        }
                        else if (time >= (TimeToPause))
                        {
                            ldr.content.pauseVideo();
                            CurrentSceneDone();
                        }
                    }
                };

            t.start();

            #region PlayScene
            this.PlayScene =
                (e, Done) =>
                {
                    PlaySceneCounter++;

                    //if (e.end == 0)
                    //    e.end = ldr.content.getDuration() - 1000;

                    CurrentScene = e;
                    CurrentSceneDone = Done;

                    TimeToPause = e.end;

                    ldr.content.seekTo(e.start);
                    ldr.content.playVideo();
                };
            #endregion



            ldr.contentLoaderInfo.ioError +=
                delegate
                {

                };

            ldr.contentLoaderInfo.init +=
                delegate
                {
                    ldr.content.addEventListener("onReady", onReady.ToFunction(), false, 0, false);
                    ldr.content.addEventListener("onStateChange", onStateChange.ToFunction(), false, 0, false);

                };

            var ctx = new LoaderContext(true, ctx_app, ctx_sec);
            ldr.load(urlReq, ctx);


            this.Scenes = new SceneSequenzer { Owner = this };
        }

        public class Scene : InteractivePromotionB.Components.IScene
        {
            public string name { get; set; }
            public double start { get; set; }
            public double end { get; set; }

            public YouTubePlayer Owner;



            public static implicit operator Action<Action>(Scene e)
            {
                return e.PlayScene;
            }

            public void PlayScene(Action Done)
            {
                Owner.PlayScene(this, Done);
            }

            public event Action LinkDenotification;

            public void RaiseLinkDenotification()
            {
                if (LinkDenotification != null)
                    LinkDenotification();
            }

            public event Action LinkNotification;

            public void RaiseLinkNotification()
            {
                if (LinkNotification != null)
                    LinkNotification();
            }



        }


        public SceneSequenzer Scenes;

        public class SceneSequenzer
        {
            public YouTubePlayer Owner;

            public double ScenePadding = 0.1;
            public double SceneDelay = 4;
            public double SceneTransitions = 0.5;

            public double Offset;


            public Scene this[string name, double extra = 0.0]
            {
                get
                {
                    var n = this.Owner[name,
                        Offset + SceneDelay - ScenePadding,
                        Offset + SceneDelay + SceneTransitions + ScenePadding + extra
                    ];

                    Offset += SceneDelay + SceneTransitions;

                    return n;
                }
            }
        }



        public Action<Scene, Action> PlayScene;

        public readonly List<Scene> ReferencedScenes = new List<Scene>();

        public Scene this[string name, double start, double end]
        {
            get
            {
                var s = new Scene
                {
                    start = start,
                    end = end,
                    name = name,

                    Owner = this
                };

                ReferencedScenes.Add(s);

                return s;
            }
        }
    }


}
