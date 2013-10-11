using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Ultra.Library.Extensions;
using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using YoutubeCaptions.HTML.Pages;

namespace YoutubeCaptions
{

    //[Description("YoutubeCaptions. Write javascript, flash and java applets within a C# project.")]

    public sealed partial class Application
    {

        public Application(IApp e)
        {
            Native.Document.title = "YoutubeCaptions";



            // Color Theory: Mixing Paint Colors : Color Theory: Mixing Primary Colors
            // http://www.youtube.com/watch?v=COzgtcZzOZw

            Native.Document.body.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.auto;

            //DoVideo("COzgtcZzOZw");
            DoVideo("2MxWAX_HuAw");
        }

        private static void DoVideo(string video)
        {
            var uri = video.ToYoutubeVideo();

            var di = new IHTMLDiv
			{
				new IHTMLAnchor { href = uri.ToString(), innerText = video }
			}.AttachToDocument();

            video.ToCaptionTuples(
                x =>
                {
                    var text = new IHTMLSpan(x.Text);

                    var n = new IHTMLDiv
					{

						text

					}.AttachTo(di);

                    var colors = @"red,blue,green,yellow".Split(',');

                    var c = Enumerable.FirstOrDefault(
                        from k in colors
                        where (" " + x.Text + " ").Contains(" " + k + " ")
                        select k
                    );

                    if (c != null)
                        text.style.backgroundColor = c;

                    if (x.Text.Contains("step"))
                    {
                        n.style.fontSize = "large";
                        n.style.color = "blue";
                        n.style.textDecoration = "underline";
                    }


                    n.title = x.Start + " (" + x.Duration + ")";
                    text.style.marginLeft = x.Start + "px";
                }
            );
        }


    }

    public class CaptionTuple
    {
        public string Text;
        public string Start;
        public string Duration;

        public override string ToString()
        {
            return "[" + Start + "] " + Text;
        }
    }

    public static class MyExtensions
    {
        public static void ToCaptionTuples(this string video, Action<CaptionTuple> yield)
        {
            video.ToCaptions(
                (XDocument x) =>
                {
                    foreach (var item in x.Root.Elements("text"))
                    {
                        var start = item.Attribute("start");
                        var dur = item.Attribute("dur");

                        yield(
                            new CaptionTuple
                            {
                                Text = item.Value,
                                Start = start.Value,
                                Duration = dur.Value
                            }
                        );
                    }
                }
            );
        }

        public static void ToCaptions(this string video, Action<XDocument> e)
        {

            new UltraWebService().GetCaptions(video,
                xml =>
                {
                    e(XDocument.Parse(xml));
                }
            );
        }
    }

    public delegate void StringAction(string e);

    public static class My2Extensions
    {
        public static Uri ToYoutubeVideo(this string video)
        {
            var uri = new Uri("http://www.youtube.com/watch?v=" + video);
            return uri;
        }
    }

    public sealed class UltraWebService
    {


        public void GetCaptions(string video, StringAction result)
        {
            if (string.IsNullOrEmpty(video))
                video = "LT_x9s67yWA";

            var uri = video.ToYoutubeVideo();

            var c = new WebClient();

            var html = c.DownloadString(uri);

            var ttsurl = Uri.UnescapeDataString(html.SkipUntilIfAny("\"ttsurl\":").SkipUntilIfAny("\"").TakeUntilIfAny("\""));

            // 20131011 they changed it!
            ttsurl = ttsurl.Replace(@"\/", "/");
            // http://video.google.com/timedtext?
            //	sparams=caps%2Cexpire%2Cv&
            //	expire=1268420400&
            //	caps=asr&
            //	key=yttt1&
            //	signature=8F7DADCF868F8302AD31C92F5D4F54532F24583E.9AD0492582C05330E9CFA753B1298D71BE71FD5F

            // http://video.google.com/timedtext?
            //	sparams=caps%2Cexpire%2Cv
            //	expire=1268420400&
            //	caps=asr&
            //	key=yttt1&
            //	signature=8F7DADCF868F8302AD31C92F5D4F54532F24583E.9AD0492582C05330E9CFA753B1298D71BE71FD5F&

            //	name=&
            //	v=LT_x9s67yWA&
            //	lang=en&
            //	type=track&
            //	hl=en&
            //	kind=asr&
            //	ts=1268397349057&

            ttsurl += "&name=" + "";
            ttsurl += "&v=" + video;
            ttsurl += "&lang=" + "en";
            ttsurl += "&type=" + "track";
            ttsurl += "&hl=" + "en";
            ttsurl += "&kind=" + "asr";
            ttsurl += "&ts=" + "";

            // http:\/\/www.youtube.com\/api\/timedtext?expire=1381505182\u0026signature=A76A5798EECE7ADFE877BC5BC190A6C26F68FB65.7D5595D5BFA370BDA21BBE053E77C7A57CCB40C1\u0026asr_langs=ja,de,pt,ru,ko,en,nl,es,fr,it\u0026hl=en_US\u0026sparams=asr_langs,caps,v,expire\u0026v=COzgtcZzOZw\u0026caps=asr\u0026key=yttt1&name=&v=COzgtcZzOZw&lang=en&type=track&hl=en&kind=asr&ts=

            //Request URL:http://www.youtube.com/api/timedtext?
            // hl=en-US&
            // asr_langs=pt%2Ces%2Cnl%2Cfr%2Cru%2Cja%2Cen%2Cit%2Cko%2Cde&
            // caps=asr&
            // key=yttt1&
            // signature=15D631B86414ABBAEE1AC9D89767A4D4B65EE3CB.89D937A606E2B198CFC6392E70B9F791B4C17F99&
            // v=COzgtcZzOZw&
            // expire=1381505508&
            // sparams=asr_langs%2Ccaps%2Cv%2Cexpire&type=track&
            // lang=en&
            // name&
            // kind

            Console.WriteLine(new { ttsurl });

            // The remote server returned an error: (404) Not Found.
            // do we have to send a cookie?
            var tts = c.DownloadString(new Uri(ttsurl));

            result(tts);
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
