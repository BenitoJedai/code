using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new WebClient();

            // http://stackoverflow.com/questions/4415175/an-operation-on-a-socket-could-not-be-performed-because-the-system-lacked-suffi
            // find a process and kill it..

            // 		Message	"An operation on a socket could not be performed because the system lacked sufficient buffer space or because a queue was full 173.194.113.0:80"	string


            var x = c.DownloadString("http://google.com/");

        }
    }
}
