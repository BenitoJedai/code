using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;
using ScriptCoreLib.Extensions;

namespace TestMP4LinkToChannelGroups
{
    class Program
    {
        static void Main(string[] args)
        {
            // if a network has a set of mp4 files
            // which are tagged to yt
            // can we get their channel info?

            // X:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs

            var p = "r:/media";

            // should any calculated data be stored into a local sqlite for any restarts?

            // http://blogs.msdn.com/b/csharpfaq/archive/2012/01/23/using-async-for-file-access-alan-berman.aspx

            var sw = Stopwatch.StartNew();

            Console.WriteLine(new { sw.ElapsedMilliseconds, p });

            // can we async/await ourselves onto the other device?
            // closer to the data?
            foreach (var px in Directory.EnumerateFiles(p, "*.mp4"))
            {
                Console.WriteLine(new { sw.ElapsedMilliseconds, px } + " before Create");

                // whats the link?
                // Show Details	Severity	Code	Description	Project	File	Line
                //Error Error signing output with public key from file 'taglib-sharp.snk' -- File not found.taglib-sharp CSC


                // takes a while
                // how would we know how much IO
                // is being moved in this api?
                // would a jsc nuget allow to augment such performance counters?
                TagLib.File videoFile = TagLib.File.Create(px);
                Console.WriteLine(new { sw.ElapsedMilliseconds, px } + " after Create");
                //TagLib.Mpeg4.AppleTag customTag = (TagLib.Mpeg4.Comm)videoFile.GetTag(TagLib.TagTypes.Apple);
                TagLib.Mpeg4.AppleTag customTag = (TagLib.Mpeg4.AppleTag)videoFile.GetTag(TagLib.TagTypes.Apple);
                //customTag.SetDashBox("Producer", "Producer1",link);
                //customTag.Comment = link;
                var link = customTag.Album;
                var videoUrl = link;

                bool isYoutubeUrl = DownloadUrlResolver.TryNormalizeYoutubeUrl(videoUrl, out videoUrl);

                Console.WriteLine(new { sw.ElapsedMilliseconds, px, videoUrl });



                // wont help
                //var y = DownloadUrlResolver.GetDownloadUrls(link);
                //var j = DownloadUrlResolver.LoadJson(videoUrl);
                var c = new WebClient().DownloadString(videoUrl);

                var ch = c.SkipUntilOrEmpty("<a href=\"/channel/");
                var ch_id = ch.TakeUntilOrEmpty("\"");
                var ch_name = ch.SkipUntilOrEmpty(">").TakeUntilOrEmpty("<");

                Console.WriteLine(new { sw.ElapsedMilliseconds, ch_name, ch_id });

                // { ElapsedMilliseconds = 301714, ch_name = TheScariestMovieEver, ch_id = UCo8fiE2-s0SZu6Onb8lNLMQ }

                // <a href="/channel/UCo8fiE2-s0SZu6Onb8lNLMQ" class=" yt-uix-sessionlink     spf-link  g-hovercard" data-ytid="UCo8fiE2-s0SZu6Onb8lNLMQ" data-sessionlink="ei=Ihu5VObLE4_2ygP--4HABQ" data-name="">TheScariestMovieEver</a>

                // [95] = {[uid, o8fiE2-s0SZu6Onb8lNLMQ]}

                //               ElapsedMilliseconds = 0, p = r:/ media }
                //           ElapsedMilliseconds = 4219, px = r:/ media\'Battle of the Sexes' is Over!EVERYONE LOST!!.mp4 }
                //       before Create
                //ElapsedMilliseconds = 171092, px = r:/media\'Battle of the Sexes' is Over! EVERYONE LOST!!.mp4
                //   }
                //   after Create
                //ElapsedMilliseconds = 185783, px = r:/media\'Battle of the Sexes' is Over! EVERYONE LOST!!.mp4, videoUrl = http://youtube.com/watch?v=hsVdNrZZM2Q }
                //url = http://youtube.com/watch?v=hsVdNrZZM2Q }

                // https://www.youtube.com/channel/UCo8fiE2-s0SZu6Onb8lNLMQ





            }

        }
    }
}
