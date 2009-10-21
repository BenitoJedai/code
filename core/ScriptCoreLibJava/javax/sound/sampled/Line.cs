// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.sound.sampled.Line

using ScriptCoreLib;

namespace javax.sound.sampled
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/sound/sampled/Line.html
	[Script(IsNative = true)]
	public interface Line
	{
		/// <summary>
		/// Adds a listener to this line.
		/// </summary>
		void addLineListener(LineListener @listener);

		/// <summary>
		/// Closes the line, indicating that any system resources
		/// in use by the line can be released.
		/// </summary>
		void close();

		///// <summary>
		///// Obtains a control of the specified type,
		///// if there is any.
		///// </summary>
		//Control getControl(Control.Type @control);

		/// <summary>
		/// Obtains the set of controls associated with this line.
		/// </summary>
		Control[] getControls();

		/// <summary>
		/// Obtains the <code>Line.Info</code> object describing this
		/// line.
		/// </summary>
		//Line.Info getLineInfo();

		/// <summary>
		/// Indicates whether the line supports a control of the specified type.
		/// </summary>
		//bool isControlSupported(Control.Type @control);

		/// <summary>
		/// Indicates whether the line is open, meaning that it has reserved
		/// system resources and is operational, although it might not currently be
		/// playing or capturing sound.
		/// </summary>
		bool isOpen();

		/// <summary>
		/// Opens the line, indicating that it should acquire any required
		/// system resources and become operational.
		/// </summary>
		void open();

		/// <summary>
		/// Removes the specified listener from this line's list of listeners.
		/// </summary>
		void removeLineListener(LineListener @listener);

	}
}
