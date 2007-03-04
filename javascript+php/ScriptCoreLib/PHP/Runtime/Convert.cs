using ScriptCoreLib.PHP;
using ScriptCoreLib;

using ScriptCoreLib.Shared;

namespace ScriptCoreLib.PHP.Runtime
{
    [Script]
    public class Convert
    {
        static public string ToUTF8(string str)
        {
            if (!Native.API.function_exists("iconv"))
                return str;

            return Native.API.iconv("ISO-8859-1", "UTF-8", str);
        }

        static public string ToAscii(string str)
        {
            if (!Native.API.function_exists("iconv"))
                return str;


            return Native.API.iconv("UTF-8", "ISO-8859-1", str);
        }

        public static string ToHexString(int e)
        {
            return Native.API.dechex(e);
        }

        public static string ToHexString(string e)
        {
            string x = "";

            foreach (char c in e)
            {
                x += ToHexString(c);
            }

            return x;
        }

        public static int[] GetUTF8Chars(string str)
        {
            IArray<int, int> a = new IArray<int, int>();

            int i = 0;
            int count = 0;
            int len = str.Length;

            while (i < len)
            {
                char chr = str[i];

                count++;
                i++;

                int x = chr;

                if (i < len)
                {
                    if ((chr & 0x80) > 0)
                    {
                        chr <<= 1;
                        while ((chr & 0x80) > 0 && x <= 0xFF)
                        {
                            x <<= 8;
                            x += str[i];
                            i++;
                            chr <<= 1;

                        }

                    }
                }

                a.Push(x);
            }

            return a.ToArray();
        }

        public static int GetUTF8Length(string str)
        {
            int i = 0;
            int count = 0;
            int len = str.Length;

            while (i < len)
            {
                char chr = str[i];

                count++;
                i++;

                if (i >= len)
                    return count;

                if ((chr & 0x80) > 0)
                {
                    chr <<= 1;
                    while ((chr & 0x80) > 0)
                    {
                        i++;
                        chr <<= 1;
                    }

                }
            }

            return count;
        }

        [Script(IsStringEnum = true)]
        public enum EncodingEnum
        {
            Unknown,
            ASCII,
            UTF8,
            UTF8_1,
            UTF8_2
        }

        public static EncodingEnum GetEncoding(string v)
        {
            int[] a = GetUTF8Chars(v);

            bool bASCII = false;
            bool bUTF8 = false;
            bool bUTF8_1 = false;
            bool bUTF8_2 = false;

            foreach (int va in a)
            {
                if (va > 0xFF)
                {
                    if (va == 0xc383)
                    {
                        bUTF8_1 = true;
                    }
                    else if (va == 0xC283)
                    {
                        bUTF8_2 = true;
                    }
                    else
                    {

                        int p = va >> 8;

                        bool p1 = p == 0xc2;
                        bool p2 = p == 0xc3;

                        if (p1 || p2)
                        {
                            bUTF8 = true;
                        }
                        else
                        {

                            bASCII = true;
                        }
                    }
                }

                //Native.echo(ToHexString(var) + " ");
            }

            if (bUTF8_2)
                return EncodingEnum.UTF8_2;

            if (bUTF8_1)
                return EncodingEnum.UTF8_1;

            if (bUTF8)
                return EncodingEnum.UTF8;

            if (bASCII)
                return EncodingEnum.ASCII;

            return EncodingEnum.Unknown;
        }

        public static string ToUTF8(string e, bool b)
        {
            if (b)
            {
                EncodingEnum x = GetEncoding(e);
                EncodingEnum v;

                v = EncodingEnum.UTF8;
                if (x == v)
                    return e;

                v = EncodingEnum.ASCII;
                if (x == v)
                    return ToUTF8(e);

                v = EncodingEnum.UTF8_1;
                if (x == v)
                    return ToAscii(e);

                v = EncodingEnum.UTF8_2;
                if (x == v)
                    return ToAscii(ToAscii(e));
            }

            return ToUTF8(e);
        }

        public static string ToAscii(string e, bool b)
        {
            if (b)
            {
                EncodingEnum x = GetEncoding(e);
                EncodingEnum v;


                v = EncodingEnum.ASCII;
                if (x == v)
                    return e;

                v = EncodingEnum.UTF8;
                if (x == v)
                    return ToAscii(e);

                v = EncodingEnum.UTF8_1;
                if (x == v)
                    return ToAscii(ToAscii(e));

                v = EncodingEnum.UTF8_2;
                if (x == v)
                    return ToAscii(ToAscii(ToAscii(e)));
            }

            return ToAscii(e);
        }

        public static int ToInt32(object p)
        {
            return int.Parse(p + "");
        }

        public static string ToString(object e)
        {
            return e + "";
        }

        public static string FromBase64String(string p)
        {
            return Native.API.base64_decode(p);
        }

        public static string ToBase64String(string p)
        {
            return Native.API.base64_encode(p);
        }

        public static TReturn To<TArg, TReturn>(EventHandler<Predicate<TArg, TReturn>> h, TArg x)
        {
            Predicate<TArg, TReturn> p = new Predicate<TArg, TReturn>();

            p.TargetIn = x;

            Helper.Invoke(h, p);

            return p.TargetOut;
        }

        public static string ToReadableSring(string p)
        {
            if (p.Length < 20)
                return p;

            return p.Substring(0, 12) + "...(" + p.Length + ")";


        }
    }
}
