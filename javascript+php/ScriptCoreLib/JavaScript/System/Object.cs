using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.System
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

        public new virtual string ToString()
        {
            return default(string);
        }
    }
}