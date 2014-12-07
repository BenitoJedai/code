using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.google.appengine.api
{
    // https://cloud.google.com/appengine/docs/java/javadoc/com/google/appengine/api/ThreadManager#currentRequestThreadFactory()
    [Script(IsNative = true)]
    public class ThreadManager
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141207/async-query

        // X:\jsc.svn\examples\java\appengine\Test\TestThreadManager\TestThreadManager\ApplicationWebService.cs

        public static java.lang.Thread createThreadForCurrentRequest(java.lang.Runnable runnable)
        {
            return null;
        }
    }
}
