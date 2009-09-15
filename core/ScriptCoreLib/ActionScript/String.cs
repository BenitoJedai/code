using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/String.html
	[Script(IsNative = true)]
    public sealed class String
    {


        #region Constants

        #endregion

        #region Properties

        /// <summary>
        /// [read-only] An integer specifying the number of characters in the specified String object.
        /// </summary>
        public int length { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new String object initialized to the specified string.
        /// </summary>
        public String(string val)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the character in the position specified by the index parameter.
        /// </summary>
        public string charAt(double index)
        {
            return default(string);
        }

        /// <summary>
        /// Returns the character in the position specified by the index parameter.
        /// </summary>
        public string charAt()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the numeric Unicode character code of the character at the specified index.
        /// </summary>
        public double charCodeAt(double index)
        {
            return default(double);
        }

        /// <summary>
        /// Returns the numeric Unicode character code of the character at the specified index.
        /// </summary>
        public double charCodeAt()
        {
            return default(double);
        }

        /// <summary>
        /// Appends the supplied arguments to the end of the String object, converting them to strings if necessary, and returns the resulting string.
        /// </summary>
        public string concat(object args)
        {
            return default(string);
        }

        /// <summary>
        /// Appends the supplied arguments to the end of the String object, converting them to strings if necessary, and returns the resulting string.
        /// </summary>
        public string concat()
        {
            return default(string);
        }

        /// <summary>
        /// [static] Returns a string comprising the characters represented by the Unicode character codes in the parameters.
        /// </summary>
        public static string fromCharCode(object charCodes)
        {
            return default(string);
        }

        /// <summary>
        /// [static] Returns a string comprising the characters represented by the Unicode character codes in the parameters.
        /// </summary>
        public static string fromCharCode()
        {
            return default(string);
        }

        /// <summary>
        /// Searches the string and returns the position of the first occurrence of val found at or after startIndex within the calling string.
        /// </summary>
        public int indexOf(string val, double startIndex)
        {
            return default(int);
        }

        /// <summary>
        /// Searches the string and returns the position of the first occurrence of val found at or after startIndex within the calling string.
        /// </summary>
        public int indexOf(string val)
        {
            return default(int);
        }

        /// <summary>
        /// Searches the string from right to left and returns the index of the last occurrence of val found before startIndex.
        /// </summary>
        public int lastIndexOf(string val, double startIndex)
        {
            return default(int);
        }

        /// <summary>
        /// Searches the string from right to left and returns the index of the last occurrence of val found before startIndex.
        /// </summary>
        public int lastIndexOf(string val)
        {
            return default(int);
        }

        /// <summary>
        /// Compares the sort order of two or more strings and returns the result of the comparison as an integer.
        /// </summary>
        public int localeCompare(string other, object values)
        {
            return default(int);
        }

        /// <summary>
        /// Compares the sort order of two or more strings and returns the result of the comparison as an integer.
        /// </summary>
        public int localeCompare(string other)
        {
            return default(int);
        }

        /// <summary>
        /// Matches the specifed pattern against the string.
        /// </summary>
        public Array match(object pattern)
        {
            return default(Array);
        }

        /// <summary>
        /// Matches the specifed pattern against the string and returns a new string in which the first match of pattern is replaced with the content specified by repl.
        /// </summary>
        public string replace(object pattern, object repl)
        {
            return default(string);
        }

        /// <summary>
        /// Searches for the specifed pattern and returns the index of the first matching substring.
        /// </summary>
        public int search(object pattern)
        {
            return default(int);
        }

        /// <summary>
        /// Returns a string that includes the startIndex character and all characters up to, but not including, the endIndex character.
        /// </summary>
        public string slice(double startIndex, double endIndex)
        {
            return default(string);
        }

        /// <summary>
        /// Returns a string that includes the startIndex character and all characters up to, but not including, the endIndex character.
        /// </summary>
        public string slice(double startIndex)
        {
            return default(string);
        }

        /// <summary>
        /// Returns a string that includes the startIndex character and all characters up to, but not including, the endIndex character.
        /// </summary>
        public string slice()
        {
            return default(string);
        }

        /// <summary>
        /// Splits a String object into an array of substrings by dividing it wherever the specified delimiter parameter occurs.
        /// </summary>
        public string[] split(object delimiter, double limit)
        {
            return default(string[]);
        }

        /// <summary>
        /// Splits a String object into an array of substrings by dividing it wherever the specified delimiter parameter occurs.
        /// </summary>
        public string[] split(object delimiter)
        {
            return default(string[]);
        }

        /// <summary>
        /// Returns a substring consisting of the characters that start at the specified startIndex and with a length specified by len.
        /// </summary>
        public string substr(double startIndex, double len)
        {
            return default(string);
        }

        /// <summary>
        /// Returns a substring consisting of the characters that start at the specified startIndex and with a length specified by len.
        /// </summary>
        public string substr(double startIndex)
        {
            return default(string);
        }

        /// <summary>
        /// Returns a substring consisting of the characters that start at the specified startIndex and with a length specified by len.
        /// </summary>
        public string substr()
        {
            return default(string);
        }

        /// <summary>
        /// Returns a string consisting of the character specified by startIndex and all characters up to endIndex - 1.
        /// </summary>
        public string substring(double startIndex, double endIndex)
        {
            return default(string);
        }

        /// <summary>
        /// Returns a string consisting of the character specified by startIndex and all characters up to endIndex - 1.
        /// </summary>
        public string substring(double startIndex)
        {
            return default(string);
        }

        /// <summary>
        /// Returns a string consisting of the character specified by startIndex and all characters up to endIndex - 1.
        /// </summary>
        public string substring()
        {
            return default(string);
        }

        /// <summary>
        /// Returns a copy of this string, with all uppercase characters converted to lowercase.
        /// </summary>
        public string toLocaleLowerCase()
        {
            return default(string);
        }

        /// <summary>
        /// Returns a copy of this string, with all lowercase characters converted to uppercase.
        /// </summary>
        public string toLocaleUpperCase()
        {
            return default(string);
        }

        /// <summary>
        /// Returns a copy of this string, with all uppercase characters converted to lowercase.
        /// </summary>
        public string toLowerCase()
        {
            return default(string);
        }

        /// <summary>
        /// Returns a copy of this string, with all lowercase characters converted to uppercase.
        /// </summary>
        public string toUpperCase()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the primitive value of a String instance.
        /// </summary>
        public string valueOf()
        {
            return default(string);
        }

        #endregion
    }



}
