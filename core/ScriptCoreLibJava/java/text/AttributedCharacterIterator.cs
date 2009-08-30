// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using java.text;
using java.util;

namespace java.text
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/text/AttributedCharacterIterator.html
	[Script(IsNative = true)]
	public interface AttributedCharacterIterator : CharacterIterator
	{
	
		/// <summary>
		/// Returns the keys of all attributes defined on the
		/// iterator's text range.
		/// </summary>
		Set getAllAttributeKeys();

		/// <summary>
		/// Returns the value of the named attribute for the current character.
		/// </summary>
		//object getAttribute(AttributedCharacterIterator.Attribute @attribute);

		/// <summary>
		/// Returns a map with the attributes defined on the current
		/// character.
		/// </summary>
		Map getAttributes();

		/// <summary>
		/// Returns the index of the first character following the run
		/// with respect to all attributes containing the current character.
		/// </summary>
		int getRunLimit();

		/// <summary>
		/// Returns the index of the first character following the run
		/// with respect to the given attribute containing the current character.
		/// </summary>
		//int getRunLimit(AttributedCharacterIterator.Attribute @attribute);

		/// <summary>
		/// Returns the index of the first character following the run
		/// with respect to the given attributes containing the current character.
		/// </summary>
		int getRunLimit(Set @attributes);

		/// <summary>
		/// Returns the index of the first character of the run
		/// with respect to all attributes containing the current character.
		/// </summary>
		int getRunStart();

		/// <summary>
		/// Returns the index of the first character of the run
		/// with respect to the given attribute containing the current character.
		/// </summary>
		//int getRunStart(AttributedCharacterIterator.Attribute @attribute);

		/// <summary>
		/// Returns the index of the first character of the run
		/// with respect to the given attributes containing the current character.
		/// </summary>
		int getRunStart(Set @attributes);

	}
}

