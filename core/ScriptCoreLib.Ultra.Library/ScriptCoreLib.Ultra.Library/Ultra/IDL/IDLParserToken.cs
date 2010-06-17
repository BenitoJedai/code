using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLParserToken : IEnumerable<IDLParserToken>
    {
        public readonly string Source;
        public int Position;
        public int Length;

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

        public bool IsComment;
        public bool IsWhiteSpace;
        public bool IsName;
        public bool IsSymbol;

        public void Initialize()
        {
            // we know now who was the previous token
            // we also know where we are starting at


            // so who are we?
            // are we whitespace, comment, keyword, literal or just name?

            #region IsComment
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
                            return this[i - 1] == '\n';
                        }
                    );

                    return;
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
                        if (char.IsWhiteSpace(this[i]))
                            return false;

                        return true;
                    }
                );
                return;
            }
            #endregion

            Func<char, bool> IsLetterOrUnderscore =
                c =>
                {
                    if (char.IsLetter(c))
                        return true;

                    if (c == '_')
                        return true;

                    return false;
                };

            #region IsName
            if (IsLetterOrUnderscore(this[0]))
            {
                this.IsName = true;
                this.Length = ScanLength(1,
                    i =>
                    {
                        if (IsLetterOrUnderscore(this[i]))
                            return false;

                        if (char.IsLetterOrDigit(this[i]))
                            return false;

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

        public string Text
        {
            get
            {
                return this.Source.Substring(this.Position, this.Length);
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


    }
}
