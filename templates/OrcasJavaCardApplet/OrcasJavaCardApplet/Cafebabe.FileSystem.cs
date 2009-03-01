using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLibJavaCard.javacard.framework;
using ScriptCoreLib;
using ScriptCoreLibJavaCard;

namespace OrcasJavaCardApplet
{

	public partial class Cafebabe : Applet
	{
		sbyte[] FileBytes;

		private void ProvideFileSystem(APDU apdu)
		{
			// /send 802000000104
			// /send 8030000004CA22FE33
			// /send 8040000004

			var buffer = apdu.getBuffer();

			var CLA = buffer[ISO7816Constants.OFFSET_CLA];
			var INS = buffer[ISO7816Constants.OFFSET_INS];
			var P1 = buffer[ISO7816Constants.OFFSET_P1];
			var P2 = buffer[ISO7816Constants.OFFSET_P2];
			var LC = buffer[ISO7816Constants.OFFSET_LC];

			unchecked
			{
				if (CLA == (sbyte)0x80)
				{
					if (INS == 0)
					{

						buffer[0] = (sbyte)0xDE;
						buffer[1] = (sbyte)0xAD;
						buffer[2] = (sbyte)0xBE;
						buffer[3] = (sbyte)0xEF;
						apdu.setOutgoingAndSend(0, 4);
					}
					else if (INS == (sbyte)0x20)
					{
						/*
						CREATE FILE: creates the file with the size given in the data field
						APDU: CLA 0x80, INS: 0x20, Lc=0x01, Data=size of the file (max 0xFF=255 bytes)
						*/

						//apdu.setIncomingAndReceive();

						var Size = (short)P1;

						this.FileBytes = new sbyte[Size];
					}
					else if (INS == (sbyte)0x30)
					{
						/*
						WRITE FILE: writes the data bytes from the APDU to the file
						APDU: CLA 0x80, INS: 0x30, Lc=0xXX (number of bytes to write), Data=bytes to write to the file
						*/

						apdu.setIncomingAndReceive();

						var length = (short)LC;

						if (length > this.FileBytes.Length)
							length = (short)this.FileBytes.Length;

						Util.arrayCopyNonAtomic(buffer, (short)(ISO7816Constants.OFFSET_LC + 2), this.FileBytes, 0, length);

					}
					else if (INS == (byte)0x40)
					{
						/*
				
						READ FILE: sends some the data bytes from the file to the terminal
						APDU: CLA 0x80, INS: 0x40, Le=0xXX (number of bytes to read)
						 */

						Util.arrayCopyNonAtomic(this.FileBytes, (short)0, buffer, (short)0, P1);


						apdu.setOutgoingAndSend((short)0, (short)P1);
					}
				}
			}
		}

	}
}
