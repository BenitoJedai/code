using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using System;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
	public partial class IHTMLDocument
	{
		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401
		// X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs
		// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTab\ChromeExtensionHopToTab\Application.cs

		// https://developer.mozilla.org/en-US/docs/Web/API/Document/currentScript
		// https://html.spec.whatwg.org/multipage/dom.html#dom-document-currentscript
		[Obsolete("is it supported for IE ? unavailable after onload")]
		public IHTMLScript currentScript;


	}
}
