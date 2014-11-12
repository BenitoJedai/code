using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    // http://referencesource.microsoft.com/#System.Xml.Linq
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Xml/Linq/XName.cs

    [Script(Implements = typeof(XName))]
    internal class __XName
    {
        internal string InternalValue;

        public string LocalName
        {
            get
            {
                return InternalValue;
            }
        }
        public override string ToString()
        {
            return LocalName;
        }

        public static implicit operator __XName(string e)
        {
            return new __XName { InternalValue = e };
        }

        public static XName Get(string localName)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\DataTypesForWebServiceExperiment\DataTypesForWebServiceExperiment\Application.cs

            return Get(localName, "");
        }

        public static XName Get(string localName, string namespaceName)
        {
            return (XName)(object)new __XName { InternalValue = localName };
        }

        public static bool operator ==(__XName left, __XName right)
        {
            return left.LocalName == right.LocalName;
        }

        public static bool operator !=(__XName left, __XName right)
        {
            return left.LocalName != right.LocalName;
        }

    }
}
