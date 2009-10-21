// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.sound.sampled.AudioSystem

using ScriptCoreLib;
using java.io;
using java.net;

namespace javax.sound.sampled
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/sound/sampled/AudioSystem.html
	[Script(IsNative = true)]
	public class AudioSystem
	{
		/// <summary>
		/// Obtains the audio file format of the specified <code>File</code>.
		/// </summary>
		static public AudioFileFormat getAudioFileFormat(File @file)
		{
			return default(AudioFileFormat);
		}

		/// <summary>
		/// Obtains the audio file format of the provided input stream.
		/// </summary>
		static public AudioFileFormat getAudioFileFormat(InputStream @stream)
		{
			return default(AudioFileFormat);
		}

		/// <summary>
		/// Obtains the audio file format of the specified URL.
		/// </summary>
		static public AudioFileFormat getAudioFileFormat(URL @url)
		{
			return default(AudioFileFormat);
		}

		/// <summary>
		/// Obtains the file types for which file writing support is provided by the system.
		/// </summary>
		//static public Type[] getAudioFileTypes()
		//{
		//    return default(Type[]);
		//}

		/// <summary>
		/// Obtains the file types that the system can write from the
		/// audio input stream specified.
		/// </summary>
		//static public Type[] getAudioFileTypes(AudioInputStream @stream)
		//{
		//    return default(Type[]);
		//}

		/// <summary>
		/// Obtains an audio input stream of the indicated encoding, by converting the
		/// provided audio input stream.
		/// </summary>
		//static public AudioInputStream getAudioInputStream(AudioFormat.Encoding @targetEncoding, AudioInputStream @sourceStream)
		//{
		//    return default(AudioInputStream);
		//}

		/// <summary>
		/// Obtains an audio input stream of the indicated format, by converting the
		/// provided audio input stream.
		/// </summary>
		static public AudioInputStream getAudioInputStream(AudioFormat @targetFormat, AudioInputStream @sourceStream)
		{
			return default(AudioInputStream);
		}

		/// <summary>
		/// Obtains an audio input stream from the provided <code>File</code>.
		/// </summary>
		static public AudioInputStream getAudioInputStream(File @file)
		{
			return default(AudioInputStream);
		}

		/// <summary>
		/// Obtains an audio input stream from the provided input stream.
		/// </summary>
		static public AudioInputStream getAudioInputStream(InputStream @stream)
		{
			return default(AudioInputStream);
		}

		/// <summary>
		/// Obtains an audio input stream from the URL provided.
		/// </summary>
		static public AudioInputStream getAudioInputStream(URL @url)
		{
			return default(AudioInputStream);
		}

		/// <summary>
		/// Obtains a line that matches the description in the specified
		/// <code>Line.Info</code> object.
		/// </summary>
		//static public Line getLine(Line.Info @info)
		//{
		//    return default(Line);
		//}

		/// <summary>
		/// Obtains the requested audio mixer.
		/// </summary>
		//static public Mixer getMixer(Mixer.Info @info)
		//{
		//    return default(Mixer);
		//}

		/// <summary>
		/// Obtains an array of mixer info objects that represents
		/// the set of audio mixers that are currently installed on the system.
		/// </summary>
		//static public Info[] getMixerInfo()
		//{
		//    return default(Info[]);
		//}

		/// <summary>
		/// Obtains information about all source lines of a particular type that are supported
		/// by the installed mixers.
		/// </summary>
		//static public Info[] getSourceLineInfo(Line.Info @info)
		//{
		//    return default(Info[]);
		//}

		/// <summary>
		/// Obtains the encodings that the system can obtain from an
		/// audio input stream with the specified encoding using the set
		/// of installed format converters.
		/// </summary>
		//static public Encoding[] getTargetEncodings(AudioFormat.Encoding @sourceEncoding)
		//{
		//    return default(Encoding[]);
		//}

		/// <summary>
		/// Obtains the encodings that the system can obtain from an
		/// audio input stream with the specified format using the set
		/// of installed format converters.
		/// </summary>
		//static public Encoding[] getTargetEncodings(AudioFormat @sourceFormat)
		//{
		//    return default(Encoding[]);
		//}

		/// <summary>
		/// Obtains the formats that have a particular encoding and that the system can
		/// obtain from a stream of the specified format using the set of
		/// installed format converters.
		/// </summary>
		//static public AudioFormat[] getTargetFormats(AudioFormat.Encoding @targetEncoding, AudioFormat @sourceFormat)
		//{
		//    return default(AudioFormat[]);
		//}

		/// <summary>
		/// Obtains information about all target lines of a particular type that are supported
		/// by the installed mixers.
		/// </summary>
		//static public Info[] getTargetLineInfo(Line.Info @info)
		//{
		//    return default(Info[]);
		//}

		/// <summary>
		/// Indicates whether an audio input stream of the specified encoding
		/// can be obtained from an audio input stream that has the specified
		/// format.
		/// </summary>
		//static public bool isConversionSupported(AudioFormat.Encoding @targetEncoding, AudioFormat @sourceFormat)
		//{
		//    return default(bool);
		//}

		/// <summary>
		/// Indicates whether an audio input stream of a specified format
		/// can be obtained from an audio input stream of another specified format.
		/// </summary>
		static public bool isConversionSupported(AudioFormat @targetFormat, AudioFormat @sourceFormat)
		{
			return default(bool);
		}

		/// <summary>
		/// Indicates whether file writing support for the specified file type is provided
		/// by the system.
		/// </summary>
		//static public bool isFileTypeSupported(AudioFileFormat.Type @fileType)
		//{
		//    return default(bool);
		//}

		/// <summary>
		/// Indicates whether an audio file of the specified file type can be written
		/// from the indicated audio input stream.
		/// </summary>
		//static public bool isFileTypeSupported(AudioFileFormat.Type @fileType, AudioInputStream @stream)
		//{
		//    return default(bool);
		//}

		/// <summary>
		/// Indicates whether the system supports any lines that match
		/// the specified <code>Line.Info</code> object.
		/// </summary>
		//static public bool isLineSupported(Line.Info @info)
		//{
		//    return default(bool);
		//}

		/// <summary>
		/// Writes a stream of bytes representing an audio file of the specified file type
		/// to the external file provided.
		/// </summary>
		//static public int write(AudioInputStream @stream, AudioFileFormat.Type @fileType, File @out)
		//{
		//    return default(int);
		//}

		/// <summary>
		/// Writes a stream of bytes representing an audio file of the specified file type
		/// to the output stream provided.
		/// </summary>
		//static public int write(AudioInputStream @stream, AudioFileFormat.Type @fileType, OutputStream @out)
		//{
		//    return default(int);
		//}

	}
}
