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
		 * 
		 * roadmap/todo:
		 * 1. events as arguments
		 * 2. delay bridge until loaded
		 * 3. visual basic example
		 * 4. pass primitives and interfaces as arguments
		 */


		public DirectoryInfo staging;

		public FileInfo assembly;

		public FileInfo mxmlc;
		public FileInfo flashplayer;

		public DirectoryInfo javapath;


	}
}
