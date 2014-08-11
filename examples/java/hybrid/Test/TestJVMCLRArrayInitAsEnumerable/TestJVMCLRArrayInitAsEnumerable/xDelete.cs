using System;
using System.Collections;
using System.Collections.Generic;

namespace TestJVMCLRArrayInitAsEnumerable
{
    internal class xDelete : IEnumerable
    {
        internal IEnumerable source;

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}