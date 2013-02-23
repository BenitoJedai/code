using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitHind.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.TestDrivers.Library
{
    public class StarlingGameSpriteWithTestDrivers : StarlingGameSpriteBase
    {
        public StarlingGameSpriteWithTestDrivers()
        {
            this.onbeforefirstframe += delegate
            {
                // can I have 
                // new ped, hind, jeep, tank

            };
        }
    }
}
