using Newtonsoft.Json.Linq;
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

namespace TestYouTubeExtractor
{
    class Program
    {
        private static void DownloadVideo(string link, IEnumerable<VideoInfo> videoInfos)
        {
            // Show Details	Severity	Code	Description	Project	File	Line
            //Error CS0246  The type or namespace name 'ICSharpCode' could not be found(are you missing a using directive or an assembly reference?)	taglib-sharp File.cs 878


            // Show Details	Severity	Code	Description	Project	File	Line
            //Error Error opening icon file X:\opensource\github\taglib - sharp\src-- Access to the path 'X:\opensource\github\taglib-sharp\src' is denied.taglib - sharp    CSC


            var mp4 = videoInfos.Where(x => x.VideoType == VideoType.Mp4);
            var mp4video = mp4.Where(x => x.Resolution > 0);
            var mp4audio = mp4video.Where(x => x.AudioBitrate > 0).ToArray();

            /*
             * Select the first .mp4 video with 360p resolution
             */
            //VideoInfo video = mp4audio.OrderByDescending(info => info.Resolution).First();
            // 300MB?
            VideoInfo video = mp4audio.OrderBy(info => info.Resolution).First();


            video.DecryptDownloadUrl();

            var Title =
                  video.Title
                //.Replace("/", " ")
                //.Replace("\\", " ")
                .Replace("\"", "'")
                //.Replace(":", " ")
                .Replace("&", " and ")
                //.Replace("*", " ")
                ;

            // http://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars(v=vs.110).aspx

            foreach (var item in
                Path.GetInvalidFileNameChars())
            {
                Title = Title.Replace(item, ' ');

            }

            var px = Path.Combine(
               //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
               "x:/media",

             Title + video.VideoExtension);

            var p = Path.Combine(
                //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "r:/media",

               Title + video.VideoExtension);

            Console.WriteLine(px);

            if (!File.Exists(p))
            {
                /*
                 * Create the video downloader.
                 * The first argument is the video to download.
                 * The second argument is the path to save the video file.
                 */
                var videoDownloader = new VideoDownloader(video, px);

                // Register the ProgressChanged event and print the current progress
                videoDownloader.DownloadProgressChanged += (sender, args) => Console.Title = "%" + args.ProgressPercentage.ToString("0.00");

                /*
                 * Execute the video downloader.
                 * For GUI applications note, that this method runs synchronously.
                 */
                videoDownloader.Execute();


                // Additional information: The process cannot access the file 'C:\Users\Arvo\Documents\Dido - Don't Believe In Love.mp4' because it is being used by another process.

                // http://stackoverflow.com/questions/18250281/reading-writing-metadata-of-audio-video-files

                Console.WriteLine("TagLib... " + new { new FileInfo(px).Length });

                TagLib.File videoFile = TagLib.File.Create(px);
                //TagLib.Mpeg4.AppleTag customTag = (TagLib.Mpeg4.Comm)videoFile.GetTag(TagLib.TagTypes.Apple);
                TagLib.Mpeg4.AppleTag customTag = (TagLib.Mpeg4.AppleTag)videoFile.GetTag(TagLib.TagTypes.Apple);
                //customTag.SetDashBox("Producer", "Producer1",link);
                //customTag.Comment = link;
                customTag.Album = link;
                videoFile.Save();
                videoFile.Dispose();

                // http://stackoverflow.com/questions/13847669/file-move-progress-bar

                Console.WriteLine("Move... " + new { p });

                //File.Move(px, p);
                Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(px, p, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs);

            }
        }


        static void Main(string[] args)
        {
            // X:\jsc.svn\examples\merge\Test\TestJObjectParse\TestJObjectParse\Program.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150115/youtubeextractor

            // X:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs
            // x:\jsc.svn\market\synergy\github\youtubeextractor\external\exampleapplication\program.cs

            var p = 2;

            for (int ioffset = 0; ioffset < 3; ioffset++)
            {

                Console.WriteLine(new { p });

                var page0 = new WebClient().DownloadString(
                    "https://zproxy.wordpress.com/page/\{p}/"
                    );

                // https://www.youtube.com/embed/FhEYvOYceNs?

                while (!string.IsNullOrEmpty(page0))
                {

                    var prefix = "https://www.youtube.com/embed/";
                    var embed = page0.SkipUntilOrEmpty(prefix);
                    var id = embed.TakeUntilOrEmpty("?");
                    var link = prefix + id;

                    Console.WriteLine();

                    // a running applicaion should know when it can reload itself
                    // when all running tasks are complete and no new tasks are to be taken.
                    Console.WriteLine(new { page0, link });

                    // Our test youtube link
                    //const string link = "https://www.youtube.com/watch?v=BJ9v4ckXyrU";
                    //Debugger.Break();

                    // rewrite broke JObject Parse.
                    // Additional information: Bad JSON escape sequence: \5.Path 'args.afv_ad_tag_restricted_to_instream', line 1, position 3029.

                    try
                    {

                        // jsc rewriter breaks it?
                        IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);
                        // Additional information: The remote name could not be resolved: 'youtube.com'

                        //DownloadAudio(videoInfos);
                        DownloadVideo(link, videoInfos);
                    }
                    catch (Exception err)
                    {
                        // https://discutils.codeplex.com/
                        // Message = "Result cannot be called on a failed Match."
                        Console.WriteLine(new { err });

                    }

                    page0 = embed.SkipUntilOrEmpty("?");
                    //goto next;

                }

                p++;
            }

            Debugger.Break();

        }
    }
}
