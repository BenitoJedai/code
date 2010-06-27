using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.meta.Library.CodeTrace
{
    public interface ICodeTrace : IDisposable
    {
        void Invoke(Action e);
    }
}
