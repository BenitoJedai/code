using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;

namespace System.Windows.Forms
{
    public static class FormAsyncExtensions
    {
        [Description("the jsc eXperience")]
        public static Task ShowAsync(this Form f, bool hideOwner = false)
        {
            // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\Application.cs


            var x = new TaskCompletionSource<object>();

            f.FormClosed +=
                delegate
                {
                    x.SetResult(new object());
                };

            new { hideOwner, f.Owner }.With(
                state =>
                {
                    if (!state.hideOwner)
                        return;

                    if (state.Owner == null)
                        return;


                    state.Owner.Hide();

                    f.FormClosed +=
                        delegate
                        {
                            Console.WriteLine("ShowAsync FormClosed");
                            state.Owner.Show();
                        };
                }
            );


            f.Show();

            return x.Task;
        }
    }
}
