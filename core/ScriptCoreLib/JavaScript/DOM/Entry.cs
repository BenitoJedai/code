using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
	// http://src.chromium.org/viewvc/blink/trunk/Source/modules/filesystem/Entry.idl

	// .WebFileSystem
	// .IOFileSystem ?
	[Script(HasNoPrototype = true, ExternalTarget = "")]
	public class Entry
	{
		// X:\jsc.svn\examples\javascript\chrome\apps\ChromeCSVFileHandler\ChromeCSVFileHandler\Application.cs

		public bool isFile;
		public string name;
	}
}
