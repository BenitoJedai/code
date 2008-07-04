using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ConvertASToCS.js.Any
{
    [Script]
    public class Disposable : IDisposable
    {
        Action _e;

        public Disposable(Action e)
        {
            _e = e;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_e != null)
                _e();
        }

        #endregion
    }
}
