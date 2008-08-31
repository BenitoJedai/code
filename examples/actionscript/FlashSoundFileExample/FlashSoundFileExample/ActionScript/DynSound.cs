using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashSoundFileExample.ActionScript
{
	/**
	 * Creates a Flash9 Sound object using a ByteArray with the sound wave.
	 */
	[Script]
	public static class DynSound
	{
		// port from http://gimme.badsectoracula.com/dynsound/DynSound.hx

		private static void writeTagInfo(this BinaryWriter swf, int code, int len)
		{
			if (len >= 63)
			{
				swf.Write((short)((code << 6) | 0x3F));
				swf.Write(len);
			}
			else swf.Write((short)((code << 6) | len));
		}

		public static void playSound(this Stream wave)
		{
			var swf = new BinaryWriter(new MemoryStream());

			// generate the file
			// swf.endian = flash.utils.Endian.LITTLE_ENDIAN;

			// SWF header
			swf.Write((byte)0x46);    // 'FWS' signature
			swf.Write((byte)0x57);
			swf.Write((byte)0x53);
			swf.Write((byte)0x07);    // version
			swf.Write((uint)0);// filesize (will be set later)
			swf.Write((byte)0x78);    // area size
			swf.Write((byte)0x00);
			swf.Write((byte)0x05);
			swf.Write((byte)0x5F);
			swf.Write((byte)0x00);
			swf.Write((byte)0x00);
			swf.Write((byte)0x0F);
			swf.Write((byte)0xA0);
			swf.Write((byte)0x00);
			swf.Write((byte)0x00);   // framerate (12fps)
			swf.Write((byte)0x0C);
			swf.Write((short)1);      // one frame

			// DefineSound tag
			swf.writeTagInfo(14, (int)(2 + 1 + 4 + wave.Length));
			swf.Write((short)1);      // sound (character) ID
			swf.Write((byte)0x3C);    // sound format (uncompressed) = 4 bits (3)
			// 44100 rate = 2 bits (3)
			// 8bit samples = 1 bit (0)
			// mono sound = 1 bit (0)
			// 00111100 = 0x3C
			swf.Write((uint)wave.Length); // sample count (one byte=one sample)

			wave.Position = 0;

			swf.Write(new BinaryReader(wave).ReadBytes((int)wave.Length));


			// StartSound tag
			swf.writeTagInfo(15, 2 + 1);
			swf.Write((short)1);      // character id of the sound
			swf.Write((byte)0);       // SOUNDINFO flags (all 0)

			// End tag
			swf.writeTagInfo(0, 0);

			// Set size
			swf.BaseStream.Position = 4;
			swf.Write((uint)swf.BaseStream.Length);
			swf.BaseStream.Position = 0;

			// "load" it
			var ldr = new Loader();
			var u = swf.BaseStream.ToByteArray();

			u.position = 0;

			ldr.loadBytes(u);

		}
	}


}
