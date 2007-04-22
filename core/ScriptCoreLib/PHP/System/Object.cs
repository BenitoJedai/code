using ScriptCoreLib.PHP;

namespace ScriptCoreLib.PHP.System
{
    [Script(Implements = typeof(global::System.Object))]
    internal class Object
    {


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