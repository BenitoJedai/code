// defined by?
//    <ProjectReference Include="..\..\..\..\..\..\..\opensource\github\Newtonsoft.Json\Src\Newtonsoft.Json\Newtonsoft.Json.csproj">
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
// defined by?
// "X:\jsc.svn\market\synergy\github\YoutubeExtractor\external\YoutubeExtractor.sln"
using YoutubeExtractor;
using ScriptCoreLib.Extensions;
using System.Threading;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace TestYouTubeExtractor
{
	static class Extensions
	{
		//var page0 = new WebClient().DownloadString(src);

		public static string DownloadStringOrRetry(this WebClient c, string u)
		{
			while (true)
				try { return c.DownloadString(u); }
				catch (Exception err) { Console.WriteLine(new { err.Message }); Thread.Sleep(10000); }


		}

	}

	public class Program
	{
		// Show Details	Severity	Code	Description	Project	File	Line
		//Error NuGet Package restore failed for project TestYouTubeExtractor: Unable to find version '1.0.0.0' of package 'YoutubeExtractor'..			0

		// https://github.com/mono/taglib-sharp/


		[DllImport("mpr.dll", SetLastError = true, EntryPoint = "WNetRestoreSingleConnectionW", CharSet = CharSet.Unicode)]
		internal static extern int WNetRestoreSingleConnection(IntPtr windowHandle,
													 [MarshalAs(UnmanagedType.LPWStr)] string localDrive,
													 [MarshalAs(UnmanagedType.Bool)] bool useUI);


		//Error CS0246  The type or namespace name 'VideoInfo' could not be found(are you missing a using directive or an assembly reference?)	TestYouTubeExtractor X:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs	47

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

			// https://msdn.microsoft.com/en-us/library/windows/desktop/aa385480(v=vs.85).aspx
			// https://msdn.microsoft.com/en-us/library/windows/desktop/aa385485(v=vs.85).aspx
			// http://stackoverflow.com/questions/8629760/how-to-force-windows-to-reconnect-to-network-drive
			// https://msdn.microsoft.com/en-us/library/windows/desktop/aa385453(v=vs.85).aspx

			try
			{
				var ee = Directory.GetFileSystemEntries("r:\\");
			}
			catch
			{
				// \\192.168.43.12\x$

				//                [Window Title]
				//        Location is not available

				//        [Content]
				//R:\ is unavailable.If the location is on this PC, make sure the device or drive is connected or the disc is inserted, and then try again.If the location is on a network, make sure you’re connected to the network or Internet, and then try again.If the location still can’t be found, it might have been moved or deleted.

				//[OK]

				// ---------------------------
				//Error
				//-------------------------- -
				//This network connection does not exist.


				//-------------------------- -
				//OK
				//-------------------------- -

				IntPtr hWnd = new IntPtr(0);
				int res = WNetRestoreSingleConnection(hWnd, "r:", true);
			}

			// res = 86
			// res = 0

			//            ---------------------------
			//            Restoring Network Connections
			//---------------------------
			//An error occurred while reconnecting r:
			//                to
			//\\RED\x$
			//Microsoft Windows Network: The local device name is already in use.


			//This connection has not been restored.
			//---------------------------
			//OK
			//-------------------------- -


			if (!File.Exists(p))
			{
				if (!File.Exists(px))
				{
					/*
                     * Create the video downloader.
                     * The first argument is the video to download.
                     * The second argument is the path to save the video file.
                     */
					var videoDownloader = new VideoDownloader(video, px);

					// Register the ProgressChanged event and print the current progress
					videoDownloader.DownloadProgressChanged += (sender, args) =>
					{
						ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowProgress(0.01 * args.ProgressPercentage);



						Console.Title = "%" + args.ProgressPercentage.ToString("0.0");
					}
					;


					/*
                     * Execute the video downloader.
                     * For GUI applications note, that this method runs synchronously.
                     */
					videoDownloader.Execute();
				}

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

				//err = System.IO.IOException: Cannot create a file when that file already exists

				// map network drive via ip. as the aias can be forgotten by the network
				Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(px, p, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs);
				//err = { "Could not find a part of the path 'r:\\media'."}
				// System.IO.DirectoryNotFoundException: Could not find a part of the path 'r:\media'.
				// System.IO.IOException: The specified network name is no longer available.
				// Systxem.IO.IOException: The system cannot move the file to a different disk drive

				//Move... { p = r:/ media\KRYON 'Evolution Revealed' - Lee Carroll.mp4 }
				//{
				//    err = System.IO.DirectoryNotFoundException: Could not find a part of the path
				//'r:\media'.
				//   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
				//   at System.IO.Directory.InternalCreateDirectory(String fullPath, String path,
				//Object dirSecurityObj, Boolean checkHost)
				//   at System.IO.Directory.InternalCreateDirectoryHelper(String path, Boolean che
				//ckHost)
				//   at System.IO.Directory.CreateDirectory(String path)
				//   at Microsoft.VisualBasic.FileIO.FileSystem.CopyOrMoveFile(CopyOrMove operatio
				//n, String sourceFileName, String destinationFileName, Boolean overwrite, UIOptio
				//nInternal showUI, UICancelOption onUserCancel)
				//   at Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(String sourceFileName, St
				//ring destinationFileName, UIOption showUI)
			}
		}


		static void Main(string[] args)
		{
			// or what if debugger starts asking for developer license and clicking ok kills to downloads in progress?
			// what if device looses power.
			// how are we to know or resume?


			// X:\jsc.svn\examples\merge\Test\TestJObjectParse\TestJObjectParse\Program.cs

			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150115/youtubeextractor

			// X:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs
			// x:\jsc.svn\market\synergy\github\youtubeextractor\external\exampleapplication\program.cs

			//var p = 1;

			for (int p = 1; p < 96; p++)
				foreach (var src in new[] {
					//"http://consciousresonance.net/?page_id=1587&paged=\{p}"
					//"https://faustuscrow.wordpress.com/page/\{p}/",
					//"https://hiddenlighthouse.wordpress.com/page/\{p}/",
					$"https://zproxy.wordpress.com/page/{p}/"

				})
				{



					Console.WriteLine("DownloadString ... " + new { p, src });

					// Additional information: The underlying connection was closed: An unexpected error occurred on a send.
					// Additional information: The operation has timed out.
					// Additional information: The underlying connection was closed: The connection was closed unexpectedly.

					// Additional information: The request was aborted: Could not create SSL/TLS secure channel.
					// xml tidy?
					var page0 = new WebClient().DownloadStringOrRetry(src);

					Console.WriteLine("DownloadString ... done " + new { p });
					// http://stackoverflow.com/questions/281682/reference-to-undeclared-entity-exception-while-working-with-xml
					// Additional information: Reference to undeclared entity 'raquo'. Line 11, position 73.
					//  Additional information: The 'p' start tag on line 105 position 2 does not match the end tag of 'div'.Line 107, position 10.
					// http://stackoverflow.com/questions/15926142/regular-expression-for-finding-href-value-of-a-a-link

					// Command: Checkout from https://htmlagilitypack.svn.codeplex.com/svn/trunk, revision HEAD, Fully recursive, Externals included  


					//// could it be used within a service worker?
					//var doc = new HtmlAgilityPack.HtmlDocument();
					//doc.LoadHtml(page0);

					//var hrefList = doc.DocumentNode.SelectNodes("//a")
					//                  .Select(xp => xp.GetAttributeValue("href", "not found"))
					//                  .ToList();
					////var xpage0 = XElement.Parse(

					//    System.Net.WebUtility.HtmlDecode(page0)

					//    );

					// http://htmlagilitypack.codeplex.com/

					//Console.WriteLine("DownloadString ... done " + new { p, hrefList.Count });

					//p++;


					// https://www.youtube.com/embed/FhEYvOYceNs?

					while (!string.IsNullOrEmpty(page0))
					{
						// <iframe src="//www.youtube.com/embed/umfjGNlxWcw" 

						var prefix = "//www.youtube.com/embed/";
						//var prefix = "https://www.youtube.com/embed/";
						var embed = page0.SkipUntilOrEmpty(prefix);
						var id = embed.TakeUntilIfAny("\"").TakeUntilIfAny("?");
						var link = prefix + id;

						page0 = embed.SkipUntilOrEmpty("?");


						Console.WriteLine();

						try
						{

							// a running applicaion should know when it can reload itself
							// when all running tasks are complete and no new tasks are to be taken.

							var videoUrl = link;

							bool isYoutubeUrl = DownloadUrlResolver.TryNormalizeYoutubeUrl(videoUrl, out videoUrl);

							//Console.WriteLine(new { sw.ElapsedMilliseconds, px, videoUrl });



							// wont help
							//var y = DownloadUrlResolver.GetDownloadUrls(link);
							//var j = DownloadUrlResolver.LoadJson(videoUrl);
							var c = new WebClient().DownloadString(videoUrl);

							// "Kryon - Timing o..." The YouTube account associated with this video has been terminated due to multiple third-party notifications of copyright infringement.

							// <link itemprop="url" href="http://www.youtube.com/user/melania1172">

							//                    { videoUrl = http://youtube.com/watch?v=li0E4_7ap3g, ch_name = , userurl = https://youtube.com/user/ }
							//{ url = http://youtube.com/watch?v=li0E4_7ap3g }
							//{ err = YoutubeExtractor.YoutubeParseException: Could not parse the Youtube page for URL http://youtube.com/watch?v=li0E4_7ap3g

							// <h1 id="unavailable-message" class="message">

							//  'IS_UNAVAILABLE_PAGE': false,
							var unavailable =

								!c.Contains("'IS_UNAVAILABLE_PAGE': false") ?
								c.SkipUntilOrEmpty("<h1 id=\"unavailable-message\" class=\"message\">").TakeUntilOrEmpty("<").Trim() : "";
							if (unavailable != "")
							{
								Console.WriteLine(new { videoUrl, unavailable });
								Thread.Sleep(3000);
								continue;
							}

							var ch = c.SkipUntilOrEmpty(" <div class=\"yt-user-info\">").SkipUntilOrEmpty("<a href=\"/channel/");
							var ch_id = ch.TakeUntilOrEmpty("\"");
							var ch_name = ch.SkipUntilOrEmpty(">").TakeUntilOrEmpty("<");

							// https://www.youtube.com/channel/UCP-Q2vpvpQmdShz-ASBj2fA/videos


							// ! originally there were users, now there are thos gplus accounts?

							//var usertoken = c.SkipUntilOrEmpty("<link itemprop=\"url\" href=\"http://www.youtube.com/user/");
							//var userid = usertoken.TakeUntilOrEmpty("\"");
							////var ch_name = ch.SkipUntilOrEmpty(">").TakeUntilOrEmpty("<");

							//var userurl = "https://youtube.com/user/" + userid;

							Console.WriteLine(new { src, link, ch_name, ch_id });
							//Console.WriteLine(new { page0, link });

							// Our test youtube link
							//const string link = "https://www.youtube.com/watch?v=BJ9v4ckXyrU";
							//Debugger.Break();

							// rewrite broke JObject Parse.
							// Additional information: Bad JSON escape sequence: \5.Path 'args.afv_ad_tag_restricted_to_instream', line 1, position 3029.



							// jsc rewriter breaks it?
							IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);
							// Additional information: The remote name could not be resolved: 'youtube.com'

							//DownloadAudio(videoInfos);
							DownloadVideo(link, videoInfos);

							//{
							//    err = System.IO.IOException: Unable to read data from the transport connection: An established connection was aborted by the software in your host machine. --->System.Net.Sockets.SocketException: An established connection was aborted by the software in your host machine
							//    at System.Net.Sockets.Socket.Receive(Byte[] buffer, Int32 offset, Int32 size, SocketFlags socketFlags)
							//   at System.Net.Sockets.NetworkStream.Read(Byte[] buffer, Int32 offset, Int32 size)
							//   -- - End of inner exception stack trace-- -
							//    at System.Net.ConnectStream.Read(Byte[] buffer, Int32 offset, Int32 size)
							//   at YoutubeExtractor.VideoDownloader.Execute()
						}
						catch (Exception err)
						{
							ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowError();

							// https://discutils.codeplex.com/
							// Message = "Result cannot be called on a failed Match."
							Console.WriteLine(new { err });

							Thread.Sleep(3000);
							ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowNoProgress();

						}

						//goto next;

					}

				}

			Debugger.Break();

		}

		public static void DoVideo(string link)
		{
			try
			{

				// a running applicaion should know when it can reload itself
				// when all running tasks are complete and no new tasks are to be taken.

				var videoUrl = link;

				bool isYoutubeUrl = DownloadUrlResolver.TryNormalizeYoutubeUrl(videoUrl, out videoUrl);

				//Console.WriteLine(new { sw.ElapsedMilliseconds, px, videoUrl });



				// wont help
				//var y = DownloadUrlResolver.GetDownloadUrls(link);
				//var j = DownloadUrlResolver.LoadJson(videoUrl);
				var c = new WebClient().DownloadString(videoUrl);

				// "Kryon - Timing o..." The YouTube account associated with this video has been terminated due to multiple third-party notifications of copyright infringement.

				// <link itemprop="url" href="http://www.youtube.com/user/melania1172">

				//                    { videoUrl = http://youtube.com/watch?v=li0E4_7ap3g, ch_name = , userurl = https://youtube.com/user/ }
				//{ url = http://youtube.com/watch?v=li0E4_7ap3g }
				//{ err = YoutubeExtractor.YoutubeParseException: Could not parse the Youtube page for URL http://youtube.com/watch?v=li0E4_7ap3g

				// <h1 id="unavailable-message" class="message">

				//  'IS_UNAVAILABLE_PAGE': false,
				var unavailable =

					!c.Contains("'IS_UNAVAILABLE_PAGE': false") ?
					c.SkipUntilOrEmpty("<h1 id=\"unavailable-message\" class=\"message\">").TakeUntilOrEmpty("<").Trim() : "";
				if (unavailable != "")
				{
					Console.WriteLine(new { videoUrl, unavailable });
					Thread.Sleep(3000);
					return;
				}

				var ch = c.SkipUntilOrEmpty(" <div class=\"yt-user-info\">").SkipUntilOrEmpty("<a href=\"/channel/");
				var ch_id = ch.TakeUntilOrEmpty("\"");
				var ch_name = ch.SkipUntilOrEmpty(">").TakeUntilOrEmpty("<");

				// https://www.youtube.com/channel/UCP-Q2vpvpQmdShz-ASBj2fA/videos


				// ! originally there were users, now there are thos gplus accounts?

				//var usertoken = c.SkipUntilOrEmpty("<link itemprop=\"url\" href=\"http://www.youtube.com/user/");
				//var userid = usertoken.TakeUntilOrEmpty("\"");
				////var ch_name = ch.SkipUntilOrEmpty(">").TakeUntilOrEmpty("<");

				//var userurl = "https://youtube.com/user/" + userid;

				Console.WriteLine(new { link, ch_name, ch_id });
				//Console.WriteLine(new { page0, link });

				// Our test youtube link
				//const string link = "https://www.youtube.com/watch?v=BJ9v4ckXyrU";
				//Debugger.Break();

				// rewrite broke JObject Parse.
				// Additional information: Bad JSON escape sequence: \5.Path 'args.afv_ad_tag_restricted_to_instream', line 1, position 3029.



				// jsc rewriter breaks it?
				IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);
				// Additional information: The remote name could not be resolved: 'youtube.com'

				//DownloadAudio(videoInfos);
				DownloadVideo(link, videoInfos);

				//{
				//    err = System.IO.IOException: Unable to read data from the transport connection: An established connection was aborted by the software in your host machine. --->System.Net.Sockets.SocketException: An established connection was aborted by the software in your host machine
				//    at System.Net.Sockets.Socket.Receive(Byte[] buffer, Int32 offset, Int32 size, SocketFlags socketFlags)
				//   at System.Net.Sockets.NetworkStream.Read(Byte[] buffer, Int32 offset, Int32 size)
				//   -- - End of inner exception stack trace-- -
				//    at System.Net.ConnectStream.Read(Byte[] buffer, Int32 offset, Int32 size)
				//   at YoutubeExtractor.VideoDownloader.Execute()
			}
			catch (Exception err)
			{
				ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowError();

				// https://discutils.codeplex.com/
				// Message = "Result cannot be called on a failed Match."
				Console.WriteLine(new { err });

				Thread.Sleep(3000);
				ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowNoProgress();

			}
		}
	}
}
