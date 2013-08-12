using PromotionWebApplication1.Assets;
using PromotionWebApplication1.Library;
using ScriptCoreLib.ActionScript.Components;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.IO;
using System.Text;
using System.Windows.Media;
//using TestSolutionBuilderV1.Views;

namespace PromotionWebApplication1.Assets
{
    class __foo
    {
    }
}

namespace PromotionWebApplication1
{

    public delegate string AtInstaller(string e);

    public sealed class Application
    {
        public class AudioLink
        {
            public IHTMLAudio Audio;

            public AudioLink Prev;
            public AudioLink Next;
        }

        public void Button1_click(IEvent e)
        {

        }

        private void AddSaveButton(IHTMLElement C, Action<ISaveAction> y)
        {
            //var ss = new SaveActionSprite();

            //ss.AttachSpriteTo(C);

            //ss.WhenReady(y);
        }

        // see: "Z:\jsc.internal.svn\tools\rebuild.installer.bat"
        // see: "Z:\jsc.internal.svn\tools\rebuild.release.bat"
#if !DEBUG
        PromotionWebApplication1.Assets.Publish __Assets;
#endif

        public Application(PromotionWebApplicationHome.HTML.Pages.IDefault app)
        {
            "jsc".ToDocumentTitle();

            // http://www.google.com/support/forum/p/Google+Analytics/thread?tid=486a963e463df665&hl=en
            var gapathname = Native.document.location.pathname;
            var gasearch = Native.document.location.search;
            var gahash = Native.window.escape(Native.document.location.hash);
            var gapageview = gapathname + gasearch + gahash;

            var hash = Native.document.location.hash;

            Action<string> Analytics =
                __hash =>
                {
                    var __gahash = Native.window.escape(__hash);
                    var __gapageview = gapathname + gasearch + __gahash;


                    "UA-13087448-1".ToGoogleAnalyticsTracker(
                        pageTracker =>
                        {
                            pageTracker._setDomainName(".jsc-solutions.net");
                            pageTracker._trackPageview(__gapageview);


                        }
                    );
                };

            Analytics(Native.document.location.hash);

            ////var IsStudio = Native.Document.location.hash.StartsWith("#/studio");

            ////if (Native.Document.location.host.StartsWith("studio."))
            ////{
            ////    IsStudio = true;
            ////}

            ////if (IsStudio)
            ////{
            ////    app.PageContent.Clear();
            ////    new StudioView(
            ////        AddSaveButton
            ////    ).Content.AttachToDocument();
            ////}
            ////else
            {
                PromotionWebApplicationHome.Components.DefaultPageExtensions.AnimateHomePage(app);
            }

            var canvas = new AvalonPromotionBrandIntro.ApplicationCanvas();

            canvas.TriggerOnClick = false;
            canvas.Background = Brushes.Transparent;

            canvas.AnimationAllWhite +=
                delegate
                {
                    Native.document.body.style.backgroundColor = JSColor.None;
                };

            canvas.AnimationStartDelay = 1;

            var JSC_robo2 = new HTML.Audio.FromAssets.JSC_robo2();

            JSC_robo2.load();

            canvas.AnimationShake +=
                delegate
                {
                    JSC_robo2.play();

                };
            canvas.AnimationCompleted +=
                delegate
                {
                    ScriptCoreLib.JavaScript.Extensions.AvalonExtensions.ToHTMLElement(
                        canvas
                    ).Orphanize();

                };

            canvas.AttachToContainer(Native.document.body);
            canvas.AutoSizeTo(Native.document.body);

            canvas.AnimationStartDelay = 1;
            canvas.PrepareAnimation()();
        }

#if false
        public void __Application(IApplicationLoader app)
        {
            //app.LoadingAnimation.FadeOut();

            var DefaultTitle = "jsc solutions";


            Native.Document.title = DefaultTitle;

            StringActionAction GetTitleFromServer = new UltraWebService().GetTitleFromServer;



            GetTitleFromServer(
                n => Native.Document.title = n
            );

            var MyPagesBackground = new IHTMLDiv
            {

            };

            MyPagesBackground.style.overflow = IStyle.OverflowEnum.hidden;
            MyPagesBackground.style.position = IStyle.PositionEnum.absolute;
            MyPagesBackground.style.width = "100%";
            MyPagesBackground.style.height = "100%";
            MyPagesBackground.AttachToDocument();

            var MyPages = new IHTMLDiv
            {

            };

            MyPages.style.overflow = IStyle.OverflowEnum.auto;
            MyPages.style.position = IStyle.PositionEnum.absolute;
            MyPages.style.width = "100%";
            MyPages.style.height = "100%";
            MyPages.AttachToDocument();

            var MyPagesInternal = new IHTMLDiv();

            MyPagesInternal.style.margin = "4em";
            MyPagesInternal.AttachTo(MyPages);

            // http://www.google.com/support/forum/p/Google+Analytics/thread?tid=486a963e463df665&hl=en
            var gapathname = Native.Document.location.pathname;
            var gasearch = Native.Document.location.search;
            var gahash = Native.Window.escape(Native.Document.location.hash);
            var gapageview = gapathname + gasearch + gahash;

            var hash = Native.Document.location.hash;

            Action<string> Analytics = delegate { };

        #region logo
            {
                var IsStudio = Native.Document.location.hash.StartsWith("#/studio");

                if (Native.Document.location.host.StartsWith("studio."))
                {
                    IsStudio = true;
                }

                if (IsStudio)
                {
                    new StudioView(AddSaveButton).Content.AttachToDocument();
                }
                else if (Native.Document.location.hash.StartsWith("#/docs"))
                {
                    var view = new DocumentationCompilationViewer();

                    view.TouchTypeSelected +=
                        type =>
                        {
                            Native.Document.location.hash = "#/docs/" + type.FullName;

                            Analytics("#/docs/" + type.FullName);
                        };

                }
                else if (Native.Document.location.hash.StartsWith("#/warehouse"))
                {
                    new UltraWebService().ThreeDWarehouse(
                        y =>
                        {
                            Func<string, IHTMLAnchor> Build =
                                mid =>
                                {
                                    var a = new IHTMLAnchor { href = "http://sketchup.google.com/3dwarehouse/details?ct=hppm&mid=" + mid }.AttachTo(MyPagesInternal);
                                    var img = new IHTMLImage { src = "http://sketchup.google.com/3dwarehouse/download?rtyp=st&ctyp=other&mid=" + mid }.AttachTo(a);

                                    return a;
                                };

                            var imgs = Enumerable.ToArray(
                                from k in y.Elements()
                                select Build(k.Value)

                            );
                        }
                    );

                }

                else if (Native.Document.location.hash == "#/source")
                {

                    var sln = new TreeNode(() => new VistaTreeNodePage());

                    sln.Text = "Solution";
                    sln.IsExpanded = true;

                    Action<TreeNode> AddReferences =
                        p =>
                        {
                            var r = p.Add("References", new References());

                            r.Add("System", new Assembly());
                            r.Add("System.Core", new Assembly());
                            r.Add("ScriptCoreLib", new Assembly());
                            r.Add("ScriptCoreLib.Ultra", new Assembly());
                            r.Add("ScriptCoreLib.Ultra.Library", new Assembly());
                            r.Add("ScriptCoreLib.Ultra.Controls", new Assembly());
                            r.Add("ScriptCoreLibJava", new Assembly());
                            r.Add("jsc.meta", new Assembly());
                        };

                    Action<TreeNode> AddUltraSource =
                        p =>
                        {
                            var my = p.Add("My.UltraSource");
                            my.Add("Default.htm", new HTMLDocument());
                            my.Add("jsc.png", new ImageFile());

                        };

                    {
                        var p = sln.Add("Visual C# Project", new VisualCSharpProject());


                        AddReferences(p);
                        AddUltraSource(p);



                        p.Add("Application.cs", new VisualCSharpCode());
                        p.Add("WebService.cs", new VisualCSharpCode());
                        p.Add("Program.cs", new VisualCSharpCode());
                    }

                    {
                        var p = sln.Add("Visual Basic Project", new VisualBasicProject());

                        AddReferences(p);
                        AddUltraSource(p);

                        p.Add("Application.vb", new VisualBasicCode());
                        p.Add("WebService.vb", new VisualBasicCode());
                        p.Add("Program.vb", new VisualBasicCode());
                    }


                    {
                        var p = sln.Add("Visual F# Project", new VisualFSharpProject());

                        AddReferences(p);
                        AddUltraSource(p);


                        p.Add("Application.fs", new VisualFSharpCode());
                        p.Add("WebService.fs", new VisualFSharpCode());
                        p.Add("Program.fs", new VisualFSharpCode());
                    }

                    sln.Container.style.Float = IStyle.FloatEnum.right;
                    sln.Container.AttachTo(MyPagesInternal);

                    new SourceEditorHeader().Container.AttachTo(MyPagesInternal);

                    //new IHTMLElement(IHTMLElement.HTMLElementEnum.h1, "Create your own Ultra Application project template").AttachTo(MyPagesInternal);

                    var n = new TextEditor(MyPagesInternal);

                    n.Width = 600;
                    n.Height = 400;

                    //n.InnerHTML = "<p>Create your own <b>Ultra Application</b> Project Template</p>";


                    new DefaultPage1().Container.AttachTo(n.Document.body);

                    var m1 = new SimpleCodeView();

                    m1.Container.AttachTo(MyPagesInternal);
                    //m1.SelectType.onchange +=
                    //    delegate
                    //    {
                    //        m1.TypeName.innerText = m1.SelectType.value;
                    //    };

                    //m1.RunJavaScript.onclick +=
                    //    delegate
                    //    {
                    //        m1.RunJavaScript.style.color = JSColor.Blue;

                    //        try
                    //        {
                    //            Native.Window.eval(m1.Code1.value);

                    //            1000.AtDelay(
                    //                delegate
                    //                {
                    //                    m1.RunJavaScript.style.color = JSColor.None;
                    //                }
                    //            );
                    //        }
                    //        catch
                    //        {
                    //            m1.RunJavaScript.style.color = JSColor.Red;

                    //            1000.AtDelay(
                    //                delegate
                    //                {
                    //                    m1.RunJavaScript.style.color = JSColor.None;
                    //                }
                    //            );
                    //        }
                    //    };
                    new Compilation().GetArchives().SelectMany(k => k.GetAssemblies()).First(k => k.Name == "ScriptCoreLib").WhenReady(
                        ScriptCoreLib =>
                        {
                            // we do not have reflection in place for native wrappers :/

                            m1.SelectEvent.Clear();

                            var Element = ScriptCoreLib.GetTypes().Single(k => k.FullName == "ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement");
                            //var Element = ScriptCoreLib.GetTypes().Single(k => k.HTMLElement == "ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement");

                            Action<CompilationEvent> Add =
                                SourceEvent =>
                                {
                                    m1.SelectEvent.Add(
                                        new IHTMLOption { innerText = SourceEvent.Name }
                                    );
                                };

                            Element.GetEvents().ForEach(Add);

                        }
                    );


                    m1.SelectEvent.onchange +=
                        delegate
                        {
                            m1.EventName.innerText = m1.SelectEvent.value;
                        };




                }
                else if (Native.Document.location.hash == "#/UltraApplicationWithAssets")
                {
                    new UltraApplicationWithAssets().Container.AttachToDocument();
                }
                else
                    if (Native.Document.location.hash == "#/audio")
                    {
                        Action AtTimer = delegate { };

                        (1000 / 15).AtInterval(
                            tt =>
                            {
                                AtTimer();
                            }
                        );

                        new SoundCloudBackground().Container.AttachTo(MyPagesBackground);
                        new SoundCloudHeader().Container.AttachTo(MyPagesInternal);

                        var page = 1;

                        var Tracks = new IHTMLDiv().AttachTo(MyPagesInternal);
                        Tracks.style.margin = "1em";

                        var More = new SoundCloudMore();

                        var AudioLinks = default(AudioLink);

                        var LoadCurrentPage = default(Action);

                        LoadCurrentPage = delegate
                        {
                            var loading = new SoundCloudLoading();

                            loading.Container.AttachTo(Tracks);


                            new UltraWebService().SoundCloudTracksDownload(
                                System.Convert.ToString(page),
                                ee =>
                                {
                                    if (loading != null)
                                    {
                                        loading.Container.Orphanize();
                                        loading = null;
                                    }

                                    var t = new SoundCloudTrack();

                                    t.Content.ApplyToggleConcept(t.HideContent, t.ShowContent).Hide();

                                    t.Title.innerHTML = ee.trackName;
                                    t.Waveform.src = ee.waveformUrl;

                                    t.Audio.src = ee.streamUrl;
                                    t.Audio.autobuffer = true;


                                    AudioLinks = new AudioLink
                                    {
                                        Audio = t.Audio,
                                        Prev = AudioLinks
                                    };

                                    var _AudioLinks = AudioLinks;

                                    if (AudioLinks.Prev != null)
                                        AudioLinks.Prev.Next = AudioLinks;
                                    else
                                        // we are the first  :)
                                        t.Audio.play();

                                    t.MoreButton.onclick +=
                                        delegate
                                        {
                                            t.Audio.pause();

                                            if (_AudioLinks.Next != null)
                                            {
                                                _AudioLinks.Next.Audio.currentTime = 0;
                                                _AudioLinks.Next.Audio.play();

                                                if (_AudioLinks.Next.Next == null)
                                                {
                                                    page++;
                                                    LoadCurrentPage();
                                                }
                                            }
                                        };

                                    t.Audio.onended +=
                                        delegate
                                        {
                                            if (_AudioLinks.Next != null)
                                            {
                                                _AudioLinks.Next.Audio.currentTime = 0;
                                                _AudioLinks.Next.Audio.play();

                                                if (_AudioLinks.Next.Next == null)
                                                {
                                                    page++;
                                                    LoadCurrentPage();
                                                }
                                            }
                                        };

                                    t.Identity.innerText = ee.uid;

                                    t.Play.onclick += eee => { eee.PreventDefault(); t.Audio.play(); };
                                    t.Pause.onclick += eee => { eee.PreventDefault(); t.Audio.pause(); };

                                    t.Title.style.cursor = IStyle.CursorEnum.pointer;
                                    t.Title.onclick += eee =>
                                        {
                                            eee.PreventDefault();

                                            var playing = true;

                                            if (t.Audio.paused)
                                                playing = false;

                                            if (t.Audio.ended)
                                                playing = false;

                                            if (!playing)
                                                t.Audio.play();
                                            else
                                                t.Audio.pause();
                                        };

                                    DoubleAction SetProgress1 = p =>
                                    {

                                        t.Gradient3.style.width = System.Convert.ToInt32(800 * p) + "px";
                                        t.Gradient4.style.width = System.Convert.ToInt32(800 * p) + "px";
                                    };

                                    t.Gradient5.style.Opacity = 0.4;
                                    t.Gradient6.style.Opacity = 0.4;

                                    DoubleAction SetProgress2 = p =>
                                    {

                                        t.Gradient5.style.width = System.Convert.ToInt32(800 * p) + "px";
                                        t.Gradient6.style.width = System.Convert.ToInt32(800 * p) + "px";
                                    };

                                    AtTimer +=
                                        delegate
                                        {
                                            if (t.Audio.duration == 0)
                                            {
                                                t.Play.Hide();
                                                t.Pause.Hide();
                                                return;
                                            }
                                            else
                                            {

                                                var playing = true;

                                                if (t.Audio.paused)
                                                    playing = false;

                                                if (t.Audio.ended)
                                                    playing = false;

                                                if (!playing)
                                                    t.Title.style.color = Color.None;
                                                else
                                                    t.Title.style.color = Color.Blue;

                                                t.Play.Show(!playing);
                                                t.Pause.Show(playing);
                                            }

                                            var p = t.Audio.currentTime / t.Audio.duration;
                                            SetProgress1(p);
                                        };

                                    t.Waveform.onmouseout +=
                                        delegate
                                        {
                                            SetProgress2(0);
                                        };

                                    t.Waveform.onmousemove +=
                                        eee =>
                                        {
                                            SetProgress2(eee.OffsetX / 800.0);
                                        };

                                    t.Waveform.onclick +=
                                        eee =>
                                        {
                                            t.Audio.currentTime = t.Audio.duration * (eee.OffsetX / 800.0);
                                            t.Audio.play();
                                        };

                                    t.Waveform.style.cursor = IStyle.CursorEnum.pointer;

                                    SetProgress1(0);
                                    SetProgress2(0);

                                    t.Container.AttachTo(Tracks);
                                }
                            );


                            10000.AtDelay(
                                delegate
                                {
                                    More.MoreButton.FadeIn(0, 1000, null);
                                }
                            );
                        };


                        More.MoreButton.Hide();
                        More.Container.AttachTo(MyPagesInternal);

                        More.MoreButton.onclick += eee =>
                            {
                                eee.PreventDefault();
                                More.MoreButton.FadeOut(1, 300,
                                    delegate
                                    {
                                        page++;
                                        LoadCurrentPage();
                                    }
                                );
                            };

                        LoadCurrentPage();

                    }
                    else
                    {
                        //new PromotionWebApplication1.HTML.Audio.FromAssets.Track1 { controls = true }.AttachToDocument();
                        //new PromotionWebApplication1.HTML.Audio.FromWeb.Track1 { controls = true, autobuffer = true }.AttachToDocument();

                        var IsAvalonJavaScript = hash == "#/avalon.js";
                        var IsAvalonActionScript = hash == "#/avalon.as";
                        var IsAvalon = IsAvalonActionScript || IsAvalonJavaScript;

                        //if (IsAvalon)
                        //{

                        //{
                        //    var ccc = new IHTMLDiv();

                        //    ccc.style.position = IStyle.PositionEnum.absolute;
                        //    ccc.style.left = "15%";
                        //    ccc.style.right = "15%";
                        //    ccc.style.top = "15%";


                        //    var Now = DateTime.Now;

                        //    var CountDown = new CountDownGadgetConcept(CountDownGadget.Create)
                        //    {
                        //        ShowOnlyDays = true,
                        //        Event = new DateTime(2010, 5, 24, 23, 59, 50),

                        //    };

                        //    CountDown.Element.GadgetContainer.style.color = "#808080";
                        //    CountDown.Element.GadgetContainer.style.textShadow = "#E0E0E0 1px 1px 1px";


                        //    CountDown.Element.GadgetContainer.AttachTo(ccc);
                        //    CountDown.Element.GadgetContainer.FadeIn(3000, 2000, null);

                        //    ccc.AttachToDocument();
                        //}

                        {
                            var ccc = new IHTMLDiv();

                            ccc.style.position = IStyle.PositionEnum.absolute;
                            ccc.style.left = "50%";
                            ccc.style.top = "50%";
                            ccc.style.marginLeft = (-JSCSolutionsNETCarouselCanvas.DefaultWidth / 2) + "px";
                            ccc.style.marginTop = (-JSCSolutionsNETCarouselCanvas.DefaultHeight / 2) + "px";

                            ccc.style.SetSize(JSCSolutionsNETCarouselCanvas.DefaultWidth, JSCSolutionsNETCarouselCanvas.DefaultHeight);

                            ccc.AttachToDocument();

                            if (IsAvalonActionScript)
                            {
                                var alof = new UltraSprite();
                                alof.ToTransparentSprite();
                                alof.AttachSpriteTo(ccc);
                            }
                            else
                            {
                                var alo = new JSCSolutionsNETCarouselCanvas();
                                alo.Container.AttachToContainer(ccc);

                                alo.AtLogoClick +=
                                    delegate
                                    {
                                        //Native.Window.open("http://sourceforge.net/projects/jsc/", "_blank");
                                        Native.Window.open("/download", "_blank");
                                    };

                            }
                        }
                        //}
                        //else
                        //{
                        //    var cc = new HTML.Pages.FromAssets.Controls.Named.CenteredLogo_Kamma();

                        //    cc.Container.AttachToDocument();

                        //    // see: http://en.wikipedia.org/wiki/Perl_control_structures
                        //    // "Unless" == "if not"  ;)

                        //    IsMicrosoftInternetExplorer.YetIfNotThen(cc.TheLogoImage.BeginPulseAnimation).ButIfSoThen(cc.TheLogoImage.HideNowButShowAtDelay);
                        //}

                        var aa = new About();
                        aa.Service.innerText = gapageview;
                        aa.Container.AttachToDocument();

                    }
            }
        #endregion


            Analytics =
                __hash =>
                {
                    var __gahash = Native.Window.escape(__hash);
                    var __gapageview = gapathname + gasearch + __gahash;


                    "UA-13087448-1".ToGoogleAnalyticsTracker(
                        pageTracker =>
                        {
                            pageTracker._setDomainName(".jsc-solutions.net");
                            pageTracker._trackPageview(__gapageview);


                        }
                    );
                };

            Analytics(Native.Document.location.hash);


        }

#endif

        /// <summary>
        /// Microsoft Internet Explorer does not support using opacity on an image with an alpha layer.
        /// </summary>
        public static bool IsMicrosoftInternetExplorer
        {
            get
            {
                return (bool)new IFunction("/*@cc_on return true; @*/ return false;").apply(null, new object[] { });
            }
        }



    }


    public delegate void StringAction(string e);
    public delegate void StringActionAction(StringAction e);


    public static class DownloadSDKFunction
    {
        public static void DownloadSDK(WebServiceHandler h)
        {
            const string _download = "/download/";
            const string a = @"assets/PromotionWebApplicationAssets";

            var path = h.Context.Request.Path;

            if (path == "/download")
            {
                //h.Context.Response.Redirect("/download/jsc.configuration.application");

                // F# redist
                h.Context.Response.Redirect("/download/jsc-web-installer.exe");
                h.CompleteRequest();
                return;
            }

            if (path == "/download/jsc-web-installer.exe")
            {
                // http://msdn.microsoft.com/en-us/library/h4k032e1.aspx
                // is chrome happier if we rename it?
                path = "/download/setup.exe";
            }

            if (path == "/download/")
            {
                var href = "http://www.jsc-solutions.net/download/jsc-web-installer.exe";

                var html = @"
                    <meta http-equiv='Refresh' target='_top' content='1;url=" + href + @"' />

                    
                    <center>
                    
                    <br />
                    <br />
                    <br />

<a href='" + href + @"'>Thank you for downloading JSC!</a>
                     
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />

<div><i>Note that recent versions of <b>Google Chrome</b> may need additional time to verify.</i></div>                  
                                       
                                       </center>";

                h.Context.Response.ContentType = "text/html";

                var bytes = Encoding.UTF8.GetBytes(html);
                h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                h.CompleteRequest();
                return;
            }


#if !DEBUG
            // we will compare the win32 relative paths here...
            var publish = path.SkipUntilOrEmpty("/download/").Replace("/", @"\");
            var p = new Publish();

            if (p.ContainsKey(publish))
            {
                var f = p[publish];


                var ext = "." + f.SkipUntilLastOrEmpty(".").ToLower();

                // http://en.wikipedia.org/wiki/Mime_type
                // http://msdn.microsoft.com/en-us/library/ms228998.aspx

                var ContentType = "application/octet-stream";

                if (ext == ".application")
                {
                    ContentType = "application/x-ms-application";
                }
                else if (ext == ".manifest")
                {
                    ContentType = "application/x-ms-manifest";
                }
                else if (ext == ".htm")
                {
                    ContentType = "text/html";
                }

                h.Context.Response.ContentType = ContentType;

                //Console.WriteLine("length: " + data.Length + " " + ContentType + " " + f);


                DownloadSDKFile(h, f);


            }
#endif


            return;
        }

        private static void DownloadSDKFile(WebServiceHandler h, string f)
        {
            Console.WriteLine("download: " + f);

            var bytes = System.IO.File.ReadAllBytes(f);

            h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            h.CompleteRequest();
        }
    }
}
