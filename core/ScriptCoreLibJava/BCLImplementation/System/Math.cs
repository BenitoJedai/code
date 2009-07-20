using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Math))]
	internal class __Math
	{
		// thanks Doug J Reichard! :)

		public static double Floor(double a)
		{
			return global::java.lang.Math.floor(a);
		}

		public static double Sin(double a)
		{
			return global::java.lang.Math.sin(a);
		}

		public static double Cos(double a)
		{
			return global::java.lang.Math.cos(a);
		}

		public static double Tan(double a)
		{
			return global::java.lang.Math.tan(a);
		}
		public static double Pow(double a, double b)
		{
			return global::java.lang.Math.pow(a, b);
		}

		public static double Sqrt(double a)
		{
			return global::java.lang.Math.sqrt(a);
		}


		public static double Abs(double a)
		{
			return global::java.lang.Math.abs(a);
		}

		public static float Abs(float a)
		{
			return global::java.lang.Math.abs(a);
		}

		public static int Abs(int a)
		{
			return global::java.lang.Math.abs(a);
		}

		public static long Abs(long a)
		{
			return global::java.lang.Math.abs(a);
		}

		public static double Acos(double a)
		{
			return global::java.lang.Math.acos(a);
		}

		public static double Asin(double a)
		{
			return global::java.lang.Math.asin(a);
		}

		public static double Atan(double a)
		{
			return global::java.lang.Math.atan(a);
		}
		public static double Atan2(double y, double x)
		{
			return global::java.lang.Math.atan2(y, x);
		}

		public static double Ceiling(double a)
		{
			return global::java.lang.Math.ceil(a);
		}

		public static double Exp(double a)
		{
			return global::java.lang.Math.exp(a);
		}

		public static double IEEEremainder(double f1, double f2)
		{
			return global::java.lang.Math.IEEEremainder(f1, f2);
		}

		public static double Log(double a)
		{
			return global::java.lang.Math.log(a);
		}

		public static double Max(double a, double b)
		{
			return global::java.lang.Math.max(a, b);
		}

		public static float Max(float a, float b)
		{
			return global::java.lang.Math.max(a, b);
		}

		public static int Max(int a, int b)
		{
			return global::java.lang.Math.max(a, b);
		}

		public static long Max(long a, long b)
		{
			return global::java.lang.Math.max(a, b);
		}

		public static double Min(double a, double b)
		{
			return global::java.lang.Math.min(a, b);
		}

		public static float Min(float a, float b)
		{
			return global::java.lang.Math.min(a, b);
		}

		public static int Min(int a, int b)
		{
			return global::java.lang.Math.min(a, b);
		}

		public static long Min(long a, long b)
		{
			return global::java.lang.Math.min(a, b);
		}

		public static double Round(double a)
		{
			return (double)global::java.lang.Math.round(a);
		}

		public static int Round(float a)
		{
			return global::java.lang.Math.round(a);
		}
	}
}
