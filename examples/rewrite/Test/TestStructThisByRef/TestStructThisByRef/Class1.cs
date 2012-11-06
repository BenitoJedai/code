using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestStructThisByRef
{
    static class R
    {
        static Random i = new Random();
        public static int Random()
        {
            return i.Next();
        }
    }

    public struct foo
    {
        public int value;
    }

    public struct _button_Click1_d__0 : IAsyncStateMachine
    {
        public AsyncVoidMethodBuilder t__Builder;
        public ICriticalNotifyCompletion t__awaiter;

        public int __random;




        public static void MoveNext_justacopy(_button_Click1_d__0 __this)
        {
            __this.__random = 3;

        }

        public static void MoveNext_byref(ref _button_Click1_d__0 __this, ref foo foo)
        {
            __this.__random = 4;
            foo.value = 4;

        }

        public void MoveNext()
        {
            this.__random = 1;

            foo loc1;


            loc1.value = 1;

            MoveNext_justacopy(this);
            MoveNext_byref(ref this, ref loc1);

            Console.WriteLine("last time null / t__Builder.m_coreState.m_stateMachine");

            // Error	1	Cannot pass '<this>' as a ref or out argument because it is read-only	X:\jsc.svn\examples\rewrite\Test\TestStructThisByRef\TestStructThisByRef\Class1.cs	34	72	TestStructThisByRef


            var __this = this;

            Action foo =
                delegate
                {
                    // Error	1	Anonymous methods, lambda expressions, and query expressions 
                    // inside structs cannot access instance members of 'this'. 
                    // Consider copying 'this' to a local variable outside the anonymous method, 
                    // lambda expression or query expression and using the local instead.
                    // X:\jsc.svn\examples\rewrite\Test\TestStructThisByRef\TestStructThisByRef\Class1.cs	42	21	TestStructThisByRef
                    __this.__random = 2;
                };

            foo();

            //var x = this;
            this.t__Builder.AwaitUnsafeOnCompleted(ref t__awaiter,
                //ref x
                ref this
                );

            ;
        }

        public void SetStateMachine(IAsyncStateMachine e)
        {
            Console.WriteLine("i hope still null? t__Builder.m_coreState.m_stateMachine");
            this.t__Builder.SetStateMachine(e);

            Console.WriteLine("not null! t__Builder.m_coreState.m_stateMachine");

        }
    }

    public interface IAsyncStateMachine
    {
        void MoveNext();
        void SetStateMachine(IAsyncStateMachine e);

    }

    public interface ICriticalNotifyCompletion
    {

    }

    public struct AsyncVoidMethodBuilderCore
    {


        public IAsyncStateMachine m_stateMachine;

        public void Start<T>(ref T s) where T : IAsyncStateMachine
        {
            s.MoveNext();
        }


        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            if (stateMachine == null)
            {
                throw new ArgumentNullException("stateMachine");
            }
            if (this.m_stateMachine != null)
            {
                throw new InvalidOperationException("AsyncMethodBuilder_InstanceNotInitialized");
            }
            this.m_stateMachine = stateMachine;
        }
    }

    public struct AsyncVoidMethodBuilder
    {

        public AsyncVoidMethodBuilderCore m_coreState;

        public void AwaitUnsafeOnCompleted<TAwaiter, T>(ref TAwaiter a, ref T stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where T : IAsyncStateMachine
        {
            var m_coreState = this.m_coreState;

            //this.
            //this.m_coreState.m_stateMachine = stateMachine;

            stateMachine.SetStateMachine(stateMachine);

            this.m_coreState = m_coreState;
        }

        public static AsyncVoidMethodBuilder Create()
        {
            return new AsyncVoidMethodBuilder { };
        }

        public void Start<T>(ref T s) where T : IAsyncStateMachine
        {
            m_coreState.Start(ref s);
        }

        public void SetStateMachine(IAsyncStateMachine e)
        {
            this.m_coreState.SetStateMachine(e);
        }
    }


    public static class Program
    {
        public static void Main(string[] e)
        {
            button1_Click();
        }

        public static void button1_Click()
        {
            var x = new _button_Click1_d__0();

            x.t__Builder = AsyncVoidMethodBuilder.Create();

            var loc = x.t__Builder;

            loc.Start(ref x);
            //x.MoveNext();
        }
    }
}
