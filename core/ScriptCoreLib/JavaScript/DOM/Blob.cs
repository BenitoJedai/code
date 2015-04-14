using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
	// http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Blob.webidl
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/fileapi/Blob.idl

	// https://github.com/bridgedotnet/Bridge/blob/master/Html5/File/Blob.cs

	[Script(HasNoPrototype = true, ExternalTarget = "Blob")]
	public class Blob
	{
		public readonly ulong size;

		public Blob()
		{

		}


		// http://stackoverflow.com/questions/19327749/javascript-blob-filename-without-link

		public Blob(string[] e)
		{

		}

		public Blob(string[] e, object args)
		{

		}
	}

	[Script]
	public static class BlobExtensions
	{
		// X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs

		[Obsolete("jsc faults?")]
		public static string ToObjectURL(this Blob e)
		{
			// tested by
			// X:\jsc.svn\examples\javascript\ScriptDynamicSourceBuilder\ScriptDynamicSourceBuilder\Application.cs

			//Console.WriteLine("ToObjectURL");

			// is jsc trying to inline without the line above?
			return URL.createObjectURL(e);
		}
	}
}
