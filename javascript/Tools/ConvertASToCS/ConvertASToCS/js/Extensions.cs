using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace ConvertASToCS.js
{
    [Script]
    static class Extensions
    {
        public static bool ContainsAny(this string e, params string[] x)
        {
            var r = false;

            foreach (var i in x)
            {
                if (e.Contains(i))
                {
                    r = true;
                    break;
                }
            }

            return r;
        }
        public static IHTMLInput ToCheckBox(this string e)
        {
            return new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox) { title = e };
        }

        public static IHTMLInput AttachToWithLabel(this IHTMLInput e, IHTMLElement c)
        {
            return AttachToWithLabel(e, e.title, c);
        }

        public static IHTMLInput AttachToWithLabel(this IHTMLInput e, string label, IHTMLElement c)
        {
            new IHTMLDiv(
                 new IHTMLLabel(label, e), e
             ).AttachTo(c);

            return e;
        }

        public static ScriptCoreLib.JavaScript.Runtime.Cookie BindTo(this ScriptCoreLib.JavaScript.Runtime.Cookie c, IHTMLTextArea a)
        {
            a.onchange += delegate { c.Value = a.value; };
            a.value = c.Value;

            return c;
        }

        public static ScriptCoreLib.JavaScript.Runtime.Cookie BindTo(this ScriptCoreLib.JavaScript.Runtime.Cookie c, IHTMLInput a)
        {
            a.onchange += delegate { c.Value = a.value; };
            a.value = c.Value;

            return c;
        }

        public static int Random(this int i)
        {
            return new Random().Next(i);
        }


        public static bool IsUpper(this char e)
        {
            var s = Convert.ToString(e);

            return s.ToUpper() == s;
        }

        public static bool IsLower(this char e)
        {
            var s = Convert.ToString(e);

            return s.ToLower() == s;
        }


        public static string ToUpper(this char e)
        {
            return Convert.ToString(e).ToUpper();
        }

        public static string ToLower(this char e)
        {
            return Convert.ToString(e).ToLower();
        }

        public static string ToCamelCase(this string e)
        {
            var v = "";

            if (e.Length > 0)
            {
                v += e[0].ToUpper();

                if (e.Length > 1)
                {
                    for (int i = 1; i < e.Length; i++)
                    {
                        if (e[i] != '_')
                        {
                            if (e[i - 1] == '_')
                                v += e[i].ToUpper();
                            else

                                v += e[i].ToLower();
                        }
                    }
                }
            }


            return v;
        }

        public static string ToCamelCaseUpper(this string e)
        {
            var v = "";

            if (e.Length > 0)
            {
                v += e[0].ToUpper();

                if (e.Length > 1)
                {
                    for (int i = 1; i < e.Length; i++)
                    {

                        if (e[i].IsUpper() && e[i - 1].IsLower())
                        {
                            if (e[i - 1] != '_')
                                v += "_";
                        }

                        v += e[i].ToUpper();
                    }
                }
            }


            return v;
        }
    }
}
