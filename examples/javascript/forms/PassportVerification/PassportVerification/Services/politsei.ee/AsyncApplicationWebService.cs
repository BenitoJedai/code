using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassportVerification
{

    public sealed partial class AsyncApplicationWebService : Component
    {
        // http://blogs.msdn.com/b/pfxteam/archive/2011/01/13/10115642.aspx

        public async Task WebMethod2(string e, Action<string> y)
        {
            // http://msdn.microsoft.com/en-us/library/vstudio/hh191443.aspx
            // http://softwareblog.alcedo.com/post/2012/01/25/TaskCompletionSource-in-real-life-(part-2-of-2).aspx

            //MessageBox.Show("before await");

            var s = new TaskCompletionSource<object>();

            new Thread(
                delegate()
                {
                    applicationWebService1.WebMethod2(
                        e,
                        x =>
                        {
                            y(x);

                            // Additional information: An attempt was made to 
                            // transition a task to a final state when it had already completed.


                            // jsc rewriter will insert:
                            s.SetResult(null);
                        }
                    );
                }
            ).Start();

            await s.Task;

            //MessageBox.Show("after await");

        }
    }
}
