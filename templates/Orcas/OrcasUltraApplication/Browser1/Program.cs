using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.WebHost;

namespace Browser1
{
	class Program
	{
		static void Main(string[] args)
		{
			// http://haacked.com/archive/2006/12/12/using_webserver.webdev_for_unit_tests.aspx
			// http://weblogs.asp.net/jdanforth/archive/2003/12/16/43841.aspx
			// C:\Windows\assembly\GAC_32\WebDev.WebHost\9.0.0.0__b03f5f7f11d50a3a
			// http://rmanimaran.wordpress.com/2008/08/05/get-a-copy-of-dll-in-gac-or-add-reference-to-a-dll-in-gac/

			var s = new Server(3333, "/", @"W:\jsc.svn\templates\Orcas\OrcasUltraApplication\OrcasUltraApplication\bin\Release\staging\OrcasUltraApplication.UltraWebService\staging.net");

			s.Start();

			Console.ReadKey(true);

			s.Stop();
		}
	}
}
