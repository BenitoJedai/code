using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.System
{
    [Script(Implements = typeof(global::System.IntPtr))]
    internal class IntPtr
    {

        [Script(OptimizedCode = "return a==b")]
        static public bool operator ==(IntPtr a, IntPtr b)
        {
            return default(bool);
        }

        [Script(OptimizedCode = "return a!=b")]
        static public bool operator !=(IntPtr a, IntPtr b)
        {
            return default(bool);
        }

        public override bool Equals(object obj)
        {
            return this == (IntPtr)obj;
        }

        public override int GetHashCode()
        {
            return default(int);
        }
    }
}