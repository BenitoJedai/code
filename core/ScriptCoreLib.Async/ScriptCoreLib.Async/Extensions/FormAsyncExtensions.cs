using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public static class FormAsyncExtensions
    {
        public static Task ShowAsync(this Form f)
        {
            var x = new TaskCompletionSource<object>();

            f.FormClosed +=
                delegate
                {
                    x.SetResult(new object());
                };

            f.Show();

            return x.Task;
        }
    }
}
