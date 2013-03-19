using android.content;
using ScriptCoreLib;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using java.lang;
using java.io;

namespace com.abstractatech.adminshell
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {

        public void ShellAsync(string e, Action<string> y)
        {
#if Android
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

#else
            y("ShellAsync not implemented.");
#endif

        }


        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            // http://tools.ietf.org/html/rfc2617#section-3.2.1

            var Authorization = h.Context.Request.Headers["Authorization"];

            var AuthorizationLiteralEncoded = Authorization.SkipUntilOrEmpty("Basic ");
            var AuthorizationLiteral = Encoding.ASCII.GetString(
                Convert.FromBase64String(AuthorizationLiteralEncoded)
            );

            var AuthorizationLiteralCredentials = new
            {
                user = AuthorizationLiteral.TakeUntilOrEmpty(":"),
                password = AuthorizationLiteral.SkipUntilOrEmpty(":"),
            };

            System.Console.WriteLine(new { AuthorizationLiteralCredentials }.ToString());

            var a = h.Applications.FirstOrDefault(k => k.TypeName == "a");

            if (h.Context.Request.Path == "/a")
            {
                if (!string.IsNullOrEmpty(AuthorizationLiteralCredentials.user))
                    if (!string.IsNullOrEmpty(AuthorizationLiteralCredentials.password))
                    {
#if Android
                        var c = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;

                        var intent = new Intent(c, typeof(foo.NotifyService).ToClass());

                        intent.putExtra("data0", AuthorizationLiteralCredentials.user + " is using Remote Web Shell");

                        c.startService(intent);
#endif



                        h.Context.Response.ContentType = "text/javascript";
                        h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");


                        //Implementation not found for type import :
                        //type: System.Web.HttpResponse
                        //method: Void AppendCookie(System.Web.HttpCookie)
                        // not working on android?
                        h.Context.Response.SetCookie(
                            new HttpCookie("foo", "bar")
                        );

                        h.WriteSource(a);
                        h.CompleteRequest();
                        return;
                    }

                h.Context.Response.StatusCode = 401;
                h.Context.Response.AddHeader(
                    "WWW-Authenticate",
                    "Basic realm=\"Android\""
                );

                // flush?
                h.Context.Response.Write(" ");
                h.CompleteRequest();

                return;
            }
        }
    }
}
