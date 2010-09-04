using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Delegates;

namespace jsc.meta.Library.Mashups
{
	internal sealed partial class UltraWebServiceWithEvents
	{
        // 1. String / StringAction
        // 2. XElement / XElementAction
        // 3. String / Action<string>
        // 4. XElement / Action<XElement>
        // 5. object with implict operator to/from XElement / Action<object with implict operator to/from XElement>
        // 6. object / Action<object> where operator to/from XElement is inferred

        // The events and properties shall be initialized before calling Method1
        // The properties modified by the Method1 will be sent back to the client
        // The events triggered will be raised on the client in the correct order.

        public event Action<string> YieldByEvent;

        public string Property1 { get; set; }

		public void Method1(string input, StringAction yield)
		{
			// running in .net, GAEJava, php


		}
	}
}
