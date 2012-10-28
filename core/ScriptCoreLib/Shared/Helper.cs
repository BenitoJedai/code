

namespace ScriptCoreLib.Shared
{

    [Script]
    [System.Obsolete]
    public static class Helper
    {
        /// <summary>
        /// date when library was compiled
        /// </summary>
        public static string BuildDateString
        {
            [Script(
                UseCompilerConstants = true,
                OptimizedCode = "return \"{BuildDate} UTC\";"
                )]
            get
            {
                return default(string);
            }
        }

        /// <summary>
        /// date the compiler was compiled
        /// </summary>
        public static string CompilerBuildDateString
        {
            [Script(
                UseCompilerConstants = true,
               OptimizedCode = "return \"{CompilerBuildDate} UTC\";"
                )]
            get
            {
                return default(string);
            }
        }

        public static string FormTemplateID = "Web.Runtime.FormTemplate";
        public static string FormTemplateJSONField = "json_field";


       


        /// <summary>
        /// will invoke a handler with one parameter, or if handler is null, will just return
        /// </summary>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="h"></param>
        /// <param name="x"></param>
        public static TArg Invoke<TArg>(System.Action<TArg> h, TArg x)
        {
            if (h != null)
                h(x);

            return x;
            
        }

        public static void Invoke(System.Action h)
        {
            if (h == null)
                return;

            h();
        }

        public static string Join(string delimit, params object[] e)
        {
            string x = "";

            for (int i = 0; i < e.Length; i++)
            {
                if (i > 0)
                    x += delimit;

                x += e[i];
            }

            return x;
        }

        public static string DefaultString(string def, string e)
        {
            if (e == null)
                return def;

            if (e == "")
                return def;

            return e;
        }

        [Script(OptimizedCode = @"return {arg0} == {arg1};", UseCompilerConstants = true)]
        public static bool VariableEquals<A, B>(A a, B b)
        {
            return default(bool);
        }

        public static bool InvokeTry(System.Action p)
        {
            bool b = true;

            try
            {
                Helper.Invoke(p);
            }
            catch
            {
                b = false;
            }

            return b;
        }


    }
}