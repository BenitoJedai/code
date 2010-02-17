// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.util.Dictionary

using ScriptCoreLib;
using java.lang;

namespace java.util
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/util/Dictionary.html
	[Script(IsNative = true)]
	public abstract class Dictionary
	{
		/// <summary>
		/// Sole constructor.
		/// </summary>
		public Dictionary()
		{
		}

		/// <summary>
		/// Returns an enumeration of the values in this dictionary.
		/// </summary>
		abstract public Enumeration elements();

		/// <summary>
		/// Returns the value to which the key is mapped in this dictionary.
		/// </summary>
		abstract public object get(object @key);

		/// <summary>
		/// Tests if this dictionary maps no keys to value.
		/// </summary>
		abstract public bool isEmpty();

		/// <summary>
		/// Returns an enumeration of the keys in this dictionary.
		/// </summary>
		abstract public Enumeration keys();

		/// <summary>
		/// Maps the specified <code>key</code> to the specified
		/// <code>value</code> in this dictionary.
		/// </summary>
		abstract public object put(object @key, object @value);

		/// <summary>
		/// Removes the <code>key</code> (and its corresponding
		/// <code>value</code>) from this dictionary.
		/// </summary>
		abstract public object remove(object @key);

		/// <summary>
		/// Returns the number of entries (dinstint keys) in this dictionary.
		/// </summary>
		abstract public int size();

	}
}
