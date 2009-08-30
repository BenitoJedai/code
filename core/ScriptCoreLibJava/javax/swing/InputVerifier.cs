// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using javax.swing;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/InputVerifier.html
	[Script(IsNative = true)]
	public abstract class InputVerifier
	{
		/// <summary>
		/// 
		/// </summary>
		public InputVerifier()
		{
		}

		/// <summary>
		/// Calls <code>verify(input)</code> to ensure that the input is valid.
		/// </summary>
		public bool shouldYieldFocus(JComponent @input)
		{
			return default(bool);
		}

		/// <summary>
		/// Checks whether the JComponent's input is valid.
		/// </summary>
		abstract public bool verify(JComponent @input);

	}
}

