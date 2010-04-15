// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/org.w3c.dom.CharacterData

using ScriptCoreLib;
using java.lang;

namespace org.w3c.dom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/org/w3c/dom/CharacterData.html
	[Script(IsNative = true)]
	public interface CharacterData : Node
	{
		/// <summary>
		/// Append the string to the end of the character data of the node.
		/// </summary>
		void appendData(string @arg);

		/// <summary>
		/// Remove a range of 16-bit units from the node.
		/// </summary>
		void deleteData(int @offset, int @count);

		/// <summary>
		/// The character data of the node that implements this interface.
		/// </summary>
		string getData();

		/// <summary>
		/// The number of 16-bit units that are available through <code>data</code>
		/// and the <code>substringData</code> method below.
		/// </summary>
		int getLength();

		/// <summary>
		/// Insert a string at the specified 16-bit unit offset.
		/// </summary>
		void insertData(int @offset, string @arg);

		/// <summary>
		/// Replace the characters starting at the specified 16-bit unit offset
		/// with the specified string.
		/// </summary>
		void replaceData(int @offset, int @count, string @arg);

		/// <summary>
		/// The character data of the node that implements this interface.
		/// </summary>
		void setData(string @data);

		/// <summary>
		/// Extracts a range of data from the node.
		/// </summary>
		string substringData(int @offset, int @count);

	}
}
