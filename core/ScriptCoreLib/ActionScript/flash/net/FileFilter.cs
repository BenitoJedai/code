using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.net
{
	// http://livedocs.adobe.com/flex/3/langref/flash/net/FileFilter.html
	[Script(IsNative = true)]
	public class FileFilter
	{
		#region Properties
		/// <summary>
		/// The description string for the filter.
		/// </summary>
		public string description { get; set; }

		/// <summary>
		/// A list of file extensions.
		/// </summary>
		public string extension { get; set; }

		/// <summary>
		/// A list of Macintosh file types.
		/// </summary>
		public string macType { get; set; }

		#endregion

		#region Methods
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a new FileFilter instance.
		/// </summary>
		public FileFilter(string description, string extension, string macType)
		{
		}

		/// <summary>
		/// Creates a new FileFilter instance.
		/// </summary>
		public FileFilter(string description, string extension)
		{
		}

		#endregion

	}
}
