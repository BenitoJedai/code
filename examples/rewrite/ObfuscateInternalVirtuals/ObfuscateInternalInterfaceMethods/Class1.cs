using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObfuscateInternalInterfaceMethods
{
    public delegate void FooAction();

    public interface IFoo
    {
        void Foo();
    }


    class XFoo : IFoo, IDisposable
    {

        public void Foo()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
