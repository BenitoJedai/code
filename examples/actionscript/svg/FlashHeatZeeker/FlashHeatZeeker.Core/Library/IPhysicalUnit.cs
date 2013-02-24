using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.Core.Library
{
    public interface IPhysicalUnit
    {
        void ShowPositionAndAngle();
        void ApplyVelocity();
        void SetVelocityFromInput(object[] __keyDown);

    }
}
