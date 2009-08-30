// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using javax.swing;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/InputMap.html
	[Script(IsNative = true)]
	public class InputMap
	{
		/// <summary>
		/// Creates an <code>InputMap</code> with no parent and no mappings.
		/// </summary>
		public InputMap()
		{
		}

		/// <summary>
		/// Returns an array of the <code>KeyStroke</code>s defined in this
		/// <code>InputMap</code> and its parent.
		/// </summary>
		public KeyStroke[] allKeys()
		{
			return default(KeyStroke[]);
		}

		/// <summary>
		/// Removes all the mappings from this <code>InputMap</code>.
		/// </summary>
		public void clear()
		{
		}

		/// <summary>
		/// Returns the binding for <code>keyStroke</code>, messaging the
		/// parent <code>InputMap</code> if the binding is not locally defined.
		/// </summary>
		public object get(KeyStroke @keyStroke)
		{
			return default(object);
		}

		/// <summary>
		/// Gets this <code>InputMap</code>'s parent.
		/// </summary>
		public InputMap getParent()
		{
			return default(InputMap);
		}

		/// <summary>
		/// Returns the <code>KeyStroke</code>s that are bound in this <code>InputMap</code>.
		/// </summary>
		public KeyStroke[] keys()
		{
			return default(KeyStroke[]);
		}

		/// <summary>
		/// Adds a binding for <code>keyStroke</code> to <code>actionMapKey</code>.
		/// </summary>
		public void put(KeyStroke @keyStroke, object @actionMapKey)
		{
		}

		/// <summary>
		/// Removes the binding for <code>key</code> from this
		/// <code>InputMap</code>.
		/// </summary>
		public void remove(KeyStroke @key)
		{
		}

		/// <summary>
		/// Sets this <code>InputMap</code>'s parent.
		/// </summary>
		public void setParent(InputMap @map)
		{
		}

		/// <summary>
		/// Returns the number of <code>KeyStroke</code> bindings.
		/// </summary>
		public int size()
		{
			return default(int);
		}

	}
}

