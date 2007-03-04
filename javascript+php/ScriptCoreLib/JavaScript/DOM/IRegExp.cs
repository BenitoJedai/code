using ScriptCoreLib.JavaScript;


namespace ScriptCoreLib.JavaScript.DOM
{
    /// <summary>
    /// http://www.cuneytyilmaz.com/prog/jrx/
    /// http://www.regular-expressions.info/examples.html
    /// http://www.regular-expressions.info/javascriptexample.html
    /// </summary>
    [Script(InternalConstructor=true)]
    public class IRegExp
    {
        #region ctor
        public IRegExp(string e)
        {

        }

        [Script(OptimizedCode="return new RegExp(e);")]
        internal static IRegExp InternalConstructor(string e)
        {
            return default(IRegExp);
        }

        public IRegExp(string e, string mod)
        {

        }

        [Script(OptimizedCode = "return new RegExp(e, mod);")]
        internal static IRegExp InternalConstructor(string e, string mod)
        {
            return default(IRegExp);
        }
        #endregion

        public string[] exec(string e)
        {
            return default(string[]);
        }

        public T[] exec<T>(T e)
        {
            return default(T[]);
        }

        [Script(OptimizedCode=@"return e.replace(r, v);")]
        public static T replace<T>(IRegExp r, T e, string v)
        {
            return default(T);
        }

        [Script(DefineAsStatic=true)]
        public T replace<T>(T e, string v)
        {
            return replace(this, e, v);
        }


        static public IRegExp Trim { get { return new IRegExp(@"^\s*|\s*$", "g"); } }
        static public IRegExp Integer { get { return new IRegExp(@"^\d+$"); } }
        static public IRegExp Currency { get { return new IRegExp(@"^[0-9]{1,3}(?:,?[0-9]{3})*(?:\.[0-9]{2})?$"); } }

        [Script(DefineAsStatic=true)]
        public string[][] ExecToArray(string v)
        {
            IArray<string[]> a = new IArray<string[]>();

            string[] p = exec(v);

            while (p != null && a.length < 80)
            {
                a.push(p);

                p = exec(v);
            }

            return a;
        }

        public static string[] ExecToArray(string regex, string v, int iGroup)
        {
            return new IRegExp(regex, "g").ExecToArray(v, iGroup);
        }

        [Script(DefineAsStatic = true)]
        public string[] ExecToArray(string v, int iGroup)
        {
            IArray<string> a = new IArray<string>();

            string[] p = exec(v);

            while (p != null && a.length < 80)
            {
                a.push(p[iGroup]);

                p = exec(v);
            }

            return a;
        }
    }
}
