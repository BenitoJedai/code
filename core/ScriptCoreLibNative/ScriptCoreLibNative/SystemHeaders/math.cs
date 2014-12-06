using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    // "X:\opensource\android-ndk-r10c\platforms\android-21\arch-arm64\usr\include\math.h"
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Math.cs

    // should we name it math_h or __math ?
    // should we use interfaces
    // like X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\GLES2\gl2ext.cs

	[Script(IsNative = true, Header = "math.h", IsSystemHeader = true)]
	public static class math
	{
		public static double sin(double e)
		{
			return default(double);
		}

		public static double cos(double e)
		{
			return default(double);
		}
	}

}
