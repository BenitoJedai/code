﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [ScriptCoreLib.Script(Implements = typeof(global::System.Windows.Forms.TabControlEventHandler))]
    internal delegate void __TabControlEventHandler(object sender, TabControlEventArgs e);
}
