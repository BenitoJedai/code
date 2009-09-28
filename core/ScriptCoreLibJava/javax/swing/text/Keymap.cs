// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.text.Keymap

using ScriptCoreLib;
using java.lang;
using javax.swing;

namespace javax.swing.text
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/text/Keymap.html
	[Script(IsNative = true)]
	public interface Keymap
	{
		/// <summary>
		/// Adds a binding to the keymap.
		/// </summary>
		void addActionForKeyStroke(KeyStroke @key, Action @a);

		/// <summary>
		/// Fetches the action appropriate for the given symbolic
		/// event sequence.
		/// </summary>
		Action getAction(KeyStroke @key);

		/// <summary>
		/// Fetches all of the actions defined in this keymap.
		/// </summary>
		Action[] getBoundActions();

		/// <summary>
		/// Fetches all of the keystrokes in this map that
		/// are bound to some action.
		/// </summary>
		KeyStroke[] getBoundKeyStrokes();

		/// <summary>
		/// Fetches the default action to fire if a
		/// key is typed (i.e.
		/// </summary>
		Action getDefaultAction();

		/// <summary>
		/// Fetches the keystrokes that will result in
		/// the given action.
		/// </summary>
		KeyStroke[] getKeyStrokesForAction(Action @a);

		/// <summary>
		/// Fetches the name of the set of key-bindings.
		/// </summary>
		string getName();

		/// <summary>
		/// Fetches the parent keymap used to resolve key-bindings.
		/// </summary>
		Keymap getResolveParent();

		/// <summary>
		/// Determines if the given key sequence is locally defined.
		/// </summary>
		bool isLocallyDefined(KeyStroke @key);

		/// <summary>
		/// Removes all bindings from the keymap.
		/// </summary>
		void removeBindings();

		/// <summary>
		/// Removes a binding from the keymap.
		/// </summary>
		void removeKeyStrokeBinding(KeyStroke @keys);

		/// <summary>
		/// Set the default action to fire if a key is typed.
		/// </summary>
		void setDefaultAction(Action @a);

		/// <summary>
		/// Sets the parent keymap, which will be used to
		/// resolve key-bindings.
		/// </summary>
		void setResolveParent(Keymap @parent);

	}
}
