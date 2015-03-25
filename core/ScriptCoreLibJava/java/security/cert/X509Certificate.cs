using ScriptCoreLib;
using java.net;
using System;
using java.math;

namespace java.security.cert
{
    // http://developer.android.com/reference/java/security/cert/X509Certificate.html
    // http://docs.oracle.com/javase/7/docs/api/java/security/cert/X509Certificate.html
    // http://stackoverflow.com/questions/4414648/javax-security-cert-x509certificate-vs-java-security-cert-x509certificate
    [Script(IsNative = true)]
    public abstract class X509Certificate : Certificate
    {
        // tested by ?
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Security\Cryptography\X509Certificates\X509Certificate.cs

        public abstract Principal getSubjectDN();

        public abstract BigInteger getSerialNumber();

    }
}
