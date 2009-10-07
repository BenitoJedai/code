﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace WavePlayer.WaveLibrary
{

	/// <summary>
	/// Wraps the Format chunk of a wave file.
	/// </summary>
	[Script]
	public class WaveFormatChunk
	{
		public string sChunkID;         // Four bytes: "fmt "
		public uint dwChunkSize;        // Length of header in bytes
		public ushort wFormatTag;       // 1 (MS PCM)
		public ushort wChannels;        // Number of channels
		public uint dwSamplesPerSec;    // Frequency of the audio in Hz... 44100
		public uint dwAvgBytesPerSec;   // for estimating RAM allocation
		public ushort wBlockAlign;      // sample frame size, in bytes
		public ushort wBitsPerSample;    // bits per sample

		/// <summary>
		/// Initializes a format chunk with the following properties:
		/// Sample rate: 44100 Hz
		/// Channels: Stereo
		/// Bit depth: 16-bit
		/// </summary>
		public WaveFormatChunk()
		{
			sChunkID = "fmt ";
			dwChunkSize = 16;
			wFormatTag = 1;
			wChannels = 2;
			dwSamplesPerSec = 44100;
			wBitsPerSample = 16;
			wBlockAlign = (ushort)(wChannels * (wBitsPerSample / 8));
			dwAvgBytesPerSec = dwSamplesPerSec * wBlockAlign;
		}
	}
}
