// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.beans;

namespace java.beans
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/beans/VetoableChangeListener.html
	[Script(IsNative = true)]
	public interface VetoableChangeListener
	{
		/// <summary>
		/// This method gets called when a constrained property is changed.
		/// </summary>
		void vetoableChange(PropertyChangeEvent @evt);

	}
}
