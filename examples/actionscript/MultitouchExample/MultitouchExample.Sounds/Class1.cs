using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "merge")]

namespace MultitouchExample.Sounds
{
    public static class building
    {
        public static readonly string Source = "assets/MultitouchExample.Sounds/building.mp3";
    }

    public static class launch
    {
        public static readonly string Source = "assets/MultitouchExample.Sounds/launch.mp3";
    }
}
