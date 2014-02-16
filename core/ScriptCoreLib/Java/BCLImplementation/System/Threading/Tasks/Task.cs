using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace ScriptCoreLib.Java.BCLImplementation.System.Threading.Tasks
//{
//    // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx
//    [Script(Implements = typeof(global::System.Threading.Tasks.Task))]
//    internal class __Task
//    {
//        public bool IsCompleted { get; internal set; }

//        public static Task<TResult> FromResult<TResult>(TResult result)
//        {
//            var t = new __Task<TResult>();

//            t.InternalSetCompleteAndYield(result);

//            return t;
//        }

//        public void InternalSetCompleteAndYield()
//        {
//            this.IsCompleted = true;

//            //if (this.InternalYield != null)
//            //    this.InternalYield();
//        }
//    }


//    [Script(Implements = typeof(global::System.Threading.Tasks.Task<>))]
//    internal class __Task<TResult> : __Task
//    {
//        public TResult Result { get; internal set; }

//        public void InternalSetCompleteAndYield(TResult value)
//        {

//            // or throw?
//            if (IsCompleted)
//                return;

//            // http://stackoverflow.com/questions/12100022/taskcompletionsource-when-to-use-setresult-versus-trysetresult-etc

//            //Console.WriteLine("__Task<TResult> InternalSetCompleteAndYield");

//            this.Result = value;

//            this.InternalSetCompleteAndYield();
//        }

//        public static implicit operator Task<TResult>(__Task<TResult> e)
//        {
//            return (Task<TResult>)(object)e;
//        }

//        public static implicit operator __Task<TResult>(Task<TResult> e)
//        {
//            return (__Task<TResult>)(object)e;
//        }

//        public Task ContinueWith(Action<Task<TResult>> continuationAction)
//        {
//            var x = new TaskCompletionSource<object>();

//            if (!this.IsCompleted)
//                throw new NotImplementedException();

//            continuationAction(this);

//            x.SetResult(null);
//            return x.Task;
//        }
//    }
//}
