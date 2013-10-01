using FormsAsyncButtonExperiment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsAsyncButtonExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }


        private void button1_Click(object sender, System.EventArgs e)
        {

            // X:\jsc.svn\examples\javascript\AsyncButtonExperiment\AsyncButtonExperiment\Application.cs

            button1.Enabled = false;

            // will do some work in the background... { handler = { IsBackground = False, ManagedThreadId = 3 }, task = { IsBackground = True, ManagedThreadId = 4 } }


            var yields = new
            {
                foo0 = new Action(delegate { }),
                foo1 = new Action(delegate { }),
                stackreversal0 = "1",
                foo2 = new Action(delegate { }),
                foo3 = new Action(delegate { }),
                foo4 = new Action(delegate { }),
                stackreversal1 = "2",
                stackreversal2 = "3",
                foo5 = new Action(delegate { }),
                foo6 = new Action(delegate { })
            };


            var awaiter = Task.Factory.StartNew(
                    new
                    {
                        goo = "goo ",
                        handler = new
                        {
                            System.Threading.Thread.CurrentThread.IsBackground,
                            System.Threading.Thread.CurrentThread.ManagedThreadId
                        }
                    },
                    state =>
                    {
                        Console.WriteLine("will do some work in the background... "
                            + new
                            {
                                state.handler,
                                task = new
                                {
                                    System.Threading.Thread.CurrentThread.IsBackground,
                                    System.Threading.Thread.CurrentThread.ManagedThreadId
                                }
                            }

                        );

                        //Task.Delay
                        Thread.Sleep(3000);



                        Console.WriteLine("will do some work in the background... done!");

                        return "done";
                    }
             ).GetAwaiter();

            if (awaiter.IsCompleted)
            {
                var task_Result = awaiter.GetResult();

                button1.Text = new { task_Result }.ToString();
                button1.Enabled = true;

                return;
            }


            awaiter.OnCompleted(
                delegate
                {
                    var task_Result = awaiter.GetResult();
                    button1.Text = new { task_Result }.ToString();
                    button1.Enabled = true;

                }
             );
        }

        partial void button2_Click(object sender, EventArgs e);
        partial void ApplicationControl_Load(object sender, EventArgs e);



#if true
        async partial void button2_Click(object sender, EventArgs e)
        {
            // tested by x:\jsc.svn\examples\rewrite\TestSwitchWithLocalByRef\TestSwitchWithLocalByRef\Class1.cs

            // see also: https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309

            //script: error JSC1000: if block not detected correctly, opcode was { Branch = [0x000c] beq        +0 -2{[0x0009] ldloc.2    +1 -0} {[0x000a] ldc.i4.s   +1 -0} , Location =
            // assembly: S:\FormsAsyncButtonExperiment.Application.exe
            // type: FormsAsyncButtonExperiment.ApplicationControl+<button2_Click>d__15, FormsAsyncButtonExperiment.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x000c
            //  method:Void MoveNext() }


            //assembly: S:\FormsAsyncButtonExperiment.Application.exe
            //type: FormsAsyncButtonExperiment.ApplicationControl+<button2_Click>d__15, FormsAsyncButtonExperiment.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            //offset: 0x002e
            // method:Int32 <MoveNext><008e>.try(<MoveNext>, <button2_Click>d__15 ByRef)

            // X:\jsc.svn\examples\javascript\AsyncButtonExperiment\AsyncButtonExperiment\Application.cs

            button2.Enabled = false;

            // will do some work in the background... { handler = { IsBackground = False, ManagedThreadId = 3 }, task = { IsBackground = True, ManagedThreadId = 4 } }

            // script: error JSC1000: Method: <MoveNext><008e>.try, 
            // Type: FormsAsyncButtonExperiment.ApplicationControl+<button2_Click>d__15; 
            // emmiting failed : System.NotImplementedException: { 
            // ParameterType = System.Runtime.CompilerServices.TaskAwaiter`1[System.String]&, 
            // p = [0x002e] call       +0 -3{[0x0021] ldflda     +1 -1{[0x001f] ldarg.s    +1 -0} } {[0x0027] ldflda     +1 -1{[0x0026] ldarg.0    +1 -0} } {[0x002c] ldarg.s    +1 -0} , 
            // m = Void AwaitUnsafeOnCompleted[TaskAwaiter`1,<button2_Click>d__15](System.Runtime.CompilerServices.TaskAwaiter`1[System.String] ByRef, <button2_Click>d__15 ByRef) }

            var task_Result = await Task.Factory.StartNew(
                new
                {
                    goo = "goo ",
                    handler = new
                    {
                        System.Threading.Thread.CurrentThread.IsBackground,
                        System.Threading.Thread.CurrentThread.ManagedThreadId
                    }
                },
                state =>
                {
                    Console.WriteLine("will do some work in the background... "
                        + new
                        {
                            state.handler,
                            task = new
                            {
                                System.Threading.Thread.CurrentThread.IsBackground,
                                System.Threading.Thread.CurrentThread.ManagedThreadId
                            }
                        }

                    );

                    //Task.Delay
                    Thread.Sleep(3000);



                    Console.WriteLine("will do some work in the background... done!");

                    return "done";
                }
            );




            button2.Text = new { task_Result }.ToString();
            button2.Enabled = true;


        }

        partial void ApplicationControl_Load(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }
#else
        partial void button2_Click(object sender, EventArgs e)
        {

        }

        partial void ApplicationControl_Load(object sender, EventArgs e)
        {
        }
#endif

    }
}
