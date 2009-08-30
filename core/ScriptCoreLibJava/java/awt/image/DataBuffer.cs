// This source code was generated for ScriptCoreLib
using ScriptCoreLib;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/DataBuffer.html
	[Script(IsNative = true)]
	public abstract class DataBuffer
	{
		/// <summary>
		/// Constructs a DataBuffer containing one bank of the specified
		/// data type and size.
		/// </summary>
		public DataBuffer(int @dataType, int @size)
		{
		}

		/// <summary>
		/// Constructs a DataBuffer containing the specified number of
		/// banks.
		/// </summary>
		public DataBuffer(int @dataType, int @size, int @numBanks)
		{
		}

		/// <summary>
		/// Constructs a DataBuffer that contains the specified number
		/// of banks.
		/// </summary>
		public DataBuffer(int @dataType, int @size, int @numBanks, int @offset)
		{
		}

		/// <summary>
		/// Constructs a DataBuffer which contains the specified number
		/// of banks.
		/// </summary>
		public DataBuffer(int @dataType, int @size, int @numBanks, int[] @offsets)
		{
		}

		/// <summary>
		/// Returns the data type of this DataBuffer.
		/// </summary>
		public int getDataType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the size (in bits) of the data type, given a datatype tag.
		/// </summary>
		static public int getDataTypeSize(int @type)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the requested data array element from the first (default) bank
		/// as an integer.
		/// </summary>
		public int getElem(int @i)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the requested data array element from the specified bank
		/// as an integer.
		/// </summary>
		abstract public int getElem(int @bank, int @i);

		/// <summary>
		/// Returns the requested data array element from the first (default) bank
		/// as a double.
		/// </summary>
		public double getElemDouble(int @i)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the requested data array element from the specified bank as
		/// a double.
		/// </summary>
		public double getElemDouble(int @bank, int @i)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the requested data array element from the first (default) bank
		/// as a float.
		/// </summary>
		public float getElemFloat(int @i)
		{
			return default(float);
		}

		/// <summary>
		/// Returns the requested data array element from the specified bank
		/// as a float.
		/// </summary>
		public float getElemFloat(int @bank, int @i)
		{
			return default(float);
		}

		/// <summary>
		/// Returns the number of banks in this DataBuffer.
		/// </summary>
		public int getNumBanks()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the offset of the default bank in array elements.
		/// </summary>
		public int getOffset()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the offsets (in array elements) of all the banks.
		/// </summary>
		public int[] getOffsets()
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns the size (in array elements) of all banks.
		/// </summary>
		public int getSize()
		{
			return default(int);
		}

		/// <summary>
		/// Sets the requested data array element in the first (default) bank
		/// from the given integer.
		/// </summary>
		public void setElem(int @i, int @val)
		{
		}

		/// <summary>
		/// Sets the requested data array element in the specified bank
		/// from the given integer.
		/// </summary>
		abstract public void setElem(int @bank, int @i, int @val);

		/// <summary>
		/// Sets the requested data array element in the first (default) bank
		/// from the given double.
		/// </summary>
		public void setElemDouble(int @i, double @val)
		{
		}

		/// <summary>
		/// Sets the requested data array element in the specified bank
		/// from the given double.
		/// </summary>
		public void setElemDouble(int @bank, int @i, double @val)
		{
		}

		/// <summary>
		/// Sets the requested data array element in the first (default) bank
		/// from the given float.
		/// </summary>
		public void setElemFloat(int @i, float @val)
		{
		}

		/// <summary>
		/// Sets the requested data array element in the specified bank
		/// from the given float.
		/// </summary>
		public void setElemFloat(int @bank, int @i, float @val)
		{
		}

	}
}

