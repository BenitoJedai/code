﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WavePlayer2Guitar1.WaveLibrary
{
	/// <summary>
	/// Wraps the header portion of a WAVE file.
	/// </summary>
	public class WaveHeader
	{
		public string sGroupID; // RIFF
		public uint dwFileLength; // total file length minus 8, which is taken up by RIFF
		public string sRiffType; // always WAVE

		/// <summary>
		/// Initializes a WaveHeader object with the default values.
		/// </summary>
		public WaveHeader()
		{
			dwFileLength = 0;
			sGroupID = "RIFF";
			sRiffType = "WAVE";
		}
	}
}
