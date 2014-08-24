using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using java.net;
using java.io;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System.Collections.Specialized;
using System.Threading;
using ScriptCoreLib.Shared.BCLImplementation.System.Net;
using System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.Net
{
    // http://referencesource.microsoft.com/#System/net/System/Net/webclient.cs
    // https://github.com/mono/mono/tree/master/mcs/class/System/System.Net/WebClient.cs


    [Script(Implements = typeof(global::System.Net.WebClient))]
    internal class __WebClient : __Component
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140212/w

        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\WebClient.cs

        public Encoding Encoding { get; set; }

        public WebHeaderCollection Headers { get; set; }


        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Net\UploadValuesCompletedEventArgs.cs
        public event UploadValuesCompletedEventHandler UploadValuesCompleted;

        public void UploadValuesAsync(Uri address, NameValueCollection data)
        {
            // X:\jsc.svn\examples\java\Test\JVMCLRWebClient\JVMCLRWebClient\Program.cs

            //{ addressString = http://my.monese.com:80/xml/GetCurrencyRateBasedOnString }
            //before writeBytes { urlParameters = _06000039_currency=RVVS&WebMethodMetadataToken=06000039&WebMethodMetadataName=GetCurrencyRateBasedOnString }


            new Thread(
                delegate()
                {
                    // http://www.xyzws.com/Javafaq/how-to-use-httpurlconnection-post-data-to-web-server/139

                    //Console.WriteLine("enter UploadValuesAsync");




                    //             String urlParameters =
                    //"fName=" + URLEncoder.encode("???", "UTF-8") +
                    //"&lName=" + URLEncoder.encode("???", "UTF-8")

                    var m = new MemoryStream();

                    try
                    {
                        //Console.WriteLine("before urlParameters");
                        #region urlParameters
                        var urlParameters = new StringBuilder();

                        //Implementation not found for type import :
                        //type: System.Collections.Specialized.NameObjectCollectionBase
                        //method: KeysCollection get_Keys()
                        //Did you forget to add the [Script] attribute?
                        //Please double check the signature!

                        //foreach (string key in data.Keys)

                        foreach (string key in data.AllKeys)
                        {
                            if (urlParameters.Length > 0)
                                urlParameters.Append("&");


                            urlParameters.Append(
                                key + "=" + URLEncoder.encode(data[key], "UTF-8")
                            );

                        }
                        #endregion

                        //Console.WriteLine("after urlParameters");

                        //            Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Net\__WebClient___c__DisplayClass2.java:60: error: unreported exception UnsupportedEncodingException; must be caught or declared to be thrown
                        //builder0.Append(__String.Concat(string1, "=", URLEncoder.encode(this.data.get_Item(string1), "UTF-8")));
                        //                                                               ^

                        var addressString = address.ToString();
                        Console.WriteLine(
                            new { addressString }
                        );

                        var url = new java.net.URL(addressString);

                        var connection = (HttpURLConnection)url.openConnection();

                        // Numeric status code, 403: Forbidden

                        // UserAgent:  Java/1.7.0_45
                        //HtmlElement: Access denied | my.monese.com used CloudFlare to restrict access</title>
                        //- Http: Request, POST /xml/GetCurrencyRateBasedOnString 
                        //   Command: POST
                        // + URI: /xml/GetCurrencyRateBasedOnString
                        //   ProtocolVersion: HTTP/1.1
                        // + ContentType:  application/x-www-form-urlencoded
                        //   Cache-Control:  no-cache
                        //   Pragma:  no-cache
                        //   UserAgent:  Java/1.7.0_45
                        //   Host:  my.monese.com
                        //   Accept:  text/html, image/gif, image/jpeg, *; q=.2, */*; q=.2
                        //   Connection:  keep-alive
                        //   ContentLength:  106
                        //   HeaderEnd: CRLF

                        //- Http: Request, POST /xml/GetCurrencyRateBasedOnString 
                        //   Command: POST
                        // + URI: /xml/GetCurrencyRateBasedOnString
                        //   ProtocolVersion: HTTP/1.1
                        // + ContentType:  application/x-www-form-urlencoded
                        //   Host:  my.monese.com
                        //   ContentLength:  106
                        //   Expect:  100-continue
                        //   Connection:  Keep-Alive
                        //   HeaderEnd: CRLF


                        //                error { Message = Connection timed out: connect, StackTrace = java.net.ConnectException: Connection timed out: connect
                        //at java.net.DualStackPlainSocketImpl.connect0(Native Method)

                        connection.setRequestMethod("POST");

                        // https://issues.jenkins-ci.org/browse/JENKINS-21033?page=com.atlassian.jira.plugin.system.issuetabpanels:all-tabpanel
                        // https://github.com/scalaj/scalaj-http/issues/27

                        connection.setRequestProperty("User-Agent", "WebClient");
                        connection.setRequestProperty("Accept", "application/xml");

                        connection.setRequestProperty(
                            "Content-Type", "application/x-www-form-urlencoded"
                             );

                        var bytes = Encoding.UTF8.GetBytes(
                            urlParameters.ToString()
                        );


                        connection.setRequestProperty("Content-Length", "" + bytes.Length);

                        //connection.setRequestProperty("Content-Language", "en-US");  

                        connection.setUseCaches(false);
                        connection.setDoInput(true);
                        connection.setDoOutput(true);


                        if (Headers != null)
                        {
                            foreach (string key in Headers.AllKeys)
                                connection.addRequestProperty(key, Headers[key]);
                        }

                        Console.WriteLine("before writeBytes " + new { bytes.Length });

                        #region Send request
                        var wr = new DataOutputStream(
                                    connection.getOutputStream());

                        wr.write((sbyte[])(object)bytes);

                        //wr.writeBytes(urlParameters.ToString());
                        wr.flush();
                        wr.close();
                        #endregion

                        //error { Message = Server returned HTTP response code: 403 for URL: 
                        //        at sun.net.www.protocol.http.HttpURLConnection.getInputStream(Unknown Source)
                        //        at ScriptCoreLibJava.BCLImplementation.System.Net.__WebClient___c__DisplayClass2._UploadValuesAsync_b__1(__WebClient___c__DisplayClass2.java:82)

                        Console.WriteLine("before Read ");

                        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\WebClient.cs

                        //Get Response	
                        // namespace java.io
                        var xis = connection.getInputStream().ToNetworkStream();

                        var buffer = new byte[0x10000];

                        while (xis.DataAvailable)
                        {
                            var s = xis.Read(buffer, 0, buffer.Length);

                            if (s > 0)
                                m.Write(buffer, 0, s);
                        }

                        xis.Close();
                        if (connection != null)
                        {
                            connection.disconnect();
                        }
                        //xis.Read(
                        //xis.readall
                    }
                    catch (Exception ex)
                    {
                        // ?
                        Console.WriteLine("error " + new { ex.Message, ex.StackTrace });

                    }

                    //Thread.Sleep(666);
                    var Result = m.ToArray();

                    Console.WriteLine("yield UploadValuesAsync " + new { Result.Length });


                    RaiseUploadValuesCompleted(Result);
                }
            ) { IsBackground = true }.Start();
        }

        private void RaiseUploadValuesCompleted(byte[] Result)
        {
            //                    - javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRWebClient\Program.java
            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Net\__WebClient___c__DisplayClass3.java:106: error: UploadValuesCompleted has private access in __WebClient
            //        if (!(this.__4__this.UploadValuesCompleted == null))
            //                            ^


            if (UploadValuesCompleted != null)
                UploadValuesCompleted(
                    this,
                    (UploadValuesCompletedEventArgs)(object)new __UploadValuesCompletedEventArgs
                    {
                        Result = Result
                    }
                    );
        }



        #region DownloadString
        public string DownloadString(string u)
        {
            return DownloadString(new Uri(u));
        }

        public string DownloadString(Uri u)
        {
            var w = new StringBuilder();

            try
            {
                var url = new java.net.URL(u.ToString());
                var i = new java.io.InputStreamReader(url.openStream(), "UTF-8");
                var reader = new java.io.BufferedReader(i);

                // can't we just read to the end?
                var line = reader.readLine();
                while (line != null)
                {
                    w.AppendLine(line);

                    line = reader.readLine();
                }
                reader.close();
            }
            catch
            {
                // oops
            }

            return w.ToString();
        }
        #endregion

        #region UploadString
        public string UploadString(string u, string method, string data)
        {
            return UploadString(new Uri(u), data, method);
        }

        public string UploadString(Uri u, string method, string data)
        {
            var w = new StringBuilder();

            HttpURLConnection conn = null;

            try
            {
                var url = new java.net.URL(u.ToString());

                conn = (HttpURLConnection)url.openConnection();
                conn.setDoOutput(true);
                conn.setDoInput(true);
                conn.setInstanceFollowRedirects(false);
                conn.setRequestMethod(method);
                conn.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
                conn.setRequestProperty("charset", "utf-8");
                conn.setRequestProperty("content-length", "" + data.Length);
                conn.setUseCaches(false);

                try
                {

                    if (Headers != null)
                    {
                        foreach (string key in Headers.AllKeys)
                            conn.addRequestProperty(key, Headers[key]);
                    }
                }
                catch (Exception e)
                {
                    //System.Console.WriteLine("ERROR: Failed to write headers. Exception was:" + e.Message);
                }

                DataOutputStream wr = new DataOutputStream(conn.getOutputStream());
                wr.writeBytes(data);
                wr.flush();
                wr.close();



                //var i = new java.io.InputStreamReader(url.openStream(), "UTF-8");
                var i = new java.io.InputStreamReader(conn.getInputStream(), "UTF-8");
                var reader = new java.io.BufferedReader(i);

                // can't we just read to the end?
                var line = reader.readLine();
                while (line != null)
                {
                    w.AppendLine(line);

                    line = reader.readLine();
                }
                reader.close();
            }
            catch
            {
                // oops
            }

            if (conn != null)
                conn.disconnect();

            return w.ToString();
        }
        #endregion



    }
}
