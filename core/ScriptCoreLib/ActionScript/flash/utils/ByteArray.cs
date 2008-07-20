using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.utils
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/utils/ByteArray.html
    [Script(IsNative=true)]
    public class ByteArray
    {
        #region Properties
        /// <summary>
        /// [read-only] The number of bytes of data available for reading from the current position in the byte array to the end of the array.
        /// </summary>
        public uint bytesAvailable { get; private set; }

        /// <summary>
        /// [static] Denotes the default object encoding for the ByteArray class to use for a new ByteArray instance.
        /// </summary>
        public uint defaultObjectEncoding { get; set; }

        /// <summary>
        /// Changes or reads the byte order for the data; either Endian.BIG_ENDIAN or Endian.LITTLE_ENDIAN.
        /// </summary>
        public string endian { get; set; }

        /// <summary>
        /// The length of the ByteArray object, in bytes.
        /// </summary>
        public uint length { get; set; }

        /// <summary>
        /// Used to determine whether the ActionScript 3.0, ActionScript 2.0, or ActionScript 1.0 format should be used when writing to, or reading from, a ByteArray instance.
        /// </summary>
        public uint objectEncoding { get; set; }

        /// <summary>
        /// Moves, or returns the current position, in bytes, of the file pointer into the ByteArray object.
        /// </summary>
        public uint position { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Compresses the byte array.
        /// </summary>
        public void compress()
        {
        }

        /// <summary>
        /// Reads a Boolean value from the byte stream.
        /// </summary>
        public bool readBoolean()
        {
            return default(bool);
        }

        /// <summary>
        /// Reads a signed byte from the byte stream.
        /// </summary>
        public int readByte()
        {
            return default(int);
        }

        /// <summary>
        /// Reads the number of data bytes, specified by the length parameter, from the byte stream.
        /// </summary>
        public void readBytes(ByteArray bytes, uint offset, uint length)
        {
        }

        /// <summary>
        /// Reads the number of data bytes, specified by the length parameter, from the byte stream.
        /// </summary>
        public void readBytes(ByteArray bytes, uint offset)
        {
        }

        /// <summary>
        /// Reads the number of data bytes, specified by the length parameter, from the byte stream.
        /// </summary>
        public void readBytes(ByteArray bytes)
        {
        }

        /// <summary>
        /// Reads an IEEE 754 double-precision (64-bit) floating-point number from the byte stream.
        /// </summary>
        public double readDouble()
        {
            return default(double);
        }

        /// <summary>
        /// Reads an IEEE 754 single-precision (32-bit) floating-point number from the byte stream.
        /// </summary>
        public double readFloat()
        {
            return default(double);
        }

        /// <summary>
        /// Reads a signed 32-bit integer from the byte stream.
        /// </summary>
        public int readInt()
        {
            return default(int);
        }

        /// <summary>
        /// Reads a multibyte string of specified length from the byte stream using the specified character set.
        /// </summary>
        public string readMultiByte(uint length, string charSet)
        {
            return default(string);
        }

        /// <summary>
        /// Reads an object from the byte array, encoded in AMF serialized format.
        /// </summary>
        public object readObject()
        {
            return default(object);
        }

        /// <summary>
        /// Reads a signed 16-bit integer from the byte stream.
        /// </summary>
        public short readShort()
        {
            return default(short);
        }

        /// <summary>
        /// Reads an unsigned byte from the byte stream.
        /// </summary>
        public uint readUnsignedByte()
        {
            return default(uint);
        }

        /// <summary>
        /// Reads an unsigned 32-bit integer from the byte stream.
        /// </summary>
        public uint readUnsignedInt()
        {
            return default(uint);
        }

        /// <summary>
        /// Reads an unsigned 16-bit integer from the byte stream.
        /// </summary>
        public uint readUnsignedShort()
        {
            return default(uint);
        }

        /// <summary>
        /// Reads a UTF-8 string from the byte stream.
        /// </summary>
        public string readUTF()
        {
            return default(string);
        }

        /// <summary>
        /// Reads a sequence of UTF-8 bytes specified by the length parameter from the byte stream and returns a string.
        /// </summary>
        public string readUTFBytes(uint length)
        {
            return default(string);
        }

        /// <summary>
        /// Decompresses the byte array.
        /// </summary>
        public void uncompress()
        {
        }

        /// <summary>
        /// Writes a Boolean value.
        /// </summary>
        public void writeBoolean(bool value)
        {
        }

        /// <summary>
        /// Writes a byte to the byte stream.
        /// </summary>
        public void writeByte(int value)
        {
        }

        /// <summary>
        /// Writes a sequence of length bytes from the specified byte array, bytes, starting offset (zero-based index) bytes into the byte stream.
        /// </summary>
        public void writeBytes(ByteArray bytes, uint offset, uint length)
        {
        }

        /// <summary>
        /// Writes a sequence of length bytes from the specified byte array, bytes, starting offset (zero-based index) bytes into the byte stream.
        /// </summary>
        public void writeBytes(ByteArray bytes, uint offset)
        {
        }

        /// <summary>
        /// Writes a sequence of length bytes from the specified byte array, bytes, starting offset (zero-based index) bytes into the byte stream.
        /// </summary>
        public void writeBytes(ByteArray bytes)
        {
        }

        /// <summary>
        /// Writes an IEEE 754 double-precision (64-bit) floating-point number to the byte stream.
        /// </summary>
        public void writeDouble(double value)
        {
        }

        /// <summary>
        /// Writes an IEEE 754 single-precision (32-bit) floating-point number to the byte stream.
        /// </summary>
        public void writeFloat(double value)
        {
        }

        /// <summary>
        /// Writes a 32-bit signed integer to the byte stream.
        /// </summary>
        public void writeInt(int value)
        {
        }

        /// <summary>
        /// Writes a multibyte string to the byte stream using the specified character set.
        /// </summary>
        public void writeMultiByte(string value, string charSet)
        {
        }

        /// <summary>
        /// Writes an object into the byte array in AMF serialized format.
        /// </summary>
        public void writeObject(object @object)
        {
        }

        /// <summary>
        /// Writes a 16-bit integer to the byte stream.
        /// </summary>
        public void writeShort(int value)
        {
        }

        /// <summary>
        /// Writes a 32-bit unsigned integer to the byte stream.
        /// </summary>
        public void writeUnsignedInt(uint value)
        {
        }

        /// <summary>
        /// Writes a UTF-8 string to the byte stream.
        /// </summary>
        public void writeUTF(string value)
        {
        }

        /// <summary>
        /// Writes a UTF-8 string to the byte stream.
        /// </summary>
        public void writeUTFBytes(string value)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a ByteArray instance representing a packed array of bytes, so that you can use the methods and properties in this class to optimize your data storage and stream.
        /// </summary>
        public ByteArray()
        {
        }

        #endregion

    }
}
