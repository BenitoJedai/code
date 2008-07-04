using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ConvertASToCS.js.Any
{
    [Script]
    public class ConverterBase
    {

        [Script]
        public class FieldInfo
        {
            public bool IsPrivate;
            public bool IsReadOnly;

            public string FieldName;

            public string TypeName;
        }

  

        [Script]
        public class TypeInfo
        {
            public string Namespace;

            public string Name;

            public string BaseTypeName;

            public bool IsSealed;

            public FieldInfo[] Fields;

            public bool NoAttributes;
        }

        public static readonly List<string> CSharpKeywords = new List<string>
            {
                "namespace",
                "params",
                "event",
                "static",
                "public",
                "private",
                "internal",
                "class",
                "interface",
                "get",
                "set",
                "object",
                "for",
                "as",
                "in",
                "out",
                "object",
                "double",
                "string",
                "bool",
                "int",
                "uint",
            };

        public static string FixVariableName(string Name)
        {




            if (CSharpKeywords.Contains(Name))
                return "@" + Name;

            return Name;
        }

        public static string FixTypeName(string TypeName)
        {
            var dict = new Dictionary<string, string>
                                {
                                    {"*", "object"},
                                    {"Object", "object"},
                                    {"Number", "double"},
                                    {"String", "string"},
                                    {"Boolean", "bool"},
                                };

            if (dict.ContainsKey(TypeName))
                TypeName = dict[TypeName];

            return TypeName;
        }
    }
}
