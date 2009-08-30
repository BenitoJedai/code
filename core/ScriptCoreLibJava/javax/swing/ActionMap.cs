// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using javax.swing;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/ActionMap.html
	[Script(IsNative = true)]
	public class ActionMap
	{
		/// <summary>
		/// Creates an <code>ActionMap</code> with no parent and no mappings.
		/// </summary>
		public ActionMap()
		{
		}

		/// <summary>
		/// Returns an array of the keys defined in this <code>ActionMap</code> and
		/// its parent.
		/// </summary>
		public Object[] allKeys()
		{
			return default(Object[]);
		}

		/// <summary>
		/// Removes all the mappings from this <code>ActionMap</code>.
		/// </summary>
		public void clear()
		{
		}

		/// <summary>
		/// Returns the binding for <code>key</code>, messaging the
		/// parent <code>ActionMap</code> if the binding is not locally defined.
		/// </summary>
		public Action get(object @key)
		{
			return default(Action);
		}

		/// <summary>
		/// Returns this <code>ActionMap</code>'s parent.
		/// </summary>
		public ActionMap getParent()
		{
			return default(ActionMap);
		}

		/// <summary>
		/// Returns the <code>Action</code> names that are bound in this <code>ActionMap</code>.
		/// </summary>
		public Object[] keys()
		{
			return default(Object[]);
		}

		/// <summary>
		/// Adds a binding for <code>key</code> to <code>action</code>.
		/// </summary>
		public void put(object @key, Action @action)
		{
		}

		/// <summary>
		/// Removes the binding for <code>key</code> from this <code>ActionMap</code>.
		/// </summary>
		public void remove(object @key)
		{
		}

		/// <summary>
		/// Sets this <code>ActionMap</code>'s parent.
		/// </summary>
		public void setParent(ActionMap @map)
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

