using ScriptCoreLib;
using ScriptCoreLibJavaCard;
using javacard.framework;
using javacard.security;
using javacardx.crypto;
using JavacardAppletExample2;
using System.Collections.Generic;
    
namespace JavacardAppletExample
{
    public partial class Cafebabe2 : Applet
    {
        private void response(APDU apdu)
        {
        //     static void reply(APDU apdu, sbyte a0, sbyte a1, sbyte a2, sbyte a3)
        //{
        //    var buffer = apdu.getBuffer();
        //    buffer[0] = a0;
        //    buffer[1] = a1;
        //    buffer[2] = a2;
        //    buffer[3] = a3;
        //    apdu.setOutgoingAndSend(0, 4);
        //}


            //upload "E:\jsc.svn\examples\java\javacard\JavacardAppletExample\JavacardAppletExample\bin\Release\web\release\JavacardAppletExample\javacard\JavacardAppletExample.cap"
            //upload "E:\jsc.svn\examples\java\javacard\TestjavaAppletCommunication\TestjavaAppletCommunication\bin\Release\web\release\JavacardAppletExample\javacard\JavacardAppletExample.cap"
            //install A0A1A2A3A40002 A0A1A2A3A4000202
            //install A0A1A2A3A40003 A0A1A2A3A4000301

            if (APDU.getProtocol() != APDU.PROTOCOL_MEDIA_USB)
            {

                var buffer = apdu.getBuffer();

                var CLA = buffer[ISO7816Constants.OFFSET_CLA];
                var INS = buffer[ISO7816Constants.OFFSET_INS];
                var P1 = buffer[ISO7816Constants.OFFSET_P1];
                var P2 = buffer[ISO7816Constants.OFFSET_P2];
                var LC = buffer[ISO7816Constants.OFFSET_LC];

                //var rets = new sbyte[5];
                //rets[0] = CLA;
                //rets[1] = INS;
                //rets[2] = P1;
                //rets[3] = P2;
                //rets[4] = LC;

                //buffer = rets;
                //apdu.setOutgoingAndSend(0, (short)rets.Length);


                //reply(apdu, INS, P1, P2, LC);
                List<sbyte[]> whiteList = new List<sbyte[]>();

                byte[] masterFile = new byte[] { 0x00, 0xA4, 0x00, 0x0C };
                byte[] EEEEcatalogue = new byte[] { 0x00, 0xA4, 0x01, 0x0C, 0x02, 0xEE, 0xEE };
                byte[] setSecEnv1 = new byte[] { 0x00, 0x22, 0xF3, 0x01 };
                byte[] pin2VerifyPartly = new byte[] { 0x00, 0x20, 0x00, 0x02 }; //additional Pin2 len and Pin2 as ASCII


                whiteList.Add((sbyte[])(object)masterFile);
                whiteList.Add((sbyte[])(object)EEEEcatalogue);
                whiteList.Add((sbyte[])(object)setSecEnv1); 



                byte[] AID = { 0xa0, 0xa1, 0xa2, 0xa3, 0xa4, 0x00, 0x02, 0x02 };
                var casted = (sbyte[])(object)AID;
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
                    var ret = sio.GetAnswerFromOtherApplet(apdu);
                    if (sio == null)
                    {
                        reply(apdu, 9, 9, 9, 9);
                    }
                    else
                    {
                        if (ret != 0)
                        {
                            apdu.setOutgoingAndSend(0, ret);
                            
                                //reply(apdu, 2, 2, 2, 2);
                                //var b = apdu.getBuffer();
                                //b = ret;
                                //apdu.setOutgoingAndSend(0, (short)ret.Length);
                            //}
                        }
                        else
                        {
                            reply(apdu, 8, 8, 8, 8);
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
