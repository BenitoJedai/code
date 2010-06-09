using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLParserToken
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

        public void Initialize()
        {
            // we know now who was the previous token
            // we also know where we are starting at


            // so who are we?
            // are we whitespace, comment, keyword, literal or just name?

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
            }

            if (char.IsWhiteSpace(this[0]))
            {
                this.IsComment = true;
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

            if (char.IsLetter(this[0]))
            {
                this.IsName = true;
                this.Length = ScanLength(1,
                    i =>
                    {
                        if (char.IsLetterOrDigit(this[i]))
                            return false;

                        return true;
                    }
                );
                return;
            }

            throw new NotImplementedException();
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
    }
}
