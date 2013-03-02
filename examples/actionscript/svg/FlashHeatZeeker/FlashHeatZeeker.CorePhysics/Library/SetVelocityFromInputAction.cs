using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.CorePhysics.Library
{
    public delegate void SetVelocityFromInputAction(
        string __egoid,
        string __identity,
        string __KeySample,
        string __fixup_x,
        string __fixup_y,
        string __fixup_angle
    );

}
