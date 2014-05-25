using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenericSwitchRewriter
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class ZProgram<TElement> : IEnumerator
    {


        public object Current
        {
            get { throw new NotImplementedException(); }
        }

        public bool MoveNext<TOther>(IEnumerable<TElement> value, Func<TElement, TOther> s, Func<TOther, bool> filter)
        {
            return value.Select(s).Any(filter);
        }

        //public async void Foo<TElement>(TElement that, int delay, Action<TElement> h)
        //{
        //    // PEVerify [IL]: Error: [X:\jsc.svn\examples\rewrite\test\TestGenericSwitchRewriter\TestGenericSwitchRewriter\bin\Debug\xTestGenericSwitchRewriter.exe : TestGenericSwitchRewriter.Program+<Foo>d__0`1[TElement]::MoveNext]  [HRESULT 0x8007000B] - An attempt was made to load a program with an incorrect format.

        //    h(that);

        //    //return value.Any(filter);
        //}

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
