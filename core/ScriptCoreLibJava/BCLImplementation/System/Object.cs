using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
//using javax.common.runtime;
using System;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/object.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Object.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Object.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Object.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Object.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Object.cs

	[Script(

		Implements = typeof(global::System.Object),
		ImplementationType = typeof(global::java.lang.Object)

		//Implements = typeof(global::System.Object),
		//ImplementationType = typeof(object)
		
		)]
	internal class __Object
	{
        // https://software.intel.com/en-us/blogs/2014/07/23/will-my-android-app-still-run-with-art-instead-of-dalvik
        // Don't attempt to look at the fields of  Object, since it now has private fields. 


        /* is this required anymore?
		[Script(ExternalTarget = "toString")]
		public new string ToString()
		{
			return default(string);
		}
         * */

		[Script(DefineAsStatic = true)]
		new public Type GetType()
		{

			return __Type.GetTypeFromValue(this);
		}

        public static bool ReferenceEquals(object objA, object objB)
        {
            return objA == objB;
        }
	}


}
