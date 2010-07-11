using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using jsc.meta.Commands.Rewrite.RewriteToInstaller.Templates;
using jsc.meta.Library;
using ScriptCoreLib.Archive.ZIP;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace jsc.meta.Commands.Rewrite.RewriteToInstaller
{
	partial class RewriteToInstaller
	{
		// http://notgartner.wordpress.com/2010/03/04/what-does-a-finished-product-look-like/
		// we are able to generate a console installer for jsc for now
		// we could Create an ultra application instead
		// if the template is provided we could just provide
		// our information and and the installer is already a web/desktop app.
        //public string TemplateAssembly;
        //public string TemplateWebService;

		public bool AttachDebugger;

		public bool Obfuscate = false;

		public string Feature;

		internal const string jsc_installer_zip = "jsc.installer.zip";

		/// <summary>
		/// The generated shell should check if there is a newer version available.
		/// If there is it should download it instead.
		/// 
		/// We can simply load that new asembly and load its new payload into our shell.
		/// 
		/// The payload should also know when it's out of date to invoke the installer again
		/// for updates.
		/// </summary>
		public Uri AutoUpdateLocation = new Uri("http://download.jsc-solutions.com");

		// we are wrapping our thing inside a splash screen, if specified
		public RewriteToSplashScreen.RewriteToSplashScreen Splash;

	}

}
