using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestTaskScheduler
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140405/taskscheduler

            //public void Start(TaskScheduler scheduler);

            // http://msdn.microsoft.com/en-us/library/system.threading.tasks.taskscheduler(v=vs.110).aspx

            //var t = new Task(
            //    delegate
            //    {
            //        Console.WriteLine("enter task");
            //    }
            //);

            var f = new TaskFactory(new MyTaskScheduler());

            // { Current = System.Threading.Tasks.ThreadPoolTaskScheduler, Default = System.Threading.Tasks.ThreadPoolTaskScheduler }
            Console.WriteLine(
                new
                {
                    Current = TaskScheduler.Current.Id,
                    Default = TaskScheduler.Default.Id,

                    //The current SynchronizationContext may not be used as a TaskScheduler.
                    //SynchronizationContext = TaskScheduler.FromCurrentSynchronizationContext().Id
                }
                );


            //{ Current = 1, Default = 1 }
            //{ task = System.Threading.Tasks.Task`1[System.Threading.Tasks.Task] }
            //{ task = System.Threading.Tasks.Task`1[System.Threading.Tasks.Task], taskWasPreviouslyQueued = True }
            //{ task = System.Threading.Tasks.Task`1[System.Threading.Tasks.Task], taskWasPreviouslyQueued = True, value = True, IsCompleted = True }


            var t = f.StartNew(
                async delegate
                {
                    //{ Current = 1, Default = 1 }
                    //{ task = System.Threading.Tasks.Task`1[System.Threading.Tasks.Task] }
                    //{ task = System.Threading.Tasks.Task`1[System.Threading.Tasks.Task], taskWasPreviouslyQueued = True }
                    //enter task { Current = 2, Default = 1 }
                    //{ task = System.Threading.Tasks.Task`1[System.Threading.Tasks.Task], taskWasPreviouslyQueued = True, value = True, IsCompleted = True }


                    Console.WriteLine("enter task " + new
                    {
                        Current = TaskScheduler.Current.Id,
                        Default = TaskScheduler.Default.Id
                        //SynchronizationContext = TaskScheduler.FromCurrentSynchronizationContext().Id
                    }
                    );

                    // wait forever
                    await new TaskCompletionSource<object>().Task;
                    Console.WriteLine("exit task");
                }
            );


            Debugger.Break();
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }

    class MyTaskScheduler : TaskScheduler
    {
        // http://stackoverflow.com/questions/6800705/why-is-taskscheduler-current-the-default-taskscheduler

        protected override System.Collections.Generic.IEnumerable<Task> GetScheduledTasks()
        {
            throw new NotImplementedException();
        }

        protected override void QueueTask(Task task)
        {
            //{ Current = 1, Default = 1 }
            //{ task = System.Threading.Tasks.Task`1[System.Threading.Tasks.Task] }
            //{ task = System.Threading.Tasks.Task`1[System.Threading.Tasks.Task], taskWasPreviouslyQueued = True }
            //enter task { Current = 2, Default = 1 }


            Console.WriteLine(
                new { task }
            );

            //throw new NotImplementedException();

            // Start may not be called on a task that was already started.
            //task.Start(this);

            // RunSynchronously may not be called on a task that was already started.
            //task.RunSynchronously(this);

            task.Wait();
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            Console.WriteLine(
                 new { task, taskWasPreviouslyQueued }
             );

            //task.Wait();
            var value = TryExecuteTask(task);

            //{ task = System.Threading.Tasks.Task`1[System.Threading.Tasks.Task] }
            //{ task = System.Threading.Tasks.Task`1[System.Threading.Tasks.Task], taskWasPreviouslyQueued = True }
            //enter task
            //{ task = System.Threading.Tasks.Task`1[System.Threading.Tasks.Task], taskWasPreviouslyQueued = True, done = True }

            Console.WriteLine(
                 new { task, taskWasPreviouslyQueued, value, task.IsCompleted }
             );


            return value;
        }
    }
}
