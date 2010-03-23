using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	public partial class ReferenceUltraSource : CommandBase
	{
		// todo: we need to add cs/vb/fs compiler support

		// we might want to only reference the files from the internet...
		// we could save a local copy as a dependant file
		// the local copy would be saved in the svn
		// then the generated assembly would use the same assets 
		// on different build machines

		// maybe in the future we could
		// point to a flash game and say we'd like to reuse
		// all the assets? :)
		// with consent ofcourse!

		// html file could be used
		// for comments and meta stuff

		// is this superseded by ReferenceJavaScriptDocument

		// yay new branding? :) 
		// step 2 enable sub folders
		// step 3 do not event filter by folder just import everything
		public const string UltraSource = "UltraSource";

		// todo: to be phased out once moved to ReferenceUltraSource
		const string WebSource_HTML = "WebSource.HTML";

		public override void Invoke()
		{
			
		}
	}
}
