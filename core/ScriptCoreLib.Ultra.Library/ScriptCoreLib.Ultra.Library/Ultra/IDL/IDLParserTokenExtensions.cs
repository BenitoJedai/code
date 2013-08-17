using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Diagnostics;

namespace ScriptCoreLib.Ultra.IDL
{
    public static class IDLParserTokenExtensions
    {
        public static IDLParserToken AssertWhiteSpace(this IDLParserToken t)
        {
            if (t.IsWhiteSpace)
            {
                return t;
            }

            throw new NotSupportedException();
        }


        public static IDLParserToken AssertDigit(this IDLParserToken t)
        {
            if (t.IsSymbol)
            {
                if (t.Text.IsDigit())
                    return t;
            }

            throw new NotSupportedException();
        }

        public static IDLParserToken AssertSymbol(this IDLParserToken t, string Text = null)
        {
            if (t != null)
                if (t.IsSymbol)
                {
                    if (Text == null)
                        return t;

                    if (t.Text == Text)
                        return t;
                }

            if (Debugger.IsAttached)
                Debugger.Break();

            throw new NotSupportedException("AssertName failed at line " + t.LineNumber);
        }

        public static IDLParserToken AssertName(this IDLParserToken t, string Text = null)
        {
            if (t.IsName)
            {
                if (Text == null)
                    return t;

                if (t.Text == Text)
                    return t;
            }

            throw new NotSupportedException("AssertName failed at line " + t.LineNumber);
        }


        public static IDLParserToken Combine(this IEnumerable<IDLParserToken> source)
        {
            var n = default(IDLParserToken);

            foreach (var item in source)
            {
                if (n == null)
                {
                    n = item;
                }
                else
                {
                    if (n.Next == item)
                    {
                        n.Length += item.Length;
                        n.InternalNext = item.InternalNext;

                        if (n.InternalNext != null)
                        {
                            n.InternalNext.Previous = n;
                        }
                    }
                }
            }

            return n;
        }



        public static IDLParserToken SkipTo(this IDLParserToken source)
        {
            return (
                from t in source
                where !t.IsComment
                where !t.IsWhiteSpace
                select t
            ).FirstOrDefault();
        }

        public static string GetString(this IDLParserToken source)
        {
            var w = new StringBuilder();

            foreach (var item in source)
            {
                w.Append(item.Text);
            }

            return w.ToString();
        }

        public static IDLParserToken Write(this IDLParserToken source, IDLParserToken.Literal e)
        {
            e.Value.Previous = source;

            source.Next.Previous = e;
            e.Value.InternalNext = source.Next;

            source.InternalNext = e;

            return e;
        }

        public static IDLParserToken WriteLine(this IDLParserToken source)
        {
            var e = source.Write("\r\n");

            e.IsWhiteSpace = true;

            return e;
        }


        public static IDLParserToken WriteSpace(this IDLParserToken source)
        {
            var e = source.Write(" ");

            e.IsWhiteSpace = true;

            return e;
        }

        public static void WriteSpaces(this IDLParserToken source, IDLParserToken.Literal p)
        {
            source.WriteSpace().Write(p).WriteSpace();
        }
    }
}
