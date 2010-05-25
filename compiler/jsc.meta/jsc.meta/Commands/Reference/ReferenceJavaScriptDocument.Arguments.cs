using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using jsc.Languages.IL;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.meta.Tools;
using jsc.Script;
using Microsoft.CSharp;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace jsc.meta.Commands.Reference
{

	partial class ReferenceJavaScriptDocument
	{

		/*
		 usage:
		 * C:\util\jsc\bin\jsc.meta.exe ReferenceJavaScriptDocument /ProjectFileName:"$(ProjectPath)"
		 * 
		 
		 * ReferenceJavaScriptDocument /ProjectFileName:"W:\jsc.svn\examples\java\PromotionWebApplication\PromotionWebApplication1\PromotionWebApplication1.csproj"
		 */

		// should be merged with ReferenceWebSource ?
		// should be renamed to ReferenceUltraWebSource ?
		// should be renamed to ReferenceHTMLDocument ?
		// should be renamed to ReferenceWhateveryouLike? HTML documents are just documentation? :)
		// maybe by adding an alias?

		// todo: What about zip files?
		// todo: Add on demand Asset Pages. We dont need all that html up front
		// todo: We want assets for flash! Including mp3!
		// todo: Seamless use of Avalon/Forms needs to be tested
		// todo: Referenced files as ScriptResources
		// todo: could we parse js content and make it callable, how do we infer types? :) we could just expose IFunctions :) sounds good!
		// todo: some system should scan the source and send todo's to twitter thanks...
		// todo: whatif i want to use some types within flash? classes should be made for flash too! in IsMerge mode atleast.
		// todo: whatif i want to reuse java source or as3 source? they should also be scanned. Something alike ReferenceWebSource
		// todo: scan for microformats?

		// trivia: in fact we are referencing HTML to convert it to javascript.

		// http://www.technospot.net/blogs/convert-html-to-javascript-dom-online-tool/

		// user drops an html file
		// ScriptCoreLib.JavaScript.DOM tree will be built

		// images should be downloaded and packaged as assets

		 

	
		// The new way to reference web resources would be within the HTML documents.
		const string __References = "references.txt";

		public FileInfo ProjectFileName;



		/// <summary>
		/// This assembly is to be merged and rewritten.
		/// 
		/// This will be always the case. :) Then we can generate more than needed. And the user chooses
		/// from what has been made available.
		/// 
		/// Should always be on!
		/// </summary>
        [Obsolete("JSC will be generating multiple variations of types of which not all will be compiled into the final product.")]
		public bool IsMerge = true;

		public bool AttachDebugger;

	}
}
