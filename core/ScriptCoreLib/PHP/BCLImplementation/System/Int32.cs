using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;
using System.Globalization;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Int32))]
    internal class __Int32
    {
        public class API
        {
            

        }

        public static int Parse(string e)
        {
            return Native.API.intval(e);
        }


		public static int Parse(string s, NumberStyles style)
		{
			if (style == NumberStyles.HexNumber)
				return Native.API.intval(s, 16);

			return Native.API.intval(s);
		}
    }
}
