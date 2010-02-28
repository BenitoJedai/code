using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace PromotionWebApplication1.Services
{
	public interface ISoundCloudTracksDownload
	{
		void SoundCloudTracksDownload(SoundCloudTrackFound yield);
	}

	public static class SoundCloudTracksDownloadExtensions
	{
		public static void SoundCloudTracksDownload(this ISoundCloudTracksDownload e, AtSoundCloudTrackFoundTuple yield)
		{
			e.SoundCloudTracksDownload(yield.ToSoundCloudTrackFound());
		}

		public static SoundCloudTrackFound ToSoundCloudTrackFound(this AtSoundCloudTrackFoundTuple y)
		{
			return (uid, streamUrl, trackName, waveformUrl) =>
			{
				// without brackets, using the inline lamda version the new statement is lost for some reason...
				// we could fix this via jsc.meta rewriter

				y(
					new SoundCloudTrackFoundTuple
					{
						uid = uid,
						streamUrl = streamUrl,
						trackName = trackName,
						waveformUrl = waveformUrl
					}
				);
			};
		}
	}

	public class SoundCloudTrackFoundTuple
	{
		public string uid;
		public string streamUrl;
		public string trackName;
		public string waveformUrl;


	}

	public delegate void AtSoundCloudTrackFoundTuple(SoundCloudTrackFoundTuple e);

	public delegate void SoundCloudTrackFound(
		string uid,
		string streamUrl,
		string trackName,
		string waveformUrl
	);

	public delegate string YieldValue(string key, string value);

	public class SoundCloudTracks
	{
		const string Source = "http://soundcloud.com/tracks";

		public void Download(SoundCloudTrackFound yield)
		{
			var c = new WebClient();

			var data = c.DownloadString(new Uri(Source));

			data.AtIndecies("'player mode medium  {",
				p =>
				{
					var json_stream = data.Substring(p.i + p.target.Length).TakeUntilIfAny("}'");

					var title = data.Substring(p.i + p.target.Length).SkipUntilIfAny("}'").SkipUntilIfAny("<h3>").SkipUntilIfAny(">").TakeUntilIfAny("<");


					#region get
					YieldValue get = (name, value) =>
					{
						json_stream.AtIndecies("\"" + name + "\":\"",
							s =>
							{
								value = json_stream.Substring(s.i + s.target.Length).TakeUntilIfAny("\"");
							}
						);

						return value;
					};
					#endregion

					var uid = get("uid", "");
					var streamUrl = get("streamUrl", "").TakeUntilIfAny("?");
					//var trackName = get("trackName", "");
					var waveformUrl = get("waveformUrl", "");

					yield(uid, streamUrl, title, waveformUrl);
				}
			);
		}
	}
}
