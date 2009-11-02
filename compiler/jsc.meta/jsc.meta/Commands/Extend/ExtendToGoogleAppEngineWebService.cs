using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace jsc.meta.Commands.Extend
{
	public class ExtendToGoogleAppEngineWebService
	{
		// asp.net web service
		// running on gae
		// this implementation could be shared
		// with php to some extent
		
		public FileInfo assembly;
		public DirectoryInfo staging;

		public FileInfo ant;
		public DirectoryInfo appengine;

		public string application;
		public string version;

		public void Invoke()
		{
			// find all WebMethods
			// create a wapper
			// compile with ant

			// we should rewrite source for jsc
		}
	}
}
