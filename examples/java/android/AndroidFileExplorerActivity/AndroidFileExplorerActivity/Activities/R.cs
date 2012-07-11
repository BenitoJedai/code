using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace AndroidFileExplorerActivity.Activities
{

    [Script(IsNative = true)]
    public static class R
    {


        [Script(IsNative = true)]
        public static class layout
        {
            public static int main;
            public static int row;
        }

        [Script(IsNative = true)]
        public static class id
        {
            public static int path;
            public static int list;
            public static int empty;


        }
    }
}
