using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReadOnlyArrayAsList
{
    public struct ReadonlyArray<T>
    {

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201306/20120626-roslyn


        public IList<T> AsList()
        {
            return SpecializedCollections.EmptyList<T>();
        }
    }

    public static class SpecializedCollections
    {
        public static IList<T> EmptyList<T>()
        {
            return null;
        }
    }
}
