using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/random.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Random.cs

    [Script(Implements = typeof(global::System.Random))]
    internal class __Random
    {
        public __Random()
        {

        }

		public virtual void NextBytes(byte[] buffer)
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)Next(0, 0xFF);
			}
		}

        public virtual int Next()
        {
            //  A 32-bit signed integer greater than or equal to zero and less than System.Int32.MaxValue.

            return Native.Math.round(NextDouble() * global::System.Int32.MaxValue);
        }

        public virtual int Next(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new Exception("ArgumentOutOfRange_MustBePositive");
            }

            return Native.Math.round(this.NextDouble() * ((double)maxValue));
        }


        public virtual int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new Exception("Argument_MinMaxValue");
            }

            return Next(maxValue - minValue) + minValue;
        }



        public virtual double NextDouble()
        {
            return Native.Math.random();
        }
    }


}
