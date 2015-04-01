using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.IO
{
    // http://referencesource.microsoft.com/#mscorlib/system/io/textwriter.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.IO/TextWriter.cs

    [Script(Implements = typeof(global::System.IO.TextWriter))]
    internal abstract class __TextWriter : IDisposable
    {
		// X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs

		public virtual void Write(object value)
        {
        }

		public virtual void Write(char value)
		{
		}

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

        public virtual void WriteLine(object value)
        {
            Write(value);
            WriteLine();
        }

        public virtual void Flush()
        {

        }

        public abstract Encoding Encoding { get; }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
