// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.lang.String

using ScriptCoreLib;
using java.util;

namespace java.lang
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/lang/String.html
	[Script(IsNative = true)]
	public class String
	{
		/// <summary>
		/// Initializes a newly created <code>String</code> object so that it
		/// represents an empty character sequence.
		/// </summary>
		public String()
		{
		}

		/// <summary>
		/// Constructs a new <tt>String</tt> by decoding the specified array of
		/// bytes using the platform's default charset.
		/// </summary>
		public String(sbyte[] @bytes)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>This method does not properly convert bytes into characters.
		/// As of JDK 1.1, the preferred way to do this is via the
		/// <code>String</code> constructors that take a charset name or
		/// that use the platform's default charset.</I>
		/// </summary>
		public String(sbyte[] @ascii, int @hibyte)
		{
		}

		/// <summary>
		/// Constructs a new <tt>String</tt> by decoding the specified subarray of
		/// bytes using the platform's default charset.
		/// </summary>
		public String(sbyte[] @bytes, int @offset, int @length)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>This method does not properly convert bytes into characters.
		/// As of JDK 1.1, the preferred way to do this is via the
		/// <code>String</code> constructors that take a charset name or that use
		/// the platform's default charset.</I>
		/// </summary>
		public String(sbyte[] @ascii, int @hibyte, int @offset, int @count)
		{
		}

		/// <summary>
		/// Constructs a new <tt>String</tt> by decoding the specified subarray of
		/// bytes using the specified charset.
		/// </summary>
		public String(sbyte[] @bytes, int @offset, int @length, string @charsetName)
		{
		}

		/// <summary>
		/// Constructs a new <tt>String</tt> by decoding the specified array of
		/// bytes using the specified charset.
		/// </summary>
		public String(sbyte[] @bytes, string @charsetName)
		{
		}

		/// <summary>
		/// Allocates a new <code>String</code> so that it represents the
		/// sequence of characters currently contained in the character array
		/// argument.
		/// </summary>
		public String(char[] @value)
		{
		}

		/// <summary>
		/// Allocates a new <code>String</code> that contains characters from
		/// a subarray of the character array argument.
		/// </summary>
		public String(char[] @value, int @offset, int @count)
		{
		}

		/// <summary>
		/// Initializes a newly created <code>String</code> object so that it
		/// represents the same sequence of characters as the argument; in other
		/// words, the newly created string is a copy of the argument string.
		/// </summary>
		public String(string @original)
		{
		}

		/// <summary>
		/// Allocates a new string that contains the sequence of characters
		/// currently contained in the string buffer argument.
		/// </summary>
		public String(StringBuffer @buffer)
		{
		}

		/// <summary>
		/// Returns the character at the specified index.
		/// </summary>
		public char charAt(int @index)
		{
			return default(char);
		}

		/// <summary>
		/// Compares this String to another Object.
		/// </summary>
		public int compareTo(object @o)
		{
			return default(int);
		}

		/// <summary>
		/// Compares two strings lexicographically.
		/// </summary>
		public int compareTo(string @anotherString)
		{
			return default(int);
		}

		/// <summary>
		/// Compares two strings lexicographically, ignoring case
		/// differences.
		/// </summary>
		public int compareToIgnoreCase(string @str)
		{
			return default(int);
		}

		/// <summary>
		/// Concatenates the specified string to the end of this string.
		/// </summary>
		public string concat(string @str)
		{
			return default(string);
		}

		/// <summary>
		/// Returns <tt>true</tt> if and only if this <tt>String</tt> represents
		/// the same sequence of characters as the specified <tt>StringBuffer</tt>.
		/// </summary>
		public bool contentEquals(StringBuffer @sb)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a String that represents the character sequence in the
		/// array specified.
		/// </summary>
		static public string copyValueOf(char[] @data)
		{
			return default(string);
		}

		/// <summary>
		/// Returns a String that represents the character sequence in the
		/// array specified.
		/// </summary>
		static public string copyValueOf(char[] @data, int @offset, int @count)
		{
			return default(string);
		}

		/// <summary>
		/// Tests if this string ends with the specified suffix.
		/// </summary>
		public bool endsWith(string @suffix)
		{
			return default(bool);
		}

		/// <summary>
		/// Compares this string to the specified object.
		/// </summary>
		public override bool Equals(object @anObject)
		{
			return default(bool);
		}

		/// <summary>
		/// Compares this <code>String</code> to another <code>String</code>,
		/// ignoring case considerations.
		/// </summary>
		public bool equalsIgnoreCase(string @anotherString)
		{
			return default(bool);
		}

		/// <summary>
		/// Encodes this <tt>String</tt> into a sequence of bytes using the
		/// platform's default charset, storing the result into a new byte array.
		/// </summary>
		public sbyte[] getBytes()
		{
			return default(sbyte[]);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>This method does not properly convert characters into bytes.
		/// As of JDK 1.1, the preferred way to do this is via the
		/// the <code>getBytes()</code> method, which uses the platform's default
		/// charset.</I>
		/// </summary>
		public void getBytes(int @srcBegin, int @srcEnd, sbyte[] @dst, int @dstBegin)
		{
		}

		/// <summary>
		/// Encodes this <tt>String</tt> into a sequence of bytes using the
		/// named charset, storing the result into a new byte array.
		/// </summary>
		public sbyte[] getBytes(string @charsetName)
		{
			return default(sbyte[]);
		}

		/// <summary>
		/// Copies characters from this string into the destination character
		/// array.
		/// </summary>
		public void getChars(int @srcBegin, int @srcEnd, char[] @dst, int @dstBegin)
		{
		}

		/// <summary>
		/// Returns a hash code for this string.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index within this string of the first occurrence of the
		/// specified character.
		/// </summary>
		public int indexOf(int @ch)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index within this string of the first occurrence of the
		/// specified character, starting the search at the specified index.
		/// </summary>
		public int indexOf(int @ch, int @fromIndex)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index within this string of the first occurrence of the
		/// specified substring.
		/// </summary>
		public int indexOf(string @str)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index within this string of the first occurrence of the
		/// specified substring, starting at the specified index.
		/// </summary>
		public int indexOf(string @str, int @fromIndex)
		{
			return default(int);
		}

		/// <summary>
		/// Returns a canonical representation for the string object.
		/// </summary>
		public string intern()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the index within this string of the last occurrence of the
		/// specified character.
		/// </summary>
		public int lastIndexOf(int @ch)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index within this string of the last occurrence of the
		/// specified character, searching backward starting at the specified
		/// index.
		/// </summary>
		public int lastIndexOf(int @ch, int @fromIndex)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index within this string of the rightmost occurrence
		/// of the specified substring.
		/// </summary>
		public int lastIndexOf(string @str)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index within this string of the last occurrence of the
		/// specified substring, searching backward starting at the specified index.
		/// </summary>
		public int lastIndexOf(string @str, int @fromIndex)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the length of this string.
		/// </summary>
		public int length()
		{
			return default(int);
		}

		/// <summary>
		/// Tells whether or not this string matches the given <a
		/// href="../util/regex/Pattern.html#sum">regular expression</a>.
		/// </summary>
		public bool matches(string @regex)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if two string regions are equal.
		/// </summary>
		public bool regionMatches(bool @ignoreCase, int @toffset, string @other, int @ooffset, int @len)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if two string regions are equal.
		/// </summary>
		public bool regionMatches(int @toffset, string @other, int @ooffset, int @len)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a new string resulting from replacing all occurrences of
		/// <code>oldChar</code> in this string with <code>newChar</code>.
		/// </summary>
		public string replace(char @oldChar, char @newChar)
		{
			return default(string);
		}

		/// <summary>
		/// Replaces each substring of this string that matches the given <a
		/// href="../util/regex/Pattern.html#sum">regular expression</a> with the
		/// given replacement.
		/// </summary>
		public string replaceAll(string @regex, string @replacement)
		{
			return default(string);
		}

		/// <summary>
		/// Replaces the first substring of this string that matches the given <a
		/// href="../util/regex/Pattern.html#sum">regular expression</a> with the
		/// given replacement.
		/// </summary>
		public string replaceFirst(string @regex, string @replacement)
		{
			return default(string);
		}

		/// <summary>
		/// Splits this string around matches of the given <a
		/// href="../../java/util/regex/Pattern.html#sum">regular expression</a>.
		/// </summary>
		public string[] split(string @regex)
		{
			return default(string[]);
		}

		/// <summary>
		/// Splits this string around matches of the given <a
		/// href="../../java/util/regex/Pattern.html#sum">regular expression</a>.
		/// </summary>
		public string[] split(string @regex, int @limit)
		{
			return default(string[]);
		}

		/// <summary>
		/// Tests if this string starts with the specified prefix.
		/// </summary>
		public bool startsWith(string @prefix)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests if this string starts with the specified prefix beginning
		/// a specified index.
		/// </summary>
		public bool startsWith(string @prefix, int @toffset)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a new character sequence that is a subsequence of this sequence.
		/// </summary>
		public CharSequence subSequence(int @beginIndex, int @endIndex)
		{
			return default(CharSequence);
		}

		/// <summary>
		/// Returns a new string that is a substring of this string.
		/// </summary>
		public string substring(int @beginIndex)
		{
			return default(string);
		}

		/// <summary>
		/// Returns a new string that is a substring of this string.
		/// </summary>
		public string substring(int @beginIndex, int @endIndex)
		{
			return default(string);
		}

		/// <summary>
		/// Converts this string to a new character array.
		/// </summary>
		public char[] toCharArray()
		{
			return default(char[]);
		}

		/// <summary>
		/// Converts all of the characters in this <code>String</code> to lower
		/// case using the rules of the default locale.
		/// </summary>
		public string toLowerCase()
		{
			return default(string);
		}

		/// <summary>
		/// Converts all of the characters in this <code>String</code> to lower
		/// case using the rules of the given <code>Locale</code>.
		/// </summary>
		public string toLowerCase(Locale @locale)
		{
			return default(string);
		}

		/// <summary>
		/// This object (which is already a string!) is itself returned.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

		/// <summary>
		/// Converts all of the characters in this <code>String</code> to upper
		/// case using the rules of the default locale.
		/// </summary>
		public string toUpperCase()
		{
			return default(string);
		}

		/// <summary>
		/// Converts all of the characters in this <code>String</code> to upper
		/// case using the rules of the given <code>Locale</code>.
		/// </summary>
		public string toUpperCase(Locale @locale)
		{
			return default(string);
		}

		/// <summary>
		/// Returns a copy of the string, with leading and trailing whitespace
		/// omitted.
		/// </summary>
		public string trim()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the string representation of the <code>boolean</code> argument.
		/// </summary>
		static public string valueOf(bool @b)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the string representation of the <code>char</code>
		/// argument.
		/// </summary>
		static public string valueOf(char @c)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the string representation of the <code>char</code> array
		/// argument.
		/// </summary>
		static public string valueOf(char[] @data)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the string representation of a specific subarray of the
		/// <code>char</code> array argument.
		/// </summary>
		static public string valueOf(char[] @data, int @offset, int @count)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the string representation of the <code>double</code> argument.
		/// </summary>
		static public string valueOf(double @d)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the string representation of the <code>float</code> argument.
		/// </summary>
		static public string valueOf(float @f)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the string representation of the <code>int</code> argument.
		/// </summary>
		static public string valueOf(int @i)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the string representation of the <code>long</code> argument.
		/// </summary>
		static public string valueOf(long @l)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the string representation of the <code>Object</code> argument.
		/// </summary>
		static public string valueOf(object @obj)
		{
			return default(string);
		}

	}
}
