using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class Pair<TA, TB>
{
    public TA A;
    public TB B;

    public Pair(TA a, TB b)
    {
        this.A = a;
        this.B = b;
    }
}

namespace ScriptCoreLib.Tools
{
    using _int_char = Pair<int, char>;
    using System.IO;
    using System.Collections;
    using System.Reflection;


    public delegate void ActionParams<A0, T>(A0 a0, params T[] e);
    public delegate void ActionParams<T>(params T[] e);
    //public delegate void Action<T>(T e);
    //public delegate void Action<A, B>(A a, B b);
    //public delegate void Action();


    public static class FunctionReturnValueExtension
    {
        public static FunctionReturnValue ToFunctionReturnValue<T>(this T e)
            where T : class
        {
            return new FunctionReturnValue(e);
        }
    }


    public class FunctionReturnValue
    {
        readonly object Value;

        public FunctionReturnValue(object e)
        {
            Value = e;
        }


        public object GetValue()
        {
            return Value;
        }

    }

    public class LiteralString
    {
        readonly string Value;

        public LiteralString(string e)
        {
            Value = e;
        }

        public static implicit operator LiteralString(string e)
        {
            return new LiteralString(e);
        }

        public static implicit operator string(LiteralString e)
        {
            return e.Value;
        }
    }

    public static class JSONSerializer
    {
        class ArrayBuilder
        {
            public readonly ArrayList List;
            public readonly Action Handler;
            public readonly Type ElementType;

            public ArrayBuilder(Action<Array> e, Type c)
            {
                List = new ArrayList();
                Handler = delegate
                {
                    e(List.ToArray(ElementType));
                };

                ElementType = c;
            }
        }

        static Func<T, T> Duplicate<T>(Action<T> e)
        {
            return delegate(T x)
            {
                e(x);
                return x;
            };
        }


        static void Invoke<A, B>(Func<Pair<A, B>> p, Action<A> a, Action<B> b)
        {
            var x = p();

            a(x.A);
            b(x.B);
        }

        public static void Deserialize<T>(T o, Stream ds)
        {
			var s = new BufferedStream(ds);

			var stext = new BinaryReader(s).ReadString();

			s.Position = 0;

            int StreamPosition = 0;

            Func<NotImplementedException> NotImplemented = () => new NotImplementedException("Invalid char in json stream at " + StreamPosition);
            Func<NotSupportedException> CreateException = () => new NotSupportedException("Invalid char in json stream at " + StreamPosition);

            Action<bool> ExpectB =
                delegate(bool x)
                {
                    if (!x) throw CreateException();
                };

            Action<object> ExpectN =
                 delegate(object x)
                 {
                     ExpectB(x != null);
                 };

            Action<object, string> ExpectNText =
                 delegate(object x, string text)
                 {
                     if (x == null) throw new NotSupportedException(text);
                 };

            ActionParams<char, char> Expect = (x, xp) => ExpectB(xp.Contains(x)); ;



            #region AsciiCharToByte
            Func<char, byte?> AsciiCharToByte = delegate(char x)
            {
                switch (x)
                {
                    case '0': return 0;
                    case '1': return 1;
                    case '2': return 2;
                    case '3': return 3;
                    case '4': return 4;
                    case '5': return 5;
                    case '6': return 6;
                    case '7': return 7;
                    case '8': return 8;
                    case '9': return 9;
                    default:
                        return null;
                }
            };
            #endregion

            Func<char, byte> GetAsciiCharAsByte =
                delegate(char xc)
                {
                    var x = AsciiCharToByte(xc);

                    if (x == null)
                        throw CreateException();

                    return (byte)x;
                };

            #region HexCharToByte
            Func<char, byte> HexCharToByte = delegate(char x)
            {
                switch (x)
                {
                    case 'a':
                    case 'A':
                        return 10;
                    case 'b':
                    case 'B':
                        return 11;
                    case 'c':
                    case 'C':
                        return 12;
                    case 'd':
                    case 'D':
                        return 13;
                    case 'e':
                    case 'E':
                        return 14;
                    case 'f':
                    case 'F':
                        return 15;
                    default:
                        return GetAsciiCharAsByte(x);

                }
            };
            #endregion

            // get char
            Func<char> GetChar = delegate
            {
                StreamPosition++;

                return (char)s.ReadByte();
            };

            Func<char, char> SkipSpace =
                delegate(char x)
                {
                    while (char.IsWhiteSpace(x))
                    {
                        x = GetChar();
                    }

                    return x;
                };

            Func<char> GetNonSpaceChar = () => SkipSpace(GetChar());


            //var GetIntegerAndNextCharReturn = new { c = (char)0, i = (int)0 };


            Func<int, _int_char> GetIntegerVAndNextChar = delegate(int xi)
                {
                    var xz = new _int_char(xi, default(char));


                    while (true)
                    {
                        xz.B = GetChar();


                        byte? x = AsciiCharToByte(xz.B);

                        if (x == null)
                            return xz;

                        xz.A *= 10;
                        xz.A += (byte)x;
                    }
                };

            Func<_int_char> GetIntegerAndNextChar = () => GetIntegerVAndNextChar(0);



            #region GetHexByte
            Func<byte> GetHexByte =
                delegate
                {
                    return (byte)((HexCharToByte(GetChar()) << 4) + HexCharToByte(GetChar()));
                };
            #endregion


            #region GetEscapedChar
            Func<char> GetEscapedChar =
                delegate
                {
                    // http://www.codecodex.com/wiki/index.php?title=Escape_sequences

                    var x = GetChar();

                    switch (x)
                    {
                        case 'n':
                            return '\n';
                        case 'r':
                            return '\r';
                        case 't':
                            return '\t';
                        case 'b':
                            return '\b';
                        case '/':
                        case '$':
                        case '\\':
                        case '\'':
                        case '"':
                            return x;
                        case 'u':
                            return (char)((GetHexByte() << 8) + GetHexByte());
                        case 'x':
                            return (char)GetHexByte();
                        default:
                            throw CreateException();

                    }
                };
            #endregion


            #region GetQuotedString
            Func<string> GetQuotedString =
                delegate
                {
                    var x = "";

                next:
                    var xc = GetChar();

                    if (xc == '"')
                        return x;

                    x += (xc == '\\') ? GetEscapedChar() : xc;

                    goto next;
                };
            #endregion

            Func<char, bool> ScanNull =
                delegate(char x)
                {
                    if (x != 'n')
                        return false;

                    ExpectB(GetChar() == 'u');
                    ExpectB(GetChar() == 'l');
                    ExpectB(GetChar() == 'l');

                    return true;
                };

            // current object pointer
            var p = new Stack<object>();



            Func<Type> GetCurrentType = () => p.Peek().GetType();

            // current char
            var c = GetChar();


            Expect(c, '{');

            p.Push(o);

            goto Members;
        NextOrUp:
            c = GetNonSpaceChar();
        NextOrUpExplicit:
            switch (c)
            {
                case ',':
                    goto Members;
                case '}':
                case ']':
                    goto Members_Explicit;
                default:
                    throw CreateException();
            }
        ArrayMembers:
            {
                var xx = p.Peek() as ArrayBuilder;

                ExpectN(xx);

                if (xx.ElementType == typeof(int))
                {
                    var xp = GetIntegerVAndNextChar(GetAsciiCharAsByte(c));

                    xx.List.Add(xp.A);
                    c = SkipSpace(xp.B);

                    goto ArrayNextOrUp;
                }
                else if (xx.ElementType.IsClass)
                {
                    Expect(c, '{');

                    var value = Activator.CreateInstance(xx.ElementType);

                    xx.List.Add(value);
                    p.Push(value);

                    goto Members;
                }

                throw NotImplemented();
            }
        // based on type we must read the stream
        ArrayNextOrUp:
            switch (c)
            {
                case ',':
                    goto Members;
                case ']':
                    goto Members_Explicit;
                default:
                    throw CreateException();
            }
        Members:
            c = GetNonSpaceChar();
        Members_Explicit:
            switch (c)
            {
                case '}':
                    ExpectB(GetCurrentType().IsClass);

                    p.Pop();

                    if (p.Count == 0)
                        return;

                    goto NextOrUp;
                case ']':
                    var x = p.Peek() as ArrayBuilder;

                    ExpectN(x);

                    p.Pop();
                    x.Handler();

                    goto NextOrUp;
            }

            if (GetCurrentType() == typeof(ArrayBuilder))
                goto ArrayMembers;

            if (c == '"')
            {
                var FieldName = GetQuotedString();

                c = GetNonSpaceChar();

                Expect(c, ':');

                c = GetNonSpaceChar();

                // at this point we need to know does this property exist, and what type it is.

                var Field = GetCurrentType().GetField(FieldName);

                ExpectNText(Field, "Field was removed, json is out of date");

                var FieldType = Field.FieldType;
                var SetFieldValue = Duplicate<object>(x => Field.SetValue(p.Peek(), x));
                // [], "", {}, 0

                if (FieldType.IsArray)
                {
                    Expect(c, '[');

                    p.Push(
                        new ArrayBuilder(x => SetFieldValue(x), FieldType.GetElementType())
                    );

                    goto Members;
                }
                else if (FieldType == typeof(string))
                {
                    if (ScanNull(c)) goto NextOrUp;

                    Expect(c, '"');

                    SetFieldValue(GetQuotedString());

                    goto NextOrUp;
                }
                else if (FieldType == typeof(int))
                {
                    var x = GetIntegerVAndNextChar((int)AsciiCharToByte(c));

                    SetFieldValue(x.A);
                    c = x.B;

                    goto NextOrUpExplicit;
                }
                else if (FieldType.IsClass)
                {
                    if (ScanNull(c)) goto NextOrUp;

                    Expect(c, '{');


                    p.Push(SetFieldValue(Activator.CreateInstance(FieldType)));

                    goto Members;
                }

                throw CreateException();
            }



        }


        public static string Serialize<T>(T obj)
        {
            var w = new MemoryStream();

            Serialize(obj, w);

            return Encoding.ASCII.GetString(w.ToArray());
        }

        public static string Serialize<T>(T obj, char QouteChar)
        {
            var w = new MemoryStream();

            Serialize(obj, w, QouteChar);

            return Encoding.ASCII.GetString(w.ToArray());
        }

        public static void Serialize<T>(T obj, Stream s)
        {
            Serialize(obj, s, '"');
        }

        public static void Serialize<T>(T obj, Stream s, char QouteChar)
        {
            Action<char> WriteChar = c => s.WriteByte((byte)c);
            Action<string> WriteChars =
                delegate(string x)
                {
                    foreach (char v in x)
                    {
                        WriteChar(v);
                    }
                };



            var h = new Hashtable();

            Action<object> WriteObject = null;
            Action<Array> WriteArray = null;
            Action<string> WriteQuotedString = null;
            Action<int> WriteInt32 = null;
            Action<Type, Func<object>> WriteElement = null;

            #region WriteQuotedString
            WriteQuotedString = delegate(string x)
            {
                if (x == null)
                {
                    WriteChars("null");

                    return;
                }

                WriteChar(QouteChar);

                foreach (char v in x)
                {
                    switch (v)
                    {
                        case '\n': WriteChars(@"\n"); continue;
                        case '\t': WriteChars(@"\t"); continue;
                        case '\r': WriteChars(@"\r"); continue;
                        case '\b': WriteChars(@"\b"); continue;
                        case '"': WriteChars("\\\""); continue;
                        case '\'': WriteChars("\\\'"); continue;
                        default:
                            if (char.IsLetterOrDigit(v) || char.IsPunctuation(v) || v == ' ')
                            {
                                WriteChar(v);
                            }
                            else if (v > 0xFF)
                            {
                                WriteChars(string.Format(@"\u{0:x4}", (int)v));
                            }
                            else
                            {
                                WriteChars(string.Format(@"\x{0:x2}", (int)v));
                            }

                            continue;
                    }
                }

                WriteChar(QouteChar);
            };
            #endregion

            WriteInt32 = delegate(int x)
            {
                foreach (char v in x.ToString())
                {
                    WriteChar(v);
                }
            };

            #region WriteArray
            WriteArray = delegate(Array x)
            {
                if (x == null)
                {
                    WriteChars("null");

                    return;
                }

                WriteChar('[');

                for (int i = 0; i < x.Length; i++)
                {
                    object v = x.GetValue(i);

                    if (i > 0)
                        WriteChar(',');

                    if (v == null)
                    {
                        WriteChars("null");

                        continue;
                    }

                    WriteElement(v.GetType(), () => v);
                }

                WriteChar(']');
            };
            #endregion

            Action<object> WriteAnyElement = v => WriteElement(v.GetType(), () => v);

            WriteElement = delegate(Type ft, Func<object> fv)
            {

                if (ft == typeof(FunctionReturnValue))
                {
                    WriteChars("function (){return ");
                    
                    WriteAnyElement(((FunctionReturnValue)fv()).GetValue());

                    WriteChars(";}");
                }
                else if (ft == typeof(LiteralString))
                {
                    WriteChars((LiteralString)fv());
                }
                else if (ft == typeof(string))
                {
                    WriteQuotedString((string)fv());
                }
                else if (ft == typeof(int))
                {
                    WriteInt32((int)fv());
                }
                else if (ft.IsArray)
                {
                    WriteArray((Array)fv());
                }
                else if (ft.IsClass)
                {
                    WriteObject(fv());
                }
                else
                    throw new NotImplementedException();
            };

            #region WriteObject
            WriteObject = delegate(object x)
            {
                if (x == null)
                {
                    WriteChars("null");

                    return;
                }

                WriteChar('{');

                var f = x.GetType().GetFields().ToArray();
                var fieldindex = -1;

                for (int i = 0; i < f.Length; i++)
                {
                    var v = f[i];


                    if (fieldindex++ > -1)
                        WriteChar(',');

                    //WriteQuotedString(v.Name);
                    WriteChars(v.Name);

                    WriteChar(':');

                    WriteElement(v.FieldType, () => v.GetValue(x));
                }

                if (ScriptAttribute.IsAnonymousType(x.GetType()))
                {
                    var p = x.GetType().GetProperties().ToArray();

                    foreach (var v in p)
                    {
                        if (fieldindex++ > -1)
                            WriteChar(',');

                        WriteChars(v.Name);

                        WriteChar(':');

                        WriteElement(v.PropertyType, () => v.GetValue(x, null));
                    }

                }

                WriteChar('}');
            };
            #endregion

            WriteAnyElement(obj);
        }
    }

}
