using System;
using ScriptCoreLib;
using System.Collections.Generic;


namespace TestGetEnumerator
{
    namespace JavaScript
    {
        [Script]
        class List<T> : IEnumerable<T>
        {

            public IEnumerator<T> GetEnumerator()
            {
                throw null;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                throw null;
            }
        }
    }

    internal static class Program
    {
        public static void Main(string[] args)
        {

        }
    }
}
