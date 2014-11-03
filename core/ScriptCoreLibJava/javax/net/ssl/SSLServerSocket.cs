using java.security;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/SSLServerSocket.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/SSLServerSocket.html
    [Script(IsNative = true)]
    public abstract class SSLServerSocket
    {
        //22e0:02:01 007a:04c1 ScriptCoreLibJava create ScriptCoreLibJava::javax.net.ssl.SSLServerSocket

        //error at CopyType:
        //         * Type must be declared abstract if any of its methods are abstract.
        //         * javax.net.ssl.SSLServerSocket 020000a1
        //22e0:02:01 RewriteToAssembly error: System.InvalidOperationException: Type must be declared abstract if any of its methods are abstract.       

    }
}
