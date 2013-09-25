using java.io;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace ShellWithPing
{

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component, PING
    {
    
        // jsc cannot see explicit interfaces just yet.
        public void PING_InvokeAsync(string host, Action<string> y)
        {

            // Implementation not found for type import :
            // System.String :: Char[] ToCharArray()
            // Did you forget to add the [Script] attribute?
            // type: ShellWithPing.ApplicationWebService offset: 0x0002  method:Void PING_InvokeAsync(System.String, System.Action`1[System.String])
            //System.NotSupportedException:

            //             Implementation not found for type import :
            // System.Char :: Boolean IsLetterOrDigit(Char)
            // Did you forget to add the [Script] attribute?
            // type: ShellWithPing.ApplicationWebService offset: 0x0001  method:Boolean <PING_InvokeAsync>b__0(Char)
            //System.NotSupportedException:


            //            [javac] Compiling 289 source files to R:\bin\classes
            //[javac] R:\src\ShellWithPing\ApplicationWebService.java:58: <T>Of(T[]) in ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator_1 cannot be applied to <java.lang.Character>(char[])
            //[javac]         if (__Enumerable.<Character>Any(__SZArrayEnumerator_1.<Character>Of(__String.ToCharArray(host)), ApplicationWebService.CS___9__CachedAnonymousMethodDelegate1))
            //[javac]                                                              ^
            //[javac] Note: R:\src\ScriptCoreLibJava\BCLImplementation\System\Threading\__Thread.java uses or overrides a deprecated API.

            var c = host.ToCharArray();

            #region reverse generic array generation
            var cc = new List<char>();

            foreach (var item in c)
            {
                cc.Add(item);
            }
            #endregion


            if (cc.Any(k => !(char.IsNumber(k) || char.IsLetter(k) || k == '.')))
            {
                y("access denied.");

                return;
            }

#if ShellAsync
            /*
C:\Users\Arvo>ping /?

Usage: ping [-t] [-a] [-n count] [-l size] [-f] [-i TTL] [-v TOS]
            [-r count] [-s count] [[-j host-list] | [-k host-list]]
            [-w timeout] [-R] [-S srcaddr] [-4] [-6] target_name

Options:
    -t             Ping the specified host until stopped.
                   To see statistics and continue - type Control-Break;
                   To stop - type Control-C.
    -a             Resolve addresses to hostnames.
    -n count       Number of echo requests to send.
    -l size        Send buffer size.
    -f             Set Don't Fragment flag in packet (IPv4-only).
    -i TTL         Time To Live.
    -v TOS         Type Of Service (IPv4-only. This setting has been deprecated
                   and has no effect on the type of service field in the IP Header).
    -r count       Record route for count hops (IPv4-only).
    -s count       Timestamp for count hops (IPv4-only).
    -j host-list   Loose source route along host-list (IPv4-only).
    -k host-list   Strict source route along host-list (IPv4-only).
    -w timeout     Timeout in milliseconds to wait for each reply.
    -R             Use routing header to test reverse route also (IPv6-only).
    -S srcaddr     Source address to use.
    -4             Force using IPv4.
    -6             Force using IPv6.
             
             */
            ShellAsync("ping " + host + " -a -n 1 -w 4000",
                yy =>
                {
                    // time=592ms 

                    var ms = yy.SkipUntilOrEmpty(" time=").TakeUntilOrEmpty("ms ");
                    if (string.IsNullOrEmpty(ms))
                        return;

                    y(ms);
                }
            );
#elif AndroidShellAsync
            // http://linux.die.net/man/8/ping

            ShellAsync("ping -c 1 -w 4 " + host,
                yy =>
                {
                    // time=592ms 

                    var ms = yy.SkipUntilOrEmpty("time=").TakeUntilOrEmpty(" ms");
                    if (string.IsNullOrEmpty(ms))
                        return;

                    y(ms);
                }
            );
#else
            Thread.Sleep(500);

            y("simulated ping to [" + host + "] complete.");
#endif
        }

        public void EchoAsync(string e, Action<string> y)
        {
            if (e == "jsc")
            {
                y("What do you want to create today?");
                y("create.jsc-solutions.net");
                return;
            }

            if (e.StartsWith("ping "))
            {
                PING_InvokeAsync(e.SkipUntilOrEmpty("ping "), y);
                return;
            }
            y("echo: " + e);
        }


        public void ShellAsync(string e, Action<string> y)
        {
#if AndroidShellAsync
            // http://www.android.pk/blog/general/launch-app-through-adb-shell/
            //  am start -a android.intent.action.MAIN -n com.android.settings/.Settings
            // am start tel:210-385-0098
            // am start -a android.intent.action.CALL tel:245007
            // am start -a android.intent.action.SENDTO "sms:5245007" -e "sms_body" "heyy"   && input keyevent 22 && input keyevent 66
            // am start -a android.intent.action.SENDTO -d sms:1234567890 --es sms_body ohai --ez exit_on_sent true
            // am start -a android.intent.action.SENDTO -d smsto:245007 --es sms_body ":*" --ez exit_on_sent true && am start -a android.intent.action.SENDTO -d sms:5245007 --es sms_body ":*" --ez exit_on_sent true && input keyevent 22 && input keyevent 66
            // pm list packages
            // pm list packages -f
        //http://stackoverflow.com/questions/11201659/android-adb-shell-dumpsys-tool
            // am start -S -e sms_body 'your message body' \
          //-e address receiver -t 'vnd.android-dir/mms-sms' \
          //com.android.mms/com.android.mms.ui.ComposeMessageActivity \
          //&& adb shell input keyevent 66

        //am start -n com.google.android.youtube/.PlayerActivity -d http://www.youtube.com/watch?v=MTT-crZBB0k
            // http://stackoverflow.com/questions/7095470/android-read-send-text-messages-on-ubuntu

            //         System.InvalidOperationException: Sequence contains more than one element
            //at System.Linq.Enumerable.SingleOrDefault[TSource](IEnumerable`1 source)
            //at jsc.Languages.Java.JavaCompiler.GetArrayEnumeratorType() in x:\jsc.internal.svn\compiler\jsc\Languages\Java\JavaCompiler.overrride.cs:line 52
            //at jsc.Languages.Java.JavaCompiler.GetImportTypes(Type t, Boolean bExcludeJavaLang) in x:\jsc.internal.svn\compiler\jsc\Languages\Java\JavaCompiler.WriteImportTypes.cs:line 363
            //at jsc.Languages.Java.JavaCompiler.WriteImportTypes(Type ContextType) in x:\jsc.internal.svn\compiler\jsc\Languages\Java\JavaCompiler.WriteImportTypes.cs:line 22
            //at jsc.Languages.Java.JavaCompiler.CompileType(Type z) in x:\jsc.internal.svn\compiler\jsc\Languages\Java\JavaCompiler.CompileType.cs:line 43
            //at jsc.Languages.CompilerJob.<>c__DisplayClass1a.<CompileJava>b__17(Type xx) in x:\jsc.internal.svn\compiler\jsc\Languages\Java\CompilerJob.cs:line 120

            //            IsArrayEnumerator: ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator`1, ScriptCoreLibAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            //IsArrayEnumerator: ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator`1, ScriptCoreLibJava, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null


            try
            {
                // http://stackoverflow.com/questions/9062182/android-icmp-ping

                var p = new ProcessBuilder(new[] { "sh" }).redirectErrorStream(true).start();

                var os = new DataOutputStream(p.getOutputStream());
                //os.writeBytes(e + '\n');
                os.writeBytes(e + "\n");
                os.flush();

                // Close the terminal
                os.writeBytes("exit\n");
                os.flush();

                // read ping replys
                var reader = new BufferedReader(new InputStreamReader(p.getInputStream()));
                string line = reader.readLine();


                while (line != null)
                {
                    y(line);
                    line = reader.readLine();
                }
            }
            catch (System.Exception ex)
            {
                y("AndroidShellAsync error: " + new { ex.Message });

            }

#elif ShellAsync
            try
            {
                var p = System.Diagnostics.Process.Start(
                    new ProcessStartInfo("cmd")
                    {

                        //ex = {"The Process object must have the UseShellExecute property set to false in order to redirect IO streams."}

                        UseShellExecute = false,

                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }

                    );
                y("pid: " + p.Id);
                y("");


                //ex = {"Timeouts are not supported on this stream."}
                //p.StandardOutput.BaseStream.ReadTimeout = 4000;
                //p.StandardError.BaseStream.ReadTimeout = 4000;

                var StandardOutput = "";
                var StandardError = "";

                p.StandardInput.WriteLine(e);
                p.StandardInput.WriteLine("exit");

                var ww = new AutoResetEvent(false);

            #region timeout
                var rr = new System.Threading.Thread(
                    delegate()
                    {
                        StandardOutput = p.StandardOutput.ReadToEnd();
                        StandardError = p.StandardError.ReadToEnd();

                        ww.Set();
                    }
                );

                rr.Start();
            #endregion


            #region timeout
                new System.Threading.Thread(
                    delegate()
                    {
                        System.Threading.Thread.Sleep(5000);


                        if (rr.IsAlive)
                        {
                            rr.Abort();
                        }
                        ww.Set();

                        //ex = {"Process must exit before requested information can be determined."}

                        if (p.HasExited)
                            return;


                        p.Kill();
                    }
                ).Start();
            #endregion

                System.Threading.Thread.Yield();

                ww.WaitOne();



                y(StandardOutput);
                y(StandardError);
                y("");
                y("exit: " + p.ExitCode);

            }
            catch (System.Exception ex)
            {

                Debugger.Break();
            }
            finally
            {
                y = null;
            }
#else
            y("ShellAsync not implemented.");
#endif

        }
    }



    interface PING
    {
        void PING_InvokeAsync(string host, Action<string> y);
    }
}
