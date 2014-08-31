using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebCrypto
{
    // https://dvcs.w3.org/hg/webcrypto-api/raw-file/tip/spec/Overview.html#dfn-JsonWebKey

    //dictionary JsonWebKey {
    //  // The following fields are defined in Section 3.1 of JSON Web Key
    //  DOMString kty;
    //  DOMString use;
    //  sequence<DOMString> key_ops;
    //  DOMString alg;

    //  // The following fields are defined in JSON Web Key Parameters Registration
    //  boolean ext;

    //  // The following fields are defined in Section 6 of JSON Web Algorithms
    //  DOMString crv;
    //  DOMString x;
    //  DOMString y;
    //  DOMString d;
    //  DOMString n;
    //  DOMString e;
    //  DOMString p;
    //  DOMString q;
    //  DOMString dp;
    //  DOMString dq;
    //  DOMString qi;
    //  sequence<RsaOtherPrimesInfo> oth;
    //  DOMString k;
    //};

    //dictionary RsaOtherPrimesInfo {
    //  // The following fields are defined in Section 6.3.2.7 of JSON Web Algorithms
    //  DOMString r;
    //  DOMString d;
    //  DOMString t;
    //};

    [Script]
    public class RsaOtherPrimesInfo
    {
        // The following fields are defined in Section 6.3.2.7 of JSON Web Algorithms
        public string r;
        public string d;
        public string t;
    }

    // Shared?
    [Script]
    public class JsonWebKey
    {
        // http://self-issued.info/docs/draft-ietf-jose-json-web-key.html
        // http://msdn.microsoft.com/en-us/library/system.identitymodel.tokens.tokenvalidationparameters%28v=vs.114%29.aspx
        // http://stackoverflow.com/questions/25372035/not-able-to-validate-json-web-token-with-net-key-to-short
        // http://forums.asp.net/t/1992049.aspx?Signing+JSON+Web+Token+JWT+

        // http://tools.ietf.org/html/draft-ietf-jose-json-web-key-31
        // alg = RSA-OAEP-256, 
        // e = AQAB, 
        // ext = true, 
        // kty = RSA, 

        //  integers are represented using the base64url encoding
        //of their big endian representations.
        // n = 8tGdxZBpFAIQN3Pzc-7NC_vDF26dleCMGDY7egB8Q136YlqfB7tRpYMU9k88MXGDIleUyEPoDT03yopH8B3Cuio61Wzk-6uXTl6WGjK-FvpxiJWMxa6rXdng7cCyzsG5rah3wI8B3ko4NhHO7NrdKoWG4-y1qxWi2JdAv1g8DLKFUqTuu4siLXPEXvHdWcV4booyeVzCsIf-xq2Zrh7hLbhN83_6bCG0KdkQCIYUgqbI2kHOI4acqTKcXE5_W2cqbw0GStQOyoqClNb0k7VIyufiYpKCRv5176NOmTjFeVBVRhnHkRn96n4Fc4EwLL-KBAj9sfJ1dVrQ2pS-IHIe3w


        // some RSA private key representations do not include the 
        // public exponent e, 
        // but only include the modulus n 
        // and the private exponent d. 
        // This is true, for instance, of the Java RSAPrivateKeySpec API, which does not include the public exponent e as a parameter. So as to enable RSA key blinding, such representations should be avoided. For Java, the RSAPrivateCrtKeySpec API can be used instead. 

        // The following fields are defined in Section 3.1 of JSON Web Key
        public string kty;
        public string use;
        public string[] key_ops;
        public string alg;
        public bool ext;
        public string crv;
        public string x;
        public string y;
        public string d;
        public string n;
        public string e;
        public string p;
        public string q;
        public string dp;
        public string dq;
        public string qi;
        public RsaOtherPrimesInfo[] oth;
        public string k;
    }
}
