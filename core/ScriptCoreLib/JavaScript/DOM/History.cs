using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
	// http://mxr.mozilla.org/mozilla-central/source/dom/webidl/History.webidl
	// https://src.chromium.org/viewvc/blink/trunk/Source/core/frame/History.idl
	// https://github.com/mono/mono/blob/master/mcs/class/Mono.WebBrowser/Mono.WebBrowser/DOM/IHistory.cs
	// https://github.com/mono/mono/blob/master/mcs/class/Mono.WebBrowser/Mono.Mozilla/DOM/History.cs
	// http://www.scala-js.org/api/scalajs-dom/0.6/index.html#org.scalajs.dom.History
	// http://sharpkit.net/help/SharpKit.Html4/SharpKit.Html4/History/

	[Script(HasNoPrototype = true)]
    public class History
    {
        // remoting.phistory?

        // how will History work with ServiceWorker?

        // X:\jsc.svn\examples\javascript\future\HistoricSnapshotMashup\HistoricSnapshotMashup\Application.cs
        public readonly int length;

        public readonly object state;

        public void back()
        {
        }


        // X:\jsc.svn\examples\javascript\Test\TestHistoryUTF8Path\TestHistoryUTF8Path\Application.cs
        // X:\jsc.svn\examples\javascript\Test\Test453History\Test453History\Application.cs
        //void pushState(any data, DOMString title, optional DOMString? url = null);
        public void pushState(object data, string title, string url = null)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\DOM\HistoryExtensions.cs

        }

        // void replaceState(any data, DOMString title, optional DOMString? url = null);
        public void replaceState(object data, string title, string url = null)
        {

        }


    }


    [Script]
    public class HistoryScope
    {
        // tested by
        // X:\jsc.svn\examples\javascript\async\AsyncHistoricActivities\AsyncHistoricActivities\Application.cs

        public static Stack<Func<bool>> inline_unwind = new Stack<Func<bool>>();
        public static Stack<object> inline_unwind_data = new Stack<object>();

    }

    [Obsolete("This class should live in ScriptCoreLib.Extensions, yet for now it has to stay here to keep identity between applications...")]
    [Script]
    public class HistoryScope<T>
    {
        public T __state;

        public T state { get { return __state; } }

        public Func<TaskCompletionSource<HistoryScope<T>>> __TaskCompletionSource;

        public TaskCompletionSource<HistoryScope<T>> TaskCompletionSource
        {
            get
            {
                return __TaskCompletionSource();
            }
        }

    }
}
