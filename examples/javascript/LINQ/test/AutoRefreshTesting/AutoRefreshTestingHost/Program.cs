using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;


namespace AutoRefreshTestingHost
{
    class Program
    {
        static Action wDispose = delegate { };

        [DllImport("user32.dll")]
        static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public FlashWindowFlags dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        public enum FlashWindowFlags : uint
        {
            /// <summary>
            /// Stop flashing. The system restores the window to its original state. 
            /// </summary>    
            FLASHW_STOP = 0,

            /// <summary>
            /// Flash the window caption 
            /// </summary>
            FLASHW_CAPTION = 1,

            /// <summary>
            /// Flash the taskbar button. 
            /// </summary>
            FLASHW_TRAY = 2,

            /// <summary>
            /// Flash both the window caption and taskbar button.
            /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags. 
            /// </summary>
            FLASHW_ALL = 3,

            /// <summary>
            /// Flash continuously, until the FLASHW_STOP flag is set.
            /// </summary>
            FLASHW_TIMER = 4,

            /// <summary>
            /// Flash continuously until the window comes to the foreground. 
            /// </summary>
            FLASHW_TIMERNOFG = 12
        }

        /// Minor adjust to the code above
        /// <summary>
        /// Flashes a window until the window comes to the foreground
        /// Receives the form that will flash
        /// </summary>
        /// <param name="hWnd">The handle to the window to flash</param>
        /// <returns>whether or not the window needed flashing</returns>
        public static bool FlashWindowEx(IntPtr hWnd)
        {
            FLASHWINFO fInfo = new FLASHWINFO();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;
            fInfo.dwFlags = FlashWindowFlags.FLASHW_ALL | FlashWindowFlags.FLASHW_TIMERNOFG;
            fInfo.uCount = UInt32.MaxValue;
            fInfo.dwTimeout = 0;

            return FlashWindowEx(ref fInfo);
        }

        static DateTime exeLastWriteTime;
        static bool exeLastWriteTimeSet;


        // called by post build event
        static void InternalMain(string[] args)
        {
            // X:\jsc.svn\examples\actionscript\Test\TestFlashBC\TestFlashBC\ApplicationSprite.cs
            // what about watching for nuget updates?

            // start /WAIT cmd /C c:/util/jsc/bin/jsc.meta.exe RewriteToAssembly /EntryPointAssembly:$(TargetPath) /AssemblyMerge:$(TargetPath)  /Output:"jsc.bc.exe"
            // start /WAIT cmd /C c:/util/jsc/bin/jsc.meta.exe RewriteToAssembly /EntryPointAssembly:$(TargetPath) /AssemblyMerge:$(TargetPath)  /Output:"c:/util/jsc/bin/jsc.bc.exe"
            // http://msdn.microsoft.com/en-us/magazine/cc163781.aspx
            // c:/util/jsc/bin/jsc.bc.exe $(ProjectPath) /rewrite /clear /run $(TargetPath) "C:\util\jsc\bin\ScriptCoreLib.Extensions.dll"
            // X:\jsc.svn\examples\javascript\Test\TestFirstBackgroundCompiler\TestFirstBackgroundCompiler\Application.cs

            // if everything is exactly the same
            // we might skip this refresh?

            // we managed to get the signal from post build event into our zombie console.
            // congratz.

            //Console.WriteLine("enter InternalMain");

            Console.Title = Environment.CurrentDirectory;

            Console.WriteLine("terminate old process?");

            wDispose();
            //Console.WriteLine("enter InternalMain wDispose.");

            // are we already watching the filesystem?
            // reset monitoring..


            var run = args.Contains("/run");
            var rewrite = args.Contains("/rewrite");
            var clear = args.Contains("/clear");

            #region security rebuild done
            foreach (var exe0 in args.Where(x => x.EndsWith(".exe")))
            {
                var exe = exe0;

                // are we supposed to run this thing?
                // do we have a whitelist we can do a security scan for atleast?

                //Console.WriteLine(new { exe });
                // { exe = X:\jsc.svn\examples\javascript\LINQ\test\AutoRefreshTesting\AutoRefreshTesting\bin\Debug\AutoRefreshTesting.exe }

                // can we do a security rewrite?
                //   // jsc.meta.exe RewriteToAssembly /EntryPointAssembly:"jsc.meta.exe" /AssemblyMerge:"jsc.meta.exe" /AssemblyMerge:"jsc.exe"  /Output:"jscc.exe"

                var LastWriteTime = File.GetLastWriteTime(exe);
                if (LastWriteTime == exeLastWriteTime)
                    continue;

                #region rewrite
                if (rewrite)
                {
                    // we should do this for jsc web apps. since they otherwise keep the server running and exe locked.
                    var count = Directory.GetFiles(Path.GetDirectoryName(exe), "*.exe").Count();
                    var Output = exe + "." + count + ".exe";
                    var ww = Stopwatch.StartNew();

                    {
                        // if there is assetslibrary we need it to be merged too.
                        // otherwise we will be blocked.

                        var pArguments = "RewriteToAssembly /DisableIsMarkedForMerge:true /EntryPointAssembly:" + exe + " /Output:" + Output + " /AssemblyMerge:" + exe;


                        var pathAssetsLibrary = exe.TakeUntilLastOrEmpty(".exe") + ".AssetsLibrary.dll";

                        Console.WriteLine(pathAssetsLibrary);

                        if (File.Exists(pathAssetsLibrary))
                        {
                            pArguments += " /AssemblyMerge:" + pathAssetsLibrary;
                        }


                        var p = Process.Start(
                             new ProcessStartInfo(@"c:/util/jsc/bin/jsc.meta.exe")
                             {
                                 //  method:Void Main(System.String[]), ex = System.InvalidOperationException: invalidmerge: jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication+AsProgram
                                 Arguments = pArguments,

                                 // can we share our console with that sub process?
                                 UseShellExecute = false,
                             }
                         );

                        p.WaitForExit();
                    }
                    Console.WriteLine("security rebuild done! " + new { ww.ElapsedMilliseconds });

                    // redirect
                    exe = Output;

                }
                #endregion

                // we will loose pdb data. line numbers will be missing! can jsc keep them?





                if (exeLastWriteTimeSet)
                {
                    FlashWindowEx(GetConsoleWindow());
                }

                exeLastWriteTimeSet = true;

                exeLastWriteTime = LastWriteTime;

                #region run
                if (run)
                {
                    var old = new { Console.ForegroundColor, Console.BackgroundColor };

                    if (clear)
                        Console.Clear();


                    {
                        //Console.Title += " (running. ";
                        // run the new version

                        new Thread(
                            delegate()
                            {
                                //Thread.Sleep(1000);

                                var p = Process.Start(
                                    //new ProcessStartInfo(Output)
                                     new ProcessStartInfo(exe)
                                     {
                                         WorkingDirectory = Path.GetDirectoryName(exe),

                                         // can we share our console with that sub process?
                                         UseShellExecute = false,

                                     }
                                 );


                                // if not rewewritten it has to complete
                                // otherwise post build event will be blocked.
                                //p.WaitForExit(300);

                                //Console.Title += " " + new { p.HasExited };

                                wDispose +=
                                    delegate
                                    {
                                        if (p == null)
                                            return;

                                        if (p.HasExited)
                                            return;

                                        // Unhandled Exception: System.UnauthorizedAccessException: Access to the path 'U:\TestFirstBackgroundCompiler.Application\TestFirstBackgroundCompiler.Application.exe' is denied.
                                        Console.WriteLine("terminating " + new { p.Id });
                                        // beause the server will have the same staging path.
                                        p.Kill();
                                        p = null;
                                    };
                            }
                        )
                        {
                            IsBackground = true
                        }.Start();

                    }
                    Console.ForegroundColor = old.ForegroundColor;
                    Console.BackgroundColor = old.BackgroundColor;
                }
                #endregion

                //{
                //    Console.WriteLine();

                //    // review the IL
                //    // when can jsc start highlighting diff?
                //    review = Process.Start(
                //         new ProcessStartInfo(@"c:/util/jsc/bin/jsc.meta.exe")
                //         {
                //             Arguments = Output,

                //             // can we share our console with that sub process?
                //             UseShellExecute = false,
                //         }
                //     );

                //    // when to close it?

                //    //p.WaitForExit();
                //}
            }
            #endregion

            var rebuilding = false;

            #region yFileSystemWatcher
            Action<string, string> yFileSystemWatcher =
                (path, filter) =>
                {
                    //Console.WriteLine("yFileSystemWatcher " + new { path, filter });

                    //var w = new FileSystemWatcher(path, "*.cs");
                    var w = new FileSystemWatcher(path, filter);

                    // X:\jsc.svn\examples\javascript\Test\TestFirstBackgroundCompiler\TestFirstBackgroundCompiler\Application.cs

                    wDispose +=
                        delegate
                        {
                            if (w == null)
                                return;

                            var ww = w;
                            w = null;
                            Thread.Yield();
                            ww.Dispose();
                        };
                    // if there is a change, we could rerun msbuild?
                    // that would call post build event and we would be called again would we not?
                    w.IncludeSubdirectories = true;
                    w.EnableRaisingEvents = true;
                    //ffs
                    w.NotifyFilter =
                         NotifyFilters.Attributes |
                        NotifyFilters.CreationTime |
                        NotifyFilters.FileName |
                        NotifyFilters.LastAccess |
                        NotifyFilters.LastWrite |
                        NotifyFilters.Size |
                        NotifyFilters.Security;


                    FileSystemEventHandler yChanged =


                        (xsender, xargs) =>
                        {

                            Console.WriteLine(
                                new { Thread.CurrentThread.ManagedThreadId, xargs.ChangeType, xargs.FullPath }
                                );


                            if (w == null)
                                return;

                            // { ManagedThreadId = 7, ChangeType = Created, FullPath = X:\jsc.svn\examples\javascript\Test\TestFirstBackgroundCompiler\TestFirstBackgroundCompiler\bin\Debug\staging\TestFirstBackgroundCompiler.Application\web\Application.htm }
                            if (xargs.FullPath.Contains(@"\obj\"))
                                return;
                            if (xargs.FullPath.Contains(@"\bin\"))
                                return;


                            //if (!w.EnableRaisingEvents)
                            //    return;
                            if (rebuilding)
                                return;
                            rebuilding = true;
                            Console.Title += " rebuilding...";
                            //w.EnableRaisingEvents = false;

                            //var sln = args.FirstOrDefault(x => x.EndsWith(".sln"));
                            var csproj = args.FirstOrDefault(x => x.EndsWith(".csproj"));

                            // { ChangeType = Changed, FullPath = X:\jsc.svn\examples\javascript\LINQ\test\AutoRefreshTesting\AutoRefreshTesting\obj\Debug }

                            //Thread.Sleep(2100);




                            // are we supposed to udp broadcast now?
                            // are we supposed to have roslyn look at comments?
                            // unless there are no other changes to happen lets do a backgroun build.

                            //w.Dispose();

                            var msbuild = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe";
                            //  { msbuild = C:\Windows\system32\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe }

                            var ww = Stopwatch.StartNew();
                            //Console.WriteLine("lets rebuild! " + new { msbuild });


                            //Thread.Sleep(1000);

                            // %SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe

                            var p = Process.Start(
                                new ProcessStartInfo(msbuild)
                                {
                                    Arguments = "/nologo /verbosity:q " + csproj,

                                    // can we share our console with that sub process?
                                    UseShellExecute = false,
                                }
                            );


                            p.WaitForExit();

                            //Console.WriteLine("rebuild done! " + new { ww.ElapsedMilliseconds });

                            // if there were errors we need to wait the next ctrl s otheriwse we will be reloaded.

                            //nhandled Exception: System.ObjectDisposedException: Cannot access a disposed object.
                            //bject name: 'FileSystemWatcher'.
                            //  at System.IO.FileSystemWatcher.StartRaisingEvents()
                            //  at System.IO.FileSystemWatcher.set_EnableRaisingEvents(Boolean value)

                            // unless we were reset!
                            //if (w != null)
                            //w.EnableRaisingEvents = true;

                            rebuilding = false;
                        };

                    w.Created += yChanged;
                    w.Changed += yChanged;

                };
            #endregion


            //foreach (var sln0 in args.Where(x => x.EndsWith(".sln")))
            foreach (var sln0 in args.Where(x => x.EndsWith(".csproj")))
            {
                // well start monitoring the directory.
                var path = Path.GetDirectoryName(sln0);


                yFileSystemWatcher(path, "*.cs");

                // changing html files shall cause assets library to be rebuilt.
                yFileSystemWatcher(path, "*.htm");
                // X:\jsc.svn\examples\javascript\Test\TestFirstBackgroundCompiler\TestFirstBackgroundCompiler\Design\App.htm

            }

            // since we are not really parsing .sln files we need to be told what other projects to keep an eye on for updates..
            //foreach (var sln0 in args.Where(x => x.EndsWith(".csproj")))
            foreach (var sln0 in args.Where(x => x.EndsWith(".dll")))
            {
                // well start monitoring the directory.
                var path = Path.GetDirectoryName(sln0);

                //             Unhandled Exception: System.ArgumentException: The directory name C:\util\jsc\bin\ScriptCoreLib.Extensions.dll is invalid.
                //at System.IO.FileSystemWatcher..ctor(String path, String filter)

                yFileSystemWatcher(path, "*.dll");
            }

        }


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int x,
            int y,
            int cx,
            int cy,
            int uFlags);

        private const int HWND_TOPMOST = -1;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOSIZE = 0x0001;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);


        // called by post build event
        static void Main(string[] args)
        {

            //1>  Unhandled Exception: System.IO.DirectoryNotFoundException: Could not find a part of the path 'X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelect\bin\DebugAsServerSignal'.
            //1>     at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
            //1>     at System.Threading.EventWaitHandle..ctor(Boolean initialState, EventResetMode mode, String name)
            //1>     at AutoRefreshTestingHost.Program.Main(String[] args) in x:\jsc.svn\examples\javascript\LINQ\test\AutoRefreshTesting\AutoRefreshTestingHost\Program.cs:line 283

            var AsServerSignal = new EventWaitHandle(
                false,
                EventResetMode.AutoReset,

                Environment.CurrentDirectory.GetHashCode() +
                "AsServerSignal"
            );


            // if its a trap then start a server
            var AsServer = AsServerSignal.WaitOne(1);

            Console.WriteLine(new { AsServer } + " continue?");
            if (AsServer)
            {
                //IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
                IntPtr hWnd = GetConsoleWindow();

                Console.WriteLine("time to create a server?" + new { hWnd });

                // are we top most?




                #region WindowPos.xml
                new Thread(
                    delegate()
                    {
                        Thread.Yield();

                        RECT rct0;
                        RECT rct;

                        #region MoveWindow
                        if (File.Exists("WindowPos.xml"))
                        {
                            var WindowPos = XElement.Parse(File.ReadAllText("WindowPos.xml"));

                            MoveWindow(hWnd,
                                (int)WindowPos.Attribute("Left"),
                                (int)WindowPos.Attribute("Top"),
                                (int)WindowPos.Attribute("Right") - (int)WindowPos.Attribute("Left"),
                                (int)WindowPos.Attribute("Bottom") - (int)WindowPos.Attribute("Top"),
                                true
                            );
                        }
                        #endregion


                        SetWindowPos(hWnd,
                          new IntPtr(HWND_TOPMOST),
                          0, 0, 0, 0,
                          SWP_NOMOVE | SWP_NOSIZE);


                        while (GetWindowRect(new HandleRef(null, hWnd), out rct0))
                            while (GetWindowRect(new HandleRef(null, hWnd), out rct))
                            {
                                //Console.Write(".");

                                //Type.mem
                                Thread.Sleep(100);

                                if (rct0.GetHashCode() == rct.GetHashCode())
                                    continue;


                                File.WriteAllText(
                                    "WindowPos.xml",
                                    new XElement(
                                        "WindowPos",
                                        new XAttribute("Left", rct.Left),
                                        new XAttribute("Top", rct.Top),
                                        new XAttribute("Right", rct.Right),
                                        new XAttribute("Bottom", rct.Bottom)
                                    ).ToString()
                                );

                                //Console.WriteLine(new { X, Y, Width, Height });
                                break;
                            }


                    }
                ) { IsBackground = true }.Start();
                #endregion


                var pipeServer = new NamedPipeServerStream(

                Environment.CurrentDirectory.GetHashCode() +

                    "foo", PipeDirection.In,
                    1,
                    PipeTransmissionMode.Message,
                    PipeOptions.WriteThrough);

                while (true)
                {
                    //Console.WriteLine("awaiting for a new connection");

                    pipeServer.WaitForConnection();

                    //Console.WriteLine("client connected to server!");

                    var buffer = new byte[0xffff];

                    var c = pipeServer.Read(buffer, 0, buffer.Length);

                    var xml = XElement.Parse(
                        Encoding.UTF8.GetString(buffer, 0, c)
                    );

                    var old = new { Console.ForegroundColor };
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(new { xml });
                    Console.ForegroundColor = old.ForegroundColor;

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
            var w = new NamedPipeClientStream(".",

                Environment.CurrentDirectory.GetHashCode() +

                "foo", PipeDirection.Out, PipeOptions.WriteThrough);
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
