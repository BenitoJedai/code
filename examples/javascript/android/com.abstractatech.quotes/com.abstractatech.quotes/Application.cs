using com.abstractatech.quotes.HTML.Audio.FromAssets;
using com.abstractatech.quotes.HTML.Pages;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace com.abstractatech.quotes
{
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();


        public Application(IDefault page)
        {
            var q = InitializeDictionary();

            var h = "";

            #region Initialize
            var Current = default(IHTMLAudio);

            Action Done = delegate { };

            var prefetch = "";

            #region Next
            var Next = q.Keys.AsEnumerable().Randomize().ToCyclicAction(
                kk =>
                {
                    var k = prefetch;
                    prefetch = kk;
                    //q[kk].load();

                    if (k == "")
                        return;

                    var html = k.ToLower();

                    h.Split(' ').WithEach(
                        hh =>
                        {
                            html = html.Replace(" " + hh, " <b>" + hh.ToUpper() + "</b>");
                        }
                    );

                    page.Content.innerHTML = html;

                    #region color
                    page.Content.style.color = JSColor.Yellow;

                    new Timer(
                        t =>
                            page.Content.style.color = JSColor.White
                    ).StartTimeout(20);

                    new Timer(
                            t =>
                                page.Content.style.color = JSColor.Yellow
                        ).StartTimeout(70);
                    #endregion

                    if (Current != null)
                        Current.pause();

                    var LocalCurrent = q[k]();
                    Current = LocalCurrent;
                    var qk = Current;


                    //http://code.metager.de/source/xref/CyanogenMod/AndroidFrameworksBase/media/libstagefright/OMXCodec.cpp I/MediaExtractor(   84): Autodetected media content as 'audio/mpeg' with confidence 0.20
                    //V/AwesomePlayer(   84): mBitrate = 128000 bits/sec
                    //V/AwesomePlayer(   84): initAudioDecoder
                    //I/OMXCodec(   84): OMXCodec::Create matchComponentName ((null)), flags (0)
                    //I/OMXCodec(   84): [OMX.SEC.mp3dec] allocating 10 buffers of size 81920 on input port
                    //I/OMXCodec(   84): [OMX.SEC.mp3dec] allocating 5 buffers of size 27648 on output port
                    //V/ANDROID_DRM_TEST(   84): [50] notify (0x5f630, 3, 100, 0)
                    //V/AwesomePlayer(   84): cache has reached EOS, prepare is done.
                    //V/ANDROID_DRM_TEST(   84): [50] notify (0x5f630, 5, 0, 0)
                    //V/ANDROID_DRM_TEST(   84): [50] notify (0x5f630, 1, 0, 0)
                    //V/ANDROID_DRM_TEST(   84): getDuration
                    //V/AwesomePlayer(   84): getDuration (7810612)
                    //V/ANDROID_DRM_TEST(   84): [50] getDuration = 7811
                    //V/ANDROID_DRM_TEST(   84): [50] isPlaying: 0

                    //I/NuHTTPDataSource(   84): connect to 192.168.1.101:13964/assets/com.abstractatech.quotes/s7qSchopenhauser.mp3 @0
                    //I/Web Console( 1658): will play { src = http://192.168.1.101:13964/assets/com.abstractatech.quotes/s7qNeal.mp3 }
                    //V/ANDROID_DRM_TEST(   84): [9] isPlaying: 0
                    //V/ANDROID_DRM_TEST(   84): [10] isPlaying: 0
                    //V/ANDROID_DRM_TEST(   84): [9] isPlaying: 0
                    //V/ANDROID_DRM_TEST(   84): [10] isPlaying: 0

                    // /ANDROID_DRM_TEST

                    Console.WriteLine("will soon play " + new { qk.src });

                    // does android need a timeout here?
                    new Timer(
                        delegate
                        {
                            // D/MediaPlayer(19678): Couldn't open file on client side, trying server side
                            // I/AudioService(  486):  AudioFocus  requestAudioFocus() from android.media.AudioManager@426db750Handler (android.webkit.HTML5Audio) {4275b890}
                            // android only plays inside click?
                            //                            I/AudioService(  486):  AudioFocus  requestAudioFocus() from android.media.AudioManager@42760f80org.chromium.media.MediaPlayerListener@42ac93c8
                            //D/MediaPlayer(19678): Couldn't open file on client side, trying server side
                            //I/AwesomePlayer(  128): setDataSource_l(URL suppressed)
                            //W/TimedEventQueue(  128): Event 19 was not found in the queue, already cancelled?
                            //I/AudioService(  486):  AudioFocus  abandonAudioFocus() from android.media.AudioManager@42760f80org.chromium.media.MediaPlayerListener@42ac7ff8
                            //I/AudioService(  486):  AudioFocus  abandonAudioFocus(): removing entry for android.media.AudioManager@42760f80org.chromium.media.MediaPlayerListener@42ac7ff8

                            Console.WriteLine("will  play " + new { qk.src });
                            qk.play();
                        }
                    ).StartTimeout(900);


                    qk.onended +=
                        delegate
                        {
                            if (Current != LocalCurrent)
                                return;

                            page.Content.style.color = JSColor.White;
                            Current = null;
                            Done();
                        };

                    //q[k] = new IHTMLAudio { src = q[k].src };
                }
            );
            #endregion

            // prefetch
            Next();



            var click = new click();

            page.AutoMode.style.Opacity = 0.5;
            page.AutoMode.onclick +=
                e =>
                {
                    e.PreventDefault();
                    e.StopPropagation();

                    click.play();
                    click = new click();
                    // script: error JSC1000: No implementation found for this native method, please implement [static System.Delegate.op_Equality(System.Delegate, System.Delegate)]
                    if ((object)Done == (object)Next)
                    {
                        page.AutoMode.style.Opacity = 0.5;
                        Done = delegate { };

                        return;
                    }

                    page.AutoMode.style.Opacity = 1;
                    Done = Next;
                    if (Current == null)
                        Next();
                };


            page.AutoMode.style.Opacity = 1;
            Done = Next;
            if (Current == null)
                Next();


            // why doesnt it work anymore??
            page.Content.onclick +=
                delegate
                {
                    Console.WriteLine("click to play!");

                    if (Current != null)
                        Current.play();
                };

            @"Quotes".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"Quotes",
            //    value => value.ToDocumentTitle()
            //);
            #endregion
        }

        private static Dictionary<string, Func<IHTMLAudio>> InitializeDictionary()
        {
            var q = new Dictionary<string, Func<IHTMLAudio>>
            {

               //{"Opportunities multiply as they are being seized", new q001()},
               //{"Knowning is not enough, we must apply, willing is not enough, we must do", new q002()},
               //{"The key to a managers success in the 21st century is the ability to cooperate", new q003()},
               //{"If you can not grab something by the head, grab it by the tail", new q004()},

               //{"What controls your focus controls your life", new quote001()},
               //{"Tomorrow only exists in your mind. Act today!", new quote002()},
               //{"Opportunities multiply as they are being seized", new quote003()}

               {"Your future is created by what you do today, not tomorrow.", () => new  s7q001()},
               {"The reason most major goals are not achieved is that we spend our time doing second things first", () => new  s7qMcKain()},
               {"The art of leadership is saying no, not saying yes. It is very easy to say yes.", () => new  s7qBlair()},
               {"Try not to become a man of success, but rather to become a man of value", () => new  s7qEinstein()},
               {"He who chases two rabbits catches none", () => new  s7qConfucius()},
               {"All our words will be useless, unless they come from within", () => new  s7qTheresa()},
               
               {"Leadership is the capacity to translate vision into reality", () => new  s7qBennis()},
               {"The manager asks how and when; the leader asks what and why", () => new  s7qBennis1()},

               {"If you want something said, ask a man. If you want something done, ask a woman.", () => new  s7qThatcher()},
               {"To be normal is the ideal aim of the unsuccessful", () => new  s7qJung()},
               {"All thruth passes throgh three stages. First, it is ridicule. Second, it is violently opposed. Third, it is accepted as being self-evident.", () => new  s7qSchopenhauser()},
               {"You are only as good as the people you hire.", () => new  s7qKroc()},
               {"He who considers too much will perform little", () => new  s7qSchiller()},
               {"Management is doing things right; leadership is doint the right thing.", () => new  s7qCicero()},
               {"I attribute my success to this: I never gave or took an excuse.", () => new  s7qNightingale()},
               {"If you are sitting, then sit. If you are standing, then stand. Just do not fidget.", () => new  s7qChinese()},
               {"Example is not the main thing in influencing others, it is the only thing.", () => new  s7qSchweitzer()},
               {"The most important thing a leader can do is unleash the potential in people.", () => new  s7qKotter()},
               {"Nothing is more difficult, and therefore more precious, than to be able to decide", () => new  s7qNapoleon()},

               {"The whole point of getting things done is knowing what to leave undone", () => new  s7qSharma()},
               {"A great leader does not necessarily make popular decisions - a great leader makes the right decisions", () => new  s7qSharma1()},
              
               {"The first rule of holes: when you are in one, stop digging", () => new  s7qFranklin()},
               {"Failures do what is tension relieving, while winners do what is goal achieving", () => new  s7qWaitley()},
               {"If you want to go fast, go alone. If you want to go far, go together.", () => new  s7qBuffett()},
               {"You know who the good seamen are when the storm comes.", () => new  s7qEnglish()},
               {"The main thing is to have strength of will, and the skill and the perseverance to realise it; all else is unimportant.", () => new  s7qGoethe()},
               {"The difficult is done at once: the impossible takes a little longer", () => new  s7qLouis()},
               {"Facts do not cease to exist because they are ignored", () => new  s7qHuxley()},
               {"You have to build on your strenghts, not spend the only life you have compensating for your weaknesses", () => new  s7qDrucker()},
               {"I will pay more for the ability to get along with people than any other ability under the sun.", () => new  s7qRockenfeller()},
               {"A certain amount of opposition is a great help to a man. Kites rise against, not with, the wind", () => new s7qNeal()},
           };
            return q;
        }


    }
}
