using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Media;
using System.IO;

namespace WavePlayer.WaveLibrary
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

	[Script]
	public static class WaveExampleTypeExtensions
	{
		public static void PlaySound(this WaveExampleType type, int frequency)
		{
			type.PlaySound(frequency, 1);
		}

		public static void PlaySound(this WaveExampleType type, int frequency, double volume)
		{
			WaveGenerator wave = new WaveGenerator(
				type,
				frequency,
				volume
			);
			
			var p = new SoundPlayer(wave);

			p.PlaySync();
		}
	}
}
