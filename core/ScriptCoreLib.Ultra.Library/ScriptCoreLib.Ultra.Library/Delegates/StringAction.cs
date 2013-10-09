using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Delegates
{
	// if only we had generic here :) just not yet

    [Obsolete]
    public delegate void XElementAction(XElement x);

    [Obsolete]
    public delegate void StringAction(string e);

    [Obsolete]
    public delegate void DoubleAction(double e);

}
