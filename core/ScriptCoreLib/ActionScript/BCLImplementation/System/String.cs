using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.String),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.String)
        )]
    internal class __String
    {
        [Script(StringConcatOperator = "+")]
        public static string Concat(object a, object b, object c)
        {
            return default(string);
        }

        #region Concat
        [Script(StringConcatOperator = "+")]
        public static string Concat(object a, object b)
        {
            return default(string);
        }


        [Script(StringConcatOperator = "+")]
        public static string Concat(string a, string b)
        {
            return default(string);
        }

        [Script(StringConcatOperator = "+")]
        public static string Concat(string a, string b, string c)
        {
            return default(string);
        }

        #endregion


        public int Length
        {
            [Script(ExternalTarget = "length")]
            get
            {
                return default(int);
            }
        }


        [Script(ExternalTarget = "toUpperCase")]
        public string ToUpper()
        {
            return default(string);
        }

        [Script(ExternalTarget = "toLowerCase")]
        public string ToLower()
        {
            return default(string);
        }


        [Script(ExternalTarget = "indexOf")]
        public int IndexOf(string str)
        {
            return default(int);
        }

        [Script(ExternalTarget = "lastIndexOf")]
        public int LastIndexOf(string str)
        {
            return default(int);
        }

        [Script(ExternalTarget = "substring")]
        public string Substring(int start)
        {
            return default(string);
        }


        [Script(ExternalTarget = "substring")]
        public string Substring(int start, int length)
        {
            return default(string);
        }

        static RegExp TrimExpCache;

        static public RegExp TrimExp
        {
            get
            {
                if (TrimExpCache == null)
                    TrimExpCache = new RegExp(@"^\s*|\s*$", "g");

                return TrimExpCache;
            }
        }

        [Script(DefineAsStatic = true)]
        public string Trim()
        {
            return ((ActionScript.String)(object)this).replace(TrimExpCache, "");
        }
    }
}
