using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#System/net/System/URI.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/System/System/Uri.cs

    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20120-1/20120817-uri

    [Script(Implements = typeof(global::System.Uri))]
    public class __Uri
    {
        // see: http://blogs.msdn.com/ncl/archive/2010/02/23/system-uri-f-a-q.aspx

        // fixme: shared BCL is not the way to go!  or is it?

        //public static readonly string SchemeDelimiter = "://";

        public string OriginalString { get; set; }

        public string Scheme { get; set; }

        public string PathAndQuery { get; set; }

        public string Host { get; set; }
        public string Fragment { get; set; }

        public string Query { get; set; }
        public string AbsolutePath { get; set; }

        public string[] Segments { get; set; }

        public int Port { get; set; }

        public __Uri(string uriString)
            : this(uriString, UriKind.Absolute)
        {
        }


        UriKind InternalUriKind;

        public __Uri(string uriString, UriKind kind)
        {
            this.InternalUriKind = kind;
            this.OriginalString = uriString;

            // http://localhost/jsc/zmovies

            const string SchemeDelimiter = "://";

            var scheme_i = uriString.IndexOf(SchemeDelimiter);

            if (scheme_i >= 0)
            {
                this.Scheme = uriString.Substring(0, scheme_i);

                var path_i = uriString.IndexOf("/", scheme_i + SchemeDelimiter.Length);

                if (path_i < 0)
                {
                    uriString += "/";
                    path_i = uriString.IndexOf("/", scheme_i + SchemeDelimiter.Length);
                }

                var HostAndPort = uriString.Substring(scheme_i + SchemeDelimiter.Length, path_i - (scheme_i + SchemeDelimiter.Length));

                // ipv6 support?
                var PortStart = HostAndPort.LastIndexOf(":");

                if (PortStart < 0)
                {
                    this.Host = HostAndPort;

                    // sure... 80 thats what the port is
                    // need to implement this!
                    // default port depends on the protocol actually
                    this.Port = 80;
                }
                else
                {
                    this.Host = HostAndPort.Substring(0, PortStart);

                    var PortString = HostAndPort.Substring(PortStart + 1);

                    this.Port = int.Parse(PortString);
                }

                this.PathAndQuery = uriString.Substring(path_i);
            }
            else
            {
                // no host
                Port = -1;
                scheme_i = uriString.IndexOf(":");
                this.Scheme = uriString.Substring(0, scheme_i);
                this.PathAndQuery = uriString.Substring(scheme_i + 1);
            }

            InitializeFragment();
            InitializeQuery();
            InitializeSegments();
        }

        private void InitializeQuery()
        {
            var query_i = this.PathAndQuery.IndexOf("?");

            if (query_i < 0)
            {
                this.Query = "";
                this.AbsolutePath = this.PathAndQuery;
            }
            else
            {
                this.Query = this.PathAndQuery.Substring(query_i + 1);
                this.AbsolutePath = this.PathAndQuery.Substring(0, query_i);
            }
        }

        private void InitializeFragment()
        {
            var FragmentIndex = this.PathAndQuery.IndexOf("#");

            if (FragmentIndex > 0)
            {
                Fragment = this.PathAndQuery.Substring(FragmentIndex + 1);
            }
            else
            {
                Fragment = "";
            }
        }

        private void InitializeSegments()
        {
            var a = new List<string>();

            var j = 0;
            var i = this.AbsolutePath.IndexOf("/");

            while (j >= 0)
            {
                i = this.AbsolutePath.IndexOf("/", j);

                if (i >= 0)
                {
                    a.Add(this.AbsolutePath.Substring(j, i - j + 1));
                    j = i + 1;
                }
                else
                {
                    if (j < this.AbsolutePath.Length - 1)
                        a.Add(this.AbsolutePath.Substring(j));

                    j = -1;
                }
            }

            this.Segments = a.ToArray();
        }

        public static bool operator !=(__Uri uri1, __Uri uri2)
        {
            object o1 = uri1;
            object o2 = uri2;

            if (o1 == null)
                return o1 != o2;

            if (o2 == null)
                return o1 != o2;

            return uri1.OriginalString == uri2.OriginalString;
        }

        public static bool operator ==(__Uri uri1, __Uri uri2)
        {
            object o1 = uri1;
            object o2 = uri2;

            if (o1 == null)
                return o1 == o2;

            if (o2 == null)
                return o1 == o2;

            return uri1.OriginalString == uri2.OriginalString;
        }

        public override string ToString()
        {
            //return this.OriginalString;

            var w = new StringBuilder();

            if (InternalUriKind == UriKind.Absolute)
            {
                // what if we do not have these details?

                w.Append(this.Scheme);
                w.Append(":");

                if (!string.IsNullOrEmpty(Host))
                {
                    w.Append("//");
                    w.Append(this.Host);
                }

                if (Port >= 0)
                {
                    if (this.Port == 80 && this.Scheme == "http")
                    {
                        // omit
                    }
                    else
                    {
                        w.Append(":");
                        w.Append(this.Port);
                    }
                }
            }

            if (string.IsNullOrEmpty(this.PathAndQuery))
                w.Append("/");
            else
                w.Append(this.PathAndQuery);

            return w.ToString();
        }

        public static bool IsWellFormedUriString(string uriString, UriKind uriKind)
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestDropURL\TestDropURL\ApplicationControl.cs
            // a naive implementation
            if (uriString.Contains("://"))
                return true;


            return false;
        }
    }
}
