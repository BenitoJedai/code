using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace Mahjong.NetworkCode.Shared
{
	[Script]
	public class VirtualClient 
	{

		public Communication.IEvents Events { get; set; }
		public Communication.IMessages Messages { get; set; }


		
	}
}
