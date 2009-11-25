using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleChat2.Network;

namespace SimpleChat2.Buffer
{
	public class ChatCheck : IDefaultRequestPath
	{
		public string DefaultRequestPath { get; set; }

		public ChatCheck()
		{
			this.DefaultRequestPath = "/chat/check.php";
		}

		//Puhvrilt kasutajale suunatud päringute küsimiseks kasutatav skript
		//Parameeter myname näitab kasutaja nime, kelle jaoks säilitatud käsklused puhvrist tagastatakse.
		//http://ahman.no-ip.info:6666/chat/check.php?myname=PÄRINGUTE_KÜSIJA_NIMI
		public string myname;
	}
}
