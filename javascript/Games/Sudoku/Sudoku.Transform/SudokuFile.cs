using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;

namespace Sudoku.Transform
{
    [Script]
    internal class Tuple<T, F>
    {
        public T TValue;
        public F FValue;
    }

    [Script]
    static class Extensions
    {
        public static IndexInfo IndexInfoOf(this IndexInfo e, string s)
        {
            return new IndexInfo { Index = e.Source.IndexOf(s, e.Index + e.Subject.Length), Subject = s, Source = e.Source };

        }



        public static IndexInfo IndexInfoOf(this string e, string s)
        {
            return new IndexInfo { Index = e.IndexOf(s), Subject = s, Source = e };
        }

        public static string SubString(this IndexInfo e, IndexInfo x)
        {
            var z = e.Index + e.Subject.Length;

            return e.Source.Substring(z, x.Index - z);

        }

        public static IndexInfoResult GetNextTrimmedLine(this IndexInfo e)
        {
            return GetTrimmedLineByOffset(e, 1);
        }

        public static void ScanToList<T>(this IndexInfo e, IList<T> list, Func<IndexInfo, Tuple<T, IndexInfo>> next)
        {
            var LastIndex = e;

            while (LastIndex != null)
            {
                var p = next(LastIndex);

                if (p != null)
                {
                    LastIndex = p.FValue;


                    list.Add(p.TValue);
                }
                else
                {
                    LastIndex = null;
                }
            }


        }



        public static IndexInfoResult ReadTrimmedLine(this IndexInfo e)
        {
            var i = e.IndexInfoOf("\n");
            if (i.Index == -1)
                throw new Exception("EOL");


            return new IndexInfoResult { Subject = i.Subject, Source = e.Source, Index = i.Index, Text = e.SubString(i).Trim() };
        }

        public static IndexInfoResult GetTrimmedLineByOffset(this IndexInfo e, int offset)
        {
            if (offset < 1)
                throw new NotImplementedException("GetLineByOffset negative offset");

            string r = null;

            var i = e.IndexInfoOf("\n");
            if (i.Index == -1)
                throw new Exception("GetTrimmedLineByOffset EOL");

            while (r == null)
            {


                var j = i.IndexInfoOf("\n");
                if (j.Index == -1)
                    throw new Exception("GetTrimmedLineByOffset EOL");

                if (offset == 1)
                    r = i.SubString(j).Trim();

                offset--;

                i = j;
            }

            return new IndexInfoResult { Subject = "", Source = e.Source, Index = i.Index, Text = r };

        }



    }

    [Script]
    public class IndexInfo
    {
        public string Source;
        public string Subject;

        public int Index;

        public string BeforeSubject
        {
            get
            {
                return Source.Substring(0, Index);
            }
        }

        public string AfterSubject
        {
            get
            {
                return Source.Substring(Index + Subject.Length);
            }
        }
    }

    [Script]
    public class IndexInfoResult : IndexInfo
    {
        public string Text;
    }


    [Script]
    public class SudokuFile
    {
        [Script]
        public /*immutable*/ class Symbol
        {
            public int MappedValue
            {
                get { return Owner.Mappings.Value[Value - 1]; }
            }

            public int MappedX
            {
                get
                {
                    if (Owner.Mappings.Rotated)
                        return Owner.Mappings.Y[Y - 1];
                    return Owner.Mappings.X[X - 1];
                }
            }

            public int MappedY
            {
                get
                {
                    if (Owner.Mappings.Rotated)
                        return Owner.Mappings.X[X - 1];

                    return Owner.Mappings.Y[Y - 1];
                }
            }

            public int Value;

            public bool Hidden;

            public int X;
            public int Y;

            public SudokuFile Owner;

            public override string ToString()
            {
                if (Hidden)
                {
                    // no-break space
                    return "\xa0";
                }
                else
                {
                    return "" + MappedValue;
                }
            }
        }


        public readonly List<Symbol> Symbols = new List<Symbol>();

        public Symbol this[int X, int Y]
        {
            get
            {
                return Symbols.Single(i => i.MappedX == X && i.MappedY == Y);
            }
        }

        [Script]
        public class SymbolMappings
        {
            public int[] Value = Enumerable.Range(1, 9).ToArray();
            public int[] X = Enumerable.Range(1, 9).ToArray();
            public int[] Y = Enumerable.Range(1, 9).ToArray();

            public bool Rotated;

            public void Randomize()
            {
                Randomize(() => new Random().NextDouble());
            }

            public void Randomize(Func<double> random)
            {
                Rotated = random() < 0.5;

                Value = Value.Randomize().ToArray();

                X = RandomizeByBlock(random, X);
                Y = RandomizeByBlock(random, Y);
            }

            static int[] RandomizeByBlock(Func<double> random, int[] e)
            {
                return new[]
                    {
                        new [] { e[0], e[1], e[2] }.Randomize(),
                        new [] { e[3], e[4], e[5] }.Randomize(),
                        new [] { e[6], e[7], e[8] }.Randomize(),
                    }.Randomize().SelectMany(i => i).ToArray();
            }
        }

        public SymbolMappings Mappings = new SymbolMappings();

        public readonly string Description;


        public SudokuFile(string data)
        {
            // create symbols
            for (int y = 1; y < 10; y++)
                for (int x = 1; x < 10; x++)
                    Symbols.Add(
                        new Symbol
                        {
                            X = x,
                            Y = y,
                            Owner = this
                        }
                    );


            var ValueStart = data.IndexInfoOf(Environment.NewLine + Environment.NewLine);
            var HiddenStart = ValueStart.IndexInfoOf(Environment.NewLine + Environment.NewLine);

            Description = ValueStart.BeforeSubject;

            {
                var p = ValueStart;
                for (int y = 1; y < 10; y++)
                {
                    var t = p.ReadTrimmedLine();
                    p = t;

                    for (int x = 1; x < 10; x++)
                        this[x, y].Value = int.Parse(t.Text.Substring(x - 1, 1));
                }
            }

            {
                var p = HiddenStart;
                for (int y = 1; y < 10; y++)
                {
                    var t = p.ReadTrimmedLine();
                    p = t;

                    for (int x = 1; x < 10; x++)
                        this[x, y].Hidden = 0 == int.Parse(t.Text.Substring(x - 1, 1));
                }

            }
        }

        public void ToConsole()
        {
            System.Console.WriteLine(Description);
            for (int y = 1; y < 10; y++)
            {
                for (int x = 1; x < 10; x++)
                    System.Console.Write(this[x, y].ToString());

                System.Console.WriteLine();
            }
            System.Console.WriteLine();
        }
    }
}
