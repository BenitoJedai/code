using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://www.w3.org/TR/domcore/#interface-domerror
	[Script(HasNoPrototype = true, ExternalTarget = "Error")]
	public class IError 
	{
		public string name;
	}
}
