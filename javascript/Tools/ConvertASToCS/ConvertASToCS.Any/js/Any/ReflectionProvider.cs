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

            public const string KeywordInherited = "Inherited";
            public const string KeywordReadOnly = "[read-only]";
            public const string KeywordStatic = "[static]";

            public bool IsStatic
            {
                get { return Summary.Contains(KeywordStatic); }
            }


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
                    var right = Signature.IndexOf(":");
                    var left = Signature.IndexOf("\t");

                    return Signature.Substring(left + 1, right).Trim();
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return Summary.Contains(KeywordReadOnly);
                }
            }

            public bool IsInherited
            {
                get
                {
                    return Signature.Contains(KeywordInherited);
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

        [Script]
        public class MethodParametersInfo
        {

            [Script]
            public class ParamInfo
            {
                public string Name;
                public string TypeName;
                public string DefaultValue;

                // http://bartdesmet.net/blogs/bart/archive/2006/09/28/4473.aspx
                // http://www.sephiroth.it/weblog/archives/2006/06/actionscript_3_rest_parameter.php
                public bool IsRestParameter;

                public bool HasDefaultValue
                {
                    get { return !string.IsNullOrEmpty(DefaultValue); }
                }
            }

            public readonly ParamInfo[] Parameters;

            public MethodParametersInfo(IEnumerable<ParamInfo> e)
            {
                Parameters = e.ToArray();
            }

            public MethodParametersInfo(string e)
            {

                Parameters = e.Split(',').Select(i => i.Trim()).Where(i => !string.IsNullOrEmpty(i)).Select(
                    text =>
                    {
                        if (text.StartsWith("..."))
                        {
                            return new ParamInfo
                            {
                                IsRestParameter = true,
                                Name = text.Substring(3).Trim(),
                                TypeName = "object"
                            };
                        }

                        var q = text.Split('=');
                        var z = q[0].Split(':');

                        if (q.Length == 1)
                            return new ParamInfo
                            {
                                Name = z[0].Trim(),
                                TypeName = z[1].Trim()
                            };

                        return new ParamInfo
                        {
                            Name = z[0].Trim(),
                            TypeName = z[1].Trim(),
                            DefaultValue = q[1].Trim()
                        };
                    }
                ).ToArray();
            }

            public MethodParametersInfo DropLastParameter()
            {
                if (this.Parameters.Length == 0)
                    return null;

                var p = new List<ParamInfo>();

                for (int i = 0; i < this.Parameters.Length - 1; i++)
                {
                    p.Add(this.Parameters[i]);
                }

                return new MethodParametersInfo(p);
            }

            public IEnumerable<MethodParametersInfo> Variations
            {
                get
                {
                    if (Parameters.Length == 0)
                    {
                        return new[] { this }.AsEnumerable();
                    }


                    var v = new[] { 
                    
                        new MethodParametersInfo
                        (
                            from p in Parameters
                            select new ParamInfo
                            {
                                IsRestParameter = p.IsRestParameter,
                                Name = p.Name,
                                TypeName = p.TypeName,
                                DefaultValue = null
                            }
                         )

                     }.AsEnumerable();

                    var last = this.Parameters.Last();

                    if (last.HasDefaultValue || last.IsRestParameter)
                    {
                        // solid this and all below

                        v = v.Concat(DropLastParameter().Variations);
                    }

                    return v;

                }
            }

            public override string ToString()
            {
                if (Parameters.Length == 0)
                    return "";

                var w = new StringBuilder();

                for (int i = 0; i < Parameters.Length; i++)
                {
                    var p = Parameters[i];

                    if (i > 0)
                        w.Append(", ");


                    if (p.IsRestParameter)
                        w.Append("/* params */ " + p.TypeName + " " + p.Name);
                    else if (string.IsNullOrEmpty(p.DefaultValue))
                        w.Append(p.TypeName + " " + p.Name);
                    else
                        w.Append(p.TypeName + " " + p.Name + " = " + p.DefaultValue);
                }



                return w.ToString();
            }

            public string NamesToString()
            {
                return Parameters.Aggregate("",
                    (v, i) =>
                    {
                        if (!string.IsNullOrEmpty(v))
                            v += ", ";

                        v += i.Name;

                        return v;
                    }
                );

            }
        }

        [Script]
        public class MethodInfo
        {

            public string Flags;
            public string Signature;
            public string Summary;
            public string DefinedBy;

            public const string KeywordInherited = "Inherited";
            public const string KeywordStatic = "[static]";

            public bool IsInherited
            {
                get { return Flags == KeywordInherited; }
            }

            public bool IsStatic
            {
                get { return Summary.Contains(KeywordStatic); }
            }

            //    Inherited	
            //addEventListener(type:String, listener:Function, useCapture:Boolean = false, priority:int = 0, useWeakReference:Boolean = false):void
            //Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
            //    EventDispatcher            

            public MethodParametersInfo ParametersInfo { get; private set; }

            public string Name { get; private set; }
            public string ReturnType { get; private set; }

            public bool ReturnTypeVoid
            {
                get { return ReturnType == "void"; }
            }

            public bool IsConstructor { get; private set; }
            public bool IsMethod { get { return !IsConstructor; } }

            internal static Tuple<MethodInfo, IndexInfo> ScanNext(IndexInfo LastIndex)
            {
                // scan properties
                var Flags = LastIndex.GetNextTrimmedLine();

                if (Flags.Text != KeywordInherited)
                    if (!string.IsNullOrEmpty(Flags.Text))
                        return null;

                var Signature = Flags.GetNextTrimmedLine();

                if (!Signature.Text.Contains(")"))
                    return null;

                if (!Signature.Text.Contains("("))
                    return null;

                var Summary = Signature.GetNextTrimmedLine();

                var DefinedBy = Summary.GetNextTrimmedLine();

                // get params
                var a = Signature.Text.IndexInfoOf("(");
                var b = a.IndexInfoOf(")");
                var p = a.SubString(b);
                var r = b.IndexInfoOf(":");

                var IsConstructor = true;
                var ReturnType = "";

                if (r.Index != -1)
                {
                    IsConstructor = false;
                    ReturnType = r.AfterSubject;
                }

                var n = new MethodInfo
                {
                    Flags = Flags.Text,
                    Signature = Signature.Text,
                    Summary = Summary.Text,
                    DefinedBy = DefinedBy.Text,
                    ParametersInfo = new MethodParametersInfo(p),
                    Name = a.BeforeSubject,
                    IsConstructor = IsConstructor,
                    ReturnType = ReturnType
                };

                // do last minute checks here


                return new Tuple<MethodInfo, IndexInfo>
                {
                    TValue = n,
                    FValue = DefinedBy
                };
            }
        }

        [Script]
        public class ConstantInfo
        {
            public string Signature;
            public string Summary;
            public string DefinedBy;

            public bool IsAirOnly;

            public const string KeywordAirOnly = "AIR-only";

            public string Type;
            public string Name;
            public string Value;


            internal static Tuple<ConstantInfo, IndexInfo> ScanNext(IndexInfo LastIndex)
            {
                var Signature = LastIndex.GetNextTrimmedLine();

                if (!Signature.Text.Contains(":"))
                    return null;

                var a = Signature.Text.IndexInfoOf(":");
                

                var IsAirOnly = false;

                var Name = a.BeforeSubject.Trim();
                if (Name.StartsWith(KeywordAirOnly))
                {
                    Name = Name.Substring(KeywordAirOnly.Length + 1).Trim();
                    IsAirOnly = true;
                }

                var b = a.IndexInfoOf("=");

                var Type = "";
                var Value = "";

                if (b.Index == -1)
                    Type = a.AfterSubject.Trim();
                else
                {
                    Type = a.SubString(b).Trim();
                    Value = b.AfterSubject.Trim();
                }


                var Summary = Signature.GetNextTrimmedLine();

                var DefinedBy = Summary.GetNextTrimmedLine();

                var n = new ConstantInfo
                {
                    Signature = Signature.Text,
                    Summary = Summary.Text,
                    DefinedBy = DefinedBy.Text,
                    IsAirOnly = IsAirOnly,
                    Type = Type,
                    Name = Name,
                    Value = Value
                };

                return new Tuple<ConstantInfo, IndexInfo>
                {
                    TValue = n,
                    FValue = DefinedBy
                };
            }
        }

        public readonly List<PropertyInfo> Properties = new List<PropertyInfo>();
        public readonly List<MethodInfo> Functions = new List<MethodInfo>();
        public readonly List<ConstantInfo> Constants = new List<ConstantInfo>();

        public IEnumerable<MethodInfo> GetInstanceConstructors()
        {
            return from i in Functions
                   where !i.IsInherited
                   where i.IsConstructor
                   select i;
        }

        public IEnumerable<MethodInfo> GetInstanceMethods()
        {
            return from i in Functions
                   where !i.IsInherited
                   where i.IsMethod
                   select i;
        }

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
            if (PropertyHeader.Index == -1)
                PropertyHeader = ClassEOL.IndexInfoOf(" \tProperty\tDefined By");

            var MethodHeader = ClassEOL.IndexInfoOf(" \tMethod\tDefined by");
            if (MethodHeader.Index == -1)
                MethodHeader = ClassEOL.IndexInfoOf(" \tMethod\tDefined By");

            var ConstantHeader = ClassEOL.IndexInfoOf(" \tConstant\tDefined by");
            if (ConstantHeader.Index == -1)
                ConstantHeader = ClassEOL.IndexInfoOf(" \tConstant\tDefined By");

            PropertyHeader.ScanToList(Properties, PropertyInfo.ScanNext);
            MethodHeader.ScanToList(Functions, MethodInfo.ScanNext);
            ConstantHeader.ScanToList(Constants, ConstantInfo.ScanNext);




        }




    }
}
