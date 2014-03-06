using ScriptCoreLib;
using ScriptCoreLibJavaCard;
using javacard.framework;
using javacard.security;
using javacardx.crypto;

namespace JavacardAppletExample2
{
    public partial class Cafebabe : Applet, test
    {
           
        public short GetAnswerFromOtherApplet(APDU apdu)
        {
            short counter = 0;

            var buffer = apdu.getBuffer();

            var CLA = buffer[ISO7816Constants.OFFSET_CLA];
            var INS = buffer[ISO7816Constants.OFFSET_INS];
            var P1 = buffer[ISO7816Constants.OFFSET_P1];
            var P2 = buffer[ISO7816Constants.OFFSET_P2];
            var LC = buffer[ISO7816Constants.OFFSET_LC];


            //buffer = ret; - need to use array copy!!

            var temp = (byte)0xB2;

            if (CLA == (sbyte)0x00 && INS == (sbyte)temp)
            {
                //last name
                if(P1 == (sbyte)0x01 && P2 == (sbyte)0x04)
                {
                    sbyte[] ret = new sbyte[6];
                    ret[0] = (sbyte)0x6b;
                    ret[1] = (sbyte)0x69;
                    ret[2] = (sbyte)0x69;
                    ret[3] = (sbyte)0x76;
                    ret[4] = (sbyte)0x65;
                    ret[5] = (sbyte)0x72;
                    javacard.framework.Util.arrayCopy(ret, 0, buffer, 0, (short)ret.Length);
                    counter = (short)ret.Length;
                }
                //first name
                else if (P1 == (sbyte)0x02 && P2 == (sbyte)0x04)
                {
                    sbyte[] ret = new sbyte[5];
                    ret[0] = (sbyte)0x6a;
                    ret[1] = (sbyte)0x6f;
                    ret[2] = (sbyte)0x6e;
                    ret[3] = (sbyte)0x61;
                    ret[4] = (sbyte)0x73;
                    javacard.framework.Util.arrayCopy(ret, 0, buffer, 0, (short)ret.Length);
                    counter = (short)ret.Length;
                }
            }

            return counter;
        }
        public Shareable getShareableInterfaceObject(AID id, byte param)
        {
            return (test)this;
        }

    }
    [Script]
    public interface test : Shareable
    {
        short GetAnswerFromOtherApplet(APDU apdu);
    }

 
}

