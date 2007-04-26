using ScriptCoreLib;

namespace java.lang
{
    /// <summary>
    /// http://java.sun.com/j2se/1.5.0/docs/api/java/lang/StringBuffer.html
    /// </summary>
    [Script(IsNative = true)]
    public class StringBuffer
    {
        // Constructor Summary
        /// <summary>
        /// Constructs a string buffer with no characters in it and an initial capacity of 16 characters.
        /// </summary>
        public StringBuffer()
        {
        }

        /// <summary>
        /// Constructs a string buffer with no characters in it and an initial capacity specified by the
        /// </summary>
        public StringBuffer(int length)
        {
        }

        /// <summary>
        /// Constructs a string buffer so that it represents the same sequence of characters as the string argument; in other words, the initial contents of the string buffer is a copy of the argument string.
        /// </summary>
        public StringBuffer(string str)
        {
        }

        // Method Summary
        /// <summary>
        /// Appends the string representation of the
        /// </summary>
        public   StringBuffer  append (bool  b)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Appends the string representation of the
        /// </summary>
        public   StringBuffer  append (char c)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Appends the string representation of the
        /// </summary>
        public   StringBuffer  append (char[] str)
        {
            return default(StringBuffer );
        }

        /// <summary>
        /// Appends the string representation of a subarray of the
        /// </summary>
        public   StringBuffer  append (char[] str, int offset, int len)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Appends the string representation of the
        /// </summary>
        public   StringBuffer  append (double d)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Appends the string representation of the
        /// </summary>
        public   StringBuffer  append (float f)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Appends the string representation of the
        /// </summary>
        public   StringBuffer  append (int i)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Appends the string representation of the
        /// </summary>
        public   StringBuffer  append (long l)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Appends the string representation of the
        /// </summary>
        public   StringBuffer  append (object  obj)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Appends the string to this string buffer.
        /// </summary>
        public   StringBuffer  append (string  str)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Appends the specified
        /// </summary>
        public   StringBuffer  append (StringBuffer sb)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Returns the current capacity of the String buffer.
        /// </summary>
        public   int  capacity ()
        {
            return default( int );
        }

        /// <summary>
        /// The specified character of the sequence currently represented by the string buffer, as indicated by the
        /// </summary>
        public   char  charAt (int index)
        {
            return default( char );
        }

        /// <summary>
        /// Removes the characters in a substring of this
        /// </summary>
        public   StringBuffer  delete (int start, int end)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Removes the character at the specified position in this
        /// </summary>
        public   StringBuffer  deleteCharAt (int index)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Ensures that the capacity of the buffer is at least equal to the specified minimum.
        /// </summary>
        public   void  ensureCapacity (int minimumCapacity)
        {
            return;
        }

        /// <summary>
        /// Characters are copied from this string buffer into the destination character array
        /// </summary>
        public   void  getChars (int srcBegin, int srcEnd, char[] dst, int dstBegin)
        {
            return;
        }

        /// <summary>
        /// Returns the index within this string of the first occurrence of the specified substring.
        /// </summary>
        public   int  indexOf (string  str)
        {
            return default( int );
        }

        /// <summary>
        /// Returns the index within this string of the first occurrence of the specified substring, starting at the specified index.
        /// </summary>
        public   int  indexOf (string  str, int fromIndex)
        {
            return default( int );
        }

        /// <summary>
        /// Inserts the string representation of the
        /// </summary>
        public   StringBuffer  insert (int offset, bool  b)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Inserts the string representation of the
        /// </summary>
        public   StringBuffer  insert (int offset, char c)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Inserts the string representation of the
        /// </summary>
        public   StringBuffer  insert (int offset, char[] str)
        {
            return default(StringBuffer);
        }

        /// <summary>
        /// Inserts the string representation of a subarray of the
        /// </summary>
        public  StringBuffer  insert (int index, char[] str, int offset, int len)
        {
            return default(StringBuffer);
        }

        /// <summary>
        /// Inserts the string representation of the
        /// </summary>
        public   StringBuffer  insert (int offset, double d)
        {
            return default(StringBuffer );
        }

        /// <summary>
        /// Inserts the string representation of the
        /// </summary>
        public   StringBuffer insert (int offset, float f)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Inserts the string representation of the second
        /// </summary>
        public   StringBuffer  insert (int offset, int i)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Inserts the string representation of the
        /// </summary>
        public   StringBuffer  insert (int offset, long l)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Inserts the string representation of the
        /// </summary>
        public  StringBuffer  insert (int offset, object  obj)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Inserts the string into this string buffer.
        /// </summary>
        public   StringBuffer  insert (int offset, string  str)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// Returns the index within this string of the rightmost occurrence of the specified substring.
        /// </summary>
        public   int  lastIndexOf (string  str)
        {
            return default( int );
        }

        /// <summary>
        /// Returns the index within this string of the last occurrence of the specified substring.
        /// </summary>
        public   int  lastIndexOf (string  str, int fromIndex)
        {
            return default( int );
        }

        /// <summary>
        /// Returns the length (character count) of this string buffer.
        /// </summary>
        public   int  length ()
        {
            return default( int );
        }

        /// <summary>
        /// Replaces the characters in a substring of this
        /// </summary>
        public   StringBuffer  replace (int start, int end, string  str)
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// The character sequence contained in this string buffer is replaced by the reverse of the sequence.
        /// </summary>
        public   StringBuffer  reverse ()
        {
            return default( StringBuffer );
        }

        /// <summary>
        /// The character at the specified index of this string buffer is set to
        /// </summary>
        public   void  setCharAt (int index, char ch)
        {
            return;
        }

        /// <summary>
        /// Sets the length of this String buffer.
        /// </summary>
        public   void  setLength (int newLength)
        {
            return;
        }

        /// <summary>
        /// Returns a new character sequence that is a subsequence of this sequence.
        /// </summary>
        public   CharSequence  subSequence (int start, int end)
        {
            return default( CharSequence );
        }

        /// <summary>
        /// Returns a new
        /// </summary>
        public   string   substring (int start)
        {
            return default( string  );
        }

        /// <summary>
        /// Returns a new
        /// </summary>
        public   string   substring (int start, int end)
        {
            return default( string  );
        }

        /// <summary>
        /// Converts to a string representing the data in this string buffer.
        /// </summary>
        public   string   toString ()
        {
            return default( string  );
        }

        // Methods inherited from class java.lang.Object


    }
}
