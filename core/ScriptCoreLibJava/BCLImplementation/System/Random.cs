using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/random.cs

    [Script(Implements = typeof(global::System.Random))]
    internal class __Random
    {
        public virtual void NextBytes(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)Next(0, 0xFF);
            }
        }

        public virtual int Next()
        {
            return Next(0, int.MaxValue);
        }

        public virtual int Next(int max)
        {
            // X:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\ApplicationWebService.cs

            return Next(0, max);
        }


        public virtual int Next(int min, int max)
        {
            var len = max - min;
            var r = global::java.lang.Math.floor(java.lang.Math.random() * len);

            int ri = (int)r;
            return ri + min;
        }

        public virtual double NextDouble()
        {
            return java.lang.Math.random();
        }
    }
}
