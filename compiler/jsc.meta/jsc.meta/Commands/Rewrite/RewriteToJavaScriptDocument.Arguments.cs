using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using jsc.meta.Tools;
using jsc.meta.Configuration;

namespace jsc.meta.Commands.Rewrite
{
	partial class RewriteToJavaScriptDocument 
	{
		/* usage:
				if $(ConfigurationName)==Debug goto :eof
				c:\util\jsc\bin\jsc.meta.exe RewriteToJavaScriptDocument /assembly:"$(TargetFileName)" /flashplayer:"C:\util\flex\runtimes\player\win\FlashPlayer.exe" /mxmlc:"C:\util\flex\bin\mxmlc.exe" /javapath:"c:\Program Files\Java\jdk1.6.0_14\bin"
		 * 
		 * roadmap/todo
		 * 1. rewrite as single application first, rewriting IL and simplify
		 * 2.
		 * 3. visual basic example
		 * 4. pass primitives and interfaces as arguments
		 */

		public DirectoryInfo staging;

		public FileInfo assembly;

		// we should enable the use of settings files!

		public DirectoryInfo javahome = SDKConfiguration.Default.JavaSDK; // new DirectoryInfo(@"C:\Program Files\Java\jdk1.6.0_14");
		// we probably do not need both :)
        //public DirectoryInfo javapath = new DirectoryInfo(@"c:\Program Files\Java\jdk1.6.0_14\bin");

        public FileInfo ant = SDKConfiguration.Default.ApacheAntSDK_ant;
		public DirectoryInfo appengine = SDKConfiguration.Default.GoogleAppEngineJavaSDK;

		public FileInfo mxmlc = ToolsExtensions.Defaults.mxmlc;
		public FileInfo flashplayer = ToolsExtensions.Defaults.flashplayer;


		public bool AttachDebugger;

		public bool IsRewriteOnly;

        // less is more :)
		public bool DisableWebServiceJava = true;
        public bool DisableWebServicePHP = true;

        public class WebServiceJavaSettingsType
        {
            public string application = "jsc-project";
            public string version = "5";
        }

        public WebServiceJavaSettingsType WebServiceJavaSettings = new WebServiceJavaSettingsType();

		/// <summary>
		/// Server side debugging scenario is enabled by setting DisableWebServiceTypeMerge to true.
		/// One can now insert Debug.Break statements into the WebService class and the original
		/// assembly will be used.
		/// </summary>
		public bool DisableWebServiceTypeMerge;

		public class AtWebServiceReadyArguments
		{
			public FileInfo Assembly;

			public string GlobalType;

			public FileInfo WebDevLauncher;
		}

		/// <summary>
		/// Rewrite was done at runtime and the caller wants to start using the new
		/// component once it is ready.
		/// </summary>
		public event Action<AtWebServiceReadyArguments> AtWebServiceReady;

		public event Action<string> ProccessStatusChanged;

		public void RaiseProccessStatusChanged(string e)
		{
			if (ProccessStatusChanged != null)
				ProccessStatusChanged(e);
		}
	}
}
