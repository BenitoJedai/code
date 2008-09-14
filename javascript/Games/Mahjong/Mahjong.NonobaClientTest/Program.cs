using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mahjong.NonobaClientTest
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Nonoba.DevelopmentServer.Server.StartWithDebugging(100);
		
		}
	}
}
