// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.sound.sampled.Mixer

using ScriptCoreLib;

namespace javax.sound.sampled
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/sound/sampled/Mixer.html
	[Script(IsNative = true)]
	public interface Mixer : Line
	{
		///// <summary>
		///// Obtains a line that is available for use and that matches the description
		///// in the specified <code>Line.Info</code> object.
		///// </summary>
		//Line getLine(Line.Info @info);

		///// <summary>
		///// Obtains the maximum number of lines of the requested type that can be open simultaneously
		///// on the mixer.
		///// </summary>
		//int getMaxLines(Line.Info @info);

		/// <summary>
		/// Obtains information about this mixer, including the product's name,
		/// version, vendor, etc.
		/// </summary>
		//Mixer.Info getMixerInfo();

		/// <summary>
		/// Obtains information about the set of source lines supported
		/// by this mixer.
		/// </summary>
		//Info[] getSourceLineInfo();

		/// <summary>
		/// Obtains information about source lines of a particular type supported
		/// by the mixer.
		/// </summary>
		//Info[] getSourceLineInfo(Line.Info @info);

		/// <summary>
		/// Obtains the set of all source lines currently open to this mixer.
		/// </summary>
		Line[] getSourceLines();

		/// <summary>
		/// Obtains information about the set of target lines supported
		/// by this mixer.
		/// </summary>
		//Info[] getTargetLineInfo();

		/// <summary>
		/// Obtains information about target lines of a particular type supported
		/// by the mixer.
		/// </summary>
		//Info[] getTargetLineInfo(Line.Info @info);

		/// <summary>
		/// Obtains the set of all target lines currently open from this mixer.
		/// </summary>
		Line[] getTargetLines();

		/// <summary>
		/// Indicates whether the mixer supports a line (or lines) that match
		/// the specified <code>Line.Info</code> object.
		/// </summary>
		//bool isLineSupported(Line.Info @info);

		/// <summary>
		/// Reports whether this mixer supports synchronization of the specified set of lines.
		/// </summary>
		bool isSynchronizationSupported(Line[] @lines, bool @maintainSync);

		/// <summary>
		/// Synchronizes two or more lines.
		/// </summary>
		void synchronize(Line[] @lines, bool @maintainSync);

		/// <summary>
		/// Releases synchronization for the specified lines.
		/// </summary>
		void unsynchronize(Line[] @lines);

	}
}
