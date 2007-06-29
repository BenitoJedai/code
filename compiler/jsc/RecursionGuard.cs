using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace jsc
{
    public class RecursionGuard : IDisposable
    {
        bool _locked;
        int _depth;
        int _stack;

        public RecursionGuard(int depth)
        {
            _depth = depth;
            _stack = 0;
        }

        public RecursionGuard()
        {
        }

        RecursionGuard _parent;

        [DebuggerNonUserCode, DebuggerHidden]
        private RecursionGuard(RecursionGuard parent)
        {
            if (parent._locked && parent._stack >= parent._depth)
            {
                throw new Exception("recursion detected at stack " + parent._stack);

            }

            parent._locked = true;
            parent._stack++;
            
            _parent = parent;
        }

       
        public RecursionGuard Lock
        {
            [DebuggerNonUserCode, DebuggerHidden]
            get
            {
                return new RecursionGuard(this);
            }
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            _parent._locked = false;
            _parent._stack--;
        }

        #endregion
}
}
