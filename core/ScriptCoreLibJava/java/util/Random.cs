

using ScriptCoreLib;

namespace java.util
{
    /// <summary>
    /// 
    /// </summary>
    [Script(IsNative = true)]
    public class Random
    {
        // Constructor Summary
        /// <summary>
        /// Creates a new random number generator.
        /// </summary>
        public Random()
        {
        }

        /// <summary>
        /// Creates a new random number generator using a single
        /// </summary>
        public Random(long seed)
        {
        }

        // Method Summary
        /// <summary>
        /// Generates the next pseudorandom number.
        /// </summary>
        protected int next(int bits)
        {
            return default(int);
        }

        /// <summary>
        /// Returns the next pseudorandom, uniformly distributed
        /// </summary>
        public bool nextBoolean()
        {
            return default(bool);
        }

        /// <summary>
        /// Generates random bytes and places them into a user-supplied byte array.
        /// </summary>
        public void nextBytes(byte[] bytes)
        {
            return;
        }

        /// <summary>
        /// Returns the next pseudorandom, uniformly distributed
        /// </summary>
        public double nextDouble()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the next pseudorandom, uniformly distributed
        /// </summary>
        public float nextFloat()
        {
            return default(float);
        }

        /// <summary>
        /// Returns the next pseudorandom, Gaussian ("normally") distributed
        /// </summary>
        public double nextGaussian()
        {
            return default(double);
        }

        /// <summary>
        /// Returns the next pseudorandom, uniformly distributed
        /// </summary>
        public int nextInt()
        {
            return default(int);
        }

        /// <summary>
        /// Returns a pseudorandom, uniformly distributed
        /// </summary>
        public int nextInt(int n)
        {
            return default(int);
        }

        /// <summary>
        /// Returns the next pseudorandom, uniformly distributed
        /// </summary>
        public long nextLong()
        {
            return default(long);
        }

        /// <summary>
        /// Sets the seed of this random number generator using a single
        /// </summary>
        public void setSeed(long seed)
        {
            return;
        }


    }
}

