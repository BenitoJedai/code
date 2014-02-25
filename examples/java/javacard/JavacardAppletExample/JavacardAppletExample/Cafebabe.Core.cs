using ScriptCoreLib;
using ScriptCoreLibJavaCard;
using javacard.framework;
using javacard.security;
using javacardx.crypto;

namespace JavacardAppletExample2
{
    public partial class Cafebabe : Applet, test
    {
        public sbyte GetAnswerFromOtherApplet()
        {
            return (sbyte)0x01;
        }
        public Shareable getShareableInterfaceObject(AID id, byte param)
        {
            return (test)this;
        }

    }
    [Script]
    public interface test : Shareable
    {
        sbyte GetAnswerFromOtherApplet();
    }

 
}

