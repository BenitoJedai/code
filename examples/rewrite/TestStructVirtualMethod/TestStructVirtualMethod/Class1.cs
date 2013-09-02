using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TestStructVirtualMethod
{
    public struct VBxStateMachine : IAsyncStateMachine
    {

        void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
        {
            throw new NotImplementedException();
        }

        public void MoveNext()
        {
            throw new NotImplementedException();
        }
    }
}
