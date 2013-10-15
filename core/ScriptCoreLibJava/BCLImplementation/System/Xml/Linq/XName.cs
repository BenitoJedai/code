using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml.Linq;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
    [Script(Implements = typeof(global::System.Xml.Linq.XName))]
    internal class __XName
    {
        public string InternalValue;

        public string LocalName
        {
            get
            {
                return InternalValue;
            }

            //    java.lang.RuntimeException
            //at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XElement.set_Name(__XElement.java:118)
            //at PageNavigationExperiment.ApplicationWebService.Handler(ApplicationWebService.java:149)

            //set
            //{
            //    // tested by
            //    // X:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\ApplicationWebService.cs

            //    InternalValue = value;
            //}
        }
        public override string ToString()
        {
            return LocalName;
        }

        public static implicit operator __XName(string e)
        {
            return __XName.Get(e);
        }

        public static __XName Get(string e)
        {
            return new __XName { InternalValue = e };
        }

        public static XName Get(string localName, string namespaceName)
        {
            return (XName)(object)new __XName { InternalValue = localName };
        }


        public static implicit operator XName(__XName e)
        {
            return (XName)(object)e;
        }
    }
}
