﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace WavePlayer.BCLImplementation.System.Media
{
	[Script(Implements = typeof(global::System.Media.SoundPlayer))]
	internal class __SoundPlayer
	{
		readonly Stream InternalStream;

		public __SoundPlayer(Stream stream)
		{
			InternalStream = stream;
		}

		public void PlaySync()
		{
			if (InternalStream == null)
			{
				Console.WriteLine("__SoundPlayer.PlaySync: InternalStream == null");
				return;
			}

			var length = (int)InternalStream.Length;

			Console.Write("wave length: ");
			Console.Write(length);
			Console.WriteLine();

			var pszSound = new byte[0];
			var fdwSound = windows_h.SoundFlags.SND_MEMORY | windows_h.SoundFlags.SND_NODEFAULT;

			var r = windows_h.PlaySound(pszSound, 0, fdwSound);

			if (r)
			{
				Console.WriteLine("__SoundPlayer.PlaySync ok");
			}
			else
			{
				Console.WriteLine("__SoundPlayer.PlaySync fail");
			}
		}
	}
}
