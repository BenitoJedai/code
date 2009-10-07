﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace WavePlayer.WaveLibrary
{
	/// <summary>
	/// Wraps the Data chunk of a wave file.
	/// </summary>
	[Script]
	public class WaveDataChunk
	{
		public string sChunkID;     // "data"
		public uint dwChunkSize;    // Length of header in bytes
		public short[] shortArray;  // 8-bit audio

		/// <summary>
		/// Initializes a new data chunk with default values.
		/// </summary>
		public WaveDataChunk()
		{
			shortArray = new short[0];
			dwChunkSize = 0;
			sChunkID = "data";
		}
	}
}
