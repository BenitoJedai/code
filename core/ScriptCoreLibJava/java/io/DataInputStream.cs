using ScriptCoreLib;

namespace java.io
{
    [Script(IsNative = true)]
    public class DataInputStream : FilterInputStream
    {
        public DataInputStream(InputStream e)
        {

        }






        #region methods




        /// <summary>
        /// See the general contract of the <code>readBoolean</code> method of <code>DataInput</code>.
        /// </summary>
        public bool readBoolean()
        {
            return default(bool);
        }

        /// <summary>
        /// See the general contract of the <code>readByte</code> method of <code>DataInput</code>.
        /// </summary>
        public sbyte readByte()
        {
            return default(sbyte);
        }

        /// <summary>
        /// See the general contract of the <code>readChar</code> method of <code>DataInput</code>.
        /// </summary>
        public char readChar()
        {
            return default(char);
        }

        /// <summary>
        /// See the general contract of the <code>readDouble</code> method of <code>DataInput</code>.
        /// </summary>
        public double readDouble()
        {
            return default(double);
        }

        /// <summary>
        /// See the general contract of the <code>readFloat</code> method of <code>DataInput</code>.
        /// </summary>
        public float readFloat()
        {
            return default(float);
        }

        /// <summary>
        /// See the general contract of the <code>readFully</code> method of <code>DataInput</code>.
        /// </summary>
        public void readFully(sbyte[] b)
        {
        }

        /// <summary>
        /// See the general contract of the <code>readFully</code> method of <code>DataInput</code>.
        /// </summary>
        public void readFully(sbyte[] b, int off, int len)
        {
        }

        /// <summary>
        /// See the general contract of the <code>readInt</code> method of <code>DataInput</code>.
        /// </summary>
        public int readInt()
        {
            return default(int);
        }

        /// <summary>
        /// <B>Deprecated.</B> <I>This method does not properly convert bytes to characters. As of JDK 1.1, the preferred way to read lines of text is via the <code>BufferedReader.readLine()</code> method.  Programs that use the <code>DataInputStream</code> class to read lines can be converted to use the <code>BufferedReader</code> class by replacing code of the form: <blockquote><pre>     DataInputStream d = new DataInputStream(in); </pre></blockquote> with: <blockquote><pre>     BufferedReader d          = new BufferedReader(new InputStreamReader(in)); </pre></blockquote></I>
        /// </summary>
        public string readLine()
        {
            return default(string);
        }

        /// <summary>
        /// See the general contract of the <code>readLong</code> method of <code>DataInput</code>.
        /// </summary>
        public long readLong()
        {
            return default(long);
        }

        /// <summary>
        /// See the general contract of the <code>readShort</code> method of <code>DataInput</code>.
        /// </summary>
        public short readShort()
        {
            return default(short);
        }

        /// <summary>
        /// See the general contract of the <code>readUnsignedByte</code> method of <code>DataInput</code>.
        /// </summary>
        public int readUnsignedByte()
        {
            return default(int);
        }

        /// <summary>
        /// See the general contract of the <code>readUnsignedShort</code> method of <code>DataInput</code>.
        /// </summary>
        public int readUnsignedShort()
        {
            return default(int);
        }

        /// <summary>
        /// See the general contract of the <code>readUTF</code> method of <code>DataInput</code>.
        /// </summary>
        public string readUTF()
        {
            return default(string);
        }

        /// <summary>
        /// Reads from the stream <code>in</code> a representation of a Unicode  character string encoded in Java modified UTF-8 format; this string of characters  is then returned as a <code>String</code>.
        /// </summary>
        public static string readUTF(DataInput @in)
        {
            return default(string);
        }

        /// <summary>
        /// See the general contract of the <code>skipBytes</code> method of <code>DataInput</code>.
        /// </summary>
        public int skipBytes(int n)
        {
            return default(int);
        }

        #endregion

    }
}
