// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.plaf.OptionPaneUI

using ScriptCoreLib;
using javax.swing;

namespace javax.swing.plaf
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/plaf/OptionPaneUI.html
	[Script(IsNative = true)]
	public abstract class OptionPaneUI : ComponentUI
	{
		/// <summary>
		/// 
		/// </summary>
		public OptionPaneUI()
		{
		}

		/// <summary>
		/// Returns true if the user has supplied instances of Component for
		/// either the options or message.
		/// </summary>
		abstract public bool containsCustomComponents(JOptionPane @op);

		/// <summary>
		/// Requests the component representing the default value to have
		/// focus.
		/// </summary>
		abstract public void selectInitialValue(JOptionPane @op);

	}
}
