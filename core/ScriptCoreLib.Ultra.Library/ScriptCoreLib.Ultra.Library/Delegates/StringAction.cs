using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Library.Delegates
{
	// if only we had generic here :) just not yet

	public delegate void XElementAction(XElement x);

	public delegate void StringAction(string e);

	public delegate void DoubleAction(double e);

}
