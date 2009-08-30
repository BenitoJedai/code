// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.datatransfer;
using java.lang;
using java.util;

namespace java.awt.datatransfer
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/datatransfer/FlavorMap.html
	[Script(IsNative = true)]
	public interface FlavorMap
	{
		/// <summary>
		/// Returns a <code>Map</code> of the specified <code>String</code> natives
		/// to their corresponding <code>DataFlavor</code>.
		/// </summary>
		Map getFlavorsForNatives(string[] @natives);

		/// <summary>
		/// Returns a <code>Map</code> of the specified <code>DataFlavor</code>s to
		/// their corresponding <code>String</code> native.
		/// </summary>
		Map getNativesForFlavors(DataFlavor[] @flavors);

	}
}

