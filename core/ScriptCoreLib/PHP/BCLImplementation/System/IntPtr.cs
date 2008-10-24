using ScriptCoreLib.PHP;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.IntPtr))]
    internal class __IntPtr
    {

        [Script(OptimizedCode = "return $a==$b;")]
		static public bool operator ==(__IntPtr a, __IntPtr b)
        {
            return default(bool);
        }

        [Script(OptimizedCode = "return $a!=$b;")]
		static public bool operator !=(__IntPtr a, __IntPtr b)
        {
            return default(bool);
        }

        public override bool Equals(object obj)
        {
			return this == (__IntPtr)obj;
        }

        public override int GetHashCode()
        {
            return default(int);
        }
    }
}