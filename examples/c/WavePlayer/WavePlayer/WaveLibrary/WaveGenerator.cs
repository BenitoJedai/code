using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib;

namespace WavePlayer.WaveLibrary
{
	/// <summary>
	/// Wraps a WAV file struture and auto-generates some canned waveforms.
	/// </summary>
	[Script]
	public class WaveGenerator
	{
		// Header, Format, Data chunks
		WaveHeader header;
		WaveFormatChunk format;
		WaveDataChunk data;
		public Stream stream;

		const double MAX_AMPLITUDE_16BIT = 32760;

		/// <summary>
		/// Initializes the object and generates a wave.
		/// </summary>
		/// <param name="type">The type of wave to generate</param>
		public WaveGenerator(WaveExampleType type, int frequency, double volume)
		{
			// Init chunks
			header = new WaveHeader();
			format = new WaveFormatChunk();
			data = new WaveDataChunk();

			// Number of samples = sample rate * channels * bytes per sample
			uint numSamples = format.dwSamplesPerSec * format.wChannels;

			// Initialize the 16-bit array
			data.shortArray = new short[numSamples];
			data.shortArrayLength = numSamples;

			// Calculate data chunk size in bytes and store it in the data chunk
			data.dwChunkSize = (uint)(numSamples * (format.wBitsPerSample / 8));

			// Max amplitude for 16-bit audio
			int amplitude = (int)(MAX_AMPLITUDE_16BIT * volume);

			// Create a double version of the frequency for easier math
			double freq = (double)frequency;

			// The "angle" used in the function, adjusted for the number of channels and sample rate.
			// This value is like the period of the wave.
			double t = (Math.PI * 2 * freq) / (format.dwSamplesPerSec * format.wChannels);



			// Fill the data array with sample data

			if (type == WaveExampleType.ExampleSineWave)
			{
				ExampleSineWave(numSamples, amplitude, t);
				return;
			}

			if (type == WaveExampleType.ExampleSquareWave)
			{
				ExampleSquareWave(numSamples, amplitude, t);
				return;
			}

			if (type == WaveExampleType.ExampleWhiteNoise)
			{
				ExampleWhiteNoise(numSamples, amplitude);
				return;
			}

			if (type == WaveExampleType.ExampleSawtoothWave)
			{
				ExampleSawtoothWave(frequency, numSamples, amplitude);
				return;
			}


			if (type == WaveExampleType.ExampleTriangleWave)
			{
				ExampleTriangleWave(frequency, numSamples, amplitude);

				return;
			}




		}

		private void ExampleSawtoothWave(int frequency, uint numSamples, int amplitude)
		{
			//Used to generate some of the linear waveforms
			int samplesPerWavelength = 0;
			short ampStep = 0;
			short tempSample = 0;
			// Determine the number of samples per wavelength

			var samplesPerWavelength_long = format.dwSamplesPerSec / (frequency / format.wChannels);

			samplesPerWavelength = (int)samplesPerWavelength_long;

			// Determine the amplitude step for consecutive samples
			ampStep = (short)((amplitude * 2) / samplesPerWavelength);

			// Temporary sample value, added to as we go through the loop
			tempSample = (short)-amplitude;

			// Total number of samples written so we know when to stop
			int totalSamplesWritten = 0;

			while (totalSamplesWritten < numSamples)
			{
				tempSample = (short)-amplitude;

				for (uint i = 0; i < samplesPerWavelength && totalSamplesWritten < numSamples; i++)
				{
					for (int channel = 0; channel < format.wChannels; channel++)
					{
						tempSample += ampStep;
						data.shortArray[totalSamplesWritten] = tempSample;

						totalSamplesWritten++;
					}
				}
			}
		}

		private void ExampleTriangleWave(int frequency, uint numSamples, int amplitude)
		{
			int samplesPerWavelength = 0;
			short ampStep = 0;
			short tempSample = 0;

			// Determine the number of samples per wavelength
			var samplesPerWavelength_long = format.dwSamplesPerSec / (frequency / format.wChannels);

			samplesPerWavelength = (int)samplesPerWavelength_long;

			// Determine the amplitude step for consecutive samples                  
			ampStep = (short)((amplitude * 2) / samplesPerWavelength);

			// Temporary sample value, added to as we go through the loop
			tempSample = (short)-amplitude;

			for (int i = 0; i < numSamples - 1; i++)
			{
				for (int channel = 0; channel < format.wChannels; channel++)
				{
					// Negate ampstep whenever it hits the amplitude boundary
					if (Math.Abs(tempSample) > amplitude)
						ampStep = (short)-ampStep;

					tempSample += ampStep;
					data.shortArray[i + channel] = tempSample;
				}
			}
		}

		private void ExampleSineWave(uint numSamples, int amplitude, double t)
		{
			for (int i = 0; i < numSamples - 1; i++)
			{
				// Fill with a simple sine wave at max amplitude
				for (int channel = 0; channel < format.wChannels; channel++)
				{
					data.shortArray[i + channel] = (short)(amplitude * Math.Sin(t * i));
				}
			}
		}

		private void ExampleSquareWave(uint numSamples, int amplitude, double t)
		{
			for (int i = 0; i < numSamples - 1; i++)
			{
				for (int channel = 0; channel < format.wChannels; channel++)
				{
					data.shortArray[i] = (short)(amplitude * Math.Sign(Math.Sin(t * i)));
				}
			}
		}

		private void ExampleWhiteNoise(uint numSamples, int amplitude)
		{
			// White noise is just a bunch of random samples.
			Random rnd = new Random();
			short randomValue = 0;

			// No need for a nested loop since it's all random anyway
			for (int i = 0; i < numSamples; i++)
			{
				randomValue = (short)(rnd.Next(-amplitude, amplitude));
				data.shortArray[i] = randomValue;
			}
		}

		public static implicit operator MemoryStream(WaveGenerator w)
		{
			//Console.WriteLine("WaveGenerator.MemoryStream start");


			var header = w.header;
			var format = w.format;
			var data = w.data;

			// Create a file (it always overwrites)
			//FileStream fileStream = new FileStream(filePath, FileMode.Create);
			//StreamWriter w = new StreamWriter(stream);

			// Use BinaryWriter to write the bytes to the file
			var stream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(stream);//fileStream);


		

			//Console.WriteLine("WaveGenerator.MemoryStream Write the header");

			// Write the header
			writer.Write(header.sGroupID.ToCharArray());
			writer.Write(header.dwFileLength);
			writer.Write(header.sRiffType.ToCharArray());

			//Console.WriteLine("WaveGenerator.MemoryStream Write the format chunk");

			// Write the format chunk
			writer.Write(format.sChunkID.ToCharArray());
			writer.Write(format.dwChunkSize);
			writer.Write(format.wFormatTag);
			writer.Write(format.wChannels);
			writer.Write(format.dwSamplesPerSec);
			writer.Write(format.dwAvgBytesPerSec);
			writer.Write(format.wBlockAlign);
			writer.Write(format.wBitsPerSample);

			//Console.WriteLine("WaveGenerator.MemoryStream Write the data chunk");

			// Write the data chunk
			writer.Write(data.sChunkID.ToCharArray());
			writer.Write(data.dwChunkSize);

			//Console.WriteLine("WaveGenerator.MemoryStream dataPoint");

			// foreach (short dataPoint in data.shortArray)
			for (int i = 0; i < data.shortArrayLength; i++)
			{

		

				var dataPoint = data.shortArray[i];
				writer.Write(dataPoint);
			}

			//Console.WriteLine("WaveGenerator.MemoryStream seek");

			writer.Seek(4, SeekOrigin.Begin);
			//Console.WriteLine("WaveGenerator.MemoryStream BaseStream.Length");
			uint filesize = (uint)writer.BaseStream.Length;
			//Console.WriteLine("WaveGenerator.MemoryStream filesize");
			writer.Write(filesize - 8);

			//Console.WriteLine("WaveGenerator.MemoryStream Set the position to the beginning of the stream.");

			// Set the position to the beginning of the stream.
			stream.Seek(0, SeekOrigin.Begin);

			//Console.WriteLine("WaveGenerator.MemoryStream end");

			return stream;
		}
	}

}
