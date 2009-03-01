using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLibJavaCard.javacard.framework;
using ScriptCoreLib;
using ScriptCoreLibJavaCard;
using ScriptCoreLibJavaCard.javacard.security;
using ScriptCoreLibJavaCard.javacardx.crypto;

namespace OrcasJavaCardApplet
{

	public partial class Cafebabe : Applet
	{

		// code of CLA byte in the command APDU header
		public const sbyte CLA = (sbyte)0x7F;

		// codes of INS byte in the command APDU header
		public const sbyte INS_CREATE_RSA_KEYPAIR = (sbyte)0x20;
		public const sbyte INS_READ_RSA_MODULUS = (sbyte)0x22;
		public const sbyte INS_READ_RSA_EXPONENT = (sbyte)0x24;

		public const sbyte INS_SET_DES_KEY = (sbyte)0x30;
		public const sbyte INS_DES_ENCRYPT = (sbyte)0x32;

		public const sbyte INS_INIT_SIGNATURE = (sbyte)0x40;
		public const sbyte INS_SIGN = (sbyte)0x42;


		readonly KeyPair rsaPair = new KeyPair(KeyPair.ALG_RSA_CRT, (short)1024);
		readonly sbyte[] textplain = new sbyte[256];
		readonly sbyte[] textenc = new sbyte[256];
		Signature signature;
		DESKey deskey;
		Cipher ciph;

		void InitializeCrypto()
		{
			this.signature = Signature.getInstance(Signature.ALG_RSA_SHA_PKCS1, false);
			this.deskey = (DESKey)KeyBuilder.buildKey(KeyBuilder.TYPE_DES, KeyBuilder.LENGTH_DES3_2KEY, false);
			this.ciph = Cipher.getInstance(Cipher.ALG_DES_ECB_NOPAD, false);
		}

		private void ProvideCrypto(APDU apdu)
		{
			var buffer = apdu.getBuffer();

			var CLA = buffer[ISO7816Constants.OFFSET_CLA];
			var INS = buffer[ISO7816Constants.OFFSET_INS];
			var P1 = buffer[ISO7816Constants.OFFSET_P1];
			var P2 = buffer[ISO7816Constants.OFFSET_P2];
			var LC = buffer[ISO7816Constants.OFFSET_LC];

			if (CLA != Cafebabe.CLA)
				ISOException.throwIt(ISO7816Constants.SW_CLA_NOT_SUPPORTED);

			if (INS == Cafebabe.INS_CREATE_RSA_KEYPAIR)
			{
				genRSAKeyPair(apdu);
			}
			else
			{
				reply(apdu, 6, 7, 8, 9);
			}
		}



		public void genRSAKeyPair(APDU apdu)
		{
			this.rsaPair.genKeyPair();

			var p = this.rsaPair.getPublic();


		}

		public void readRSAmodulus(APDU apdu)
		{

		}

		public void readRSAexponent(APDU apdu)
		{

		}

		public void set_des_key(APDU apdu)
		{

		}

		public void des_encrypt(APDU apdu)
		{

		}

		public void init_signature(APDU apdu)
		{

		}

		public void sign(APDU apdu)
		{

		}
	}
}
