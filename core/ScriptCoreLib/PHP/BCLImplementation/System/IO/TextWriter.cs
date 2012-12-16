﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
    [Script(Implements = typeof(global::System.IO.TextWriter))]
    internal abstract class __TextWriter : IDisposable
    {
        public virtual void Write(string value)
        {
        }

        public virtual void WriteLine()
        {
            Write(Environment.NewLine);
        }

        public virtual void WriteLine(string value)
        {
            Write(value + Environment.NewLine);
        }

        public virtual void Flush()
        {

        }

        public abstract Encoding Encoding { get; }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
