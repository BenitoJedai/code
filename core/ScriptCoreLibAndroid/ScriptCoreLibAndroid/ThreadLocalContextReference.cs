﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android
{
    [Script]
    public static class ThreadLocalContextReference
    {
        // this is a workaround until WebActivity finds a better way.
        public static android.content.Context CurrentContext;
    }
}
