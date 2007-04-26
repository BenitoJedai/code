
using ScriptCoreLib;

using java.lang;
using java.io;

namespace java.nio
{
    [Script(IsNative=true)]
    public abstract class CharBuffer : Buffer
    { 
        #region methods
        /// <summary>
        /// Allocates a new character buffer.
        /// </summary>
        public static CharBuffer allocate(int capacity)
        {
            return default(CharBuffer);
        }

        /// <summary>
        /// Returns the character array that backs this buffer  <i>(optional operation)</i>.
        /// </summary>
        public char[] array()
        {
            return default(char[]);
        }

        /// <summary>
        /// Returns the offset within this buffer's backing array of the first element of the buffer  <i>(optional operation)</i>.
        /// </summary>
        public int arrayOffset()
        {
            return default(int);
        }

        /// <summary>
        /// Creates a new, read-only character buffer that shares this buffer's content.
        /// </summary>
        public abstract CharBuffer asReadOnlyBuffer();

        /// <summary>
        /// Reads the character at the given index relative to the current position.
        /// </summary>
        public char charAt(int index)
        {
            return default(char);
        }

        /// <summary>
        /// Compacts this buffer  <i>(optional operation)</i>.
        /// </summary>
        public abstract CharBuffer compact();

        /// <summary>
        /// Compares this buffer to another object.
        /// </summary>
        public int compareTo(object ob)
        {
            return default(int);
        }

        /// <summary>
        /// Creates a new character buffer that shares this buffer's content.
        /// </summary>
        public abstract CharBuffer duplicate();

        /// <summary>
        /// Relative <i>get</i> method.
        /// </summary>
        public abstract char get();

        /// <summary>
        /// Relative bulk <i>get</i> method.
        /// </summary>
        public CharBuffer get(char[] dst)
        {
            return default(CharBuffer);
        }

        /// <summary>
        /// Relative bulk <i>get</i> method.
        /// </summary>
        public CharBuffer get(char[] dst, int offset, int length)
        {
            return default(CharBuffer);
        }

        /// <summary>
        /// Absolute <i>get</i> method.
        /// </summary>
        public abstract char get(int index);

        /// <summary>
        /// Tells whether or not this buffer is backed by an accessible character array.
        /// </summary>
        public bool hasArray()
        {
            return default(bool);
        }

        /// <summary>
        /// Tells whether or not this character buffer is direct.
        /// </summary>
        public abstract bool isDirect();

        /// <summary>
        /// Returns the length of this character buffer.
        /// </summary>
        public int length()
        {
            return default(int);
        }

        /// <summary>
        /// Retrieves this buffer's byte order.
        /// </summary>
        public abstract ByteOrder order();

        /// <summary>
        /// Relative <i>put</i> method  <i>(optional operation)</i>.
        /// </summary>
        public abstract CharBuffer put(char c);

        /// <summary>
        /// Relative bulk <i>put</i> method  <i>(optional operation)</i>.
        /// </summary>
        public CharBuffer put(char[] src)
        {
            return default(CharBuffer);
        }

        /// <summary>
        /// Relative bulk <i>put</i> method  <i>(optional operation)</i>.
        /// </summary>
        public CharBuffer put(char[] src, int offset, int length)
        {
            return default(CharBuffer);
        }

        /// <summary>
        /// Relative bulk <i>put</i> method  <i>(optional operation)</i>.
        /// </summary>
        public CharBuffer put(CharBuffer src)
        {
            return default(CharBuffer);
        }

        /// <summary>
        /// Absolute <i>put</i> method  <i>(optional operation)</i>.
        /// </summary>
        public abstract CharBuffer put(int index, char c);

        /// <summary>
        /// Relative bulk <i>put</i> method  <i>(optional operation)</i>.
        /// </summary>
        public CharBuffer put(string src)
        {
            return default(CharBuffer);
        }

        /// <summary>
        /// Relative bulk <i>put</i> method  <i>(optional operation)</i>.
        /// </summary>
        public CharBuffer put(string src, int start, int end)
        {
            return default(CharBuffer);
        }

        /// <summary>
        /// Creates a new character buffer whose content is a shared subsequence of this buffer's content.
        /// </summary>
        public abstract CharBuffer slice();

        /// <summary>
        /// Creates a new character buffer that represents the specified subsequence of this buffer, relative to the current position.
        /// </summary>
        public abstract CharSequence subSequence(int start, int end);

        /// <summary>
        /// Wraps a character array into a buffer.
        /// </summary>
        public static CharBuffer wrap(char[] array)
        {
            return default(CharBuffer);
        }

        /// <summary>
        /// Wraps a character array into a buffer.
        /// </summary>
        public static CharBuffer wrap(char[] array, int offset, int length)
        {
            return default(CharBuffer);
        }

        /// <summary>
        /// Wraps a string into a buffer.
        /// </summary>
        public static CharBuffer wrap(CharSequence csq)
        {
            return default(CharBuffer);
        }

        /// <summary>
        /// Wraps a character sequence into a buffer.
        /// </summary>
        public static CharBuffer wrap(CharSequence csq, int start, int end)
        {
            return default(CharBuffer);
        }

        #endregion

    }
}
