using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.WebService
{
    /// <summary>
    /// The compiler will gather details of which files are included in the
    /// current web application.
    /// 
    /// The server side application has a collection of those files.
    /// </summary>
	public class InternalFileInfo
	{
		public string Name;

        public int Length;
	}
}
