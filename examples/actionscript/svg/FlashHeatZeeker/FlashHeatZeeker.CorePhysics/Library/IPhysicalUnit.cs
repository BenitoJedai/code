using Box2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.Core.Library
{
    public class DriverSeat
    {
        public IPhysicalUnit driver;
    }

    public interface IPhysicalUnit
    {
        DriverSeat driverseat { get; set; }
        b2Body body { get; }

        void ShowPositionAndAngle();
        void ApplyVelocity();
        void SetVelocityFromInput(object[] __keyDown);

    }
}
