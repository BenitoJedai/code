﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.PHP.DOM;

namespace ScriptCoreLib.PHP.BCLImplementation.System.XML.XLinq
{
    [Script(Implements = typeof(XNode))]
    internal class __XNode : __XObject
    {
        internal DOMNode InternalValue;

    }
}
