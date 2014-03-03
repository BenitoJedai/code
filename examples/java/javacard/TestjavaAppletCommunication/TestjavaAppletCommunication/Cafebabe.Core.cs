using ScriptCoreLib;
using ScriptCoreLibJavaCard;
using javacard.framework;
using javacard.security;
using javacardx.crypto;
using JavacardAppletExample2;
    
namespace JavacardAppletExample
{
    public partial class Cafebabe2 : Applet
    {
        private void response(APDU apdu)
        {
            //upload "E:\jsc.svn\examples\java\javacard\JavacardAppletExample\JavacardAppletExample\bin\Release\web\release\JavacardAppletExample\javacard\JavacardAppletExample.cap"
            //upload "E:\jsc.svn\examples\java\javacard\TestjavaAppletCommunication\TestjavaAppletCommunication\bin\Release\web\release\JavacardAppletExample\javacard\JavacardAppletExample.cap"
            //install A0A1A2A3A40002 A0A1A2A3A4000202
            //install A0A1A2A3A40003 A0A1A2A3A4000301

            if (APDU.getProtocol() == APDU.PROTOCOL_MEDIA_USB)
            {
                
                byte[] t = { 0xa0, 0xa1, 0xa2, 0xa3, 0xa4, 0x00, 0x02, 0x02 };
                var casted = (sbyte[])(object)t;
                var s = new AID(casted, 0, (sbyte)casted.Length);


                if (casted == null)
                {
                    reply(apdu, 6, 6, 6, 6);
                    return;
                }

                if (s == null)
                {
                    reply(apdu, 5, 5, 5, 5);
                    return;
                }

                var temp = JCSystem.getAppletShareableInterfaceObject(s, 0);

                if (temp == null)
                {
                    reply(apdu, 1, 1, 1, 1);
                }
                else
                {
                    //reply(apdu, 3, 3, 3, 3);
                    test sio = (test)temp;
                    var ret = sio.GetAnswerFromOtherApplet();
                    if (sio == null)
                    {
                        reply(apdu, 9, 9, 9, 9);
                    }
                    else
                    {
                        if (ret == (sbyte)0x01)
                        {
                            reply(apdu, 2, 2, 2, 2);
                        }
                        else
                        {
                            reply(apdu, 3, 3, 3, 3);
                        }
                    }
                }
            }
            else
            {
                reply(apdu, 5, 5, 5, 5);
            }
        }
    }
}
