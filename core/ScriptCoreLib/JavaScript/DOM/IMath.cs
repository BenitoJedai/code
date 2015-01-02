using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM
{
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/Microsoft/LiveLabs/JavaScript/JSMath.cs

    [Script(HasNoPrototype = true)]
    public partial class IMath
    {
        public double abs(double e) { return default(double); }
        public double acos(double e) { return default(double); }
        public double asin(double e) { return default(double); }
        public double atan(double e) { return default(double); }
        public double atan2(double x, double y) { return default(double); }


        public int ceil<T>(T e) { return default(int); }
        public int floor<T>(T e) { return default(int); }

        public double cos(double e) { return default(double); }
        public double exp(double e) { return default(double); }

        public double log(double e) { return default(double); }
        public double sin(double e) { return default(double); }
        public double sqrt(double e) { return default(double); }
        public int round(double e) { return default(int); }
        public double tan(double e) { return default(double); }
        public double random() { return default(double); }


        public uint max(uint e, uint f) { return default(uint); }
        public int max(int e, int f) { return default(int); }
        public ushort max(ushort e, ushort f) { return default(int); }
        public double max(double e, double f) { return default(double); }
        public float max(float e, float f) { return default(float); }




        public int min(int e, int f) { return default(int); }
        public double min(double e, double f) { return default(double); }
        public float min(float e, float f) { return default(float); }

        public double pow(double e, double f) { return default(double); }

        public double E;
        public double PI;
        public double SQRT2;
        public double SQRT1_2;
        public double LN2;
        public double LN10;
        public double LOG2E;
        public double LOG10E;
    }
}