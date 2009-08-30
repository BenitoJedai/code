// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.beans;
using java.lang;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/Action.html
	[Script(IsNative = true)]
	public interface Action
	{
		/// <summary>
		/// Adds a <code>PropertyChange</code> listener.
		/// </summary>
		void addPropertyChangeListener(PropertyChangeListener @listener);

		/// <summary>
		/// Gets one of this object's properties
		/// using the associated key.
		/// </summary>
		object getValue(string @key);

		/// <summary>
		/// Returns the enabled state of the <code>Action</code>.
		/// </summary>
		bool isEnabled();

		/// <summary>
		/// Sets one of this object's properties
		/// using the associated key.
		/// </summary>
		void putValue(string @key, object @value);

		/// <summary>
		/// Removes a <code>PropertyChange</code> listener.
		/// </summary>
		void removePropertyChangeListener(PropertyChangeListener @listener);

		/// <summary>
		/// Sets the enabled state of the <code>Action</code>.
		/// </summary>
		void setEnabled(bool @b);

	}
}

