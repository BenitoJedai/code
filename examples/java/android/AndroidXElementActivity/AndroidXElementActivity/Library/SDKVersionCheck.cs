using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.net;

namespace AndroidXElementActivity.Library
{
    public static class SDKVersionCheck
    {
        public const string SDKVersionLocation = "http://www.jsc-solutions.net/assets/PromotionWebApplication1/jsc.configuration.application";

        public static string GetSDKVersion()
        {
            var w = "";

            try
            {
                w = InternalGetSDKVersion();
            }
            catch
            { 
            }

            return w;
        }

        static string InternalGetSDKVersion()
        {
            var version = "";
            var w = "";

            try
            {
                var url = new java.net.URL(SDKVersionLocation);

                HttpURLConnection con = (HttpURLConnection)url.openConnection();

                int CONNECT_TIMEOUT_MILL = 1000;
                int READ_TIMEOUT_MILL = 600;

                con.setConnectTimeout(CONNECT_TIMEOUT_MILL);
                con.setReadTimeout(READ_TIMEOUT_MILL);


                var i = new java.io.InputStreamReader(con.getInputStream(), "UTF-8");
                var reader = new java.io.BufferedReader(i);

                // can't we just read to the end?
                var line = reader.readLine();
                while (line != null)
                {
                    w += line;
                    w += "\n";

                    line = reader.readLine();
                }
                reader.close();
            }
            catch
            {
                // oops
            }
            //Log.wtf("HttpURLConnection", w);

            if (w.Length > 0)
            {
                var value = w;
                var offset = 0;

                var i = ((java.lang.String)(object)value).indexOf("version=\"", offset) + "version=\"".Length;
                var j = ((java.lang.String)(object)value).indexOf("\"", i);

                var ii = ((java.lang.String)(object)value).indexOf("version=\"", j) + "version=\"".Length;
                var jj = ((java.lang.String)(object)value).indexOf("\"", ii);

                version = ((java.lang.String)(object)value).substring(ii, jj);


            }
            return version;
        }
    }

}
