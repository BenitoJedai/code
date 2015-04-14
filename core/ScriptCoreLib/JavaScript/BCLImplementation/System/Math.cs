using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
	// FEATURE_CORECLR


	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Math.cs
	// https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/math.cs
	// http://referencesource.microsoft.com/#mscorlib/system/math.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Math.cs

	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Math.cs
	// https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Math.cs
	// https://github.com/sq/JSIL/blob/master/Proxies/Math.cs
	// https://github.com/bridgedotnet/Bridge/blob/master/Bridge/System/Math.cs


	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Math.cs
	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Math.cs
	// X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Math.cs
	// X:\opensource\ovr_mobile_sdk_20141111\VRLib\jni\LibOVR\Src\Kernel\OVR_Math.cpp

	// X:\opensource\github\WootzJs\WootzJs.Runtime\Math.cs

	[Script(Implements = typeof(global::System.Math))]
    internal class __Math
    {
        // inline static methods optimization needed

        [Script(ExternalTarget = "Math")]
        readonly static DOM.IMath m;

        public static double Floor(double d) { return m.floor(d); }
        public static double Ceiling(double d) { return m.ceil(d); }
        public static double Atan(double d) { return m.atan(d); }
        public static double Tan(double d) { return m.tan(d); }
        public static double Cos(double d) { return m.cos(d); }
        public static double Sin(double d) { return m.sin(d); }

        public static double Abs(double e) { return m.abs(e); }
        public static long Abs(long e) { return (long)m.abs(e); }
        public static double Sqrt(double e) { return m.sqrt(e); }
        public static int Abs(int e) { return (int)m.abs(e); }
        public static double Round(double e) { return m.round(e); }


        // like linq to sql Max
        #region Max
        public static byte Max(byte e, byte x) { return (byte)m.max(e, x); }
        public static int Max(int e, int x) { return m.max(e, x); }
        public static ushort Max(ushort e, ushort x) { return m.max(e, x); }
        public static uint Max(uint e, uint x) { return m.max(e, x); }

        // X:\jsc.svn\examples\javascript\io\ZIPDecoderExperiment\ZIPDecoderExperiment\Application.cs

        public static double Max(double e, double x) { return m.max(e, x); }
        public static float Max(float e, float x) { return m.max(e, x); }
        #endregion


        public static byte Min(byte e, byte x) { return (byte)m.min(e, x); }
        public static int Min(int e, int x) { return m.min(e, x); }
        public static double Min(double e, double x) { return m.min(e, x); }
        public static float Min(float e, float x) { return m.min(e, x); }

		// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyProgramsAsLODTiles\ChromeShaderToyProgramsAsLODTiles\Application.cs

		public static int Sign(float d)
		{
			if (d == 0) return 0;
			if (d < 0) return -1;
			return 1;
		}

		public static int Sign(double d)
        {
            if (d == 0) return 0;
            if (d < 0) return -1;
            return 1;
        }

        public static int Sign(int d)
        {
            if (d == 0) return 0;
            if (d < 0) return -1;
            return 1;
        }

        public static double Pow(double e, double x)
        {
            return m.pow(e, x);
        }
    }
}
