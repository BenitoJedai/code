using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.meta.Commands
{
	public class ReferenceAssets
	{
		// we might want to only reference the files from the internet...
		// we could save a local copy as a dependant file
		// the local copy would be saved in the svn
		// then the generated assembly would use the same assets 
		// on different build machines

		// maybe in the future we could
		// point to a flash game and say we'd like to reuse
		// all the assets? :)
		// with consent ofcourse!
	}
}
