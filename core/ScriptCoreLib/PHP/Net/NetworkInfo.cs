using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.PHP.IO;

namespace ScriptCoreLib.PHP.Net
{
    [Script]
    public static class NetworkInfo
    {
        [Script]
        class IPInfo
        {
            public byte[] Bytes = new byte[4];

            public string Text;

            public int Address;
            public int Subnet;


            public static IPInfo FromString(string e)
            {

                string[] x = IArray.Split(e, ".");

                if (x.Length != 4)
                {
                    Native.Break("invalid IPv4, environment broken?");
                }


                IPInfo n = new IPInfo();

                n.Text = e;

                n.Subnet = 0;
                n.Address = 0;

                for (int i = 0; i < 4; i++)
                {
                    n.Bytes[i] = byte.Parse(x[i]);

                    //Native.Message("#" + i, n.Bytes[i] + "");

                    if (i < 3)
                        n.Subnet += n.Bytes[i] << (24 - i * 8);
                }

                n.Address = n.Subnet + n.Bytes[3];

                //Native.Message("subnet", Native.API.dechex(n.Subnet) + " - " + n.Text);

                return n;
            }
        }

        public static string ServerIP = Native.SuperGlobals.Server[Native.SuperGlobals.ServerVariables.SERVER_ADDR];
        public static string ClientIP = Native.SuperGlobals.Server[Native.SuperGlobals.ServerVariables.REMOTE_ADDR];

        public static bool IsLocalRequest
        {
            get
            {

                IPInfo a = IPInfo.FromString(ServerIP);



                IPInfo b = IPInfo.FromString(ClientIP);


                return a.Subnet == b.Subnet;
            }
        }

        public static string PublicLocation
        {
            get
            {
                return "http://" + Native.SuperGlobals.Server[Native.SuperGlobals.ServerVariables.HTTP_HOST]
                + Native.API.dirname(Native.SuperGlobals.Server[Native.SuperGlobals.ServerVariables.SCRIPT_NAME]);
            }
        }

        public static string RequestMethod
        {
            get
            {
                string e = Native.SuperGlobals.Server[Native.SuperGlobals.ServerVariables.REQUEST_METHOD];

                return e.ToLower();
            }
        }

        public static string UserAgent
        {
            get
            {
                string e = Native.SuperGlobals.Server[Native.SuperGlobals.ServerVariables.HTTP_USER_AGENT];

                return e;
            }
        }

        public static string Accept
        {
            get
            {
                string e = Native.SuperGlobals.Server[Native.SuperGlobals.ServerVariables.HTTP_ACCEPT];

                return e;
            }
        }

    }
}
