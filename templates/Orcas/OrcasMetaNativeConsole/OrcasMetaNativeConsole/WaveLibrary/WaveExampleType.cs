using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.IO;

namespace OrcasMetaNativeConsole.WaveLibrary
{
	/// <summary>
	/// Possible example waves to generate
	/// </summary>
	public enum WaveExampleType
	{
		ExampleSineWave = 0,
		ExampleSquareWave = 1,
		ExampleSawtoothWave = 2,
		ExampleTriangleWave = 3,
		ExampleWhiteNoise = 4
	}

	public static class WaveExampleTypeExtensions
	{
		public static SoundPlayer ToSoundPlayer(this WaveExampleType type, int frequency)
		{
			return type.ToSoundPlayer(frequency, 1);
		}

		public static SoundPlayer ToSoundPlayer(this WaveExampleType type, int frequency, double volume)
		{
			WaveGenerator wave = new WaveGenerator(
				type,
				frequency,
				volume
			);

			var p = new SoundPlayer(wave);

			return p;
		}
	}
}
