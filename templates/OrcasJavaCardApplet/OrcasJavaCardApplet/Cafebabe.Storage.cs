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
		[Script]
		public class File
		{
			// string name
			// stream data
			public sbyte[] Name;

			public sbyte[] Data;

			public File NextFile;
		}

		public File FirstFile;

		public sbyte Count
		{
			get
			{
				sbyte c = 0;
				var p = FirstFile;

				while (p != null)
				{
					c++;
					p = p.NextFile;
				}

				return c;
			}
		}
	

		private void ProvideStorage(APDU apdu)
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
				if (CLA == (sbyte)0x10)
				{
					#region Add File
					if (INS == (sbyte)0x1)
					{
						var n = new File { NextFile = FirstFile };

						apdu.setIncomingAndReceive();

						n.Name = new sbyte[LC];

						Util.arrayCopyNonAtomic(buffer, (short)(ISO7816Constants.OFFSET_LC + 1), n.Name, 0, LC);

						this.FirstFile = n;

						reply(apdu, 6, 6, 6, Count);
					}
					#endregion

					#region Count
					if (INS == (sbyte)0x2)
					{
						reply(apdu, 6, 6, 6, Count);
					}
					#endregion

					//reply(apdu, 6, 6, 1, 1);
				}
			}
		}

	}
}
