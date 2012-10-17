﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{


    public class IDLParserToken : IEnumerable<IDLParserToken>
    {
        // see: http://en.wikipedia.org/wiki/IDL_specification_language

        public readonly string Source;

        public int Position { get; set; }
        public int Length { get; set; }

        public int LineNumber
        {
            get
            {
                return Source.Substring(0, Position).Split('\n').Length;
            }
        }


        public static implicit operator IDLParserToken(string Source)
        {
            return new IDLParserToken(Source);
        }

        public IDLParserToken(string Source)
        {
            this.Source = Source;
        }

        public IDLParserToken Previous;

        public IDLParserToken InternalNext;
        public IDLParserToken Next
        {
            get
            {
                if (InternalNext == null)
                {
                    if (Position + Length >= this.Source.Length)
                        return null;

                    InternalNext = new IDLParserToken(Source)
                    {
                        Position = Position + Length,
                        Previous = this
                    };

                    InternalNext.Initialize();
                }

                return InternalNext;
            }
        }

        public bool IsCommentDiscoveryEnabled = true;

        public bool IsComment;
        public bool IsWhiteSpace;
        public bool IsName;
        public bool IsSymbol;
        public bool IsLiteral;

        public void Initialize()
        {
            // we know now who was the previous token
            // we also know where we are starting at


            // so who are we?
            // are we whitespace, comment, keyword, literal or just name?

            // we have to add support for literals so we wont mix them with comments.
            // or we should add support to disable comment recognition?

            #region IsComment
            if (this.Previous == null || this.Previous.IsCommentDiscoveryEnabled)
            {
                if (this[0] == '/')
                {
                    // are we a box comment?
                    if (this[1] == '*')
                    {

                        // this means we will scan to */


                        this.IsComment = true;
                        this.Length = ScanLength(4,
                            i =>
                            {
                                if (this[i - 2] == '*')
                                    if (this[i - 1] == '/')
                                        return true;

                                return false;
                            }
                        );
                        return;
                    }

                    if (this[1] == '/')
                    {
                        this.IsComment = true;

                        this.Length = ScanLength(3,
                            i =>
                            {
                                // end of file
                                if (this.Source.Length == this.Position + i)
                                    return true;

                                return this[i - 1] == '\n';
                            }
                        );

                        return;
                    }
                }
            }
            #endregion

            #region IsWhiteSpace
            if (char.IsWhiteSpace(this[0]))
            {
                this.IsWhiteSpace = true;
                this.Length = ScanLength(1,
                    i =>
                    {
                        if ((this.Position + i) < this.Source.Length)
                        {
                            if (char.IsWhiteSpace(this[i]))
                                return false;

                        }

                        return true;
                    }
                );
                return;
            }
            #endregion

            #region IsLetterOrUnderscore
            Func<char, bool> IsLetterOrUnderscore =
                c =>
                {
                    if (char.IsLetter(c))
                        return true;

                    if (c == '_')
                        return true;

                    return false;
                };
            #endregion


            #region IsName
            if (IsLetterOrUnderscore(this[0]))
            {
                this.IsName = true;
                this.Length = ScanLength(1,
                    i =>
                    {
                        if ((this.Position + i) < this.Source.Length)
                        {

                            if (IsLetterOrUnderscore(this[i]))
                                return false;

                            if (char.IsLetterOrDigit(this[i]))
                                return false;

                        }
                        
                        return true;
                    }
                );
                return;
            }
            #endregion

            this.IsSymbol = true;
            this.Length = 1;
        }

        public int ScanLength(int Length, Func<int, bool> f)
        {
            var x = false;
            while (!x)
            {

                x = f(Length);
                if (!x)
                    Length++;
            }

            return Length;
        }


        public char this[int i]
        {
            get
            {
                return this.Source[this.Position + i];
            }
        }

        string InternalText;

        public string Text
        {
            get
            {
                if (InternalText != null)
                    return InternalText;

                return this.Source.Substring(this.Position, this.Length);
            }
            set
            {
                InternalText = value;
            }
        }

        public override string ToString()
        {
            return Text;
        }

        class InternalEnumerator : IEnumerator<IDLParserToken>
        {
            public IDLParserToken Current { get; set; }

            public void Dispose()
            {
            }

            object System.Collections.IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                Current = Current.Next;

                return Current != null;
            }

            public void Reset()
            {
            }
        }

        public IEnumerator<IDLParserToken> GetEnumerator()
        {
            return new InternalEnumerator { Current = this };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IDLParserToken CombineToSymbol(string e)
        {
            if (this.IsSymbol)
            {
                var f = default(Action);

                f = delegate
                    {
                        var Next = this.Next;
                        if (Next.IsSymbol)
                        {
                            if (e.StartsWith(this.Text + Next.Text))
                            {
                                this.Length += Next.Length;
                                this.InternalNext = Next.Next;
                                this.InternalNext.Previous = this;

                                // tail call would be awesome? :)
                                f();
                            }
                        }
                    };

                f();
            }

            return this;
        }

        public class Literal
        {
            public IDLParserToken Value;

            public static implicit operator Literal(IDLParserToken e)
            {
                if (e == null)
                    return null;

                return new Literal { Value = e };
            }

            public static implicit operator IDLParserToken(Literal e)
            {
                if (e == null)
                    return null;

                return e.Value;
            }


            public static implicit operator Literal(string e)
            {
                if (e == null)
                    return null;


                var n = new Literal { Value = e };

                n.Value.Length = e.Length;

                return n;
            }
        }


    }
}
