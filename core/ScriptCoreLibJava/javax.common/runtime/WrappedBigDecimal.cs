using ScriptCoreLib;

using java.math;
using java.lang;


namespace javax.common.runtime.types
{
    [Script]
	[System.Obsolete]
	public class WrappedBigDecimal
    {
        public readonly BigDecimal Value;

        public WrappedBigDecimal(BigDecimal e)
        {
            Value = e;
        }

        public static implicit operator WrappedBigDecimal(BigDecimal e)
        {
            return new WrappedBigDecimal(e);
        }

        public static implicit operator BigDecimal(WrappedBigDecimal e)
        {
            return e.Value;
        }

        public static implicit operator double(WrappedBigDecimal e)
        {
            return e.Value.doubleValue();
        }

        public static implicit operator float(WrappedBigDecimal e)
        {
            return e.Value.floatValue();
        }

        public override string ToString()
        {
            return Value + "";
        }
    
        

        public static implicit operator WrappedBigDecimal(double e)
        {
            return new WrappedBigDecimal(new BigDecimal(e));
        }

        public static implicit operator int(WrappedBigDecimal e)
        {
            return e.Value.intValue();
        }

        public static implicit operator WrappedBigDecimal(int e)
        {
            return new WrappedBigDecimal(new BigDecimal(e));
        }

        public static explicit operator WrappedBigDecimal(string e)
        {
            double x = 0;

            try { x = double.Parse(e); }
            catch { }

            return (WrappedBigDecimal) x;
        }


        public static WrappedBigDecimal operator +(WrappedBigDecimal a, WrappedBigDecimal b)
        {
            return a.Value.add(b);
        }

        public static WrappedBigDecimal operator -(WrappedBigDecimal a, WrappedBigDecimal b)
        {
            return a.Value.subtract(b);
        }

        public static WrappedBigDecimal operator *(WrappedBigDecimal a, WrappedBigDecimal b)
        {
            return a.Value.multiply(b);
        }

        public static WrappedBigDecimal operator /(WrappedBigDecimal a, WrappedBigDecimal b)
        {
            return a.Value.divide(b, 20, BigDecimal.ROUND_CEILING);
        }

        public static WrappedBigDecimal operator ^(WrappedBigDecimal a, WrappedBigDecimal b)
        {
            return Math.pow(a, b);
        }


    }
}
