// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using java.math;
using java.util;

namespace java.math
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/math/BigInteger.html
	[Script(IsNative = true)]
	public class BigInteger : Number
	{
		/// <summary>
		/// Translates a byte array containing the two's-complement binary
		/// representation of a BigInteger into a BigInteger.
		/// </summary>
		public BigInteger(byte[] @val)
		{
		}

		/// <summary>
		/// Translates the sign-magnitude representation of a BigInteger into a
		/// BigInteger.
		/// </summary>
		public BigInteger(int @signum, byte[] @magnitude)
		{
		}

		/// <summary>
		/// Constructs a randomly generated positive BigInteger that is probably
		/// prime, with the specified bitLength.
		/// </summary>
		public BigInteger(int @bitLength, int @certainty, Random @rnd)
		{
		}

		/// <summary>
		/// Constructs a randomly generated BigInteger, uniformly distributed over
		/// the range <tt>0</tt> to <tt>(2<sup>numBits</sup> - 1)</tt>, inclusive.
		/// </summary>
		public BigInteger(int @numBits, Random @rnd)
		{
		}

		/// <summary>
		/// Translates the decimal String representation of a BigInteger into a
		/// BigInteger.
		/// </summary>
		public BigInteger(string @val)
		{
		}

		/// <summary>
		/// Translates the String representation of a BigInteger in the specified
		/// radix into a BigInteger.
		/// </summary>
		public BigInteger(string @val, int @radix)
		{
		}

		/// <summary>
		/// Returns a BigInteger whose value is the absolute value of this
		/// BigInteger.
		/// </summary>
		public BigInteger abs()
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this + val)</tt>.
		/// </summary>
		public BigInteger add(BigInteger @val)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this &amp; val)</tt>.
		/// </summary>
		public BigInteger and(BigInteger @val)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this &amp; ~val)</tt>.
		/// </summary>
		public BigInteger andNot(BigInteger @val)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns the number of bits in the two's complement representation
		/// of this BigInteger that differ from its sign bit.
		/// </summary>
		public int bitCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of bits in the minimal two's-complement
		/// representation of this BigInteger, <i>excluding</i> a sign bit.
		/// </summary>
		public int bitLength()
		{
			return default(int);
		}

		/// <summary>
		/// Returns a BigInteger whose value is equivalent to this BigInteger
		/// with the designated bit cleared.
		/// </summary>
		public BigInteger clearBit(int @n)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Compares this BigInteger with the specified BigInteger.
		/// </summary>
		public int compareTo(BigInteger @val)
		{
			return default(int);
		}

		/// <summary>
		/// Compares this BigInteger with the specified Object.
		/// </summary>
		public int compareTo(object @o)
		{
			return default(int);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this / val)</tt>.
		/// </summary>
		public BigInteger divide(BigInteger @val)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns an array of two BigIntegers containing <tt>(this / val)</tt>
		/// followed by <tt>(this % val)</tt>.
		/// </summary>
		public BigInteger[] divideAndRemainder(BigInteger @val)
		{
			return default(BigInteger[]);
		}

		/// <summary>
		/// Converts this BigInteger to a <code>double</code>.
		/// </summary>
		public override double doubleValue()
		{
			return default(double);
		}

		/// <summary>
		/// Compares this BigInteger with the specified Object for equality.
		/// </summary>
		public override bool Equals(object @x)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a BigInteger whose value is equivalent to this BigInteger
		/// with the designated bit flipped.
		/// </summary>
		public BigInteger flipBit(int @n)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Converts this BigInteger to a <code>float</code>.
		/// </summary>
		public override float floatValue()
		{
			return default(float);
		}

		/// <summary>
		/// Returns a BigInteger whose value is the greatest common divisor of
		/// <tt>abs(this)</tt> and <tt>abs(val)</tt>.
		/// </summary>
		public BigInteger gcd(BigInteger @val)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns the index of the rightmost (lowest-order) one bit in this
		/// BigInteger (the number of zero bits to the right of the rightmost
		/// one bit).
		/// </summary>
		public int getLowestSetBit()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the hash code for this BigInteger.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Converts this BigInteger to an <code>int</code>.
		/// </summary>
		public override int intValue()
		{
			return default(int);
		}

		/// <summary>
		/// Returns <tt>true</tt> if this BigInteger is probably prime,
		/// <tt>false</tt> if it's definitely composite.
		/// </summary>
		public bool isProbablePrime(int @certainty)
		{
			return default(bool);
		}

		/// <summary>
		/// Converts this BigInteger to a <code>long</code>.
		/// </summary>
		public override long longValue()
		{
			return default(long);
		}

		/// <summary>
		/// Returns the maximum of this BigInteger and <tt>val</tt>.
		/// </summary>
		public BigInteger max(BigInteger @val)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns the minimum of this BigInteger and <tt>val</tt>.
		/// </summary>
		public BigInteger min(BigInteger @val)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this mod m</tt>).
		/// </summary>
		public BigInteger mod(BigInteger @m)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this<sup>-1</sup> mod m)</tt>.
		/// </summary>
		public BigInteger modInverse(BigInteger @m)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is
		/// <tt>(this<sup>exponent</sup> mod m)</tt>.
		/// </summary>
		public BigInteger modPow(BigInteger @exponent, BigInteger @m)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this * val)</tt>.
		/// </summary>
		public BigInteger multiply(BigInteger @val)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(-this)</tt>.
		/// </summary>
		public BigInteger negate()
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(~this)</tt>.
		/// </summary>
		public BigInteger not()
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this | val)</tt>.
		/// </summary>
		public BigInteger or(BigInteger @val)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this<sup>exponent</sup>)</tt>.
		/// </summary>
		public BigInteger pow(int @exponent)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a positive BigInteger that is probably prime, with the
		/// specified bitLength.
		/// </summary>
		public BigInteger probablePrime(int @bitLength, Random @rnd)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this % val)</tt>.
		/// </summary>
		public BigInteger remainder(BigInteger @val)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is equivalent to this BigInteger
		/// with the designated bit set.
		/// </summary>
		public BigInteger setBit(int @n)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this &lt;&lt; n)</tt>.
		/// </summary>
		public BigInteger shiftLeft(int @n)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this &gt;&gt; n)</tt>.
		/// </summary>
		public BigInteger shiftRight(int @n)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns the signum function of this BigInteger.
		/// </summary>
		public int signum()
		{
			return default(int);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this - val)</tt>.
		/// </summary>
		public BigInteger subtract(BigInteger @val)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns <tt>true</tt> if and only if the designated bit is set.
		/// </summary>
		public bool testBit(int @n)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a byte array containing the two's-complement
		/// representation of this BigInteger.
		/// </summary>
		public byte[] toByteArray()
		{
			return default(byte[]);
		}

		/// <summary>
		/// Returns the decimal String representation of this BigInteger.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the String representation of this BigInteger in the
		/// given radix.
		/// </summary>
		public  string ToString(int @radix)
		{
			return default(string);
		}

		/// <summary>
		/// Returns a BigInteger whose value is equal to that of the
		/// specified <code>long</code>.
		/// </summary>
		public BigInteger valueOf(long @val)
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns a BigInteger whose value is <tt>(this ^ val)</tt>.
		/// </summary>
		public BigInteger xor(BigInteger @val)
		{
			return default(BigInteger);
		}

	}
}

