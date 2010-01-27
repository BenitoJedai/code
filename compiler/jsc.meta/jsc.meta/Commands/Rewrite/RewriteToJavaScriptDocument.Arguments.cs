using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace jsc.meta.Commands.Rewrite
{
	partial class RewriteToJavaScriptDocument 
	{
		/* usage:
				if $(ConfigurationName)==Debug goto :eof
				c:\util\jsc\bin\jsc.meta.exe RewriteToJavaScriptDocument /assembly:"$(TargetFileName)" /flashplayer:"C:\util\flex\runtimes\player\win\FlashPlayer.exe" /mxmlc:"C:\util\flex\bin\mxmlc.exe" /javapath:"c:\Program Files\Java\jdk1.6.0_14\bin"
		 */


		public DirectoryInfo staging;

		public FileInfo assembly;

		public FileInfo mxmlc;
		public FileInfo flashplayer;

		public DirectoryInfo javapath;

		public bool debug1;

	}
}
