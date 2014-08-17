using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/TaskScheduler.cs
    // https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/corlib/System.Threading.Tasks/TaskScheduler.cs

    [Script(Implements = typeof(global::System.Threading.Tasks.TaskScheduler))]
    internal class __TaskScheduler
    {
        public static TaskScheduler Current { get; set; }

        // to add: WorkerTaskScheduler with url
        public static TaskScheduler Default { get; set; }

        public static TaskScheduler FromCurrentSynchronizationContext()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130828-thread-run

            return new __TaskScheduler();
        }


        public static implicit operator TaskScheduler(__TaskScheduler e)
        {
            return (TaskScheduler)(object)e;
        }
    }
}
