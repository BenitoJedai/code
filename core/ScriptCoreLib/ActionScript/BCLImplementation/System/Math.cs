using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.ConstrainedExecution;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
	// 
	[Script(Implements = typeof(global::System.Math))]
	internal class __Math
	{
		// Summary:
		//     Returns the absolute value of a System.Decimal number.
		//
		// Parameters:
		//   value:
		//     A number in the range System.Decimal.MinValue≤ value ≤System.Decimal.MaxValue.
		//
		// Returns:
		//     A System.Decimal, x, such that 0 ≤ x ≤System.Decimal.MaxValue.
		public static decimal Abs(decimal value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the absolute value of a double-precision floating-point number.
		//
		// Parameters:
		//   value:
		//     A number in the range System.Double.MinValue≤value≤System.Double.MaxValue.
		//
		// Returns:
		//     A double-precision floating-point number, x, such that 0 ≤ x ≤System.Double.MaxValue.
		public static double Abs(double value) { return Math.abs(value); }
		//
		// Summary:
		//     Returns the absolute value of a single-precision floating-point number.
		//
		// Parameters:
		//   value:
		//     A number in the range System.Single.MinValue≤value≤System.Single.MaxValue.
		//
		// Returns:
		//     A single-precision floating-point number, x, such that 0 ≤ x ≤System.Single.MaxValue.
		public static float Abs(float value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the absolute value of a 32-bit signed integer.
		//
		// Parameters:
		//   value:
		//     A number in the range System.Int32.MinValue < value≤System.Int32.MaxValue.
		//
		// Returns:
		//     A 32-bit signed integer, x, such that 0 ≤ x ≤System.Int32.MaxValue.
		//
		// Exceptions:
		//   System.OverflowException:
		//     value equals System.Int32.MinValue.
		public static int Abs(int value) { return Convert.ToInt32(Math.abs(value)); }
		//
		// Summary:
		//     Returns the absolute value of a 64-bit signed integer.
		//
		// Parameters:
		//   value:
		//     A number in the range System.Int64.MinValue < value≤System.Int64.MaxValue.
		//
		// Returns:
		//     A 64-bit signed integer, x, such that 0 ≤ x ≤System.Int64.MaxValue.
		//
		// Exceptions:
		//   System.OverflowException:
		//     value equals System.Int64.MinValue.
		public static long Abs(long value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the absolute value of an 8-bit signed integer.
		//
		// Parameters:
		//   value:
		//     A number in the range System.SByte.MinValue < value≤System.SByte.MaxValue.
		//
		// Returns:
		//     An 8-bit signed integer, x, such that 0 ≤ x ≤System.SByte.MaxValue.
		//
		// Exceptions:
		//   System.OverflowException:
		//     value equals System.SByte.MinValue.
		[CLSCompliant(false)]
		public static sbyte Abs(sbyte value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the absolute value of a 16-bit signed integer.
		//
		// Parameters:
		//   value:
		//     A number in the range System.Int16.MinValue < value≤System.Int16.MaxValue.
		//
		// Returns:
		//     A 16-bit signed integer, x, such that 0 ≤ x ≤System.Int16.MaxValue.
		//
		// Exceptions:
		//   System.OverflowException:
		//     value equals System.Int16.MinValue.
		public static short Abs(short value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the angle whose cosine is the specified number.
		//
		// Parameters:
		//   d:
		//     A number representing a cosine, where -1 ≤d≤ 1.
		//
		// Returns:
		//     An angle, θ, measured in radians, such that 0 ≤θ≤π-or- System.Double.NaN
		//     if d < -1 or d > 1.
		public static double Acos(double d) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the angle whose sine is the specified number.
		//
		// Parameters:
		//   d:
		//     A number representing a sine, where -1 ≤d≤ 1.
		//
		// Returns:
		//     An angle, θ, measured in radians, such that -π/2 ≤θ≤π/2 -or- System.Double.NaN
		//     if d < -1 or d > 1.
		public static double Asin(double d) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the angle whose tangent is the specified number.
		//
		// Parameters:
		//   d:
		//     A number representing a tangent.
		//
		// Returns:
		//     An angle, θ, measured in radians, such that -π/2 ≤θ≤π/2.-or- System.Double.NaN
		//     if d equals System.Double.NaN, -π/2 rounded to double precision (-1.5707963267949)
		//     if d equals System.Double.NegativeInfinity, or π/2 rounded to double precision
		//     (1.5707963267949) if d equals System.Double.PositiveInfinity.
		public static double Atan(double d) { return Math.atan(d); }
		//
		// Summary:
		//     Returns the angle whose tangent is the quotient of two specified numbers.
		//
		// Parameters:
		//   y:
		//     The y coordinate of a point.
		//
		//   x:
		//     The x coordinate of a point.
		//
		// Returns:
		//     An angle, θ, measured in radians, such that -π≤θ≤π, and tan(θ) = y / x, where
		//     (x, y) is a point in the Cartesian plane. Observe the following: For (x,
		//     y) in quadrant 1, 0 < θ < π/2.For (x, y) in quadrant 2, π/2 < θ≤π.For (x,
		//     y) in quadrant 3, -π < θ < -π/2.For (x, y) in quadrant 4, -π/2 < θ < 0.For
		//     points on the boundaries of the quadrants, the return value is the following:If
		//     x is 0 and y is not negative, θ = 0.If x is 0 and y is negative, θ = π.If
		//     x is positive and y is 0, θ = π/2.If x is negative and y is 0, θ = -π/2.
		public static double Atan2(double y, double x) { return Math.atan2(y, x); }
		//
		// Summary:
		//     Produces the full product of two 32-bit numbers.
		//
		// Parameters:
		//   a:
		//     The first System.Int32 to multiply.
		//
		//   b:
		//     The second System.Int32 to multiply.
		//
		// Returns:
		//     The System.Int64 containing the product of the specified numbers.
		public static long BigMul(int a, int b) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the smallest integer greater than or equal to the specified decimal
		//     number.
		//
		// Parameters:
		//   d:
		//     A decimal number.
		//
		// Returns:
		//     The smallest integer greater than or equal to d.
		public static decimal Ceiling(decimal d) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the smallest integer greater than or equal to the specified double-precision
		//     floating-point number.
		//
		// Parameters:
		//   a:
		//     A double-precision floating-point number.
		//
		// Returns:
		//     The smallest integer greater than or equal to a. If a is equal to System.Double.NaN,
		//     System.Double.NegativeInfinity, or System.Double.PositiveInfinity, that value
		//     is returned.
		public static double Ceiling(double a) { return Math.ceil(a); }
		//
		// Summary:
		//     Returns the cosine of the specified angle.
		//
		// Parameters:
		//   d:
		//     An angle, measured in radians.
		//
		// Returns:
		//     The cosine of d.
		public static double Cos(double d) { return Math.cos(d); }
		//
		// Summary:
		//     Returns the hyperbolic cosine of the specified angle.
		//
		// Parameters:
		//   value:
		//     An angle, measured in radians.
		//
		// Returns:
		//     The hyperbolic cosine of value. If value is equal to System.Double.NegativeInfinity
		//     or System.Double.PositiveInfinity, System.Double.PositiveInfinity is returned.
		//     If value is equal to System.Double.NaN, System.Double.NaN is returned.
		public static double Cosh(double value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Calculates the quotient of two 32-bit signed integers and also returns the
		//     remainder in an output parameter.
		//
		// Parameters:
		//   a:
		//     The System.Int32 that contains the dividend.
		//
		//   b:
		//     The System.Int32 that contains the divisor.
		//
		//   result:
		//     The System.Int32 that receives the remainder.
		//
		// Returns:
		//     The System.Int32 containing the quotient of the specified numbers.
		//
		// Exceptions:
		//   System.DivideByZeroException:
		//     b is zero.
		//public static int DivRem(int a, int b, out int result) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Calculates the quotient of two 64-bit signed integers and also returns the
		//     remainder in an output parameter.
		//
		// Parameters:
		//   a:
		//     The System.Int64 that contains the dividend.
		//
		//   b:
		//     The System.Int64 that contains the divisor.
		//
		//   result:
		//     The System.Int64 that receives the remainder.
		//
		// Returns:
		//     The System.Int64 containing the quotient of the specified numbers.
		//
		// Exceptions:
		//   System.DivideByZeroException:
		//     b is zero.
		//public static long DivRem(long a, long b, out long result) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns e raised to the specified power.
		//
		// Parameters:
		//   d:
		//     A number specifying a power.
		//
		// Returns:
		//     The number e raised to the power d. If d equals System.Double.NaN or System.Double.PositiveInfinity,
		//     that value is returned. If d equals System.Double.NegativeInfinity, 0 is
		//     returned.
		public static double Exp(double d) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the largest integer less than or equal to the specified decimal number.
		//
		// Parameters:
		//   d:
		//     A decimal number.
		//
		// Returns:
		//     The largest integer less than or equal to d.
		public static decimal Floor(decimal d) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the largest integer less than or equal to the specified double-precision
		//     floating-point number.
		//
		// Parameters:
		//   d:
		//     A double-precision floating-point number.
		//
		// Returns:
		//     The largest integer less than or equal to d. If d is equal to System.Double.NaN,
		//     System.Double.NegativeInfinity, or System.Double.PositiveInfinity, that value
		//     is returned.
		public static double Floor(double d) { return Math.floor(d); }
		//
		// Summary:
		//     Returns the remainder resulting from the division of a specified number by
		//     another specified number.
		//
		// Parameters:
		//   x:
		//     A dividend.
		//
		//   y:
		//     A divisor.
		//
		// Returns:
		//     A number equal to x - (y Q), where Q is the quotient of x / y rounded to
		//     the nearest integer (if x / y falls halfway between two integers, the even
		//     integer is returned).If x - (y Q) is zero, the value +0 is returned if x
		//     is positive, or -0 if x is negative.If y = 0, System.Double.NaN (Not-A-Number)
		//     is returned.
		public static double IEEERemainder(double x, double y) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the natural (base e) logarithm of a specified number.
		//
		// Parameters:
		//   d:
		//     A number whose logarithm is to be found.
		//
		// Returns:
		//     Sign of dReturns Positive The natural logarithm of d { throw new NotImplementedException(); } that is, ln d, or log
		//     edZero System.Double.NegativeInfinityNegative System.Double.NaNIf d is equal
		//     to System.Double.NaN, returns System.Double.NaN. If d is equal to System.Double.PositiveInfinity,
		//     returns System.Double.PositiveInfinity.
		public static double Log(double d) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the logarithm of a specified number in a specified base.
		//
		// Parameters:
		//   a:
		//     A number whose logarithm is to be found.
		//
		//   newBase:
		//     The base of the logarithm.
		//
		// Returns:
		//     In the following table +Infinity denotes System.Double.PositiveInfinity,
		//     -Infinity denotes System.Double.NegativeInfinity, and NaN denotes System.Double.NaN.anewBaseReturn
		//     Valuea> 0(0 <newBase< 1) -or-(newBase> 1)lognewBase(a)a< 0(any value)NaN(any
		//     value)newBase< 0NaNa != 1newBase = 0NaNa != 1newBase = +InfinityNaNa = NaN(any
		//     value)NaN(any value)newBase = NaNNaN(any value)newBase = 1NaNa = 00 <newBase<
		//     1 +Infinitya = 0newBase> 1-Infinitya = +Infinity0 <newBase< 1-Infinitya =
		//     +InfinitynewBase> 1+Infinitya = 1newBase = 00a = 1newBase = +Infinity0
		public static double Log(double a, double newBase) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the base 10 logarithm of a specified number.
		//
		// Parameters:
		//   d:
		//     A number whose logarithm is to be found.
		//
		// Returns:
		//     Sign of dReturns Positive The base 10 log of d { throw new NotImplementedException(); } that is, log 10d. Zero System.Double.NegativeInfinityNegative
		//     System.Double.NaNIf d is equal to System.Double.NaN, this method returns
		//     System.Double.NaN. If d is equal to System.Double.PositiveInfinity, this
		//     method returns System.Double.PositiveInfinity.
		public static double Log10(double d) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the larger of two 8-bit unsigned integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 8-bit unsigned integers to compare.
		//
		//   val2:
		//     The second of two 8-bit unsigned integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is larger.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static byte Max(byte val1, byte val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the larger of two decimal numbers.
		//
		// Parameters:
		//   val1:
		//     The first of two System.Decimal numbers to compare.
		//
		//   val2:
		//     The second of two System.Decimal numbers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is larger.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static decimal Max(decimal val1, decimal val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the larger of two double-precision floating-point numbers.
		//
		// Parameters:
		//   val1:
		//     The first of two double-precision floating-point numbers to compare.
		//
		//   val2:
		//     The second of two double-precision floating-point numbers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is larger. If val1, val2, or both val1
		//     and val2 are equal to System.Double.NaN, System.Double.NaN is returned.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static double Max(double val1, double val2)
		{
			if (val1 > val2)
				return val1;

			return val2;
		}
		//
		// Summary:
		//     Returns the larger of two single-precision floating-point numbers.
		//
		// Parameters:
		//   val1:
		//     The first of two single-precision floating-point numbers to compare.
		//
		//   val2:
		//     The second of two single-precision floating-point numbers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is larger. If val1, or val2, or both val1
		//     and val2 are equal to System.Single.NaN, System.Single.NaN is returned.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static float Max(float val1, float val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the larger of two 32-bit signed integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 32-bit signed integers to compare.
		//
		//   val2:
		//     The second of two 32-bit signed integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is larger.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Max(int val1, int val2)
		{
			if (val1 > val2)
				return val1;

			return val2;
		}
		//
		// Summary:
		//     Returns the larger of two 64-bit signed integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 64-bit signed integers to compare.
		//
		//   val2:
		//     The second of two 64-bit signed integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is larger.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static long Max(long val1, long val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the larger of two 8-bit signed integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 8-bit signed integers to compare.
		//
		//   val2:
		//     The second of two 8-bit signed integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is larger.
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static sbyte Max(sbyte val1, sbyte val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the larger of two 16-bit signed integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 16-bit signed integers to compare.
		//
		//   val2:
		//     The second of two 16-bit signed integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is larger.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static short Max(short val1, short val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the larger of two 32-bit unsigned integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 32-bit unsigned integers to compare.
		//
		//   val2:
		//     The second of two 32-bit unsigned integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is larger.
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static uint Max(uint val1, uint val2)
		{
			if (val1 > val2)
				return val1;

			return val2;
		}
		//
		// Summary:
		//     Returns the larger of two 64-bit unsigned integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 64-bit unsigned integers to compare.
		//
		//   val2:
		//     The second of two 64-bit unsigned integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is larger.
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static ulong Max(ulong val1, ulong val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the larger of two 16-bit unsigned integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 16-bit unsigned integers to compare.
		//
		//   val2:
		//     The second of two 16-bit unsigned integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is larger.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		public static ushort Max(ushort val1, ushort val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the smaller of two 8-bit unsigned integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 8-bit unsigned integers to compare.
		//
		//   val2:
		//     The second of two 8-bit unsigned integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is smaller.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static byte Min(byte val1, byte val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the smaller of two decimal numbers.
		//
		// Parameters:
		//   val1:
		//     The first of two System.Decimal numbers to compare.
		//
		//   val2:
		//     The second of two System.Decimal numbers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is smaller.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static decimal Min(decimal val1, decimal val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the smaller of two double-precision floating-point numbers.
		//
		// Parameters:
		//   val1:
		//     The first of two double-precision floating-point numbers to compare.
		//
		//   val2:
		//     The second of two double-precision floating-point numbers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is smaller. If val1, val2, or both val1
		//     and val2 are equal to System.Double.NaN, System.Double.NaN is returned.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static double Min(double val1, double val2)
		{
			if (val1 < val2)
				return val1;

			return val2;
		}
		//
		// Summary:
		//     Returns the smaller of two single-precision floating-point numbers.
		//
		// Parameters:
		//   val1:
		//     The first of two single-precision floating-point numbers to compare.
		//
		//   val2:
		//     The second of two single-precision floating-point numbers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is smaller. If val1, val2, or both val1
		//     and val2 are equal to System.Single.NaN, System.Single.NaN is returned.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static float Min(float val1, float val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the smaller of two 32-bit signed integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 32-bit signed integers to compare.
		//
		//   val2:
		//     The second of two 32-bit signed integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is smaller.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Min(int val1, int val2)
		{
			if (val1 < val2)
				return val1;

			return val2;

		}
		//
		// Summary:
		//     Returns the smaller of two 64-bit signed integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 64-bit signed integers to compare.
		//
		//   val2:
		//     The second of two 64-bit signed integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is smaller.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static long Min(long val1, long val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the smaller of two 8-bit signed integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 8-bit signed integers to compare.
		//
		//   val2:
		//     The second of two 8-bit signed integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is smaller.
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static sbyte Min(sbyte val1, sbyte val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the smaller of two 16-bit signed integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 16-bit signed integers to compare.
		//
		//   val2:
		//     The second of two 16-bit signed integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is smaller.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static short Min(short val1, short val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the smaller of two 32-bit unsigned integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 32-bit unsigned integers to compare.
		//
		//   val2:
		//     The second of two 32-bit unsigned integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is smaller.
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static uint Min(uint val1, uint val2)
		{
			if (val1 < val2)
				return val1;

			return val2;

		}
		//
		// Summary:
		//     Returns the smaller of two 64-bit unsigned integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 64-bit unsigned integers to compare.
		//
		//   val2:
		//     The second of two 64-bit unsigned integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is smaller.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		public static ulong Min(ulong val1, ulong val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the smaller of two 16-bit unsigned integers.
		//
		// Parameters:
		//   val1:
		//     The first of two 16-bit unsigned integers to compare.
		//
		//   val2:
		//     The second of two 16-bit unsigned integers to compare.
		//
		// Returns:
		//     Parameter val1 or val2, whichever is smaller.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		public static ushort Min(ushort val1, ushort val2) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns a specified number raised to the specified power.
		//
		// Parameters:
		//   x:
		//     A double-precision floating-point number to be raised to a power.
		//
		//   y:
		//     A double-precision floating-point number that specifies a power.
		//
		// Returns:
		//     The number x raised to the power y.The following table indicates the return
		//     value when various values or ranges of values are specified for the x and
		//     y parameters. For more information, see System.Double.PositiveInfinity, System.Double.NegativeInfinity,
		//     and System.Double.NaN.Parameters Return Value x or y = NaN NaN x = Any value
		//     except NaN { throw new NotImplementedException(); } y = 0 1 x = NegativeInfinity { throw new NotImplementedException(); } y < 0 0 x = NegativeInfinity { throw new NotImplementedException(); } y
		//     is a positive odd integer NegativeInfinity x = NegativeInfinity { throw new NotImplementedException(); } y is positive
		//     but not an odd integer PositiveInfinity x < 0 but not NegativeInfinity { throw new NotImplementedException(); } y
		//     is not an integer, NegativeInfinity, or PositiveInfinityNaN x = -1 { throw new NotImplementedException(); } y = NegativeInfinity
		//     or PositiveInfinity NaN -1 < x < 1 { throw new NotImplementedException(); } y = NegativeInfinity PositiveInfinity
		//     -1 < x < 1 { throw new NotImplementedException(); } y = PositiveInfinity 0 x < -1 or x > 1 { throw new NotImplementedException(); } y = NegativeInfinity
		//     0 x < -1 or x > 1 { throw new NotImplementedException(); } y = PositiveInfinity PositiveInfinity x = 0 { throw new NotImplementedException(); } y < 0 PositiveInfinity
		//     x = 0 { throw new NotImplementedException(); } y > 0 0 x = 1 { throw new NotImplementedException(); } y is any value except NaN 1 x = PositiveInfinity { throw new NotImplementedException(); } y
		//     < 0 0 x = PositiveInfinity { throw new NotImplementedException(); } y > 0 PositiveInfinity
		public static double Pow(double x, double y) { return global::ScriptCoreLib.ActionScript.Math.pow(x, y); }
		//
		// Summary:
		//     Rounds a decimal value to the nearest integer.
		//
		// Parameters:
		//   d:
		//     A decimal number to be rounded.
		//
		// Returns:
		//     The integer nearest parameter d. If the fractional component of d is halfway
		//     between two integers, one of which is even and the other odd, then the even
		//     number is returned.
		//
		// Exceptions:
		//   System.OverflowException:
		//     The result is outside the range of a System.Decimal.
		public static decimal Round(decimal d) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Rounds a double-precision floating-point value to the nearest integer.
		//
		// Parameters:
		//   a:
		//     A double-precision floating-point number to be rounded.
		//
		// Returns:
		//     The integer nearest a. If the fractional component of a is halfway between
		//     two integers, one of which is even and the other odd, then the even number
		//     is returned.
		public static double Round(double a) { return Math.round(a); }
		//
		// Summary:
		//     Rounds a decimal value to a specified precision.
		//
		// Parameters:
		//   d:
		//     A decimal number to be rounded.
		//
		//   decimals:
		//     The number of decimal places (precision) in the return value.
		//
		// Returns:
		//     The number nearest d with a precision equal to decimals.
		//
		// Exceptions:
		//   System.ArgumentOutOfRangeException:
		//     decimals is less than 0 or greater than 28.
		//
		//   System.OverflowException:
		//     The result is outside the range of a System.Decimal.
		public static decimal Round(decimal d, int decimals) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Rounds a decimal value to the nearest integer. A parameter specifies how
		//     to round the value if it is midway between two other numbers.
		//
		// Parameters:
		//   d:
		//     A decimal number to be rounded.
		//
		//   mode:
		//     Specification for how to round d if it is midway between two other numbers.
		//
		// Returns:
		//     The integer nearest d. If d is halfway between two numbers, one of which
		//     is even and the other odd, then mode determines which of the two is returned.
		//
		// Exceptions:
		//   System.ArgumentException:
		//     mode is not a valid value of System.MidpointRounding.
		//
		//   System.OverflowException:
		//     The result is outside the range of a System.Decimal.
		public static decimal Round(decimal d, MidpointRounding mode) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Rounds a double-precision floating-point value to the specified precision.
		//
		// Parameters:
		//   value:
		//     A double-precision floating-point number to be rounded.
		//
		//   digits:
		//     The number of fractional digits (precision) in the return value.
		//
		// Returns:
		//     The number nearest value with a precision equal to digits.
		//
		// Exceptions:
		//   System.ArgumentOutOfRangeException:
		//     digits is less than 0 or greater than 15.
		public static double Round(double value, int digits) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Rounds a double-precision floating-point value to the nearest integer. A
		//     parameter specifies how to round the value if it is midway between two other
		//     numbers.
		//
		// Parameters:
		//   value:
		//     A double-precision floating-point number to be rounded.
		//
		//   mode:
		//     Specification for how to round value if it is midway between two other numbers.
		//
		// Returns:
		//     The integer nearest value. If value is halfway between two integers, one
		//     of which is even and the other odd, then mode determines which of the two
		//     is returned.
		//
		// Exceptions:
		//   System.ArgumentException:
		//     mode is not a valid value of System.MidpointRounding.
		public static double Round(double value, MidpointRounding mode) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Rounds a decimal value to a specified precision. A parameter specifies how
		//     to round the value if it is midway between two other numbers.
		//
		// Parameters:
		//   d:
		//     A decimal number to be rounded.
		//
		//   decimals:
		//     The number of decimal places (precision) in the return value.
		//
		//   mode:
		//     Specification for how to round d if it is midway between two other numbers.
		//
		// Returns:
		//     The number nearest d with a precision equal to decimals. If d is halfway
		//     between two numbers, one of which is even and the other odd, then mode determines
		//     which of the two numbers is returned. If the precision of d is less than
		//     decimals, then d is returned unchanged.
		//
		// Exceptions:
		//   System.ArgumentOutOfRangeException:
		//     decimals is less than 0 or greater than 28.
		//
		//   System.ArgumentException:
		//     mode is not a valid value of System.MidpointRounding.
		//
		//   System.OverflowException:
		//     The result is outside the range of a System.Decimal.
		public static decimal Round(decimal d, int decimals, MidpointRounding mode) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Rounds a double-precision floating-point value to the specified precision.
		//     A parameter specifies how to round the value if it is midway between two
		//     other numbers.
		//
		// Parameters:
		//   value:
		//     A double-precision floating-point number to be rounded.
		//
		//   digits:
		//     The number of fractional digits (precision) in the return value.
		//
		//   mode:
		//     Specification for how to round value if it is midway between two other numbers.
		//
		// Returns:
		//     The number nearest value with a precision equal to digits. If value is halfway
		//     between two numbers, one of which is even and the other odd, then the mode
		//     parameter determines which number is returned. If the precision of value
		//     is less than digits, then value is returned unchanged.
		//
		// Exceptions:
		//   System.ArgumentOutOfRangeException:
		//     digits is less than 0 or greater than 15.
		//
		//   System.ArgumentException:
		//     mode is not a valid value of System.MidpointRounding.
		public static double Round(double value, int digits, MidpointRounding mode) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns a value indicating the sign of a decimal number.
		//
		// Parameters:
		//   value:
		//     A signed System.Decimal number.
		//
		// Returns:
		//     A number indicating the sign of value.Number Description -1 value is less
		//     than zero. 0 value is equal to zero. 1 value is greater than zero.
		public static int Sign(decimal value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns a value indicating the sign of a double-precision floating-point
		//     number.
		//
		// Parameters:
		//   value:
		//     A signed number.
		//
		// Returns:
		//     A number indicating the sign of value.Number Description -1 value is less
		//     than zero. 0 value is equal to zero. 1 value is greater than zero.
		//
		// Exceptions:
		//   System.ArithmeticException:
		//     value is equal to System.Double.NaN.
		public static int Sign(double value)
		{
			if (value < 0)
				return -1;
			if (value > 0)
				return 1;
			return 0;
		}
		//
		// Summary:
		//     Returns a value indicating the sign of a single-precision floating-point
		//     number.
		//
		// Parameters:
		//   value:
		//     A signed number.
		//
		// Returns:
		//     A number indicating the sign of value.Number Description -1 value is less
		//     than zero. 0 value is equal to zero. 1 value is greater than zero.
		//
		// Exceptions:
		//   System.ArithmeticException:
		//     value is equal to System.Single.NaN.
		public static int Sign(float value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns a value indicating the sign of a 32-bit signed integer.
		//
		// Parameters:
		//   value:
		//     A signed number.
		//
		// Returns:
		//     A number indicating the sign of value.Number Description -1 value is less
		//     than zero. 0 value is equal to zero. 1 value is greater than zero.
		public static int Sign(int value)
		{
			if (value < 0)
				return -1;
			if (value > 0)
				return 1;
			return 0;
		}
		//
		// Summary:
		//     Returns a value indicating the sign of a 64-bit signed integer.
		//
		// Parameters:
		//   value:
		//     A signed number.
		//
		// Returns:
		//     A number indicating the sign of value.Number Description -1 value is less
		//     than zero. 0 value is equal to zero. 1 value is greater than zero.
		public static int Sign(long value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns a value indicating the sign of an 8-bit signed integer.
		//
		// Parameters:
		//   value:
		//     A signed number.
		//
		// Returns:
		//     A number indicating the sign of value.Number Description -1 value is less
		//     than zero. 0 value is equal to zero. 1 value is greater than zero.
		[CLSCompliant(false)]
		public static int Sign(sbyte value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns a value indicating the sign of a 16-bit signed integer.
		//
		// Parameters:
		//   value:
		//     A signed number.
		//
		// Returns:
		//     A number indicating the sign of value.Number Description -1 value is less
		//     than zero. 0 value is equal to zero. 1 value is greater than zero.
		public static int Sign(short value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the sine of the specified angle.
		//
		// Parameters:
		//   a:
		//     An angle, measured in radians.
		//
		// Returns:
		//     The sine of a. If a is equal to System.Double.NaN, System.Double.NegativeInfinity,
		//     or System.Double.PositiveInfinity, this method returns System.Double.NaN.
		public static double Sin(double a) { return Math.sin(a); }
		//
		// Summary:
		//     Returns the hyperbolic sine of the specified angle.
		//
		// Parameters:
		//   value:
		//     An angle, measured in radians.
		//
		// Returns:
		//     The hyperbolic sine of value. If value is equal to System.Double.NegativeInfinity,
		//     System.Double.PositiveInfinity, or System.Double.NaN, this method returns
		//     a System.Double equal to value.
		public static double Sinh(double value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Returns the square root of a specified number.
		//
		// Parameters:
		//   d:
		//     A number.
		//
		// Returns:
		//     Value of dReturns Zero, or positive The positive square root of d. Negative
		//     System.Double.NaNIf d is equal to System.Double.NaN or System.Double.PositiveInfinity,
		//     that value is returned.
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static double Sqrt(double d) { return global::ScriptCoreLib.ActionScript.Math.sqrt(d); }
		//
		// Summary:
		//     Returns the tangent of the specified angle.
		//
		// Parameters:
		//   a:
		//     An angle, measured in radians.
		//
		// Returns:
		//     The tangent of a. If a is equal to System.Double.NaN, System.Double.NegativeInfinity,
		//     or System.Double.PositiveInfinity, this method returns System.Double.NaN.
		public static double Tan(double a) { return global::ScriptCoreLib.ActionScript.Math.tan(a); }
		//
		// Summary:
		//     Returns the hyperbolic tangent of the specified angle.
		//
		// Parameters:
		//   value:
		//     An angle, measured in radians.
		//
		// Returns:
		//     The hyperbolic tangent of value. If value is equal to System.Double.NegativeInfinity,
		//     this method returns -1. If value is equal to System.Double.PositiveInfinity,
		//     this method returns 1. If value is equal to System.Double.NaN, this method
		//     returns System.Double.NaN.
		public static double Tanh(double value) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Calculates the integral part of a specified decimal number.
		//
		// Parameters:
		//   d:
		//     A number to truncate.
		//
		// Returns:
		//     The integral part of d { throw new NotImplementedException(); } that is, the number that remains after any fractional
		//     digits have been discarded.
		public static decimal Truncate(decimal d) { throw new NotImplementedException(); }
		//
		// Summary:
		//     Calculates the integral part of a specified double-precision floating-point
		//     number.
		//
		// Parameters:
		//   d:
		//     A number to truncate.
		//
		// Returns:
		//     The integral part of d { throw new NotImplementedException(); } that is, the number that remains after any fractional
		//     digits have been discarded.
		public static double Truncate(double d) { throw new NotImplementedException(); }
	}
}
