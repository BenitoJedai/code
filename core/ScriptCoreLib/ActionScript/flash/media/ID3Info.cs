using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.media
{
	// http://livedocs.adobe.com/flex/3/langref/flash/media/ID3Info.html
	[Script(IsNative = true)]
	public class ID3Info
	{
		#region Fields
		/// <summary>
		/// The name of the album; corresponds to the ID3 2.0 tag TALB.
		/// </summary>
		public string album;

		/// <summary>
		/// The name of the artist; corresponds to the ID3 2.0 tag TPE1.
		/// </summary>
		public string artist;

		/// <summary>
		/// A comment about the recording; corresponds to the ID3 2.0 tag COMM.
		/// </summary>
		public string comment;

		/// <summary>
		/// The genre of the song; corresponds to the ID3 2.0 tag TCON.
		/// </summary>
		public string genre;

		/// <summary>
		/// The name of the song; corresponds to the ID3 2.0 tag TIT2.
		/// </summary>
		public string songName;

		/// <summary>
		/// The track number; corresponds to the ID3 2.0 tag TRCK.
		/// </summary>
		public string track;

		/// <summary>
		/// The year of the recording; corresponds to the ID3 2.0 tag TYER.
		/// </summary>
		public string year;

		#endregion

	}
}
