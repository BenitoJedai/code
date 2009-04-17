using ScriptCoreLib;
using ScriptCoreLibJavaCard;
using ScriptCoreLibJavaCard.javacard.framework;
using ScriptCoreLibJavaCard.javacard.security;
using ScriptCoreLibJavaCard.javacardx.crypto;

namespace OrcasJavaCardApplet
{
	[Script]
	[AID(0x01)]
	public partial class Cafebabe : Applet
	{
		private Cafebabe(sbyte[] bArray, short bOffset, sbyte bLength)
		{
			//InitializeCrypto();
		}


		public static void install(sbyte[] bArray, short bOffset, sbyte bLength)
		{
			(new Cafebabe(bArray, bOffset, bLength)).register();
		}

		public override void process(APDU apdu)
		{
	

			if (this.selectingApplet())
			{
				return;
			}

			ProvideStorage(apdu);
			//ProvideFileSystem(apdu);
			//ProvideCrypto(apdu);

			//reply(apdu, 4, 3, 2, 1);
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
