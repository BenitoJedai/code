using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace AndroidOpenGLESLesson6Activity.Activities
{
    [Script(IsNative = true)]
    public static class R
    {
        [Script(IsNative = true)]
        public static class drawable
        {
            public static int stone_wall_public_domain;
            public static int noisy_grass_public_domain;
        }


        [Script(IsNative = true)]
        public static class layout
        {
            public static int main;
        }

        [Script(IsNative = true)]
        public static class id
        {
            public static int gl_surface_view;

            public static int button_set_min_filter;
            public static int button_set_mag_filter;
            
        }
    }
}
