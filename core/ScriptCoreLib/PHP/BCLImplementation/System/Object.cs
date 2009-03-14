using ScriptCoreLib.PHP;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Object))]
    internal class __Object
    {

		[Script(OptimizedCode = "return {arg0} === {arg0};", UseCompilerConstants = true)]
		public static bool ReferenceEquals(object a, object b)
		{
			return default(bool);
		}


        public new virtual bool Equals(object obj)
        {
            return this == obj;
        }

        public new virtual int GetHashCode()
        {
            return default(int);
        }
    }
}