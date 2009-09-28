// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.ComponentInputMap

using ScriptCoreLib;
using java.lang;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/ComponentInputMap.html
	[Script(IsNative = true)]
	public class ComponentInputMap : InputMap
	{
		/// <summary>
		/// Creates a <code>ComponentInputMap</code> associated with the
		/// specified component.
		/// </summary>
		public ComponentInputMap(JComponent @component)
		{
		}

		/// <summary>
		/// Removes all the mappings from this object.
		/// </summary>
		public void clear()
		{
		}

		/// <summary>
		/// Returns the component the <code>InputMap</code> was created for.
		/// </summary>
		public JComponent getComponent()
		{
			return default(JComponent);
		}

		/// <summary>
		/// Adds a binding for <code>keyStroke</code> to <code>actionMapKey</code>.
		/// </summary>
		public void put(KeyStroke @keyStroke, object @actionMapKey)
		{
		}

		/// <summary>
		/// Removes the binding for <code>key</code> from this object.
		/// </summary>
		public void remove(KeyStroke @key)
		{
		}

		/// <summary>
		/// Sets the parent, which must be a <code>ComponentInputMap</code>
		/// associated with the same component as this
		/// <code>ComponentInputMap</code>.
		/// </summary>
		public void setParent(InputMap @map)
		{
		}

	}
}
