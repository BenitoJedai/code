using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.PHP.IO;

namespace jsc.meta.Commands.Extend
{
	public class ExtendToAvalonEverywhere
	{
		public FileInfo assembly;
		public string type;
		public DirectoryInfo staging;
		public FileInfo zip;
		public DirectoryInfo flexpath;

		/// <summary>
		/// Setting this field to false disables javascript generation.
		/// </summary>
		public bool javascript = true;

		/// <summary>
		/// The swf file shall be converted to an exe file.
		/// </summary>
		public bool flashprojector = false;

		public void Invoke()
		{

			new Builder
			{

			}.Invoke();
		}

		public class Builder
		{
			public void Invoke()
			{

			}
		}
	}
}
