using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ConvertASToCS.js.Any
{
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
    }

    [Script]
    public class IndexInfoResult : IndexInfo
    {
        public string Text;
    }

    [Script]
    internal class Tuple<T, F>
    {
        public T TValue;
        public F FValue;
    }

    [Script]
    public class ReflectionProvider
    {
        //public int PackageIndex { get; private set; }
        //public int PackageEOL { get; private set; }

        public string PackageName { get; private set; }
        public string ClassSignature { get; private set; }

        public bool IsSealed
        {
            get
            {
                return ClassSignature.Contains("final");
            }
        }

        public string TypeName
        {
            get
            {
                return ClassSignature.Substring(ClassSignature.LastIndexOf(" ") + 1);
            }
        }

        [Script]
        public class PropertyInfo
        {
            public string Signature;
            public string Summary;
            public string DefinedBy;

            public string PropertyType
            {
                get
                {
                    return Signature.Substring(Signature.IndexOf(":") + 1).Trim();
                }
            }

            public string PropertyName
            {
                get
                {
                    return Signature.Substring(0, Signature.IndexOf(":")).Trim();
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return Summary.Contains("[read-only]");
                }
            }

            internal static Tuple<PropertyInfo, IndexInfo> ScanNext(IndexInfo LastIndex)
            {
                // scan properties
                var PropertySignature = LastIndex.GetNextTrimmedLine();

                if (!PropertySignature.Text.Contains(":"))
                    return null;

                var PropertySummary = PropertySignature.GetNextTrimmedLine();

                if (string.IsNullOrEmpty(PropertySignature.Text))
                    return null;

                var PropertyDefinedBy = PropertySummary.GetNextTrimmedLine();

                if (string.IsNullOrEmpty(PropertyDefinedBy.Text))
                    return null;

                var n = new PropertyInfo
                    {
                        Signature = PropertySignature.Text,
                        Summary = PropertySummary.Text,
                        DefinedBy = PropertyDefinedBy.Text,
                    };

                // do last minute checks here


                return new Tuple<PropertyInfo, IndexInfo>
                {
                    TValue = n,
                    FValue = PropertyDefinedBy
                };
            }
        }

        public readonly List<PropertyInfo> Properties = new List<PropertyInfo>();

        public ReflectionProvider(string text)
        {
            // todo: Environment.NewLine make browser dependant

            var PackageIndex = text.IndexInfoOf("Package\t");
            //this.PackageIndex = PackageIndex.Index;
            var PackageEOL = PackageIndex.IndexInfoOf("\n");
            //this.PackageEOL = PackageEOL.Index;
            PackageName = PackageIndex.SubString(PackageEOL).Trim();

            var ClassIndex = PackageEOL.IndexInfoOf("Class\t");
            var ClassEOL = ClassIndex.IndexInfoOf("\n");
            ClassSignature = ClassIndex.SubString(ClassEOL).Trim();


            var PropertyHeader = ClassEOL.IndexInfoOf(" \tProperty\tDefined by");
            var MethodHeader = ClassEOL.IndexInfoOf(" \tMethod\tDefined by");
            var ConstantHeader = ClassEOL.IndexInfoOf(" \tConstant\tDefined by");

            PropertyHeader.ScanToList(Properties, f => PropertyInfo.ScanNext(f));




        }




    }
}
