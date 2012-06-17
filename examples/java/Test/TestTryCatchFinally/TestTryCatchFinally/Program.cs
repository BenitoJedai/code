using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]
namespace TestTryCatchFinally
{
	public partial class Program
	{
		static void TestMethod()
		{

		}

		public static void Main(string[] args)
		{

	
			/*
.method private hidebysig static void TryLoopCatchFinally() cil managed
{
    .maxstack 1
    .locals init (
        [0] bool flag)
    L_0000: nop 
    L_0001: call void TestTryCatchFinally.Program::TestMethod()
    L_0006: nop 
    L_0007: br.s L_0039
    L_0009: nop 
    L_000a: call void TestTryCatchFinally.Program::TestMethod()
    L_000f: nop 
    L_0010: nop 
    L_0011: call void TestTryCatchFinally.Program::TestMethod()
    L_0016: nop 
    L_0017: nop 
    L_0018: leave.s L_0025
    L_001a: pop 
    L_001b: nop 
    L_001c: call void TestTryCatchFinally.Program::TestMethod()
    L_0021: nop 
    L_0022: nop 
    L_0023: leave.s L_0025
    L_0025: nop 
    L_0026: leave.s L_0031
    L_0028: nop 
    L_0029: call void TestTryCatchFinally.Program::TestMethod()
    L_002e: nop 
    L_002f: nop 
    L_0030: endfinally 
    L_0031: nop 
    L_0032: call void TestTryCatchFinally.Program::TestMethod()
    L_0037: nop 
    L_0038: nop 
    L_0039: ldc.i4.1 
    L_003a: stloc.0 
    L_003b: br.s L_0009
    .try L_0010 to L_001a catch object handler L_001a to L_0025
    .try L_0010 to L_0028 finally handler L_0028 to L_0031
}
			 */

			TestMethod();

			while (true)
			{
				TestMethod();

				try
				{
					TestMethod();
				}
				catch
				{
					TestMethod();
				}
				finally
				{
					TestMethod();
				}
				TestMethod();

			}

			TestMethod();

			/*
			 * rewritten from leave.s to leave
			 * 
.method private hidebysig static void TryLoopCatchFinally() cil managed
{
    .maxstack 3
    .locals init (
        [0] bool flag)
    L_0000: nop 
    L_0001: call void TestTryCatchFinally.Program::TestMethod()
    L_0006: nop 
    L_0007: br.s L_0042
    L_0009: nop 
    L_000a: call void TestTryCatchFinally.Program::TestMethod()
    L_000f: nop 
    L_0010: nop 
    L_0011: call void TestTryCatchFinally.Program::TestMethod()
    L_0016: nop 
    L_0017: nop 
    L_0018: leave L_002b
    L_001d: pop 
    L_001e: nop 
    L_001f: call void TestTryCatchFinally.Program::TestMethod()
    L_0024: nop 
    L_0025: nop 
    L_0026: leave L_002b
    L_002b: nop 
    L_002c: leave L_003a
    L_0031: nop 
    L_0032: call void TestTryCatchFinally.Program::TestMethod()
    L_0037: nop 
    L_0038: nop 
    L_0039: endfinally 
    L_003a: nop 
    L_003b: call void TestTryCatchFinally.Program::TestMethod()
    L_0040: nop 
    L_0041: nop 
    L_0042: ldc.i4.1 
    L_0043: stloc.0 
    L_0044: br.s L_0009
    .try L_0010 to L_001d catch object handler L_001d to L_002b
    .try L_0010 to L_0031 finally handler L_0031 to L_003a
}

 

 


			 */
		}
	}
}
