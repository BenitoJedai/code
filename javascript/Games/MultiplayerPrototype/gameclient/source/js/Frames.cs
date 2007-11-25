using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NatureBoy.js;
using ScriptCoreLib;

namespace gameclient.source.js
{
    [Script]
    static class Frames
    {

        static FrameInfo[] _Harvester1;

        public static FrameInfo[] Harvester1
        {
            get
            {
                if (_Harvester1 == null)
                    _Harvester1 =

                        32.Range(i => 71 + ((32 - i) + 24) % 32)
                        .Select(i =>
                        new FrameInfo
                        {
                            Source = "fx/harvester_1/" + i + ".png",
                            Weight = 1d / 32d,
                            OffsetY = -24
                        }
                        ).ToArray();

                return _Harvester1;
            }
        }
    }
}
