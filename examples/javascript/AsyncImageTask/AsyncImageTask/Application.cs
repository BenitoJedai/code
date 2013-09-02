using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AsyncImageTask;
using AsyncImageTask.Design;
using AsyncImageTask.HTML.Pages;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace AsyncImageTask
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            new IHTMLButton("load it").AttachToDocument().WhenClicked(
                async btn =>
                {
                    btn.disabled = true;


                    // slow it down
                    //await Task.Delay(333);

                    var i = new HTML.Images.FromAssets.jsc();


                    //Console.WriteLine(new { i.complete });
                    //var y = new TaskCompletionSource<IHTMLImage>();
                    //i.InvokeOnComplete(y.SetResult);

                    i.AttachToDocument();
                    Console.WriteLine(new { i.complete });

                    //await y.Task;
                    // Error	1	'System.Runtime.CompilerServices.INotifyCompletion' does not contain a definition for 'IsCompleted'	X:\jsc.svn\examples\javascript\AsyncImageTask\AsyncImageTask\Application.cs	55	21	AsyncImageTask

                    var j = new ImageCompletionSource(i);


                    await j;
                    //await i;



                    btn.disabled = false;
                }
            );
        }

    }
}

class ImageCompletionSource
{
    public Task<IHTMLImage> Task;

    public ImageCompletionSource(IHTMLImage i)
    {
        var y = new TaskCompletionSource<IHTMLImage>();
        i.InvokeOnComplete(y.SetResult);

        this.Task = y.Task;

    }

    public static implicit operator Task<IHTMLImage>(ImageCompletionSource t)
    {
        return t.Task;
    }
}

public static class X
{
    public static TaskAwaiter<IHTMLImage> GetAwaiter(this IHTMLImage i)
    {
        // Error	5	'System.Runtime.CompilerServices.INotifyCompletion' does not contain a definition for 'IsCompleted'	X:\jsc.svn\examples\javascript\AsyncImageTask\AsyncImageTask\Application.cs	57	21	AsyncImageTask


        var y = new TaskCompletionSource<IHTMLImage>();
        i.InvokeOnComplete(y.SetResult);

        return y.Task.GetAwaiter();

        //return default(ITaskAwaiter<IHTMLImage>);
    }

    //public interface ITaskAwaiter<TResult> 
    //{
    //    // Error	4	'X.XAwaiter' does not implement 'System.Runtime.CompilerServices.INotifyCompletion'	X:\jsc.svn\examples\javascript\AsyncImageTask\AsyncImageTask\Application.cs	57	21	AsyncImageTask


    //    bool IsCompleted { get; set; }

    //    void OnCompleted(Action continuation);

    //    TResult GetResult();
    //}
}
