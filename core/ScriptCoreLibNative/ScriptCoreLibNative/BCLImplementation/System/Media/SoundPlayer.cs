using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLibNative.SystemHeaders;

namespace ScriptCoreLibNative.BCLImplementation.System.Media
{
	[Script(Implements = typeof(global::System.Media.SoundPlayer))]
	internal class __SoundPlayer
	{
		readonly Stream InternalStream;

		public __SoundPlayer(Stream stream)
		{
			InternalStream = stream;
		}

		public Stream Stream
		{
			get
			{
				return InternalStream;
			}
		}

		public static byte[] ToArray( Stream s)
		{
			var a = new byte[s.Length];

			s.Seek(0, SeekOrigin.Begin);
			s.Read(a, 0, (int)s.Length);

			return a;
		}

		public void PlaySync()
		{
			if (InternalStream == null)
			{
				Console.WriteLine("__SoundPlayer.PlaySync: InternalStream == null");
				return;
			}



			var pszSound = ToArray(InternalStream);
			var fdwSound = windows_h.SoundFlags.SND_MEMORY | windows_h.SoundFlags.SND_NODEFAULT;

			var r = windows_h.PlaySound(pszSound, 0, fdwSound);

			//if (r)
			//{
			//    Console.WriteLine("__SoundPlayer.PlaySync ok");
			//}
			//else
			//{
			//    Console.WriteLine("__SoundPlayer.PlaySync fail");
			//}
		}
	}

}
