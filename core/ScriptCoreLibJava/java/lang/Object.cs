using ScriptCoreLib;


namespace java.lang
{


    [Script(IsNative = true)]
    public class Object
    {

        [Script(ExternalTarget = "getClass", DefineAsInstance=true)]
        public static Class getClass(object a)
        {
            return default(Class);
        }

    }
}
