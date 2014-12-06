using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;

namespace ScriptCoreLibNative.BCLImplementation.System
{

    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Math.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\SystemHeaders\math.cs

    // https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/math.cs
    // http://referencesource.microsoft.com/#mscorlib/system/math.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Math.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Math.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Math.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Math.cs
    // X:\opensource\ovr_mobile_sdk_20141111\VRLib\jni\LibOVR\Src\Kernel\OVR_Math.cpp

	[Script(Implements = typeof(global::System.Math))]
	internal class __Math
	{
		public static int Sign(double e)
		{
			if (e == 0)
				return 0;

			if (e < 0)
				return -1;

			return 1;
		}

		public static double Sin(double e)
		{
            // could jsc not inline this yet? its 2015 aleady!
			return math.sin(e);
		}

		public static double Cos(double e)
		{
			return math.cos(e);
		}

		public static short Abs(short e)
		{
			if (e < 0)
				return (short)-e;

			return e;
		}
	}

}
