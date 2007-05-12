using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Random))]
    internal class __Random
    {
        public __Random()
        {

        }

        public virtual int Next()
        {
            return Native.Math.round(NextDouble() * 0xFFFFFFFF);
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
            if (minValue <= maxValue)
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
