using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;

namespace HybridReflectionExampleLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
      
            Console.WriteLine("thread: " + Thread.CurrentThread.ManagedThreadId);
            
            HybridReflectionExample.VMContinuationSupport.__value = () => new[] { "c1" };
            HybridReflectionExample.Program.Main(new[] { "hello" });

            var t = new Thread(
                delegate()
                {
                    Console.WriteLine("thread: " + Thread.CurrentThread.ManagedThreadId);

                    HybridReflectionExample.VMContinuationSupport.__value = () => new[] { "c2" };
                    HybridReflectionExample.Program.Main(new[] { "world" });
                }
            );

            t.Start();
            t.Join();
        }
    }
}
