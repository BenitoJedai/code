using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace ScriptCoreLib.JavaScript.Controls.Effects
{
    [Script]
    public class TweenDataPoint : TweenData<Point>
    {
        public TweenDataPoint(EventHandler e)
            : this()
        {
            this.ValueChanged += e;
        }


        public TweenDataPoint()
        {
            this.Tick += delegate
            {
                var z = (this.CurrentValue + this.FutureValue) / 2;

                this.CurrentValue = round(z);

        
                this.RaiseValueChanged();
            };

            this.FutureValueChanged +=
                delegate
                {
                    this.FutureValue = round(this.FutureValue);
                };



            this.IsCloseEnoughHandler +=
                delegate(Predicate p)
                {
                    Point z = this.CurrentValue - this.FutureValue;

                    if (Native.Math.abs(z.X) > 1)
                        return;

                    if (Native.Math.abs(z.Y) > 1)
                        return;

                    p.Value = true;
                };
        }

        Point round(Point z)
        {
            return new Point(round(z.X), round(z.Y));

        }

        int round(double z)
        {


            return Native.Math.round(z);
        }



    }


    [Script]
    public class TweenDataDouble : TweenData<double>
    {
        public TweenDataDouble(EventHandler e)
            : this()
        {
            this.ValueChanged += e;
        }

        public TweenDataDouble()
        {
            this.Tick += delegate
            {
                var z = (this.CurrentValue + this.FutureValue) / 2;

                this.CurrentValue = round(z);
  
                this.RaiseValueChanged();
            };

            this.FutureValueChanged +=
                delegate
                {
                    this.FutureValue = round(this.FutureValue);
                };



            this.IsCloseEnoughHandler +=
                delegate(Predicate p)
                {
                    p.Value = Native.Math.abs(this.CurrentValue - this.FutureValue) < 0.05;
                };
        }

        double round(double z)
        {
            z *= 100;

            z = Native.Math.round(z);

            z /= 100;

            return z;
        }



    }

}
