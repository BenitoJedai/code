using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutoRefreshTestingHost
{
    class Program
    {
        static FileSystemWatcher w;

        // called by post build event
        static void InternalMain(string[] args)
        {
            // we managed to get the signal from post build event into our zombie console.
            // congratz.

            Console.WriteLine("enter InternalMain");

            // are we already watching the filesystem?
            // reset monitoring..

            foreach (var exe in args.Where(x => x.EndsWith(".exe")))
            {
                // are we supposed to run this thing?
                // do we have a whitelist we can do a security scan for atleast?

                Console.WriteLine(new { exe });
                // { exe = X:\jsc.svn\examples\javascript\LINQ\test\AutoRefreshTesting\AutoRefreshTesting\bin\Debug\AutoRefreshTesting.exe }

                // can we do a security rewrite?
            }

            #region EnableRaisingEvents
            foreach (var sln in args.Where(x => x.EndsWith(".sln")))
            {
                if (w != null)
                    w.Dispose();

                // well start monitoring the directory.
                var path = Path.GetDirectoryName(sln);

                Console.WriteLine(new { path });

                w = new FileSystemWatcher(path, "*.cs");
                // if there is a change, we could rerun msbuild?
                // that would call post build event and we would be called again would we not?
                w.IncludeSubdirectories = true;
                w.EnableRaisingEvents = true;

                w.Changed +=
                    (xsender, xargs) =>
                    {
                        // { ChangeType = Changed, FullPath = X:\jsc.svn\examples\javascript\LINQ\test\AutoRefreshTesting\AutoRefreshTesting\obj\Debug }

                        Console.WriteLine(
                            new { xargs.ChangeType, xargs.FullPath }
                            );

                        // are we supposed to udp broadcast now?
                        // are we supposed to have roslyn look at comments?
                        // unless there are no other changes to happen lets do a backgroun build.

                        w.Dispose();

                        var msbuild = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe";
                        //  { msbuild = C:\Windows\system32\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe }

                        var ww = Stopwatch.StartNew();
                        Console.WriteLine("lets rebuild! " + new { msbuild });
                        //Thread.Sleep(1000);

                        // %SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe

                        var p = Process.Start(
                            new ProcessStartInfo(msbuild)
                            {
                                Arguments = "/nologo /verbosity:q " + sln,

                                // can we share our console with that sub process?
                                UseShellExecute = false,
                            }
                        );


                        p.WaitForExit();

                        Console.WriteLine("rebuild done! " + new { ww.ElapsedMilliseconds });
                    };
            }
            #endregion

        }

        // called by post build event
        static void Main(string[] args)
        {
            var AsServerSignal = new EventWaitHandle(
                false,
                EventResetMode.AutoReset,
                "AsServerSignal"
            );


            // if its a trap then start a server
            var AsServer = AsServerSignal.WaitOne(1);

            Console.WriteLine(new { AsServer } + " continue?");
            if (AsServer)
            {
                Console.WriteLine("time to create a server?");

                var pipeServer = new NamedPipeServerStream(
                    "foo", PipeDirection.In,
                    1,
                    PipeTransmissionMode.Message,
                    PipeOptions.WriteThrough);

                while (true)
                {
                    Console.WriteLine("awaiting for a new connection");

                    pipeServer.WaitForConnection();

                    Console.WriteLine("client connected to server!");

                    var buffer = new byte[0xffff];

                    var c = pipeServer.Read(buffer, 0, buffer.Length);

                    var xml = XElement.Parse(
                        Encoding.UTF8.GetString(buffer, 0, c)
                    );

                    //var old = new { Console.ForegroundColor };
                    //Console.ForegroundColor = ConsoleColor.Cyan;
                    //Console.WriteLine(new { xml });
                    //Console.ForegroundColor = old.ForegroundColor;

                    // do something with incoming params?

                    pipeServer.Disconnect();

                    // server inactive for a moment, update the state and reconnect to next client
                    InternalMain(
                        xml.Elements().Select(x => x.Value).ToArray()
                    );
                }
            }




            var wConnected = false;

            new Thread(
                delegate()
                {
                    // allow 100 until we need to take action
                    Thread.Sleep(100);

                    if (wConnected)
                    {
                        // server already active!
                        return;
                    }

                    AsServerSignal.Set();

                    // make us a new server
                    // cmd.exe allows us to survive a process crash.
                    Process.Start("cmd.exe", "/K " + typeof(Program).Assembly.Location);





                }
            )
            {
                IsBackground = true
            }.Start();


            Console.WriteLine("connecting to the server...");
            var w = new NamedPipeClientStream(".", "foo", PipeDirection.Out, PipeOptions.WriteThrough);
            Thread.Yield();
            w.Connect();
            wConnected = true;

            Console.WriteLine("connecting to the server... done");

            // http://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.jmp(v=vs.110).aspx

            var bytes = Encoding.UTF8.GetBytes(
                new XElement("xml",

                    from x in args
                    select new XElement("arg", x)
                    ).ToString()
            );


            w.Write(bytes, 0, bytes.Length);
            w.Flush();
            w.Dispose();


            //Thread.Sleep(100);


            //Unhandled Exception: System.InvalidOperationException: Cannot read keys when either application does not have a console or when console input has been redirected from a file. Try Console.Read.
            //1>     at System.Console.ReadKey(Boolean intercept)
            //1>     at System.Console.ReadKey()


            //Debugger.Break();
            //Console.ReadKey();
        }
    }




}
