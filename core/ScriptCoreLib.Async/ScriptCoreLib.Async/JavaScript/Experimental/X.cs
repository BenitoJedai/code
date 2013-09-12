using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.Experimental
{
    public sealed class InternalScriptApplicationReference
    {
        public int index;
        public string name;
        public int size;
    }

    [Obsolete("jsc, how many types like this do we have already? :P")]
    public sealed class InternalScriptApplicationSource
    {
        public string source;

        public InternalScriptApplicationReference[] references;

    }

    internal static class InternalScriptApplicationSourceExtensions
    {
        public static Action eval(this InternalScriptApplicationSource e)
        {
            var src = new Blob(e.source).ToObjectURL();

            //.SetInternalScriptApplicationSource();

            object old = (Native.self as dynamic).InternalScriptApplicationSource;

            (Native.self as dynamic).InternalScriptApplicationSource = src;

            var core = e.references.First(k => k.index == 0).size;

            //            eval { index = 2, name =  ScriptCoreLib.Windows.Forms.dll.js, size = 586774 }
            // view-source:27522
            //eval { index = 0, name =  ScriptCoreLib.dll.js, size = 1325900 }
            // view-source:27522
            //eval { index = 1, name =  ScriptCoreLib.Drawing.dll.js, size = 54215 }
            // view-source:27522
            //eval { index = 3, name =  com.abstractatech.adminshell.Application+a.exe.js, size = 384908 }
            // view-source:27522
            //eval { core = 586774, Length = 2351797 }



            e.references.WithEach(
                k =>
                    Console.WriteLine("eval " + new { k.index, k.name, k.size })
            );


            Console.WriteLine("eval " + new { core, e.source.Length });

            var source = e.source.Substring(
                core
            );

            Native.window.eval(source);

            return delegate
            {
                // hacky way to restore
                if (old != null)
                    (Native.self as dynamic).InternalScriptApplicationSource = old;
                else
                    (Native.self as dynamic).InternalScriptApplicationSource = Native.document.location + "view-source";
            };
        }
    }

    [Obsolete("This wont work when rewritten. Why? Workaround is to link it in as source.")]
    internal static class X
    {
        // tested by
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.adminshell\com.abstractatech.adminshell\Application.cs

        //02000066 ScriptCoreLib.JavaScript.Experimental.X+<>c__DisplayClassc+<<GetAwaiter>b__7>d__11
        //script: error JSC1000: if block not detected correctly, opcode was { Branch = [0x0020] beq        +0 -2{[0x0019] ldfld      +1 -1{[0x0018] ldarg.0    +1 -0} } {[0x001e] ldc.i4.s   +1 -0} , Location =
        // assembly: T:\com.abstractatech.adminshell.Application.exe
        // type: ScriptCoreLib.JavaScript.Experimental.X+<>c__DisplayClassc+<<GetAwaiter>b__7>d__11, com.abstractatech.adminshell.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // offset: 0x0020
        //  method:Int32 <>02000019<>06000044<>MoveNext<0000>.try(<>02000019<>06000044<>MoveNext, <<GetAwaiter>b__7>d__11 ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[System.Byte[]] ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[System.Byte[]] ByRef) }

        //public static object 


        // .NET 4.5!!!
        public static TaskAwaiter<InternalScriptApplicationSource> GetAwaiter(this Type __e)
        {
            //Console.WriteLine(new { __e.Name });

            // http://stackoverflow.com/questions/9713058/sending-post-data-with-a-xmlhttprequest

            var y = new TaskCompletionSource<InternalScriptApplicationSource>();

            //var ysource = Native.window.sessionStorage[__e.Name];
            //if (ysource != null)
            //{
            //    y.SetResult(ysource);

            //    return y.Task.GetAwaiter();
            //}

            //return 

            //InternalInitializeInlineWorker Report: { __IProgress_Report = { value = [object Object] } }
            // view-source:27346
            //{ Name = a, loaded = 4538818, total = 4538818 } view-source:27346

            // view-source:27346
            //loading secondary app in a moment... { responseType = arraybuffer, ManagedThreadId = 10 }
            // view-source:27346
            //loading secondary app in a moment... { Length = 4538818 } decrypting...
            // view-source:27346
            //loading secondary app in a moment... { Length = 2269409 } done!

            var bar = new IHTMLDiv { }.AttachToDocument();

            bar.style.SetLocation(0, -2);
            bar.style.position = IStyle.PositionEnum.@fixed;
            bar.style.height = "3px";
            bar.style.backgroundColor = "red";
            //bar.style.borderBottom = "1px solid darkred";

            // http://stackoverflow.com/questions/9670075/css-transition-shorthand-with-multiple-properties

            (bar.style as dynamic).webkitTransition = "top 0.5s linear";
            //(bar.style as dynamic).webkitTransitionProperty = "top, width, background-color";
            (bar.style as dynamic).webkitTransitionProperty = "top, width";


            Task.Factory.StartNewWithProgress(
                new
                {
                    __e.Name,

                    backgroundColor = "red",

                    loaded = default(long),
                    total = default(long),
                    source = default(string),

                    Native.document.location.href,


                    references = new InternalScriptApplicationReference[0]
                },

                progress: x =>
                {
                    bar.style.top = "0px";


                    #region bar
                    if (x.loaded > 0)
                        if (x.total > 0)
                        {
                            //if (x.loaded == x.total)
                            //{
                            //}
                            //else
                            //{
                            //    bar.style.SetLocation(0, 0);
                            //}

                            var xx = 100 * x.loaded / x.total;

                            var per = xx + "%";

                            //Console.WriteLine(new { per, x.loaded, x.total, x.backgroundColor });

                            bar.style.backgroundColor = x.backgroundColor;
                            bar.style.width = per;

                        }
                    #endregion



                    //Console.WriteLine(
                    //    new { x.Name, x.loaded, x.total }
                    //);

                    #region SetResult
                    x.source.With(
                        async source =>
                        {
                            //        // should we analyze? IFunction

                            //Console.WriteLine("wall save source to localStorage " + new { __e.Name, source.Length });

                            // sessionStorage out of memory?
                            //Native.window.sessionStorage[__e.Name] = source;

                            //Native.window.eval(
                            //    //x.responseText
                            //    source
                            //);


                            bar.style.backgroundColor = "yellow";
                            await Task.Delay(300);
                            bar.style.backgroundColor = "red";
                            await Task.Delay(300);
                            bar.style.backgroundColor = "yellow";
                            await Task.Delay(300);
                            bar.style.backgroundColor = "red";
                            await Task.Delay(300);
                            bar.Orphanize();

                            //                            { index = 0, name =  ScriptCoreLib.dll.js, size = 1330538 } view-source:27530

                            // view-source:27530
                            //{ index = 1, name =  WorkerInsideSecondaryApplicationWithBackButton.Application+x.exe.js, size = 507500 } view-source:2753


                            y.SetResult(
                                new InternalScriptApplicationSource
                                {
                                    source = source,
                                    references = x.references
                                }

                            );
                        }
                    );
                    #endregion


                },


                function:
                    tuple =>
                    {
                        var progress = tuple.Item1;
                        var scope = tuple.Item2;

                        // http://stackoverflow.com/questions/13870853/how-to-upload-files-in-web-workers-when-formdata-is-not-defined
                        // FormData is not defined
                        //var f = new FormData();

                        //f.append("Application", scope.Name);

                        var x = new IXMLHttpRequest();

                        // { src = /more-source } 

                        // { src = blob:http%3A//192.168.43.252%3A5485/fdec11d3-735f-4165-937a-43f25ef8d8d3#worker/more-source } 
                        // worker location might not help us talk to our server!

                        //var src = Native.worker.location.href + "/more-source";
                        //POST http://public:key1555555@192.168.43.252:14356//more-source 400 (Bad Request) 
                        var src = scope.href;

                        Console.WriteLine(new { src });

                        x.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, src,
                            async: true,
                            name: "public",
                            pass: "key1555555"
                        );

                        // Uncaught InvalidStateError: An attempt was made to use an object that is not, or is no longer, usable.
                        x.setRequestHeader(
                            "X-Application", scope.Name
                        );

                        // what about progress?


                        // http://stackoverflow.com/questions/10956574/why-might-xmlhttprequest-progressevent-lengthcomputable-be-false

                        var xprogress = new { scope.loaded, scope.total };

                        // AppEngine will not report progress

                        #region onprogress
                        x.onprogress +=
                            e =>
                            {
                                // make room for decrypt progress
                                var loaded = e.loaded / 2;

                                xprogress = new { loaded, e.total };

                                //Console.WriteLine();

                                progress.Report(
                                    new
                                    {
                                        scope.Name,
                                        scope.backgroundColor,
                                        xprogress.loaded,
                                        xprogress.total,
                                        scope.source,
                                        scope.href,
                                        scope.references
                                    }
                                );

                            };
                        #endregion


                        #region decrypt
                        Action<byte[]> decrypt = response =>
                        {
                            var AllResponseHeaders = x.getAllResponseHeaders();

                            //AllResponseHeaders = Date: Wed, 11 Sep 2013 14:31:43 GMT
                            //X-Reference-0: ScriptCoreLib.dll.js 1330538
                            //Server: ASP.NET Development Server/11.0.0.0
                            //X-AspNet-Version: 4.0.30319
                            //X-Reference-1: WorkerInsideSecondaryApplicationWithBackButton.Application+x.exe.js 487668
                            //X-DiagnosticsMakeItSlowAndAddSalt: ok
                            //Content-Type: application/octet-stream
                            //Cache-Control: public
                            //Connection: Close
                            //Content-Length: 3636412
                            //Expires: Wed, 11 Sep 2013 14:46:43 GMT

                            var prefix = "X-Reference-";

                            var references = AllResponseHeaders.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Where(k => k.StartsWith(prefix)).Select(
                                k =>
                                {
                                    var text = k.Substring(prefix.Length);

                                    var index = int.Parse(text.TakeUntilIfAny(":"));
                                    var name = text.SkipUntilIfAny(":").TakeUntilLastIfAny(" ");
                                    var size = int.Parse(text.SkipUntilIfAny(":").SkipUntilLastIfAny(" "));

                                    return new InternalScriptApplicationReference
                                    {
                                        index = index,
                                        name = name,
                                        size = size
                                    };
                                }
                            ).ToArray();


                            Console.WriteLine("loading secondary app in a moment... " + new { response.Length } + " decrypting...");



                            //                            X-Reference-0:ScriptCoreLib.dll.js 1330538
                            //X-Reference-1:WorkerInsideSecondaryApplicationWithBackButton.Application+x.exe.js 485234



                            //y.SetResult(new { x.responseText.Length }.ToString());
                            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.cs

                            var m = new MemoryStream();

                            var lo = default(byte);
                            var lo_set = false;

                            foreach (var item in response)
                            {
                                if (lo_set)
                                {
                                    lo_set = false;

                                    var hi = (byte)(item << 4);

                                    m.WriteByte(
                                        (byte)(lo | hi)
                                    );

                                    if ((m.Length % 1024 * 8) == 0)
                                    {
                                        var loaded = xprogress.total / 2 + m.Length;

                                        xprogress = new { loaded, xprogress.total };

                                        progress.Report(
                                            new
                                            {
                                                scope.Name,
                                                backgroundColor = "cyan",
                                                xprogress.loaded,
                                                xprogress.total,
                                                scope.source,
                                                scope.href,
                                                scope.references
                                            }
                                        );
                                    }
                                }
                                else
                                {
                                    lo = item;
                                    lo_set = true;
                                }
                            }

                            // decrypted
                            var source = Encoding.UTF8.GetString(m.ToArray());


                            Console.WriteLine("loading secondary app in a moment... " + new { source.Length } + " done!");

                            //return new { response.Length, responseText = source };

                            progress.Report(
                                new
                                {
                                    scope.Name,
                                    backgroundColor = "green",
                                    xprogress.loaded,
                                    xprogress.total,
                                    source,
                                    scope.href,
                                    references
                                }
                            );

                        };
                        #endregion


                        Action send = async delegate
                        {


                            var response = await x.bytes;


                            decrypt(response);

                        };


                        send();

                        // no changes yet
                        return scope;
                    }


            );


            return y.Task.GetAwaiter();


        }


        [Obsolete(@"
X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\Worker.cs 

jsc, can you refactor this to be a static function property, 
a program about a program, this is what we do with nuget releases,
in a way this in itself is an example of the chat with the compiler

")]
        public static string SetInternalScriptApplicationSource(this string InternalScriptApplicationSource)
        {
            (Native.self as dynamic).InternalScriptApplicationSource = InternalScriptApplicationSource;

            //Console.WriteLine(new { InternalScriptApplicationSource });

            return InternalScriptApplicationSource;
        }

    }

}
