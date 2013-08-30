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



#if !DEBUG
        async partial void button2_Click(object sender, EventArgs e)
        {

            // X:\jsc.svn\examples\javascript\AsyncButtonExperiment\AsyncButtonExperiment\Application.cs

            button2.Enabled = false;

            // will do some work in the background... { handler = { IsBackground = False, ManagedThreadId = 3 }, task = { IsBackground = True, ManagedThreadId = 4 } }

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
