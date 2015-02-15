using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLibNative.SystemHeaders.sys.socket_h;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: Obfuscation(Feature = "script")]

namespace TestNDKUDP
{
    public class xNativeActivity : ScriptCoreLibAndroidNDK.IAssemblyReferenceToken
    {
        // double click on exe./metro?
        // and have it run on android?

        // Error: X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\bin\Debug is not a valid project (AndroidManifest.xml not found).


        // https://msdn.microsoft.com/en-us/library/hh534540.aspx
        static void trace(
            string message = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            => log.__android_log_print(
                log.android_LogPriority.ANDROID_LOG_INFO,
                "xNativeActivity",
                //"line %i file %s",
                "%s:%i %s",
                __arglist(
                    sourceFilePath,
                    sourceLineNumber,
                    message
                )
            );


        unsafe static void trace(
    byte* message,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0)
    => log.__android_log_print(
        log.android_LogPriority.ANDROID_LOG_INFO,
        "xNativeActivity",
        //"line %i file %s",
        "%s:%i %s",
        __arglist(
            sourceFilePath,
            sourceLineNumber,
            message
        )
    );


        unsafe static void tracei(
    string message = "",
    int value = 0,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0)
    => log.__android_log_print(
        log.android_LogPriority.ANDROID_LOG_INFO,
        "xNativeActivity",
        //"line %i file %s",
        "%s:%i %s %i errno: %i %s",
        __arglist(
            sourceFilePath,
            sourceLineNumber,
            message,
            value,

                *errno_h.__errno(),

                errno_h.strerror(*errno_h.__errno())

        )
    );


        // http://stackoverflow.com/questions/24581245/send-broadcast-from-c-code


        [Script(NoDecoration = true)]
        unsafe static void android_main(android_native_app_glue.android_app state)
        {
            // http://elfsharp.hellsgate.pl/examples.shtml
            // https://msdn.microsoft.com/en-us/library/dd554932(VS.100).aspx

            android_native_app_glue.app_dummy();
            //Action<

            trace("enter TestNDKUDP");

            // listen to sockets.
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
            // http://stackoverflow.com/questions/10408980/android-ndk-sockets-network-unreachable
            // http://stackoverflow.com/questions/1593946/what-is-af-inet-and-why-do-i-need-it
            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff543744(v=vs.85).aspx
            // http://stackoverflow.com/questions/6033581/using-socket-in-android-ndk
            // can we load apk from udp? and reload on update?
            var s = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);

            tracei("socket ", (int)s);



            // http://stackoverflow.com/questions/8330808/bind-with-so-reuseaddr-fails

            // https://books.google.ee/books?id=ptSC4LpwGA0C&pg=PA610&lpg=PA610&dq=SO_REUSEADDR+-1+errno+22&source=bl&ots=Ks2AUohlOn&sig=5ytq_BKAlj1sbZVNSGaaqPhM4lg&hl=en&sa=X&ei=glLgVM_SOsjCOa2RgfgN&ved=0CCQQ6AEwATgK#v=onepage&q=SO_REUSEADDR%20-1%20errno%2022&f=false



            //byte hopLimit = 2;
            //  The default value is one for all IP multicast datagrams. 
            //setsockopt(s, IPPROTO_IP, IP_MULTICAST_TTL, &hopLimit, sizeof(byte));

            // http://www.tldp.org/HOWTO/Multicast-HOWTO-6.html
            var localAddr = new in_addr();

            localAddr.s_addr = INADDR_ANY;


            // For multicast sending use an IP_MULTICAST_IF flag with the setsockopt() call. This specifies the interface to be used.
            {
                var status = s.setsockopt(IPPROTO_IP, IP_MULTICAST_IF, &localAddr, sizeof(in_addr));

                // anonymous types like linq expressions?
                tracei("setsockopt IP_MULTICAST_IF: ", status);
            }

            // http://www.phonesdevelopers.com/1817807/

            // http://www.infres.enst.fr/~dax/polys/multicast/api_en.html

            //ip_mreq mreq;
            var mreq = new ip_mreq();

            // "239.1.2.3"
            // ip_mreq3->imr_multiaddr.s_addr = inet_addr((char*)"239.1.2.3");
            mreq.imr_multiaddr.s_addr = "239.1.2.3".inet_addr();

            {
                var status = s.setsockopt(IPPROTO_IP, IP_ADD_MEMBERSHIP, &mreq, sizeof(ip_mreq));
                tracei("setsockopt IP_ADD_MEMBERSHIP: ", status);
            }


            //var bAllowMultiple = true;
            //{
            //    var status = s.setsockopt(SOL_SOCKET, SO_REUSEADDR, &bAllowMultiple, sizeof(bool));

            //    // anonymous types like linq expressions?
            //    tracei("setsockopt SO_REUSEADDR: ", status);
            //}

            // Create the local endpoint
            sockaddr_in localEndPoint;
            ushort gport = 40804;

            localEndPoint.sin_family = AF_INET;
            localEndPoint.sin_addr.s_addr = INADDR_ANY.htonl();
            localEndPoint.sin_port = gport.htons();


            // Bind the socket to the port
            {
                int bindret = s.bind((sockaddr*)&localEndPoint, sizeof(sockaddr_in));

                tracei("bind: ", bindret);
            }

            var ok = true;


            while (ok)
            {

                var buff = stackalloc byte[0xfff];

                sockaddr_in sender;
                var sizeof_sender = sizeof(sockaddr_in);

                trace("before recvfrom");

                // http://pubs.opengroup.org/onlinepubs/009695399/functions/recvfrom.html
                // Upon successful completion, recvfrom() shall return the length of the message in bytes. 
                var recvfromret = s.recvfrom(buff, 0xfff, 0, (sockaddr*)&sender, &sizeof_sender);

                //I/xNativeActivity(24024): X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\xNativeActivity.cs:167 recvfrom:  116 errno: 22 Invalid argument
                //I/xNativeActivity(24024): X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\xNativeActivity.cs:168 SenderAddrSize:  16 errno: 22 Invalid argument
                tracei("recvfrom: ", recvfromret);
                //tracei("sockaddr_in: ", sizeof_sender);

                buff[recvfromret] = 0;


                //trace(sender.sin_addr.inet_ntoa());
                trace(buff);

            }

            // do we have XElement in native mode yet?



            // http://pubs.opengroup.org/onlinepubs/7908799/xns/arpainet.h.html
            // http://stackoverflow.com/questions/15569012/android-udp-client-not-able-to-receive-data-on-non-rooted-phone
            // http://www.phonesdevelopers.com/1817807/

            // !!1
            // https://www.mail-archive.com/android-developers@googlegroups.com/msg115225.html

            // http://www.gta.ufrj.br/ensino/eel878/sockets/inet_ntoaman.html

            // could jsc web apps be turned into ndk servers?


            //I/xNativeActivity(23301): X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\xNativeActivity.cs:74 enter TestNDKUDP
            //I/xNativeActivity(23301): X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\xNativeActivity.cs:85 socket  28 errno: 0
            //I/xNativeActivity(23301): X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\xNativeActivity.cs:112 setsockopt SO_REUSEADDR:  -1 errno: 22
            //I/xNativeActivity(23301): X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\xNativeActivity.cs:129 setsockopt IP_MULTICAST_IF:  -1 errno: 99
            //I/xNativeActivity(23301): X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\xNativeActivity.cs:143 setsockopt IP_ADD_MEMBERSHIP:  -1 errno: 19
            //I/xNativeActivity(23301): X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\xNativeActivity.cs:159 bind:  -1 errno: 98
            //I/xNativeActivity(23301): X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\xNativeActivity.cs:168 before recvfrom
            //I/CwMcuSensor(  461): CwMcuSensor::flush: fd = 194, sensors_id = 0, path = /sys/class/htc_sensorhub/sensor_hub/flush, err = 0
        }

    }
}
