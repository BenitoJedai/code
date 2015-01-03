using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Xml.Linq
{
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Xml/Linq/Extensions.cs

    [Script(Implements = typeof(global::System.Xml.Linq.Extensions))]
	internal static class __Extensions
	{
        // roslyn 
        //    0200016a ScriptCoreLib.Shared.BCLImplementation.System.Xml.Linq.__Extensions
        //{ SourceMethod = System.Collections.Generic.IEnumerable`1[System.Xml.Linq.XElement] Elements[T](System.Collections.Generic.IEnumerable`1[T]) }


        public static IEnumerable<XElement> Elements<T>(IEnumerable<T> source) where T : XContainer
        {
            // X:\jsc.svn\examples\javascript\Test\Test453LINQSum\Test453LINQSum\Class1.cs
            // X:\jsc.svn\examples\javascript\Test\Test435SelectManyDelegate\Test435SelectManyDelegate\Class1.cs
            return source.SelectMany(k => k.Elements());
        }

		public static IEnumerable<XElement> Elements<T>(IEnumerable<T> source, XName name) where T : XContainer
		{
			return source.SelectMany(k => k.Elements(name));
		}
	}
}
