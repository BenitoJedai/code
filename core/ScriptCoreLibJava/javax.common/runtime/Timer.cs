using ScriptCoreLib;

using Exception = global::System.Exception;

using java;
using java.lang;
using java.io;
using java.util;
using java.text;

namespace javax.common.runtime
{
    using IDisposable = global::System.IDisposable;


    [Script]
	// yay! some business applications use this... revert to warning!
	[System.Obsolete("Use Thread or Timer instead", false)]
    public class Timer
    {

        [Script]
        class DisposableTimer : IDisposable
        {
            public long Start = new Date().getTime();
            public long End;

            public void Dispose()
            {
                End = new Date().getTime();

            }
        }

        DisposableTimer Value;

        public long ElapsedMilliseconds
        {
            get
            {
                return Value.End - Value.Start;
            }
        }

        public void ToConsole()
        {
            ToConsole("Elapsed time:");
        }

        public void ToConsole(string of)
        {
            System.Console.WriteLine(of + " " + ElapsedMilliseconds + "ms");
        }


        public IDisposable Measure
        {
            get
            {
                this.Value = new DisposableTimer();

                return this.Value;
            }
        }
    }

}
