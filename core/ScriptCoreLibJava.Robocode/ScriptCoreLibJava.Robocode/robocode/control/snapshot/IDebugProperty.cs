// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;

namespace robocode.control.snapshot
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/snapshot/IDebugProperty.html
	[Script(IsNative = true)]
	public interface IDebugProperty
	{
		/// <summary>
		/// Returns the key of the property.
		/// </summary>
		string getKey();

		/// <summary>
		/// Returns the value of the property.
		/// </summary>
		string getValue();

	}
}
