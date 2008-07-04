using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ConvertASToCS.js.Any
{
    [Script]
    public static class Extensions
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

}
