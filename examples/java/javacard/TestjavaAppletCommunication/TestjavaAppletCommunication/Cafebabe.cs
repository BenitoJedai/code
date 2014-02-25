using ScriptCoreLib;
using ScriptCoreLibJavaCard;
using javacard.framework;
using javacard.security;
using javacardx.crypto;

namespace JavacardAppletExample
{
    [Script]
    [AID(0x01)]
    public partial class Cafebabe2 : Applet
    {
        private Cafebabe2(sbyte[] bArray, short bOffset, sbyte bLength)
        {
            //InitializeCrypto();
        }


        public static void install(sbyte[] bArray, short bOffset, sbyte bLength)
        {
            (new Cafebabe2(bArray, bOffset, bLength)).register();
        }

        public override void process(APDU apdu)
        {


            if (this.selectingApplet())
            {
                return;
            }
            
            //ProvideStorage(apdu);
            //ProvideFileSystem(apdu);
            //ProvideCrypto(apdu);

            //reply(apdu, 4, 3, 2, 1);

            response(apdu);

        }


        static void reply(APDU apdu, sbyte a0, sbyte a1, sbyte a2, sbyte a3)
        {
            var buffer = apdu.getBuffer();
            buffer[0] = a0;
            buffer[1] = a1;
            buffer[2] = a2;
            buffer[3] = a3;
            apdu.setOutgoingAndSend(0, 4);
        }
    }
}
