using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleChat2.Network;
using System.ComponentModel;

namespace SimpleChat2.Buffer
{
	public class ChatRequest : IDefaultRequestPath
	{
		public const string DefaultTTL = "100";

		public string DefaultRequestPath { get; set; }

		public ChatRequest()
		{
			this.DefaultRequestPath = "/chat/request.php";
		}

		public string request;
		public string target;
		public string myname;
		public string name;
		public string myip;
		public string ip;
		public string ttl;
		public string message;

		public static class Requests
		{
			public class findname : ChatRequest
			{
				//Nime olemasolu küsimine. Päringute edasisaatmisel rakendustes tuleks säilitada myname parameetri väärtust, et viimane sendname saaks vastuse saata esialgsele kasutajale.
				//http://ahman.no-ip.info/chat/request.php?request=findname&target=SIHT_KASUTAJA&myname=SAATJA_KASUTAJA&name=NIMI&myip=22.22.33.44:6666&ttl=TTL

				public findname()
				{

				}

				public findname(string target, string myname, string name, string myip, string ttl)
				{
					this.request = "findname";
					this.target = target;
					this.myname = myname;
					this.name = name;
					this.myip = myip;
					this.ttl = ttl;
				}

	
			}

			public class asknames : ChatRequest
			{
				//Nimed/ip_pluss_port faili küsimine.
				//http://ahman.no-ip.info/chat/request.php?request=asknames&target=SIHT_KASUTAJA&myname=SAATJA_KASUTAJA&ttl=TTL

				public asknames()
				{

				}
				public asknames(string target, string myname, string ttl)
				{
					this.request = "asknames";
					this.target = target;
					this.myname = myname;
					this.ttl = ttl;
				}

				public asknames(string target, string myname)
					: this(target, myname, DefaultTTL)
				{
				}
			}

			public class sendname : ChatRequest
			{
				//Vastuse saatmine nime küsimisele ja ka minu nime teatamine teistele.
				//http://ahman.no-ip.info/chat/request.php?request=sendname&target=SIHT_KASUTAJA&myname=SAATJA_KASUTAJA&name=NIMI&ip=22.22.33.44:6666&ttl=TTL
				public sendname()
				{

				}
				public sendname(string target, string myname, string name, string ttl)
				{
					this.request = "sendname";
					this.target = target;
					this.myname = myname;
					this.name = name;
					this.ttl = ttl;
				}
			}

			public class sendmessage : ChatRequest
			{
				//Chatisõnumi saatmine.
				//http://ahman.no-ip.info/chat/request.php?request=sendmessage&target=SIHT_KASUTAJA&myname=SAATJA_KASUTAJA&myip=22.33.44.55:6666&message=asasas&ttl=TTL
				public sendmessage()
				{

				}
				public sendmessage(string target, string myname, string myip, string message, string ttl)
				{
					this.request = "sendmessage";
					this.target = target;
					this.myname = myname;
					this.myip = myip;
					this.message = message;
					this.ttl = ttl;
				}
			}

			public class sendnames : ChatRequest
			{
				//Nimed/ip_pluss_port faili sisu saatmine küsijale. Selle päringuga asendatakse asknames requesti response, mille alusel tavalises protokollis nimede faili sisu edastatakse. Nimede faili sisu tuleb edastada POST päringuga names nimelises parameetris allolevale URL-le
				//http://ahman.no-ip.info/chat/request.php?request=sendnames&target=SIHT_KASUTAJA&myname=SAATJA_KASUTAJA&ttl=TTL

				public sendnames()
				{

				}
				public sendnames(string target, string myname, string ttl)
				{
					this.request = "sendmessage";
					this.target = target;
					this.myname = myname;
					this.ttl = ttl;
				}
			}
		}




	}
}
