using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
	// filesystem IO ? WebFiles?
	[Script(HasNoPrototype = true)]
	public class File : Blob
	{
		// can we thread jump a file yet? to worker, to server?
		// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTab\ChromeExtensionHopToTab\Application.cs


		public readonly string type;
		public readonly string name;

		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140910
	}
}
